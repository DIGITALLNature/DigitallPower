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
            Task<string> CreateAsync(Action<WebResourceScaffoldingProgress> progress) =>
                new WebResourceProjectScaffolder(httpClient).CreateAsync(
                    settings.Destination,
                    settings.TemplateVersion,
                    new WebResourceProjectValues(
                        settings.Name,
                        settings.Global,
                        settings.Solution,
                        settings.PublisherPrefix,
                        settings.DgtpConnection),
                    cancellationToken,
                    progress);

            var commitSha = settings.NonInteractive || !Console.Profile.Capabilities.Interactive
                ? await CreateWithoutAnimationAsync(CreateAsync)
                : await Console.Progress()
                .AutoClear(true)
                .Columns(
                    new TaskDescriptionColumn(),
                    new ProgressBarColumn(),
                    new PercentageColumn(),
                    new SpinnerColumn())
                .StartAsync(async context =>
                {
                    var stage = WebResourceScaffoldingProgress.Stage.Resolving;
                    var task = context.AddTask("Resolving template...");
                    task.IsIndeterminate = true;
                    var sha = await CreateAsync(update =>
                        {
                            if (update.CurrentStage != stage)
                            {
                                task.StopTask();
                                stage = update.CurrentStage;
                                task = context.AddTask(StageDescription(stage));
                            }

                            task.IsIndeterminate = update.TotalBytes is null;
                            if (update.TotalBytes is { } totalBytes)
                            {
                                task.MaxValue = totalBytes;
                                task.Value = update.BytesReceived;
                            }
                        });
                    task.StopTask();
                    return sha;
                });

            var destination = Markup.Escape(Path.GetFullPath(settings.Destination));
            Console.MarkupLine($"[green]{Emoji.Known.CheckMark}[/] Webresource project created at {destination}");
            Console.MarkupLine($"[grey]Template commit: {commitSha}[/]");
            Console.WriteLine();
            Console.MarkupLine($"{Emoji.Known.Rocket} Next steps:");
            Console.MarkupLine($"   cd \"{Markup.Escape(Path.GetFullPath(settings.Destination))}\"");
            Console.MarkupLine("   pnpm install --frozen-lockfile");
            Console.MarkupLine("   pnpm run build:prod");
            Console.WriteLine();
            Console.MarkupLine($"{Emoji.Known.OpenBook} See README.md in the generated project for additional configuration and setup steps.");
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

    private async Task<string> CreateWithoutAnimationAsync(
        Func<Action<WebResourceScaffoldingProgress>, Task<string>> create)
    {
        WebResourceScaffoldingProgress.Stage? stage = null;
        return await create(update =>
        {
            if (update.CurrentStage != stage)
            {
                stage = update.CurrentStage;
                Console.WriteLine(StageDescription(stage.Value));
            }
        });
    }

    private static string StageDescription(WebResourceScaffoldingProgress.Stage stage) => stage switch
    {
        WebResourceScaffoldingProgress.Stage.Resolving => "Resolving template...",
        WebResourceScaffoldingProgress.Stage.Downloading => "Downloading template...",
        WebResourceScaffoldingProgress.Stage.Creating => "Creating project...",
        _ => throw new InvalidOperationException("Unknown scaffolding stage.")
    };
}
