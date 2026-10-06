// Copyright (c) DIGITALL Nature. All rights reserved
// DIGITALL Nature licenses this file to you under the Microsoft Public License.

using dgt.power.plugin.Local;

namespace dgt.power.plugin.tests.Local;

public class DataProviderRegistrationGrouperTests
{
    private static LocalPluginType Type(string name, int operation, string? providerName = null, string schema = "dgt_Source", string? description = null) =>
        new(name, name, string.Empty, true, [])
        {
            DataProviders = [new LocalDataProviderRegistration(schema, (DataProviderOperation)operation, providerName, null, null, description)]
        };

    [Test]
    [Arguments(0, "retrieveplugin")]
    [Arguments(1, "retrievemultipleplugin")]
    [Arguments(2, "createplugin")]
    [Arguments(3, "updateplugin")]
    [Arguments(4, "deleteplugin")]
    public async Task HandlerField_SupportedEvent_ReturnsProviderField(int operation, string field)
    {
        await Assert.That(((DataProviderOperation)operation).HandlerField()).IsEqualTo(field);
    }

    [Test]
    public async Task Group_MetadataOnOneDeclaration_MergesAcrossClasses()
    {
        var provider = DataProviderRegistrationGrouper.Group([Type("Retrieve", 0), Type("RetrieveMultiple", 1, "Provider")]).Single();
        using (Assert.Multiple())
        {
            await Assert.That(provider.ProviderName).IsEqualTo("Provider");
            await Assert.That(provider.Handlers[DataProviderOperation.Retrieve]).IsEqualTo("Retrieve");
            await Assert.That(provider.Handlers[DataProviderOperation.RetrieveMultiple]).IsEqualTo("RetrieveMultiple");
            await Assert.That(provider.DataSourceLogicalName).IsEqualTo("dgt_source");
        }
    }

    [Test]
    public async Task Group_DifferentClassesClaimSameEvent_Throws()
    {
        await Assert.That(() => DataProviderRegistrationGrouper.Group([Type("First", 0, "Provider"), Type("Second", 0)])).ThrowsExactly<AssemblyException>();
    }

    [Test]
    public async Task Group_ConflictingMetadata_Throws()
    {
        await Assert.That(() => DataProviderRegistrationGrouper.Group([Type("First", 0, "Provider"), Type("Second", 1, "Other")])).ThrowsExactly<AssemblyException>();
    }

    [Test]
    public async Task Group_ProviderNameMissing_Throws()
    {
        await Assert.That(() => DataProviderRegistrationGrouper.Group([Type("First", 0)])).ThrowsExactly<AssemblyException>();
    }

    [Test]
    [Arguments(-1)]
    [Arguments(99)]
    public async Task Group_UnsupportedEvent_Throws(int operation)
    {
        await Assert.That(() => DataProviderRegistrationGrouper.Group([Type("First", operation, "Provider")])).ThrowsExactly<AssemblyException>();
    }

    [Test]
    [Arguments("")]
    [Arguments("ordinary")]
    [Arguments("dgt_<invalid>")]
    public async Task Group_InvalidSchemaName_Throws(string schema)
    {
        await Assert.That(() => DataProviderRegistrationGrouper.Group([Type("First", 0, "Provider", schema)])).ThrowsExactly<AssemblyException>();
    }

    [Test]
    public async Task Group_RepeatedSameHandler_CollapsesDuplicates()
    {
        var provider = DataProviderRegistrationGrouper.Group([Type("First", 0, "Provider"), Type("First", 0, schema: "dgt_source")]).Single();
        await Assert.That(provider.Handlers).Count().IsEqualTo(1);
    }
}
