using dgt.power.common;
using dgt.power.common.Commands;
using dgt.power.common.Logic;
using Spectre.Console;

namespace dgt.power.profile.Commands;

public class CreateProfileCommand : AbstractPowerCommand<CreateProfileSettings>
{
    private readonly IXrmConnection _connection;
    private readonly IProfileManager _profileManager;


    public CreateProfileCommand(IProfileManager profileManager, IConfigResolver configResolver, IXrmConnection connection) : base(configResolver)
    {
        _profileManager = profileManager;
        _connection = connection;
    }

    public override ExitCode Execute(CreateProfileSettings settings)
    {
        var identities = _profileManager.GetIdentities();
        identities.Upsert(settings.Name.ToLowerInvariant(),
            new Identity
            {
                ConnectionString = settings.ConnectionString,
                Insecure = settings.Insecure,
                SecurityProtocol = settings.SecurityProtocol
            });
        _profileManager.Save();

        if (!settings.SkipChecking)
        {
            _connection.Connect();
        }

        var rule = new Rule($"Identity [lime]{settings.Name}[/] upserted.");
        rule.LeftJustified();
        AnsiConsole.Write(rule);

        return ExitCode.Success;
    }
}
