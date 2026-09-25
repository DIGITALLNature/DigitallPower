// Copyright (c) DIGITALL Nature. All rights reserved
// DIGITALL Nature licenses this file to you under the Microsoft Public License.

using Digitall.Dataverse.Testing;
using Digitall.Dataverse.Testing.OrganizationRequests;
using Microsoft.Crm.Sdk.Messages;
using Microsoft.Xrm.Sdk;

namespace dgt.power.plugin.tests.Repositories;

public sealed class InitializePackageFileDownloadExecutor(
    IReadOnlyDictionary<Guid, byte[]> packageFiles) : IOrganizationRequestFake
{
    public Type ForType => typeof(InitializeFileBlocksDownloadRequest);

    public OrganizationResponse Execute(
        OrganizationRequest organizationRequest,
        FakeOrganizationService fakeOrganizationService)
    {
        var request = (InitializeFileBlocksDownloadRequest)organizationRequest;
        if (!packageFiles.TryGetValue(request.Target.Id, out var packageFile))
        {
            throw new InvalidOperationException($"No package file is registered for {request.Target.Id}.");
        }

        return new InitializeFileBlocksDownloadResponse
        {
            [nameof(InitializeFileBlocksDownloadResponse.FileContinuationToken)] = request.Target.Id.ToString("D"),
            [nameof(InitializeFileBlocksDownloadResponse.FileSizeInBytes)] = (long)packageFile.Length,
            [nameof(InitializeFileBlocksDownloadResponse.IsChunkingSupported)] = true
        };
    }
}
