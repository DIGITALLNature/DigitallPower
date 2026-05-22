// Copyright (c) DIGITALL Nature. All rights reserved
// DIGITALL Nature licenses this file to you under the Microsoft Public License.

using System.ServiceModel;
using dgt.power.dataverse;
using dgt.power.dto;
using dgt.power.import.Base;
using dgt.power.import.Logic;
using dgt.power.import.tests.Base;
using Microsoft.Xrm.Sdk;
using Microsoft.Xrm.Sdk.Messages;
#pragma warning disable CS8602

namespace dgt.power.import.tests;

public class SecureConfigImportTests : ImportTestBase<SecureConfigImport>
{
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
        await Assert.That(GetContext().Execute(new ImportVerb
            {
                FileName = WriteConfigurationArtifact(new SecureConfig()).Name,
                FileDir = ArtifactDirectory,
            }
        )).IsFalse();

    [Test]
    public async Task ShouldFailOnSecureConfigUpdate()
    {
        var securePluginConfig = new SdkMessageProcessingStepSecureConfig(Guid.NewGuid());
        var pluginStep = GetData();
        pluginStep.SdkMessageProcessingStepSecureConfigId = securePluginConfig.ToEntityReference();
        var secureConfig = new SecureConfig
        {
            PluginStep = pluginStep.Name!,
        };
        var context = GetBuilder()
            .WithData(pluginStep)
            .WithData(securePluginConfig)
            .WithExecutionMock<UpdateRequest>(_ =>
                throw new FaultException<OrganizationServiceFault>(new OrganizationServiceFault()))
            .Build();

        const string secret = "secret";
        await Assert.That(context.Execute(new ImportVerb
            {
                InlineData = secret,
                FileName = WriteConfigurationArtifact(secureConfig).Name,
                FileDir = ArtifactDirectory,
            }
        )).IsFalse();

        var postStep = context.GetById<SdkMessageProcessingStep>(pluginStep.Id);
        await Assert.That(postStep.SdkMessageProcessingStepSecureConfigId).IsNotNull();

        var createdSecureConfig = context
            .GetById<SdkMessageProcessingStepSecureConfig>(postStep.SdkMessageProcessingStepSecureConfigId.Id);
        await Assert.That(createdSecureConfig.SecureConfig).IsNull();
    }


    [Test]
    public async Task ShouldUpdateSecurecConfig()
    {
        var securePluginConfig = new SdkMessageProcessingStepSecureConfig(Guid.NewGuid());
        var pluginStep = GetData();
        pluginStep.SdkMessageProcessingStepSecureConfigId = securePluginConfig.ToEntityReference();
        var secureConfig = new SecureConfig
        {
            PluginStep = pluginStep.Name!,
        };
        var context = GetBuilder()
            .WithData(pluginStep)
            .WithData(securePluginConfig)
            .Build();

        const string secret = "secret";
        await Assert.That(context.Execute(new ImportVerb
            {
                InlineData = secret,
                FileName = WriteConfigurationArtifact(secureConfig).Name,
                FileDir = ArtifactDirectory,
            }
        )).IsTrue();

        var postStep = context.GetById<SdkMessageProcessingStep>(pluginStep.Id);
        await Assert.That(postStep.SdkMessageProcessingStepSecureConfigId).IsNotNull();

        var createdSecureConfig = context
            .GetById<SdkMessageProcessingStepSecureConfig>(postStep.SdkMessageProcessingStepSecureConfigId.Id);
        await Assert.That(createdSecureConfig.SecureConfig).IsEqualTo(secret);
    }

    [Test]
    public async Task ShouldCreateSecureConfig()
    {
        var pluginStep = GetData();
        var secureConfig = new SecureConfig
        {
            PluginStep = pluginStep.Name!,
            Data = "secret"
        };
        var context = GetBuilder()
            .WithData(pluginStep)
            .Build();

        await Assert.That(context.Execute(new ImportVerb
            {
                FileName = WriteConfigurationArtifact(secureConfig).Name,
                FileDir = ArtifactDirectory,
            }
        )).IsTrue();

        var postStep = context.GetById<SdkMessageProcessingStep>(pluginStep.Id);
        await Assert.That(postStep.SdkMessageProcessingStepSecureConfigId).IsNotNull();

        var createdSecureConfig = context
            .GetById<SdkMessageProcessingStepSecureConfig>(postStep.SdkMessageProcessingStepSecureConfigId.Id);
        await Assert.That(createdSecureConfig.SecureConfig).IsEqualTo(secureConfig.Data);
    }

    [Test]
    public async Task ShouldFailOnSecureConfigCreation()
    {
        var pluginStep = GetData();
        var secureConfig = new SecureConfig
        {
            PluginStep = pluginStep.Name!,
            Data = "secret"
        };
        var context = GetBuilder()
            .WithData(pluginStep)
            .WithExecutionMock<UpdateRequest>(_ =>
                throw new FaultException<OrganizationServiceFault>(new OrganizationServiceFault()))
            .Build();

        await Assert.That(context.Execute(new ImportVerb
            {
                FileName = WriteConfigurationArtifact(secureConfig).Name,
                FileDir = ArtifactDirectory,
            }
        )).IsFalse();

        var postStep = context.GetById<SdkMessageProcessingStep>(pluginStep.Id);
        await Assert.That(postStep.SdkMessageProcessingStepSecureConfigId).IsNull();

        await Assert.That(context.Get<SdkMessageProcessingStepSecureConfig>()).IsEmpty();
    }

    private static SdkMessageProcessingStep GetData()
    {
        var pluginStep = new SdkMessageProcessingStep(Guid.NewGuid())
        {
            Name = "Plugin Step"
        };
        return pluginStep;
    }
}
