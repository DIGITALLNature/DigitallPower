// Copyright (c) DIGITALL Nature. All rights reserved
// DIGITALL Nature licenses this file to you under the Microsoft Public License.

using dgt.power.dataverse;
using dgt.power.plugin.Commands;
using Digitall.Dataverse.Testing;
using Microsoft.Xrm.Sdk;
using Microsoft.Xrm.Sdk.Metadata;
using dgt.power.tests;
using dgt.power.tests.Extensions;

namespace dgt.power.plugin.tests.Commands;

public class PluginStepConfigSetCommandTests : CommandTestsBase<PluginStepConfigSetCommand, PluginStepConfigSetSettings>
{
    private Guid _stepId;
    private Guid _messageId;
    private Guid _pluginTypeId;

    [Before(Test)]
    public void SetUp()
    {
        _stepId = Guid.NewGuid();
        _messageId = Guid.NewGuid();
        _pluginTypeId = Guid.NewGuid();
    }

    protected override CommandTestContextBuilder<PluginStepConfigSetCommand, PluginStepConfigSetSettings> GetBuilder()
    {
        return base.GetBuilder()
            .WithMetaData(GetSdkMessageMetadata())
            .WithMetaData(GetSdkMessageProcessingStepMetadata())
            .WithMetaData(GetSdkMessageProcessingStepSecureConfigMetadata())
            .WithMetaData(GetPluginTypeMetadata())
            .WithMetaData(GetSdkMessageFilterMetadata());
    }

    // ==================== VALIDATION TESTS ====================

    [Test]
    public async Task Validate_WithStepIdAndCompositeKey_ReturnsError()
    {
        var settings = new PluginStepConfigSetSettings
        {
            StepId = Guid.NewGuid(),
            PluginType = "TestPluginType",
            UnsecureConfig = "test-value"
        };
        var result = settings.Validate();

        await Assert.That(result).IsNotNull();
        await Assert.That(result.Message).IsNotNull();
        await Assert.That(result.Message).Contains("Cannot use --step-id together with composite key options");
    }

    [Test]
    public async Task Validate_WithBothUnsecureAndUnsecureFile_ReturnsError()
    {
        var settings = new PluginStepConfigSetSettings
        {
            StepId = Guid.NewGuid(),
            UnsecureConfig = "inline-value",
            UnsecureConfigFile = new FileInfo("/tmp/test.txt")
        };
        var result = settings.Validate();

        await Assert.That(result).IsNotNull();
        await Assert.That(result.Message).IsNotNull();
        await Assert.That(result.Message).Contains("Cannot use both --unsecure and --unsecure-file");
    }

    [Test]
    public async Task Validate_WithBothSecureAndSecureFile_ReturnsError()
    {
        var settings = new PluginStepConfigSetSettings
        {
            StepId = Guid.NewGuid(),
            SecureConfig = "inline-value",
            SecureConfigFile = new FileInfo("/tmp/test.txt")
        };
        var result = settings.Validate();

        await Assert.That(result).IsNotNull();
        await Assert.That(result.Message).IsNotNull();
        await Assert.That(result.Message).Contains("Cannot use both --secure and --secure-file");
    }

    [Test]
    public async Task Validate_WithNoStepResolution_ReturnsError()
    {
        var settings = new PluginStepConfigSetSettings
        {
            UnsecureConfig = "test-config-value"
        };
        var result = settings.Validate();

        await Assert.That(result).IsNotNull();
        await Assert.That(result.Message).IsNotNull();
        await Assert.That(result.Message).Contains("Either --step-id or composite key options");
    }

    [Test]
    public async Task Validate_WithNoConfigValue_ReturnsError()
    {
        var settings = new PluginStepConfigSetSettings
        {
            StepId = Guid.NewGuid()
        };
        var result = settings.Validate();

        await Assert.That(result).IsNotNull();
        await Assert.That(result.Message).IsNotNull();
        await Assert.That(result.Message).Contains("At least one of --unsecure, --unsecure-file, --secure, or --secure-file must be provided");
    }

    [Test]
    public async Task Validate_WithValidStepIdAndUnsecureConfig_Passes()
    {
        var settings = new PluginStepConfigSetSettings
        {
            StepId = Guid.NewGuid(),
            UnsecureConfig = "test-value"
        };
        var result = settings.Validate();

        await Assert.That(result).IsNotNull();
        await Assert.That(result.Message).IsNull();
    }

    [Test]
    public async Task Validate_WithValidCompositeKeyAndUnsecureConfig_Passes()
    {
        var settings = new PluginStepConfigSetSettings
        {
            PluginType = "TestPluginType",
            Message = "Create",
            Stage = 20,
            Entity = "account",
            UnsecureConfig = "test-value"
        };
        var result = settings.Validate();

        await Assert.That(result).IsNotNull();
        await Assert.That(result.Message).IsNull();
    }

    [Test]
    public async Task Validate_WithValidSecureConfigFile_Passes()
    {
        var configFile = WriteConfigurationArtifact("test-file-config");
        var settings = new PluginStepConfigSetSettings
        {
            StepId = Guid.NewGuid(),
            SecureConfigFile = new FileInfo(configFile.FullName)
        };
        var result = settings.Validate();

        await Assert.That(result).IsNotNull();
        await Assert.That(result.Message).IsNull();
    }

    [Test]
    public async Task Validate_WithNonExistentUnsecureConfigFile_ReturnsError()
    {
        var fileInfo = new FileInfo("/nonexistent/path/config.txt");
        var settings = new PluginStepConfigSetSettings
        {
            StepId = Guid.NewGuid(),
            UnsecureConfigFile = fileInfo
        };
        var result = settings.Validate();

        await Assert.That(result).IsNotNull();
        await Assert.That(result.Message).IsNotNull();
        await Assert.That(result.Message).Contains("File not found");
    }

    // ==================== EXECUTION TESTS ====================

    [Test]
    public async Task Execute_WithStepIdAndUnsecureConfig_UpdatesConfiguration()
    {
        var service = CreateFakeService();
        var entities = CreateTestStep(service, _stepId, _messageId, _pluginTypeId, "account");

        var context = GetBuilder()
            .WithData(entities)
            .WithAnsiConsole(TestConsole)
            .Build();

        var settings = new PluginStepConfigSetSettings
        {
            StepId = _stepId,
            UnsecureConfig = "updated-config-value"
        };

        await context.Execute(settings).Succeed();

        var updatedStep = context.GetById<SdkMessageProcessingStep>(_stepId);
        await Assert.That(updatedStep.Configuration).IsEqualTo("updated-config-value");
    }

    [Test]
    public async Task Execute_WithCompositeKeyAndUnsecureConfig_UpdatesConfiguration()
    {
        var service = CreateFakeService();
        var entities = CreateTestStep(service, _stepId, _messageId, _pluginTypeId, "account");

        var context = GetBuilder()
            .WithData(entities)
            .WithAnsiConsole(TestConsole)
            .Build();

        var settings = new PluginStepConfigSetSettings
        {
            PluginType = "TestPluginType",
            Message = "Create",
            Stage = 20,
            Entity = "account",
            UnsecureConfig = "updated-config-value"
        };

        await context.Execute(settings).Succeed();

        var updatedStep = context.GetById<SdkMessageProcessingStep>(_stepId);
        await Assert.That(updatedStep.Configuration).IsEqualTo("updated-config-value");
    }

    [Test]
    public async Task Execute_WithSecureConfig_CreatesSecureConfig()
    {
        var service = CreateFakeService();
        var entities = CreateTestStep(service, _stepId, _messageId, _pluginTypeId, "account");

        var context = GetBuilder()
            .WithData(entities)
            .WithAnsiConsole(TestConsole)
            .Build();

        var settings = new PluginStepConfigSetSettings
        {
            StepId = _stepId,
            SecureConfig = "secure-config-value"
        };

        await context.Execute(settings).Succeed();

        var updatedStep = context.GetById<SdkMessageProcessingStep>(_stepId);
        await Assert.That(updatedStep.SdkMessageProcessingStepSecureConfigId).IsNotNull();
    }

    [Test]
    public async Task Execute_WithUnsecureConfigFile_ReadsFileAndUpdatesConfiguration()
    {
        var service = CreateFakeService();
        var entities = CreateTestStep(service, _stepId, _messageId, _pluginTypeId, "account");

        var context = GetBuilder()
            .WithData(entities)
            .WithAnsiConsole(TestConsole)
            .Build();

        var configFile = WriteConfigurationArtifact("file-config-value");
        var settings = new PluginStepConfigSetSettings
        {
            StepId = _stepId,
            UnsecureConfigFile = new FileInfo(configFile.FullName)
        };

        await context.Execute(settings).Succeed();

        var updatedStep = context.GetById<SdkMessageProcessingStep>(_stepId);
        await Assert.That(updatedStep.Configuration).IsEqualTo("file-config-value");
    }

    [Test]
    public async Task Execute_WithMissingStep_ReturnsFalse()
    {
        var context = GetBuilder()
            .WithAnsiConsole(TestConsole)
            .Build();

        var settings = new PluginStepConfigSetSettings
        {
            PluginType = "NonExistentPluginType",
            Message = "Create",
            Stage = 20,
            Entity = "account",
            UnsecureConfig = "test-config-value"
        };

        await context.Execute(settings).Fail();
    }

    [Test]
    public async Task Execute_WithNoStepResolution_ReturnsFalse()
    {
        var context = GetBuilder()
            .WithAnsiConsole(TestConsole)
            .Build();

        var settings = new PluginStepConfigSetSettings
        {
            UnsecureConfig = "test-config-value"
        };

        await context.Execute(settings).Fail();
    }

    private static FakeOrganizationServiceAsync CreateFakeService()
    {
        var service = new FakeOrganizationServiceAsync();
        service.AddDefaultRequests();
        return service;
    }

    private static List<Entity> CreateTestStep(FakeOrganizationServiceAsync service, Guid stepId, Guid messageId, Guid pluginTypeId, string entityName)
    {
        var message = new SdkMessage(messageId) { Name = "Create" };
        var pluginType = new PluginType(pluginTypeId) { Name = "TestPluginType" };
        var filter = new SdkMessageFilter(Guid.NewGuid())
        {
            Attributes = { [SdkMessageFilter.LogicalNames.PrimaryObjectTypeCode] = entityName }
        };

        service.Create(message);
        service.Create(pluginType);
        service.Create(filter);

        var step = new SdkMessageProcessingStep(stepId)
        {
            Name = "TestStep",
            EventHandler = new EntityReference(PluginType.EntityLogicalName, pluginTypeId),
            SdkMessageId = new EntityReference(SdkMessage.EntityLogicalName, messageId),
            SdkMessageFilterId = new EntityReference(SdkMessageFilter.EntityLogicalName, filter.Id),
            Mode = new OptionSetValue(SdkMessageProcessingStep.Options.Mode.Synchronous),
            Stage = new OptionSetValue(SdkMessageProcessingStep.Options.Stage.PreOperation),
            Rank = 1
        };
        service.Create(step);

        return [message, pluginType, filter, step];
    }

    private static EntityMetadata GetSdkMessageMetadata()
    {
        return new EntityMetadata { LogicalName = SdkMessage.EntityLogicalName };
    }

    private static EntityMetadata GetSdkMessageProcessingStepMetadata()
    {
        return new EntityMetadata { LogicalName = SdkMessageProcessingStep.EntityLogicalName };
    }

    private static EntityMetadata GetSdkMessageProcessingStepSecureConfigMetadata()
    {
        return new EntityMetadata { LogicalName = SdkMessageProcessingStepSecureConfig.EntityLogicalName };
    }

    private static EntityMetadata GetPluginTypeMetadata()
    {
        return new EntityMetadata { LogicalName = PluginType.EntityLogicalName };
    }

    private static EntityMetadata GetSdkMessageFilterMetadata()
    {
        return new EntityMetadata { LogicalName = SdkMessageFilter.EntityLogicalName };
    }
}
