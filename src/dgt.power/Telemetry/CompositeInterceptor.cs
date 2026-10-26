// SPDX-FileCopyrightText: DIGITALL Nature
// SPDX-License-Identifier: MS-RL

using Spectre.Console.Cli;

namespace dgt.power.Telemetry;

/// <summary>
/// Combines multiple ICommandInterceptor instances into one,
/// since Spectre.Console.Cli only supports a single interceptor.
/// </summary>
internal sealed class CompositeInterceptor(params ICommandInterceptor[] interceptors) : ICommandInterceptor
{
    public void Intercept(CommandContext context, CommandSettings settings)
    {
        foreach (var interceptor in interceptors)
        {
            interceptor.Intercept(context, settings);
        }
    }
}
