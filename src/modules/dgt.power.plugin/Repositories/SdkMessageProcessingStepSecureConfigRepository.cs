// Copyright (c) DIGITALL Nature. All rights reserved
// DIGITALL Nature licenses this file to you under the Microsoft Public License.

using dgt.power.dataverse;
using Microsoft.PowerPlatform.Dataverse.Client;
using Microsoft.Xrm.Sdk.Messages;
using Microsoft.Xrm.Sdk.Query;

namespace dgt.power.plugin.Repositories;

/// <inheritdoc cref="ISdkMessageProcessingStepSecureConfigRepository" />
public sealed class SdkMessageProcessingStepSecureConfigRepository(IOrganizationServiceAsync2 service)
    : ISdkMessageProcessingStepSecureConfigRepository
{
    private async Task<Guid?> FindSecureConfigIdByStepIdAsync(Guid stepId, CancellationToken cancellationToken = default)
    {
        var query = new QueryExpression(SdkMessageProcessingStep.EntityLogicalName)
        {
            NoLock = true,
            ColumnSet = new ColumnSet(SdkMessageProcessingStep.LogicalNames.SdkMessageProcessingStepSecureConfigId),
            Criteria = new FilterExpression
            {
                Conditions =
                {
                    new ConditionExpression(
                        SdkMessageProcessingStep.LogicalNames.SdkMessageProcessingStepId,
                        ConditionOperator.Equal,
                        stepId)
                }
            }
        };

        var results = await service.RetrieveMultipleAsync(query, cancellationToken);
        var step = results.Entities.FirstOrDefault()?.ToEntity<SdkMessageProcessingStep>();
        return step?.SdkMessageProcessingStepSecureConfigId?.Id;
    }

    private async Task CreateAsync(Guid stepId, string secureConfig, CancellationToken cancellationToken = default)
    {
        var secureConfigEntity = new SdkMessageProcessingStepSecureConfig(Guid.NewGuid())
        {
            SecureConfig = secureConfig
        };

        var step = new SdkMessageProcessingStep(stepId)
        {
            SdkMessageProcessingStepSecureConfigId = secureConfigEntity.ToEntityReference()
        };

        await service.ExecuteAsync(new ExecuteTransactionRequest
        {
            ReturnResponses = false,
            Requests =
            [
                new CreateRequest { Target = secureConfigEntity },
                new UpdateRequest { Target = step }
            ]
        }, cancellationToken);
    }

    private async Task UpdateAsync(Guid secureConfigId, string secureConfig, CancellationToken cancellationToken = default)
    {
        var secureConfigEntity = new SdkMessageProcessingStepSecureConfig(secureConfigId)
        {
            SecureConfig = secureConfig
        };
        await service.UpdateAsync(secureConfigEntity, cancellationToken);
    }

    public async Task UpsertAsync(Guid stepId, string secureConfig, CancellationToken cancellationToken = default)
    {
        var secureConfigId = await FindSecureConfigIdByStepIdAsync(stepId, cancellationToken);
        if (!secureConfigId.HasValue)
        {
            await CreateAsync(stepId, secureConfig, cancellationToken);
            return;
        }

        await UpdateAsync(secureConfigId.Value, secureConfig, cancellationToken);
    }
}
