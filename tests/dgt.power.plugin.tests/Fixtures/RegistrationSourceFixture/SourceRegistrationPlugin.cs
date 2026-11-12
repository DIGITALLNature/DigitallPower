// SPDX-FileCopyrightText: DIGITALL Nature
// SPDX-License-Identifier: MS-RL

using Digitall.Plugins.Registration;
using Microsoft.Xrm.Sdk;

namespace dgt.power.plugin.tests.Fixtures.RegistrationSourceFixture;

[PluginRegistration(PluginExecutionMode.Synchronous, "Create", PluginExecutionStage.PostOperation, PrimaryEntityName = "account", ExecutionOrder = 17)]
public sealed class SourceRegistrationPlugin : IPlugin
{
    public void Execute(IServiceProvider serviceProvider) => throw new NotSupportedException();
}
