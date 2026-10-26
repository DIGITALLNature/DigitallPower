// SPDX-FileCopyrightText: DIGITALL Nature
// SPDX-License-Identifier: MS-RL

namespace dgt.power.plugin.Deployment.Local;

public sealed record LocalDataProvider(
    string DataSourceSchemaName,
    string ProviderName,
    string? DataSourceDisplayName,
    string? DataSourcePluralName,
    string? Description,
    IReadOnlyDictionary<DataProviderOperation, string> Handlers)
{
    public string DataSourceLogicalName => DataSourceSchemaName.ToLowerInvariant();
}
