// Copyright (c) DIGITALL Nature. All rights reserved
// DIGITALL Nature licenses this file to you under the Microsoft Public License.

namespace dgt.power.common.Logic;

/// <summary>
/// The non-secret values of an Azure DevOps service connection needed to open an
/// <see cref="AzureDevOpsFederatedIdentity"/> connection, as resolved from its name.
/// </summary>
#pragma warning disable CA1056, S3996, CA1054 // Url is intentionally a string, not Uri, to mirror AzureDevOpsFederatedIdentity/CreateConnectionSettings.
public sealed record ResolvedServiceConnection(string Url, string TenantId, string ClientId, string ServiceConnectionId);
#pragma warning restore CA1056, S3996, CA1054
