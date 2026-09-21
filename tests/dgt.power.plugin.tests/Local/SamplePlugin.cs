// Copyright (c) DIGITALL Nature. All rights reserved
// DIGITALL Nature licenses this file to you under the Microsoft Public License.

using Microsoft.Xrm.Sdk;

namespace dgt.power.plugin.tests.Local;

/// <summary>
/// A top-level (non-nested) <see cref="IPlugin"/> implementation so it is discoverable via
/// <see cref="System.Reflection.Assembly.GetTypes"/> when this test assembly itself is used as a
/// "plugin assembly" fixture (see <see cref="PluginPackageReaderTests"/>).
/// </summary>
public sealed class SamplePlugin : IPlugin
{
    public void Execute(IServiceProvider serviceProvider) => throw new NotSupportedException();
}
