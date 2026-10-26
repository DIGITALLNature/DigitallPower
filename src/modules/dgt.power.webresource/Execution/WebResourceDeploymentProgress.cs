// SPDX-FileCopyrightText: DIGITALL Nature
// SPDX-License-Identifier: MS-RL

namespace dgt.power.webresource.Execution;

public sealed record WebResourceDeploymentProgress(
    string Operation,
    string Resource,
    string Name);
