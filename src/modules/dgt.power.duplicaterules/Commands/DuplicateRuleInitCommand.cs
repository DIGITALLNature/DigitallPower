// Copyright (c) DIGITALL Nature. All rights reserved

using dgt.power.common;
using dgt.power.common.Ownership;
using dgt.power.duplicaterules.Logic;
using Microsoft.Xrm.Sdk;
using Spectre.Console;

namespace dgt.power.duplicaterules.Commands;

// ReSharper disable once ClassNeverInstantiated.Global — instantiated by Spectre.Console.Cli through the DI container.
public sealed class DuplicateRuleInitCommand(
    ICommandSpanFactory spans,
    IDataverseConnection connection,
    IAnsiConsole console)
    : PowerCommand<DuplicateRuleInitSettings>(spans, console)
{
    private readonly OwnerResolver _ownerResolver = new();

    protected override async Task<bool> RunAsync(DuplicateRuleInitSettings settings, CancellationToken cancellationToken)
    {
        var outputPath = Path.GetFullPath(settings.File!, Environment.CurrentDirectory);
        if (!settings.Overwrite && File.Exists(outputPath))
        {
            Console.MarkupLine($"[red]Output file '{outputPath.EscapeMarkup()}' already exists. Use --overwrite to replace it.[/]");
            return false;
        }

        var mapping = string.IsNullOrWhiteSpace(settings.OwnerMapping)
            ? null
            : OwnerMappingLoader.Load(settings.OwnerMapping);
        var service = await connection.GetServiceAsync();

        IReadOnlyList<Entity> rules;
        try
        {
            rules = await new DuplicateRuleSelector(service).SelectAsync(settings.Entity, settings.GetParsedRuleIds(), cancellationToken);
        }
        catch (Exception exception) when (exception is not OperationCanceledException)
        {
            Console.MarkupLine($"[red]Duplicate-rule selection failed: {exception.Message.EscapeMarkup()}[/]");
            return false;
        }

        var ownerKeys = new Dictionary<Guid, string>();
        if (settings.Capture is "owner" or "both")
        {
            if (mapping is null)
            {
                Console.MarkupLine("[red]An owner mapping is required for ownership capture.[/]");
                return false;
            }

            try
            {
                await CaptureOwnersAsync(service, mapping, rules, ownerKeys, cancellationToken);
            }
            catch (Exception exception) when (exception is not OperationCanceledException)
            {
                Console.MarkupLine($"[red]Owner capture failed: {exception.Message.EscapeMarkup()}[/]");
                return false;
            }
        }

        string content;
        try
        {
            content = DuplicateRuleConfigurationWriter.Serialize(rules, settings.Capture, ownerKeys);
        }
        catch (InvalidOperationException exception)
        {
            Console.MarkupLine($"[red]Could not serialize duplicate-rule configuration: {exception.Message.EscapeMarkup()}[/]");
            return false;
        }

        try
        {
            await DuplicateRuleConfigurationWriter.WriteAtomicallyAsync(outputPath, content, settings.Overwrite, cancellationToken);
        }
        catch (Exception exception) when (exception is not OperationCanceledException)
        {
            Console.MarkupLine($"[red]Could not write duplicate-rule configuration: {exception.Message.EscapeMarkup()}[/]");
            return false;
        }

        Console.MarkupLine($"[green]Captured {rules.Count} duplicate rule(s) to '{outputPath.EscapeMarkup()}'.[/]");
        return true;
    }

    private async Task CaptureOwnersAsync(
        Microsoft.PowerPlatform.Dataverse.Client.IOrganizationServiceAsync2 service,
        OwnerMapping mapping,
        IReadOnlyList<Entity> rules,
        Dictionary<Guid, string> ownerKeys,
        CancellationToken cancellationToken)
    {
        var byRule = new Dictionary<Guid, EntityReference>();
        foreach (var rule in rules)
        {
            var owner = rule.GetAttributeValue<EntityReference>("ownerid")
                ?? throw new OwnerResolutionException(
                    OwnerResolutionFailure.MissingCaptureMapping,
                    $"Duplicate rule '{rule.Id:D}' did not have an owner reference.");
            byRule.Add(rule.Id, owner);
        }

        if (byRule.Count == 0)
        {
            return;
        }

        var captures = await _ownerResolver.ResolveForCaptureAsync(
            service,
            mapping,
            byRule.Values.DistinctBy(owner => (owner.LogicalName, owner.Id)),
            cancellationToken);
        var byPrincipal = captures.ToDictionary(
            capture => (capture.Principal.LogicalName, capture.Principal.Id),
            capture => capture.LogicalKey);
        foreach (var (ruleId, principal) in byRule)
        {
            if (!byPrincipal.TryGetValue((principal.LogicalName, principal.Id), out var key))
            {
                throw new OwnerResolutionException(
                    OwnerResolutionFailure.MissingCaptureMapping,
                    $"No owner-mapping key was resolved for duplicate rule '{ruleId:D}'.",
                    capturedOwner: principal);
            }

            ownerKeys.Add(ruleId, key);
        }
    }
}
