// Copyright (c) DIGITALL Nature. All rights reserved
// DIGITALL Nature licenses this file to you under the Microsoft Public License.

using System.Diagnostics.CodeAnalysis;
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
    private readonly ITracer _tracer;

    public PushCommand(ITracer tracer, IConfigResolver configResolver, IOrganizationService connection)
    {
        _tracer = tracer;
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

                packageCrm = processor.CreatePluginPackage(packageLocal, solutionPrefix, settings.Solution);
            }
            else
            {
                ctx.Status("UpdatePluginPackage");
                packageCrm = processor.UpdatePluginPackage(packageCrm, packageLocal, settings.Publish, settings.Solution);
            }

            assemblies = modelBuilder.BuildAssemblyFromPackage(packageCrm);
        }
        else
        {
            var env = new List<string>(Directory.GetFiles(RuntimeEnvironment.GetRuntimeDirectory(),"*.dll").Concat(Directory.GetFiles(Path.GetDirectoryName(System.Reflection.Assembly.GetExecutingAssembly().Location!)!,"*.dll")));
            var loadContext = new MetadataLoadContext(new PathAssemblyResolver(env));
            assemblies = new List<Assembly?> { modelBuilder.BuildAssemblyFromDll(settings.Target,loadContext) };
        }


        foreach (var localAssembly in assemblies)
        {
            if (localAssembly == default(Assembly))
            {
                continue;
            }

            if (localAssembly.Type == AssemblyType.Undefined)
            {
                AnsiConsole.MarkupLine(CultureInfo.InvariantCulture, "Assembly [bold green]{0} ({1})[/] [bold red]does not contain[/] plugins or workflows - aborting", localAssembly.Name, localAssembly.Version);
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
                crmAssembly = processor.UpdatePluginAssembly(localAssembly, crmAssembly, settings.Solution,settings.Publish);
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
        processor.UpsertResources(settings.Publish);

        if (settings.DeleteObsolete)
        {
            processor.DiscoverObsoleteWebresources();
            ctx.Status("Delete Webresources");
            processor.DeleteResources();
        }
    }

}
