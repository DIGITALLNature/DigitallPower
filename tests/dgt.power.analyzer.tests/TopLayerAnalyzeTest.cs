// Copyright (c) DIGITALL Nature. All rights reserved
// DIGITALL Nature licenses this file to you under the Microsoft Public License.

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
using Digitall.Dataverse.Testing;
using Microsoft.Xrm.Sdk;
using Microsoft.Xrm.Sdk.Messages;
using Microsoft.Xrm.Sdk.Metadata;
using Spectre.Console;

namespace dgt.power.analyzer.tests
{
    [NotInParallel("Serial_Analyzer_Tests")]
    public class TopLayerAnalyzeTest : AnalyzeTestsBase<TopLayerAnalyze>
    {
        private const string SolutionUniqueName = "customizations";

        protected override CommandTestContext<TopLayerAnalyze, AnalyzeVerb> GetContext()
        {
            return GetBuilder()
                .WithFakeMessageExecutor(new RetrieveAllEntitiesExecutor())
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

        private IEnumerable<Entity> PrepareData(FakeOrganizationServiceAsync service)
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
                    service.State.EntityMetadata[TestEntity.EntityLogicalName].MetadataId,
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
            var formLayerForeign = new MsdynComponentlayer(Guid.NewGuid())
            {
                MsdynName = "Test Formular",
                MsdynSolutioncomponentname = "SystemForm",
                MsdynSolutionname = "somebodyelsesolution",
                MsdynOrder = 2,
                MsdynComponentid = $"{formComponent.ObjectId:B}"
            };
            var formLayerActive = new MsdynComponentlayer(Guid.NewGuid())
            {
                MsdynName = "Test Formular",
                MsdynSolutioncomponentname = "SystemForm",
                MsdynSolutionname = "Active",
                MsdynOrder = 3,
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
            var entityLayerPatch = new MsdynComponentlayer(Guid.NewGuid())
            {
                MsdynName = "Test Entity",
                MsdynSolutioncomponentname = "Entity",
                MsdynSolutionname = SolutionUniqueName+"_Patch", // Should be ignored
                MsdynOrder = 2,
                MsdynComponentid = $"{entityComponent.ObjectId:B}"
            };
            var entityLayerActive = new MsdynComponentlayer(Guid.NewGuid())
            {
                MsdynName = "Test Entity",
                MsdynSolutioncomponentname = "Entity",
                MsdynSolutionname = "Active",
                MsdynOrder = 3,
                MsdynComponentid = $"{entityComponent.ObjectId:B}"
            };
            return new Entity[]
            {
            solution,
            entityComponent,
            entityLayer,
            entityLayerPatch,
            entityLayerActive,
            formComponent,
            formLayer,
            formLayerForeign,
            formLayerActive,
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

        [Test]
        public async Task ShouldFailOnMissingInlineData() =>
            await Assert.That(GetContext()
                .Execute(new AnalyzeVerb
                {
                    InlineData = string.Empty
                })).IsFalse();

        [Test]
        public async Task ShouldAnalyzeTopLayer()
        {
            AnsiConsole.Record();
            await Assert.That(GetContext()
                .Execute(new AnalyzeVerb
                {
                    InlineData = SolutionUniqueName,
                    GenerateSummaryFile = true,
                    GenerateReportFile = true
                })).IsTrue();

            var output = AnsiConsole.ExportText();
            await Assert.That(output).StartsWith("── solution unique name: customizations ──");
            await Assert.That(File.Exists(Path.Combine(BaseAnalyze.ResultFolder, "TopLayer-summary.json"))).IsTrue();
            await Assert.That(File.Exists(Path.Combine(BaseAnalyze.ResultFolder, "TopLayer-result.csv"))).IsTrue();

            // Check Summary
            var summary = JsonSerializer.Deserialize<AnalyzerSummary>(File.ReadAllBytes(Path.Combine(BaseAnalyze.ResultFolder, "TopLayer-summary.json")));
            await Assert.That(summary.Anomalies).IsEqualTo(1);

            // Check Result
            using var reader = new StreamReader(Path.Combine(BaseAnalyze.ResultFolder, "TopLayer-result.csv"));
            using var csv = new CsvReader(reader, CultureInfo.InvariantCulture);
            var results = csv.GetRecords<ActiveLayerLine>().ToArray();
            await Assert.That(results.Length).IsEqualTo(1);
            await Assert.That(results[0].Component).IsEqualTo("SystemForm");
            await Assert.That(results[0].Name).IsEqualTo("Test Formular (testentity)");
            await Assert.That(results[0].Solution).IsEqualTo(SolutionUniqueName);
            await Assert.That(results[0].Order).IsEqualTo(2);
        }
    }
}
