// Copyright (c) DIGITALL Nature. All rights reserved
// DIGITALL Nature licenses this file to you under the Microsoft Public License.

using dgt.power.dto;
using dgt.power.export.Base;
using dgt.power.export.Logic;
using dgt.power.export.tests.Base;
using dgt.power.tests;
using Microsoft.Xrm.Sdk.Metadata;
using TeamTemplate = dgt.power.dataverse.TeamTemplate;

namespace dgt.power.export.tests;

public class TeamTemplateExportTest : ExportTestBase<TeamTemplateExport>
{
    protected override CommandTestContext<TeamTemplateExport, ExportVerb> GetContext()
    {
        var entityMeta = new EntityMetadata();
        entityMeta.GetType().GetProperty(nameof(EntityMetadata.LogicalName))!.SetValue(entityMeta, "custom_entity");
        entityMeta.GetType().GetProperty(nameof(EntityMetadata.ObjectTypeCode))!.SetValue(entityMeta, 10000);

        return GetBuilder()
            .WithMetaData(entityMeta)
            .WithData(new[]
                {
                    new TeamTemplate(Guid.NewGuid())
                    {
                        TeamTemplateName = "Access Team 1",
                        Description = "Access Team 1",
                        DefaultAccessRightsMask = 32,
                        ObjectTypeCode = 10000
                    },
                    new TeamTemplate(Guid.NewGuid())
                    {
                        TeamTemplateName = "Access Team 2",
                        Description = "Access Team 2",
                        DefaultAccessRightsMask = 4,
                        ObjectTypeCode = 10000
                    }
                }
            )
            .Build();
    }

    [Test]
    public async Task ShouldExportAccessTeamsWithDefaultConfiguration()
    {
        await Assert.That(GetContext().Execute(new ExportVerb
            {
                FileName = GetTestFileName(),
                FileDir = ArtifactDirectory
            }
        )).IsTrue();

        var teamTemplates = GetConfigurationTestArtifact<TeamTemplates>(GetTestFileName());
        await Assert.That(teamTemplates).Count().IsEqualTo(2);
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

        var teamTemplates = GetConfigurationTestArtifact<TeamTemplates>("teamtemplate.json");
        await Assert.That(teamTemplates).Count().IsEqualTo(2);
    }
}
