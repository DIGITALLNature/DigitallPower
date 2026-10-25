// Copyright (c) DIGITALL Nature. All rights reserved

using System.ServiceModel;
using dgt.power.common;
using dgt.power.common.FileAccess;
using dgt.power.teamtemplates.Configuration;
using dgt.power.teamtemplates.Logic;
using Microsoft.Xrm.Sdk;
using Spectre.Console;

namespace dgt.power.teamtemplates.Commands;

public sealed class TeamTemplateExportCommand(
    ICommandSpanFactory spans, IDataverseConnection connection, IAnsiConsole console)
    : PowerCommand<TeamTemplateExportSettings>(spans, console)
{
    protected override async Task<bool> RunAsync(TeamTemplateExportSettings settings, CancellationToken cancellationToken)
    {
        var validation = settings.Validate();
        if (!validation.Successful)
        {
            Console.MarkupLine($"[red]{validation.Message?.EscapeMarkup()}[/]");
            return false;
        }
        var output = Path.GetFullPath(settings.File!, Environment.CurrentDirectory);
        if (File.Exists(output) && !settings.Overwrite)
        {
            Console.MarkupLine("[red]Output already exists. Use --overwrite to replace it.[/]");
            return false;
        }

        var service = await connection.GetServiceAsync();
        try
        {
            var (metadata, templates) = await new TeamTemplateTransfer(service, Console).ReadAsync(cancellationToken);
            var configuration = TeamTemplateTransfer.Export(metadata, templates, settings.Tables ?? []);
            var json = configuration.Serialize();
            if (settings.DryRun)
            {
                Console.MarkupLine($"DRY RUN: export {configuration.Templates.Count} templates to '{output.EscapeMarkup()}'.");
                return true;
            }
            await AtomicFileWriter.WriteAsync(output, json, settings.Overwrite, cancellationToken);
            Console.MarkupLine($"[green]Exported {configuration.Templates.Count} templates to '{output.EscapeMarkup()}'.[/]");
            return true;
        }
        catch (Exception exception) when (exception is TeamTemplateConfigurationException or IOException or UnauthorizedAccessException)
        {
            Console.MarkupLine($"[red]Teamtemplate export failed: {exception.Message.EscapeMarkup()}[/]");
            return false;
        }
        catch (Exception exception) when (exception is FaultException<OrganizationServiceFault> or HttpRequestException or TimeoutException)
        {
            Console.MarkupLine($"[red]Teamtemplate export failed: {exception.Message.EscapeMarkup()}[/]");
            return false;
        }
    }
}
