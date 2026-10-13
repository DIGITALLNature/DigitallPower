// Copyright (c) DIGITALL Nature. All rights reserved
// DIGITALL Nature licenses this file to you under the Microsoft Public License.

using dgt.power.plugin.Models;

namespace dgt.power.plugin.Deployment.Local;

public sealed record LocalDataProviderRegistration(
    string DataSourceSchemaName,
    DataProviderOperation Event,
    string? ProviderName,
    string? DataSourceDisplayName,
    string? DataSourcePluralName,
    string? Description);
