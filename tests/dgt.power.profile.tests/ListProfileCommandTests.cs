// Copyright (c) DIGITALL Nature. All rights reserved
// DIGITALL Nature licenses this file to you under the Microsoft Public License.

using dgt.power.profile.Base;
using dgt.power.profile.Commands;
using dgt.power.profile.tests.Base;
using dgt.power.tests.Extensions;

namespace dgt.power.profile.tests;

[NotInParallel("Serial_Profile_Tests")]
public class ListProfileCommandTests : ProfileTestsBase<ListProfileCommand, ProfileSettings>
{
    [Test]
    public async Task ShouldListProfiles()
    {
        const string identity1 = "FIRST";
        AddIdentity(identity1, "connection");
        const string identity2 = "SECOND";
        AddIdentity(identity2, "connection");

        await GetContext().Execute(new ProfileSettings()).Succeed();

        var consoleOutput = TestConsole.Output;
        await Assert.That(consoleOutput).Contains(identity1);
        await Assert.That(consoleOutput).Contains(identity2);
    }
}
