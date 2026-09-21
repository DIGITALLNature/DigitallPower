// Copyright (c) DIGITALL Nature. All rights reserved
// DIGITALL Nature licenses this file to you under the Microsoft Public License.

using dgt.power.dataverse;
using dgt.power.plugin.Repositories;
using Digitall.Dataverse.Testing;
using Microsoft.Xrm.Sdk;

namespace dgt.power.plugin.tests.Repositories;

public class SdkMessageRepositoryTests
{
    private static FakeOrganizationServiceAsync CreateService()
    {
        var service = new FakeOrganizationServiceAsync();
        service.AddDefaultRequests();
        return service;
    }

    private static SdkMessageFilter Filter(Guid id, Guid messageId, string primaryObjectTypeCode, string? secondaryObjectTypeCode = null)
    {
        var filter = new SdkMessageFilter(id) { SdkMessageId = new EntityReference(SdkMessage.EntityLogicalName, messageId) };
        filter.Attributes[SdkMessageFilter.LogicalNames.PrimaryObjectTypeCode] = primaryObjectTypeCode;
        if (secondaryObjectTypeCode is not null)
        {
            filter.Attributes[SdkMessageFilter.LogicalNames.SecondaryObjectTypeCode] = secondaryObjectTypeCode;
        }

        return filter;
    }

    [Test]
    public async Task ResolveAsync_GlobalMessage_NoMatch_ReturnsNull()
    {
        var service = CreateService();
        var repository = new SdkMessageRepository(service);

        var result = await repository.ResolveAsync("WhoAmI", "none", "none");

        await Assert.That(result).IsNull();
    }

    [Test]
    [Arguments("none")]
    [Arguments("")]
    public async Task ResolveAsync_GlobalMessage_Match_ReturnsMessageIdWithoutFilter(string primaryEntityName)
    {
        var service = CreateService();
        var messageId = Guid.NewGuid();
        service.Create(new SdkMessage(messageId) { Name = "WhoAmI" });
        var repository = new SdkMessageRepository(service);

        var result = await repository.ResolveAsync("WhoAmI", primaryEntityName, "none");

        await Assert.That(result).IsNotNull();
        await Assert.That(result!.MessageId).IsEqualTo(messageId);
        await Assert.That(result.MessageFilterId).IsNull();
    }

    [Test]
    public async Task ResolveAsync_EntityScopedMessage_NoMatchingFilter_ReturnsNull()
    {
        var service = CreateService();
        var repository = new SdkMessageRepository(service);

        var result = await repository.ResolveAsync("Create", "account", "none");

        await Assert.That(result).IsNull();
    }

    [Test]
    public async Task ResolveAsync_EntityScopedMessage_Match_ReturnsMessageAndFilterId()
    {
        var service = CreateService();
        var messageId = Guid.NewGuid();
        var filterId = Guid.NewGuid();
        service.Create(new SdkMessage(messageId) { Name = "Create" });
        service.Create(Filter(filterId, messageId, "account"));
        var repository = new SdkMessageRepository(service);

        var result = await repository.ResolveAsync("Create", "account", "none");

        await Assert.That(result).IsNotNull();
        await Assert.That(result!.MessageId).IsEqualTo(messageId);
        await Assert.That(result.MessageFilterId).IsEqualTo(filterId);
    }

    [Test]
    public async Task ResolveAsync_WrongEntity_ReturnsNull()
    {
        var service = CreateService();
        var messageId = Guid.NewGuid();
        service.Create(new SdkMessage(messageId) { Name = "Create" });
        service.Create(Filter(Guid.NewGuid(), messageId, "contact"));
        var repository = new SdkMessageRepository(service);

        var result = await repository.ResolveAsync("Create", "account", "none");

        await Assert.That(result).IsNull();
    }

    [Test]
    public async Task ResolveAsync_SecondaryEntitySpecified_MatchesOnBothEntities()
    {
        var service = CreateService();
        var messageId = Guid.NewGuid();
        var filterId = Guid.NewGuid();
        service.Create(new SdkMessage(messageId) { Name = "Associate" });
        service.Create(Filter(filterId, messageId, "account", "contact"));
        var repository = new SdkMessageRepository(service);

        var result = await repository.ResolveAsync("Associate", "account", "contact");

        await Assert.That(result).IsNotNull();
        await Assert.That(result!.MessageFilterId).IsEqualTo(filterId);
    }
}
