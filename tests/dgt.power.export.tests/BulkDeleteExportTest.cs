using dgt.power.dataverse;
using dgt.power.dto;
using dgt.power.export.Base;
using dgt.power.export.Logic;
using dgt.power.export.tests.Base;
using dgt.power.tests;
using FluentAssertions;
using Microsoft.Xrm.Sdk;
using Xunit.Abstractions;

namespace dgt.power.export.tests;

public class BulkDeleteExportTest : ExportTestBase<BulkDeleteExport>
{
    public BulkDeleteExportTest(ITestOutputHelper testOutputHelper) : base(testOutputHelper)
    {
    }

    protected override CommandTestContext<BulkDeleteExport, ExportVerb> GetContext() =>
        GetBuilder()
            .WithData(new Entity[]
            {
                new AsyncOperation(Guid.NewGuid())
                {
                    Name = "Analysis Results Cleanup Job",
                    OperationType = new OptionSetValue(AsyncOperation.Options.OperationType.BulkDelete),
                    RecurrenceStartTime = DateTime.UtcNow.AddDays(-1),
                    RecurrencePattern = "FREQ=WEEKLY",
                    Data =
                        "<string>&lt;fetch version=\"1.0\" output-format=\"xml-platform\" mapping=\"logical\" &gt;&lt;entity name=\"testentity\" &gt;&lt;attribute name=\"name\" /&gt;&lt;/entity&gt;&lt;/fetch&gt;</string>"
                },
                new AsyncOperation(Guid.NewGuid())
                {
                    Name = "Old Bulk Delete",
                    OperationType = new OptionSetValue(AsyncOperation.Options.OperationType.BulkDelete),
                    RecurrenceStartTime = DateTime.UtcNow.AddDays(-1),
                    RecurrencePattern = "FREQ=WEEKLY",
                    Data =
                        "<string>&lt;fetch version=\"1.0\" output-format=\"xml-platform\" mapping=\"logical\" &gt;&lt;entity name=\"testentity\" &gt;&lt;attribute name=\"name\" /&gt;&lt;/entity&gt;&lt;/fetch&gt;</string>"
                }
            }).Build();

    [Fact]
    public void ShouldGetPlainBulkDeleteExport()
    {
        var context = GetContext();
        context.Execute(new ExportVerb {FileName = GetTestFileName(), FileDir = ArtifactDirectory,}
        ).Should().BeTrue();
        var bulkDeletes = GetConfigurationTestArtifact<BulkDeletes>(GetTestFileName());
        bulkDeletes.Deletes.Should().HaveCount(2);
    }

    [Fact]
    public void ShouldUseDefaultOnEmptyFileName()
    {
        GetContext().Execute(new ExportVerb {FileName = string.Empty, FileDir = ArtifactDirectory,}
        ).Should().BeTrue();

        var bulkDeletes = GetConfigurationTestArtifact<BulkDeletes>("bulkdelete.json");
        bulkDeletes.Deletes.Should().HaveCount(2);
    }
}
