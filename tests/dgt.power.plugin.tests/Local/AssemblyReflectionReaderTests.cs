// Copyright (c) DIGITALL Nature. All rights reserved
// DIGITALL Nature licenses this file to you under the Microsoft Public License.

using dgt.power.plugin.Local;
using Microsoft.Xrm.Sdk;
using Spectre.Console.Testing;
using dgt.registration;
using System.Reflection;
using System.Runtime.InteropServices;

namespace dgt.power.plugin.tests.Local;

public class AssemblyReflectionReaderTests
{
    private sealed class PlainPlugin : IPlugin
    {
        public void Execute(IServiceProvider serviceProvider) => throw new NotSupportedException();
    }

    [PluginRegistration("Create", 0, 40, PrimaryEntityName = "account", ExecutionOrder = 25)]
    private sealed class ExplicitOrderPlugin : IPlugin
    {
        public void Execute(IServiceProvider serviceProvider) => throw new NotSupportedException();
    }

    [Test]
    [System.Diagnostics.CodeAnalysis.SuppressMessage(
        "Reliability", "CA2000", Justification = "The test console remains in scope for output assertions.")]
    public async Task BuildPluginType_TypeWithoutRegistrationAttribute_HasRegistrationAttributeIsFalseAndHasNoSteps()
    {
        var console = new TestConsole();
        var reader = new AssemblyReflectionReader(console);

        var result = reader.BuildPluginType(typeof(PlainPlugin));

        using (Assert.Multiple())
        {
            await Assert.That(result.HasRegistrationAttribute).IsFalse();
            await Assert.That(result.Steps).IsEmpty();
            await Assert.That(result.CustomApi).IsEmpty();
        }
    }

    [Test]
    public async Task BuildPluginType_TypeWithoutRegistrationAttribute_PrintsHint()
    {
        using var console = new TestConsole();
        var reader = new AssemblyReflectionReader(console);

        reader.BuildPluginType(typeof(PlainPlugin));

        await Assert.That(console.Output).Contains(nameof(PlainPlugin));
    }

    [Test]
    public async Task BuildPluginType_ExplicitExecutionOrder_PreservesMetadataValue()
    {
        using var console = new TestConsole();
        var reader = new AssemblyReflectionReader(console);

        var result = reader.BuildPluginType(typeof(ExplicitOrderPlugin));

        await Assert.That(result.Steps[0].ExecutionOrder).IsEqualTo(25);
    }

    [Test]
    public async Task Read_ExternalRegistrationDependency_DiscoversRegistrationWithoutResolverPath()
    {
        using var console = new TestConsole();
        var resolverPaths = Directory.GetFiles(RuntimeEnvironment.GetRuntimeDirectory(), "*.dll")
            .Concat(Directory.GetFiles(Path.GetDirectoryName(typeof(AssemblyReflectionReader).Assembly.Location)!, "*.dll"));
        using var metadataLoadContext = new MetadataLoadContext(new PathAssemblyResolver(resolverPaths));

        var result = new AssemblyReflectionReader(console).Read(
            typeof(AssemblyReflectionReaderTests).Assembly.Location,
            metadataLoadContext);

        var pluginType = result!.PluginTypes.Single(type => type.TypeName == typeof(ExplicitOrderPlugin).FullName);
        using (Assert.Multiple())
        {
            await Assert.That(pluginType.HasRegistrationAttribute).IsTrue();
            await Assert.That(pluginType.Steps).Count().IsEqualTo(1);
            await Assert.That(pluginType.Steps[0].ExecutionOrder).IsEqualTo(25);
        }
    }

    [Test]
    [Arguments(0, "Retrieve")]
    [Arguments(1, "RetrieveMultiple")]
    [Arguments(2, "Create")]
    [Arguments(3, "Update")]
    [Arguments(4, "Delete")]
    public async Task MapDataProviderEventToMessage_ReturnsCorrectMessage(int eventValue, string expectedMessage)
    {
        var result = AssemblyReflectionReader.MapDataProviderEventToMessage(eventValue);

        await Assert.That(result).IsEqualTo(expectedMessage);
    }

    [Test]
    public async Task MapDataProviderEventToMessage_ThrowsForUnknownEvent()
    {
        await Assert.That(() => AssemblyReflectionReader.MapDataProviderEventToMessage(99))
            .ThrowsExactly<AssemblyException>();
    }

    [Test]
    [Arguments("Update", "Target")]
    [Arguments("update", "Target")]
    [Arguments("Delete", "Target")]
    [Arguments("SetState", "EntityMoniker")]
    [Arguments("SetStateDynamicEntity", "EntityMoniker")]
    [Arguments("Create", "Id")]
    [Arguments("Retrieve", "Id")]
    public async Task GetMessagePropertyName_ReturnsExpectedProperty(string messageName, string expected)
    {
        var result = AssemblyReflectionReader.GetMessagePropertyName(messageName);

        await Assert.That(result).IsEqualTo(expected);
    }

    [Test]
    [Arguments(0, "Synchronous")]
    [Arguments(1, "Asynchronous")]
    [Arguments(99, null)]
    public async Task Mode_MapsKnownValues(int mode, string? expected)
    {
        var result = AssemblyReflectionReader.Mode(mode);

        await Assert.That(result).IsEqualTo(expected);
    }

    [Test]
    [Arguments(10, "PreValidation")]
    [Arguments(20, "PreOperation")]
    [Arguments(30, "MainOperation")]
    [Arguments(40, "PostOperation")]
    [Arguments(99, null)]
    public async Task Stage_MapsKnownValues(int stage, string? expected)
    {
        var result = AssemblyReflectionReader.Stage(stage);

        await Assert.That(result).IsEqualTo(expected);
    }

    [Test]
    public async Task GetStepName_WithExecutionOrder_IncludesOrderInName()
    {
        var step = new LocalPluginStep("", 0, "Create", 40, "account", "none", null, 5, null, []);

        var result = AssemblyReflectionReader.GetStepName(step, "MyPlugin");

        await Assert.That(result).IsEqualTo("MyPlugin|account|Synchronous|PostOperation|Create|5");
    }

    [Test]
    public async Task GetStepName_WithoutExecutionOrder_OmitsOrderFromName()
    {
        var step = new LocalPluginStep("", 1, "Delete", 20, "none", "none", null, null, null, []);

        var result = AssemblyReflectionReader.GetStepName(step, "MyPlugin");

        await Assert.That(result).IsEqualTo("MyPlugin|none|Asynchronous|PreOperation|Delete");
    }
}
