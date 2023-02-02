using dgt.power.dataverse;
using dgt.power.dto;
using dgt.power.export.Base;
using dgt.power.export.Logic;
using dgt.power.export.tests.Base;
using dgt.power.tests;
using FluentAssertions;
using Microsoft.Xrm.Sdk;
using Xunit.Abstractions;
using DocumentTemplate = dgt.power.dataverse.DocumentTemplate;

namespace dgt.power.export.tests;

public class DocumentTemplateExportTests : ExportTestBase<DocumentTemplateExport>
{

    public DocumentTemplateExportTests(ITestOutputHelper testOutputHelper) : base(testOutputHelper)
    {
    }

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


    [Fact]
    public void ShouldFilterTemplatesWhenAddingInlineDataFilter()
    {
        const string fetchXml = "<filter><condition attribute=\"documenttype\" operator=\"eq\" value=\"1\" /></filter>";
        GetContext().Execute(new ExportVerb
            {
                FileName = GetTestFileName(),
                FileDir = ArtifactDirectory,
                InlineData = fetchXml
            }
        ).Should().BeTrue();

        var templates =
            GetConfigurationTestArtifact<DocumentTemplates>(GetTestFileName());
        templates.Templates.Should().HaveCount(2);

        foreach (var template in templates.Templates)
        {
            File.Exists(GetArtifactPath(template.File)).Should().BeTrue();
        }
    }

    [Fact]
    public void ShouldFailOnInvalidFetchXml()
    {
        const string fetchXml = "<invalid-fetch/>";
        GetContext().Execute(new ExportVerb
            {
                FileName = GetTestFileName(),
                FileDir = ArtifactDirectory,
                InlineData = fetchXml
            }
        ).Should().BeFalse();
    }

    [Fact]
    public void ShouldExportTemplatesWithAdvancedInlineData()
    {
        const string inlineData =
            "force=false,missing=false<filter><condition attribute=\"documenttype\" operator=\"eq\" value=\"1\" /></filter>";

        GetContext()
            .Execute(new ExportVerb
                {
                    FileName = GetTestFileName(),
                    FileDir = ArtifactDirectory,
                    InlineData = inlineData
                }
            ).Should().BeTrue();

        var templates =
            GetConfigurationTestArtifact<DocumentTemplates>(GetTestFileName());

        templates.Templates.Should().HaveCount(2);

        foreach (var template in templates.Templates)
        {
            File.Exists(GetArtifactPath(template.File)).Should().BeTrue();
        }
    }

    [Fact]
    public void ShouldSetForceUpdateWhithDefaultConfiguration()
    {
        GetContext()
            .Execute(new ExportVerb
                {
                    FileName = GetTestFileName(),
                    FileDir = ArtifactDirectory,
                }
            ).Should().BeTrue();
        var templates =
            GetConfigurationTestArtifact<DocumentTemplates>(GetTestFileName());
        templates.Templates.Should().HaveCount(4);

        foreach (var template in templates.Templates) {
            template.ForceUpdate.Should().BeTrue();
        }
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
        var templates = GetConfigurationTestArtifact<DocumentTemplates>("documenttemplate.json");
        templates.Templates.Should().HaveCount(4);

        foreach (var template in templates.Templates)
        {
            template.ForceUpdate.Should().BeTrue();
        }
    }
}
