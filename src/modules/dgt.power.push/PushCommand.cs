// Copyright (c) DIGITALL Nature. All rights reserved
// DIGITALL Nature licenses this file to you under the Microsoft Public License.

using System.Globalization;
using System.Reflection;
using System.Runtime.InteropServices;
using dgt.power.common;
using dgt.power.dataverse;
using dgt.power.push.Base;
using dgt.power.push.Logic;
using dgt.power.push.Model;
using Microsoft.Xrm.Sdk;
using Spectre.Console;
using Spectre.Console.Cli;
using Assembly = dgt.power.push.Model.Assembly;

namespace dgt.power.push;

public class PushCommand : Command<PushVerb>, IPowerLogic
{
    private readonly IOrganizationService _connection;
    private readonly WebresourcesProcessor _webresourcesProcessor;
    private readonly ITracer _tracer;
    private readonly IAnsiConsole _console;

    public PushCommand(ITracer tracer, IOrganizationService connection, WebresourcesProcessor webresourcesProcessor, IAnsiConsole console)
    {
        _tracer = tracer;
        _connection = connection;
        _webresourcesProcessor = webresourcesProcessor;
        _console = console;
    }

    protected override int Execute(CommandContext context, PushVerb settings, CancellationToken cancellationToken)
    {
        _tracer.Start(this);

        _console.Status()
            .Spinner(Spinner.Known.Pong)
            .SpinnerStyle(Style.Parse("green bold"))
            .Start("Connect to XRM...", ctx =>
            {
                // ReSharper disable once ObjectCreationAsStatement - Justification:  Set Earlybound Resolution fixed
#pragma warning disable CA1806
#pragma warning disable S1848
                new DataContext(_connection);
#pragma warning restore S1848
#pragma warning restore CA1806


                if (File.Exists(settings.Target))
                {
                    ProcessAssemblyFile(settings, ctx);
                }
                else if (Directory.Exists(settings.Target))
                {
                    ProcessWebresourcesFolder(settings, ctx);
                }
                else
                {
                    _console.MarkupLine("[Red] File or Folder not existing - aborting[/]");
                }

                ctx.Status("Finishing");
            });

        return _tracer.End(this, true) ? 0 : -1;
    }

    private void ProcessAssemblyFile(PushVerb settings, StatusContext ctx)
    {
        IReadOnlyList<Assembly?> assemblies;
        Guid? currentPackageId = null;
        using var modelBuilder = new AssemblyModelBuilder(_connection, _console);
        using var processor = new AssemblyProcessor(_connection, _console);

        var solutionPrefix = processor.GetSolutionPrefix(settings.Solution);

        if (settings.Target.EndsWith(".nupkg", StringComparison.InvariantCultureIgnoreCase))
        {
            // Dependent Plugin
            _console.MarkupLine(CultureInfo.InvariantCulture, "Package found - unpack");

            var packageLocal = modelBuilder.BuildPackageFromFile(settings.Target);
            if (packageLocal == null)
            {
                _console.MarkupLine("[red]Failed to read package metadata - aborting[/]");
                return;
            }

            var packageCrm = modelBuilder.BuildPackageFromCrm(packageLocal.Name, packageLocal.Version);

            if (packageCrm.State == AssemblyState.Create)
            {
                ctx.Status("CreatePluginPackage");
                if (solutionPrefix == "new")
                {
                    _console.MarkupLine(CultureInfo.InvariantCulture, "[yellow] Solution not set or found - Package will have prefix 'new' [/]");
                }

                packageCrm = processor.CreatePluginPackage(packageLocal, solutionPrefix, settings.Solution);
            }
            else
            {
                ctx.Status("UpdatePluginPackage");
                packageCrm = processor.UpdatePluginPackage(packageCrm, packageLocal, settings.Publish, settings.Solution);
            }

            assemblies = modelBuilder.BuildAssemblyFromPackage(packageCrm);
            currentPackageId = packageCrm.Id;
        }
        else
        {
            var env = new List<string>(Directory.GetFiles(RuntimeEnvironment.GetRuntimeDirectory(),"*.dll").Concat(Directory.GetFiles(Path.GetDirectoryName(typeof(PushCommand).Assembly.Location)!,"*.dll")));
            using var loadContext = new MetadataLoadContext(new PathAssemblyResolver(env));
            assemblies = new List<Assembly?> { modelBuilder.BuildAssemblyFromDll(settings.Target,loadContext) };
        }


        foreach (var localAssembly in assemblies)
        {
            if (localAssembly == null)
            {
                continue;
            }

            if (localAssembly.Type == AssemblyType.Undefined)
            {
                _console.MarkupLine(CultureInfo.InvariantCulture, "Assembly [bold green]{0} ({1})[/] [bold red]does not contain[/] plugins or workflows - aborting", localAssembly.Name, localAssembly.Version);
                continue;
            }

            _console.MarkupLine(CultureInfo.InvariantCulture, "Check Assembly [bold green]{0} ({1})[/]", localAssembly.Name, localAssembly.Version);

            ctx.Status("BuildFromCrm");
            var crmAssembly = modelBuilder.BuildAssemblyFromCrm(localAssembly.Name, localAssembly.Version);
            var state = crmAssembly.State;

            if (crmAssembly.State == AssemblyState.Create || crmAssembly.State == AssemblyState.Upgrade)
            {
                ctx.Status("CreatePluginAssembly");
                crmAssembly = processor.CreatePluginAssembly(localAssembly, settings.Solution);
            }
            else if (crmAssembly.State == AssemblyState.Update && !localAssembly.Equals(crmAssembly))
            {
                ctx.Status("UpdatePluginAssembly");
                crmAssembly = processor.UpdatePluginAssembly(localAssembly, crmAssembly, settings.Solution,settings.Publish);
            }

            if (localAssembly.Type.HasFlag(AssemblyType.Workflow))
            {
                ctx.Status("UpsertAndPurgeWorkflowTypes");
                processor.UpsertAndPurgeWorkflowTypes(localAssembly, crmAssembly);
            }

            // Link managed identity if attribute is present
            if (!string.IsNullOrWhiteSpace(localAssembly.ManagedIdentityClientId))
            {
                ctx.Status("LinkManagedIdentity");
                processor.LinkManagedIdentityToAssembly(crmAssembly.Id, localAssembly.ManagedIdentityClientId, localAssembly.ManagedIdentityTenantId);

                // For package deployments, also link the managed identity to the package entity itself
                if (currentPackageId.HasValue)
                {
                    ctx.Status("LinkManagedIdentityToPackage");
                    processor.LinkManagedIdentityToPackage(currentPackageId.Value, localAssembly.ManagedIdentityClientId, localAssembly.ManagedIdentityTenantId);
                    currentPackageId = null; // only link once per package (first assembly with the attribute wins)
                }
            }

            if (localAssembly.Type.HasFlag(AssemblyType.Plugin))
            {
                ctx.Status("UpsertAndPurgePluginTypes");
                crmAssembly = processor.UpsertAndPurgePluginTypes(localAssembly, crmAssembly, settings.Solution);

                if (localAssembly.Type.HasFlag(AssemblyType.PowerPlugin))
                {
                    ctx.Status("UpsertAndPurgePluginSteps");
                    crmAssembly = processor.UpsertAndPurgePluginSteps(localAssembly, crmAssembly, settings.Solution);
                    ctx.Status("UpsertAndPurgePluginStepImages");
                    processor.UpsertAndPurgePluginStepImages(localAssembly, crmAssembly);
                }
            }

            if (settings.DeleteOnUpgrade  && state == AssemblyState.Upgrade && localAssembly.Type.HasFlag(AssemblyType.Plugin))
            {
                ctx.Status("Purge outdated plugin types");
                var outdated = modelBuilder.BuildOutdatedAssemblyContentFromCrm(localAssembly.Name);
                foreach (var assemblyContent in outdated)
                {
                    foreach (var pluginType in assemblyContent.PluginTypes)
                    {
                        processor.DeletePluginType(pluginType);
                    }

                    processor.DeletePluginAssembly(assemblyContent.Id);
                }
            }
        }
    }

    private void ProcessWebresourcesFolder(PushVerb settings, StatusContext ctx)
    {
        var statusCallback = (string status) => {
            ctx.Status(status);
        };

        _webresourcesProcessor.Process(settings, statusCallback);
    }
}
