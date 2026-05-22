// Copyright (c) DIGITALL Nature. All rights reserved
// DIGITALL Nature licenses this file to you under the Microsoft Public License.

using dgt.power.dataverse;
using dgt.power.dto;
using dgt.power.import.Base;
using dgt.power.import.Logic;
using dgt.power.import.tests.Base;
using dgt.power.tests;
using dgt.power.tests.Extensions;
using dgt.power.tests.FakeExecutor;
using Digitall.Dataverse.Testing;
using Microsoft.Xrm.Sdk.Messages;
using Microsoft.Xrm.Sdk.Metadata;
using TeamTemplate = dgt.power.dto.TeamTemplate;
#pragma warning disable CS8601

namespace dgt.power.import.tests;

public class TeamTemplateImportTests : ImportTestBase<TeamTemplateImport>
{
    private readonly EntityMetadata _testEntityMetadata;

    public TeamTemplateImportTests()
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
            .WithFakeMessageExecutor(new RetrieveAllEntitiesExecutor())
            .WithMetaData(_testEntityMetadata);

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
        await Assert.That(GetContext()
        .Execute(new ImportVerb
        {
            FileName = WriteConfigurationArtifact(new TeamTemplates()).Name,
            FileDir = ArtifactDirectory
        })).IsFalse();

    [Test]
    public async Task ShouldDeleteMissingTeamTemplate()
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
        await Assert.That(context.Execute(new ImportVerb
        {
            FileName = WriteConfigurationArtifact(templateConfig).Name,
            FileDir = ArtifactDirectory
        })).IsTrue();

        await Assert.That(context.Get<dataverse.TeamTemplate>().Any(x => x.Id == missingTemplate.Id)).IsFalse();
    }

    [Test]
    public async Task ShouldUpdateExistingTeamTemplate()
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
        await Assert.That(context.Execute(new ImportVerb
        {
            FileName = WriteConfigurationArtifact(new TeamTemplates { teamTemplateConfig }).Name,
            FileDir = ArtifactDirectory
        })).IsTrue();

        var postTemplate = context.GetSingle<dataverse.TeamTemplate>(x => x.Id == existingTemplate.Id);
        await Assert.That(postTemplate.TeamTemplateName).IsEqualTo(teamTemplateConfig.TeamTemplateName);
        await Assert.That(postTemplate.Description).IsEqualTo(teamTemplateConfig.Description);
        await Assert.That(postTemplate.IsSystem).IsEqualTo(teamTemplateConfig.IsSystem);
        await Assert.That(postTemplate.DefaultAccessRightsMask).IsEqualTo(teamTemplateConfig.DefaultAccessRightsMask);
        await Assert.That(postTemplate.ObjectTypeCode).IsEqualTo(TestEntity.EntityTypeCode);
    }

    [Test]
    public async Task ShouldFailOnTemplateUpdateIfMetadataNotFound()
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
        await Assert.That(context.Execute(new ImportVerb
        {
            FileName = WriteConfigurationArtifact(new TeamTemplates { teamTemplateConfig }).Name,
            FileDir = ArtifactDirectory
        })).IsFalse();

        var postTemplate = context.GetSingle<dataverse.TeamTemplate>(x => x.Id == existingTemplate.Id);
        await Assert.That(postTemplate.ModifiedOn).IsEqualTo(existingTemplate.ModifiedOn);
    }


    [Test]
    public async Task ShouldSkipUpdateIfUnchanged()
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
        await Assert.That(context.Execute(new ImportVerb
        {
            FileName = WriteConfigurationArtifact(new TeamTemplates { teamTemplateConfig }).Name,
            FileDir = ArtifactDirectory
        })).IsTrue();

        var postTemplate = context.GetSingle<dataverse.TeamTemplate>(x => x.Id == existingTemplate.Id);
        await Assert.That(postTemplate.ModifiedOn).IsEqualTo(existingTemplate.ModifiedOn);
    }

    [Test]
    public async Task ShouldFailOnEntityCreationBecauseEntityIsNotEnabledForAccessTeams()
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
        await Assert.That(context.Execute(new ImportVerb
        {
            FileName = WriteConfigurationArtifact(new TeamTemplates { teamTemplateConfig }).Name,
            FileDir = ArtifactDirectory
        })).IsFalse();
    }

    [Test]
    public async Task ShouldFailOnTemplateCreationIfObjectTypeCodeIsNull()
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
        await Assert.That(context.Execute(new ImportVerb
        {
            FileName = WriteConfigurationArtifact(new TeamTemplates { teamTemplateConfig }).Name,
            FileDir = ArtifactDirectory
        })).IsFalse();
    }

    [Test]
    public async Task ShouldCreateNewTeamTemplate()
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
        await Assert.That(context.Execute(new ImportVerb
        {
            FileName = WriteConfigurationArtifact(new TeamTemplates { teamTemplateConfig }).Name,
            FileDir = ArtifactDirectory
        })).IsTrue();

        var postTemplate = context.GetSingle<dataverse.TeamTemplate>(x => x.Id == teamTemplateConfig.TeamTemplateId);
        await Assert.That(postTemplate.TeamTemplateName).IsEqualTo(teamTemplateConfig.TeamTemplateName);
        await Assert.That(postTemplate.Description).IsEqualTo(teamTemplateConfig.Description);
        await Assert.That(postTemplate.DefaultAccessRightsMask).IsEqualTo(teamTemplateConfig.DefaultAccessRightsMask);
        await Assert.That(postTemplate.ObjectTypeCode).IsEqualTo(TestEntity.EntityTypeCode);
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
