// Copyright (c) DIGITALL Nature. All rights reserved
// DIGITALL Nature licenses this file to you under the Microsoft Public License.

using Spectre.Console;
using Spectre.Console.Cli;

namespace dgt.power;

/// <summary>
/// Prints a deprecation warning when the user invokes a command via a deprecated command name.
/// </summary>
/// <remarks>
/// <see cref="CommandContext.Name"/> only exposes the leaf command's name (e.g. "list"), not the
/// top-level branch name (e.g. "profile") that was actually typed, so <see cref="CommandContext.Arguments"/>
/// (the raw, unparsed application arguments) is used instead to detect the invoked branch.
/// </remarks>
internal sealed class DeprecationInterceptor(IAnsiConsole console) : ICommandInterceptor
{
    private static readonly Dictionary<string, string> s_deprecations = new(StringComparer.OrdinalIgnoreCase)
    {
        ["profile"] = "connection"
    };

    public void Intercept(CommandContext context, CommandSettings settings)
    {
        if (context.Arguments.Count > 0 && s_deprecations.TryGetValue(context.Arguments[0], out var replacement))
        {
            console.MarkupLine($"[yellow]DEPRECATED: '{context.Arguments[0]}' has been renamed to '{replacement}'. Please update your scripts.[/]");
        }
    }
}
