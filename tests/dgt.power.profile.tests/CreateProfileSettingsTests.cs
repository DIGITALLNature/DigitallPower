// Copyright (c) DIGITALL Nature. All rights reserved
// DIGITALL Nature licenses this file to you under the Microsoft Public License.

using dgt.power.profile.Commands;

namespace dgt.power.profile.tests;

[Collection("Serial_Profile_Tests")]
public class CreateProfileSettingsTests
{
    [Fact]
    public void ShouldBeInvalidWhenSettingInvalidSecurityProtocol()
    {
        var settings = new CreateProfileSettings
        {
            SecurityProtocol = "something"
        };

        settings.Validate().Successful.Should().BeFalse();
    }
}
