// Copyright (c) DIGITALL Nature. All rights reserved
// DIGITALL Nature licenses this file to you under the Microsoft Public License.

using dgt.power.maintenance.Logic;
using dgt.power.tests;

namespace dgt.power.maintenance.tests;

public class UpdateWorkflowStateTests : CommandTestsBase<UpdateWorkflowState, UpdateWorkflowStateSettings>
{
    [Test]
    [Arguments("")]
    [Arguments("missing.json")]
    public async Task InvalidConfigPathsShouldFail(string config) =>
        await Assert.That(GetBuilder().Build()
            .Execute(new UpdateWorkflowStateSettings
            {
                Config = GetResourcePath(config)
            })).IsFalse();

    [Test]
    [Arguments("empty.json")]
    [Arguments("simple.json")]
    public async Task ValidConfigShouldSucceed(string config) =>
        await Assert.That(GetBuilder().Build()
            .Execute(new UpdateWorkflowStateSettings
            {
                Config = GetResourcePath(config)
            })).IsTrue();

}
