// Copyright (c) DIGITALL Nature. All rights reserved
// DIGITALL Nature licenses this file to you under the Microsoft Public License.

using dgt.power.dataverse;
using dgt.power.push.Logic;
using dgt.power.push.Model;
using dgt.power.tests.FakeExecutor;
using Digitall.Dataverse.Testing;
using Microsoft.Xrm.Sdk;
using Spectre.Console.Testing;
using PluginType = dgt.power.push.Model.PluginType;

namespace dgt.power.push.tests.Logic;

public class AssemblyProcessorMigrationTests
{
    private FakeOrganizationServiceAsync _service = null!;
    private AssemblyProcessor _processor = null!;
    private TestConsole _console = null!;

    [Before(Test)]
    public void Setup()
    {
        _service = new FakeOrganizationServiceAsync();
        _service.AddRequests([new AddSolutionComponentExecutor()]);
        _service.AddDefaultRequests();
        _console = new TestConsole();
        _processor = new AssemblyProcessor(_service, _console);
    }

    [After(Test)]
    public void Teardown()
    {
        _processor.Dispose();
    }

    #region MigratePluginSteps

    [Test]
    public async Task MigratePluginSteps_MatchingTypes_ShouldUpdateStepEventHandler()
    {
        // Arrange
        var oldTypeId = Guid.NewGuid();
        var newTypeId = Guid.NewGuid();
        var stepId = Guid.NewGuid();
        var messageId = Guid.NewGuid();

        var step = new SdkMessageProcessingStep
        {
            Id = stepId,
            EventHandler = new EntityReference(dataverse.PluginType.EntityLogicalName, oldTypeId),
            Name = "TestStep",
            SdkMessageId = new EntityReference(SdkMessage.EntityLogicalName, messageId)
        };
        _service.AddRange([step]);

        var outdatedAssemblies = new List<AssemblyContent>
        {
            new()
            {
                Id = Guid.NewGuid(),
                PluginTypes =
                {
                    new PluginType
                    {
                        Name = "MyPlugin",
                        TypeName = "MyNamespace.MyPlugin",
                        FriendlyName = "MyPlugin",
                        Id = oldTypeId,
                        PluginSteps = { new PluginStep { Id = stepId, Name = "TestStep" } }
                    }
                }
            }
        };

        var newAssembly = new Assembly
        {
            Name = "TestAssembly",
            Version = "1.1.0.0",
            Id = Guid.NewGuid(),
            PluginTypes = { new PluginType { Name = "MyPlugin", TypeName = "MyNamespace.MyPlugin", FriendlyName = "MyPlugin", Id = newTypeId } }
        };

        // Act
        _processor.MigratePluginSteps(outdatedAssemblies, newAssembly);

        // Assert
        var updatedStep = _service.Retrieve(SdkMessageProcessingStep.EntityLogicalName, stepId,
            new Microsoft.Xrm.Sdk.Query.ColumnSet(true)).ToEntity<SdkMessageProcessingStep>();
        await Assert.That(updatedStep.EventHandler!.Id).IsEqualTo(newTypeId);
    }

    [Test]
    public async Task MigratePluginSteps_TypeRemovedInNewVersion_ShouldSkipAndLogWarning()
    {
        // Arrange
        var oldTypeId = Guid.NewGuid();
        var stepId = Guid.NewGuid();

        var step = new SdkMessageProcessingStep
        {
            Id = stepId,
            EventHandler = new EntityReference(dataverse.PluginType.EntityLogicalName, oldTypeId),
            Name = "TestStep"
        };
        _service.AddRange([step]);

        var outdatedAssemblies = new List<AssemblyContent>
        {
            new()
            {
                Id = Guid.NewGuid(),
                PluginTypes =
                {
                    new PluginType
                    {
                        Name = "RemovedPlugin",
                        TypeName = "MyNamespace.RemovedPlugin",
                        FriendlyName = "RemovedPlugin",
                        Id = oldTypeId,
                        PluginSteps = { new PluginStep { Id = stepId, Name = "TestStep" } }
                    }
                }
            }
        };

        // New assembly does NOT contain "RemovedPlugin"
        var newAssembly = new Assembly
        {
            Name = "TestAssembly",
            Version = "1.1.0.0",
            Id = Guid.NewGuid(),
            PluginTypes = { new PluginType { Name = "OtherPlugin", TypeName = "MyNamespace.OtherPlugin", FriendlyName = "OtherPlugin", Id = Guid.NewGuid() } }
        };

        // Act
        _processor.MigratePluginSteps(outdatedAssemblies, newAssembly);

        // Assert - step should NOT be updated (still points to old type)
        var unchangedStep = _service.Retrieve(SdkMessageProcessingStep.EntityLogicalName, stepId,
            new Microsoft.Xrm.Sdk.Query.ColumnSet(true)).ToEntity<SdkMessageProcessingStep>();
        await Assert.That(unchangedStep.EventHandler!.Id).IsEqualTo(oldTypeId);

        // Warning should be logged
        var output = _console.Output;
        await Assert.That(output).Contains("removed in new version");
    }

    [Test]
    public async Task MigratePluginSteps_MultipleStepsOnSameType_ShouldMigrateAll()
    {
        // Arrange
        var oldTypeId = Guid.NewGuid();
        var newTypeId = Guid.NewGuid();
        var stepId1 = Guid.NewGuid();
        var stepId2 = Guid.NewGuid();

        _service.AddRange([
            new SdkMessageProcessingStep { Id = stepId1, EventHandler = new EntityReference(dataverse.PluginType.EntityLogicalName, oldTypeId), Name = "Step1" },
            new SdkMessageProcessingStep { Id = stepId2, EventHandler = new EntityReference(dataverse.PluginType.EntityLogicalName, oldTypeId), Name = "Step2" }
        ]);

        var outdatedAssemblies = new List<AssemblyContent>
        {
            new()
            {
                Id = Guid.NewGuid(),
                PluginTypes =
                {
                    new PluginType
                    {
                        Name = "MyPlugin", TypeName = "MyNamespace.MyPlugin", FriendlyName = "MyPlugin", Id = oldTypeId,
                        PluginSteps =
                        {
                            new PluginStep { Id = stepId1, Name = "Step1" },
                            new PluginStep { Id = stepId2, Name = "Step2" }
                        }
                    }
                }
            }
        };

        var newAssembly = new Assembly
        {
            Name = "TestAssembly", Version = "1.1.0.0", Id = Guid.NewGuid(),
            PluginTypes = { new PluginType { Name = "MyPlugin", TypeName = "MyNamespace.MyPlugin", FriendlyName = "MyPlugin", Id = newTypeId } }
        };

        // Act
        _processor.MigratePluginSteps(outdatedAssemblies, newAssembly);

        // Assert
        var s1 = _service.Retrieve(SdkMessageProcessingStep.EntityLogicalName, stepId1, new Microsoft.Xrm.Sdk.Query.ColumnSet(true)).ToEntity<SdkMessageProcessingStep>();
        var s2 = _service.Retrieve(SdkMessageProcessingStep.EntityLogicalName, stepId2, new Microsoft.Xrm.Sdk.Query.ColumnSet(true)).ToEntity<SdkMessageProcessingStep>();
        await Assert.That(s1.EventHandler!.Id).IsEqualTo(newTypeId);
        await Assert.That(s2.EventHandler!.Id).IsEqualTo(newTypeId);
    }

    #endregion

    #region MigrateCustomApis

    [Test]
    public async Task MigrateCustomApis_MatchingTypes_ShouldUpdateApiPluginTypeId()
    {
        // Arrange
        var oldTypeId = Guid.NewGuid();
        var newTypeId = Guid.NewGuid();
        var apiId = Guid.NewGuid();

        var customApi = new CustomAPI
        {
            Id = apiId,
            UniqueName = "my_custom_action",
            PluginTypeId = new EntityReference(dataverse.PluginType.EntityLogicalName, oldTypeId)
        };
        _service.AddRange([customApi]);

        var outdatedAssemblies = new List<AssemblyContent>
        {
            new()
            {
                Id = Guid.NewGuid(),
                PluginTypes =
                {
                    new PluginType
                    {
                        Name = "ActionPlugin", TypeName = "MyNamespace.ActionPlugin", FriendlyName = "ActionPlugin", Id = oldTypeId
                    }
                }
            }
        };

        var newAssembly = new Assembly
        {
            Name = "TestAssembly", Version = "1.1.0.0", Id = Guid.NewGuid(),
            PluginTypes = { new PluginType { Name = "ActionPlugin", TypeName = "MyNamespace.ActionPlugin", FriendlyName = "ActionPlugin", Id = newTypeId } }
        };

        // Act
        _processor.MigrateCustomApis(outdatedAssemblies, newAssembly);

        // Assert
        var updatedApi = _service.Retrieve(CustomAPI.EntityLogicalName, apiId,
            new Microsoft.Xrm.Sdk.Query.ColumnSet(true)).ToEntity<CustomAPI>();
        await Assert.That(updatedApi.PluginTypeId!.Id).IsEqualTo(newTypeId);
    }

    [Test]
    public async Task MigrateCustomApis_TypeRemovedInNewVersion_ShouldSkipAndLogWarning()
    {
        // Arrange
        var oldTypeId = Guid.NewGuid();
        var apiId = Guid.NewGuid();

        var customApi = new CustomAPI
        {
            Id = apiId,
            UniqueName = "my_action",
            PluginTypeId = new EntityReference(dataverse.PluginType.EntityLogicalName, oldTypeId)
        };
        _service.AddRange([customApi]);

        var outdatedAssemblies = new List<AssemblyContent>
        {
            new()
            {
                Id = Guid.NewGuid(),
                PluginTypes =
                {
                    new PluginType { Name = "Removed", TypeName = "MyNamespace.Removed", FriendlyName = "Removed", Id = oldTypeId }
                }
            }
        };

        var newAssembly = new Assembly
        {
            Name = "TestAssembly", Version = "1.1.0.0", Id = Guid.NewGuid(),
            PluginTypes = { new PluginType { Name = "Other", TypeName = "MyNamespace.Other", FriendlyName = "Other", Id = Guid.NewGuid() } }
        };

        // Act
        _processor.MigrateCustomApis(outdatedAssemblies, newAssembly);

        // Assert - API should NOT be updated
        var unchangedApi = _service.Retrieve(CustomAPI.EntityLogicalName, apiId,
            new Microsoft.Xrm.Sdk.Query.ColumnSet(true)).ToEntity<CustomAPI>();
        await Assert.That(unchangedApi.PluginTypeId!.Id).IsEqualTo(oldTypeId);

        // Warning logged
        var output = _console.Output;
        await Assert.That(output).Contains("cannot be migrated").Or.Contains("removed in new version");
    }

    [Test]
    public async Task MigrateCustomApis_MultipleApisOnSameType_ShouldMigrateAll()
    {
        // Arrange
        var oldTypeId = Guid.NewGuid();
        var newTypeId = Guid.NewGuid();
        var apiId1 = Guid.NewGuid();
        var apiId2 = Guid.NewGuid();

        _service.AddRange([
            new CustomAPI { Id = apiId1, UniqueName = "action_one", PluginTypeId = new EntityReference(dataverse.PluginType.EntityLogicalName, oldTypeId) },
            new CustomAPI { Id = apiId2, UniqueName = "action_two", PluginTypeId = new EntityReference(dataverse.PluginType.EntityLogicalName, oldTypeId) }
        ]);

        var outdatedAssemblies = new List<AssemblyContent>
        {
            new()
            {
                Id = Guid.NewGuid(),
                PluginTypes = { new PluginType { Name = "MultiApi", TypeName = "MyNamespace.MultiApi", FriendlyName = "MultiApi", Id = oldTypeId } }
            }
        };

        var newAssembly = new Assembly
        {
            Name = "TestAssembly", Version = "1.1.0.0", Id = Guid.NewGuid(),
            PluginTypes = { new PluginType { Name = "MultiApi", TypeName = "MyNamespace.MultiApi", FriendlyName = "MultiApi", Id = newTypeId } }
        };

        // Act
        _processor.MigrateCustomApis(outdatedAssemblies, newAssembly);

        // Assert
        var a1 = _service.Retrieve(CustomAPI.EntityLogicalName, apiId1, new Microsoft.Xrm.Sdk.Query.ColumnSet(true)).ToEntity<CustomAPI>();
        var a2 = _service.Retrieve(CustomAPI.EntityLogicalName, apiId2, new Microsoft.Xrm.Sdk.Query.ColumnSet(true)).ToEntity<CustomAPI>();
        await Assert.That(a1.PluginTypeId!.Id).IsEqualTo(newTypeId);
        await Assert.That(a2.PluginTypeId!.Id).IsEqualTo(newTypeId);
    }

    [Test]
    public async Task MigrateCustomApis_NoApisLinked_ShouldNotThrow()
    {
        // Arrange
        var oldTypeId = Guid.NewGuid();
        var newTypeId = Guid.NewGuid();

        var outdatedAssemblies = new List<AssemblyContent>
        {
            new()
            {
                Id = Guid.NewGuid(),
                PluginTypes = { new PluginType { Name = "NoApi", TypeName = "MyNamespace.NoApi", FriendlyName = "NoApi", Id = oldTypeId } }
            }
        };

        var newAssembly = new Assembly
        {
            Name = "TestAssembly", Version = "1.1.0.0", Id = Guid.NewGuid(),
            PluginTypes = { new PluginType { Name = "NoApi", TypeName = "MyNamespace.NoApi", FriendlyName = "NoApi", Id = newTypeId } }
        };

        // Act & Assert - should not throw
        _processor.MigrateCustomApis(outdatedAssemblies, newAssembly);
        await Assert.That(true).IsTrue();
    }

    #endregion
}
