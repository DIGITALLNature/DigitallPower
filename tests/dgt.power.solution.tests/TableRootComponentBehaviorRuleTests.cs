// Copyright (c) DIGITALL Nature. All rights reserved
// DIGITALL Nature licenses this file to you under the Microsoft Public License.

using dgt.power.dataverse;
using dgt.power.solution.Rules;
using dgt.power.solution.tests.Base;
using dgt.power.tests.Extensions;
using dgt.power.tests.FakeExecutor;
using Digitall.Dataverse.Testing;

namespace dgt.power.solution.tests;

public class TableRootComponentBehaviorRuleTests : LintTestsBase<SolutionLintCommand>
{
    private const string SolutionName = "sample_solution";

    protected override CommandTestContext<SolutionLintCommand, SolutionLintSettings> GetContext() => CreateContext();

    [Test]
    public async Task EvaluateAsync_FlagsCompleteManagedTablesAndPartialUnmanagedTables()
    {
        var findings = await EvaluateAsync();

        var flaggedEntities = findings.Select(finding => finding.ComponentLogicalName!).ToHashSet(StringComparer.Ordinal);
        await Assert.That(flaggedEntities).IsEquivalentTo(
        [
            "dgt_unmanaged_partial", // unmanaged table not included completely -> violation
            "dgt_unmanaged_shell", // unmanaged table not included completely -> violation
            "isv_managed_complete" // managed table included completely -> violation
        ]);
        foreach (var finding in findings)
        {
            await Assert.That(finding.RuleId).IsEqualTo("completeness.table-root-component-behavior");
            await Assert.That(finding.ComponentType).IsEqualTo("Entity");
        }
    }

    private async Task<IReadOnlyList<LintFinding>> EvaluateAsync()
    {
        var testContext = CreateContext();
        var context = new LintContext(testContext.FakedService, [SolutionName]);
        return await new TableRootComponentBehaviorRule().EvaluateAsync(context, ruleConfig: null, CancellationToken.None);
    }

    private CommandTestContext<SolutionLintCommand, SolutionLintSettings> CreateContext()
    {
        return GetBuilder()
            .WithFakeMessageExecutor(new RetrieveAllEntitiesExecutor())
            .WithMetaData(BuildEntities())
            .WithData(PrepareData)
            .Build();
    }

    // One entity per (IsManaged x RootComponentBehavior) combination - only the two "correct"
    // combinations (unmanaged+complete, managed+partial/shell) must never be flagged.
    private static EntityMetadata[] BuildEntities() =>
    [
        CreateEntity("dgt_unmanaged_complete", isManaged: false),
        CreateEntity("dgt_unmanaged_partial", isManaged: false),
        CreateEntity("dgt_unmanaged_shell", isManaged: false),
        CreateEntity("isv_managed_complete", isManaged: true),
        CreateEntity("isv_managed_partial", isManaged: true),
        CreateEntity("isv_managed_shell", isManaged: true)
    ];

    private static EntityMetadata CreateEntity(string logicalName, bool isManaged)
    {
        var entity = new EntityMetadata { LogicalName = logicalName, MetadataId = Guid.NewGuid() };
        entity.SetSealedPropertyValue(nameof(EntityMetadata.IsManaged), isManaged);
        return entity;
    }

    private static IEnumerable<Entity> PrepareData(FakeOrganizationServiceAsync service)
    {
        var solution = new Solution(Guid.NewGuid()) { UniqueName = SolutionName };

        var behaviorByEntity = new Dictionary<string, int>(StringComparer.Ordinal)
        {
            ["dgt_unmanaged_complete"] = SolutionComponent.Options.RootComponentBehavior.IncludeSubcomponents,
            ["dgt_unmanaged_partial"] = SolutionComponent.Options.RootComponentBehavior.DoNotIncludeSubcomponents,
            ["dgt_unmanaged_shell"] = SolutionComponent.Options.RootComponentBehavior.IncludeAsShellOnly,
            ["isv_managed_complete"] = SolutionComponent.Options.RootComponentBehavior.IncludeSubcomponents,
            ["isv_managed_partial"] = SolutionComponent.Options.RootComponentBehavior.DoNotIncludeSubcomponents,
            ["isv_managed_shell"] = SolutionComponent.Options.RootComponentBehavior.IncludeAsShellOnly
        };

        var components = behaviorByEntity.Select(entry =>
        {
            var metadata = service.State.EntityMetadata[entry.Key];
            return new SolutionComponent(Guid.NewGuid())
            {
                [SolutionComponent.LogicalNames.ComponentType] = new OptionSetValue(SolutionComponent.Options.ComponentType.Entity),
                [SolutionComponent.LogicalNames.ObjectId] = metadata.MetadataId,
                [SolutionComponent.LogicalNames.SolutionId] = solution.ToEntityReference(),
                [SolutionComponent.LogicalNames.IsMetadata] = true,
                [SolutionComponent.LogicalNames.RootComponentBehavior] = new OptionSetValue(entry.Value)
            };
        }).Cast<Entity>();

        return [solution, .. components];
    }
}
