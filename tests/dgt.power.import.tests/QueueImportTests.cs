// Copyright (c) DIGITALL Nature. All rights reserved
// DIGITALL Nature licenses this file to you under the Microsoft Public License.

using System.ServiceModel;
using dgt.power.dataverse;
using dgt.power.dto;
using dgt.power.import.Base;
using dgt.power.import.Logic;
using dgt.power.import.tests.Base;
using Microsoft.Crm.Sdk.Messages;
using Microsoft.Xrm.Sdk;
using Queue = dgt.power.dto.Queue;
#pragma warning disable CS8602
#pragma warning disable CS8601

namespace dgt.power.import.tests;

public class QueueImportTests : ImportTestBase<QueueImport>
{
    [Test]
    public async Task ShouldFailOnWrongConfiguration() =>
        await Assert.That(GetContext().Execute(new ImportVerb
            {
                FileName = string.Empty,
                FileDir = ArtifactDirectory
            }
        )).IsFalse();

    [Test]
    public async Task ShouldFailOnEmptyConfiguration() =>
        await Assert.That(GetContext().Execute(new ImportVerb
            {
                FileName = WriteConfigurationArtifact(new Queues()).Name,
                FileDir = ArtifactDirectory
            }
        )).IsFalse();

    [Test]
    public async Task ShouldFailOnCreateWithAlternativeOwner()
    {
        var (owner, _) = GetData();
        var context = GetBuilder()
            .WithData(owner)
            .WithExecutionMock<AssignRequest>(_ =>
                throw new FaultException<OrganizationServiceFault>(new OrganizationServiceFault()))
            .Build();

        var queueToBeCreated = GetNewConfig();
        var configArtifact = new Queues
        {
            Owner = owner.DomainName,
            QueuesToTransport = new[]
            {
                queueToBeCreated
            }
        };

        await Assert.That(context.Execute(new ImportVerb
            {
                FileName = WriteConfigurationArtifact(configArtifact).Name,
                FileDir = ArtifactDirectory
            }
        )).IsFalse();

        var createdQueue = context.GetById<dataverse.Queue>(queueToBeCreated.QueueId);
        await Assert.That(createdQueue.OwnerId.Id).IsNotEqualTo(owner.Id);
    }

    [Test]
    public async Task ShouldSkipUpdateOfExistingQueue()
    {
        var (owner, existingQueue) = GetData();

        var context = GetBuilder()
            .WithData(owner)
            .WithData(existingQueue)
            .Build();
        var queueToBeUpdated = GetExistingConfig(existingQueue);
        var configArtifact = new Queues
        {
            Owner = owner.DomainName,
            QueuesToTransport = new[]
            {
                queueToBeUpdated
            }
        };

        await Assert.That(context.Execute(new ImportVerb
            {
                FileName = WriteConfigurationArtifact(configArtifact).Name,
                FileDir = ArtifactDirectory
            }
        )).IsTrue();

        var updatedQueue = context.GetById<dataverse.Queue>(queueToBeUpdated.QueueId);
        await Assert.That(updatedQueue.Name).IsEqualTo(queueToBeUpdated.Name);
        await Assert.That(updatedQueue.Description).StartsWith(queueToBeUpdated.Description);
        await Assert.That(updatedQueue.QueueViewType.Value).IsEqualTo((int) queueToBeUpdated.ViewType);
        await Assert.That(updatedQueue.IncomingEmailDeliveryMethod.Value).IsEqualTo((int) queueToBeUpdated.IncomingEmailDelivery);
        await Assert.That(updatedQueue.IncomingEmailFilteringMethod.Value).IsEqualTo((int) queueToBeUpdated.IncomingEmailFiltering);
        await Assert.That(updatedQueue.OutgoingEmailDeliveryMethod.Value).IsEqualTo((int) queueToBeUpdated.OutgoingEmailDelivery);
        await Assert.That(updatedQueue.OwnerId.Id).IsEqualTo(owner.Id);
    }

    [Test]
    public async Task ShouldUpdateExistingQueue()
    {
        var (owner, existingQueue) = GetData();

        var context = GetBuilder()
            .WithData(owner)
            .WithData(existingQueue)
            .Build();

        var queueToBeUpdated = GetExistingConfig(existingQueue);
        queueToBeUpdated.Name += " Updated";
        queueToBeUpdated.ViewType = Queue.QueueViewType.Private;

        var configArtifact = new Queues
        {
            Owner = owner.DomainName,
            QueuesToTransport = new[]
            {
                queueToBeUpdated
            }
        };

        await Assert.That(context.Execute(new ImportVerb
            {
                FileName = WriteConfigurationArtifact(configArtifact).Name,
                FileDir = ArtifactDirectory
            }
        )).IsTrue();

        var updatedQueue = context.GetById<dataverse.Queue>(queueToBeUpdated.QueueId);
        await Assert.That(updatedQueue.Name).IsEqualTo(queueToBeUpdated.Name);
        await Assert.That(updatedQueue.Description).StartsWith(queueToBeUpdated.Description);
        await Assert.That(updatedQueue.QueueViewType.Value).IsEqualTo((int) queueToBeUpdated.ViewType);
        await Assert.That(updatedQueue.IncomingEmailDeliveryMethod.Value).IsEqualTo((int) queueToBeUpdated.IncomingEmailDelivery);
        await Assert.That(updatedQueue.IncomingEmailFilteringMethod.Value).IsEqualTo((int) queueToBeUpdated.IncomingEmailFiltering);
        await Assert.That(updatedQueue.OutgoingEmailDeliveryMethod.Value).IsEqualTo((int) queueToBeUpdated.OutgoingEmailDelivery);
        await Assert.That(updatedQueue.OwnerId.Id).IsEqualTo(owner.Id);
    }

    [Test]
    public async Task ShouldFailOnUpdateWithAlternativeOwner()
    {
        var (owner, existingQueue) = GetData();

        var context = GetBuilder()
            .WithData(owner)
            .WithData(existingQueue)
            .WithExecutionMock<AssignRequest>(_ =>
                throw new FaultException<OrganizationServiceFault>(new OrganizationServiceFault()))
            .Build();

        var queueToBeUpdated = GetExistingConfig(existingQueue);
        var configArtifact = new Queues
        {
            Owner = owner.DomainName,
            QueuesToTransport = new[]
            {
                queueToBeUpdated
            }
        };

        await Assert.That(context.Execute(new ImportVerb
            {
                FileName = WriteConfigurationArtifact(configArtifact).Name,
                FileDir = ArtifactDirectory
            }
        )).IsFalse();

        var updatedQueue = context.GetById<dataverse.Queue>(queueToBeUpdated.QueueId);
        await Assert.That(updatedQueue.OwnerId.Id).IsNotEqualTo(owner.Id);
    }

    [Test]
    public async Task ShouldCreateQueue()
    {
        var (owner, _) = GetData();
        var context = GetBuilder()
            .WithData(owner)
            .Build();
        var queueToBeCreated = GetNewConfig();
        var configArtifact = new Queues
        {
            Owner = owner.DomainName,
            QueuesToTransport = new[]
            {
                queueToBeCreated
            }
        };

        await Assert.That(context.Execute(new ImportVerb
            {
                FileName = WriteConfigurationArtifact(configArtifact).Name,
                FileDir = ArtifactDirectory
            }
        )).IsTrue();

        var createdQueue = context.GetById<dataverse.Queue>(queueToBeCreated.QueueId);
        await Assert.That(createdQueue.Name).IsEqualTo(queueToBeCreated.Name);
        await Assert.That(createdQueue.Description).StartsWith(queueToBeCreated.Description);
        await Assert.That(createdQueue.QueueViewType.Value).IsEqualTo((int) queueToBeCreated.ViewType);
        await Assert.That(createdQueue.IncomingEmailDeliveryMethod.Value).IsEqualTo((int) queueToBeCreated.IncomingEmailDelivery);
        await Assert.That(createdQueue.IncomingEmailFilteringMethod.Value).IsEqualTo((int) queueToBeCreated.IncomingEmailFiltering);
        await Assert.That(createdQueue.OutgoingEmailDeliveryMethod.Value).IsEqualTo((int) queueToBeCreated.OutgoingEmailDelivery);
        await Assert.That(createdQueue.OwnerId.Id).IsEqualTo(owner.Id);
    }

    private static Queue GetNewConfig() =>
        new()
        {
            Name = "New Queue",
            Description = "New",
            QueueId = Guid.NewGuid(),
            ViewType = Queue.QueueViewType.Public,
            IncomingEmailDelivery = Queue.IncomingEmailDeliveryMethod.ServerSideSynchronizationOrEmailRouter,
            IncomingEmailFiltering = Queue.IncomingEmailFilteringMethod.AllEmailMessages,
            OutgoingEmailDelivery = Queue.OutgoingEmailDeliveryMethod.ServerSideSynchronizationOrEmailRouter
        };

    private static Queue GetExistingConfig(dataverse.Queue existingQueue) =>
        new()
        {
            Name = existingQueue.Name,
            Description = existingQueue.Description,
            IncomingEmailDelivery = (Queue.IncomingEmailDeliveryMethod) existingQueue.IncomingEmailDeliveryMethod.Value,
            ViewType = (Queue.QueueViewType) existingQueue.QueueViewType.Value,
            IncomingEmailFiltering =
                (Queue.IncomingEmailFilteringMethod) existingQueue.IncomingEmailFilteringMethod.Value,
            OutgoingEmailDelivery = (Queue.OutgoingEmailDeliveryMethod) existingQueue.OutgoingEmailDeliveryMethod.Value,
            QueueId = existingQueue.Id
        };

    private static (SystemUser owner, dataverse.Queue existingQueue) GetData()
    {
        var owner = new SystemUser(Guid.NewGuid())
        {
            DomainName = "owner@test.de"
        };
        var queue = new dataverse.Queue(Guid.NewGuid())
        {
            Name = "Existing",
            QueueViewType = new OptionSetValue(dataverse.Queue.Options.QueueViewType.Public),
            Description = "Existing",
            OutgoingEmailDeliveryMethod = new OptionSetValue(dataverse.Queue.Options.OutgoingEmailDeliveryMethod
                .ServerSideSynchronizationOrEmailRouter),
            IncomingEmailDeliveryMethod = new OptionSetValue(dataverse.Queue.Options.IncomingEmailDeliveryMethod
                .ServerSideSynchronizationOrEmailRouter),
            IncomingEmailFilteringMethod =
                new OptionSetValue(dataverse.Queue.Options.IncomingEmailFilteringMethod.AllEmailMessages),
        };
        return (owner, queue);
    }
}
