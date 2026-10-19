// Copyright (c) DIGITALL Nature. All rights reserved
// DIGITALL Nature licenses this file to you under the Microsoft Public License.

using dgt.power.dataverse;
using dgt.power.maintenance.Logic;
using dgt.power.maintenance.Model.Settings;
using dgt.power.tests;
using dgt.power.tests.FakeExecutor;
using Digitall.Dataverse.Testing;
using Digitall.Dataverse.Testing.OrganizationRequests;
using Microsoft.Crm.Sdk.Messages;
using Microsoft.Xrm.Sdk;
using Microsoft.Xrm.Sdk.Messages;
using Microsoft.Xrm.Sdk.Query;

namespace dgt.power.maintenance.tests;

public class RemoveRedundantComponentsTests : CommandTestsBase<RemoveRedundantComponents, RemoveRedundantComponentsVerb>
{
    [Test]
    public async Task RunAsync_DryRun_UsesNoLockQueriesWithoutRemovingComponents()
    {
        var (context, queryExecutor, removeExecutor) = CreateContext();

        var succeeded = context.Execute(new RemoveRedundantComponentsVerb
        {
            SourceSolutions = "source",
            TargetSolution = "target",
            DryRun = true
        });

        await Assert.That(succeeded).IsTrue();
        await Assert.That(queryExecutor.Queries.Count).IsEqualTo(4);
        await Assert.That(queryExecutor.Queries.All(static query => query.NoLock)).IsTrue();
        await Assert.That(removeExecutor.RequestCount).IsEqualTo(0);
    }

    [Test]
    public async Task RunAsync_Apply_UsesAsyncRemoveRequest()
    {
        var (context, _, removeExecutor) = CreateContext();

        var succeeded = context.Execute(new RemoveRedundantComponentsVerb
        {
            SourceSolutions = "source",
            TargetSolution = "target"
        });

        await Assert.That(succeeded).IsTrue();
        await Assert.That(removeExecutor.RequestCount).IsEqualTo(1);
    }

    private (
        CommandTestContext<RemoveRedundantComponents, RemoveRedundantComponentsVerb> Context,
        SolutionComponentQueryExecutor QueryExecutor,
        RemoveComponentExecutor RemoveExecutor) CreateContext()
    {
        var sourceSolution = new Solution(Guid.NewGuid()) { UniqueName = "source" };
        var targetSolution = new Solution(Guid.NewGuid()) { UniqueName = "target" };
        var queryExecutor = new SolutionComponentQueryExecutor(sourceSolution, targetSolution, Guid.NewGuid());
        var removeExecutor = new RemoveComponentExecutor();
        var context = GetBuilder()
            .WithFakeMessageExecutor(new RetrieveAllEntitiesExecutor())
            .WithFakeMessageExecutor(queryExecutor)
            .WithFakeMessageExecutor(removeExecutor)
            .Build();
        return (context, queryExecutor, removeExecutor);
    }

    private sealed class SolutionComponentQueryExecutor(
        Solution sourceSolution,
        Solution targetSolution,
        Guid componentId) : IOrganizationRequestFake
    {
        public Type ForType => typeof(RetrieveMultipleRequest);

        public List<QueryExpression> Queries { get; } = [];

        public OrganizationResponse Execute(
            OrganizationRequest organizationRequest,
            FakeOrganizationService fakeOrganizationService)
        {
            var query = (QueryExpression)((RetrieveMultipleRequest)organizationRequest).Query;
            Queries.Add(query);
            if (query.EntityName == Solution.EntityLogicalName)
            {
                var name = query.Criteria.Conditions.Single().Values[0] as string;
                var solution = name == sourceSolution.UniqueName ? sourceSolution : targetSolution;
                return new RetrieveMultipleResponse
                {
                    [nameof(RetrieveMultipleResponse.EntityCollection)] = new EntityCollection([solution])
                };
            }

            var solutionId = (Guid)query.Criteria.Conditions.First(
                condition => condition.AttributeName == SolutionComponent.LogicalNames.SolutionId).Values[0];
            var solutionComponent = new Entity(SolutionComponent.EntityLogicalName, Guid.NewGuid())
            {
                [SolutionComponent.LogicalNames.ComponentType] =
                    new OptionSetValue(SolutionComponent.Options.ComponentType.PluginAssembly),
                [SolutionComponent.LogicalNames.ObjectId] = componentId,
                [SolutionComponent.LogicalNames.SolutionId] = solutionId
            };
            return new RetrieveMultipleResponse
            {
                [nameof(RetrieveMultipleResponse.EntityCollection)] = new EntityCollection([solutionComponent])
            };
        }
    }

    private sealed class RemoveComponentExecutor : IOrganizationRequestFake
    {
        public Type ForType => typeof(RemoveSolutionComponentRequest);

        public int RequestCount { get; private set; }

        public OrganizationResponse Execute(
            OrganizationRequest organizationRequest,
            FakeOrganizationService fakeOrganizationService)
        {
            RequestCount++;
            return new RemoveSolutionComponentResponse();
        }
    }
}
