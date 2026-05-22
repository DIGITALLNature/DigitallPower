// Copyright (c) DIGITALL Nature. All rights reserved
// DIGITALL Nature licenses this file to you under the Microsoft Public License.

using dgt.power.dataverse;
using dgt.power.dto;
using dgt.power.import.Base;
using dgt.power.import.Logic;
using dgt.power.import.tests.Base;
using Microsoft.Xrm.Sdk;
using DocumentTemplate = dgt.power.dataverse.DocumentTemplate;

#pragma warning disable CS8629

namespace dgt.power.import.tests;

public class DocumentTemplateImportTests : ImportTestBase<DocumentTemplateImport>
{
    private (DocumentTemplate accountExcel, DocumentTemplate contactExcel, DocumentTemplate accountWord,
        DocumentTemplate contactWord) GetData() =>
    (
        new DocumentTemplate(Guid.NewGuid())
        {
            Name = "AccountExcel",
            DocumentType = new OptionSetValue(DocumentTemplate.Options.DocumentType.MicrosoftExcel),
            Content = Convert.ToBase64String(File.ReadAllBytes(GetResourcePath("Accounts.xlsx"))),
            Description = "Internal",
            LanguageCode = 1033,
            Status = DocumentTemplate.Options.Status.Activated, //inverted logic, don't ask why
            AssociatedEntityTypeCode = "account"
        },
        new DocumentTemplate(Guid.NewGuid())
        {
            Name = "ContactExcel",
            DocumentType = new OptionSetValue(DocumentTemplate.Options.DocumentType.MicrosoftExcel),
            Content = Convert.ToBase64String(File.ReadAllBytes(GetResourcePath("Contacts.xlsx"))),
            Description = "Internal",
            LanguageCode = 1033,
            Status = DocumentTemplate.Options.Status.Activated, //inverted logic, don't ask why
            AssociatedEntityTypeCode = Contact.EntityLogicalName
        },
        new DocumentTemplate(Guid.NewGuid())
        {
            Name = "AccountWord",
            DocumentType = new OptionSetValue(DocumentTemplate.Options.DocumentType.MicrosoftWord),
            Content = Convert.ToBase64String(File.ReadAllBytes(GetResourcePath("Account.docx"))),
            Description = "Internal",
            LanguageCode = 1033,
            Status = DocumentTemplate.Options.Status.Activated, //inverted logic, don't ask why
            AssociatedEntityTypeCode = "account"
        },
        new DocumentTemplate(Guid.Parse("d1004e38-1033-461c-aa0e-7043f80c49cb"))
        {
            Name = "ContactWord",
            DocumentType = new OptionSetValue(DocumentTemplate.Options.DocumentType.MicrosoftWord),
            Content = Convert.ToBase64String(File.ReadAllBytes(GetResourcePath("Contact.docx"))),
            Description = "Internal",
            LanguageCode = 1033,
            Status = DocumentTemplate.Options.Status.Activated, //inverted logic, don't ask why
            AssociatedEntityTypeCode = Contact.EntityLogicalName
        }
    );

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
                FileName = WriteConfigurationArtifact(new DocumentTemplates
                {
                    Templates = new List<dto.DocumentTemplate>()
                }).Name,
                FileDir = ArtifactDirectory
            }
        )).IsFalse();

    [Test]
    public async Task ShouldForceTemplateUpdate()
    {
        var (existingTemplate, _, _, _) = GetData();
        var templateConfiguration = GetConfigurationResource<DocumentTemplates>("force-update-templates.json");
        var forceUpdateTemplate = templateConfiguration.Templates.Single();

        var context = GetBuilder()
            .WithData(existingTemplate)
            .Build();

        await Assert.That(context.Execute(new ImportVerb
            {
                FileName = "force-update-templates.json",
                FileDir = ResourceDirectory
            }
        )).IsTrue();

        var templates = context.Get<DocumentTemplate>()
            .OrderBy(x => x.Name)
            .ToList();

        await Assert.That(templates).HasCount().EqualTo(1);

        var template = templates.Single(x => x.Name == forceUpdateTemplate.Name);
        await Assert.That(template.Id).IsEqualTo(forceUpdateTemplate.DocumentTemplateId.Value);
        await Assert.That(template.Description).IsEqualTo(forceUpdateTemplate.Description);
        await Assert.That(template.Status).IsEqualTo(forceUpdateTemplate.DocumentStatus);
    }

    [Test]
    public async Task ShouldDisableExistingTemplate()
    {
        var (_, _, existingTemplate, _) = GetData();
        var templateConfiguration = new DocumentTemplates
        {
            IgnoreMissing = false,
            Templates = new List<dto.DocumentTemplate>
            {
                new()
                {
                    DocumentTemplateId = existingTemplate.Id,
                    Name = "AccountWord",
                    File = ResourceDirectory + "/DocumentTemplate/Account.docx",
                    DocumentType = dto.DocumentTemplate.DocumentTemplateType.MicrosoftWord,
                    ForceUpdate = false,
                    Description = "Update",
                    LanguageCode = 1033,
                    AssociatedEntityTypeCode = Account.EntityLogicalName,
                    DocumentStatus = true
                }
            }
        };
        var updateTemplate = templateConfiguration.Templates.Single();

        var context = GetBuilder()
            .WithData(existingTemplate)
            .Build();

        await Assert.That(context.Execute(new ImportVerb
            {
                FileName = WriteConfigurationArtifact(templateConfiguration).FullName,
                FileDir = ArtifactDirectory
            }
        )).IsTrue();

        var templates = context.Get<DocumentTemplate>()
            .OrderBy(x => x.Name)
            .ToList();

        await Assert.That(templates).HasCount().EqualTo(1);

        var template = templates.Single(x => x.Name == updateTemplate.Name);
        await Assert.That(template.Status).IsTrue();
    }

    [Test]
    public async Task ShouldUpdateTemplate()
    {
        var templateConfiguration = GetConfigurationResource<DocumentTemplates>("update-templates.json");
        var updateTemplate = templateConfiguration.Templates.Single();
        var (_, _, existingTemplate, _) = GetData();
        existingTemplate.Status = true;
        var context = GetBuilder()
            .WithData(existingTemplate)
            .Build();

        await Assert.That(context.Execute(new ImportVerb
            {
                FileName = "update-templates.json",
                FileDir = ResourceDirectory
            }
        )).IsTrue();

        var templates = context.Get<DocumentTemplate>()
            .OrderBy(x => x.Name)
            .ToList();

        await Assert.That(templates).HasCount().EqualTo(1);

        var template = templates.Single(x => x.Name == updateTemplate.Name);
        await Assert.That(template.Id).IsNotEqualTo(updateTemplate.DocumentTemplateId.Value);
        await Assert.That(template.Description).IsEqualTo(updateTemplate.Description);
        await Assert.That(template.Status).IsEqualTo(updateTemplate.DocumentStatus);
    }

    [Test]
    public async Task ShouldFailOnMissingDocumentTemplateFile()
    {
        var templateConfiguration = GetConfigurationResource<DocumentTemplates>("not-found-templates.json");
        var createTemplate = templateConfiguration.Templates.Single();
        var context = GetBuilder()
            .Build();

        await Assert.That(context.Execute(new ImportVerb
            {
                FileName = string.Empty,
                FileDir = ResourceDirectory
            }
        )).IsFalse();

        var templates = context.Get<DocumentTemplate>()
            .OrderBy(x => x.Name)
            .ToList();

        await Assert.That(templates).HasCount().EqualTo(0);
    }


    [Test]
    public async Task ShouldCreateTemplate()
    {
        var templateConfiguration = GetConfigurationResource<DocumentTemplates>("create-templates.json");
        var createTemplate = templateConfiguration.Templates.Single();
        var context = GetBuilder()
            .Build();

        await Assert.That(context.Execute(new ImportVerb
            {
                FileName = "create-templates.json",
                FileDir = ResourceDirectory
            }
        )).IsTrue();

        var templates = context.Get<DocumentTemplate>()
            .OrderBy(x => x.Name)
            .ToList();

        await Assert.That(templates).HasCount().EqualTo(1);

        var template = templates.Single(x => x.Name == createTemplate.Name);
        await Assert.That(template.Id).IsEqualTo(createTemplate.DocumentTemplateId.Value);
        await Assert.That(template.Description).IsEqualTo(createTemplate.Description);
        await Assert.That(template.Status).IsEqualTo(createTemplate.DocumentStatus);
    }

    [Test]
    public async Task ShouldIgnoreMissingTemplates()
    {
        var missingTemplate = new DocumentTemplate(Guid.NewGuid())
        {
            Name = "Missing Template",
            Status = DocumentTemplate.Options.Status.Activated
        };
        var context = GetBuilder()
            .WithData(missingTemplate)
            .Build();

        await Assert.That(context.Execute(new ImportVerb
            {
                FileName = "update-templates.json", // We abuse update config here.
                FileDir = ResourceDirectory
            }
        )).IsTrue();

        var templates = context.Get<DocumentTemplate>()
            .OrderBy(x => x.Name)
            .ToList();

        await Assert.That(templates).HasCount().EqualTo(2);
        var missing = templates.Single(x => x.Id == missingTemplate.Id);
        await Assert.That(missing.Status).IsEqualTo(DocumentTemplate.Options.Status.Activated);
    }

    [Test]
    public async Task ShouldDisableMissingDocument()
    {
        var missingTemplate = new DocumentTemplate(Guid.NewGuid())
        {
            Name = "Missing Template",
            Status = DocumentTemplate.Options.Status.Activated
        };
        var context = GetBuilder()
            .WithData(missingTemplate)
            .Build();

        await Assert.That(context.Execute(new ImportVerb
            {
                FileName = "create-templates.json",
                FileDir = ResourceDirectory
            }
        )).IsTrue();

        var templates = context.Get<DocumentTemplate>()
            .OrderBy(x => x.Name)
            .ToList();

        await Assert.That(templates).HasCount().EqualTo(2);
        var missing = templates.Single(x => x.Id == missingTemplate.Id);
        await Assert.That(missing.Status).IsEqualTo(DocumentTemplate.Options.Status.Draft);
    }
}
