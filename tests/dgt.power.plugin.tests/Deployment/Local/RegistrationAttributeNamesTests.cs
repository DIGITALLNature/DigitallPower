// SPDX-FileCopyrightText: DIGITALL Nature
// SPDX-License-Identifier: MS-RL

using dgt.power.plugin.Deployment.Local;
using Digitall.Plugins.Registration;

namespace dgt.power.plugin.tests.Deployment.Local;

/// <summary>
/// Pins the name-based matching contract in <see cref="RegistrationAttributeNames"/> to the real
/// Digitall.Plugins.Registration package, so a library change that renames the namespace, an
/// attribute type, or a constructor parameter/property the reader reads breaks the build instead of
/// silently going undetected. Constructor parameter order is deliberately not pinned: the reader
/// matches constructor arguments by parameter name.
/// </summary>
public class RegistrationAttributeNamesTests
{
    [Test]
    public async Task Namespace_And_AssemblyName_MatchRealLibrary()
    {
        using (Assert.Multiple())
        {
            await Assert.That(RegistrationAttributeNames.Namespace).IsEqualTo(typeof(PluginRegistrationAttribute).Namespace);
            await Assert.That(RegistrationAttributeNames.AssemblyName).IsEqualTo(typeof(PluginRegistrationAttribute).Assembly.GetName().Name);
        }
    }

    [Test]
    public async Task KnownAttributeNames_MatchRealLibraryTypes()
    {
        using (Assert.Multiple())
        {
            await Assert.That(RegistrationAttributeNames.PluginRegistration).IsEqualTo(nameof(PluginRegistrationAttribute));
            await Assert.That(RegistrationAttributeNames.CustomApiRegistration).IsEqualTo(nameof(CustomApiRegistrationAttribute));
            await Assert.That(RegistrationAttributeNames.CustomDataProviderRegistration).IsEqualTo(nameof(CustomDataProviderRegistrationAttribute));
        }
    }

    [Test]
    public async Task PluginRegistration_MembersReadByReflectionReader_ExistOnRealAttribute()
    {
        var type = typeof(PluginRegistrationAttribute);
        var ctorParameterNames = type.GetConstructors().Single().GetParameters().Select(p => p.Name).ToArray();
        var propertyNames = type.GetProperties().Select(p => p.Name).ToArray();

        using (Assert.Multiple())
        {
            await Assert.That(ctorParameterNames).Contains("mode");
            await Assert.That(ctorParameterNames).Contains("messageName");
            await Assert.That(ctorParameterNames).Contains("stage");
            await Assert.That(propertyNames).Contains("PrimaryEntityName");
            await Assert.That(propertyNames).Contains("SecondaryEntityName");
            await Assert.That(propertyNames).Contains("ExecutionOrder");
            await Assert.That(propertyNames).Contains("PreEntityImage");
            await Assert.That(propertyNames).Contains("PostEntityImage");
            await Assert.That(propertyNames).Contains("FilterAttributes");
            await Assert.That(propertyNames).Contains("PreEntityImageAttributes");
            await Assert.That(propertyNames).Contains("PostEntityImageAttributes");
        }
    }

    [Test]
    public async Task CustomDataProviderRegistration_MembersReadByReflectionReader_ExistOnRealAttribute()
    {
        var type = typeof(CustomDataProviderRegistrationAttribute);
        var ctorParameterNames = type.GetConstructors().Single().GetParameters().Select(p => p.Name).ToArray();
        var propertyNames = type.GetProperties().Select(p => p.Name).ToArray();

        using (Assert.Multiple())
        {
            await Assert.That(ctorParameterNames).Contains("dataSourceSchemaName");
            await Assert.That(ctorParameterNames).Contains("eventRegistration");
            await Assert.That(ctorParameterNames).Contains("providerName");
            await Assert.That(propertyNames).Contains("DataSourceDisplayName");
            await Assert.That(propertyNames).Contains("DataSourcePluralName");
            await Assert.That(propertyNames).Contains("Description");
        }
    }

    [Test]
    public async Task CustomApiRegistration_MembersReadByReflectionReader_ExistOnRealAttribute()
    {
        var ctorParameterNames = typeof(CustomApiRegistrationAttribute).GetConstructors().Single().GetParameters().Select(p => p.Name).ToArray();

        await Assert.That(ctorParameterNames).Contains("messageName");
    }
}
