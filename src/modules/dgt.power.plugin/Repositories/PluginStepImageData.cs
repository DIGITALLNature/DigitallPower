// Copyright (c) DIGITALL Nature. All rights reserved
// DIGITALL Nature licenses this file to you under the Microsoft Public License.

namespace dgt.power.plugin.Repositories;

/// <summary>
/// All Dataverse-facing fields needed to create a <c>sdkmessageprocessingstepimage</c>.
/// </summary>
public sealed record PluginStepImageData(
    Guid StepId,
    string Name,
    string EntityAlias,
    int ImageType,
    string MessagePropertyName,
    IReadOnlyList<string>? Attributes);
