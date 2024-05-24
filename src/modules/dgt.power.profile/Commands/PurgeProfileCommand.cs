// Copyright (c) DIGITALL Nature. All rights reserved
// DIGITALL Nature licenses this file to you under the Microsoft Public License.

using System.Diagnostics.CodeAnalysis;
using dgt.power.common;
using dgt.power.profile.Base;
using Spectre.Console;
using Spectre.Console.Cli;

// ReSharper disable ClassNeverInstantiated.Global

namespace dgt.power.profile.Commands;

public class PurgeProfileCommand : Command<ProfileSettings>
{
    private readonly IProfileManager _profileManager;

    public PurgeProfileCommand(IProfileManager profileManager) => _profileManager = profileManager;

    public override int Execute([NotNull] CommandContext context, [NotNull] ProfileSettings settings)
    {
        _profileManager.Purge();

        var rule = new Rule("Identities have been [red]purged[/].");
        rule.LeftJustified();
        AnsiConsole.Write(rule);

        return 0;
    }
}
