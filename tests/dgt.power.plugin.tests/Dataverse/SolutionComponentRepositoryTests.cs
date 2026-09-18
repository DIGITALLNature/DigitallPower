// Copyright (c) DIGITALL Nature. All rights reserved
// DIGITALL Nature licenses this file to you under the Microsoft Public License.

using dgt.power.dataverse;
using dgt.power.plugin.Dataverse;
using dgt.power.tests.FakeExecutor;
using Digitall.Dataverse.Testing;
using Microsoft.Xrm.Sdk;

namespace dgt.power.plugin.tests.Dataverse;

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
        service.Create(new SolutionComponentDefinition
        {
            Id = Guid.NewGuid(),
            SolutionComponentType = 10119,
            PrimaryEntityName = PluginPackage.EntityLogicalName
        });
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
    public async Task GetPublisherPrefixAsync_NoSolution_ReturnsDefault()
    {
        var service = CreateService();
        var repository = new SolutionComponentRepository(service);

        var result = await repository.GetPublisherPrefixAsync(null);

        await Assert.That(result).IsEqualTo("new");
    }

    [Test]
    public async Task GetPublisherPrefixAsync_SolutionNotFound_ReturnsDefault()
    {
        var service = CreateService();
        var repository = new SolutionComponentRepository(service);

        var result = await repository.GetPublisherPrefixAsync("MySolution");

        await Assert.That(result).IsEqualTo("new");
    }

    [Test]
    public async Task GetPublisherPrefixAsync_SolutionFound_ReturnsPublisherPrefix()
    {
        var service = CreateService();
        var publisherId = Guid.NewGuid();
        service.Create(new Publisher(publisherId) { CustomizationPrefix = "dgt" });
        service.Create(new Solution(Guid.NewGuid())
        {
            UniqueName = "MySolution",
            PublisherId = new EntityReference(Publisher.EntityLogicalName, publisherId)
        });
        var repository = new SolutionComponentRepository(service);

        var result = await repository.GetPublisherPrefixAsync("MySolution");

        await Assert.That(result).IsEqualTo("dgt");
    }
}
