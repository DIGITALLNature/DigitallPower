// Copyright (c) DIGITALL Nature. All rights reserved
// DIGITALL Nature licenses this file to you under the Microsoft Public License.

namespace dgt.power.plugin.Local;

public sealed record LocalDataProviderRegistration(
    string DataSourceSchemaName,
    DataProviderOperation Event,
    string? ProviderName,
    string? DataSourceDisplayName,
    string? DataSourcePluralName,
    string? Description);
