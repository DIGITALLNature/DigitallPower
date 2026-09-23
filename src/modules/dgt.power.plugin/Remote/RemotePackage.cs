// Copyright (c) DIGITALL Nature. All rights reserved
// DIGITALL Nature licenses this file to you under the Microsoft Public License.

namespace dgt.power.plugin.Remote;

/// <summary>
/// Minimal, already-fetched state of a plugin package in the target Dataverse environment, as
/// looked up by name.
/// </summary>
/// <param name="Id">Plugin package record id.</param>
/// <param name="PackageHash">SHA-256 hash of the package file stored in Dataverse.</param>
public sealed record RemotePackage(Guid Id, string? PackageHash);
