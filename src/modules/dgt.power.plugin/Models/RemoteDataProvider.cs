// SPDX-FileCopyrightText: DIGITALL Nature
// SPDX-License-Identifier: MS-RL

using dgt.power.plugin.Deployment.Local;

namespace dgt.power.plugin.Models;

public sealed record RemoteDataProvider(Guid Id, string DataSourceLogicalName, string Name, string? Description, IReadOnlyDictionary<DataProviderOperation, Guid> Handlers);
