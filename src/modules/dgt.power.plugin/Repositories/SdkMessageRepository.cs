// Copyright (c) DIGITALL Nature. All rights reserved
// DIGITALL Nature licenses this file to you under the Microsoft Public License.

using dgt.power.dataverse;
using Microsoft.PowerPlatform.Dataverse.Client;
using Microsoft.Xrm.Sdk.Query;

namespace dgt.power.plugin.Repositories;

/// <inheritdoc cref="ISdkMessageRepository" />
public sealed class SdkMessageRepository(IOrganizationServiceAsync2 service) : ISdkMessageRepository
{
    public async Task<ResolvedSdkMessage?> ResolveAsync(
        string messageName, string primaryEntityName, string secondaryEntityName, CancellationToken cancellationToken = default)
    {
        if (IsNone(primaryEntityName))
        {
            return await ResolveGlobalMessageAsync(messageName, cancellationToken);
        }

        var query = new QueryExpression(SdkMessageFilter.EntityLogicalName)
        {
            NoLock = true,
            ColumnSet = new ColumnSet(SdkMessageFilter.LogicalNames.SdkMessageFilterId, SdkMessageFilter.LogicalNames.SdkMessageId),
            Criteria = new FilterExpression
            {
                Conditions =
                {
                    new ConditionExpression(SdkMessageFilter.LogicalNames.PrimaryObjectTypeCode, ConditionOperator.Equal, primaryEntityName)
                }
            },
            LinkEntities =
            {
                new LinkEntity(
                    SdkMessageFilter.EntityLogicalName, SdkMessage.EntityLogicalName,
                    SdkMessageFilter.LogicalNames.SdkMessageId, SdkMessage.LogicalNames.SdkMessageId, JoinOperator.Inner)
                {
                    LinkCriteria = new FilterExpression
                    {
                        Conditions = { new ConditionExpression(SdkMessage.LogicalNames.Name, ConditionOperator.Equal, messageName) }
                    }
                }
            }
        };

        if (!IsNone(secondaryEntityName))
        {
            query.Criteria.Conditions.Add(
                new ConditionExpression(SdkMessageFilter.LogicalNames.SecondaryObjectTypeCode, ConditionOperator.Equal, secondaryEntityName));
        }

        var result = await service.RetrieveMultipleAsync(query, cancellationToken);
        var filter = result.Entities.FirstOrDefault()?.ToEntity<SdkMessageFilter>();
        if (filter is null)
        {
            return null;
        }

        return new ResolvedSdkMessage(filter.SdkMessageId!.Id, filter.Id);
    }

    private async Task<ResolvedSdkMessage?> ResolveGlobalMessageAsync(string messageName, CancellationToken cancellationToken)
    {
        var query = new QueryExpression(SdkMessage.EntityLogicalName)
        {
            NoLock = true,
            ColumnSet = new ColumnSet(SdkMessage.LogicalNames.SdkMessageId),
            Criteria = new FilterExpression
            {
                Conditions = { new ConditionExpression(SdkMessage.LogicalNames.Name, ConditionOperator.Equal, messageName) }
            }
        };

        var result = await service.RetrieveMultipleAsync(query, cancellationToken);
        var message = result.Entities.FirstOrDefault()?.ToEntity<SdkMessage>();
        return message is null ? null : new ResolvedSdkMessage(message.Id, null);
    }

    private static bool IsNone(string entityName) => string.IsNullOrEmpty(entityName) || entityName == "none";
}
