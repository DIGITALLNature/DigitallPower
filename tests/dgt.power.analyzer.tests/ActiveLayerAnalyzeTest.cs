// Copyright (c) DIGITALL Nature. All rights reserved
// DIGITALL Nature licenses this file to you under the Microsoft Public License.

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
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
using Spectre.Console;

namespace dgt.power.analyzer.tests
{
    public class ActiveLayerAnalyzeTest : AnalyzeTestsBase<ActiveLayerAnalyze>
    {
        private const string SolutionUniqueName = "customizations";

        public ActiveLayerAnalyzeTest(ITestOutputHelper testOutputHelper) : base(testOutputHelper)
        {
        }

        protected override CommandTestContext<ActiveLayerAnalyze, AnalyzeVerb> GetContext()
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
        public void ShouldAnalyzeActiveLayer()
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
            Assert.EndsWith("── solution unique name: customizations ──", output);
            Assert.True(File.Exists(Path.Combine(BaseAnalyze.ResultFolder, "NoActiveLayer-summary.json")));
            Assert.True(File.Exists(Path.Combine(BaseAnalyze.ResultFolder, "NoActiveLayer-result.csv")));
        }
    }
}
