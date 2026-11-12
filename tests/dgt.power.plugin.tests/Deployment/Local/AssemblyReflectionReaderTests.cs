// SPDX-FileCopyrightText: DIGITALL Nature
// SPDX-License-Identifier: MS-RL

using System.Reflection;
using System.Reflection.Emit;
using System.Runtime.InteropServices;
using dgt.power.plugin.Deployment.Local;
using dgt.power.plugin.Exceptions;
using dgt.registration;
using Digitall.Plugins.Registration;
using Microsoft.Xrm.Sdk;
using Spectre.Console.Testing;

namespace dgt.power.plugin.tests.Deployment.Local;

public class AssemblyReflectionReaderTests
{
    private static AssemblyException CaptureBuildPluginTypeException(AssemblyReflectionReader reader, Type type)
    {
        try
        {
            reader.BuildPluginType(type);
        }
        catch (AssemblyException exception)
        {
            return exception;
        }

        throw new InvalidOperationException("Expected plugin type validation to fail.");
    }

    private sealed class PlainPlugin;

    [PluginRegistration(PluginExecutionMode.Synchronous, "Create", PluginExecutionStage.PostOperation, PrimaryEntityName = "account", ExecutionOrder = 25)]
    private sealed class ExplicitOrderPlugin : IPlugin
    {
        public void Execute(IServiceProvider serviceProvider) => throw new NotSupportedException();
    }

    [LegacyPluginRegistration]
    private sealed class LegacyRegistrationPlugin;

    [CustomDataProviderRegistration("dgt_Source", DataProviderEvent.Retrieve, "Provider",
        DataSourceDisplayName = "Source", DataSourcePluralName = "Sources", Description = "Provider description")]
    [CustomDataProviderRegistration("dgt_Source", DataProviderEvent.RetrieveMultiple, "Provider")]
    private sealed class ProviderPlugin;

    [CustomDataProviderRegistration("dgt_Source", (DataProviderEvent)(-1), "Provider")]
    internal sealed class NegativeEventProviderPlugin;

    [CustomDataProviderRegistration("dgt_Source", (DataProviderEvent)99, "Provider")]
    internal sealed class UnknownEventProviderPlugin;

    [CustomDataProviderRegistration(" ", DataProviderEvent.Retrieve, "Provider")]
    internal sealed class EmptySchemaProviderPlugin;

    [CustomDataProviderRegistration("dgt_Source", DataProviderEvent.Retrieve, " ")]
    internal sealed class EmptyNameProviderPlugin;

    [CustomDataProviderRegistration("dgt_Source", DataProviderEvent.Retrieve, null!)]
    internal sealed class NullNameProviderPlugin;

    [Test]
    [Arguments(false)]
    [Arguments(true)]
    public async Task BuildPluginType_ProviderConstructor_ProducesHandlersNotSteps(bool metadataOnly)
    {
        using var console = new TestConsole();
        using var context = MetadataLoadContextFactory.Create(Path.GetDirectoryName(typeof(ProviderPlugin).Assembly.Location)!);
        var type = metadataOnly
            ? context.LoadFromAssemblyPath(typeof(ProviderPlugin).Assembly.Location).GetType(typeof(ProviderPlugin).FullName!)!
            : typeof(ProviderPlugin);
        var reader = new AssemblyReflectionReader(console);
        var plugin = reader.BuildPluginType(type);
        using (Assert.Multiple())
        {
            await Assert.That(plugin.Steps).IsEmpty();
            await Assert.That(plugin.DataProviders).Count().IsEqualTo(2);
            await Assert.That(plugin.DataProviders[0].Event).IsEqualTo(DataProviderOperation.Retrieve);
            await Assert.That(plugin.DataProviders[1].Event).IsEqualTo(DataProviderOperation.RetrieveMultiple);
            await Assert.That(plugin.DataProviders[0].ProviderName).IsEqualTo("Provider");
            await Assert.That(plugin.DataProviders[1].ProviderName).IsEqualTo("Provider");
            await Assert.That(plugin.DataProviders[0].DataSourceSchemaName).IsEqualTo("dgt_Source");
            await Assert.That(plugin.DataProviders[0].DataSourceDisplayName).IsEqualTo("Source");
            await Assert.That(plugin.DataProviders[0].DataSourcePluralName).IsEqualTo("Sources");
            await Assert.That(plugin.DataProviders[0].Description).IsEqualTo("Provider description");
            await Assert.That(plugin.DataProviders[1].DataSourceDisplayName).IsNull();
            await Assert.That(plugin.DataProviders[1].DataSourcePluralName).IsNull();
            await Assert.That(plugin.DataProviders[1].Description).IsNull();
        }
    }

    [Test]
    [Arguments(typeof(NegativeEventProviderPlugin))]
    [Arguments(typeof(UnknownEventProviderPlugin))]
    [Arguments(typeof(EmptySchemaProviderPlugin))]
    [Arguments(typeof(EmptyNameProviderPlugin))]
    [Arguments(typeof(NullNameProviderPlugin))]
    public Task BuildPluginType_InvalidProviderArguments_Throws(Type pluginType)
    {
        using var console = new TestConsole();
        _ = CaptureBuildPluginTypeException(new AssemblyReflectionReader(console), pluginType);
        return Task.CompletedTask;
    }

    [Test]
    [System.Diagnostics.CodeAnalysis.SuppressMessage("Reliability", "CA2000", Justification = "The test console remains in scope for output assertions.")]
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
    public async Task EnsureAllPluginTypesDeclared_UndeclaredPluginType_Throws()
    {
        var pluginType = new LocalPluginType("Contoso.Plugin", "Contoso.Plugin", string.Empty, false, []);

        await Assert.That(() => AssemblyReflectionReader.EnsureAllPluginTypesDeclared([pluginType])).ThrowsExactly<AssemblyException>();
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
    public async Task BuildPluginType_LegacyRegistrationNamespace_IsIgnored()
    {
        using var console = new TestConsole();
        var reader = new AssemblyReflectionReader(console);

        var result = reader.BuildPluginType(typeof(LegacyRegistrationPlugin));

        using (Assert.Multiple())
        {
            await Assert.That(result.HasRegistrationAttribute).IsFalse();
            await Assert.That(result.Steps).IsEmpty();
        }
    }

    [ImpostorPluginRegistration]
    private sealed class ImpostorRegistrationPlugin;

    [Test]
    public async Task IsSupportedRegistrationAttribute_RealLibraryAttribute_ReturnsTrue()
    {
        var attribute = CustomAttributeData.GetCustomAttributes(typeof(ExplicitOrderPlugin))
            .Single(a => a.AttributeType.Name == RegistrationAttributeNames.PluginRegistration);

        await Assert.That(AssemblyReflectionReader.IsSupportedRegistrationAttribute(attribute, typeof(ExplicitOrderPlugin).Assembly)).IsTrue();
    }

    [Test]
    public async Task IsSupportedRegistrationAttribute_ForeignAssemblyInSupportedNamespace_ReturnsFalse()
    {
        var attribute = CustomAttributeData.GetCustomAttributes(typeof(ImpostorRegistrationPlugin))
            .Single(a => a.AttributeType.Name == nameof(ImpostorPluginRegistrationAttribute));

        await Assert.That(AssemblyReflectionReader.IsSupportedRegistrationAttribute(attribute, typeof(PluginRegistrationAttribute).Assembly)).IsFalse();
    }

    [Test]
    public async Task BuildPluginType_UnknownRegistrationAttributeName_IsIgnored()
    {
        using var console = new TestConsole();
        var reader = new AssemblyReflectionReader(console);

        var result = reader.BuildPluginType(typeof(ImpostorRegistrationPlugin));
        var attribute = CustomAttributeData.GetCustomAttributes(typeof(ImpostorRegistrationPlugin))
            .Single(a => a.AttributeType.Name == nameof(ImpostorPluginRegistrationAttribute));

        using (Assert.Multiple())
        {
            await Assert.That(AssemblyReflectionReader.IsSupportedRegistrationAttribute(attribute, typeof(ImpostorRegistrationPlugin).Assembly)).IsTrue();
            await Assert.That(result.HasRegistrationAttribute).IsFalse();
            await Assert.That(result.Steps).IsEmpty();
        }
    }

    [Test]
    public async Task BuildPluginType_SameNamedRegistrationAttributeFromForeignAssembly_IsIgnored()
    {
        var pluginType = EmitForeignPluginWithSameNamedAttribute();
        var attribute = CustomAttributeData.GetCustomAttributes(pluginType)
            .Single(a => a.AttributeType.Name == RegistrationAttributeNames.PluginRegistration);

        using var console = new TestConsole();
        var reader = new AssemblyReflectionReader(console);
        var result = reader.BuildPluginType(pluginType);

        using (Assert.Multiple())
        {
            await Assert.That(AssemblyReflectionReader.IsSupportedRegistrationAttribute(attribute, typeof(AssemblyReflectionReaderTests).Assembly)).IsFalse();
            await Assert.That(result.HasRegistrationAttribute).IsFalse();
            await Assert.That(result.Steps).IsEmpty();
        }
    }

    // Emits a foreign assembly that defines its own Digitall.Plugins.Registration.PluginRegistrationAttribute
    // with valid constructor metadata. Namespace and attribute name both match the supported contract, so only
    // the assembly-name check can reject it: without that check this fixture is detected as a registration.
    [System.Diagnostics.CodeAnalysis.SuppressMessage("Reliability", "S3011", Justification = "The emitted fixture intentionally calls the protected Attribute base constructor.")]
    private static Type EmitForeignPluginWithSameNamedAttribute()
    {
        var attributeAssembly = AssemblyBuilder.DefineDynamicAssembly(new AssemblyName("Foreign.Registration.Fixture"), AssemblyBuilderAccess.Run);
        var attributeModule = attributeAssembly.DefineDynamicModule("Foreign.Registration.Fixture");

        var attributeTypeBuilder = attributeModule.DefineType("Digitall.Plugins.Registration.PluginRegistrationAttribute",
            TypeAttributes.Public | TypeAttributes.Sealed | TypeAttributes.Class, typeof(Attribute));
        var attributeCtor = attributeTypeBuilder.DefineConstructor(MethodAttributes.Public, CallingConventions.Standard,
            [typeof(string), typeof(int), typeof(int)]);
        attributeCtor.DefineParameter(1, ParameterAttributes.None, "messageName");
        attributeCtor.DefineParameter(2, ParameterAttributes.None, "mode");
        attributeCtor.DefineParameter(3, ParameterAttributes.None, "stage");
        var il = attributeCtor.GetILGenerator();
        il.Emit(OpCodes.Ldarg_0);
        il.Emit(OpCodes.Call, typeof(Attribute).GetConstructor(BindingFlags.NonPublic | BindingFlags.Instance, null, Type.EmptyTypes, null)!);
        il.Emit(OpCodes.Ret);
        attributeTypeBuilder.CreateType();

        var pluginAssembly = AssemblyBuilder.DefineDynamicAssembly(new AssemblyName("Plugin.Registration.Fixture"), AssemblyBuilderAccess.Run);
        var pluginModule = pluginAssembly.DefineDynamicModule("Plugin.Registration.Fixture");
        var pluginTypeBuilder = pluginModule.DefineType("Plugin.Registration.Plugin",
            TypeAttributes.Public | TypeAttributes.Sealed | TypeAttributes.Class);
        pluginTypeBuilder.SetCustomAttribute(new CustomAttributeBuilder(attributeCtor, ["Create", 0, 40]));
        return pluginTypeBuilder.CreateType();
    }

    [Test]
    public async Task Read_ExternalRegistrationDependency_DiscoversRegistrationWithoutResolverPath()
    {
        using var console = new TestConsole();
        using var metadataLoadContext = MetadataLoadContextFactory.Create(Path.GetDirectoryName(typeof(AssemblyReflectionReaderTests).Assembly.Location)!);

        var result = new AssemblyReflectionReader(console).Read(typeof(AssemblyReflectionReaderTests).Assembly.Location, metadataLoadContext);

        var pluginType = result!.PluginTypes.Single(type => type.TypeName == typeof(ExplicitOrderPlugin).FullName);
        using (Assert.Multiple())
        {
            await Assert.That(pluginType.HasRegistrationAttribute).IsTrue();
            await Assert.That(pluginType.Steps).Count().IsEqualTo(1);
            await Assert.That(pluginType.Steps[0].ExecutionOrder).IsEqualTo(25);
        }
    }

    [Test]
    public async Task Read_SourcePackageRegistration_DiscoversRegistrationDefinedInPluginAssembly()
    {
        using var console = new TestConsole();
        var assemblyPath = Path.Combine(AppContext.BaseDirectory, "RegistrationSourceFixture.dll");
        using var metadataLoadContext = MetadataLoadContextFactory.Create(Path.GetDirectoryName(assemblyPath)!);

        var result = new AssemblyReflectionReader(console).Read(assemblyPath, metadataLoadContext);

        var pluginType = result!.PluginTypes.Single(type => type.TypeName == "dgt.power.plugin.tests.Fixtures.RegistrationSourceFixture.SourceRegistrationPlugin");
        using (Assert.Multiple())
        {
            await Assert.That(pluginType.HasRegistrationAttribute).IsTrue();
            await Assert.That(pluginType.Steps).Count().IsEqualTo(1);
            await Assert.That(pluginType.Steps[0].MessageName).IsEqualTo("Create");
            await Assert.That(pluginType.Steps[0].ExecutionOrder).IsEqualTo(17);
        }
    }

    [Test]
    public async Task Read_MissingDependency_ReturnsNoAssembly()
    {
        using var console = new TestConsole();
        var resolverPaths = Directory.GetFiles(RuntimeEnvironment.GetRuntimeDirectory(), "*.dll");
        using var metadataLoadContext = new MetadataLoadContext(new PathAssemblyResolver(resolverPaths));

        var result = new AssemblyReflectionReader(console).Read(typeof(AssemblyReflectionReaderTests).Assembly.Location, metadataLoadContext);

        await Assert.That(result).IsNull();
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
        var step = new LocalPluginStep("", 0, "Create", 40, "account", "none", null, 5, []);

        var result = AssemblyReflectionReader.GetStepName(step, "MyPlugin");

        await Assert.That(result).IsEqualTo("MyPlugin|account|Synchronous|PostOperation|Create|5");
    }

    [Test]
    public async Task GetStepName_WithoutExecutionOrder_OmitsOrderFromName()
    {
        var step = new LocalPluginStep("", 1, "Delete", 20, "none", "none", null, null, []);

        var result = AssemblyReflectionReader.GetStepName(step, "MyPlugin");

        await Assert.That(result).IsEqualTo("MyPlugin|none|Asynchronous|PreOperation|Delete");
    }
}
