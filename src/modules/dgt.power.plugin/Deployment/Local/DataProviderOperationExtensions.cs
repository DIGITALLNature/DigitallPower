// SPDX-FileCopyrightText: DIGITALL Nature
// SPDX-License-Identifier: MS-RL

using dgt.power.dataverse;
using dgt.power.plugin.Exceptions;

namespace dgt.power.plugin.Deployment.Local;

public static class DataProviderOperationExtensions
{
    public static string HandlerField(this DataProviderOperation operation) => operation switch
    {
        DataProviderOperation.Retrieve => EntityDataProvider.LogicalNames.RetrievePlugin,
        DataProviderOperation.RetrieveMultiple => EntityDataProvider.LogicalNames.RetrieveMultiplePlugin,
        DataProviderOperation.Create => EntityDataProvider.LogicalNames.CreatePlugin,
        DataProviderOperation.Update => EntityDataProvider.LogicalNames.UpdatePlugin,
        DataProviderOperation.Delete => EntityDataProvider.LogicalNames.DeletePlugin,
        _ => throw new AssemblyException($"Data provider Event '{(int)operation}' is unspecified or unsupported.")
    };
}
