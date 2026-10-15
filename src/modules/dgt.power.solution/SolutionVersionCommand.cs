// Copyright (c) DIGITALL Nature. All rights reserved
// DIGITALL Nature licenses this file to you under the Microsoft Public License.

using dgt.power.common;
using dgt.power.dataverse;
using dgt.power.solution.Base;
using Microsoft.PowerPlatform.Dataverse.Client;
using Microsoft.Xrm.Sdk;
using Microsoft.Xrm.Sdk.Query;
using Spectre.Console;

namespace dgt.power.solution;

// ReSharper disable once ClassNeverInstantiated.Global — instantiated by the DI container via Spectre.Console.Cli
public class SolutionVersionCommand(
    ICommandSpanFactory spans,
    IDataverseConnection connection,
    IAnsiConsole console)
    : PowerCommand<SolutionVersionSettings>(spans, console)
{
    protected override async Task<bool> RunAsync(SolutionVersionSettings settings, CancellationToken cancellationToken)
    {
        ArgumentNullException.ThrowIfNull(settings);

        if (string.IsNullOrWhiteSpace(settings.Solution))
        {
            console.MarkupLine($"[red]Invalid or empty solution name '{settings.Solution}'[/]");
            return false;
        }

        var orgService = await connection.GetServiceAsync();

        var query = new QueryExpression(Solution.EntityLogicalName)
        {
            NoLock = true,
            ColumnSet = new ColumnSet(Solution.LogicalNames.UniqueName, Solution.LogicalNames.FriendlyName, Solution.LogicalNames.Version)
        };
        query.Criteria.AddCondition(Solution.LogicalNames.UniqueName, ConditionOperator.Equal, settings.Solution);

        var solution = (await orgService.RetrieveMultipleAsync(query, cancellationToken)).Entities
            .Select(static entity => entity.ToEntity<Solution>())
            .FirstOrDefault();

        if (solution == null)
        {
            console.MarkupLine($"[red]Solution with unique name '{settings.Solution}' not found[/]");
            return false;
        }

        if (!Version.TryParse(solution.Version, out var version))
        {
            console.MarkupLine($"[red]Couldn't parse solution version '{solution.Version}'[/]");
            return false;
        }

        console.MarkupLine($"Retrieved solution [green]{solution.UniqueName}[/] with version [green]{version}[/]");

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
            console.MarkupLine("[red]Invalid version strategy. Try --major,--minor,--build or --revision[/]");
            return false;
        }

        await orgService.UpdateAsync(new Solution(solution.Id)
        {
            Version = incrementedVersion.ToString()
        }, cancellationToken);
        console.MarkupLine($"Updated solution version [yellow]{version}[/] --> [green]{incrementedVersion}[/]");

        return true;
    }
}
