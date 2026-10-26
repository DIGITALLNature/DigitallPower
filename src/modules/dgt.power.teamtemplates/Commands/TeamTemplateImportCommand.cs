// Copyright (c) DIGITALL Nature. All rights reserved

using System.ServiceModel;
using System.Text.Json;
using dgt.power.common;
using dgt.power.teamtemplates.Configuration;
using dgt.power.teamtemplates.Logic;
using Microsoft.Xrm.Sdk;
using Spectre.Console;

namespace dgt.power.teamtemplates.Commands;

// ReSharper disable once ClassNeverInstantiated.Global — instantiated by Spectre.Console.Cli through the DI container.
public sealed class TeamTemplateImportCommand(
    ICommandSpanFactory spans, IDataverseConnection connection, IAnsiConsole console)
    : PowerCommand<TeamTemplateImportSettings>(spans, console)
{
    protected override async Task<bool> RunAsync(TeamTemplateImportSettings settings, CancellationToken cancellationToken)
    {
        var validation = settings.Validate();
        if (!validation.Successful)
        {
            Console.MarkupLine($"[red]{validation.Message?.EscapeMarkup()}[/]");
            return false;
        }
        TeamTemplateConfiguration configuration;
        try
        {
            configuration = TeamTemplateConfiguration.Parse(await File.ReadAllTextAsync(settings.File!, cancellationToken));
            if (settings.Cleanup && configuration.Templates.Count == 0)
            {
                throw new TeamTemplateConfigurationException("Empty templates cannot be combined with cleanup.");
            }
        }
        catch (Exception exception) when (exception is IOException or UnauthorizedAccessException or JsonException or TeamTemplateConfigurationException)
        {
            Console.MarkupLine($"[red]Invalid teamtemplate input: {exception.Message.EscapeMarkup()}[/]");
            return false;
        }
        if (configuration.Templates.Count == 0)
        {
            Console.MarkupLine("No templates to import; no changes.");
            return true;
        }

        var service = await connection.GetServiceAsync();
        var transfer = new TeamTemplateTransfer(service, Console);
        TeamTemplatePlan plan;
        try
        {
            var (metadata, templates) = await transfer.ReadAsync(cancellationToken);
            plan = TeamTemplateTransfer.Plan(configuration, metadata, templates, settings.CleanupTables ?? []);
        }
        catch (Exception exception) when (exception is TeamTemplateConfigurationException or FaultException<OrganizationServiceFault> or HttpRequestException or TimeoutException)
        {
            Console.MarkupLine($"[red]Teamtemplate preflight failed: {exception.Message.EscapeMarkup()}[/]");
            return false;
        }
        return await transfer.ApplyAsync(plan, settings.DryRun, cancellationToken);
    }
}
