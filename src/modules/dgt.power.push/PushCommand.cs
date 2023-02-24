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

                if (verb.DllFile.EndsWith(".nupkg"))
                {
                    // Dependent Plugin
                    AnsiConsole.MarkupLine(CultureInfo.InvariantCulture, "Package found - unpack");

                    var packageLocal = PackageBuilder.BuildFromFile(verb.DllFile);
                    var packageCrm = PackageBuilder.BuildFromCrm(packageLocal.Name, packageLocal.Version, "dgt", _connection);


                    if (packageCrm.State == AssemblyState.Create)
                    {
                        ctx.Status("CreatePluginPackage");
                        packageCrm = Processor.CreatePluginPackage(packageLocal, _connection, "dgt");
                    }


                    assemblies = PackageBuilder.UnpackPackage(verb.DllFile, _connection);
                }
                else
                {
                    assemblies = new List<Assembly?> { AssemblyBuilder.BuildFromDll(verb.DllFile, _connection) };
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
                    var crmAssembly = AssemblyBuilder.BuildFromCrm(localAssembly.Name, localAssembly.Version, _connection);

                    if (crmAssembly.State == AssemblyState.Create || (crmAssembly.State == AssemblyState.Upgrade && crmAssembly.Type == AssemblyType.Workflow))
                    {
                        ctx.Status("CreatePluginAssembly");
                        crmAssembly = Processor.CreatePluginAssembly(localAssembly, _connection, verb.Solution);
                    }
                    else if (crmAssembly.State != AssemblyState.Package && localAssembly.Equals(crmAssembly))
                    {
                        ctx.Status("UpdatePluginAssembly");
                        crmAssembly = Processor.UpdatePluginAssembly(localAssembly, crmAssembly, _connection, verb.Solution);
                    }

                    if (localAssembly.Type.HasFlag(AssemblyType.Workflow))
                    {
                        ctx.Status("UpsertAndPurgeWorkflowTypes");
                        Processor.UpsertAndPurgeWorkflowTypes(localAssembly, crmAssembly, _connection);
                    }

                    if (localAssembly.Type.HasFlag(AssemblyType.Plugin))
                    {
                        ctx.Status("UpsertAndPurgePluginTypes");
                        crmAssembly = Processor.UpsertAndPurgePluginTypes(localAssembly, crmAssembly, _connection, verb.Solution);

                        if (localAssembly.Type.HasFlag(AssemblyType.PowerPlugin))
                        {
                            ctx.Status("UpsertAndPurgePluginSteps");
                            crmAssembly = Processor.UpsertAndPurgePluginSteps(localAssembly, crmAssembly, _connection, verb.Solution);
                            ctx.Status("UpsertAndPurgePluginStepImages");
                            Processor.UpsertAndPurgePluginStepImages(localAssembly, crmAssembly, _connection);
                        }
                    }
                }

                ctx.Status("Finishing");
            });

        return _tracer.End(this, true) ? 0 : -1;
    }
}
