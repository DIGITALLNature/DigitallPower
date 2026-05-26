// Copyright (c) DIGITALL Nature. All rights reserved
// DIGITALL Nature licenses this file to you under the Microsoft Public License.

using dgt.power.dataverse;
using dgt.power.export.Base;
using dgt.power.export.Logic;
using dgt.power.export.tests.Base;
using dgt.power.tests;
using Microsoft.Crm.Sdk;

namespace dgt.power.export.tests;

public class OutlookTemplatesExportTest : ExportTestBase<OutlookTemplateExport>
{
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

    [Test]
    public async Task ShouldExportOutlookTemplatesWithDefaultConfiguration()
    {
        await Assert.That(GetContext()
            .Execute(new ExportVerb
                {
                    FileName = GetTestFileName(),
                    FileDir = ArtifactDirectory
                }
            )).IsTrue();
        var templates = GetConfigurationTestArtifact<dto.SavedQuery>(GetTestFileName());
        await Assert.That(templates.DisabledOutlookTemplates).Count().IsEqualTo(1);
        await Assert.That(templates.OutlookTemplates).Count().IsEqualTo(1);
    }

    [Test]
    public async Task ShouldUseDefaultOnEmptyFileName()
    {
        await Assert.That(GetContext().Execute(new ExportVerb
            {
                FileName = string.Empty,
                FileDir = ArtifactDirectory
            }
        )).IsTrue();
        var templates = GetConfigurationTestArtifact<dto.SavedQuery>("outlooktemplate.json");
        await Assert.That(templates.DisabledOutlookTemplates).Count().IsEqualTo(1);
        await Assert.That(templates.OutlookTemplates).Count().IsEqualTo(1);
    }
}
