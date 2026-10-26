// Copyright (c) DIGITALL Nature. All rights reserved

using dgt.power.common.Extensions;
using dgt.power.common.Ownership;
using dgt.power.dataverse;
using dgt.power.duplicaterules.Configuration;
using Microsoft.Crm.Sdk.Messages;
using Microsoft.PowerPlatform.Dataverse.Client;
using Microsoft.Xrm.Sdk;
using Microsoft.Xrm.Sdk.Query;

namespace dgt.power.duplicaterules.Logic;

public sealed class DuplicateRulePreflight(IOwnerResolver ownerResolver)
{
    public const int MaximumPublishedRulesPerTable = 5;
    public const string ReadPrivilegeName = "prvReadDuplicateRule";

    public Task<(IReadOnlyList<DuplicateRulePlan> Plans, IReadOnlyList<DuplicateRuleCapacity> Capacity)> PrepareAsync(
        IOrganizationServiceAsync2 service,
        DuplicateRuleConfiguration configuration,
        OwnerMapping? mapping,
        CancellationToken cancellationToken)
    {
        ArgumentNullException.ThrowIfNull(service);
        ArgumentNullException.ThrowIfNull(configuration);
        return PrepareCoreAsync(service, configuration, mapping, cancellationToken);
    }

    private async Task<(IReadOnlyList<DuplicateRulePlan> Plans, IReadOnlyList<DuplicateRuleCapacity> Capacity)> PrepareCoreAsync(
        IOrganizationServiceAsync2 service,
        DuplicateRuleConfiguration configuration,
        OwnerMapping? mapping,
        CancellationToken cancellationToken)
    {
        var intents = configuration.Rules;
        var query = new QueryExpression(DuplicateRule.EntityLogicalName)
        {
            ColumnSet = new ColumnSet(DuplicateRuleStates.Columns)
        };
        query.Criteria.AddCondition("duplicateruleid", ConditionOperator.In, intents.Select(intent => (object)intent.RuleId).ToArray());
        var rules = (await service.RetrieveAllAsync(query, cancellationToken: cancellationToken))
            .DistinctBy(rule => rule.Id)
            .ToDictionary(rule => rule.Id);
        var missing = intents.Where(intent => !rules.ContainsKey(intent.RuleId)).Select(intent => intent.RuleId.ToString("D")).ToArray();
        if (missing.Length > 0)
        {
            throw new InvalidOperationException($"Duplicate rules were not found for ID(s): {string.Join(", ", missing)}. Rules are matched by ID only.");
        }

        var unstable = intents
            .Select(intent => rules[intent.RuleId])
            .Where(rule => DuplicateRuleStates.Classify(rule) is DuplicateRulePublicationState.Publishing or DuplicateRulePublicationState.Unknown)
            .Select(rule => $"{rule.Id:D} is {DuplicateRuleStates.Describe(rule)}")
            .ToArray();
        if (unstable.Length > 0)
        {
            throw new InvalidOperationException(
                $"Listed duplicate rule(s) are not in a stable Published or Unpublished state: {string.Join("; ", unstable)}.");
        }

        var ownerKeys = intents.Where(intent => intent.Owner is not null).Select(intent => intent.Owner!).Distinct(StringComparer.Ordinal).ToArray();
        IReadOnlyDictionary<string, ResolvedOwner> owners = new Dictionary<string, ResolvedOwner>(StringComparer.Ordinal);
        if (ownerKeys.Length > 0)
        {
            if (mapping is null)
            {
                throw new InvalidOperationException("An owner-mapping file is required to resolve owner keys.");
            }

            owners = await ownerResolver.ResolveAsync(service, mapping, ownerKeys, cancellationToken);
        }

        var plans = new List<DuplicateRulePlan>();
        foreach (var intent in intents)
        {
            var rule = rules[intent.RuleId];
            var state = DuplicateRuleStates.Classify(rule);
            var currentOwner = rule.GetAttributeValue<EntityReference>("ownerid")
                ?? throw new InvalidOperationException($"Duplicate rule '{intent.RuleId:D}' has no owner reference.");
            var baseTable = rule.GetAttributeValue<string>("baseentityname")
                ?? throw new InvalidOperationException($"Duplicate rule '{intent.RuleId:D}' has no base table.");
            var desiredOwner = intent.Owner is null ? null : owners[intent.Owner];
            var needsAssignment = desiredOwner is not null &&
                                  (desiredOwner.Principal.Id != currentOwner.Id ||
                                   !string.Equals(desiredOwner.Principal.LogicalName, currentOwner.LogicalName, StringComparison.Ordinal));
            if (needsAssignment && desiredOwner!.IsDisabled is true)
            {
                throw new InvalidOperationException(
                    $"Duplicate rule '{intent.RuleId:D}' owner key '{desiredOwner.LogicalKey}' resolves to a disabled user.");
            }

            var isPublished = state == DuplicateRulePublicationState.Published;
            var desiredPublished = intent.Published ?? isPublished;
            plans.Add(new DuplicateRulePlan(
                intent,
                rule.GetAttributeValue<string>("name") ?? string.Empty,
                baseTable,
                state,
                desiredOwner,
                NeedsUnpublish: isPublished && !desiredPublished,
                NeedsAssignment: needsAssignment,
                NeedsPublish: !isPublished && desiredPublished));
        }

        await EnsureReadPrivilegeAsync(service, plans, cancellationToken);
        var capacity = await ProjectCapacityAsync(service, plans, cancellationToken);
        return (plans, capacity);
    }

    private static async Task EnsureReadPrivilegeAsync(
        IOrganizationServiceAsync2 service,
        IReadOnlyList<DuplicateRulePlan> plans,
        CancellationToken cancellationToken)
    {
        var newOwners = plans
            .Where(plan => plan.NeedsAssignment)
            .Select(plan => plan.DesiredOwner!)
            .DistinctBy(owner => (owner.Principal.LogicalName, owner.Principal.Id))
            .ToArray();
        if (newOwners.Length == 0)
        {
            return;
        }

        var privilegeQuery = new QueryExpression("privilege")
        {
            ColumnSet = new ColumnSet("privilegeid", "name")
        };
        privilegeQuery.Criteria.AddCondition("name", ConditionOperator.Equal, ReadPrivilegeName);
        var privileges = await service.RetrieveAllAsync(privilegeQuery, cancellationToken: cancellationToken);
        if (privileges.Count != 1)
        {
            throw new InvalidOperationException($"The privilege '{ReadPrivilegeName}' could not be resolved uniquely in the target environment.");
        }

        var privilegeId = privileges[0].Id;
        var lacking = new List<string>();
        foreach (var owner in newOwners)
        {
            cancellationToken.ThrowIfCancellationRequested();
            RolePrivilege[] granted = owner.Kind == OwnerKind.Team
                ? ((RetrieveTeamPrivilegesResponse)await service.ExecuteAsync(
                    new RetrieveTeamPrivilegesRequest { TeamId = owner.Principal.Id },
                    cancellationToken)).RolePrivileges
                : ((RetrieveUserPrivilegesResponse)await service.ExecuteAsync(
                    new RetrieveUserPrivilegesRequest { UserId = owner.Principal.Id },
                    cancellationToken)).RolePrivileges;
            if (!Array.Exists(granted ?? [], privilege =>
                    privilege.PrivilegeId == privilegeId ||
                    string.Equals(privilege.PrivilegeName, ReadPrivilegeName, StringComparison.OrdinalIgnoreCase)))
            {
                lacking.Add($"'{owner.LogicalKey}' ({owner.Kind} {owner.Principal.Id:D})");
            }
        }

        if (lacking.Count > 0)
        {
            throw new InvalidOperationException(
                $"New duplicate-rule owner(s) lack the {ReadPrivilegeName} privilege required for assignment: {string.Join(", ", lacking)}.");
        }
    }

    private static async Task<IReadOnlyList<DuplicateRuleCapacity>> ProjectCapacityAsync(
        IOrganizationServiceAsync2 service,
        IReadOnlyList<DuplicateRulePlan> plans,
        CancellationToken cancellationToken)
    {
        var tables = plans
            .Where(plan => plan.NeedsPublish)
            .Select(plan => plan.BaseTable)
            .Distinct(StringComparer.OrdinalIgnoreCase)
            .ToArray();
        if (tables.Length == 0)
        {
            return [];
        }

        var query = new QueryExpression(DuplicateRule.EntityLogicalName)
        {
            ColumnSet = new ColumnSet(DuplicateRuleStates.Columns)
        };
        query.Criteria.AddCondition("baseentityname", ConditionOperator.In, tables.Cast<object>().ToArray());
        var existing = (await service.RetrieveAllAsync(query, cancellationToken: cancellationToken))
            .DistinctBy(rule => rule.Id)
            .ToArray();

        var result = new List<DuplicateRuleCapacity>();
        foreach (var table in tables)
        {
            var occupied = existing.Count(rule =>
                string.Equals(rule.GetAttributeValue<string>("baseentityname"), table, StringComparison.OrdinalIgnoreCase) &&
                DuplicateRuleStates.Classify(rule) is DuplicateRulePublicationState.Published or DuplicateRulePublicationState.Publishing);
            var inTable = plans.Where(plan => string.Equals(plan.BaseTable, table, StringComparison.OrdinalIgnoreCase)).ToArray();
            result.Add(new DuplicateRuleCapacity(
                table,
                occupied,
                inTable.Count(plan => plan.NeedsUnpublish),
                inTable.Count(plan => plan.NeedsPublish)));
        }

        var exceeded = result.Where(capacity => capacity.Projected > MaximumPublishedRulesPerTable).ToArray();
        if (exceeded.Length > 0)
        {
            throw new InvalidOperationException(
                "Requested publications exceed the limit of " + MaximumPublishedRulesPerTable + " published rules per base table: " +
                string.Join("; ", exceeded.Select(capacity =>
                    $"'{capacity.BaseTable}' has {capacity.Occupied} occupied, {capacity.Released} requested unpublish(es), " +
                    $"{capacity.Added} requested publish(es), projected {capacity.Projected}")) +
                ". Unlisted rules are never unpublished to make room.");
        }

        return result;
    }
}
