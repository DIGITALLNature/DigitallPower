// Copyright (c) DIGITALL Nature. All rights reserved
// DIGITALL Nature licenses this file to you under the Microsoft Public License.

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
        var identities = _profileManager.GetIdentities();
        if (!identities.Keys.Contains(settings.Name.ToLowerInvariant()))
        {
            AnsiConsole.MarkupLine($"[Red]Identity {settings.Name} not found![/]");
            return -1;
        }

        identities.SetCurrent(settings.Name.ToLowerInvariant());
        _profileManager.Save();

        var rule = new Rule($"Identity [lime]{settings.Name}[/] set.");
        rule.LeftJustified();
        AnsiConsole.Write(rule);

        return 0;
    }
}
