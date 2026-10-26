// SPDX-FileCopyrightText: DIGITALL Nature
// SPDX-License-Identifier: MS-RL

namespace dgt.power.plugin.Deployment.Reconciliation;

public sealed record PluginDeploymentProgress(
    PluginDeploymentOperation Operation,
    string Resource,
    string Name);
