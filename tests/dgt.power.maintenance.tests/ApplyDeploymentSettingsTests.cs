// Copyright (c) DIGITALL Nature. All rights reserved
// DIGITALL Nature licenses this file to you under the Microsoft Public License.

using dgt.power.dataverse;
using dgt.power.maintenance.Logic;
using dgt.power.tests;
using dgt.power.tests.Extensions;
using Microsoft.Xrm.Sdk;

namespace dgt.power.maintenance.tests;

public class ApplyDeploymentSettingsTests(ITestOutputHelper testOutputHelper) : CommandTestsBase<ApplyDeploymentSettings, ApplyDeploymentSettings.Settings>(testOutputHelper)
{
    [Fact]
    private void EnvironmentDeploymentSettings_Should_SetMissingValue()
    {
        var environmentVariableDefinition = new EnvironmentVariableDefinition
        {
            Id = Guid.NewGuid(),
            SchemaName = "test_apply_deployment_settings",
            Type = new OptionSetValue(EnvironmentVariableDefinition.Options.Type.String),
            DefaultValue = "default",
        };

        var context = GetBuilder()
            .WithData(environmentVariableDefinition)
            .Build();

        var result = context.Execute(new ApplyDeploymentSettings.Settings
        {
            SettingsFile = GetResourcePath("environment.deploymentsettings.json"),
        });

        result.Should().Succeed();

        context.DataContext.EnvironmentVariableValueSet.Should().ContainSingle();
        var variableValue = context.DataContext.EnvironmentVariableValueSet.Single();
        variableValue.Value.Should().Be("value");
    }

    [Fact]
    private void EnvironmentDeploymentSettings_Should_UpdateExistingValue()
    {
        var environmentVariableDefinition = new EnvironmentVariableDefinition
        {
            Id = Guid.NewGuid(),
            SchemaName = "test_apply_deployment_settings",
            Type = new OptionSetValue(EnvironmentVariableDefinition.Options.Type.String),
            DefaultValue = "default",
        };
        var environmentVariableValue = new EnvironmentVariableValue
        {
            Id = Guid.NewGuid(),
            EnvironmentVariableDefinitionId = environmentVariableDefinition.ToEntityReference(),
            Value = "oldvalue",
        };

        var context = GetBuilder()
            .WithData(environmentVariableDefinition)
            .WithData(environmentVariableValue)
            .Build();

        var result = context.Execute(new ApplyDeploymentSettings.Settings
        {
            SettingsFile = GetResourcePath("environment.deploymentsettings.json"),
        });

        result.Should().Succeed();

        context.DataContext.EnvironmentVariableValueSet.Should().ContainSingle();
        var variableValue = context.DataContext.EnvironmentVariableValueSet.Single();
        variableValue.Id.Should().Be(environmentVariableValue.Id);
        variableValue.Value.Should().Be("value");
    }

    [Fact]
    private void EmptyEnvironmentValue_Should_ThrowException()
    {
        var context = GetBuilder()
            .Build();

        context.Invoking(c => c.Execute(new ApplyDeploymentSettings.Settings
        {
            SettingsFile = GetResourcePath("environment.emptyvalue.deploymentsettings.json"),
        })).Should().Throw<InvalidOperationException>();
    }

    [Fact]
    private void ConnectionReferenceDeploymentSettings_Should_SetMissingConnectionId()
    {
        var connectionReference = new Connectionreference
        {
            Id = Guid.NewGuid(),
            ConnectionReferenceLogicalName = "test_conn",
        };

        var context = GetBuilder()
            .WithData(connectionReference)
            .Build();

        var result = context.Execute(new ApplyDeploymentSettings.Settings
        {
            SettingsFile = GetResourcePath("connection.deploymentsettings.json"),
        });

        result.Should().Succeed();

        context.DataContext.ConnectionreferenceSet.Should().ContainSingle();
        var resultingConnectionReference = context.DataContext.ConnectionreferenceSet.Single();
        resultingConnectionReference.ConnectionId.Should().Be("some-connection-id");
    }

    [Fact]
    private void ConnectionReferenceDeploymentSettings_Should_UpdateExistingConnectionId()
    {
        var connectionReference = new Connectionreference
        {
            Id = Guid.NewGuid(),
            ConnectionReferenceLogicalName = "test_conn",
            ConnectionId = "old-connection-id",
        };

        var context = GetBuilder()
            .WithData(connectionReference)
            .Build();

        var result = context.Execute(new ApplyDeploymentSettings.Settings
        {
            SettingsFile = GetResourcePath("connection.deploymentsettings.json"),
        });

        result.Should().Succeed();

        context.DataContext.ConnectionreferenceSet.Should().ContainSingle();
        var resultingConnectionReference = context.DataContext.ConnectionreferenceSet.Single();
        resultingConnectionReference.ConnectionId.Should().Be("some-connection-id");
    }
}