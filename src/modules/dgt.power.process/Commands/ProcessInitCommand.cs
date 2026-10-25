// Copyright (c) DIGITALL Nature. All rights reserved

using dgt.power.common;
using dgt.power.common.Ownership;
using dgt.power.process.Logic;
using Microsoft.Xrm.Sdk;
using Spectre.Console;

namespace dgt.power.process.Commands;

// ReSharper disable once ClassNeverInstantiated.Global — instantiated by Spectre.Console.Cli through the DI container.
public sealed class ProcessInitCommand(
    ICommandSpanFactory spans,
    IDataverseConnection connection,
    IAnsiConsole console)
    : PowerCommand<ProcessInitSettings>(spans, console)
{
    private readonly OwnerResolver _ownerResolver = new();

    protected override async Task<bool> RunAsync(ProcessInitSettings settings, CancellationToken cancellationToken)
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

        IReadOnlyList<Entity> processes;
        try
        {
            processes = await new ProcessSolutionSelector(service).SelectAsync(settings.Solutions!, cancellationToken);
        }
        catch (Exception exception) when (exception is not OperationCanceledException)
        {
            Console.MarkupLine($"[red]Process selection failed: {exception.Message.EscapeMarkup()}[/]");
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
                var byProcess = processes
                    .Where(process => process.GetAttributeValue<EntityReference>("ownerid") is not null)
                    .ToDictionary(process => process.Id, process => process.GetAttributeValue<EntityReference>("ownerid")!);
                if (byProcess.Count != processes.Count)
                {
                    throw new OwnerResolutionException(
                        OwnerResolutionFailure.MissingCaptureMapping,
                        "A selected process did not have an owner reference.");
                }

                IReadOnlyList<OwnerCapture> captures = [];
                if (byProcess.Count > 0)
                {
                    captures = await _ownerResolver.ResolveForCaptureAsync(
                        service,
                        mapping,
                        byProcess.Values.DistinctBy(owner => (owner.LogicalName, owner.Id)),
                        cancellationToken);
                }
                var byPrincipal = captures.ToDictionary(
                    capture => (capture.Principal.LogicalName, capture.Principal.Id),
                    capture => capture.LogicalKey);
                foreach (var (processId, principal) in byProcess)
                {
                    if (!byPrincipal.TryGetValue((principal.LogicalName, principal.Id), out var key))
                    {
                        throw new OwnerResolutionException(
                            OwnerResolutionFailure.MissingCaptureMapping,
                            $"No owner mapping was resolved for process '{processId:D}'.",
                            capturedOwner: principal);
                    }

                    ownerKeys.Add(processId, key);
                }
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
            content = ProcessConfigurationWriter.Serialize(processes, settings.Capture, ownerKeys);
        }
        catch (InvalidOperationException exception)
        {
            Console.MarkupLine($"[red]Could not serialize process configuration: {exception.Message.EscapeMarkup()}[/]");
            return false;
        }
        try
        {
            await ProcessConfigurationWriter.WriteAtomicallyAsync(outputPath, content, settings.Overwrite, cancellationToken);
        }
        catch (Exception exception) when (exception is not OperationCanceledException)
        {
            Console.MarkupLine($"[red]Could not write process configuration: {exception.Message.EscapeMarkup()}[/]");
            return false;
        }

        Console.MarkupLine($"[green]Captured {processes.Count} processes to '{outputPath.EscapeMarkup()}'.[/]");
        return true;
    }
}
