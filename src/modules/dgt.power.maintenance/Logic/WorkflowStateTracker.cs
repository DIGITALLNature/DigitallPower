using System.Globalization;
using dgt.power.dataverse;
using Microsoft.Xrm.Sdk;
using Spectre.Console;

namespace dgt.power.maintenance.Logic;

public class WorkflowStateTracker
{
    private Dictionary<string, WorflowChange> _workflowChanges = [];

    internal void AddWorkflows(Workflow[] workflows)
    {
        foreach (var workflow in workflows)
        {
            AddWorkflow(workflow);
        }
    }

    private WorflowChange AddWorkflow(Workflow workflow)
    {
        var identifier = $"{workflow.PrimaryEntity}.{workflow.UniqueName}.{workflow.Name}";

        if (!_workflowChanges.TryGetValue(identifier, out var workflowChange))
        {
            workflowChange = new WorflowChange();
            _workflowChanges.Add(identifier, workflowChange);
        }

        workflowChange.Name = workflow.Name;
        workflowChange.UniqueName = workflow.UniqueName;
        workflowChange.TableName = workflow.PrimaryEntity;
        workflowChange.DisabledPre = workflow.StateCode?.Value != Workflow.Options.StateCode.Activated;

        workflowChange.CategoryName = workflow.Category?.Value switch
        {
            Workflow.Options.Category.ModernFlow => ":cloud: Modern Flow",
            Workflow.Options.Category.Workflow_ => ":gear: Workflow",
            Workflow.Options.Category.Action => ":gear: Action",
            Workflow.Options.Category.BusinessProcessFlow => ":bookmark: Business Process Flow",
            Workflow.Options.Category.BusinessRule => ":scroll: Business Rule",
            _ => workflow.Category?.Value.ToString(CultureInfo.InvariantCulture),
        };

        workflow.TryGetAttributeValue<AliasedValue>("owner.domainname", out var ownerName);
        workflowChange.OwnerPre = (ownerName?.Value ?? workflow.OwnerId!.Id).ToString();

        return workflowChange;
    }

    internal void TrackDisabled(Workflow workflow, bool disabledTarget, bool? disabledPost = default)
    {
        var identifier = $"{workflow.PrimaryEntity}.{workflow.UniqueName}.{workflow.Name}";

        if (!_workflowChanges.TryGetValue(identifier, out var workflowChange))
        {
            workflowChange = AddWorkflow(workflow);
        }

        workflowChange.DisabledTarget = disabledTarget;
        workflowChange.DisabledPost = disabledPost ?? workflowChange.DisabledPre;
    }

    internal void TrackOwner(Workflow workflow, SystemUser? desiredOwnerTarget, SystemUser? desiredOwnerPost = default)
    {
        var identifier = $"{workflow.PrimaryEntity}.{workflow.UniqueName}.{workflow.Name}";

        if (!_workflowChanges.TryGetValue(identifier, out var workflowChange))
        {
            workflowChange = AddWorkflow(workflow);
        }

        workflowChange.OwnerTarget = desiredOwnerTarget?.DomainName ?? desiredOwnerTarget?.SystemUserId.ToString();
        workflowChange.OwnerPost = desiredOwnerPost?.DomainName ?? desiredOwnerPost?.SystemUserId.ToString() ?? workflowChange.OwnerPre;
    }

    internal void WriteToConsole()
    {
        var table = new Table()
            .AddColumn(" ")
            .AddColumn("Unique Name")
            .AddColumn("Name")
            .AddColumn("Category")
            .AddColumn("Table Name")
            .AddColumn("State")
            .AddColumn("Owner");

        var orderedWorfklowChanges = _workflowChanges.Values
            .OrderBy(w => w.TableName)
            .OrderBy(w => w.UniqueName)
            .ThenBy(w => w.Name);

        foreach (var workflowChange in orderedWorfklowChanges)
        {
            var success = workflowChange.DisabledTarget == workflowChange.DisabledPost
                && workflowChange.OwnerTarget == workflowChange.OwnerPost;

            var status = success ? $"[green]{Emoji.Known.CheckMark}[/]" : Emoji.Known.CrossMark;

            var tableNameFormat = workflowChange.TableName != default && workflowChange.TableName != "none" ? "white" : "grey italic";

            var disabledState = workflowChange.GetDisabledState();

            var owner = workflowChange.GetOwnerText();

            var row = new string[] {
                status,
                workflowChange.UniqueName?.EscapeMarkup() ?? "[grey italic]null[/]",
                workflowChange.Name?.EscapeMarkup() ?? "[grey italic]null[/]",
                workflowChange.CategoryName?.EscapeMarkup() ?? "[grey italic]null[/]",
                $"[{tableNameFormat}]{workflowChange.TableName.EscapeMarkup()}[/]",
                disabledState,
                owner,
            };
            table.AddRow(row);
        }

        AnsiConsole.Write(table);
    }
}

internal class WorflowChange
{
    internal string? Name { get; set; }
    internal string? UniqueName { get; set; }
    internal string? TableName { get; set; }
    internal string? CategoryName { get; set; }
    internal bool? DisabledPre { get; set; }
    internal bool? DisabledPost { get; set; }
    internal bool? DisabledTarget { get; set; }
    internal string? OwnerPre { get; set; }
    internal string? OwnerPost { get; set; }
    internal string? OwnerTarget { get; set; }

    internal string GetDisabledState()
    {
        return (DisabledPre, DisabledTarget, DisabledPost) switch
        {
            (true, true, _) => "[grey]Disabled[/]",
            (false, false, _) => "[grey]Enabled[/]",
            (_, true, true) => $"[blue]Enabled[/] {Emoji.Known.RightArrow} [green]Disabled[/]",
            (_, false, false) => $"[blue]Disabled[/] {Emoji.Known.RightArrow} [green]Enabled[/]",
            (_, true, false) => $"[red]Disabled[/] [strikethrough grey]{Emoji.Known.RightArrow} Disabled[/]",
            (_, false, true) => $"[red]Enabled[/] [strikethrough grey]{Emoji.Known.RightArrow} Enabled[/]",
            (true, null, null) => "[blue]Disabled[/]",
            (false, null, null) => "[blue]Enabled[/]",
            _ => "[orange3]Unknown[/]",
        };
    }

    internal string GetOwnerText()
    {
        if (OwnerPre == OwnerTarget) return $"[grey]{OwnerPre}[/]";
        if (!string.IsNullOrWhiteSpace(OwnerPost) && OwnerPost == OwnerTarget) return $"[blue]{OwnerPre}[/] {Emoji.Known.RightArrow} [green]{OwnerPost}[/]";
        if (OwnerPost == OwnerTarget) return $"[blue]{OwnerPre}[/]";
        return $"[red]{OwnerPre}[/] [strikethrough grey]{Emoji.Known.RightArrow} {OwnerPost}[/]";
    }
}
