// Copyright (c) DIGITALL Nature. All rights reserved
// DIGITALL Nature licenses this file to you under the Microsoft Public License.

using dgt.power.dataverse;
using dgt.power.plugin.Exceptions;
using dgt.power.plugin.Repositories;
using dgt.power.tests.FakeExecutor;
using Digitall.Dataverse.Testing;
using Microsoft.Xrm.Sdk;
using Microsoft.Xrm.Sdk.Messages;
using Microsoft.Xrm.Sdk.Query;

namespace dgt.power.plugin.tests.Repositories;

public class SolutionComponentRepositoryTests
{
    private static FakeOrganizationServiceAsync CreateService()
    {
        var service = new FakeOrganizationServiceAsync();
        service.AddRequests(new AddSolutionComponentExecutor());
        service.AddDefaultRequests();
        return service;
    }

    [Test]
    public async Task GetComponentTypeAsync_NoDefinition_ReturnsNull()
    {
        var service = CreateService();
        var repository = new SolutionComponentRepository(service);

        var result = await repository.GetComponentTypeAsync(PluginPackage.EntityLogicalName);

        await Assert.That(result).IsNull();
    }

    [Test]
    public async Task GetComponentTypeAsync_MatchingDefinition_ReturnsComponentType()
    {
        var service = CreateService();
        service.Create(new SolutionComponentDefinition { Id = Guid.NewGuid(), SolutionComponentType = 10119, PrimaryEntityName = PluginPackage.EntityLogicalName });
        var repository = new SolutionComponentRepository(service);

        var result = await repository.GetComponentTypeAsync(PluginPackage.EntityLogicalName);

        await Assert.That(result).IsEqualTo(10119);
    }

    [Test]
    public async Task AddToSolutionAsync_DoesNotThrow()
    {
        var service = CreateService();
        var repository = new SolutionComponentRepository(service);

        await repository.AddToSolutionAsync(91, Guid.NewGuid(), "TestSolution");
    }

    [Test]
    public async Task ListComponentIdsAsync_ReturnsComponentsInSpecifiedSolutionAndType()
    {
        var service = CreateService();
        var solutionId = Guid.NewGuid();
        service.Create(new Solution(solutionId) { UniqueName = "TestSolution" });
        var expectedComponentId = Guid.NewGuid();
        service.Create(CreateSolutionComponent(solutionId, expectedComponentId, IPluginAssemblyRepository.ComponentType));
        service.Create(CreateSolutionComponent(solutionId, Guid.NewGuid(), ISdkMessageProcessingStepRepository.ComponentType));
        var repository = new SolutionComponentRepository(service);

        var componentIds = await repository.ListComponentIdsAsync("TestSolution", IPluginAssemblyRepository.ComponentType);

        await Assert.That(componentIds).IsEquivalentTo([expectedComponentId]);
    }

    [Test]
    public async Task ListComponentIdsAsync_MissingSolution_Throws()
    {
        var repository = new SolutionComponentRepository(CreateService());

        await Assert.That(async () => await repository.ListComponentIdsAsync("MissingSolution", IPluginAssemblyRepository.ComponentType)).ThrowsExactly<MissingSolutionException>();
    }

    [Test]
    public async Task ListComponentIdsAsync_MultiplePages_CollectsEveryMembership()
    {
        var service = new FakeOrganizationServiceAsync();
        var solutionId = Guid.NewGuid();
        var firstId = Guid.NewGuid();
        var secondId = Guid.NewGuid();
        var pages = new List<(int Page, string? Cookie)>();
        service.AddRequests(new ProviderRequestFake(typeof(RetrieveMultipleRequest), request =>
        {
            var query = (QueryExpression)((RetrieveMultipleRequest)request).Query;
            if (query.EntityName == Solution.EntityLogicalName)
            {
                return new RetrieveMultipleResponse
                {
                    Results = { ["EntityCollection"] = new EntityCollection([new Solution(solutionId)]) }
                };
            }

            pages.Add((query.PageInfo.PageNumber, query.PageInfo.PagingCookie));
            var componentId = query.PageInfo.PageNumber == 1 ? firstId : secondId;
            return new RetrieveMultipleResponse
            {
                Results =
                {
                    ["EntityCollection"] = new EntityCollection(
                        [CreateSolutionComponent(solutionId, componentId, IPluginAssemblyRepository.ComponentType)])
                    {
                        MoreRecords = query.PageInfo.PageNumber == 1,
                        PagingCookie = "next-page"
                    }
                }
            };
        }));

        var result = await new SolutionComponentRepository(service).ListComponentIdsAsync("target", IPluginAssemblyRepository.ComponentType);

        await Assert.That(result).IsEquivalentTo([firstId, secondId]);
        await Assert.That(pages).IsEquivalentTo([(1, (string?)null), (2, "next-page")]);
    }

    private static SolutionComponent CreateSolutionComponent(Guid solutionId, Guid componentId, int componentType)
    {
        return new SolutionComponent(Guid.NewGuid())
        {
            Attributes =
            {
                [SolutionComponent.LogicalNames.ObjectId] = componentId,
                [SolutionComponent.LogicalNames.SolutionId] = new EntityReference(Solution.EntityLogicalName, solutionId),
                [SolutionComponent.LogicalNames.ComponentType] = new OptionSetValue(componentType)
            }
        };
    }
}
