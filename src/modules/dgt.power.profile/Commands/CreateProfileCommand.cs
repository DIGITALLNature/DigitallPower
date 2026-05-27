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
    private readonly IAnsiConsole _console;


    public CreateProfileCommand(IProfileManager profileManager, IConfigResolver configResolver, IXrmConnection connection, IAnsiConsole console) : base(configResolver)
    {
        _profileManager = profileManager;
        _connection = connection;
        _console = console;
    }

    public override ExitCode Execute(CreateProfileSettings settings)
    {
        Debug.Assert(settings != null, nameof(settings) + " != null");

        var identities = _profileManager.LoadIdentities();
        if (settings.TokenBased)
        {
            identities.Upsert(settings.Name.ToUpperInvariant(),
                new TokenIdentity
                {
                    ConnectionString = settings.ConnectionString,
                    Insecure = settings.Insecure,
                    SecurityProtocol = settings.SecurityProtocol,
                    Token = null
                });
        }
        else
        {
            identities.Upsert(settings.Name.ToUpperInvariant(),
                new Identity
                {
                    ConnectionString = settings.ConnectionString,
                    Insecure = settings.Insecure,
                    SecurityProtocol = settings.SecurityProtocol
                });
        }
        
        _profileManager.Save();

        if (!settings.SkipChecking)
        {
            try
            {
                _connection.Connect();
            }
            catch (FailedConnectionException fc)
            {
                _console.WriteLine(fc.RootMessage());
                throw;
            }
        }

        var rule = new Rule($"Identity [lime]{settings.Name}[/] upserted.");
        rule.LeftJustified();
        _console.Write(rule);

        return ExitCode.Success;
    }
}
