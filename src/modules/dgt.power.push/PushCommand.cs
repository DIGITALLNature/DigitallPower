using System.Diagnostics.CodeAnalysis;
using System.Globalization;
using dgt.power.common;
using dgt.power.dataverse;
using dgt.power.push.Base;
using dgt.power.push.Logic;
using dgt.power.push.Model;
using Microsoft.Xrm.Sdk;
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

    public override int Execute([NotNull] CommandContext context, [NotNull] PushVerb verb)
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


                List<Assembly?> assemblies;
                var modelBuilder = new ModelBuilder(_connection);
                var processor = new Processor(_connection);

                var solutionPrefix = processor.GetSolutionPrefix(verb.Solution);


                if (verb.DllFile.EndsWith(".nupkg"))
                {
                    // Dependent Plugin
                    AnsiConsole.MarkupLine(CultureInfo.InvariantCulture, "Package found - unpack");

                    var packageLocal = modelBuilder.BuildPackageFromFile(verb.DllFile);
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
                    assemblies = new List<Assembly?> { modelBuilder.BuildAssemblyFromDll(verb.DllFile) };
                }


                foreach (var localAssembly in assemblies)
                {
                    if (localAssembly == default(Assembly))
                    {
                        continue;
                    }

                    if (localAssembly.Type == AssemblyType.Undefined)
                    {
                        AnsiConsole.MarkupLine(CultureInfo.InvariantCulture,
                            "Assembly [bold red]does not contain[/] plugins or workflows - aborting");
                        continue;
                    }

                    AnsiConsole.MarkupLine(CultureInfo.InvariantCulture, "Check Assembly [bold green]{0} ({1})[/]",
                        localAssembly.Name, localAssembly.Version);
                    ctx.Status("BuildFromCrm");
                    var crmAssembly = modelBuilder.BuildAssemblyFromCrm(localAssembly.Name, localAssembly.Version);

                    if (crmAssembly.State == AssemblyState.Create || (crmAssembly.State == AssemblyState.Upgrade &&
                                                                      crmAssembly.Type == AssemblyType.Workflow))
                    {
                        ctx.Status("CreatePluginAssembly");
                        crmAssembly = processor.CreatePluginAssembly(localAssembly, verb.Solution);
                    }
                    else if (crmAssembly.State != AssemblyState.Package && localAssembly.Equals(crmAssembly))
                    {
                        ctx.Status("UpdatePluginAssembly");
                        crmAssembly = processor.UpdatePluginAssembly(localAssembly, crmAssembly, verb.Solution);
                    }

                    if (localAssembly.Type.HasFlag(AssemblyType.Workflow))
                    {
                        ctx.Status("UpsertAndPurgeWorkflowTypes");
                        processor.UpsertAndPurgeWorkflowTypes(localAssembly, crmAssembly);
                    }

                    if (localAssembly.Type.HasFlag(AssemblyType.Plugin))
                    {
                        ctx.Status("UpsertAndPurgePluginTypes");
                        crmAssembly = processor.UpsertAndPurgePluginTypes(localAssembly, crmAssembly, verb.Solution);

                        if (localAssembly.Type.HasFlag(AssemblyType.PowerPlugin))
                        {
                            ctx.Status("UpsertAndPurgePluginSteps");
                            crmAssembly =
                                processor.UpsertAndPurgePluginSteps(localAssembly, crmAssembly, verb.Solution);
                            ctx.Status("UpsertAndPurgePluginStepImages");
                            processor.UpsertAndPurgePluginStepImages(localAssembly, crmAssembly);
                        }
                    }
                }

                ctx.Status("Finishing");
            });

        return _tracer.End(this, true) ? 0 : -1;
    }
}
