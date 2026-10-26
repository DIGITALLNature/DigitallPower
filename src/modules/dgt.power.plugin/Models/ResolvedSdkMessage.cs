// SPDX-FileCopyrightText: DIGITALL Nature
// SPDX-License-Identifier: MS-RL

namespace dgt.power.plugin.Models;

/// <summary>
/// A resolved SDK message, optionally scoped to a specific entity via a message filter.
/// </summary>
/// <param name="MessageId"><c>sdkmessage</c> id.</param>
/// <param name="MessageFilterId">
/// <c>sdkmessagefilter</c> id when the step targets a specific entity, or <see langword="null"/>
/// for an entity-agnostic (global) message.
/// </param>
public sealed record ResolvedSdkMessage(Guid MessageId, Guid? MessageFilterId);
