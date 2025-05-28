// Copyright (c) DIGITALL Nature. All rights reserved
// DIGITALL Nature licenses this file to you under the Microsoft Public License.

using dgt.power.common.Exceptions;
using Microsoft.Extensions.Configuration;
using Spectre.Console;

namespace dgt.power.common.Logic;

public class XrmConnectionFactory(IProfileManager profileManager, IConfiguration configuration) : IXrmConnectionFactory
{
    public IXrmConnection GetDefault() => new XrmConnection(profileManager, configuration);

    public IXrmConnection GetWithProfile(string profile)
    {
        var identities = profileManager.LoadIdentities();
        if (!identities.Contains(profile.ToUpperInvariant()))
        {
            AnsiConsole.MarkupLine($"[Red]Identity {profile} not found![/]");
            throw new MissingConnectionException($"Profile {profile} not found.");
        }

        identities.SetCurrent(profile.ToUpperInvariant());
        return new XrmConnection(profileManager, configuration);
    }
}
