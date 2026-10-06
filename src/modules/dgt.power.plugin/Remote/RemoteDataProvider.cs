// Copyright (c) DIGITALL Nature. All rights reserved
// DIGITALL Nature licenses this file to you under the Microsoft Public License.

using dgt.power.plugin.Local;

namespace dgt.power.plugin.Remote;

public sealed record RemoteDataProvider(Guid Id, string DataSourceLogicalName, string Name, string? Description, IReadOnlyDictionary<DataProviderOperation, Guid> Handlers);
