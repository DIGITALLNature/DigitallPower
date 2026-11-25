// SPDX-FileCopyrightText: DIGITALL Nature
// SPDX-License-Identifier: MS-RL

using dgt.power.common;
using dgt.power.webresource.Scaffolding;
using Spectre.Console;

namespace dgt.power.webresource.Commands;

// ReSharper disable once ClassNeverInstantiated.Global
public sealed class WebResourceInitCommand(
    ICommandSpanFactory spans,
    IAnsiConsole console,
    HttpClient httpClient)
    : PowerCommand<WebResourceInitSettings>(spans, console)
{
    protected override async Task<bool> RunAsync(
        WebResourceInitSettings settings,
        CancellationToken cancellationToken)
    {
        var validationError = settings.GetValidationError();
        if (validationError is not null)
        {
            Console.MarkupLine($"[red]{Markup.Escape(validationError)}[/]");
            return false;
        }

        try
        {
            var commitSha = await new WebResourceProjectScaffolder(httpClient).CreateAsync(
                settings.Destination,
                settings.TemplateVersion,
                new WebResourceProjectValues(
                    settings.Name,
                    settings.Global,
                    settings.Solution,
                    settings.PublisherPrefix),
                cancellationToken);

            var destination = Markup.Escape(Path.GetFullPath(settings.Destination));
            Console.MarkupLine($"[green]Webresource project created at[/] {destination}");
            Console.MarkupLine($"Template commit: {commitSha}");
            Console.MarkupLine("Next steps:");
            Console.MarkupLine($"  cd \"{Markup.Escape(Path.GetFullPath(settings.Destination))}\"");
            Console.MarkupLine("  pnpm install --frozen-lockfile");
            Console.MarkupLine("  pnpm run build:prod");
            Console.MarkupLine("See README.md in the generated project for additional configuration and setup steps.");
            return true;
        }
        catch (WebResourceScaffoldingException exception)
        {
            Console.MarkupLine($"[red]{Markup.Escape(exception.Message)}[/]");
            return false;
        }
        catch (OperationCanceledException) when (cancellationToken.IsCancellationRequested)
        {
            Console.MarkupLine("[yellow]Webresource project generation was cancelled.[/]");
            return false;
        }
    }
}
