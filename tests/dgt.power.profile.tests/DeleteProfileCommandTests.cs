// Copyright (c) DIGITALL Nature. All rights reserved
// DIGITALL Nature licenses this file to you under the Microsoft Public License.

using dgt.power.profile.Commands;
using dgt.power.profile.tests.Base;
using dgt.power.tests.Extensions;

namespace dgt.power.profile.tests;

[NotInParallel("Serial_Profile_Tests")]
public class DeleteProfileCommandTests : ProfileTestsBase<DeleteProfileCommand, NamedProfileSettings>
{
    [Test]
    public async Task ShouldDeleteProfile()
    {
        var settings = new NamedProfileSettings {Name = "existing"};
        AddIdentity(settings.Name, "connectionstring");

        await GetContext().Execute(settings).Succeed();

        await Assert.That(GetIdentities().Infos).IsEmpty();
    }
}
