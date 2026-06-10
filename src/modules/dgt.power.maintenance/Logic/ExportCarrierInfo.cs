// Copyright (c) DIGITALL Nature. All rights reserved
// DIGITALL Nature licenses this file to you under the Microsoft Public License.

using System.Diagnostics;
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
using Spectre.Console;
using Spectre.Console.Cli;

namespace dgt.power.maintenance.Logic;

public class ExportCarrierInfo(
    ITracer tracer,
    IOrganizationService connection,
    IConfigResolver configResolver,
    IFileService fileService,
    IAnsiConsole console)
    : PowerLogic<CarrierInfoSettings>(tracer, connection, configResolver, console)
{
    public static readonly string ValidationErrorMessage = $"Carrier entity '{Ec4uCarrier.EntityLogicalName}' or  '{DgtCarrier.EntityLogicalName}' isn't installed in the current environment.";

    protected override ValidationResult Validate(CommandContext context, CarrierInfoSettings settings)
    {
        var isSuccessfulDgt = Connection.TryExecute<RetrieveEntityRequest, RetrieveEntityResponse>(new RetrieveEntityRequest
        {
            EntityFilters = EntityFilters.Entity,
            LogicalName = DgtCarrier.EntityLogicalName
        }, out _);

        var isSuccessfulEc4U = !isSuccessfulDgt && Connection.TryExecute<RetrieveEntityRequest, RetrieveEntityResponse>(new RetrieveEntityRequest
        {
            EntityFilters = EntityFilters.Entity,
            LogicalName = Ec4uCarrier.EntityLogicalName
        }, out _);

        return (isSuccessfulDgt || isSuccessfulEc4U)
            ? base.Validate(context, settings)
            : ValidationResult.Error(ValidationErrorMessage);
    }

    protected override bool Invoke(CarrierInfoSettings settings)
    {
        Debug.Assert(settings != null, nameof(settings) + " != null");
        Tracer.Start(this);

        using var dataContext = new DataContext(Connection);

        var isSuccessfulOld = Connection.TryExecute<RetrieveEntityRequest, RetrieveEntityResponse>(new RetrieveEntityRequest
        {
            EntityFilters = EntityFilters.Entity,
            LogicalName = Ec4uCarrier.EntityLogicalName
        }, out _);

        var isSuccessfulDgt = Connection.TryExecute<RetrieveEntityRequest, RetrieveEntityResponse>(new RetrieveEntityRequest
        {
            EntityFilters = EntityFilters.Entity,
            LogicalName = DgtCarrier.EntityLogicalName
        }, out _);

        var carriers = new List<Ec4uCarrier>();
        if (isSuccessfulOld)
        {
            carriers = dataContext.Ec4uCarrierSet
                .Where(x => x.Statecode != null && x.Statecode.Value == Ec4uCarrier.Options.Statecode.Active)
                .OrderBy(x => x.Ec4uCarTransportOrderNo)
                .Select(x => new Ec4uCarrier
                {
                    Id = x.Id,
                    Ec4uCarrierId = x.Ec4uCarrierId,
                    Ec4uCarReference = x.Ec4uCarReference,
                    Ec4uCarSolutionversion = x.Ec4uCarSolutionversion,
                    Ec4uCarSolutionid = x.Ec4uCarSolutionid,
                    Ec4uCarSolutionuniquename = x.Ec4uCarSolutionuniquename,
                    Ec4uCarSolutionfriendlyname = x.Ec4uCarSolutionfriendlyname,
                    Ec4uCarTransportOrderNo = x.Ec4uCarTransportOrderNo
                })
                .ToList();
        }

        if (isSuccessfulDgt)
        {
            carriers = dataContext.DgtCarrierSet
                .Where(x => x.Statecode.Value == Ec4uCarrier.Options.Statecode.Active)
                .OrderBy(x => x.DgtTransportOrderNo)
                .Select(x => new Ec4uCarrier
                {
                    Id = x.Id,
                    Ec4uCarrierId = x.DgtCarrierId,
                    Ec4uCarReference = x.DgtReference,
                    Ec4uCarSolutionversion = x.DgtSolutionversion,
                    Ec4uCarSolutionid = x.DgtSolutionid,
                    Ec4uCarSolutionuniquename = x.DgtSolutionuniquename,
                    Ec4uCarSolutionfriendlyname = x.DgtSolutionfriendlyname,
                    Ec4uCarTransportOrderNo = x.DgtTransportOrderNo
                })
                .ToList();
        }

        if (carriers.Count == 0)
        {
            Console.MarkupLine("[red]No active carrier[/]");
            return Tracer.End(this, false);
        }

        var carrierInfo = new List<Carrier>();
        foreach (var carrier in carriers)
        {
            if (!IsCarrierValid(carrier))
            {
                continue;
            }

            var solutionLookup = GetSolution(dataContext, ExtractIdAndOrder(carrier));
            if (solutionLookup.Solution == null)
            {
                continue;
            }

            carrierInfo.Add(ToCarrierInfo(solutionLookup.Solution, solutionLookup.Carrier));
        }

        fileService.ExportJsonFile(settings.FileDir, settings.FileName, carrierInfo.OrderBy(carrier => carrier.Order).ToList());

        return Tracer.End(this, true);
    }

    private static Carrier ToCarrierInfo(Solution solution, Ec4uCarrier carrier) => new()
    {
        UniqueName = solution.UniqueName!,
        FriendlyName = solution.FriendlyName!,
        SolutionId = solution.Id,
        CarrierId = carrier.Id,
        Order = carrier.Ec4uCarTransportOrderNo!.Value,
        Version = solution.Version!
    };


    private bool IsCarrierValid(Ec4uCarrier carrier)
    {
        if (!string.IsNullOrWhiteSpace(carrier.Ec4uCarSolutionid) && Guid.TryParse(carrier.Ec4uCarSolutionid,CultureInfo.InvariantCulture, out _))
        {
            return true;
        }

        Console.MarkupLine($"[yellow]Solution '{carrier.Ec4uCarSolutionuniquename}' has invalid solutionid '{carrier.Ec4uCarSolutionid}'[/]");
        return false;
    }

    private (Solution? Solution, Ec4uCarrier Carrier) GetSolution(DataContext dataContext, (Guid SolutionId, Ec4uCarrier Order) tuple)
    {
        var solution = dataContext.SolutionSet.Where(x => x.Id == tuple.SolutionId)
            .Select(x => new Solution
            {
                Id = x.Id,
                SolutionId = x.SolutionId,
                FriendlyName = x.FriendlyName,
                UniqueName = x.UniqueName,
                Version = x.Version
            })
            .SingleOrDefault();

        Console.MarkupLine(solution != null ? $"found carrier solution [green]{solution.UniqueName}[/]" : $"[yellow]Solution carrier with ID '{tuple.SolutionId}' not found[/]");

        return (solution, tuple.Order);
    }

    private static (Guid SolutionId, Ec4uCarrier Carrier) ExtractIdAndOrder(Ec4uCarrier carrier)
    {
        carrier.Ec4uCarTransportOrderNo ??= -1;
        return (Guid.Parse(carrier.Ec4uCarSolutionid!,CultureInfo.InvariantCulture), carrier);
    }
}
