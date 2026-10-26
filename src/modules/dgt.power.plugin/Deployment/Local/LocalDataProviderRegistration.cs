// SPDX-FileCopyrightText: DIGITALL Nature
// SPDX-License-Identifier: MS-RL

namespace dgt.power.plugin.Deployment.Local;

public sealed record LocalDataProviderRegistration(
    string DataSourceSchemaName,
    DataProviderOperation Event,
    string? ProviderName,
    string? DataSourceDisplayName,
    string? DataSourcePluralName,
    string? Description);
