// Copyright (c) DIGITALL Nature. All rights reserved
// DIGITALL Nature licenses this file to you under the Microsoft Public License.

using dgt.power.common;
using Spectre.Console.Cli;

namespace dgt.power.Telemetry;

/// <summary>
/// Intercepts command execution to suppress telemetry when --no-telemetry is passed.
/// </summary>
internal sealed class TelemetryInterceptor : ICommandInterceptor
{
    public void Intercept(CommandContext context, CommandSettings settings)
    {
        if (settings is BaseProgramSettings { NoTelemetry: true })
        {
            Tracer.SuppressForInvocation = true;
        }
    }
}
