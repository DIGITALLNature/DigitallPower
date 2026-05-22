// Copyright (c) DIGITALL Nature. All rights reserved
// DIGITALL Nature licenses this file to you under the Microsoft Public License.

using dgt.power.dto;
using dgt.power.export.Base;
using dgt.power.export.Logic;
using dgt.power.export.tests.Base;
using dgt.power.tests;
using Microsoft.Xrm.Sdk;
using Queue = dgt.power.dataverse.Queue;

namespace dgt.power.export.tests;

public class QueueExportTest : ExportTestBase<QueueExport>
{
    protected override CommandTestContext<QueueExport, ExportVerb> GetContext() =>
        GetBuilder()
            .WithData(
                new[]
                {
                    new Queue(Guid.Parse("469005c9-ca23-4d53-a1ae-f909c7863f6b"))
                    {
                        Name = "Queue 1",
                        Description = "Queue 1",
                        IncomingEmailFilteringMethod =
                            new OptionSetValue(Queue.Options.IncomingEmailFilteringMethod.AllEmailMessages),
                        IncomingEmailDeliveryMethod =
                            new OptionSetValue(Queue.Options.IncomingEmailDeliveryMethod.None),
                        OutgoingEmailDeliveryMethod =
                            new OptionSetValue(Queue.Options.OutgoingEmailDeliveryMethod.None),
                        QueueViewType = new OptionSetValue(Queue.Options.QueueViewType.Public)
                    },
                    new Queue(Guid.NewGuid())
                    {
                        Name = "Queue 2",
                        Description = "Queue 2",
                        IncomingEmailFilteringMethod =
                            new OptionSetValue(Queue.Options.IncomingEmailFilteringMethod.AllEmailMessages),
                        IncomingEmailDeliveryMethod =
                            new OptionSetValue(Queue.Options.IncomingEmailDeliveryMethod.None),
                        OutgoingEmailDeliveryMethod =
                            new OptionSetValue(Queue.Options.OutgoingEmailDeliveryMethod.None),
                        QueueViewType = new OptionSetValue(Queue.Options.QueueViewType.Public)
                    },
                    new Queue(Guid.NewGuid())
                    {
                        Name = "Queue 3",
                        Description = "Queue 3",
                        IncomingEmailFilteringMethod =
                            new OptionSetValue(Queue.Options.IncomingEmailFilteringMethod.AllEmailMessages),
                        IncomingEmailDeliveryMethod =
                            new OptionSetValue(Queue.Options.IncomingEmailDeliveryMethod.None),
                        OutgoingEmailDeliveryMethod =
                            new OptionSetValue(Queue.Options.OutgoingEmailDeliveryMethod.None),
                        QueueViewType = new OptionSetValue(Queue.Options.QueueViewType.Private)
                    },
                    new Queue(Guid.NewGuid())
                    {
                        Name = "<Queue System>",
                        Description = "Queue System",
                        IncomingEmailFilteringMethod =
                            new OptionSetValue(Queue.Options.IncomingEmailFilteringMethod.AllEmailMessages),
                        IncomingEmailDeliveryMethod =
                            new OptionSetValue(Queue.Options.IncomingEmailDeliveryMethod.None),
                        OutgoingEmailDeliveryMethod =
                            new OptionSetValue(Queue.Options.OutgoingEmailDeliveryMethod.None),
                        QueueViewType = new OptionSetValue(Queue.Options.QueueViewType.Private)
                    }
                })
            .Build();

    [Test]
    public async Task ShouldExportQueuesWithDefaultConfiguration()
    {
        await Assert.That(GetContext()
            .Execute(new ExportVerb {FileName = GetTestFileName(), FileDir = ArtifactDirectory,}
            )).IsTrue();
        var queues = GetConfigurationTestArtifact<Queues>(GetTestFileName());
        await Assert.That(queues.QueuesToTransport).HasCount().EqualTo(3);
    }


    [Test]
    public async Task ShouldUseDefaultOnEmptyFileName()
    {
        await Assert.That(GetContext().Execute(new ExportVerb {FileName = string.Empty, FileDir = ArtifactDirectory,}
        )).IsTrue();
        var queues = GetConfigurationTestArtifact<Queues>("queue.json");
        await Assert.That(queues.QueuesToTransport).HasCount().EqualTo(3);
    }
}
