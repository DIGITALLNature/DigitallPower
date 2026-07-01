// Copyright (c) DIGITALL Nature. All rights reserved
// DIGITALL Nature licenses this file to you under the Microsoft Public License.

using dgt.power.dataverse;
using dgt.power.push.Base;
using dgt.power.push.Logic;
using dgt.power.push.tests.Base;
using dgt.power.tests;
using dgt.power.tests.FakeExecutor;
using Microsoft.Crm.Sdk.Messages;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Xrm.Sdk;

namespace dgt.power.push.tests.Logic;

public class WebresourcesProcessorTests : PushTestsBase<PushCommand>
{
    private const string SolutionUniqueName = "TestSolution";
    private const string PublisherPrefix = "test";

    // Base64 of the content of Resources/PushCommand/webresources/test_/main.js
    private const string KnownJsBase64 = "Ly8gdGVzdCB3ZWJyZXNvdXJjZQpmdW5jdGlvbiBoZWxsbygpIHsgcmV0dXJuICJ3b3JsZCI7IH0K";
    private const string WebresourceLogicalName = "test_/main.js";

    private string WebresourcesFolder => Path.GetFullPath(
        Path.Combine(ResourceDirectory, "webresources"));

    private static (Publisher publisher, Solution solution) CreateSolutionData()
    {
        var publisherId = Guid.NewGuid();
        var publisher = new Publisher(publisherId)
        {
            CustomizationPrefix = PublisherPrefix
        };
        var solution = new Solution(Guid.NewGuid())
        {
            UniqueName = SolutionUniqueName,
            PublisherId = new EntityReference(Publisher.EntityLogicalName, publisherId)
        };
        return (publisher, solution);
    }

    protected override CommandTestContext<PushCommand, PushVerb> GetContext() =>
        GetBuilder()
            .WithServiceCollection(new TestServiceCollection().AddScoped<WebresourcesProcessor>())
            .WithFakeMessageExecutor(new AddSolutionComponentExecutor())
            .Build();

    [Test]
    public async Task ShouldAddNewWebresourceToSolution_WhenSolutionIsSpecified()
    {
        // Arrange
        var addedComponents = new List<AddSolutionComponentRequest>();
        var (publisher, solution) = CreateSolutionData();
        var ctx = GetBuilder()
            .WithServiceCollection(new TestServiceCollection().AddScoped<WebresourcesProcessor>())
            .WithExecutionMock<AddSolutionComponentRequest>(req =>
            {
                addedComponents.Add((AddSolutionComponentRequest)req);
                return new AddSolutionComponentResponse();
            })
            .WithData(publisher)
            .WithData(solution)
            .Build();

        // Act
        var result = ctx.Execute(new PushVerb
        {
            Target = WebresourcesFolder,
            Solution = SolutionUniqueName
        });

        // Assert
        await Assert.That(result).IsTrue();
        await Assert.That(addedComponents).Count().IsEqualTo(1);
        await Assert.That(addedComponents[0].SolutionUniqueName).IsEqualTo(SolutionUniqueName);
        await Assert.That(addedComponents[0].ComponentType).IsEqualTo(SolutionComponent.Options.ComponentType.WebResource);
    }

    [Test]
    public async Task ShouldAddExistingUpdatedWebresourceToSolution_WhenSolutionIsSpecified()
    {
        // Arrange - web resource already exists in CRM but with different (old) content
        var addedComponents = new List<AddSolutionComponentRequest>();
        var (publisher, solution) = CreateSolutionData();
        var existingWebResourceId = Guid.NewGuid();
        var existingWebResource = new WebResource(existingWebResourceId)
        {
            Name = WebresourceLogicalName,
            WebResourceType = new OptionSetValue(WebResource.Options.WebResourceType.ScriptJScript),
            Content = Convert.ToBase64String("old content"u8.ToArray())
        };

        var ctx = GetBuilder()
            .WithServiceCollection(new TestServiceCollection().AddScoped<WebresourcesProcessor>())
            .WithExecutionMock<AddSolutionComponentRequest>(req =>
            {
                addedComponents.Add((AddSolutionComponentRequest)req);
                return new AddSolutionComponentResponse();
            })
            .WithData(publisher)
            .WithData(solution)
            .WithData(existingWebResource)
            .Build();

        // Act
        var result = ctx.Execute(new PushVerb
        {
            Target = WebresourcesFolder,
            Solution = SolutionUniqueName
        });

        // Assert
        await Assert.That(result).IsTrue();
        await Assert.That(addedComponents).Count().IsEqualTo(1);
        await Assert.That(addedComponents[0].ComponentId).IsEqualTo(existingWebResourceId);
        await Assert.That(addedComponents[0].SolutionUniqueName).IsEqualTo(SolutionUniqueName);
        await Assert.That(addedComponents[0].ComponentType).IsEqualTo(SolutionComponent.Options.ComponentType.WebResource);
    }

    [Test]
    public async Task ShouldAddUnchangedWebresourceToSolution_WhenSolutionIsSpecified()
    {
        // Arrange - web resource already exists in CRM with identical content (Up2Date)
        var addedComponents = new List<AddSolutionComponentRequest>();
        var (publisher, solution) = CreateSolutionData();
        var existingWebResourceId = Guid.NewGuid();
        var existingWebResource = new WebResource(existingWebResourceId)
        {
            Name = WebresourceLogicalName,
            WebResourceType = new OptionSetValue(WebResource.Options.WebResourceType.ScriptJScript),
            Content = KnownJsBase64
        };

        var ctx = GetBuilder()
            .WithServiceCollection(new TestServiceCollection().AddScoped<WebresourcesProcessor>())
            .WithExecutionMock<AddSolutionComponentRequest>(req =>
            {
                addedComponents.Add((AddSolutionComponentRequest)req);
                return new AddSolutionComponentResponse();
            })
            .WithData(publisher)
            .WithData(solution)
            .WithData(existingWebResource)
            .Build();

        // Act
        var result = ctx.Execute(new PushVerb
        {
            Target = WebresourcesFolder,
            Solution = SolutionUniqueName
        });

        // Assert
        await Assert.That(result).IsTrue();
        await Assert.That(addedComponents).Count().IsEqualTo(1);
        await Assert.That(addedComponents[0].ComponentId).IsEqualTo(existingWebResourceId);
        await Assert.That(addedComponents[0].SolutionUniqueName).IsEqualTo(SolutionUniqueName);
        await Assert.That(addedComponents[0].ComponentType).IsEqualTo(SolutionComponent.Options.ComponentType.WebResource);
    }

    [Test]
    public async Task ShouldNotAddWebresourceToSolution_WhenNoSolutionIsSpecified()
    {
        // Arrange
        var addedComponents = new List<AddSolutionComponentRequest>();
        var ctx = GetBuilder()
            .WithServiceCollection(new TestServiceCollection().AddScoped<WebresourcesProcessor>())
            .WithExecutionMock<AddSolutionComponentRequest>(req =>
            {
                addedComponents.Add((AddSolutionComponentRequest)req);
                return new AddSolutionComponentResponse();
            })
            .Build();

        // Act
        var result = ctx.Execute(new PushVerb
        {
            Target = WebresourcesFolder
        });

        // Assert
        await Assert.That(result).IsTrue();
        await Assert.That(addedComponents).IsEmpty();
    }

    [Test]
    public async Task ShouldNotAddWebresourceToSolution_WhenAlreadyInSolutionAndUnchanged()
    {
        // Arrange — resource is Up2Date in CRM and already in solution
        var addedComponents = new List<AddSolutionComponentRequest>();
        var (publisher, solution) = CreateSolutionData();
        var existingWebResourceId = Guid.NewGuid();
        var existingWebResource = new WebResource(existingWebResourceId)
        {
            Name = WebresourceLogicalName,
            WebResourceType = new OptionSetValue((int)WebResource.Options.WebResourceType.ScriptJScript),
            Content = KnownJsBase64,
            Attributes = { [WebResource.LogicalNames.IsManaged] = false }
        };

        var solutionComponent = new SolutionComponent(Guid.NewGuid())
        {
            Attributes =
            {
                [SolutionComponent.LogicalNames.ObjectId] = existingWebResourceId,
                [SolutionComponent.LogicalNames.SolutionId] =
                    new EntityReference(Solution.EntityLogicalName, solution.Id)
            }
        };

        var ctx = GetBuilder()
            .WithServiceCollection(new TestServiceCollection().AddScoped<WebresourcesProcessor>())
            .WithExecutionMock<AddSolutionComponentRequest>(req =>
            {
                addedComponents.Add((AddSolutionComponentRequest)req);
                return new AddSolutionComponentResponse();
            })
            .WithData(publisher)
            .WithData(solution)
            .WithData(existingWebResource)
            .WithData(solutionComponent)
            .Build();

        // Act
        var result = ctx.Execute(new PushVerb
        {
            Target = WebresourcesFolder,
            Solution = SolutionUniqueName
        });

        // Assert
        await Assert.That(result).IsTrue();
        await Assert.That(addedComponents).IsEmpty();
    }

    [Test]
    public async Task ShouldNotAddWebresourceToSolution_WhenAlreadyInSolutionAndUpdated()
    {
        // Arrange — resource has stale content in CRM (needs update) but is already in solution
        var addedComponents = new List<AddSolutionComponentRequest>();
        var (publisher, solution) = CreateSolutionData();
        var existingWebResourceId = Guid.NewGuid();
        var existingWebResource = new WebResource(existingWebResourceId)
        {
            Name = WebresourceLogicalName,
            WebResourceType = new OptionSetValue((int)WebResource.Options.WebResourceType.ScriptJScript),
            Content = Convert.ToBase64String("old content"u8.ToArray()),
            Attributes = { [WebResource.LogicalNames.IsManaged] = false }
        };

        var solutionComponent = new SolutionComponent(Guid.NewGuid())
        {
            Attributes =
            {
                [SolutionComponent.LogicalNames.ObjectId] = existingWebResourceId,
                [SolutionComponent.LogicalNames.SolutionId] =
                    new EntityReference(Solution.EntityLogicalName, solution.Id)
            }
        };

        var ctx = GetBuilder()
            .WithServiceCollection(new TestServiceCollection().AddScoped<WebresourcesProcessor>())
            .WithExecutionMock<AddSolutionComponentRequest>(req =>
            {
                addedComponents.Add((AddSolutionComponentRequest)req);
                return new AddSolutionComponentResponse();
            })
            .WithData(publisher)
            .WithData(solution)
            .WithData(existingWebResource)
            .WithData(solutionComponent)
            .Build();

        // Act
        var result = ctx.Execute(new PushVerb
        {
            Target = WebresourcesFolder,
            Solution = SolutionUniqueName
        });

        // Assert
        await Assert.That(result).IsTrue();
        await Assert.That(addedComponents).IsEmpty();
    }
}
