using dgt.power.common;
using dgt.power.push.Base;
using Spectre.Console.Cli;
using System.Diagnostics.CodeAnalysis;
using System.Globalization;
using Spectre.Console;
using dgt.power.push.Logic;
using dgt.power.push.Model;
using dgt.power.dataverse;
using Microsoft.Xrm.Sdk;

namespace dgt.power.push;

public class PushCommand : Command<PushVerb>, IPowerLogic
{
    private readonly ITracer _tracer;
    private readonly IConfigResolver _configResolver;
    private readonly IOrganizationService _connection;

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

                var dllAssembly = Builder.BuildFromDll(verb.DllFile, _connection);
                if (dllAssembly == default(Assembly)) return;
                AnsiConsole.MarkupLine(CultureInfo.InvariantCulture, "Check Assembly [bold green]{0} ({1})[/]", dllAssembly.Name, dllAssembly.Version);
                ctx.Status("BuildFromCrm");
                var crmAssembly = Builder.BuildFromCrm(dllAssembly.Name, dllAssembly.Version, _connection);

                if (crmAssembly.State == AssemblyState.Create ||
                    (crmAssembly.State == AssemblyState.Upgrade && crmAssembly.Type == AssemblyType.Workflow))
                {
                    ctx.Status("CreatePluginAssembly");
                    crmAssembly = Processor.CreatePluginAssembly(dllAssembly, _connection, verb.Solution);
                }
                else if (!dllAssembly.Equals(crmAssembly))
                {
                    ctx.Status("UpdatePluginAssembly");
                    crmAssembly = Processor.UpdatePluginAssembly(dllAssembly, crmAssembly, _connection, verb.Solution);
                }

                if (dllAssembly.Type == AssemblyType.Workflow)
                {
                    ctx.Status("UpsertAndPurgeWorkflowTypes");
                    Processor.UpsertAndPurgeWorkflowTypes(dllAssembly, crmAssembly, _connection);
                }
                else
                {
                    ctx.Status("UpsertAndPurgePluginTypes");
                    crmAssembly = Processor.UpsertAndPurgePluginTypes(dllAssembly, crmAssembly, _connection, verb.Solution);
                    ctx.Status("UpsertAndPurgePluginSteps");
                    crmAssembly = Processor.UpsertAndPurgePluginSteps(dllAssembly, crmAssembly, _connection, verb.Solution);
                    ctx.Status("UpsertAndPurgePluginStepImages");
                    Processor.UpsertAndPurgePluginStepImages(dllAssembly, crmAssembly, _connection);
                }
                ctx.Status("Finishing");
            });

        return _tracer.End(this, true) ? 0 : -1;
    }

    public PushCommand(ITracer tracer, IConfigResolver configResolver, IOrganizationService connection)
    {
        _tracer = tracer;
        _configResolver = configResolver;
        _connection = connection;
    }
}
