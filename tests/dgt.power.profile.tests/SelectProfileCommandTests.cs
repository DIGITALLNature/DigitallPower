using dgt.power.profile.Commands;
using dgt.power.profile.tests.Base;
using dgt.power.tests.Extensions;
using Spectre.Console;

namespace dgt.power.profile.tests;

[Collection("Serial_Profile_Tests")]
public class SelectProfileCommandTests : ProfileTestsBase<SelectProfileCommand, NamedProfileSettings>
{
    private readonly ITestOutputHelper _testOutputHelper;

    public SelectProfileCommandTests(ITestOutputHelper testOutputHelper) : base(testOutputHelper)
    {
        _testOutputHelper = testOutputHelper;
    }

    [Fact]
    public void ShouldSetProfileAsCurrentOnSelection()
    {
        AnsiConsole.Record();
        const string identity = "some identity";
        const string connectionString = "con";
        AddIdentity(identity, connectionString);
        AddIdentity("something different", "something");
        GetIdentities().CurrentIdentity.Should().NotBe(identity);
        GetIdentities().CurrentConnectionString.Should().NotBe(connectionString);

        GetContext().Execute(new NamedProfileSettings
        {
            Name = identity
        }).Should().Succeed();

        _testOutputHelper.WriteLine(AnsiConsole.ExportText());
        GetIdentities().Current.Should().Be(identity);
        GetIdentities().CurrentConnectionString.Should().Be(connectionString);
    }

    [Fact]
    public void ShouldFailIfIdentityIsMissing()
    {
        const string identity = "some identity";

        GetContext().Execute(new NamedProfileSettings
        {
            Name = identity
        }).Should().Fail();
    }
}
