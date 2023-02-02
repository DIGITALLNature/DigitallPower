using System.ServiceModel;
using dgt.power.dataverse;
using dgt.power.dto;
using dgt.power.import.Base;
using dgt.power.import.Logic;
using dgt.power.import.tests.Base;
using FluentAssertions;
using Microsoft.Xrm.Sdk;
using Microsoft.Xrm.Sdk.Messages;
using Xunit.Abstractions;
#pragma warning disable CS8602

namespace dgt.power.import.tests;

public class SecureConfigImportTests : ImportTestBase<SecureConfigImport>
{
    public SecureConfigImportTests(ITestOutputHelper testOutputHelper) : base(testOutputHelper)
    {
    }
    
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
                FileName = WriteConfigurationArtifact(new SecureConfig()).Name,
                FileDir = ArtifactDirectory,
            }
        ).Should().BeFalse();

    [Fact]
    public void ShouldFailOnSecureConfigUpdate()
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
        context.Execute(new ImportVerb
            {
                InlineData = secret,
                FileName = WriteConfigurationArtifact(secureConfig).Name,
                FileDir = ArtifactDirectory,
            }
        ).Should().BeFalse();

        var postStep = context.GetById<SdkMessageProcessingStep>(pluginStep.Id);
        postStep.SdkMessageProcessingStepSecureConfigId.Should().NotBeNull();

        var createdSecureConfig = context
            .GetById<SdkMessageProcessingStepSecureConfig>(postStep.SdkMessageProcessingStepSecureConfigId.Id);
        createdSecureConfig.SecureConfig.Should().BeNull();
    }


    [Fact]
    public void ShouldUpdateSecurecConfig()
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
        context.Execute(new ImportVerb
            {
                InlineData = secret,
                FileName = WriteConfigurationArtifact(secureConfig).Name,
                FileDir = ArtifactDirectory,
            }
        ).Should().BeTrue();

        var postStep = context.GetById<SdkMessageProcessingStep>(pluginStep.Id);
        postStep.SdkMessageProcessingStepSecureConfigId.Should().NotBeNull();

        var createdSecureConfig = context
            .GetById<SdkMessageProcessingStepSecureConfig>(postStep.SdkMessageProcessingStepSecureConfigId.Id);
        createdSecureConfig.SecureConfig.Should().Be(secret);
    }

    [Fact]
    public void ShouldCreateSecureConfig()
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

        context.Execute(new ImportVerb
            {
                FileName = WriteConfigurationArtifact(secureConfig).Name,
                FileDir = ArtifactDirectory,
            }
        ).Should().BeTrue();

        var postStep = context.GetById<SdkMessageProcessingStep>(pluginStep.Id);
        postStep.SdkMessageProcessingStepSecureConfigId.Should().NotBeNull();

        var createdSecureConfig = context
            .GetById<SdkMessageProcessingStepSecureConfig>(postStep.SdkMessageProcessingStepSecureConfigId.Id);
        createdSecureConfig.SecureConfig.Should().Be(secureConfig.Data);
    }

    [Fact]
    public void ShouldFailOnSecureConfigCreation()
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

        context.Execute(new ImportVerb
            {
                FileName = WriteConfigurationArtifact(secureConfig).Name,
                FileDir = ArtifactDirectory,
            }
        ).Should().BeFalse();

        var postStep = context.GetById<SdkMessageProcessingStep>(pluginStep.Id);
        postStep.SdkMessageProcessingStepSecureConfigId.Should().BeNull();

        context.Get<SdkMessageProcessingStepSecureConfig>()
            .Should().BeEmpty();
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
