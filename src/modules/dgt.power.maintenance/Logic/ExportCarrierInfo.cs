// Copyright (c) DIGITALL Nature. All rights reserved
// DIGITALL Nature licenses this file to you under the Microsoft Public License.

using System.Diagnostics.CodeAnalysis;
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
    public const string ValidationErrorMessage = $"Carrier entity '{Ec4uCarrier.EntityLogicalName}' or  '{DgtCarrier.EntityLogicalName}' isn't installed in the current environment.";
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
        var isSuccessfulDgt = OrganizationService.TryExecute<RetrieveEntityRequest, RetrieveEntityResponse>(new RetrieveEntityRequest
        {
            EntityFilters = EntityFilters.Entity,
            LogicalName = DgtCarrier.EntityLogicalName
        }, out _);

        List<DgtCarrier> carriers;
        if (isSuccessfulDgt)
        {
            carriers = DataContext.DgtCarrierSet
                .Where(x => x.Statecode!.Value == DgtCarrier.Options.Statecode.Active)
                .OrderBy(x => x.DgtTransportOrderNo)
                .Select(x => new DgtCarrier
                {
                    Id = x.Id,
                    DgtCarrierId = x.DgtCarrierId,
                    DgtReference = x.DgtReference,
                    DgtSolutionversion = x.DgtSolutionversion,
                    DgtSolutionid = x.DgtSolutionid,
                    DgtSolutionuniquename = x.DgtSolutionuniquename,
                    DgtSolutionfriendlyname = x.DgtSolutionfriendlyname,
                    DgtTransportOrderNo = x.DgtTransportOrderNo
                })
                .ToList();
        }
        else
        {
#pragma warning disable CS8601 // Possible null reference assignment.
            carriers = DataContext.Ec4uCarrierSet
                .Where(x => x.Statecode!.Value == Ec4uCarrier.Options.Statecode.Active)
                .OrderBy(x => x.Ec4uCarTransportOrderNo)
                .Select(x => new DgtCarrier
                {
                    Id = x.Id,
                    DgtCarrierId = x.Ec4uCarrierId,
                    DgtReference = x.Ec4uCarReference,
                    DgtSolutionversion = x.Ec4uCarSolutionversion,
                    DgtSolutionid = x.Ec4uCarSolutionid,
                    DgtSolutionuniquename = x.Ec4uCarSolutionuniquename,
                    DgtSolutionfriendlyname = x.Ec4uCarSolutionfriendlyname,
                    DgtTransportOrderNo = x.Ec4uCarTransportOrderNo
                })
                .ToList();
#pragma warning restore CS8601 // Possible null reference assignment.
        }

        if (carriers.Count == 0)
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

    private bool SolutionIsNotNull((Solution? Solution, DgtCarrier Carrier) tuple)
    {
        return tuple.Solution != null;
    }

    private static Carrier ToCarrierInfo((Solution? Solution, DgtCarrier Carrier) tuple) => new()
    {
        UniqueName = tuple.Solution!.UniqueName!,
        FriendlyName = tuple.Solution.FriendlyName!,
        SolutionId = tuple.Solution.Id,
        CarrierId = tuple.Carrier.Id,
        Order = tuple.Carrier.DgtTransportOrderNo!.Value,
        Version = tuple.Solution.Version!
    };


    private bool IsCarrierValid(DgtCarrier carrier)
    {
        if (!string.IsNullOrWhiteSpace(carrier.DgtSolutionid) && Guid.TryParse(carrier.DgtSolutionid, out _))
        {
            return true;
        }

        AnsiConsole.MarkupLine($"[yellow]Solution '{carrier.DgtSolutionuniquename}' has invalid solutionid '{carrier.DgtSolutionid}'[/]");
        return false;
    }

    private (Solution? Solution, DgtCarrier Carrier) GetSolution((Guid SolutionId, DgtCarrier Order) tuple)
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

        if (solution != null)
        {
            AnsiConsole.MarkupLine($"Found carrier solution [green]{solution.UniqueName}[/]");
        }
        else
        {
            AnsiConsole.MarkupLine($"[yellow]Solution carrier with ID '{tuple.SolutionId}' not found[/]");
        }

        return (solution, tuple.Order);
    }

    private static (Guid SolutionId, DgtCarrier Carrier) ExtractIdAndOrder(DgtCarrier carrier)
    {
        carrier.DgtTransportOrderNo ??= -1;
        return (Guid.Parse(carrier.DgtSolutionid!), carrier);
    }
}
