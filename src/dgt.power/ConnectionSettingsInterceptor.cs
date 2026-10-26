// SPDX-FileCopyrightText: DIGITALL Nature
// SPDX-License-Identifier: MS-RL

using dgt.power.common;
using dgt.power.common.Connections;
using Spectre.Console.Cli;

namespace dgt.power;

internal sealed class ConnectionSettingsInterceptor(ConnectionInvocationContext invocationContext)
    : ICommandInterceptor
{
    public void Intercept(CommandContext context, CommandSettings settings)
    {
        if (settings is BaseProgramSettings baseSettings)
        {
            invocationContext.Capture(baseSettings);
        }
    }
}
