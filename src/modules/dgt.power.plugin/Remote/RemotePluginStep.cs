// Copyright (c) DIGITALL Nature. All rights reserved
// DIGITALL Nature licenses this file to you under the Microsoft Public License.

namespace dgt.power.plugin.Remote;

/// <summary>
/// A plugin step already registered on the target environment, as read from Dataverse.
/// </summary>
public sealed record RemotePluginStep(
    Guid Id,
    string Name,
    int Mode,
    string MessageName,
    int Stage,
    string PrimaryEntityName,
    string SecondaryEntityName,
    IReadOnlyList<string>? FilterAttributes,
    int? ExecutionOrder);
