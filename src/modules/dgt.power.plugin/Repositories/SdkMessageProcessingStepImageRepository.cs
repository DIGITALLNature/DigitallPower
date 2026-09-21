// Copyright (c) DIGITALL Nature. All rights reserved
// DIGITALL Nature licenses this file to you under the Microsoft Public License.

using dgt.power.dataverse;
using Microsoft.PowerPlatform.Dataverse.Client;
using Microsoft.Xrm.Sdk;
using Microsoft.Xrm.Sdk.Query;
using dgt.power.plugin.Remote;

namespace dgt.power.plugin.Repositories;

/// <inheritdoc cref="ISdkMessageProcessingStepImageRepository" />
public sealed class SdkMessageProcessingStepImageRepository(IOrganizationServiceAsync2 service) : ISdkMessageProcessingStepImageRepository
{
    public async Task<IReadOnlyList<RemotePluginStepImage>> ListByStepAsync(Guid stepId, CancellationToken cancellationToken = default)
    {
        var query = new QueryExpression(SdkMessageProcessingStepImage.EntityLogicalName)
        {
            NoLock = true,
            ColumnSet = new ColumnSet(
                SdkMessageProcessingStepImage.LogicalNames.SdkMessageProcessingStepImageId,
                SdkMessageProcessingStepImage.LogicalNames.Name,
                SdkMessageProcessingStepImage.LogicalNames.ImageType,
                SdkMessageProcessingStepImage.LogicalNames.Attributes),
            Criteria = new FilterExpression
            {
                Conditions = { new ConditionExpression(SdkMessageProcessingStepImage.LogicalNames.SdkMessageProcessingStepId, ConditionOperator.Equal, stepId) }
            }
        };

        var result = await service.RetrieveMultipleAsync(query, cancellationToken);
        return result.Entities.Select(e => e.ToEntity<SdkMessageProcessingStepImage>()).Select(ToRemoteImage).ToList();
    }

    private static RemotePluginStepImage ToRemoteImage(SdkMessageProcessingStepImage image)
    {
        var attributes = string.IsNullOrEmpty(image.AttributesField)
            ? null
            : image.AttributesField.Split(',', StringSplitOptions.RemoveEmptyEntries | StringSplitOptions.TrimEntries);

        return new RemotePluginStepImage(image.Id, image.Name!, image.ImageType!.Value, attributes);
    }

    public Task<Guid> CreateAsync(PluginStepImageData data, CancellationToken cancellationToken = default)
    {
        ArgumentNullException.ThrowIfNull(data);
        return CreateCoreAsync(data, cancellationToken);
    }

    private async Task<Guid> CreateCoreAsync(PluginStepImageData data, CancellationToken cancellationToken)
    {
        var image = new SdkMessageProcessingStepImage
        {
            SdkMessageProcessingStepId = new EntityReference(SdkMessageProcessingStep.EntityLogicalName, data.StepId),
            Name = data.Name,
            EntityAlias = data.EntityAlias,
            ImageType = new OptionSetValue(data.ImageType),
            MessagePropertyName = data.MessagePropertyName,
            AttributesField = data.Attributes is { Count: > 0 } ? string.Join(",", data.Attributes) : null
        };

        return await service.CreateAsync(image, cancellationToken);
    }

    public async Task UpdateAsync(Guid id, IReadOnlyList<string>? attributes, CancellationToken cancellationToken = default)
    {
        var image = new SdkMessageProcessingStepImage(id)
        {
            AttributesField = attributes is { Count: > 0 } ? string.Join(",", attributes) : null
        };

        await service.UpdateAsync(image, cancellationToken);
    }

    public async Task DeleteAsync(Guid id, CancellationToken cancellationToken = default) =>
        await service.DeleteAsync(SdkMessageProcessingStepImage.EntityLogicalName, id, cancellationToken);
}
