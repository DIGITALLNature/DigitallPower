// Copyright (c) DIGITALL Nature. All rights reserved
// DIGITALL Nature licenses this file to you under the Microsoft Public License.

using dgt.power.common;
using dgt.power.common.Commands;
using dgt.power.profile.Base;
using Spectre.Console;
using Spectre.Console.Cli;

namespace dgt.power.profile.Commands;

/// <summary>
/// Checks whether the current MSAL token is still valid without opening a browser.
/// Exit codes:
///   0 — token is valid (or profile uses classic auth, no MSAL)
///   2 — interactive login is required; ask the user to re-authenticate
/// Intended for coding agents to use as a pre-flight check before any Dataverse command.
/// </summary>
// ReSharper disable once ClassNeverInstantiated.Global — instantiated by the DI container via Spectre.Console.Cli
public class AuthCheckCommand(IXrmConnection xrmConnection, IAnsiConsole console)
    : AsyncCommand<ProfileSettings>
{
    protected override async Task<int> ExecuteAsync(CommandContext context, ProfileSettings settings, CancellationToken cancellationToken)
    {
        var isValid = await xrmConnection.CheckAuthAsync();

        if (isValid)
        {
            console.MarkupLine("[green]AUTH_OK: Token is valid — no interactive login required.[/]");
            return (int)ExitCode.Success;
        }

        console.MarkupLine("[red]AUTH_REQUIRED: Interactive login is required.[/]");
        console.MarkupLine("[red]              Ask the user to authenticate, then retry the command.[/]");
        console.MarkupLine("[grey]Tip: run 'dgtp profile create <name> <url> --msal' to refresh credentials.[/]");
        return (int)ExitCode.AuthRequired;
    }
}



