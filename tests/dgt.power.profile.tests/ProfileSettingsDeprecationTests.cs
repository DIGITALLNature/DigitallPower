// Copyright (c) DIGITALL Nature. All rights reserved
// DIGITALL Nature licenses this file to you under the Microsoft Public License.

using dgt.power.common.Commands;
using dgt.power.profile.Base;

namespace dgt.power.profile.tests;

/// <summary>
/// Guards the '[[Deprecated]] profile' branch marker. <see cref="DeprecationInterceptor"/> (in
/// dgt.power) relies on <see cref="ProfileSettings"/> carrying a <see cref="DeprecatedCommandAttribute"/>
/// to print the deprecation warning for every command under the 'profile' branch - if this attribute is
/// ever removed by accident, the warning silently stops firing for all profile commands.
/// </summary>
public class ProfileSettingsDeprecationTests
{
    [Test]
    public async Task ProfileSettings_IsMarkedAsDeprecated_WithConnectionAsReplacement()
    {
        var attribute = typeof(ProfileSettings).GetCustomAttributes(typeof(DeprecatedCommandAttribute), inherit: true)
            .Cast<DeprecatedCommandAttribute>()
            .SingleOrDefault();

        await Assert.That(attribute).IsNotNull();
        await Assert.That(attribute!.UseInstead).IsEqualTo("connection");
    }
}
