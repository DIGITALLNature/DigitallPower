// Copyright (c) DIGITALL Nature. All rights reserved
// DIGITALL Nature licenses this file to you under the Microsoft Public License.

using dgt.power.maintenance.Base;
using dgt.power.maintenance.Logic;
using dgt.power.maintenance.tests.Base;
using dgt.power.tests;
using dgt.power.tests.FakeExecutor;
using Microsoft.Xrm.Sdk.Messages;

namespace dgt.power.maintenance.tests;

public class AutoNumberFormatActionTests : MaintenanceTestsBase<AutoNumberFormatAction>
{
    protected override CommandTestContext<AutoNumberFormatAction, MaintenanceVerb> GetContext()
    {
        return GetBuilder()
            .WithFakeMessageExecutor(new UpdateAttributeExecutor())
            .Build();
    }

    [Test]
    public async Task ShouldFailOnEmptyConfiguration() =>
        await Assert.That(GetContext()
            .Execute(new MaintenanceVerb
                {
                    Config = GetResourcePath("empty.json")
                }
            )).IsFalse();

    [Test]
    public async Task ShouldFailOnMissingConfiguration() =>
        await Assert.That(GetContext()
            .Execute(new MaintenanceVerb
                {
                    Config = "missing.json"
                }
            )).IsFalse();

    [Test]
    public async Task ShouldFailOnWrongConfiguration() =>
        await Assert.That(GetContext()
            .Execute(new MaintenanceVerb
                {
                    Config = GetResourcePath("wrong.json")
                }
            )).IsFalse();

    [Test]
    public async Task ShouldApplyAutoNumberFormatting() =>
        await Assert.That(GetContext()
            .Execute(new MaintenanceVerb
                {
                    Config = GetResourcePath("auto-number-formats.json")
                }
            )).IsTrue();
}
