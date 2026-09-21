// Copyright (c) DIGITALL Nature. All rights reserved
// DIGITALL Nature licenses this file to you under the Microsoft Public License.

namespace dgt.power.plugin.Remote;

/// <summary>
/// Minimal, already-fetched state of a plugin package in the target Dataverse environment, as
/// looked up by name.
/// </summary>
/// <param name="Id">Plugin package record id.</param>
public sealed record RemotePackage(Guid Id);
