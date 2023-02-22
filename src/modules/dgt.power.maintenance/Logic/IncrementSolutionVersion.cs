// Copyright (c) DIGITALL Nature. All rights reserved
// DIGITALL Nature licenses this file to you under the Microsoft Public License.

using dgt.power.common;
using dgt.power.common.Commands;
using dgt.power.dataverse;
using dgt.power.maintenance.Model.Settings;
using Microsoft.Xrm.Sdk;
using Spectre.Console;

namespace dgt.power.maintenance.Logic;

public class IncrementSolutionVersion : AbstractDataverseCommand<IncrementSolutionVersionSettings>
{
    public IncrementSolutionVersion(IOrganizationService organizationService, IConfigResolver configResolver) : base(organizationService,
        configResolver)
    {
    }

    public override ExitCode Execute(IncrementSolutionVersionSettings settings)
    {
        if (string.IsNullOrWhiteSpace(settings.Solution))
        {
            AnsiConsole.MarkupLine($"[red]Invalid or empty solution name '{settings.Solution}'[/]");
            return ExitCode.Error;
        }

        var solution = DataContext.SolutionSet.Where(x => x.UniqueName == settings.Solution)
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
            AnsiConsole.MarkupLine($"[red]Solution with unique name '{settings.Solution}' not found[/]");
            return ExitCode.Error;
        }

        if (!Version.TryParse(solution.Version, out var version))
        {
            AnsiConsole.MarkupLine($"[red]Couldn't parse solution version '{solution.Version}'[/]");
            return ExitCode.Error;
        }

        AnsiConsole.MarkupLine($"Retrieved solution [green]{solution.UniqueName}[/] with version [green]{version}[/]");

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
            AnsiConsole.MarkupLine("[red]Invalid version strategy. Try --major,--minor,--build or --revision[/]");
            return ExitCode.Error;
        }

        OrganizationService.Update(new Solution(solution.Id)
        {
            Version = incrementedVersion.ToString()
        });
        AnsiConsole.MarkupLine($"Updated solution version [yellow]{version}[/] --> [green]{incrementedVersion}[/]");

        return ExitCode.Success;
    }
}
