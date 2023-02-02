using dgt.power.dto;
using dgt.power.export.Base;
using dgt.power.export.Logic;
using dgt.power.export.tests.Base;
using dgt.power.tests;
using dgt.power.tests.FakeExecutor;
using FluentAssertions;
using Microsoft.Xrm.Sdk.Messages;
using Xunit.Abstractions;
using TeamTemplate = dgt.power.dataverse.TeamTemplate;

namespace dgt.power.export.tests;

public class TeamTemplateExportTest : ExportTestBase<TeamTemplateExport>
{
    public TeamTemplateExportTest(ITestOutputHelper testOutputHelper) : base(testOutputHelper)
    {
    }

    protected override CommandTestContext<TeamTemplateExport, ExportVerb> GetContext()
    {
        return GetBuilder()
            .WithFakeMessageExecutor<RetrieveAllEntitiesRequest>(new RetrieveAllEntitiesExecutor())
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

    [Fact]
    public void ShouldExportAccessTeamsWithDefaultConfiguration()
    {
        GetContext().Execute(new ExportVerb
            {
                FileName = GetTestFileName(),
                FileDir = ArtifactDirectory,
            }
        ).Should().BeTrue();

        var teamTemplates = GetConfigurationTestArtifact<TeamTemplates>(GetTestFileName());
        teamTemplates.Should().HaveCount(2);
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

        var teamTemplates = GetConfigurationTestArtifact<TeamTemplates>("teamtemplate.json");
        teamTemplates.Should().HaveCount(2);
    }
}
