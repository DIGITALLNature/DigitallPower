using System.Diagnostics.CodeAnalysis;
using System.Globalization;
using dgt.power.common;
using dgt.power.dataverse;
using dgt.power.push.Base;
using dgt.power.push.Logic;
using dgt.power.push.Model;
using Microsoft.Xrm.Sdk;
using NuGet.Packaging;
using Spectre.Console;
using Spectre.Console.Cli;

namespace dgt.power.push;

public class PushCommand : Command<PushVerb>, IPowerLogic
{
    private readonly IConfigResolver _configResolver;
    private readonly IOrganizationService _connection;
    private readonly ITracer _tracer;

    public PushCommand(ITracer tracer, IConfigResolver configResolver, IOrganizationService connection)
    {
        _tracer = tracer;
        _configResolver = configResolver;
        _connection = connection;
    }

    public override int Execute([NotNull] CommandContext context, [NotNull] PushVerb settings)
    {
        _tracer.Start(this);

        AnsiConsole.Status()
            .Spinner(Spinner.Known.Pong)
            .SpinnerStyle(Style.Parse("green bold"))
            .Start("Connect to XRM...", ctx =>
            {
                // ReSharper disable once ObjectCreationAsStatement - Justification:  Set Earlybound Resolution fixed
#pragma warning disable CA1806
                new DataContext(_connection);
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
                    AnsiConsole.MarkupLine("[Red] File or Folder not existing - aborting[/]");
                }

                ctx.Status("Finishing");
            });

        return _tracer.End(this, true) ? 0 : -1;
    }

    private void ProcessAssemblyFile(PushVerb settings, StatusContext ctx)
    {
        List<Assembly?> assemblies;
        var modelBuilder = new AssemblyModelBuilder(_connection);
        var processor = new AssemblyProcessor(_connection);

        var solutionPrefix = processor.GetSolutionPrefix(settings.Solution);

        if (settings.Target.EndsWith(".nupkg", StringComparison.InvariantCultureIgnoreCase))
        {
            // Dependent Plugin
            AnsiConsole.MarkupLine(CultureInfo.InvariantCulture, "Package found - unpack");

            var packageLocal = modelBuilder.BuildPackageFromFile(settings.Target);
            var packageCrm = modelBuilder.BuildPackageFromCrm(packageLocal.Name, packageLocal.Version);

            if (packageCrm.State == AssemblyState.Create)
            {
                ctx.Status("CreatePluginPackage");
                if (solutionPrefix == "new")
                {
                    AnsiConsole.MarkupLine(CultureInfo.InvariantCulture, "[yellow] Solution not set or found - Package will have prefix 'new' [/]");
                }

                packageCrm = processor.CreatePluginPackage(packageLocal, solutionPrefix);
            }
            else
            {
                ctx.Status("UpdatePluginPackage");
                packageCrm = processor.UpdatePluginPackage(packageCrm.Id, packageLocal);
            }

            assemblies = modelBuilder.BuildAssemblyFromPackage(packageCrm);
        }
        else
        {
            assemblies = new List<Assembly?> { modelBuilder.BuildAssemblyFromDll(settings.Target) };
        }


        foreach (var localAssembly in assemblies)
        {
            if (localAssembly == default(Assembly))
            {
                continue;
            }

            if (localAssembly.Type == AssemblyType.Undefined)
            {
                AnsiConsole.MarkupLine(CultureInfo.InvariantCulture, "Assembly [bold red]does not contain[/] plugins or workflows - aborting");
                continue;
            }

            AnsiConsole.MarkupLine(CultureInfo.InvariantCulture, "Check Assembly [bold green]{0} ({1})[/]", localAssembly.Name, localAssembly.Version);

            ctx.Status("BuildFromCrm");
            var crmAssembly = modelBuilder.BuildAssemblyFromCrm(localAssembly.Name, localAssembly.Version);

            if (crmAssembly.State == AssemblyState.Create || (crmAssembly.State == AssemblyState.Upgrade && crmAssembly.Type.HasFlag(AssemblyType.Workflow)))
            {
                ctx.Status("CreatePluginAssembly");
                crmAssembly = processor.CreatePluginAssembly(localAssembly, settings.Solution);
            }
            else if (crmAssembly.State == AssemblyState.Update && !localAssembly.Equals(crmAssembly))
            {
                ctx.Status("UpdatePluginAssembly");
                crmAssembly = processor.UpdatePluginAssembly(localAssembly, crmAssembly, settings.Solution);
            }

            if (localAssembly.Type.HasFlag(AssemblyType.Workflow))
            {
                ctx.Status("UpsertAndPurgeWorkflowTypes");
                processor.UpsertAndPurgeWorkflowTypes(localAssembly, crmAssembly);
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
        }
    }

    private void ProcessWebresourcesFolder(PushVerb settings, StatusContext ctx)
    {
        var processor = new WebresourcesProcessor(_connection);

        ctx.Status("Check Settings");

        processor.LoadSolution(settings.Solution, out bool isDefault);
        if (settings.DeleteObsolete && isDefault)
        {
            AnsiConsole.MarkupLine(CultureInfo.InvariantCulture, "Delete Obsolete [bold red]without[/] proper solution set - aborting");
            return;
        }

        ctx.Status("Discover Webresources");
        processor.DiscoverWebresources(settings.Target);

        ctx.Status("Upsert Webresources");
        processor.UpsertResources();

        if (settings.DeleteObsolete)
        {
            processor.DiscoverObsoleteWebresources();
            ctx.Status("Delete Webresources");
            processor.DeleteResources();
        }
    }

}
