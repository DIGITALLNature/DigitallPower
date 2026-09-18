// Copyright (c) DIGITALL Nature. All rights reserved
// DIGITALL Nature licenses this file to you under the Microsoft Public License.

namespace dgt.power.plugin.Planning;

/// <summary>
/// A plugin type already registered on the target environment, as read from Dataverse.
/// </summary>
public sealed record RemotePluginType(Guid Id, string TypeName);
