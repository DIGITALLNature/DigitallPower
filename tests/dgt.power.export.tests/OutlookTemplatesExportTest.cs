using dgt.power.dataverse;
using dgt.power.export.Base;
using dgt.power.export.Logic;
using dgt.power.export.tests.Base;
using dgt.power.tests;
using FluentAssertions;
using Microsoft.Crm.Sdk;
using Xunit.Abstractions;

namespace dgt.power.export.tests;

public class OutlookTemplatesExportTest : ExportTestBase<OutlookTemplateExport>
{
    public OutlookTemplatesExportTest(ITestOutputHelper testOutputHelper) : base(testOutputHelper)
    {
    }

    protected override CommandTestContext<OutlookTemplateExport, ExportVerb> GetContext() =>
        GetBuilder()
            .WithData(new[]
                {
                    new SavedQuery(Guid.NewGuid())
                    {
                        Name = "Disabled Outlook Template",
                        QueryType = SavedQueryQueryType.OutlookTemplate,
                        FetchXml =
                            "<fetch version=\"1.0\" output-format=\"xml-platform\" mapping=\"logical\" ><entity name=\"testentity\" ><attribute name=\"name\" /><order attribute=\"name\" descending=\"false\" /></entity></fetch>",
                        IsDefault = true
                    },
                    new SavedQuery(Guid.NewGuid())
                    {
                        Name = "Outdated Outlook Template",
                        QueryType = SavedQueryQueryType.OutlookTemplate,
                        FetchXml =
                            "<fetch version=\"1.0\" output-format=\"xml-platform\" mapping=\"logical\" ><entity name=\"testentity\" ><attribute name=\"name\" /><order attribute=\"name\" descending=\"false\" /></entity></fetch>",
                        IsDefault = false
                    }
                }
            )
            .Build();

    [Fact]
    public void ShouldExportOutlookTemplatesWithDefaultConfiguration()
    {
        GetContext()
            .Execute(new ExportVerb
                {
                    FileName = GetTestFileName(),
                    FileDir = ArtifactDirectory,
                }
            ).Should().BeTrue();
        var templates = GetConfigurationTestArtifact<dto.SavedQuery>(GetTestFileName());
        templates.DisabledOutlookTemplates.Should().ContainSingle();
        templates.OutlookTemplates.Should().ContainSingle();
    }

    [Fact]
    public void ShouldUseDefaultOnEmptyFileName()
    {
        GetContext().Execute(new ExportVerb
            {
                FileName = string.Empty,
                FileDir = ArtifactDirectory,
            }
        ).Should().BeTrue();
        var templates = GetConfigurationTestArtifact<dto.SavedQuery>("outlooktemplate.json");
        templates.DisabledOutlookTemplates.Should().ContainSingle();
        templates.OutlookTemplates.Should().ContainSingle();
    }
}
