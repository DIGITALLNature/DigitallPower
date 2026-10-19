// Copyright (c) DIGITALL Nature. All rights reserved
// DIGITALL Nature licenses this file to you under the Microsoft Public License.

using System.Globalization;
using dgt.power.common;
using dgt.power.common.Extensions;
using dgt.power.common.FileAccess;
using dgt.power.dataverse;
using dgt.power.maintenance.Model.Serialization;
using dgt.power.maintenance.Model.Settings;
using Microsoft.PowerPlatform.Dataverse.Client;
using Microsoft.Xrm.Sdk;
using Microsoft.Xrm.Sdk.Messages;
using Microsoft.Xrm.Sdk.Metadata;
using Microsoft.Xrm.Sdk.Query;
using Spectre.Console;

namespace dgt.power.maintenance.Logic;

public class ExportCarrierInfo(
    ICommandSpanFactory spans,
    IDataverseConnection connection,
    IFileService fileService,
    IAnsiConsole console)
    : PowerCommand<CarrierInfoSettings>(spans, console)
{
    private const string Ec4UCarrierEntityName = "ec4u_carrier";
    private const string DgtCarrierEntityName = "dgt_carrier";

    private const string ValidationErrorMessage = $"Carrier entity '{Ec4UCarrierEntityName}' or  '{DgtCarrierEntityName}' isn't installed in the current environment.";

    protected override async Task<bool> RunAsync(CarrierInfoSettings settings, CancellationToken cancellationToken)
    {
        var service = await connection.GetServiceAsync();
        return await RunAsync(service, settings, cancellationToken);
    }

    private async Task<bool> RunAsync(
        IOrganizationServiceAsync2 service,
        CarrierInfoSettings settings,
        CancellationToken cancellationToken)
    {
        var (isSuccessfulOld, _) = await service.TryExecuteAsync<RetrieveEntityRequest, RetrieveEntityResponse>(
            new RetrieveEntityRequest
            {
                EntityFilters = EntityFilters.Entity,
                LogicalName = Ec4UCarrierEntityName
            },
            cancellationToken);
        var (isSuccessfulDgt, _) = await service.TryExecuteAsync<RetrieveEntityRequest, RetrieveEntityResponse>(
            new RetrieveEntityRequest
            {
                EntityFilters = EntityFilters.Entity,
                LogicalName = DgtCarrierEntityName
            },
            cancellationToken);

        if (!isSuccessfulDgt && !isSuccessfulOld)
        {
            Console.MarkupLine($"[red]{Markup.Escape(ValidationErrorMessage)}[/]");
            return false;
        }

        List<CarrierRecord> carriers;
        if (isSuccessfulDgt)
        {
            carriers = await RetrieveCarriersAsync(service, DgtCarrierEntityName, "dgt_carrierid", "dgt_reference",
                "dgt_solutionversion", "dgt_solutionid", "dgt_solutionuniquename",
                "dgt_solutionfriendlyname", "dgt_transport_order_no", cancellationToken);
        }
        else
        {
            carriers = await RetrieveCarriersAsync(service, Ec4UCarrierEntityName, "ec4u_carrierid", "ec4u_car_reference",
                "ec4u_car_solutionversion", "ec4u_car_solutionid", "ec4u_car_solutionuniquename",
                "ec4u_car_solutionfriendlyname", "ec4u_car_transport_order_no", cancellationToken);
        }

        if (carriers.Count == 0)
        {
            Console.MarkupLine("[red]No active carrier[/]");
            return false;
        }

        var carrierInfo = new List<Carrier>();
        foreach (var carrier in carriers)
        {
            cancellationToken.ThrowIfCancellationRequested();
            if (!IsCarrierValid(carrier))
            {
                continue;
            }

            var solutionLookup = await GetSolutionAsync(service, carrier, cancellationToken);
            if (solutionLookup == null)
            {
                continue;
            }

            carrierInfo.Add(ToCarrierInfo(solutionLookup, carrier));
        }

        fileService.ExportJsonFile(settings.FileDir, settings.FileName, carrierInfo.OrderBy(carrier => carrier.Order).ToList());

        return true;
    }

#pragma warning disable S107
    private static async Task<List<CarrierRecord>> RetrieveCarriersAsync(
        IOrganizationServiceAsync2 service,
        string entityName,
        string idField,
        string referenceField,
        string versionField,
        string solutionIdField,
        string uniqueNameField,
        string friendlyNameField,
        string orderField,
        CancellationToken cancellationToken)
#pragma warning restore S107
    {
        var query = new QueryExpression(entityName)
        {
            ColumnSet = new ColumnSet(idField, referenceField, versionField, solutionIdField,
                uniqueNameField, friendlyNameField, orderField, "statecode"),
            Criteria = new FilterExpression
            {
                Conditions = { new ConditionExpression("statecode", ConditionOperator.Equal, 0) }
            },
            Orders = { new OrderExpression(orderField, OrderType.Ascending) }
        };

        var entities = await service.RetrieveAllAsync(query, cancellationToken: cancellationToken);
        return entities
            .Select(e => new CarrierRecord
            {
                Id = e.Id,
                Reference = e.GetAttributeValue<string>(referenceField),
                SolutionVersion = e.GetAttributeValue<string>(versionField),
                SolutionId = e.GetAttributeValue<string>(solutionIdField),
                SolutionUniqueName = e.GetAttributeValue<string>(uniqueNameField),
                SolutionFriendlyName = e.GetAttributeValue<string>(friendlyNameField),
                TransportOrderNo = e.GetAttributeValue<int?>(orderField)
            })
            .ToList();
    }

    private static Carrier ToCarrierInfo(Solution solution, CarrierRecord carrier) => new()
    {
        UniqueName = solution.UniqueName!,
        FriendlyName = solution.FriendlyName!,
        SolutionId = solution.Id,
        CarrierId = carrier.Id,
        Order = carrier.TransportOrderNo ?? -1,
        Version = solution.Version!
    };

    private bool IsCarrierValid(CarrierRecord carrier)
    {
        if (!string.IsNullOrWhiteSpace(carrier.SolutionId) && Guid.TryParse(carrier.SolutionId, CultureInfo.InvariantCulture, out _))
        {
            return true;
        }

        Console.MarkupLine($"[yellow]Solution '{carrier.SolutionUniqueName}' has invalid solutionid '{carrier.SolutionId}'[/]");
        return false;
    }

    private async Task<Solution?> GetSolutionAsync(
        IOrganizationServiceAsync2 service,
        CarrierRecord carrier,
        CancellationToken cancellationToken)
    {
        var solutionId = Guid.Parse(carrier.SolutionId!, CultureInfo.InvariantCulture);
        var query = new QueryExpression(Solution.EntityLogicalName)
        {
            ColumnSet = new ColumnSet(
                Solution.LogicalNames.SolutionId,
                Solution.LogicalNames.FriendlyName,
                Solution.LogicalNames.UniqueName,
                Solution.LogicalNames.Version)
        };
        query.Criteria.AddCondition(Solution.LogicalNames.SolutionId, ConditionOperator.Equal, solutionId);
        var entities = await service.RetrieveAllAsync(query, cancellationToken: cancellationToken);
        var solution = entities.Select(static entity => entity.ToEntity<Solution>()).SingleOrDefault();

        Console.MarkupLine(solution != null ? $"found carrier solution [green]{solution.UniqueName}[/]" : $"[yellow]Solution carrier with ID '{solutionId}' not found[/]");

        return solution;
    }

    /// <summary>
    /// Internal record to hold carrier data retrieved via late-bound access.
    /// Both ec4u_carrier and dgt_carrier share the same logical structure.
    /// </summary>
    private sealed class CarrierRecord
    {
        public Guid Id { get; init; }
        public string? Reference { get; init; }
        public string? SolutionVersion { get; init; }
        public string? SolutionId { get; init; }
        public string? SolutionUniqueName { get; init; }
        public string? SolutionFriendlyName { get; init; }
        public int? TransportOrderNo { get; init; }
    }
}
