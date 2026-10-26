// SPDX-FileCopyrightText: DIGITALL Nature
// SPDX-License-Identifier: MS-RL

using Digitall.Dataverse.Testing;
using Digitall.Dataverse.Testing.OrganizationRequests;
using Microsoft.Crm.Sdk.Messages;
using Microsoft.Xrm.Sdk;

namespace dgt.power.plugin.tests.Repositories;

public sealed class DownloadPackageFileBlockExecutor(
    IReadOnlyDictionary<Guid, byte[]> packageFiles) : IOrganizationRequestFake
{
    public Type ForType => typeof(DownloadBlockRequest);

    public OrganizationResponse Execute(
        OrganizationRequest organizationRequest,
        FakeOrganizationService fakeOrganizationService)
    {
        var request = (DownloadBlockRequest)organizationRequest;
        var packageId = Guid.Parse(request.FileContinuationToken);
        if (!packageFiles.TryGetValue(packageId, out var packageFile))
        {
            throw new InvalidOperationException($"No package file is registered for {packageId}.");
        }

        var data = packageFile
            .Skip((int)request.Offset)
            .Take((int)request.BlockLength)
            .ToArray();
        return new DownloadBlockResponse
        {
            [nameof(DownloadBlockResponse.Data)] = data
        };
    }
}
