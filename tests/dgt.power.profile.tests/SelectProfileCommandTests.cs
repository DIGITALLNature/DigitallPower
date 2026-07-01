// Copyright (c) DIGITALL Nature. All rights reserved
// DIGITALL Nature licenses this file to you under the Microsoft Public License.

using dgt.power.profile.Commands;
using dgt.power.profile.tests.Base;
using dgt.power.tests.Extensions;

namespace dgt.power.profile.tests;

[NotInParallel("Serial_Profile_Tests")]
public class SelectProfileCommandTests : ProfileTestsBase<SelectProfileCommand, NamedProfileSettings>
{
    [Test]
    public async Task ShouldSetProfileAsCurrentOnSelection()
    {
        const string identity = "SOME IDENTITY";
        const string connectionString = "con";
        AddIdentity(identity, connectionString);
        AddIdentity("SOMETHING DIFFERENT", "something");
        await Assert.That(GetIdentities().Current).IsNotEqualTo(identity);
        await Assert.That(GetIdentities().CurrentConnectionString).IsNotEqualTo(connectionString);

        await GetContext().Execute(new NamedProfileSettings
        {
            Name = identity
        }).Succeed();

        await Assert.That(GetIdentities().Current).IsEqualTo(identity);
        await Assert.That(GetIdentities().CurrentConnectionString).IsEqualTo(connectionString);
    }

    [Test]
    public async Task ShouldFailIfIdentityIsMissing()
    {
        const string identity = "some identity";

        await GetContext().Execute(new NamedProfileSettings
        {
            Name = identity
        }).Fail();
    }
}
