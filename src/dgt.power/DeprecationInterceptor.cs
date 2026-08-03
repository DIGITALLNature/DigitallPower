// Copyright (c) DIGITALL Nature. All rights reserved
// DIGITALL Nature licenses this file to you under the Microsoft Public License.

using System.Reflection;
using dgt.power.common.Commands;
using Spectre.Console;
using Spectre.Console.Cli;

namespace dgt.power;

/// <summary>
/// Prints a deprecation warning whenever a command whose <see cref="CommandSettings"/> type is
/// annotated with <see cref="DeprecatedCommandAttribute"/> is invoked.
/// </summary>
/// <remarks>
/// Detection is based on the actual bound <c>settings</c> instance, not on the raw,
/// unparsed application arguments. This is inherently reliable, unlike scanning
/// <c>CommandContext.Arguments</c> for the branch name: it does not care where in the argument
/// list the branch appeared, whether global options precede it, or how deep the command is nested.
/// <para>
/// To deprecate a command or an entire branch, annotate its <see cref="CommandSettings"/> class with
/// <c>[DeprecatedCommand("replacement")]</c> — no changes to the command's execution logic, and no
/// per-module wiring, are required.
/// </para>
/// </remarks>
internal sealed class DeprecationInterceptor(IAnsiConsole console) : ICommandInterceptor
{
    public void Intercept(CommandContext context, CommandSettings settings)
    {
        var deprecation = settings.GetType().GetCustomAttribute<DeprecatedCommandAttribute>(inherit: true);
        if (deprecation is null)
        {
            return;
        }

        var replacementHint = deprecation.UseInstead is { } replacement ? $" Use '{replacement}' instead." : string.Empty;

        console.MarkupLine($"[yellow]DEPRECATED: This command is deprecated.{replacementHint} Please update your scripts.[/]");
    }
}
