// Copyright (c) DIGITALL Nature. All rights reserved
// DIGITALL Nature licenses this file to you under the Microsoft Public License.

using Spectre.Console;
using Spectre.Console.Cli;

namespace dgt.power;

/// <summary>
/// Prints a deprecation warning when the user invokes a command via a deprecated command name.
/// </summary>
internal sealed class DeprecationInterceptor(string[] args, IAnsiConsole console) : ICommandInterceptor
{
    private static readonly Dictionary<string, string> s_deprecations = new(StringComparer.OrdinalIgnoreCase)
    {
        ["profile"] = "connection"
    };

    public void Intercept(CommandContext context, CommandSettings settings)
    {
        if (args.Length > 0 && s_deprecations.TryGetValue(args[0], out var replacement))
        {
            console.MarkupLine($"[yellow]DEPRECATED: '{args[0]}' has been renamed to '{replacement}'. Please update your scripts.[/]");
        }
    }
}
