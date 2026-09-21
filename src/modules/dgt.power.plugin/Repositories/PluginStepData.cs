// Copyright (c) DIGITALL Nature. All rights reserved
// DIGITALL Nature licenses this file to you under the Microsoft Public License.

namespace dgt.power.plugin.Repositories;

/// <summary>
/// All Dataverse-facing fields needed to create/update a <c>sdkmessageprocessingstep</c>.
/// </summary>
public sealed record PluginStepData(
    string Name,
    Guid PluginTypeId,
    Guid MessageId,
    Guid? MessageFilterId,
    int Stage,
    int Mode,
    int? ExecutionOrder,
    IReadOnlyList<string>? FilterAttributes,
    string? Configuration);
