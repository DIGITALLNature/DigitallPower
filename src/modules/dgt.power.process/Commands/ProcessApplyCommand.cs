// Copyright (c) DIGITALL Nature. All rights reserved

using dgt.power.common;
using dgt.power.common.Ownership;
using dgt.power.process.Configuration;
using dgt.power.process.Logic;
using Spectre.Console;

namespace dgt.power.process.Commands;

public sealed class ProcessApplyCommand(
    ICommandSpanFactory spans,
    IDataverseConnection connection,
    IAnsiConsole console)
    : PowerCommand<ProcessApplySettings>(spans, console)
{
    protected override async Task<bool> RunAsync(ProcessApplySettings settings, CancellationToken cancellationToken)
    {
        ProcessConfiguration configuration;
        OwnerMapping? mapping = null;
        try
        {
            configuration = ProcessConfiguration.Load(settings.File!);
            if (!string.IsNullOrWhiteSpace(settings.OwnerMapping))
            {
                mapping = OwnerMappingLoader.Load(settings.OwnerMapping);
            }
        }
        catch (Exception exception) when (exception is ProcessConfigurationException or OwnerResolutionException or ArgumentException)
        {
            Console.MarkupLine($"[red]Invalid process input: {exception.Message.EscapeMarkup()}[/]");
            return false;
        }

        var engine = new ProcessApplyEngine(connection, new OwnerResolver(), Console);
        return await engine.ApplyAsync(
            configuration,
            mapping,
            settings.ActivateAs,
            settings.ActivateAsOwner,
            settings.DryRun,
            cancellationToken);
    }
}
