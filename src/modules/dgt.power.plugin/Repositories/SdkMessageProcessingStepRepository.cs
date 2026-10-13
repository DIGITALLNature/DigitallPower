// Copyright (c) DIGITALL Nature. All rights reserved
// DIGITALL Nature licenses this file to you under the Microsoft Public License.

using dgt.power.dataverse;
using dgt.power.plugin.Models;
using Microsoft.PowerPlatform.Dataverse.Client;
using Microsoft.Xrm.Sdk;
using Microsoft.Xrm.Sdk.Query;

namespace dgt.power.plugin.Repositories;

/// <inheritdoc cref="ISdkMessageProcessingStepRepository" />
public sealed class SdkMessageProcessingStepRepository(IOrganizationServiceAsync2 service) : ISdkMessageProcessingStepRepository
{
    private const int StateQueryChunkSize = 500;

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

        var result = await DataverseQueryReader.ReadAllAsync(service, query, cancellationToken);
        return result.Select(ToRemoteStep).ToList();
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

    public async Task<IReadOnlyList<RemotePluginStep>> FindByCompositeKeyAsync(
        string? pluginTypeName,
        string? messageName,
        int? stageValue,
        string? primaryEntityName,
        string? secondaryEntityName,
        int? executionOrder,
        CancellationToken cancellationToken = default)
    {
        var filter = new FilterExpression();
        var pluginTypeFilter = new FilterExpression();
        var messageFilter = new FilterExpression();

        if (pluginTypeName != null)
        {
            pluginTypeFilter.AddCondition(PluginType.LogicalNames.TypeName, ConditionOperator.Equal, pluginTypeName);
        }

        if (messageName != null)
        {
            messageFilter.AddCondition(SdkMessage.LogicalNames.Name, ConditionOperator.Equal, messageName);
        }

        if (stageValue.HasValue)
        {
            filter.AddCondition(SdkMessageProcessingStep.LogicalNames.Stage, ConditionOperator.Equal, stageValue.Value);
        }

        if (primaryEntityName != null)
        {
            filter.AddCondition(new ConditionExpression(
                SdkMessageFilter.LogicalNames.PrimaryObjectTypeCode, ConditionOperator.Equal, primaryEntityName)
            {
                EntityName = "filter"
            });
        }

        if (secondaryEntityName != null)
        {
            filter.AddCondition(new ConditionExpression(
                SdkMessageFilter.LogicalNames.SecondaryObjectTypeCode, ConditionOperator.Equal, secondaryEntityName)
            {
                EntityName = "filter"
            });
        }

        if (executionOrder.HasValue)
        {
            filter.AddCondition(SdkMessageProcessingStep.LogicalNames.Rank, ConditionOperator.Equal, executionOrder.Value);
        }

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
            Criteria = filter,
            LinkEntities =
            {
                new LinkEntity(
                    SdkMessageProcessingStep.EntityLogicalName, SdkMessage.EntityLogicalName,
                    SdkMessageProcessingStep.LogicalNames.SdkMessageId, SdkMessage.LogicalNames.SdkMessageId, JoinOperator.Inner)
                {
                    EntityAlias = "message",
                    LinkCriteria = messageFilter,
                    Columns = new ColumnSet(SdkMessage.LogicalNames.Name)
                },
                new LinkEntity(
                    SdkMessageProcessingStep.EntityLogicalName, PluginType.EntityLogicalName,
                    SdkMessageProcessingStep.LogicalNames.EventHandler, PluginType.LogicalNames.PluginTypeId, JoinOperator.Inner)
                {
                    EntityAlias = "plugintype",
                    LinkCriteria = pluginTypeFilter,
                    Columns = new ColumnSet(PluginType.LogicalNames.TypeName)
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

    public async Task UpdateConfigurationAsync(Guid stepId, string? configuration, CancellationToken cancellationToken = default)
    {
        var step = new SdkMessageProcessingStep(stepId)
        {
            Configuration = configuration
        };
        await service.UpdateAsync(step, cancellationToken);
    }

    public async Task EnableAsync(Guid stepId, CancellationToken cancellationToken = default)
    {
        var step = new SdkMessageProcessingStep(stepId)
        {
            StateCode = new OptionSetValue(SdkMessageProcessingStep.Options.StateCode.Enabled),
            StatusCode = new OptionSetValue(SdkMessageProcessingStep.Options.StatusCode.Enabled)
        };
        await service.UpdateAsync(step, cancellationToken);
    }

    public async Task DisableAsync(Guid stepId, CancellationToken cancellationToken = default)
    {
        var step = new SdkMessageProcessingStep(stepId)
        {
            StateCode = new OptionSetValue(SdkMessageProcessingStep.Options.StateCode.Disabled),
            StatusCode = new OptionSetValue(SdkMessageProcessingStep.Options.StatusCode.Disabled)
        };
        await service.UpdateAsync(step, cancellationToken);
    }

    public Task<IReadOnlyList<RemotePluginStepState>> ListStatesAsync(IReadOnlyCollection<Guid> stepIds, CancellationToken cancellationToken = default)
    {
        ArgumentNullException.ThrowIfNull(stepIds);
        return ListStatesCoreAsync(stepIds, cancellationToken);
    }

    private async Task<IReadOnlyList<RemotePluginStepState>> ListStatesCoreAsync(IReadOnlyCollection<Guid> stepIds, CancellationToken cancellationToken)
    {
        var states = new List<RemotePluginStepState>();

        // Chunked to keep the In-condition well below the Dataverse limits.
        foreach (var chunk in stepIds.Chunk(StateQueryChunkSize))
        {
            var query = new QueryExpression(SdkMessageProcessingStep.EntityLogicalName)
            {
                NoLock = true,
                ColumnSet = new ColumnSet(
                    SdkMessageProcessingStep.LogicalNames.SdkMessageProcessingStepId,
                    SdkMessageProcessingStep.LogicalNames.Name,
                    SdkMessageProcessingStep.LogicalNames.StateCode)
            };
            query.Criteria.AddCondition(SdkMessageProcessingStep.LogicalNames.SdkMessageProcessingStepId, ConditionOperator.In, chunk.Cast<object>().ToArray());

            var result = await service.RetrieveMultipleAsync(query, cancellationToken);
            states.AddRange(result.Entities.Select(ToStepState));
        }

        return states.OrderBy(state => state.Name, StringComparer.OrdinalIgnoreCase).ToList();
    }

    public async Task<RemotePluginStepState?> GetStateAsync(Guid stepId, CancellationToken cancellationToken = default)
    {
        var states = await ListStatesAsync([stepId], cancellationToken);
        return states.Count > 0 ? states[0] : null;
    }

    private static RemotePluginStepState ToStepState(Entity entity)
    {
        var step = entity.ToEntity<SdkMessageProcessingStep>();
        return new RemotePluginStepState(
            step.Id,
            step.Name ?? step.Id.ToString(),
            step.StateCode?.Value == SdkMessageProcessingStep.Options.StateCode.Enabled);
    }

    public async Task UpdateDescriptionAsync(Guid stepId, string description, CancellationToken cancellationToken = default)
    {
        var step = new SdkMessageProcessingStep(stepId)
        {
            Description = description
        };
        await service.UpdateAsync(step, cancellationToken);
    }
}
