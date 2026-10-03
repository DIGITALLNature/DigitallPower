// Copyright (c) DIGITALL Nature. All rights reserved
// DIGITALL Nature licenses this file to you under the Microsoft Public License.

using dgt.power.dataverse;
using Microsoft.PowerPlatform.Dataverse.Client;
using Microsoft.Xrm.Sdk;
using Microsoft.Xrm.Sdk.Query;

namespace dgt.power.plugin.Repositories;

/// <inheritdoc cref="ISdkMessageProcessingStepSecureConfigRepository" />
public sealed class SdkMessageProcessingStepSecureConfigRepository(IOrganizationServiceAsync2 service)
    : ISdkMessageProcessingStepSecureConfigRepository
{
    public async Task<Guid?> GetIdByStepIdAsync(Guid stepId, CancellationToken cancellationToken = default)
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

    public async Task<Guid> CreateAsync(Guid stepId, string secureConfig, CancellationToken cancellationToken = default)
    {
        var secureConfigEntity = new SdkMessageProcessingStepSecureConfig
        {
            SecureConfig = secureConfig
        };

        var secureConfigId = await service.CreateAsync(secureConfigEntity, cancellationToken);

        // Link the step to the secure config
        var step = new SdkMessageProcessingStep(stepId)
        {
            SdkMessageProcessingStepSecureConfigId = new EntityReference(
                SdkMessageProcessingStepSecureConfig.EntityLogicalName,
                secureConfigId)
        };
        await service.UpdateAsync(step, cancellationToken);

        return secureConfigId;
    }

    public async Task UpdateAsync(Guid secureConfigId, string secureConfig, CancellationToken cancellationToken = default)
    {
        var secureConfigEntity = new SdkMessageProcessingStepSecureConfig(secureConfigId)
        {
            SecureConfig = secureConfig
        };
        await service.UpdateAsync(secureConfigEntity, cancellationToken);
    }
}
