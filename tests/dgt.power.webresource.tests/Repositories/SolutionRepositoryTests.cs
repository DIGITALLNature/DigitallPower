// Copyright (c) DIGITALL Nature. All rights reserved
// DIGITALL Nature licenses this file to you under the Microsoft Public License.

using dgt.power.dataverse;
using dgt.power.webresource.Repositories;
using Digitall.Dataverse.Testing;
using Microsoft.Xrm.Sdk;

namespace dgt.power.webresource.tests.Repositories;

public class SolutionRepositoryTests
{
    [Test]
    public async Task ListWebResourcesAsync_ReturnsWebResourcesInRequestedSolution()
    {
        var service = CreateService();
        var solutionId = Guid.NewGuid();
        service.Create(new Solution(solutionId) { UniqueName = "TargetSolution" });
        var otherSolutionId = Guid.NewGuid();
        service.Create(new Solution(otherSolutionId) { UniqueName = "OtherSolution" });
        var expected = new WebResource(Guid.NewGuid())
        {
            Name = "contoso_/app.js",
            WebResourceType = new OptionSetValue(3),
            Attributes = { [WebResource.LogicalNames.IsManaged] = true }
        };
        var unrelated = new WebResource(Guid.NewGuid())
        {
            Name = "contoso_/other.js",
            WebResourceType = new OptionSetValue(3),
            Attributes = { [WebResource.LogicalNames.IsManaged] = false }
        };
        service.Create(expected);
        service.Create(unrelated);
        service.Create(CreateSolutionComponent(solutionId, expected.Id, SolutionComponent.Options.ComponentType.WebResource));
        service.Create(CreateSolutionComponent(otherSolutionId, unrelated.Id, SolutionComponent.Options.ComponentType.WebResource));
        service.Create(CreateSolutionComponent(solutionId, Guid.NewGuid(), SolutionComponent.Options.ComponentType.PluginAssembly));
        var repository = new SolutionRepository(service);

        var result = await repository.ListWebResourcesAsync("TargetSolution");

        await Assert.That(result).Count().IsEqualTo(1);
        await Assert.That(result[0].Id).IsEqualTo(expected.Id);
        await Assert.That(result[0].Name).IsEqualTo("contoso_/app.js");
        await Assert.That(result[0].Type).IsEqualTo(3);
        await Assert.That(result[0].IsManaged).IsTrue();
    }

    [Test]
    public async Task ListWebResourcesAsync_MissingSolution_Throws()
    {
        var repository = new SolutionRepository(CreateService());

        await Assert.That(async () => await repository.ListWebResourcesAsync("MissingSolution"))
            .ThrowsExactly<WebResourceSolutionNotFoundException>();
    }

    private static FakeOrganizationServiceAsync CreateService()
    {
        var service = new FakeOrganizationServiceAsync();
        service.AddDefaultRequests();
        return service;
    }

    private static SolutionComponent CreateSolutionComponent(Guid solutionId, Guid componentId, int componentType) =>
        new(Guid.NewGuid())
        {
            Attributes =
            {
                [SolutionComponent.LogicalNames.ObjectId] = componentId,
                [SolutionComponent.LogicalNames.SolutionId] = new EntityReference(Solution.EntityLogicalName, solutionId),
                [SolutionComponent.LogicalNames.ComponentType] = new OptionSetValue(componentType)
            }
        };
}
