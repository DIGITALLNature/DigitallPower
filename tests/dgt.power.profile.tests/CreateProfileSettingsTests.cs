// Copyright (c) DIGITALL Nature. All rights reserved
// DIGITALL Nature licenses this file to you under the Microsoft Public License.

using dgt.power.profile.Commands;

namespace dgt.power.profile.tests;

[NotInParallel("Serial_Profile_Tests")]
public class CreateProfileSettingsTests
{
    [Test]
    public async Task ShouldBeInvalidWhenSettingInvalidSecurityProtocol()
    {
        var settings = new CreateProfileSettings
        {
            SecurityProtocol = "something"
        };

        await Assert.That(settings.Validate().Successful).IsFalse();
    }
}
