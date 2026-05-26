// Copyright (c) DIGITALL Nature. All rights reserved
// DIGITALL Nature licenses this file to you under the Microsoft Public License.

using dgt.power.dataverse;
using dgt.power.dto;
using dgt.power.export.Base;
using dgt.power.export.Logic;
using dgt.power.export.tests.Base;
using dgt.power.tests;
using Microsoft.Xrm.Sdk;
using DocumentTemplate = dgt.power.dataverse.DocumentTemplate;

namespace dgt.power.export.tests;

public class DocumentTemplateExportTests : ExportTestBase<DocumentTemplateExport>
{
    protected override CommandTestContext<DocumentTemplateExport, ExportVerb> GetContext()
    {
        return GetBuilder()
            .WithData(new[]
            {
                new DocumentTemplate(Guid.NewGuid())
                {
                    Name = "AccountExcel",
                    DocumentType = new OptionSetValue(DocumentTemplate.Options.DocumentType.MicrosoftExcel),
                    Content = Convert.ToBase64String(File.ReadAllBytes(GetResourcePath("Accounts.xlsx"))),
                    Description = "Internal",
                    LanguageCode = 1033,
                    Status = false, //inverted logic, don't ask why
                    AssociatedEntityTypeCode = "account"
                },
                new DocumentTemplate(Guid.NewGuid())
                {
                    Name = "ContactExcel",
                    DocumentType = new OptionSetValue(DocumentTemplate.Options.DocumentType.MicrosoftExcel),
                    Content = Convert.ToBase64String(File.ReadAllBytes(GetResourcePath("Contacts.xlsx"))),
                    Description = "Internal",
                    LanguageCode = 1033,
                    Status = false, //inverted logic, don't ask why
                    AssociatedEntityTypeCode = Contact.EntityLogicalName
                },
                new DocumentTemplate(Guid.Parse("ecf388de-1033-4c7c-93f3-0803b16c09c7"))
                {
                    Name = "AccountWord",
                    DocumentType = new OptionSetValue(DocumentTemplate.Options.DocumentType.MicrosoftWord),
                    Content = Convert.ToBase64String(File.ReadAllBytes(GetResourcePath("Account.docx"))),
                    Description = "Internal",
                    LanguageCode = 1033,
                    Status = false, //inverted logic, don't ask why
                    AssociatedEntityTypeCode = "account"
                },
                new DocumentTemplate(Guid.Parse("d1004e38-1033-461c-aa0e-7043f80c49cb"))
                {
                    Name = "ContactWord",
                    DocumentType = new OptionSetValue(DocumentTemplate.Options.DocumentType.MicrosoftWord),
                    Content = Convert.ToBase64String(File.ReadAllBytes(GetResourcePath("Contact.docx"))),
                    Description = "Internal",
                    LanguageCode = 1033,
                    Status = false, //inverted logic, don't ask why
                    AssociatedEntityTypeCode = Contact.EntityLogicalName
                }
            })
            .Build();
    }


    [Test]
    public async Task ShouldFilterTemplatesWhenAddingInlineDataFilter()
    {
        const string fetchXml = "<filter><condition attribute=\"documenttype\" operator=\"eq\" value=\"1\" /></filter>";
        await Assert.That(GetContext().Execute(new ExportVerb
            {
                FileName = GetTestFileName(),
                FileDir = ArtifactDirectory,
                InlineData = fetchXml
            }
        )).IsTrue();

        var templates =
            GetConfigurationTestArtifact<DocumentTemplates>(GetTestFileName());
        await Assert.That(templates.Templates).Count().EqualTo(2);

        foreach (var template in templates.Templates)
        {
            await Assert.That(File.Exists(GetArtifactPath(template.File))).IsTrue();
        }
    }

    [Test]
    public async Task ShouldFailOnInvalidFetchXml()
    {
        const string fetchXml = "<invalid-fetch/>";
        await Assert.That(GetContext().Execute(new ExportVerb
            {
                FileName = GetTestFileName(),
                FileDir = ArtifactDirectory,
                InlineData = fetchXml
            }
        )).IsFalse();
    }

    [Test]
    public async Task ShouldExportTemplatesWithAdvancedInlineData()
    {
        const string inlineData =
            "force=false,missing=false<filter><condition attribute=\"documenttype\" operator=\"eq\" value=\"1\" /></filter>";

        await Assert.That(GetContext()
            .Execute(new ExportVerb
                {
                    FileName = GetTestFileName(),
                    FileDir = ArtifactDirectory,
                    InlineData = inlineData
                }
            )).IsTrue();

        var templates =
            GetConfigurationTestArtifact<DocumentTemplates>(GetTestFileName());

        await Assert.That(templates.Templates).Count().EqualTo(2);

        foreach (var template in templates.Templates)
        {
            await Assert.That(File.Exists(GetArtifactPath(template.File))).IsTrue();
        }
    }

    [Test]
    public async Task ShouldSetForceUpdateWhithDefaultConfiguration()
    {
        await Assert.That(GetContext()
            .Execute(new ExportVerb
                {
                    FileName = GetTestFileName(),
                    FileDir = ArtifactDirectory
                }
            )).IsTrue();
        var templates =
            GetConfigurationTestArtifact<DocumentTemplates>(GetTestFileName());
        await Assert.That(templates.Templates).Count().EqualTo(4);

        foreach (var template in templates.Templates) {
            await Assert.That(template.ForceUpdate).IsTrue();
        }
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
        var templates = GetConfigurationTestArtifact<DocumentTemplates>("documenttemplate.json");
        await Assert.That(templates.Templates).Count().EqualTo(4);

        foreach (var template in templates.Templates)
        {
            await Assert.That(template.ForceUpdate).IsTrue();
        }
    }
}
