// Copyright (c) DIGITALL Nature. All rights reserved
// DIGITALL Nature licenses this file to you under the Microsoft Public License.

using dgt.power.profile.Base;
using dgt.power.profile.Commands;
using dgt.power.profile.tests.Base;
using dgt.power.tests.Extensions;

namespace dgt.power.profile.tests;

[NotInParallel("Serial_Profile_Tests")]
public class PurgeProfileCommandTests : ProfileTestsBase<PurgeProfileCommand, ProfileSettings>
{
    [Test]
    public async Task ShouldPurgeAllProfiles()
    {
        AddIdentity("some", "some");
        await Assert.That(GetIdentities().Infos).HasCount().EqualTo(1);

        await GetContext().Execute(new ProfileSettings()).Succeed();

        await Assert.That(GetIdentities().Infos).IsEmpty();
    }
}
