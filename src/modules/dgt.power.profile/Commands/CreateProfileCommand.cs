// Copyright (c) DIGITALL Nature. All rights reserved
// DIGITALL Nature licenses this file to you under the Microsoft Public License.

using System.Diagnostics;
using dgt.power.common;
using dgt.power.common.Exceptions;
using dgt.power.common.Extensions;
using dgt.power.common.Logic;
using Spectre.Console;
using Spectre.Console.Cli;

namespace dgt.power.profile.Commands;

public class CreateProfileCommand(
    IProfileManager profileManager,
    IXrmConnection connection,
    IAnsiConsole console)
    : AsyncCommand<CreateProfileSettings>
{
    protected override async Task<int> ExecuteAsync(CommandContext context, CreateProfileSettings settings, CancellationToken cancellationToken)
    {
        Debug.Assert(settings != null, nameof(settings) + " != null");

        var identities = profileManager.LoadIdentities();
        if (settings.TokenBased)
        {
            identities.Upsert(settings.Name,
                new TokenIdentity
                {
                    ConnectionString = settings.ConnectionString,
                    Token = string.Empty
                });
        }
        else
        {
            identities.Upsert(settings.Name,
                new Identity
                {
                    ConnectionString = settings.ConnectionString
                });
        }

        profileManager.Save();

        if (!settings.SkipChecking)
        {
            try
            {
                await connection.ConnectAsync();
            }
            catch (FailedConnectionException fc)
            {
                console.WriteLine(fc.RootMessage());
                throw;
            }
        }

        var rule = new Rule($"Identity [lime]{settings.Name}[/] upserted.");
        rule.LeftJustified();
        console.Write(rule);

        return 0;
    }
}
