// Copyright (c) DIGITALL Nature. All rights reserved
// DIGITALL Nature licenses this file to you under the Microsoft Public License.

using System.Diagnostics;
using System.Diagnostics.CodeAnalysis;
using dgt.power.common;
using Spectre.Console;
using Spectre.Console.Cli;

namespace dgt.power.profile.Commands;

public class DeleteProfileCommand : Command<NamedProfileSettings>
{
    private readonly IProfileManager _profileManager;
    private readonly IAnsiConsole _console;

    public DeleteProfileCommand(IProfileManager profileManager, IAnsiConsole console)
    {
        _profileManager = profileManager;
        _console = console;
    }

    protected override int Execute(CommandContext context, NamedProfileSettings settings, CancellationToken cancellationToken)
    {
        Debug.Assert(settings != null, nameof(settings) + " != null");

        var identities = _profileManager.LoadIdentities();
        identities.Remove(settings.Name.ToUpperInvariant());
        _profileManager.Save();

        var rule = new Rule($"Identity [lime]{settings.Name}[/] is removed.");
        rule.LeftJustified();
        _console.Write(rule);

        return 0;
    }
}
