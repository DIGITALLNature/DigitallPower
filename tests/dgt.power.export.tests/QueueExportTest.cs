using dgt.power.dto;
using dgt.power.export.Base;
using dgt.power.export.Logic;
using dgt.power.export.tests.Base;
using dgt.power.tests;
using FluentAssertions;
using Microsoft.Xrm.Sdk;
using Xunit.Abstractions;
using Queue = dgt.power.dataverse.Queue;

namespace dgt.power.export.tests;

public class QueueExportTest : ExportTestBase<QueueExport>
{
    public QueueExportTest(ITestOutputHelper testOutputHelper) : base(testOutputHelper)
    {
    }

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

    [Fact]
    public void ShouldExportQueuesWithDefaultConfiguration()
    {
        GetContext()
            .Execute(new ExportVerb {FileName = GetTestFileName(), FileDir = ArtifactDirectory,}
            ).Should().BeTrue();
        var queues = GetConfigurationTestArtifact<Queues>(GetTestFileName());
        queues.QueuesToTransport.Should().HaveCount(3);
    }


    [Fact]
    public void ShouldUseDefaultOnEmptyFileName()
    {
        GetContext().Execute(new ExportVerb {FileName = string.Empty, FileDir = ArtifactDirectory,}
        ).Should().BeTrue();
        var queues = GetConfigurationTestArtifact<Queues>("queue.json");
        queues.QueuesToTransport.Should().HaveCount(3);
    }
}
