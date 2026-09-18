// Copyright (c) DIGITALL Nature. All rights reserved
// DIGITALL Nature licenses this file to you under the Microsoft Public License.

namespace dgt.power.plugin.Planning;

/// <summary>
/// Decision on whether a plugin type from an outdated (superseded) assembly has a same-named
/// replacement on the newly registered assembly. When it does, Custom API links (always) and
/// steps (only when purging) should be migrated to the replacement before the outdated assembly is
/// deleted. When it does not, anything still referencing the outdated type is orphaned and must be
/// deleted along with it.
/// </summary>
public sealed record OutdatedTypeMigration(Guid OldTypeId, string TypeName, bool HasReplacement);
