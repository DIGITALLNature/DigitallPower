// Copyright (c) DIGITALL Nature. All rights reserved
// DIGITALL Nature licenses this file to you under the Microsoft Public License.

namespace dgt.power.plugin.Models;

public sealed record RemoteDataProvider(Guid Id, string DataSourceLogicalName, string Name, string? Description, IReadOnlyDictionary<DataProviderOperation, Guid> Handlers);
