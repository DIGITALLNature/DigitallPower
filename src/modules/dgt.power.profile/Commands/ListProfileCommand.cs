using System.Diagnostics.CodeAnalysis;
using dgt.power.common;
using dgt.power.profile.Base;
using Spectre.Console;
using Spectre.Console.Cli;

// ReSharper disable ClassNeverInstantiated.Global


namespace dgt.power.profile.Commands;

public class ListProfileCommand : Command<ProfileSettings>
{
    private readonly IProfileManager _profileManager;

    public ListProfileCommand(IProfileManager profileManager) => _profileManager = profileManager;

    public override int Execute([NotNull] CommandContext context, [NotNull] ProfileSettings settings)
    {
        var identities = _profileManager.GetIdentities();

        var grid = new Grid();
        // Add columns
        grid.AddColumn().AddColumn().AddColumn().AddColumn();

        // Add header row
        grid.AddRow("Current", "Name", "Protocol", "Insecure");

        foreach (var identity in identities.Keys)
        {
            grid.AddRow(identity == _profileManager.Current ? "*" : string.Empty, identity, _profileManager.CurrentIdentity?.SecurityProtocol ?? string.Empty,
                _profileManager.CurrentIdentity?.Insecure == true ? "yes" : "no");
        }

        AnsiConsole.Write(grid);
        return 0;
    }
}
