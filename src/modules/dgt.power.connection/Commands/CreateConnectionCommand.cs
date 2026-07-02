// Copyright (c) DIGITALL Nature. All rights reserved
// DIGITALL Nature licenses this file to you under the Microsoft Public License.

using dgt.power.common;
using dgt.power.common.Exceptions;
using dgt.power.common.Extensions;
using dgt.power.common.Logic;
using Spectre.Console;
using Spectre.Console.Cli;

namespace dgt.power.connection.Commands;

// ReSharper disable once ClassNeverInstantiated.Global
public class CreateConnectionCommand(
    IProfileManager profileManager,
    IXrmConnection connection,
    IAnsiConsole console)
    : AsyncCommand<CreateConnectionSettings>
{
    protected override async Task<int> ExecuteAsync(CommandContext context, CreateConnectionSettings settings, CancellationToken cancellationToken)
    {
        ArgumentNullException.ThrowIfNull(settings);
        var identities = profileManager.LoadIdentities();

        if (settings.Url != null)
        {
            identities.Upsert(settings.Name,
                new TokenIdentity
                {
                    ConnectionString = settings.Url,
                    Token = string.Empty
                });
        }
        else
        {
            identities.Upsert(settings.Name,
                new Identity
                {
                    ConnectionString = settings.ConnectionString!
                });
        }

        profileManager.Save();

        if (!settings.NoVerify)
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

        var rule = new Rule($"Connection [lime]{settings.Name}[/] upserted.");
        rule.LeftJustified();
        console.Write(rule);

        return 0;
    }
}

