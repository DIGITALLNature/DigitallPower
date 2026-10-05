// Copyright (c) DIGITALL Nature. All rights reserved
// DIGITALL Nature licenses this file to you under the Microsoft Public License.

using dgt.power.dataverse;
using dgt.power.tests;
using dgt.power.webresource.Commands;
using Microsoft.Crm.Sdk.Messages;
using Microsoft.Xrm.Sdk;

namespace dgt.power.webresource.tests.Commands;

public class WebResourcePushCommandTests
    : CommandTestsBase<WebResourcePushCommand, WebResourcePushSettings>
{
    [Test]
    public async Task Execute_DryRunWithAllWriteActions_DoesNotWriteToDataverse()
    {
        var solutionId = Guid.NewGuid();
        var updatedResourceId = Guid.NewGuid();
        var obsoleteResourceId = Guid.NewGuid();
        var solutionComponentId = Guid.NewGuid();
        var publishRequestCount = 0;
        var addSolutionComponentRequestCount = 0;
        var directory = Directory.CreateTempSubdirectory();
        try
        {
            await File.WriteAllTextAsync(Path.Combine(directory.FullName, "update.js"), "new content");
            await File.WriteAllTextAsync(Path.Combine(directory.FullName, "create.js"), "create content");

            var existingResource = CreateWebResource(
                updatedResourceId,
                "contoso_/update.js",
                "old content");
            var obsoleteResource = CreateWebResource(
                obsoleteResourceId,
                "contoso_/obsolete.js",
                "obsolete content");
            var solutionComponent = new SolutionComponent(solutionComponentId)
            {
                Attributes =
                {
                    [SolutionComponent.LogicalNames.ObjectId] = obsoleteResourceId,
                    [SolutionComponent.LogicalNames.SolutionId] = new EntityReference(Solution.EntityLogicalName, solutionId),
                    [SolutionComponent.LogicalNames.ComponentType] = new OptionSetValue(SolutionComponent.Options.ComponentType.WebResource)
                }
            };

            var context = GetBuilder()
                .WithData(
                [
                    new Solution(solutionId) { UniqueName = "ContosoCore" },
                    existingResource,
                    obsoleteResource,
                    solutionComponent
                ])
                .WithExecutionMock<PublishXmlRequest>(_ =>
                {
                    publishRequestCount++;
                    return new OrganizationResponse();
                })
                .WithExecutionMock<AddSolutionComponentRequest>(_ =>
                {
                    addSolutionComponentRequestCount++;
                    return new OrganizationResponse();
                })
                .Build();

            var success = context.Execute(new WebResourcePushSettings
            {
                Target = directory.FullName,
                PublisherPrefix = "contoso",
                Solution = "ContosoCore",
                DeleteObsolete = true,
                DryRun = true
            });

            await Assert.That(success).IsTrue();
            await Assert.That(TestConsole.Output).Contains("DRY RUN");
            await Assert.That(TestConsole.Output).Contains("Create");
            await Assert.That(TestConsole.Output).Contains("Update");
            await Assert.That(TestConsole.Output).Contains("Obsolete webresources");
            await Assert.That(TestConsole.Output).DoesNotContain("Execution");
            await Assert.That(publishRequestCount).IsZero();
            await Assert.That(addSolutionComponentRequestCount).IsZero();
            await Assert.That(context.Get<WebResource>()).Count().IsEqualTo(2);
            await Assert.That(context.GetById<WebResource>(updatedResourceId).Content)
                .IsEqualTo(Convert.ToBase64String("old content"u8.ToArray()));
            await Assert.That(context.GetById<WebResource>(obsoleteResourceId)).IsNotNull();
            await Assert.That(context.Get<SolutionComponent>()).Count().IsEqualTo(1);
        }
        finally
        {
            directory.Delete(true);
        }
    }

    private static WebResource CreateWebResource(Guid id, string name, string content) =>
        new(id)
        {
            Name = name,
            Content = Convert.ToBase64String(System.Text.Encoding.UTF8.GetBytes(content)),
            WebResourceType = new OptionSetValue(3),
            Attributes = { [WebResource.LogicalNames.IsManaged] = false }
        };
}
