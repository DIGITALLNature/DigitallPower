// Copyright (c) DIGITALL Nature. All rights reserved
// DIGITALL Nature licenses this file to you under the Microsoft Public License.

namespace dgt.power.plugin.Repositories;

/// <summary>
/// A resolved SDK message, optionally scoped to a specific entity via a message filter.
/// </summary>
/// <param name="MessageId"><c>sdkmessage</c> id.</param>
/// <param name="MessageFilterId">
/// <c>sdkmessagefilter</c> id when the step targets a specific entity, or <see langword="null"/>
/// for an entity-agnostic (global) message.
/// </param>
public sealed record ResolvedSdkMessage(Guid MessageId, Guid? MessageFilterId);
