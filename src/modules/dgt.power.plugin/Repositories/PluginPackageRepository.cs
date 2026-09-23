// Copyright (c) DIGITALL Nature. All rights reserved
// DIGITALL Nature licenses this file to you under the Microsoft Public License.

using dgt.power.dataverse;
using Microsoft.Crm.Sdk.Messages;
using Microsoft.PowerPlatform.Dataverse.Client;
using Microsoft.Xrm.Sdk;
using Microsoft.Xrm.Sdk.Query;
using dgt.power.plugin.Remote;
using System.Security.Cryptography;

namespace dgt.power.plugin.Repositories;

/// <inheritdoc cref="IPluginPackageRepository" />
public sealed class PluginPackageRepository(IOrganizationServiceAsync2 service) : IPluginPackageRepository
{
    private const long DownloadBlockSize = 4 * 1024 * 1024;

    public async Task<RemotePackage?> FindByNameAsync(string name, CancellationToken cancellationToken = default)
    {
        var query = new QueryExpression(PluginPackage.EntityLogicalName)
        {
            NoLock = true,
            ColumnSet = new ColumnSet(PluginPackage.LogicalNames.PluginPackageId, PluginPackage.LogicalNames.Package),
            Criteria = new FilterExpression
            {
                Conditions = { new ConditionExpression(PluginPackage.LogicalNames.Name, ConditionOperator.Equal, name) }
            },
            Orders = { new OrderExpression(PluginPackage.LogicalNames.CreatedOn, OrderType.Descending) }
        };

        var result = await service.RetrieveMultipleAsync(query, cancellationToken);
        var entity = result.Entities.FirstOrDefault();
        if (entity is null)
        {
            return null;
        }

        var package = entity.ToEntity<PluginPackage>();
        var packageHash = package.Package is null
            ? null
            : await DownloadPackageHashAsync(entity.Id, cancellationToken);
        return new RemotePackage(entity.Id, packageHash);
    }

    public async Task<Guid> CreateAsync(string name, string version, string content, CancellationToken cancellationToken = default)
    {
        var package = new PluginPackage { Name = name, Version = version, Content = content };
        return await service.CreateAsync(package, cancellationToken);
    }

    public async Task UpdateContentAsync(Guid id, string content, CancellationToken cancellationToken = default)
    {
        var package = new PluginPackage(id) { Content = content };
        await service.UpdateAsync(package, cancellationToken);
    }

    private async Task<string> DownloadPackageHashAsync(Guid packageId, CancellationToken cancellationToken)
    {
        var initializeRequest = new InitializeFileBlocksDownloadRequest
        {
            Target = new EntityReference(PluginPackage.EntityLogicalName, packageId),
            FileAttributeName = PluginPackage.LogicalNames.Package
        };
        var initializeResponse = (InitializeFileBlocksDownloadResponse)await service.ExecuteAsync(
            initializeRequest,
            cancellationToken);

        using var hash = IncrementalHash.CreateHash(HashAlgorithmName.SHA256);
        var remainingBytes = initializeResponse.FileSizeInBytes;
        var offset = 0L;
        var blockSize = initializeResponse.IsChunkingSupported
            ? Math.Min(DownloadBlockSize, remainingBytes)
            : remainingBytes;

        while (remainingBytes > 0)
        {
            var blockLength = Math.Min(blockSize, remainingBytes);
            var downloadRequest = new DownloadBlockRequest
            {
                BlockLength = blockLength,
                FileContinuationToken = initializeResponse.FileContinuationToken,
                Offset = offset
            };
            var downloadResponse = (DownloadBlockResponse)await service.ExecuteAsync(
                downloadRequest,
                cancellationToken);
            if (downloadResponse.Data.Length == 0)
            {
                throw new InvalidOperationException("Dataverse returned an empty package file block.");
            }

            hash.AppendData(downloadResponse.Data);
            remainingBytes -= downloadResponse.Data.Length;
            offset += downloadResponse.Data.Length;
        }

        return Convert.ToHexString(hash.GetHashAndReset());
    }
}
