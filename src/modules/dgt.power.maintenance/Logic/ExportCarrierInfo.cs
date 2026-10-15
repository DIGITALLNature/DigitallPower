// Copyright (c) DIGITALL Nature. All rights reserved
// DIGITALL Nature licenses this file to you under the Microsoft Public License.

using System.Diagnostics.CodeAnalysis;
using System.Globalization;
using dgt.power.common;
using dgt.power.common.Extensions;
using dgt.power.common.FileAccess;
using dgt.power.dataverse;
using dgt.power.maintenance.Model.Serialization;
using dgt.power.maintenance.Model.Settings;
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

    protected override async Task<bool> RunAsync([NotNull] CarrierInfoSettings settings, CancellationToken cancellationToken)
    {
        ArgumentNullException.ThrowIfNull(settings);

        var service = (IOrganizationService)await connection.GetServiceAsync();
        return Run(service, settings);
    }

    private bool Run(IOrganizationService service, CarrierInfoSettings settings)
    {
        var isSuccessfulOld = service.TryExecute<RetrieveEntityRequest, RetrieveEntityResponse>(new RetrieveEntityRequest
        {
            EntityFilters = EntityFilters.Entity,
            LogicalName = Ec4UCarrierEntityName
        }, out _);

        var isSuccessfulDgt = service.TryExecute<RetrieveEntityRequest, RetrieveEntityResponse>(new RetrieveEntityRequest
        {
            EntityFilters = EntityFilters.Entity,
            LogicalName = DgtCarrierEntityName
        }, out _);

        if (!isSuccessfulDgt && !isSuccessfulOld)
        {
            Console.MarkupLine($"[red]{Markup.Escape(ValidationErrorMessage)}[/]");
            return false;
        }

        List<CarrierRecord> carriers;
        if (isSuccessfulDgt)
        {
            carriers = RetrieveCarriers(service, DgtCarrierEntityName, "dgt_carrierid", "dgt_reference",
                "dgt_solutionversion", "dgt_solutionid", "dgt_solutionuniquename",
                "dgt_solutionfriendlyname", "dgt_transport_order_no");
        }
        else
        {
            carriers = RetrieveCarriers(service, Ec4UCarrierEntityName, "ec4u_carrierid", "ec4u_car_reference",
                "ec4u_car_solutionversion", "ec4u_car_solutionid", "ec4u_car_solutionuniquename",
                "ec4u_car_solutionfriendlyname", "ec4u_car_transport_order_no");
        }

        if (carriers.Count == 0)
        {
            Console.MarkupLine("[red]No active carrier[/]");
            return false;
        }

        using var dataContext = new DataContext(service);
        var carrierInfo = new List<Carrier>();
        foreach (var carrier in carriers)
        {
            if (!IsCarrierValid(carrier))
            {
                continue;
            }

            var solutionLookup = GetSolution(dataContext, carrier);
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
    private static List<CarrierRecord> RetrieveCarriers(IOrganizationService service, string entityName, string idField, string referenceField,
        string versionField, string solutionIdField, string uniqueNameField, string friendlyNameField, string orderField)
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

        return service.RetrieveMultiple(query).Entities
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

    private Solution? GetSolution(DataContext dataContext, CarrierRecord carrier)
    {
        var solutionId = Guid.Parse(carrier.SolutionId!, CultureInfo.InvariantCulture);
        var solution = dataContext.SolutionSet.Where(x => x.Id == solutionId)
            .Select(x => new Solution
            {
                Id = x.Id,
                SolutionId = x.SolutionId,
                FriendlyName = x.FriendlyName,
                UniqueName = x.UniqueName,
                Version = x.Version
            })
            .SingleOrDefault();

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
