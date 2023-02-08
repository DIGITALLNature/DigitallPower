using System.Globalization;
using System.Text.Json;
using CsvHelper;
using dgt.power.analyzer.Base;
using dgt.power.analyzer.Logic;
using dgt.power.analyzer.Reports;
using dgt.power.analyzer.tests.Base;
using dgt.power.dataverse;
using dgt.power.dto;
using dgt.power.tests;
using dgt.power.tests.FakeExecutor;
using FakeXrmEasy.Abstractions;
using Microsoft.VisualStudio.TestPlatform.Utilities;
using Microsoft.Xrm.Sdk;
using Microsoft.Xrm.Sdk.Messages;
using Microsoft.Xrm.Sdk.Metadata;
using Spectre.Console;
using Spectre.Console.Testing;

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
    public void ShouldAnalyzeNoActiveLayer()
    {
        AnsiConsole.Record();
        GetContext()
            .Execute(new AnalyzeVerb
            {
                InlineData = SolutionUniqueName,
                GenerateSummaryFile = true,
                GenerateReportFile = true
            })
            .Should().BeTrue();

        var output = AnsiConsole.ExportText();
        Assert.StartsWith("── solution unique name: customizations ──", output);
        Assert.True(File.Exists(Path.Combine(BaseAnalyze.ResultFolder,"NoActiveLayer-summary.json")));
        Assert.True(File.Exists(Path.Combine(BaseAnalyze.ResultFolder,"NoActiveLayer-result.csv")));

        // Check Summary
        var summary = JsonSerializer.Deserialize<AnalyzerSummary>(File.ReadAllBytes(Path.Combine(BaseAnalyze.ResultFolder, "NoActiveLayer-summary.json")));
        Assert.Equal(1,summary.Anomalies);

        // Check Result
        using var reader = new StreamReader(Path.Combine(BaseAnalyze.ResultFolder, "NoActiveLayer-result.csv"));
        using var csv = new CsvReader(reader, CultureInfo.InvariantCulture);
        var results = csv.GetRecords<ActiveLayerLine>().ToArray();
        Assert.Equal(1,results.Length);
        Assert.Equal("SystemForm", results[0].Component);
        Assert.Equal("Test Formular (testentity)", results[0].Name);
        Assert.Equal(SolutionUniqueName, results[0].Solution);
        Assert.Equal(1, results[0].Order);
    }
}
