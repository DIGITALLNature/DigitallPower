// Copyright (c) DIGITALL Nature. All rights reserved
// DIGITALL Nature licenses this file to you under the Microsoft Public License.

using dgt.power.dataverse;

namespace dgt.power.plugin.Local;

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
