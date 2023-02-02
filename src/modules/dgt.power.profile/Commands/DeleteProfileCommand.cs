using System.Diagnostics.CodeAnalysis;
using dgt.power.common;
using Spectre.Console;
using Spectre.Console.Cli;

namespace dgt.power.profile.Commands;

public class DeleteProfileCommand : Command<NamedProfileSettings>
{
    private readonly IProfileManager _profileManager;

    public DeleteProfileCommand(IProfileManager profileManager) => _profileManager = profileManager;

    public override int Execute([NotNull] CommandContext context, [NotNull] NamedProfileSettings settings)
    {
        var identities = _profileManager.GetIdentities();
        identities.Remove(settings.Name.ToLowerInvariant());
        _profileManager.Save();

        var rule = new Rule($"Identity [lime]{settings.Name}[/] is removed.");
        rule.LeftJustified();
        AnsiConsole.Write(rule);

        return 0;
    }
}
