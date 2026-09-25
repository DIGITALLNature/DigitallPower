// Copyright (c) DIGITALL Nature. All rights reserved
// DIGITALL Nature licenses this file to you under the Microsoft Public License.

using dgt.power.dataverse;
using Microsoft.PowerPlatform.Dataverse.Client;
using Microsoft.Xrm.Sdk;
using Microsoft.Xrm.Sdk.Query;
using dgt.power.plugin.Remote;

namespace dgt.power.plugin.Repositories;

/// <inheritdoc cref="ISdkMessageProcessingStepRepository" />
public sealed class SdkMessageProcessingStepRepository(IOrganizationServiceAsync2 service) : ISdkMessageProcessingStepRepository
{
    public async Task<IReadOnlyList<RemotePluginStep>> ListByPluginTypeAsync(Guid pluginTypeId, CancellationToken cancellationToken = default)
    {
        var query = new QueryExpression(SdkMessageProcessingStep.EntityLogicalName)
        {
            NoLock = true,
            ColumnSet = new ColumnSet(
                SdkMessageProcessingStep.LogicalNames.SdkMessageProcessingStepId,
                SdkMessageProcessingStep.LogicalNames.Name,
                SdkMessageProcessingStep.LogicalNames.Mode,
                SdkMessageProcessingStep.LogicalNames.Stage,
                SdkMessageProcessingStep.LogicalNames.Rank,
                SdkMessageProcessingStep.LogicalNames.FilteringAttributes,
                SdkMessageProcessingStep.LogicalNames.SdkMessageId,
                SdkMessageProcessingStep.LogicalNames.SdkMessageFilterId),
            Criteria = new FilterExpression
            {
                Conditions = { new ConditionExpression(SdkMessageProcessingStep.LogicalNames.EventHandler, ConditionOperator.Equal, pluginTypeId) }
            },
            LinkEntities =
            {
                new LinkEntity(
                    SdkMessageProcessingStep.EntityLogicalName, SdkMessage.EntityLogicalName,
                    SdkMessageProcessingStep.LogicalNames.SdkMessageId, SdkMessage.LogicalNames.SdkMessageId, JoinOperator.Inner)
                {
                    EntityAlias = "message",
                    Columns = new ColumnSet(SdkMessage.LogicalNames.Name)
                },
                new LinkEntity(
                    SdkMessageProcessingStep.EntityLogicalName, SdkMessageFilter.EntityLogicalName,
                    SdkMessageProcessingStep.LogicalNames.SdkMessageFilterId, SdkMessageFilter.LogicalNames.SdkMessageFilterId, JoinOperator.LeftOuter)
                {
                    EntityAlias = "filter",
                    Columns = new ColumnSet(
                        SdkMessageFilter.LogicalNames.PrimaryObjectTypeCode,
                        SdkMessageFilter.LogicalNames.SecondaryObjectTypeCode)
                }
            }
        };

        var result = await service.RetrieveMultipleAsync(query, cancellationToken);
        return result.Entities.Select(ToRemoteStep).ToList();
    }

    private static RemotePluginStep ToRemoteStep(Entity entity)
    {
        var step = entity.ToEntity<SdkMessageProcessingStep>();
        var messageName = entity.GetAttributeValue<AliasedValue>($"message.{SdkMessage.LogicalNames.Name}")?.Value as string ?? string.Empty;
        var primaryEntityName = entity.GetAttributeValue<AliasedValue>($"filter.{SdkMessageFilter.LogicalNames.PrimaryObjectTypeCode}")?.Value as string ?? "none";
        var secondaryEntityName = entity.GetAttributeValue<AliasedValue>($"filter.{SdkMessageFilter.LogicalNames.SecondaryObjectTypeCode}")?.Value as string ?? "none";

        var filterAttributes = string.IsNullOrEmpty(step.FilteringAttributesField)
            ? null
            : step.FilteringAttributesField.Split(',', StringSplitOptions.RemoveEmptyEntries | StringSplitOptions.TrimEntries);

        return new RemotePluginStep(
            step.Id, step.Name!, step.Mode!.Value, messageName, step.Stage!.Value, primaryEntityName, secondaryEntityName,
            filterAttributes, step.Rank);
    }

    public Task<Guid> CreateAsync(PluginStepData data, CancellationToken cancellationToken = default)
    {
        ArgumentNullException.ThrowIfNull(data);
        return CreateCoreAsync(data, cancellationToken);
    }

    private async Task<Guid> CreateCoreAsync(PluginStepData data, CancellationToken cancellationToken)
    {
        var step = ToEntity(data);
        return await service.CreateAsync(step, cancellationToken);
    }

    public Task UpdateAsync(Guid id, PluginStepData data, CancellationToken cancellationToken = default)
    {
        ArgumentNullException.ThrowIfNull(data);
        return UpdateCoreAsync(id, data, cancellationToken);
    }

    private async Task UpdateCoreAsync(Guid id, PluginStepData data, CancellationToken cancellationToken)
    {
        var step = ToEntity(data);
        step.Id = id;
        await service.UpdateAsync(step, cancellationToken);
    }

    public async Task DeleteAsync(Guid id, CancellationToken cancellationToken = default) =>
        await service.DeleteAsync(SdkMessageProcessingStep.EntityLogicalName, id, cancellationToken);

    public async Task ReassignPluginTypeAsync(Guid stepId, Guid newPluginTypeId, CancellationToken cancellationToken = default)
    {
        var step = new SdkMessageProcessingStep(stepId)
        {
            EventHandler = new EntityReference(PluginType.EntityLogicalName, newPluginTypeId)
        };

        await service.UpdateAsync(step, cancellationToken);
    }

    private static SdkMessageProcessingStep ToEntity(PluginStepData data)
    {
        var step = new SdkMessageProcessingStep
        {
            Name = data.Name,
            EventHandler = new EntityReference(PluginType.EntityLogicalName, data.PluginTypeId),
            SdkMessageId = new EntityReference(SdkMessage.EntityLogicalName, data.MessageId),
            Stage = new OptionSetValue(data.Stage),
            Mode = new OptionSetValue(data.Mode),
            Rank = data.ExecutionOrder ?? 1,
            AsyncAutoDelete = data.Mode == SdkMessageProcessingStep.Options.Mode.Asynchronous,
            FilteringAttributesField = data.FilterAttributes is { Count: > 0 } ? string.Join(",", data.FilterAttributes) : null
        };

        if (data.MessageFilterId is { } messageFilterId)
        {
            step.SdkMessageFilterId = new EntityReference(SdkMessageFilter.EntityLogicalName, messageFilterId);
        }

        return step;
    }
}
