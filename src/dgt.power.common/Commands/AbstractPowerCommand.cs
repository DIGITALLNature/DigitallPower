using System.Diagnostics.CodeAnalysis;
using Spectre.Console.Cli;

namespace dgt.power.common.Commands;

public abstract class AbstractPowerCommand<TCommandSettings> : Command<TCommandSettings>, IPowerLogic where TCommandSettings : BaseProgramSettings
{
    protected AbstractPowerCommand(IConfigResolver configResolver) => ConfigResolver = configResolver;

    protected IConfigResolver ConfigResolver { get; }

    protected CommandContext? CommandContext { get; private set; }

    public override int Execute([NotNull] CommandContext context, [NotNull] TCommandSettings settings)
    {
        CommandContext = context;

        return (int)Execute(settings);
    }

    public abstract ExitCode Execute(TCommandSettings settings);
}
