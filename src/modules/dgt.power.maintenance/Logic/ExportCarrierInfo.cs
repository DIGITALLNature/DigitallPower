// Copyright (c) DIGITALL Nature. All rights reserved
// DIGITALL Nature licenses this file to you under the Microsoft Public License.

using System.Diagnostics;
using System.Diagnostics.CodeAnalysis;
using System.Globalization;
using dgt.power.common;
using dgt.power.common.Commands;
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

public class ExportCarrierInfo : AbstractDataverseCommand<CarrierInfoSettings>
{
    public static readonly string ValidationErrorMessage = $"Carrier entity '{Ec4uCarrier.EntityLogicalName}' or  '{DgtCarrier.EntityLogicalName}' isn't installed in the current environment.";
    private readonly IFileService _fileService;

    public ExportCarrierInfo(IOrganizationService organizationService, IConfigResolver configResolver, IFileService fileService) : base(
        organizationService, configResolver)
    {
        _fileService = fileService;
    }

    public override ValidationResult Validate([NotNull] CommandContext context, [NotNull] CarrierInfoSettings settings)
    {
        var isSuccessfulDgt = OrganizationService.TryExecute<RetrieveEntityRequest, RetrieveEntityResponse>(new RetrieveEntityRequest
        {
            EntityFilters = EntityFilters.Entity,
            LogicalName = DgtCarrier.EntityLogicalName
        }, out _);

        var isSuccessfulEc4u = !isSuccessfulDgt && OrganizationService.TryExecute<RetrieveEntityRequest, RetrieveEntityResponse>(new RetrieveEntityRequest
        {
            EntityFilters = EntityFilters.Entity,
            LogicalName = Ec4uCarrier.EntityLogicalName
        }, out _);

        return (isSuccessfulDgt || isSuccessfulEc4u)
            ? base.Validate(context, settings)
            : ValidationResult.Error(ValidationErrorMessage);
    }

    public override ExitCode Execute(CarrierInfoSettings settings)
    {
        Debug.Assert(settings != null, nameof(settings) + " != null");

        var isSuccessfulOld = OrganizationService.TryExecute<RetrieveEntityRequest, RetrieveEntityResponse>(new RetrieveEntityRequest
        {
            EntityFilters = EntityFilters.Entity,
            LogicalName = Ec4uCarrier.EntityLogicalName
        }, out _);

        List<Ec4uCarrier> carriers;
        if (isSuccessfulOld)
        {
            carriers = DataContext.Ec4uCarrierSet
                .Where(x => x.Statecode!.Value == Ec4uCarrier.Options.Statecode.Active)
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
        else
        {
            carriers = DataContext.DgtCarrierSet
                .Where(x => x.Statecode!.Value == Ec4uCarrier.Options.Statecode.Active)
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

        if (!carriers.Any())
        {
            AnsiConsole.MarkupLine("[red]No active carrier[/]");
            return ExitCode.Error;
        }

        var carrierInfo = carriers
            .Where(IsCarrierValid)
            .Select(ExtractIdAndOrder)
            .Select(GetSolution)
            .Where(SolutionIsNotNull)
            .Select(ToCarrierInfo)
            .OrderBy(carrier => carrier.Order);

        _fileService.ExportJsonFile(settings.FileDir, settings.FileName, new Carriers(carrierInfo));

        return ExitCode.Success;
    }

    private static bool SolutionIsNotNull((Solution? Solution, Ec4uCarrier Carrier) tuple)
    {
        return tuple.Solution != null;
    }

    private static Carrier ToCarrierInfo((Solution? Solution, Ec4uCarrier Carrier) tuple) => new()
    {
        UniqueName = tuple.Solution!.UniqueName!,
        FriendlyName = tuple.Solution.FriendlyName!,
        SolutionId = tuple.Solution.Id,
        CarrierId = tuple.Carrier.Id,
        Order = tuple.Carrier.Ec4uCarTransportOrderNo!.Value,
        Version = tuple.Solution.Version!
    };


    private static bool IsCarrierValid(Ec4uCarrier carrier)
    {
        if (!string.IsNullOrWhiteSpace(carrier.Ec4uCarSolutionid) && Guid.TryParse(carrier.Ec4uCarSolutionid,CultureInfo.InvariantCulture, out _))
        {
            return true;
        }

        AnsiConsole.MarkupLine($"[yellow]Solution '{carrier.Ec4uCarSolutionuniquename}' has invalid solutionid '{carrier.Ec4uCarSolutionid}'[/]");
        return false;
    }

    private (Solution? Solution, Ec4uCarrier Carrier) GetSolution((Guid SolutionId, Ec4uCarrier Order) tuple)
    {
        var solution = DataContext.SolutionSet.Where(x => x.Id == tuple.SolutionId)
            .Select(x => new Solution
            {
                Id = x.Id,
                SolutionId = x.SolutionId,
                FriendlyName = x.FriendlyName,
                UniqueName = x.UniqueName,
                Version = x.Version
            })
            .SingleOrDefault();

        AnsiConsole.MarkupLine(solution != null ? $"found carrier solution [green]{solution.UniqueName}[/]" : $"[yellow]Solution carrier with ID '{tuple.SolutionId}' not found[/]");

        return (solution, tuple.Order);
    }

    private static (Guid SolutionId, Ec4uCarrier Carrier) ExtractIdAndOrder(Ec4uCarrier carrier)
    {
        carrier.Ec4uCarTransportOrderNo ??= -1;
        return (Guid.Parse(carrier.Ec4uCarSolutionid!,CultureInfo.InvariantCulture), carrier);
    }
}
