// Copyright (c) DIGITALL Nature. All rights reserved

using dgt.power.common;
using dgt.power.common.Ownership;
using dgt.power.duplicaterules.Configuration;
using dgt.power.duplicaterules.Logic;
using Spectre.Console;

namespace dgt.power.duplicaterules.Commands;

// ReSharper disable once ClassNeverInstantiated.Global — instantiated by Spectre.Console.Cli through the DI container.
public sealed class DuplicateRuleApplyCommand(
    ICommandSpanFactory spans,
    IDataverseConnection connection,
    IAnsiConsole console,
    TimeProvider timeProvider)
    : PowerCommand<DuplicateRuleApplySettings>(spans, console)
{
    protected override async Task<bool> RunAsync(DuplicateRuleApplySettings settings, CancellationToken cancellationToken)
    {
        DuplicateRuleConfiguration configuration;
        OwnerMapping? mapping = null;
        try
        {
            configuration = DuplicateRuleConfiguration.Load(settings.File!);
            if (!string.IsNullOrWhiteSpace(settings.OwnerMapping))
            {
                mapping = OwnerMappingLoader.Load(settings.OwnerMapping);
            }
        }
        catch (Exception exception) when (exception is DuplicateRuleConfigurationException or OwnerResolutionException or ArgumentException)
        {
            Console.MarkupLine($"[red]Invalid duplicate-rule input: {exception.Message.EscapeMarkup()}[/]");
            return false;
        }

        var engine = new DuplicateRuleApplyEngine(connection, new OwnerResolver(), Console, timeProvider);
        return await engine.ApplyAsync(
            configuration,
            mapping,
            new DuplicateRuleApplyOptions(settings.DryRun, settings.NoWait, TimeSpan.FromSeconds(settings.Timeout)),
            cancellationToken);
    }
}
