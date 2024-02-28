// Copyright (c) DIGITALL Nature. All rights reserved
// DIGITALL Nature licenses this file to you under the Microsoft Public License.

using System.Diagnostics;
using System.Diagnostics.CodeAnalysis;
using dgt.power.common;
using Spectre.Console;
using Spectre.Console.Cli;

// ReSharper disable ClassNeverInstantiated.Global

namespace dgt.power.profile.Commands;

public class SelectProfileCommand : Command<NamedProfileSettings>
{
    private readonly IProfileManager _profileManager;

    public SelectProfileCommand(IProfileManager profileManager) => _profileManager = profileManager;

    public override int Execute([NotNull] CommandContext context, [NotNull] NamedProfileSettings settings)
    {
        Debug.Assert(settings != null, nameof(settings) + " != null");

        var identities = _profileManager.LoadIdentities();
        if (!identities.Contains(settings.Name.ToUpperInvariant()))
        {
            AnsiConsole.MarkupLine($"[Red]Identity {settings.Name} not found![/]");
            return -1;
        }

        identities.SetCurrent(settings.Name.ToUpperInvariant());
        _profileManager.Save();

        var rule = new Rule($"Identity [lime]{settings.Name}[/] set.");
        rule.LeftJustified();
        AnsiConsole.Write(rule);

        return 0;
    }
}
