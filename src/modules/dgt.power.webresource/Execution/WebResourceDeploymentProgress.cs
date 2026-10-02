// Copyright (c) DIGITALL Nature. All rights reserved
// DIGITALL Nature licenses this file to you under the Microsoft Public License.

namespace dgt.power.webresource.Execution;

public sealed record WebResourceDeploymentProgress(
    string Operation,
    string Resource,
    string Name);
