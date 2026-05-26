// Copyright (c) DIGITALL Nature. All rights reserved
// DIGITALL Nature licenses this file to you under the Microsoft Public License.

using System.ServiceModel;
using dgt.power.dataverse;
using dgt.power.dto;
using dgt.power.import.Base;
using dgt.power.import.Logic;
using dgt.power.import.tests.Base;
using dgt.power.tests;
using dgt.power.tests.FakeExecutor;
using Microsoft.Crm.Sdk;
using Microsoft.Xrm.Sdk;
using SavedQuery = dgt.power.dataverse.SavedQuery;

namespace dgt.power.import.tests;

public class OutlookTemplateImportTests : ImportTestBase<OutlookTemplateImport>
{
    protected override CommandTestContextBuilder<OutlookTemplateImport, ImportVerb> GetBuilder()
    {
        return base.GetBuilder()
            .WithFakeMessageExecutor(new QueryExpressionToFetchXmlExecutor());
    }

    [Test]
    public async Task ShouldFailOnWrongConfiguration() =>
        await Assert.That(GetContext().Execute(new ImportVerb
            {
                FileName = "",
                FileDir = ArtifactDirectory
            }
        )).IsFalse();


    [Test]
    public async Task ShouldDisableSpecifiedOutlookTemplates()
    {
        var toBeDisabledOutlookTemplate = new SavedQuery(Guid.NewGuid())
        {
            Name = "Default",
            IsDefault = true,
            QueryType = SavedQueryQueryType.OutlookTemplate
        };
        var toBeActivatedOutlookTemplate = new SavedQuery(Guid.NewGuid())
        {
            Name = "NonDefault",
            IsDefault = false,
            QueryType = SavedQueryQueryType.OutlookTemplate
        };
        var context = GetBuilder()
            .WithData(toBeDisabledOutlookTemplate)
            .WithData(toBeActivatedOutlookTemplate)
            .Build();

        var configFile = WriteConfigurationArtifact(new dto.SavedQuery
        {
            DisabledOutlookTemplates = new[]
            {
                toBeDisabledOutlookTemplate.Name
            }
        });

        await Assert.That(context.Execute(new ImportVerb
            {
                FileName = configFile.Name,
                FileDir = ArtifactDirectory
            }
        )).IsTrue();

        toBeDisabledOutlookTemplate = context.GetById<SavedQuery>(toBeDisabledOutlookTemplate.Id);
        await Assert.That(toBeDisabledOutlookTemplate.IsDefault).IsFalse();
        toBeActivatedOutlookTemplate = context.GetById<SavedQuery>(toBeActivatedOutlookTemplate.Id);
        await Assert.That(toBeActivatedOutlookTemplate.IsDefault).IsTrue();
    }

    [Test]
    public async Task ShouldUpdateExistingOutlookTemplate()
    {
        var existingOutlookTemplate = new SavedQuery(Guid.NewGuid())
        {
            Name = "Default",
            IsDefault = false,
            QueryType = SavedQueryQueryType.OutlookTemplate,
            FetchXml = "<fetch><entity name=\"contact\"/></fetch>"
        };

        var context = GetBuilder()
            .WithData(existingOutlookTemplate)
            .Build();

        const string fetchXml =
            @"<fetch version=""1.0"" output-format=""xml-platform"" mapping=""logical"" ><entity name=""contact"" ><attribute name=""fullname"" /><order attribute="">fullname"" descending=""false"" /></entity></fetch>";
        var existingConfig = new OutlookTemplate
        {
            Name = existingOutlookTemplate.Name,
            IsDefault = true,
            Entity = Contact.EntityLogicalName,
            FetchXml = fetchXml,
            Description = "some description"
        };
        var configFile = WriteConfigurationArtifact(new dto.SavedQuery
        {
            OutlookTemplates = new[]
            {
                existingConfig
            }
        });

        await Assert.That(context.Execute(new ImportVerb
            {
                FileName = configFile.Name,
                FileDir = ArtifactDirectory
            }
        )).IsTrue();

        existingOutlookTemplate = context.GetSingle<SavedQuery>(x => x.Name == existingConfig.Name);
        await Assert.That(existingOutlookTemplate.IsDefault).IsEqualTo(existingConfig.IsDefault);
        await Assert.That(existingOutlookTemplate.FetchXml).IsEqualTo(existingConfig.FetchXml);
        await Assert.That(existingOutlookTemplate.IsQuickFindQuery).IsFalse();
        await Assert.That(existingOutlookTemplate.QueryType).IsEqualTo(SavedQueryQueryType.OutlookTemplate);
        await Assert.That(existingOutlookTemplate.ReturnedTypeCode).IsEqualTo(existingConfig.Entity);
        await Assert.That(existingOutlookTemplate.Name).IsEqualTo(existingConfig.Name);
        await Assert.That(existingOutlookTemplate.Description).StartsWith(existingConfig.Description);
    }

    [Test]
    public async Task ShouldCreateNewOutlookTemplate()
    {


        var context = GetBuilder()
            .Build();

        const string fetchXml =
            @"<fetch version=""1.0"" output-format=""xml-platform"" mapping=""logical"" ><entity name=""contact"" ><attribute name=""fullname"" /><order attribute="">fullname"" descending=""false"" /></entity></fetch>";
        var template = new OutlookTemplate
        {
            Name = "New Template",
            IsDefault = true,
            Entity = Contact.EntityLogicalName,
            FetchXml = fetchXml,
            Description = "some description"
        };
        var configFile = WriteConfigurationArtifact(new dto.SavedQuery
        {
            OutlookTemplates = new[]
            {
                template
            }
        });

        await Assert.That(context.Execute(new ImportVerb
            {
                FileName = configFile.Name,
                FileDir = ArtifactDirectory
            }
        )).IsTrue();

        var createdTemplate = context.GetSingle<SavedQuery>(x => x.Name == template.Name);
        await Assert.That(createdTemplate.IsDefault).IsEqualTo(template.IsDefault);
        await Assert.That(createdTemplate.FetchXml).IsEqualTo(template.FetchXml);
        await Assert.That(createdTemplate.IsQuickFindQuery).IsFalse();
        await Assert.That(createdTemplate.QueryType).IsEqualTo(SavedQueryQueryType.OutlookTemplate);
        await Assert.That(createdTemplate.ReturnedTypeCode).IsEqualTo(template.Entity);
        await Assert.That(createdTemplate.Name).IsEqualTo(template.Name);
        await Assert.That(createdTemplate.Description).StartsWith(template.Description);
    }

    [Test]
    public async Task ShouldSkipUpdateForExistingTemplate()
    {
        var existingOutlookTemplate = new SavedQuery(Guid.NewGuid())
        {
            Name = "Default",
            IsDefault = false,
            QueryType = SavedQueryQueryType.OutlookTemplate,
            FetchXml = "<fetch><entity name=\"contact\"/></fetch>"
        };

        var context = GetBuilder()
            .WithData(existingOutlookTemplate)
            .WithExecutionMock<OrganizationRequest>(_ =>
                throw new FaultException<OrganizationServiceFault>(new OrganizationServiceFault()))
            .Build();

        var existingConfig = new OutlookTemplate
        {
            Name = existingOutlookTemplate.Name,
            IsDefault = true,
            Entity = Contact.EntityLogicalName,
            FetchXml = existingOutlookTemplate.FetchXml,
            Description = "some description"
        };
        var configFile = WriteConfigurationArtifact(new dto.SavedQuery
        {
            OutlookTemplates = new[]
            {
                existingConfig
            }
        });

        await Assert.That(context.Execute(new ImportVerb
            {
                FileName = configFile.Name,
                FileDir = ArtifactDirectory
            }
        )).IsTrue();

        var postOutlookTemplate = context.GetSingle<SavedQuery>(x => x.Id == existingOutlookTemplate.Id);
        await Assert.That(postOutlookTemplate.FetchXml).IsEqualTo(existingConfig.FetchXml);
    }
}
