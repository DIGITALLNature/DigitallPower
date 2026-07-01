// Copyright (c) DIGITALL Nature. All rights reserved
// DIGITALL Nature licenses this file to you under the Microsoft Public License.

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
