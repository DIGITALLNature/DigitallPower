using dgt.power.analyzer.Base;
using dgt.power.analyzer.Logic;
using dgt.power.analyzer.tests.Base;
using dgt.power.dataverse;
using dgt.power.tests;
using dgt.power.tests.FakeExecutor;
using FakeXrmEasy.Abstractions;
using Microsoft.Xrm.Sdk;
using Microsoft.Xrm.Sdk.Messages;
using Microsoft.Xrm.Sdk.Metadata;

namespace dgt.power.analyzer.tests;

public class EntityAllAssetsAnalyzeTests : AnalyzeTestsBase<EntityAllAssetsAnalyze>
{
    public EntityAllAssetsAnalyzeTests(ITestOutputHelper testOutputHelper) : base(testOutputHelper)
    {
    }

    protected override CommandTestContext<EntityAllAssetsAnalyze, AnalyzeVerb> GetContext()
    {
        return GetBuilder()
            .WithMetaData(new[]
            {
                new EntityMetadata
                {
                    LogicalName = SystemUser.EntityLogicalName,
                    MetadataId = Guid.NewGuid(),
                    DisplayName = new Label
                    {
                        UserLocalizedLabel = new LocalizedLabel("User", 1031)
                    }
                },
                new EntityMetadata
                {
                    LogicalName = TestEntity.EntityLogicalName,
                    MetadataId = Guid.NewGuid(),
                    DisplayName = new Label
                    {
                        UserLocalizedLabel = new LocalizedLabel("Test Entity", 1031)
                    }
                },
                new EntityMetadata
                {
                    LogicalName = Team.EntityLogicalName,
                    MetadataId = Guid.NewGuid(),
                    DisplayName = new Label
                    {
                        UserLocalizedLabel = new LocalizedLabel("Team", 1031)
                    }
                },
                new EntityMetadata
                {
                    LogicalName = Queue.EntityLogicalName,
                    MetadataId = Guid.NewGuid(),
                    DisplayName = new Label
                    {
                        UserLocalizedLabel = new LocalizedLabel("Queue", 1031)
                    }
                },
                new EntityMetadata
                {
                    LogicalName = Contact.EntityLogicalName,
                    MetadataId = Guid.NewGuid(),
                    DisplayName = new Label
                    {
                        UserLocalizedLabel = new LocalizedLabel("Contact", 1031)
                    }
                }
            })
            .WithData(PrepareData)
            .WithFakeMessageExecutor<RetrieveAllEntitiesRequest>(new RetrieveAllEntitiesExecutor())
            .Build();
    }

    private IEnumerable<Entity> PrepareData(IXrmFakedContext context)
    {
        var testEntityMetadata = context.GetEntityMetadataByName(TestEntity.EntityLogicalName);
        var userMetadata = context.GetEntityMetadataByName(SystemUser.EntityLogicalName);
        var teamMetadata = context.GetEntityMetadataByName(Team.EntityLogicalName);
        var queueMetadata = context.GetEntityMetadataByName(Queue.EntityLogicalName);
        var contactMetadata = context.GetEntityMetadataByName(Contact.EntityLogicalName);

        var solution = new Solution(Guid.NewGuid())
        {
            UniqueName = "customizations"
        };
        var testEntityComponent = new SolutionComponent(Guid.NewGuid())
        {
            [SolutionComponent.LogicalNames.ComponentType] =
                new OptionSetValue(SolutionComponent.Options.ComponentType.Entity),
            [SolutionComponent.LogicalNames.RootComponentBehavior] =
                new OptionSetValue(SolutionComponent.Options.RootComponentBehavior.IncludeSubcomponents),
            [SolutionComponent.LogicalNames.ObjectId] = testEntityMetadata.MetadataId,
            [SolutionComponent.LogicalNames.IsMetadata] = true,
            [SolutionComponent.LogicalNames.SolutionId] = solution.ToEntityReference()
        };
        var accountComponent = new SolutionComponent(Guid.NewGuid())
        {
            [SolutionComponent.LogicalNames.ComponentType] =
                new OptionSetValue(SolutionComponent.Options.ComponentType.Entity),
            [SolutionComponent.LogicalNames.RootComponentBehavior] =
                new OptionSetValue(SolutionComponent.Options.RootComponentBehavior.IncludeSubcomponents),
            [SolutionComponent.LogicalNames.ObjectId] = userMetadata.MetadataId,
            [SolutionComponent.LogicalNames.IsMetadata] = true,
            [SolutionComponent.LogicalNames.SolutionId] = solution.ToEntityReference()
        };
        var teamComponent = new SolutionComponent(Guid.NewGuid())
        {
            [SolutionComponent.LogicalNames.ComponentType] =
                new OptionSetValue(SolutionComponent.Options.ComponentType.Entity),
            [SolutionComponent.LogicalNames.RootComponentBehavior] =
                new OptionSetValue(SolutionComponent.Options.RootComponentBehavior.DoNotIncludeSubcomponents),
            [SolutionComponent.LogicalNames.ObjectId] = teamMetadata.MetadataId,
            [SolutionComponent.LogicalNames.IsMetadata] = true,
            [SolutionComponent.LogicalNames.SolutionId] = solution.ToEntityReference()
        };
        var queueComponent = new SolutionComponent(Guid.NewGuid())
        {
            [SolutionComponent.LogicalNames.ComponentType] =
                new OptionSetValue(SolutionComponent.Options.ComponentType.Entity),
            [SolutionComponent.LogicalNames.RootComponentBehavior] =
                new OptionSetValue(SolutionComponent.Options.RootComponentBehavior.DoNotIncludeSubcomponents),
            [SolutionComponent.LogicalNames.ObjectId] = queueMetadata.MetadataId,
            [SolutionComponent.LogicalNames.IsMetadata] = true,
            [SolutionComponent.LogicalNames.SolutionId] = solution.ToEntityReference()
        };
        var contactComponent = new SolutionComponent(Guid.NewGuid())
        {
            [SolutionComponent.LogicalNames.ComponentType] =
                new OptionSetValue(SolutionComponent.Options.ComponentType.Entity),
            [SolutionComponent.LogicalNames.RootComponentBehavior] =
                new OptionSetValue(SolutionComponent.Options.RootComponentBehavior.IncludeAsShellOnly),
            [SolutionComponent.LogicalNames.ObjectId] = contactMetadata.MetadataId,
            [SolutionComponent.LogicalNames.IsMetadata] = true,
            [SolutionComponent.LogicalNames.SolutionId] = solution.ToEntityReference()
        };

        return new Entity[]
        {
            solution,
            accountComponent,
            testEntityComponent,
            teamComponent,
            queueComponent, 
            contactComponent
        };
    }

    [Fact]
    public void ShouldFailOnMissingConfiguration() =>
        GetContext()
            .Execute(new AnalyzeVerb
                {
                    Config = "missing.json"
                }
            ).Should().BeFalse();

    [Fact]
    public void ShouldFailOnEmptyConfiguration() =>
        GetContext()
            .Execute(new AnalyzeVerb
                {
                    Config = GetResourcePath("empty.json")
                }
            ).Should().BeFalse();

    [Fact]
    public void ShouldApproveAllEntityAssetsAnalysis() =>
        GetContext()
            .Execute(new AnalyzeVerb
                {
                    Config = GetResourcePath("approved.json")
                }
            ).Should().BeTrue();

    [Fact]
    public void ShouldNotApproveAllEntityAssetsAnalysis() =>
        GetContext()
            .Execute(new AnalyzeVerb
                {
                    Config = GetResourcePath("unapproved.json")
                }
            ).Should().BeFalse();

    [Fact]
    public void ShouldApproveAllEntityAssetsWithWarning() =>
        GetContext()
            .Execute(new AnalyzeVerb
                {
                    Config = GetResourcePath("approved-not-strict.json")
                }
            ).Should().BeTrue();
}