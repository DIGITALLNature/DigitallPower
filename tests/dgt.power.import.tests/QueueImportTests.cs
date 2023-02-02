using System.ServiceModel;
using dgt.power.dataverse;
using dgt.power.dto;
using dgt.power.import.Base;
using dgt.power.import.Logic;
using dgt.power.import.tests.Base;
using FluentAssertions;
using Microsoft.Crm.Sdk.Messages;
using Microsoft.Xrm.Sdk;
using Xunit.Abstractions;
using Queue = dgt.power.dto.Queue;
#pragma warning disable CS8602
#pragma warning disable CS8601

namespace dgt.power.import.tests;

public class QueueImportTests : ImportTestBase<QueueImport>
{
    public QueueImportTests(ITestOutputHelper testOutputHelper) : base(testOutputHelper)
    {
    }

    [Fact]
    public void ShouldFailOnWrongConfiguration() =>
        GetContext().Execute(new ImportVerb
            {
                FileName = string.Empty,
                FileDir = ArtifactDirectory
            }
        ).Should().BeFalse();

    [Fact]
    public void ShouldFailOnEmptyConfiguration() =>
        GetContext().Execute(new ImportVerb
            {
                FileName = WriteConfigurationArtifact(new Queues()).Name,
                FileDir = ArtifactDirectory
            }
        ).Should().BeFalse();

    [Fact]
    public void ShouldFailOnCreateWithAlternativeOwner()
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

        context.Execute(new ImportVerb
            {
                FileName = WriteConfigurationArtifact(configArtifact).Name,
                FileDir = ArtifactDirectory
            }
        ).Should().BeFalse();

        var createdQueue = context.GetById<dataverse.Queue>(queueToBeCreated.QueueId);
        createdQueue.OwnerId.Id.Should().NotBe(owner.Id);
    }

    [Fact]
    public void ShouldSkipUpdateOfExistingQueue()
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

        context.Execute(new ImportVerb
            {
                FileName = WriteConfigurationArtifact(configArtifact).Name,
                FileDir = ArtifactDirectory
            }
        ).Should().BeTrue();

        var updatedQueue = context.GetById<dataverse.Queue>(queueToBeUpdated.QueueId);
        updatedQueue.Name.Should().Be(queueToBeUpdated.Name);
        updatedQueue.Description.Should().StartWith(queueToBeUpdated.Description);
        updatedQueue.QueueViewType.Value.Should().Be((int) queueToBeUpdated.ViewType);
        updatedQueue.IncomingEmailDeliveryMethod.Value.Should().Be((int) queueToBeUpdated.IncomingEmailDelivery);
        updatedQueue.IncomingEmailFilteringMethod.Value.Should().Be((int) queueToBeUpdated.IncomingEmailFiltering);
        updatedQueue.OutgoingEmailDeliveryMethod.Value.Should().Be((int) queueToBeUpdated.OutgoingEmailDelivery);
        updatedQueue.OwnerId.Id.Should().Be(owner.Id);
    }

    [Fact]
    public void ShouldUpdateExistingQueue()
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

        context.Execute(new ImportVerb
            {
                FileName = WriteConfigurationArtifact(configArtifact).Name,
                FileDir = ArtifactDirectory
            }
        ).Should().BeTrue();

        var updatedQueue = context.GetById<dataverse.Queue>(queueToBeUpdated.QueueId);
        updatedQueue.Name.Should().Be(queueToBeUpdated.Name);
        updatedQueue.Description.Should().StartWith(queueToBeUpdated.Description);
        updatedQueue.QueueViewType.Value.Should().Be((int) queueToBeUpdated.ViewType);
        updatedQueue.IncomingEmailDeliveryMethod.Value.Should().Be((int) queueToBeUpdated.IncomingEmailDelivery);
        updatedQueue.IncomingEmailFilteringMethod.Value.Should().Be((int) queueToBeUpdated.IncomingEmailFiltering);
        updatedQueue.OutgoingEmailDeliveryMethod.Value.Should().Be((int) queueToBeUpdated.OutgoingEmailDelivery);
        updatedQueue.OwnerId.Id.Should().Be(owner.Id);
    }

    [Fact]
    public void ShouldFailOnUpdateWithAlternativeOwner()
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

        context.Execute(new ImportVerb
            {
                FileName = WriteConfigurationArtifact(configArtifact).Name,
                FileDir = ArtifactDirectory
            }
        ).Should().BeFalse();

        var updatedQueue = context.GetById<dataverse.Queue>(queueToBeUpdated.QueueId);
        updatedQueue.OwnerId.Id.Should().NotBe(owner.Id);
    }

    [Fact]
    public void ShouldCreateQueue()
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

        context.Execute(new ImportVerb
            {
                FileName = WriteConfigurationArtifact(configArtifact).Name,
                FileDir = ArtifactDirectory
            }
        ).Should().BeTrue();

        var createdQueue = context.GetById<dataverse.Queue>(queueToBeCreated.QueueId);
        createdQueue.Name.Should().Be(queueToBeCreated.Name);
        createdQueue.Description.Should().StartWith(queueToBeCreated.Description);
        createdQueue.QueueViewType.Value.Should().Be((int) queueToBeCreated.ViewType);
        createdQueue.IncomingEmailDeliveryMethod.Value.Should().Be((int) queueToBeCreated.IncomingEmailDelivery);
        createdQueue.IncomingEmailFilteringMethod.Value.Should().Be((int) queueToBeCreated.IncomingEmailFiltering);
        createdQueue.OutgoingEmailDeliveryMethod.Value.Should().Be((int) queueToBeCreated.OutgoingEmailDelivery);
        createdQueue.OwnerId.Id.Should().Be(owner.Id);
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
