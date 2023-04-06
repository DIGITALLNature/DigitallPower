using System.Globalization;
using dgt.power.dataverse;
using dgt.power.push.Model;
using Microsoft.Xrm.Sdk;
using Microsoft.Xrm.Sdk.Client;
using Spectre.Console;

namespace dgt.power.push.Logic;

internal class WebresourcesProcessor
{
    private readonly IOrganizationService _connection;

    private readonly List<WebresourcesPattern> _knownWebresourcesPattern = new()
    {
        new(WebResource.Options.WebResourceType.Webpage_HTML_, "*.html"),
        new(WebResource.Options.WebResourceType.StyleSheet_CSS_, "*.css"),
        new(WebResource.Options.WebResourceType.Script_JScript_, "*.js"),
        new(WebResource.Options.WebResourceType.Data_XML_, "*.xml"),
        new(WebResource.Options.WebResourceType.PNGFormat, "*.png"),
        new(WebResource.Options.WebResourceType.JPGFormat, "*.jpg"),
        new(WebResource.Options.WebResourceType.GIFFormat, "*.gif"),
        new(WebResource.Options.WebResourceType.Silverlight_XAP_, "*.xap"),
        new(WebResource.Options.WebResourceType.StyleSheet_XSL_, "*.xsl"),
        new(WebResource.Options.WebResourceType.ICOFormat, "*.ico"),
        new(WebResource.Options.WebResourceType.VectorFormat_SVG_, "*.svg"),
        new(WebResource.Options.WebResourceType.String_RESX_, "*.resx")
    };

    private readonly DataContext _context;
    private string _solutionPrefix;
    private List<Webresources> _files;
    private Guid? _solutionId;

    public WebresourcesProcessor(IOrganizationService connection)
    {
        _connection = connection;
        _context = new DataContext(connection) { MergeOption = MergeOption.NoTracking };
    }

    public void DiscoverWebresources(string folder)
    {
        var folderInfo = Path.GetFullPath(folder);

        var groups = _knownWebresourcesPattern
            .Select(pattern =>
                new
                {
                    pattern.Type,
                    Files = Directory.EnumerateFiles(folderInfo, pattern.FilePattern, SearchOption.AllDirectories)
                });

        _files = (from @group in groups from file in @group.Files select new Webresources(@group.Type, file, folderInfo, _solutionPrefix)).ToList();

        foreach (var webresource in _files)
        {
            CheckStatus(webresource);
            AnsiConsole.MarkupLine(CultureInfo.InvariantCulture, "Found webresource [bold green]{0} ({1})[/] <{2}>", webresource.DisplayName, webresource.Name, webresource.State);
        }
    }

    private void CheckStatus(Webresources resource)
    {
        var existing = _context.WebResourceSet.Where(wr => wr.WebResourceType!.Value == resource.Type && wr.Name == resource.Name).Select(wr => new {wr.Id, wr.Content}).SingleOrDefault();

        if (existing == null)
        {
            resource.State = WebresourceState.Create;
            return;
        }

        resource.State = existing.Content != resource.Content ? WebresourceState.Update : WebresourceState.Up2date;
        resource.XrmId = existing.Id;
    }

    private Guid? GetSolution(string solution, out string prefix)
    {
        var solutionDetails = (from s in _context.SolutionSet
            join p in _context.PublisherSet on s.PublisherId.Id equals p.Id
            where s.UniqueName == solution
            select new {s.SolutionId, p.CustomizationPrefix}).SingleOrDefault();

        if (solutionDetails == null)
        {
            AnsiConsole.WriteLine(CultureInfo.InvariantCulture,"Solution {0} [Red]not found[/]!", solution);
        }

        prefix = solutionDetails?.CustomizationPrefix ?? "new";
        return solutionDetails.SolutionId;
    }

    public void LoadSolution(string solution, out bool defaultSolution)
    {
        _solutionPrefix = "new";
        if (!string.IsNullOrWhiteSpace(solution))
        {
            _solutionId = GetSolution(solution, out _solutionPrefix);
        }

        defaultSolution = !_solutionId.HasValue;
    }

    public void UpsertResources()
    {
        foreach (var resource in _files.Where(f => f.State == WebresourceState.Create))
        {
            AnsiConsole.MarkupLine(CultureInfo.InvariantCulture, "Create WebResource: [green]{0}[/] for [bold]{1}[/]", resource.Name, resource.Type);
            var id = _connection.Create(new WebResource
            {
                Content = resource.Content,
                Description = $"Upserted with DGTP: {resource.Hash}",
                DisplayName = resource.DisplayName,
                Name = resource.Name,
                WebResourceType = new OptionSetValue(resource.Type)
            });
            AnsiConsole.MarkupLine(CultureInfo.InvariantCulture, "    => {0}", id);
        }

        foreach (var resource in _files.Where(f => f.State == WebresourceState.Update))
        {
            AnsiConsole.MarkupLine(CultureInfo.InvariantCulture, "Update WebResource: [green]{0}[/] for [bold]{1}[/]", resource.Name, resource.Type);
            _connection.Update(new WebResource
            {
                Id = resource.XrmId,
                Content = resource.Content,
                Description = $"Upserted with DGTP: {resource.Hash}",
            });
        }
    }

    public void DiscoverObsoleteWebresources()
    {
        var webresourcesInSolution = from wr in _context.WebResourceSet
            join sc in _context.SolutionComponentSet on wr.WebResourceId equals sc.ObjectId
            where sc.SolutionId.Id == _solutionId
            where wr.IsManaged == false
            select new { wr.WebResourceId, wr.WebResourceType, wr.Name };

        foreach (var wris in webresourcesInSolution)
        {
            if (_files.SingleOrDefault(f => f.Type == wris.WebResourceType.Value && f.Name == wris.Name) == null)
            {
                AnsiConsole.MarkupLine(CultureInfo.InvariantCulture,"Found obsolete: [Green]{0}[/] <{1}>",wris.Name,wris.WebResourceType);
                _files.Add(new Webresources(wris.WebResourceType.Value, wris.Name, wris.WebResourceId.Value));
            }
        }
    }

    public void DeleteResources()
    {
        foreach (var resource in _files.Where(f => f.State == WebresourceState.Delete))
        {
            AnsiConsole.MarkupLine(CultureInfo.InvariantCulture,"Delete obsolete: [Red]{0}[/] <{1}>",resource.Name,resource.Type);
            _connection.Delete(WebResource.EntityLogicalName, resource.XrmId);
        }
    }
}
