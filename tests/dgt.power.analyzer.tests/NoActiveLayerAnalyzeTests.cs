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

public class NoActiveLayerAnalyzeTests : AnalyzeTestsBase<NoActiveLayerAnalyze>
{
    private const string SolutionUniqueName = "customizations";

    public NoActiveLayerAnalyzeTests(ITestOutputHelper testOutputHelper) : base(testOutputHelper)
    {
    }

    protected override CommandTestContext<NoActiveLayerAnalyze, AnalyzeVerb> GetContext()
    {
        return GetBuilder()
            .WithFakeMessageExecutor<RetrieveAllEntitiesRequest>(new RetrieveAllEntitiesExecutor())
            .WithData(PrepareData)
            .WithMetaData(new[]
            {
                new EntityMetadata
                {
                    LogicalName = TestEntity.EntityLogicalName,
                    MetadataId = Guid.NewGuid(),
                    DisplayName = new Label
                    {
                        UserLocalizedLabel = new LocalizedLabel("Test Entity", 1031)
                    }
                },
            })
            .Build();
    }

    private IEnumerable<Entity> PrepareData(IXrmFakedContext context)
    {
        var solution = new Solution(Guid.NewGuid())
        {
            UniqueName = SolutionUniqueName
        };
        var entityComponent = new SolutionComponent(Guid.NewGuid())
        {
            [SolutionComponent.LogicalNames.ComponentType] =
                new OptionSetValue(SolutionComponent.Options.ComponentType.Entity),
            [SolutionComponent.LogicalNames.RootComponentBehavior] =
                new OptionSetValue(SolutionComponent.Options.RootComponentBehavior.IncludeSubcomponents),
            [SolutionComponent.LogicalNames.ObjectId] =
                context.GetEntityMetadataByName(TestEntity.EntityLogicalName).MetadataId,
            [SolutionComponent.LogicalNames.IsMetadata] = true,
            [SolutionComponent.LogicalNames.SolutionId] = solution.ToEntityReference(),
            FormattedValues =
            {
                {SolutionComponent.LogicalNames.ComponentType, "Entity"}
            }
        };
        var formComponent = new SolutionComponent(Guid.NewGuid())
        {
            [SolutionComponent.LogicalNames.ComponentType] =
                new OptionSetValue(SolutionComponent.Options.ComponentType.SystemForm),
            [SolutionComponent.LogicalNames.RootSolutionComponentId] = entityComponent.Id,
            [SolutionComponent.LogicalNames.ObjectId] = Guid.NewGuid(),
            [SolutionComponent.LogicalNames.IsMetadata] = true,
            [SolutionComponent.LogicalNames.SolutionId] = solution.ToEntityReference(),
            FormattedValues =
            {
                {SolutionComponent.LogicalNames.ComponentType, "SystemForm"}
            }
        };
        var formLayer = new MsdynComponentlayer(Guid.NewGuid())
        {
            MsdynName = "Test Formular",
            MsdynSolutioncomponentname = "SystemForm",
            MsdynSolutionname = SolutionUniqueName,
            MsdynOrder = 1,
            MsdynComponentid = $"{formComponent.ObjectId:B}"
        };
        var entityLayer = new MsdynComponentlayer(Guid.NewGuid())
        {
            MsdynName = "Test Entity",
            MsdynSolutioncomponentname = "Entity",
            MsdynSolutionname = SolutionUniqueName,
            MsdynOrder = 1,
            MsdynComponentid = $"{entityComponent.ObjectId:B}"
        };
        var entityLayerActive = new MsdynComponentlayer(Guid.NewGuid())
        {
            MsdynName = "Test Entity",
            MsdynSolutioncomponentname = "Entity",
            MsdynSolutionname = "Active",
            MsdynOrder = 2,
            MsdynComponentid = $"{entityComponent.ObjectId:B}"
        };
        return new Entity[]
        {
            solution,
            entityComponent,
            entityLayer,
            entityLayerActive,
            formComponent,
            formLayer,
            new SolutionComponent(Guid.NewGuid())
            {
                [SolutionComponent.LogicalNames.ComponentType] =
                    new OptionSetValue(SolutionComponent.Options.ComponentType.EmailTemplate),
                [SolutionComponent.LogicalNames.ObjectId] = Guid.NewGuid(),
                [SolutionComponent.LogicalNames.IsMetadata] = true,
                [SolutionComponent.LogicalNames.SolutionId] = solution.ToEntityReference(),
                FormattedValues =
                {
                    {SolutionComponent.LogicalNames.ComponentType, "Email Template"}
                }
            }
        };
    }

    [Fact]
    public void ShouldFailOnMissingInlineData() =>
        GetContext()
            .Execute(new AnalyzeVerb
            {
                InlineData = string.Empty
            }).Should().BeFalse();

    [Fact]
    public void ShouldAnalyzeActiveLayer() =>
        GetContext()
            .Execute(new AnalyzeVerb
            {
                InlineData = SolutionUniqueName
            })
            .Should().BeTrue();
}