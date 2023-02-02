using dgt.power.maintenance.Base;
using dgt.power.maintenance.Logic;
using dgt.power.maintenance.tests.Base;
using dgt.power.tests;
using dgt.power.tests.FakeExecutor;
using Microsoft.Xrm.Sdk.Messages;

namespace dgt.power.maintenance.tests;

public class AutoNumberFormatActionTests : MaintenanceTestsBase<AutoNumberFormatAction>
{
    public AutoNumberFormatActionTests(ITestOutputHelper testOutputHelper) : base(testOutputHelper)
    {
    }

    protected override CommandTestContext<AutoNumberFormatAction, MaintenanceVerb> GetContext()
    {
        return GetBuilder()
            .WithFakeMessageExecutor<UpdateAttributeRequest>(new UpdateAttributeExecutor())
            .Build();
    }

    [Fact]
    public void ShouldFailOnEmptyConfiguration() =>
        GetContext()
            .Execute(new MaintenanceVerb
                {
                    Config = GetResourcePath("empty.json")
                }
            ).Should().BeFalse();

    [Fact]
    public void ShouldFailOnMissingConfiguration() =>
        GetContext()
            .Execute(new MaintenanceVerb
                {
                    Config = "missing.json"
                }
            ).Should().BeFalse();

    [Fact]
    public void ShouldFailOnWrongConfiguration() =>
        GetContext()
            .Execute(new MaintenanceVerb
                {
                    Config = GetResourcePath("wrong.json")
                }
            ).Should().BeFalse();

    [Fact]
    public void ShouldApplyAutoNumberFormatting() =>
        GetContext()
            .Execute(new MaintenanceVerb
                {
                    Config = GetResourcePath("auto-number-formats.json")
                }
            ).Should().BeTrue();
}
