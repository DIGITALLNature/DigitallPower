// Copyright (c) DIGITALL Nature. All rights reserved
// DIGITALL Nature licenses this file to you under the Microsoft Public License.

using dgt.power.dataverse;
using dgt.power.maintenance.Logic;
using dgt.power.tests;
using dgt.power.tests.Extensions;
using Microsoft.Xrm.Sdk;

namespace dgt.power.maintenance.tests;

public class UpdateWorkflowStateTests : CommandTestsBase<UpdateWorkflowState, UpdateWorkflowState.Settings>
{
    public UpdateWorkflowStateTests(ITestOutputHelper testOutputHelper) : base(testOutputHelper) { }

    [Theory]
    [InlineData("")]
    [InlineData("missing.json")]
    public void InvalidConfigPathsShouldFail(string config) =>
        GetBuilder().Build()
            .Execute(new UpdateWorkflowState.Settings
            {
                Config = GetResourcePath(config)
            }).Should().BeFalse();

    [Theory]
    [InlineData("empty.json")]
    [InlineData("simple.json")]
    public void ValidConfigShouldSucceed(string config) =>
        GetBuilder().Build()
            .Execute(new UpdateWorkflowState.Settings
            {
                Config = GetResourcePath(config)
            }).Should().BeTrue();

}
