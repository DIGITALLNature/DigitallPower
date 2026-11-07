// SPDX-FileCopyrightText: DIGITALL Nature
// SPDX-License-Identifier: MS-RL

using Microsoft.Xrm.Sdk;
using Digitall.Plugins.Registration;

namespace dgt.power.plugin.tests.Deployment.Local;

/// <summary>
/// A top-level (non-nested) <see cref="IPlugin"/> implementation so it is discoverable via
/// <see cref="System.Reflection.Assembly.GetTypes"/> when this test assembly itself is used as a
/// "plugin assembly" fixture (see <see cref="PluginPackageReaderTests"/>).
/// </summary>
[PluginRegistration(PluginExecutionMode.Synchronous, "Create", PluginExecutionStage.PostOperation, PrimaryEntityName = "account")]
public sealed class SamplePlugin : IPlugin
{
    public void Execute(IServiceProvider serviceProvider) => throw new NotSupportedException();
}
