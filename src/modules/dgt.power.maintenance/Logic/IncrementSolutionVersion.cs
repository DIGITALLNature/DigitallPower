// Copyright (c) DIGITALL Nature. All rights reserved
// DIGITALL Nature licenses this file to you under the Microsoft Public License.

using dgt.power.common;
using dgt.power.dataverse;
using dgt.power.maintenance.Model.Settings;
using Microsoft.Xrm.Sdk;
using Spectre.Console;

namespace dgt.power.maintenance.Logic;

public class IncrementSolutionVersion(
    ITracer tracer,
    IOrganizationService connection,
    IConfigResolver configResolver,
    IAnsiConsole console)
    : PowerLogic<IncrementSolutionVersionSettings>(tracer, connection, configResolver, console)
{
    protected override bool Invoke(IncrementSolutionVersionSettings settings)
    {
        ArgumentNullException.ThrowIfNull(settings);
        Tracer.Start(this);

        if (string.IsNullOrWhiteSpace(settings.Solution))
        {
            Console.MarkupLine($"[red]Invalid or empty solution name '{settings.Solution}'[/]");
            return Tracer.End(this, false);
        }

        using var dataContext = new DataContext(Connection);

        var solution = dataContext.SolutionSet.Where(x => x.UniqueName == settings.Solution)
            .Select(x => new Solution
            {
                Id = x.Id,
                UniqueName = x.UniqueName,
                FriendlyName = x.FriendlyName,
                Version = x.Version
            })
            .FirstOrDefault();

        if (solution == null)
        {
            Console.MarkupLine($"[red]Solution with unique name '{settings.Solution}' not found[/]");
            return Tracer.End(this, false);
        }

        if (!Version.TryParse(solution.Version, out var version))
        {
            Console.MarkupLine($"[red]Couldn't parse solution version '{solution.Version}'[/]");
            return Tracer.End(this, false);
        }

        Console.MarkupLine($"Retrieved solution [green]{solution.UniqueName}[/] with version [green]{version}[/]");

        Version incrementedVersion;
        if (settings.Major)
        {
            incrementedVersion = new Version(version.Major + 1, 0, 0, 0);
        }
        else if (settings.Minor)
        {
            incrementedVersion = new Version(version.Major, version.Minor + 1, 0, 0);
        }
        else if (settings.Build)
        {
            incrementedVersion = new Version(version.Major, version.Minor, version.Build + 1, 0);
        }
        else if (settings.Revision)
        {
            incrementedVersion = new Version(version.Major, version.Minor, version.Build, version.Revision + 1);
        }
        else
        {
            Console.MarkupLine("[red]Invalid version strategy. Try --major,--minor,--build or --revision[/]");
            return Tracer.End(this, false);
        }

        Connection.Update(new Solution(solution.Id)
        {
            Version = incrementedVersion.ToString()
        });
        Console.MarkupLine($"Updated solution version [yellow]{version}[/] --> [green]{incrementedVersion}[/]");

        return Tracer.End(this, true);
    }
}
