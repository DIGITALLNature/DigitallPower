// SPDX-FileCopyrightText: DIGITALL Nature
// SPDX-License-Identifier: MS-RL

using dgt.power.plugin.Models;

namespace dgt.power.plugin.Repositories;

/// <summary>
/// Resolves the <c>sdkmessage</c>/<c>sdkmessagefilter</c> identifiers a plugin step needs, given
/// only the message and entity names declared locally. This lookup genuinely requires Dataverse -
/// message/filter ids are environment-specific and cannot be derived from the assembly alone.
/// </summary>
// Public repository contract; the deployment core uses the sealed implementations directly.
// ReSharper disable UnusedMemberInSuper.Global
public interface ISdkMessageRepository
{
    /// <summary>
    /// Resolves a message, optionally scoped to <paramref name="primaryEntityName"/> (and
    /// <paramref name="secondaryEntityName"/> for messages that support a secondary entity, such
    /// as <c>Associate</c>). Pass <c>"none"</c> (or an empty string) for an entity-agnostic
    /// message. Returns <see langword="null"/> when no matching message (or, for entity-scoped
    /// messages, no matching filter) exists.
    /// </summary>
    Task<ResolvedSdkMessage?> ResolveAsync(
        string messageName,
        string primaryEntityName,
        string secondaryEntityName,
        CancellationToken cancellationToken = default);
}
