// Copyright (c) DIGITALL Nature. All rights reserved
// DIGITALL Nature licenses this file to you under the Microsoft Public License.

using dgt.power.dataverse;
using dgt.power.dto;
using dgt.power.import.Base;
using dgt.power.import.Logic;
using dgt.power.import.tests.Base;
using dgt.power.tests;
using dgt.power.tests.FakeExecutor;
using FakeXrmEasy.Extensions;
using FluentAssertions;
using Microsoft.Xrm.Sdk.Messages;
using Microsoft.Xrm.Sdk.Metadata;
using Xunit.Abstractions;
using TeamTemplate = dgt.power.dto.TeamTemplate;
#pragma warning disable CS8601

namespace dgt.power.import.tests;

public class TeamTemplateImportTests : ImportTestBase<TeamTemplateImport>
{
    private readonly EntityMetadata _testEntityMetadata;

    public TeamTemplateImportTests(ITestOutputHelper testOutputHelper) : base(testOutputHelper)
    {
        _testEntityMetadata = new EntityMetadata
        {
            LogicalName = TestEntity.EntityLogicalName,
            MetadataId = Guid.NewGuid(),
            AutoCreateAccessTeams = true,
        };
        _testEntityMetadata.SetSealedPropertyValue(nameof(_testEntityMetadata.ObjectTypeCode),
            TestEntity.EntityTypeCode);
    }

    protected override CommandTestContextBuilder<TeamTemplateImport, ImportVerb> GetBuilder() =>
        base.GetBuilder()
            .WithFakeMessageExecutor<RetrieveAllEntitiesRequest>(new RetrieveAllEntitiesExecutor())
            .WithMetaData(_testEntityMetadata);

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
            FileName = WriteConfigurationArtifact(new TeamTemplates()).Name,
            FileDir = ArtifactDirectory
        }).Should().BeFalse();

    [Fact]
    public void ShouldDeleteMissingTeamTemplate()
    {
        var missingTemplate = GetData();
        var context = GetBuilder()
            .WithData(missingTemplate)
            .Build();
        var templateConfig = new TeamTemplates
        {
            // we need some templates to not skip execution
            new()
            {
                Entity = TestEntity.EntityLogicalName,
                Description = "some template",
                IsSystem = false,
                TeamTemplateId = Guid.NewGuid(),
                TeamTemplateName = "Some Name",
                DefaultAccessRightsMask = 2
            }
        };
        context.Execute(new ImportVerb
        {
            FileName = WriteConfigurationArtifact(templateConfig).Name,
            FileDir = ArtifactDirectory
        }).Should().BeTrue();

        context.Get<dataverse.TeamTemplate>().Should().NotContain(x => x.Id == missingTemplate.Id);
    }

    [Fact]
    public void ShouldUpdateExistingTeamTemplate()
    {
        var existingTemplate = GetData();
        var context = GetBuilder()
            .WithData(existingTemplate)
            .Build();
        var teamTemplateConfig = new TeamTemplate
        {
            Entity = TestEntity.EntityLogicalName,
            Description = $"{existingTemplate.Description} Updated",
            IsSystem = existingTemplate.IsSystem,
            TeamTemplateId = existingTemplate.Id,
            TeamTemplateName = $"{existingTemplate.TeamTemplateName} Updated",
            DefaultAccessRightsMask = 8
        };
        context.Execute(new ImportVerb
        {
            FileName = WriteConfigurationArtifact(new TeamTemplates { teamTemplateConfig }).Name,
            FileDir = ArtifactDirectory
        }).Should().BeTrue();

        var postTemplate = context.GetSingle<dataverse.TeamTemplate>(x => x.Id == existingTemplate.Id);
        postTemplate.TeamTemplateName.Should().Be(teamTemplateConfig.TeamTemplateName);
        postTemplate.Description.Should().Be(teamTemplateConfig.Description);
        postTemplate.IsSystem.Should().Be(teamTemplateConfig.IsSystem);
        postTemplate.DefaultAccessRightsMask.Should().Be(teamTemplateConfig.DefaultAccessRightsMask);
        postTemplate.ObjectTypeCode.Should().Be(TestEntity.EntityTypeCode);
    }

    [Fact]
    public void ShouldFailOnTemplateUpdateIfMetadataNotFound()
    {
        var existingTemplate = GetData();
        existingTemplate[dataverse.TeamTemplate.LogicalNames.ModifiedOn] = DateTime.UtcNow.AddDays(-1);
        var context = GetBuilder()
            .WithData(existingTemplate)
            .Build();
        var teamTemplateConfig = new TeamTemplate
        {
            Entity = "some",
            Description = existingTemplate.Description,
            IsSystem = existingTemplate.IsSystem,
            TeamTemplateId = existingTemplate.Id,
            TeamTemplateName = existingTemplate.TeamTemplateName,
            DefaultAccessRightsMask = existingTemplate.DefaultAccessRightsMask
        };
        context.Execute(new ImportVerb
        {
            FileName = WriteConfigurationArtifact(new TeamTemplates { teamTemplateConfig }).Name,
            FileDir = ArtifactDirectory
        }).Should().BeFalse();

        var postTemplate = context.GetSingle<dataverse.TeamTemplate>(x => x.Id == existingTemplate.Id);
        postTemplate.ModifiedOn.Should().Be(existingTemplate.ModifiedOn);
    }

    
    [Fact]
    public void ShouldSkipUpdateIfUnchanged()
    {
        var existingTemplate = GetData();
        existingTemplate[dataverse.TeamTemplate.LogicalNames.ModifiedOn] = DateTime.UtcNow.AddDays(-1);
        var context = GetBuilder()
            .WithData(existingTemplate)
            .Build();
        var teamTemplateConfig = new TeamTemplate
        {
            Entity = TestEntity.EntityLogicalName,
            Description = existingTemplate.Description,
            IsSystem = existingTemplate.IsSystem,
            TeamTemplateId = existingTemplate.Id,
            TeamTemplateName = existingTemplate.TeamTemplateName,
            DefaultAccessRightsMask = existingTemplate.DefaultAccessRightsMask
        };
        context.Execute(new ImportVerb
        {
            FileName = WriteConfigurationArtifact(new TeamTemplates { teamTemplateConfig }).Name,
            FileDir = ArtifactDirectory
        }).Should().BeTrue();

        var postTemplate = context.GetSingle<dataverse.TeamTemplate>(x => x.Id == existingTemplate.Id);
        postTemplate.ModifiedOn.Should().Be(existingTemplate.ModifiedOn);
    }

    [Fact]
    public void ShouldFailOnEntityCreationBecauseEntityIsNotEnabledForAccessTeams()
    {
        _testEntityMetadata.AutoCreateAccessTeams = null;
        var context = GetBuilder()
            .Build();
        var teamTemplateConfig = new TeamTemplate
        {
            Entity = TestEntity.EntityLogicalName,
            Description = "New",
            IsSystem = false,
            TeamTemplateId = Guid.NewGuid(),
            TeamTemplateName = "New",
            DefaultAccessRightsMask = 16
        };
        context.Execute(new ImportVerb
        {
            FileName = WriteConfigurationArtifact(new TeamTemplates { teamTemplateConfig }).Name,
            FileDir = ArtifactDirectory
        }).Should().BeFalse();
    }

    [Fact]
    public void ShouldFailOnTemplateCreationIfObjectTypeCodeIsNull()
    {
        _testEntityMetadata.SetSealedPropertyValue(nameof(_testEntityMetadata.ObjectTypeCode), null);
        var context = GetBuilder()
            .Build();
        
        var teamTemplateConfig = new TeamTemplate
        {
            Entity = TestEntity.EntityLogicalName,
            Description = "New",
            IsSystem = false,
            TeamTemplateId = Guid.NewGuid(),
            TeamTemplateName = "New",
            DefaultAccessRightsMask = 16
        };
        context.Execute(new ImportVerb
        {
            FileName = WriteConfigurationArtifact(new TeamTemplates { teamTemplateConfig }).Name,
            FileDir = ArtifactDirectory
        }).Should().BeFalse();
    }

    [Fact]
    public void ShouldCreateNewTeamTemplate()
    {
        var context = GetBuilder()
            .Build();
        var teamTemplateConfig = new TeamTemplate
        {
            Entity = TestEntity.EntityLogicalName,
            Description = "New",
            IsSystem = false,
            TeamTemplateId = Guid.NewGuid(),
            TeamTemplateName = "New",
            DefaultAccessRightsMask = 16
        };
        context.Execute(new ImportVerb
        {
            FileName = WriteConfigurationArtifact(new TeamTemplates { teamTemplateConfig }).Name,
            FileDir = ArtifactDirectory
        }).Should().BeTrue();

        var postTemplate = context.GetSingle<dataverse.TeamTemplate>(x => x.Id == teamTemplateConfig.TeamTemplateId);
        postTemplate.TeamTemplateName.Should().Be(teamTemplateConfig.TeamTemplateName);
        postTemplate.Description.Should().Be(teamTemplateConfig.Description);
        postTemplate.DefaultAccessRightsMask.Should().Be(teamTemplateConfig.DefaultAccessRightsMask);
        postTemplate.ObjectTypeCode.Should().Be(TestEntity.EntityTypeCode);
    }

    private static dataverse.TeamTemplate GetData()
    {
        var missingTemplate = new dataverse.TeamTemplate(Guid.NewGuid())
        {
            TeamTemplateName = "Template",
            Description = "Team Template",
            DefaultAccessRightsMask = 4,
            ObjectTypeCode = TestEntity.EntityTypeCode,
            [dataverse.TeamTemplate.LogicalNames.IsSystem] = false
        };

        return missingTemplate;
    }
}
