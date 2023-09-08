// Copyright (c) DIGITALL Nature. All rights reserved
// DIGITALL Nature licenses this file to you under the Microsoft Public License.

using System.Diagnostics;
using dgt.power.common;
using dgt.power.common.Commands;
using dgt.power.common.Exceptions;
using dgt.power.common.Extensions;
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
        Debug.Assert(settings != null, nameof(settings) + " != null");

        var identities = _profileManager.GetIdentities();
#if DEBUG
        if (settings.TokenBased)
        {
            identities.Upsert(settings.Name.ToUpperInvariant(),
                new TokenIdentity
                {
                    ConnectionString = settings.ConnectionString,
                    Insecure = settings.Insecure,
                    SecurityProtocol = settings.SecurityProtocol,
                    Token = "-test-"
                });
        }
        else
        {
#endif
            identities.Upsert(settings.Name.ToUpperInvariant(),
                new Identity
                {
                    ConnectionString = settings.ConnectionString,
                    Insecure = settings.Insecure,
                    SecurityProtocol = settings.SecurityProtocol
                });
#if DEBUG
        }
#endif
        _profileManager.Save();

        if (!settings.SkipChecking)
        {
            try
            {
                _connection.Connect();
            }
            catch (FailedConnectionException fc)
            {
                AnsiConsole.WriteLine(fc.RootMessage());
                throw;
            }
        }

        var rule = new Rule($"Identity [lime]{settings.Name}[/] upserted.");
        rule.LeftJustified();
        AnsiConsole.Write(rule);

        return ExitCode.Success;
    }
}
