// Copyright (c) DIGITALL Nature. All rights reserved
// DIGITALL Nature licenses this file to you under the Microsoft Public License.

namespace dgt.power.plugin.Execution;

public sealed record PluginDeploymentProgress(
    PluginDeploymentOperation Operation,
    string Resource,
    string Name);
