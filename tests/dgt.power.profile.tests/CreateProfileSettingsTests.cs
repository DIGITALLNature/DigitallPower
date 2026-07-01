// Copyright (c) DIGITALL Nature. All rights reserved
// DIGITALL Nature licenses this file to you under the Microsoft Public License.

using dgt.power.profile.Commands;

namespace dgt.power.profile.tests;

[NotInParallel("Serial_Profile_Tests")]
public class CreateProfileSettingsTests
{
    [Test]
    public async Task ShouldBeValidWithMinimalSettings()
    {
        var settings = new CreateProfileSettings
        {
            Name = "dev",
            ConnectionString = "AuthType=OAuth;Url=https://contoso.crm4.dynamics.com"
        };

        await Assert.That(settings.Validate().Successful).IsTrue();
    }
}
