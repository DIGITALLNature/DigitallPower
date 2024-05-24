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
using FluentAssertions;
using Microsoft.Crm.Sdk;
using Microsoft.Crm.Sdk.Messages;
using Microsoft.Xrm.Sdk;
using Xunit.Abstractions;
using SavedQuery = dgt.power.dataverse.SavedQuery;

namespace dgt.power.import.tests;

public class OutlookTemplateImportTests : ImportTestBase<OutlookTemplateImport>
{
    public OutlookTemplateImportTests(ITestOutputHelper testOutputHelper) : base(testOutputHelper)
    {
    }

    protected override CommandTestContextBuilder<OutlookTemplateImport, ImportVerb> GetBuilder()
    {
        return base.GetBuilder()
            .WithFakeMessageExecutor<QueryExpressionToFetchXmlRequest>(new QueryExpressionToFetchXmlExecutor());
    }

    [Fact]
    public void ShouldFailOnWrongConfiguration() =>
        GetContext().Execute(new ImportVerb
            {
                FileName = "",
                FileDir = ArtifactDirectory
            }
        ).Should().BeFalse();


    [Fact]
    public void ShouldDisableSpecifiedOutlookTemplates()
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

        context.Execute(new ImportVerb
            {
                FileName = configFile.Name,
                FileDir = ArtifactDirectory
            }
        ).Should().BeTrue();

        toBeDisabledOutlookTemplate = context.GetById<SavedQuery>(toBeDisabledOutlookTemplate.Id);
        toBeDisabledOutlookTemplate.IsDefault.Should().BeFalse();
        toBeActivatedOutlookTemplate = context.GetById<SavedQuery>(toBeActivatedOutlookTemplate.Id);
        toBeActivatedOutlookTemplate.IsDefault.Should().BeTrue();
    }

    [Fact]
    public void ShouldUpdateExistingOutlookTemplate()
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
                existingConfig,
            }
        });

        context.Execute(new ImportVerb
            {
                FileName = configFile.Name,
                FileDir = ArtifactDirectory
            }
        ).Should().BeTrue();

        existingOutlookTemplate = context.GetSingle<SavedQuery>(x => x.Name == existingConfig.Name);
        existingOutlookTemplate.IsDefault.Should().Be(existingConfig.IsDefault);
        existingOutlookTemplate.FetchXml.Should().Be(existingConfig.FetchXml);
        existingOutlookTemplate.IsQuickFindQuery.Should().BeFalse();
        existingOutlookTemplate.QueryType.Should().Be(SavedQueryQueryType.OutlookTemplate);
        existingOutlookTemplate.ReturnedTypeCode.Should().Be(existingConfig.Entity);
        existingOutlookTemplate.Name.Should().Be(existingConfig.Name);
        existingOutlookTemplate.Description.Should().StartWith(existingConfig.Description);
    }

    [Fact]
    public void ShouldCreateNewOutlookTemplate()
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
                template,
            }
        });

        context.Execute(new ImportVerb
            {
                FileName = configFile.Name,
                FileDir = ArtifactDirectory
            }
        ).Should().BeTrue();

        var createdTemplate = context.GetSingle<SavedQuery>(x => x.Name == template.Name);
        createdTemplate.IsDefault.Should().Be(template.IsDefault);
        createdTemplate.FetchXml.Should().Be(template.FetchXml);
        createdTemplate.IsQuickFindQuery.Should().BeFalse();
        createdTemplate.QueryType.Should().Be(SavedQueryQueryType.OutlookTemplate);
        createdTemplate.ReturnedTypeCode.Should().Be(template.Entity);
        createdTemplate.Name.Should().Be(template.Name);
        createdTemplate.Description.Should().StartWith(template.Description);
    }

    [Fact]
    public void ShouldSkipUpdateForExistingTemplate()
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
                existingConfig,
            }
        });

        context.Execute(new ImportVerb
            {
                FileName = configFile.Name,
                FileDir = ArtifactDirectory
            }
        ).Should().BeTrue();

        var postOutlookTemplate = context.GetSingle<SavedQuery>(x => x.Id == existingOutlookTemplate.Id);
        postOutlookTemplate.FetchXml.Should().Be(existingConfig.FetchXml);
    }
}
