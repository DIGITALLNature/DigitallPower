﻿using dgt.power.profile.Commands;
using dgt.power.profile.tests.Base;
using dgt.power.tests.Extensions;

namespace dgt.power.profile.tests;

[Collection("Serial_Profile_Tests")]
public class DeleteProfileCommandTests : ProfileTestsBase<DeleteProfileCommand, NamedProfileSettings>
{
    public DeleteProfileCommandTests(ITestOutputHelper testOutputHelper) : base(testOutputHelper)
    {
    }

    [Fact]
    public void ShouldDeleteProfile()
    {
        var settings = new NamedProfileSettings {Name = "existing"};
        AddIdentity(settings.Name, "connectionstring");

        GetContext().Execute(settings).Should().Succeed();

        GetIdentities().Keys.Should().NotContain(settings.Name);
    }
}
