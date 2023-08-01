// Copyright (c) DIGITALL Nature. All rights reserved
// DIGITALL Nature licenses this file to you under the Microsoft Public License.

using dgt.power.dataverse;
using dgt.power.dto;
using dgt.power.import.Base;
using dgt.power.import.Logic;
using dgt.power.import.tests.Base;
using FluentAssertions;
using Microsoft.Xrm.Sdk;
using Xunit.Abstractions;
using DocumentTemplate = dgt.power.dataverse.DocumentTemplate;

#pragma warning disable CS8629

namespace dgt.power.import.tests;

public class DocumentTemplateImportTests : ImportTestBase<DocumentTemplateImport>
{
    public DocumentTemplateImportTests(ITestOutputHelper testOutputHelper) : base(testOutputHelper)
    {
    }

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
                FileName = WriteConfigurationArtifact(new DocumentTemplates
                {
                    Templates = new List<dto.DocumentTemplate>()
                }).Name,
                FileDir = ArtifactDirectory
            }
        ).Should().BeFalse();

    [Fact]
    public void ShouldForceTemplateUpdate()
    {
        var (existingTemplate, _, _, _) = GetData();
        var templateConfiguration = GetConfigurationResource<DocumentTemplates>("force-update-templates.json");
        var forceUpdateTemplate = templateConfiguration.Templates.Single();

        var context = GetBuilder()
            .WithData(existingTemplate)
            .Build();

        context.Execute(new ImportVerb
            {
                FileName = "force-update-templates.json",
                FileDir = ResourceDirectory
            }
        ).Should().BeTrue();

        var templates = context.Get<DocumentTemplate>()
            .OrderBy(x => x.Name)
            .ToList();

        templates.Should().HaveCount(1);

        var template = templates.Single(x => x.Name == forceUpdateTemplate.Name);
        template.Id.Should().Be(forceUpdateTemplate.DocumentTemplateId.Value);
        template.Description.Should().Be(forceUpdateTemplate.Description);
        template.Status.Should().Be(forceUpdateTemplate.DocumentStatus);
    }

    [Fact]
    public void ShouldDisableExistingTemplate()
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

        context.Execute(new ImportVerb
            {
                FileName = WriteConfigurationArtifact(templateConfiguration).FullName,
                FileDir = ArtifactDirectory
            }
        ).Should().BeTrue();

        var templates = context.Get<DocumentTemplate>()
            .OrderBy(x => x.Name)
            .ToList();

        templates.Should().HaveCount(1);

        var template = templates.Single(x => x.Name == updateTemplate.Name);
        template.Status.Should().BeTrue();
    }

    [Fact]
    public void ShouldUpdateTemplate()
    {
        var templateConfiguration = GetConfigurationResource<DocumentTemplates>("update-templates.json");
        var updateTemplate = templateConfiguration.Templates.Single();
        var (_, _, existingTemplate, _) = GetData();
        existingTemplate.Status = true;
        var context = GetBuilder()
            .WithData(existingTemplate)
            .Build();

        context.Execute(new ImportVerb
            {
                FileName = "update-templates.json",
                FileDir = ResourceDirectory
            }
        ).Should().BeTrue();

        var templates = context.Get<DocumentTemplate>()
            .OrderBy(x => x.Name)
            .ToList();

        templates.Should().HaveCount(1);

        var template = templates.Single(x => x.Name == updateTemplate.Name);
        template.Id.Should().NotBe(updateTemplate.DocumentTemplateId.Value);
        template.Description.Should().Be(updateTemplate.Description);
        template.Status.Should().Be(updateTemplate.DocumentStatus);
    }

    [Fact]
    public void ShouldFailOnMissingDocumentTemplateFile()
    {
        var templateConfiguration = GetConfigurationResource<DocumentTemplates>("not-found-templates.json");
        var createTemplate = templateConfiguration.Templates.Single();
        var context = GetBuilder()
            .Build();

        context.Execute(new ImportVerb
            {
                FileName = string.Empty,
                FileDir = ResourceDirectory
            }
        ).Should().BeFalse();

        var templates = context.Get<DocumentTemplate>()
            .OrderBy(x => x.Name)
            .ToList();

        templates.Should().HaveCount(0);
    }


    [Fact]
    public void ShouldCreateTemplate()
    {
        var templateConfiguration = GetConfigurationResource<DocumentTemplates>("create-templates.json");
        var createTemplate = templateConfiguration.Templates.Single();
        var context = GetBuilder()
            .Build();

        context.Execute(new ImportVerb
            {
                FileName = "create-templates.json",
                FileDir = ResourceDirectory
            }
        ).Should().BeTrue();

        var templates = context.Get<DocumentTemplate>()
            .OrderBy(x => x.Name)
            .ToList();

        templates.Should().HaveCount(1);

        var template = templates.Single(x => x.Name == createTemplate.Name);
        template.Id.Should().Be(createTemplate.DocumentTemplateId.Value);
        template.Description.Should().Be(createTemplate.Description);
        template.Status.Should().Be(createTemplate.DocumentStatus);
    }

    [Fact]
    public void ShouldIgnoreMissingTemplates()
    {
        var missingTemplate = new DocumentTemplate(Guid.NewGuid())
        {
            Name = "Missing Template",
            Status = DocumentTemplate.Options.Status.Activated
        };
        var context = GetBuilder()
            .WithData(missingTemplate)
            .Build();

        context.Execute(new ImportVerb
            {
                FileName = "update-templates.json", // We abuse update config here.
                FileDir = ResourceDirectory
            }
        ).Should().BeTrue();

        var templates = context.Get<DocumentTemplate>()
            .OrderBy(x => x.Name)
            .ToList();

        templates.Should().HaveCount(2);
        var missing = templates.Single(x => x.Id == missingTemplate.Id);
        missing.Status.Should().Be(DocumentTemplate.Options.Status.Activated);
    }

    [Fact]
    public void ShouldDisableMissingDocument()
    {
        var missingTemplate = new DocumentTemplate(Guid.NewGuid())
        {
            Name = "Missing Template",
            Status = DocumentTemplate.Options.Status.Activated
        };
        var context = GetBuilder()
            .WithData(missingTemplate)
            .Build();

        context.Execute(new ImportVerb
            {
                FileName = "create-templates.json",
                FileDir = ResourceDirectory
            }
        ).Should().BeTrue();

        var templates = context.Get<DocumentTemplate>()
            .OrderBy(x => x.Name)
            .ToList();

        templates.Should().HaveCount(2);
        var missing = templates.Single(x => x.Id == missingTemplate.Id);
        missing.Status.Should().Be(DocumentTemplate.Options.Status.Draft);
    }
}
