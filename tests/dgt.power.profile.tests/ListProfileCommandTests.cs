// Copyright (c) DIGITALL Nature. All rights reserved
// DIGITALL Nature licenses this file to you under the Microsoft Public License.

using dgt.power.profile.Base;
using dgt.power.profile.Commands;
using dgt.power.profile.tests.Base;
using dgt.power.tests.Extensions;
using Spectre.Console;

namespace dgt.power.profile.tests;

[Collection("Serial_Profile_Tests")]
public class ListProfileCommandTests : ProfileTestsBase<ListProfileCommand, ProfileSettings>
{
    public ListProfileCommandTests(ITestOutputHelper testOutputHelper) : base(testOutputHelper)
    {
    }

    [Fact]
    public void ShouldListProfiles()
    {
        const string identity1 = "first";
        AddIdentity(identity1, "connection");
        const string identity2 = "second";
        AddIdentity(identity2, "connection");

        AnsiConsole.Record();

        GetContext().Execute(new ProfileSettings()).Should().Succeed();

        var consoleOutput = AnsiConsole.ExportText();
        consoleOutput.Should().Contain(identity1);
        consoleOutput.Should().Contain(identity2);
    }
}
