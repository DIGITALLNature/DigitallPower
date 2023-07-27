// Copyright (c) DIGITALL Nature. All rights reserved
// DIGITALL Nature licenses this file to you under the Microsoft Public License.

using dgt.power.profile.Base;
using dgt.power.profile.Commands;
using dgt.power.profile.tests.Base;
using dgt.power.tests.Extensions;

namespace dgt.power.profile.tests;

[Collection("Serial_Profile_Tests")]
public class PurgeProfileCommandTests : ProfileTestsBase<PurgeProfileCommand, ProfileSettings>
{
    public PurgeProfileCommandTests(ITestOutputHelper testOutputHelper) : base(testOutputHelper)
    {
    }

    [Fact]
    public void ShouldPurgeAllProfiles()
    {
        AddIdentity("some", "some");
        GetIdentities().Keys.Should().HaveCount(1);

        GetContext().Execute(new ProfileSettings()).Should().Succeed();

        GetIdentities().Keys.Should().BeEmpty();
    }
}
