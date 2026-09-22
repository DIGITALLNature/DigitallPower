// Copyright (c) DIGITALL Nature. All rights reserved
// DIGITALL Nature licenses this file to you under the Microsoft Public License.

using dgt.power.dataverse;
using dgt.power.solution.Base;
using dgt.power.tests.Extensions;
using Microsoft.Xrm.Sdk;
using Microsoft.Xrm.Sdk.Metadata;

namespace dgt.power.solution.tests;

public class EntityComponentMembershipResolverTests
{
    private const string SolutionName = "sample_solution";
    private const string EntityLogicalName = "account";

    [Test]
    public async Task Resolve_IncludeSubcomponents_ReturnsAllAttributesEvenWithoutExplicitRows()
    {
        var (entityMetadata, attributeA, attributeB) = BuildEntityMetadata();
        var solutionId = Guid.NewGuid();
        var entityComponent = CreateEntityComponent(solutionId, entityMetadata.MetadataId!.Value, SolutionComponent.Options.RootComponentBehavior.IncludeSubcomponents);

        var memberships = EntityComponentMembershipResolver.Resolve(
            new Dictionary<Guid, SolutionComponent> { [entityComponent.Id] = entityComponent },
            new Dictionary<Guid, string> { [solutionId] = SolutionName },
            new Dictionary<string, EntityMetadata> { [EntityLogicalName] = entityMetadata },
            new Dictionary<Guid, AttributeMetadata> { [attributeA.MetadataId!.Value] = attributeA, [attributeB.MetadataId!.Value] = attributeB });

        var membership = memberships[EntityComponentMembershipResolver.BuildKey(SolutionName, EntityLogicalName)];
        await Assert.That(membership.EffectiveAttributes.Select(a => a.LogicalName)).IsEquivalentTo(["dgt_a", "dgt_b"]);
        await Assert.That(membership.ExplicitSubcomponentsByType).IsEmpty();
    }

    [Test]
    public async Task Resolve_DoNotIncludeSubcomponents_ReturnsOnlyExplicitlyListedAttributes()
    {
        var (entityMetadata, attributeA, attributeB) = BuildEntityMetadata();
        var solutionId = Guid.NewGuid();
        var entityComponent = CreateEntityComponent(solutionId, entityMetadata.MetadataId!.Value, SolutionComponent.Options.RootComponentBehavior.DoNotIncludeSubcomponents);
        var attributeComponent = CreateAttributeComponent(solutionId, attributeA.MetadataId!.Value, entityComponent.Id);

        var memberships = EntityComponentMembershipResolver.Resolve(
            new Dictionary<Guid, SolutionComponent> { [entityComponent.Id] = entityComponent, [attributeComponent.Id] = attributeComponent },
            new Dictionary<Guid, string> { [solutionId] = SolutionName },
            new Dictionary<string, EntityMetadata> { [EntityLogicalName] = entityMetadata },
            new Dictionary<Guid, AttributeMetadata> { [attributeA.MetadataId!.Value] = attributeA, [attributeB.MetadataId!.Value] = attributeB });

        var membership = memberships[EntityComponentMembershipResolver.BuildKey(SolutionName, EntityLogicalName)];
        await Assert.That(membership.EffectiveAttributes.Select(a => a.LogicalName)).IsEquivalentTo(["dgt_a"]);
        await Assert.That(membership.ExplicitSubcomponentsByType[SolutionComponent.Options.ComponentType.Attribute]).Count().IsEqualTo(1);
    }

    [Test]
    public async Task Resolve_IncludeAsShell_ReturnsNoEffectiveAttributes()
    {
        var (entityMetadata, _, _) = BuildEntityMetadata();
        var solutionId = Guid.NewGuid();
        var entityComponent = CreateEntityComponent(solutionId, entityMetadata.MetadataId!.Value, SolutionComponent.Options.RootComponentBehavior.IncludeAsShellOnly);

        var memberships = EntityComponentMembershipResolver.Resolve(
            new Dictionary<Guid, SolutionComponent> { [entityComponent.Id] = entityComponent },
            new Dictionary<Guid, string> { [solutionId] = SolutionName },
            new Dictionary<string, EntityMetadata> { [EntityLogicalName] = entityMetadata },
            new Dictionary<Guid, AttributeMetadata>());

        var membership = memberships[EntityComponentMembershipResolver.BuildKey(SolutionName, EntityLogicalName)];
        await Assert.That(membership.EffectiveAttributes).IsEmpty();
    }

    [Test]
    public async Task Resolve_ExposesWhetherTheTableItselfIsManaged()
    {
        var (entityMetadata, _, _) = BuildEntityMetadata(isTableManaged: true);
        var solutionId = Guid.NewGuid();
        var entityComponent = CreateEntityComponent(solutionId, entityMetadata.MetadataId!.Value, SolutionComponent.Options.RootComponentBehavior.DoNotIncludeSubcomponents);

        var memberships = EntityComponentMembershipResolver.Resolve(
            new Dictionary<Guid, SolutionComponent> { [entityComponent.Id] = entityComponent },
            new Dictionary<Guid, string> { [solutionId] = SolutionName },
            new Dictionary<string, EntityMetadata> { [EntityLogicalName] = entityMetadata },
            new Dictionary<Guid, AttributeMetadata>());

        var membership = memberships[EntityComponentMembershipResolver.BuildKey(SolutionName, EntityLogicalName)];
        await Assert.That(membership.IsTableManaged).IsTrue();
    }

    private static (EntityMetadata Entity, AttributeMetadata AttributeA, AttributeMetadata AttributeB) BuildEntityMetadata(bool isTableManaged = false)
    {
        var attributeA = new StringAttributeMetadata { MetadataId = Guid.NewGuid(), LogicalName = "dgt_a" };
        var attributeB = new StringAttributeMetadata { MetadataId = Guid.NewGuid(), LogicalName = "dgt_b" };

        var entity = new EntityMetadata { LogicalName = EntityLogicalName, MetadataId = Guid.NewGuid() };
        entity.SetSealedPropertyValue(nameof(EntityMetadata.IsManaged), isTableManaged);
        entity.SetAttributeCollection([attributeA, attributeB]);

        return (entity, attributeA, attributeB);
    }

    private static SolutionComponent CreateEntityComponent(Guid solutionId, Guid entityMetadataId, int behavior) => new(Guid.NewGuid())
    {
        [SolutionComponent.LogicalNames.ComponentType] = new OptionSetValue(SolutionComponent.Options.ComponentType.Entity),
        [SolutionComponent.LogicalNames.ObjectId] = entityMetadataId,
        [SolutionComponent.LogicalNames.SolutionId] = new EntityReference(Solution.EntityLogicalName, solutionId),
        [SolutionComponent.LogicalNames.RootComponentBehavior] = new OptionSetValue(behavior)
    };

    private static SolutionComponent CreateAttributeComponent(Guid solutionId, Guid attributeMetadataId, Guid rootSolutionComponentId) => new(Guid.NewGuid())
    {
        [SolutionComponent.LogicalNames.ComponentType] = new OptionSetValue(SolutionComponent.Options.ComponentType.Attribute),
        [SolutionComponent.LogicalNames.ObjectId] = attributeMetadataId,
        [SolutionComponent.LogicalNames.SolutionId] = new EntityReference(Solution.EntityLogicalName, solutionId),
        [SolutionComponent.LogicalNames.RootSolutionComponentId] = rootSolutionComponentId
    };
}
