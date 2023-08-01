// Copyright (c) DIGITALL Nature. All rights reserved
// DIGITALL Nature licenses this file to you under the Microsoft Public License.

using dgt.power.analyzer.Logic;
using dgt.power.analyzer.tests.Base;
using dgt.power.analyzer.Base;
using dgt.power.dataverse;
using dgt.power.tests;
using Microsoft.Xrm.Sdk;
using FakeXrmEasy.Abstractions;
using FakeXrmEasy.Extensions;

namespace dgt.power.analyzer.tests;

public class RedundantComponentsAnalyzeTest : AnalyzeTestsBase<RedundantComponentsAnalyze>
{
    private const string SolutionUniqueName = "customizations";
    private const string SolutionPatchUniqueName = "customizations_Patch_ABC123";
    private const string ParallelSolutionUniqueName = "customizations_parallel";

    public RedundantComponentsAnalyzeTest(ITestOutputHelper testOutputHelper) : base(testOutputHelper)
    {
    }

    protected override CommandTestContext<RedundantComponentsAnalyze, AnalyzeVerb> GetContext()
    {
        return GetBuilder()
            .WithData(PrepareData)
            .Build();
    }

    private IEnumerable<Entity> PrepareData(IXrmFakedContext context)
    {
        var solution = new Solution(Guid.NewGuid())
        {
            UniqueName = SolutionUniqueName
        };

        var solutionPatch = new Solution(Guid.NewGuid())
        {
            UniqueName = SolutionPatchUniqueName
        }.AddAttribute(Solution.LogicalNames.ParentSolutionId, solution.ToEntityReference());

        var parallelSolution = new Solution(Guid.NewGuid())
        {
            UniqueName = ParallelSolutionUniqueName
        };

        //var entityComponent = new SolutionComponent(Guid.NewGuid())
        //{
        //    [SolutionComponent.LogicalNames.ComponentType] =
        //        new OptionSetValue(SolutionComponent.Options.ComponentType.Entity),
        //    [SolutionComponent.LogicalNames.RootComponentBehavior] =
        //        new OptionSetValue(SolutionComponent.Options.RootComponentBehavior.IncludeSubcomponents),
        //    [SolutionComponent.LogicalNames.ObjectId] =
        //        context.GetEntityMetadataByName(TestEntity.EntityLogicalName).MetadataId,
        //    [SolutionComponent.LogicalNames.IsMetadata] = true,
        //    [SolutionComponent.LogicalNames.SolutionId] = solution.ToEntityReference(),
        //    FormattedValues =
        //{
        //    {SolutionComponent.LogicalNames.ComponentType, "Entity"}
        //}
        //};
        //var formComponent = new SolutionComponent(Guid.NewGuid())
        //{
        //    [SolutionComponent.LogicalNames.ComponentType] =
        //        new OptionSetValue(SolutionComponent.Options.ComponentType.SystemForm),
        //    [SolutionComponent.LogicalNames.RootSolutionComponentId] = entityComponent.Id,
        //    [SolutionComponent.LogicalNames.ObjectId] = Guid.NewGuid(),
        //    [SolutionComponent.LogicalNames.IsMetadata] = true,
        //    [SolutionComponent.LogicalNames.SolutionId] = solution.ToEntityReference(),
        //    FormattedValues =
        //{
        //    {SolutionComponent.LogicalNames.ComponentType, "SystemForm"}
        //}
        //};
        //var formLayer = new MsdynComponentlayer(Guid.NewGuid())
        //{
        //    MsdynName = "Test Formular",
        //    MsdynSolutioncomponentname = "SystemForm",
        //    MsdynSolutionname = SolutionUniqueName,
        //    MsdynOrder = 1,
        //    MsdynComponentid = $"{formComponent.ObjectId:B}"
        //};
        //var entityLayer = new MsdynComponentlayer(Guid.NewGuid())
        //{
        //    MsdynName = "Test Entity",
        //    MsdynSolutioncomponentname = "Entity",
        //    MsdynSolutionname = SolutionUniqueName,
        //    MsdynOrder = 1,
        //    MsdynComponentid = $"{entityComponent.ObjectId:B}"
        //};
        //var entityLayerActive = new MsdynComponentlayer(Guid.NewGuid())
        //{
        //    MsdynName = "Test Entity",
        //    MsdynSolutioncomponentname = "Entity",
        //    MsdynSolutionname = "Active",
        //    MsdynOrder = 2,
        //    MsdynComponentid = $"{entityComponent.ObjectId:B}"
        //};
        return new Entity[]
        {
            solution,
            solutionPatch,
            parallelSolution
            //entityComponent,
            //entityLayer,
            //entityLayerActive,
            //formComponent,
            //formLayer,
            //new SolutionComponent(Guid.NewGuid())
            //{
            //    [SolutionComponent.LogicalNames.ComponentType] =
            //        new OptionSetValue(SolutionComponent.Options.ComponentType.EmailTemplate),
            //    [SolutionComponent.LogicalNames.ObjectId] = Guid.NewGuid(),
            //    [SolutionComponent.LogicalNames.IsMetadata] = true,
            //    [SolutionComponent.LogicalNames.SolutionId] = solution.ToEntityReference(),
            //    FormattedValues =
            //    {
            //        {SolutionComponent.LogicalNames.ComponentType, "Email Template"}
            //    }
            //}
        };
    }


    [Fact]
    public void ShouldFailOnMissingInlineData() =>
        GetContext()
            .Execute(new AnalyzeVerb
            {
                InlineData = string.Empty
            }).Should().BeFalse();
}