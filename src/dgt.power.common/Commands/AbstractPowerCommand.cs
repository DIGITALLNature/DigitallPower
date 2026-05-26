// Copyright (c) DIGITALL Nature. All rights reserved
// DIGITALL Nature licenses this file to you under the Microsoft Public License.

using System.Diagnostics.CodeAnalysis;
using Spectre.Console.Cli;

namespace dgt.power.common.Commands;

public abstract class AbstractPowerCommand<TCommandSettings> : Command<TCommandSettings>, IPowerLogic where TCommandSettings : BaseProgramSettings
{
    protected AbstractPowerCommand(IConfigResolver configResolver) => ConfigResolver = configResolver;

    protected IConfigResolver ConfigResolver { get; }

    protected CommandContext? CommandContext { get; private set; }

    protected override int Execute(CommandContext context, TCommandSettings settings, CancellationToken cancellationToken)
    {
        CommandContext = context;

        return (int)Execute(settings);
    }

    public abstract ExitCode Execute(TCommandSettings settings);
}
