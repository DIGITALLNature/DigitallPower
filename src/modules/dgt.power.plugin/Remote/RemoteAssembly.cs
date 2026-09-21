// Copyright (c) DIGITALL Nature. All rights reserved
// DIGITALL Nature licenses this file to you under the Microsoft Public License.

namespace dgt.power.plugin.Remote;

/// <summary>
/// Minimal, already-fetched state of a plugin assembly in the target Dataverse environment, as
/// looked up by name. Intentionally does not carry the full entity - only what planning needs.
/// </summary>
/// <param name="Id">Plugin assembly record id.</param>
/// <param name="Version">Currently registered version.</param>
/// <param name="PackageId">Owning plugin package id, when the assembly belongs to a package.</param>
public sealed record RemoteAssembly(Guid Id, Version Version, Guid? PackageId);
