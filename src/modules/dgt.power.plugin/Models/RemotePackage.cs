// SPDX-FileCopyrightText: DIGITALL Nature
// SPDX-License-Identifier: MS-RL

namespace dgt.power.plugin.Models;

/// <summary>
/// Minimal, already-fetched state of a plugin package in the target Dataverse environment, as
/// looked up by name.
/// </summary>
/// <param name="Id">Plugin package record id.</param>
/// <param name="PackageHash">SHA-256 hash of the package file stored in Dataverse.</param>
/// <param name="Version">Immutable package version stored in Dataverse.</param>
/// <param name="ManagedIdentityId">Linked managed identity id, if one is assigned.</param>
public sealed record RemotePackage(Guid Id, string? PackageHash, string Version, Guid? ManagedIdentityId = null);
