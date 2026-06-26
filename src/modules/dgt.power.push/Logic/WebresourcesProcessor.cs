// Copyright (c) DIGITALL Nature. All rights reserved
// DIGITALL Nature licenses this file to you under the Microsoft Public License.

using System.Globalization;
using dgt.power.common;
using dgt.power.dataverse;
using dgt.power.push.Base;
using dgt.power.push.Model;
using Microsoft.Crm.Sdk.Messages;
using Microsoft.Xrm.Sdk;
using Microsoft.Xrm.Sdk.Client;
using Spectre.Console;

namespace dgt.power.push.Logic;

public class WebresourcesProcessor(
    IOrganizationService connection,
    IConfigResolver configResolver,
    IAnsiConsole console)
    : IDisposable
{
    private readonly WebresourcesPattern[] _knownWebresourcesPattern =
    [
        new WebresourcesPattern(WebResource.Options.WebResourceType.WebpageHTML, "*.html"),
        new WebresourcesPattern(WebResource.Options.WebResourceType.StyleSheetCSS, "*.css"),
        new WebresourcesPattern(WebResource.Options.WebResourceType.ScriptJScript, "*.js"),
        new WebresourcesPattern(WebResource.Options.WebResourceType.DataXML, "*.xml"),
        new WebresourcesPattern(WebResource.Options.WebResourceType.PNGFormat, "*.png"),
        new WebresourcesPattern(WebResource.Options.WebResourceType.JPGFormat, "*.jpg"),
        new WebresourcesPattern(WebResource.Options.WebResourceType.GIFFormat, "*.gif"),
        new WebresourcesPattern(WebResource.Options.WebResourceType.SilverlightXAP, "*.xap"),
        new WebresourcesPattern(WebResource.Options.WebResourceType.StyleSheetXSL, "*.xsl"),
        new WebresourcesPattern(WebResource.Options.WebResourceType.ICOFormat, "*.ico"),
        new WebresourcesPattern(WebResource.Options.WebResourceType.VectorFormatSVG, "*.svg"),
        new WebresourcesPattern(WebResource.Options.WebResourceType.StringRESX, "*.resx")
    ];

    private readonly DataContext _context = new(connection) { MergeOption = MergeOption.NoTracking };

    internal void Process(PushVerb settings, Action<string>? statusCallback) {
        statusCallback?.Invoke("Check settings");

        Dictionary<string, string>? fileMappings = null;
        if (!string.IsNullOrEmpty(settings.Config))
        {
            console.MarkupLine(CultureInfo.InvariantCulture, "Loading config file [bold]{0}[/]", settings.Config);
            if (configResolver.TryGetConfigFile<WebresourceConfig>(settings.Config, out var config))
            {
                console.MarkupLine(CultureInfo.InvariantCulture, "[grey] > Found [green]{0}[/] mappings[/]", config.Maps?.Count ?? 0);
                fileMappings = config.Maps;
            }
            else
            {
                throw new ArgumentException($"Config file {settings.Config} not found or invalid");
            }
        }

        var (prefix, solutionId) = GetSolutionDetails(settings.Solution);
        if (settings.DeleteObsolete && solutionId == null)
        {
            throw new ArgumentException("Delete Obsolete without proper solution set - aborting");
        }

        SolutionWebresourceInfo[]? solutionContents = null;
        if (solutionId.HasValue)
        {
            statusCallback?.Invoke("Load solution contents");
            solutionContents = LoadSolutionContents(solutionId.Value);
        }

        statusCallback?.Invoke("Discover webresources");
        var localWebresources = DiscoverWebresources(settings.Target, prefix, fileMappings);

        statusCallback?.Invoke("Upsert webresources");
        UpsertResources(localWebresources, solutionId, settings.Solution, settings.Publish, solutionContents);

        if (settings.DeleteObsolete)
        {
            statusCallback?.Invoke("Discover obsolete webresources");
            var obsoleteWebresources = DiscoverObsoleteWebresources(localWebresources, solutionContents!);

            statusCallback?.Invoke("Delete webresources");
            DeleteResources(obsoleteWebresources);
        }
    }

    private (string prefix, Guid? solutionId) GetSolutionDetails(string solution)
    {
        if (!string.IsNullOrWhiteSpace(solution))
        {
            return LoadSolution(solution);
        }

        return ("new", null);
    }

    private SolutionWebresourceInfo[] LoadSolutionContents(Guid solutionId)
    {
        return (from wr in _context.WebResourceSet
            join sc in _context.SolutionComponentSet on wr.WebResourceId equals sc.ObjectId
            where sc.SolutionId!.Id == solutionId
            where wr.IsManaged == false
            select new SolutionWebresourceInfo(wr.WebResourceId!.Value, wr.WebResourceType!.Value, wr.Name!))
            .ToArray();
    }

    private (string prefix, Guid solutionId) LoadSolution(string solutionUniqueName)
    {
        var solutionDetails = (from s in _context.SolutionSet
            join p in _context.PublisherSet on s.PublisherId!.Id equals p.Id
            where s.UniqueName == solutionUniqueName
            select new { s.SolutionId, p.CustomizationPrefix }).SingleOrDefault();

        if (solutionDetails == null)
        {
            throw new ArgumentException($"Solution {solutionUniqueName} not found");
        }

        return (solutionDetails.CustomizationPrefix, solutionDetails.SolutionId!.Value);
    }

    private Webresources[] DiscoverWebresources(string folder, string solutionPrefix, IDictionary<string, string>? fileMapping)
    {
        var folderInfo = Path.GetFullPath(folder);

        var groups = _knownWebresourcesPattern
            .Select(pattern =>
                new
                {
                    pattern.Type,
                    Files = Directory.EnumerateFiles(folderInfo, pattern.FilePattern, SearchOption.AllDirectories)
                });

        var webresources = (from @group in groups from file in @group.Files select Webresources.FromFile(file, folderInfo, @group.Type, solutionPrefix, fileMapping, console)).ToArray();

        foreach (var webresource in webresources)
        {
            EnrichState(webresource);
            console.MarkupLine(CultureInfo.InvariantCulture, "Found webresource [bold green]{0} ({1})[/] <{2}>", webresource.DisplayName, webresource.Name, webresource.State!);
        }

        return webresources;
    }

    private void EnrichState(Webresources resource)
    {
        var existing = _context.WebResourceSet
            .Where(wr => wr.WebResourceType!.Value == resource.Type && wr.Name == resource.Name)
            .Select(wr => new { wr.Id, wr.Content })
            .SingleOrDefault();

        if (existing == null)
        {
            resource.State = WebresourceState.Create;
            return;
        }

        resource.State = existing.Content != resource.Content ? WebresourceState.Update : WebresourceState.Up2Date;
        resource.XrmId = existing.Id;
    }

    private void UpsertResources(Webresources[] webresources, Guid? solutionId, string? solutionName, bool publish, IReadOnlyCollection<SolutionWebresourceInfo>? solutionContents)
    {
        foreach (var resource in webresources.Where(f => f.State == WebresourceState.Create))
        {
            console.MarkupLine(CultureInfo.InvariantCulture, "Create WebResource: [green]{0}[/] for [bold]{1}[/]", resource.Name, resource.Type);
            var id = connection.Create(new WebResource
            {
                Content = resource.Content,
                Description = $"Upserted with DGTP: {resource.Hash}",
                DisplayName = resource.DisplayName,
                Name = resource.Name,
                WebResourceType = new OptionSetValue(resource.Type)
            });
            console.MarkupLine(CultureInfo.InvariantCulture, "    => {0}", id);

            if (solutionId.HasValue)
            {
                AddResourceToSolution(id, resource.Name, solutionName!);
            }
        }

        foreach (var resource in webresources.Where(f => f.State == WebresourceState.Update))
        {
            console.MarkupLine(CultureInfo.InvariantCulture, "Update WebResource: [green]{0}[/] for [bold]{1}[/]", resource.Name, resource.Type);
            connection.Update(new WebResource
            {
                Id = resource.XrmId!.Value,
                Content = resource.Content,
                Description = $"Upserted with DGTP: {resource.Hash}"
            });

            if (solutionId.HasValue && !IsAlreadyInSolution(resource.XrmId!.Value, solutionContents))
            {
                AddResourceToSolution(resource.XrmId!.Value, resource.Name, solutionName!);
            }

            if (publish)
            {
                console.MarkupLine(CultureInfo.InvariantCulture, "Publish WebResource: [green]{0}[/] for [bold]{1}[/]", resource.Name, resource.Type);
                connection.Execute(new PublishXmlRequest
                {
                    ParameterXml = $"<importexportxml><webresources><webresource>{resource.XrmId}</webresource></webresources></importexportxml>"
                });
            }
        }

        if (solutionId.HasValue)
        {
            foreach (var resource in webresources.Where(f => f.State == WebresourceState.Up2Date))
            {
                if (!IsAlreadyInSolution(resource.XrmId!.Value, solutionContents))
                {
                    AddResourceToSolution(resource.XrmId!.Value, resource.Name, solutionName!);
                }
            }
        }
    }

    private static bool IsAlreadyInSolution(Guid resourceId, IReadOnlyCollection<SolutionWebresourceInfo>? solutionContents)
        => solutionContents?.Any(s => s.WebResourceId == resourceId) ?? false;

    private void AddResourceToSolution(Guid resourceId, string resourceName, string solutionName)
    {
        var addReq = new AddSolutionComponentRequest
        {
            AddRequiredComponents = false,
            ComponentType = SolutionComponent.Options.ComponentType.WebResource,
            ComponentId = resourceId,
            SolutionUniqueName = solutionName
        };

        try
        {
            console.MarkupLine(CultureInfo.InvariantCulture, "Adding webresource [green]{0}[/] to solution [bold]{1}[/]", resourceName, solutionName);
            connection.Execute(addReq);

            console.MarkupLine(CultureInfo.InvariantCulture, $" {Emoji.Known.RightArrow} [green]Added[/]");
        }
        catch (Exception ex) when (ex is not OutOfMemoryException and not StackOverflowException)
        {
            console.MarkupLine(CultureInfo.InvariantCulture, $" {Emoji.Known.RightArrow} [red]Failed[/]");
            console.WriteException(ex);
        }
    }

    private IEnumerable<Webresources> DiscoverObsoleteWebresources(Webresources[] webresources, IReadOnlyCollection<SolutionWebresourceInfo> solutionContents)
    {
        foreach (var webresource in solutionContents)
        {
            if (webresources.SingleOrDefault(f => f.Type == webresource.WebResourceType && f.Name == webresource.Name) == null)
            {
                console.MarkupLine(CultureInfo.InvariantCulture, "Found obsolete: [Green]{0}[/] <{1}>", webresource.Name, webresource.WebResourceType);
                yield return new Webresources(webresource.WebResourceType, webresource.Name, WebresourceState.Delete, webresource.WebResourceId);
            }
        }
    }

    private void DeleteResources(IEnumerable<Webresources> webResources)
    {
        foreach (var resource in webResources)
        {
            console.MarkupLine(CultureInfo.InvariantCulture, "Delete obsolete: [Red]{0}[/] <{1}>", resource.Name, resource.Type);
            connection.Delete(WebResource.EntityLogicalName, resource.XrmId!.Value);
        }
    }

    public void Dispose()
    {
        Dispose(true);
        GC.SuppressFinalize(this);
    }

    protected virtual void Dispose(bool disposing)
    {
        if (disposing)
        {
            _context.Dispose();
        }
    }
}
