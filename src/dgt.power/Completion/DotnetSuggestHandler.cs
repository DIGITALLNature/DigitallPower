// Copyright (c) DIGITALL Nature. All rights reserved
// DIGITALL Nature licenses this file to you under the Microsoft Public License.

using Microsoft.Extensions.DependencyInjection;
using Spectre.Console;
using Spectre.Console.Cli;
using Spectre.Console.Cli.Help;
using dgt.power.common;

namespace dgt.power.Completion;

/// <summary>
/// Handles the dotnet-suggest tab-completion protocol.
/// <para>
/// When dotnet-suggest intercepts a TAB keypress, it re-invokes the CLI with
/// <c>[suggest:&lt;position&gt;] "&lt;command-line&gt;"</c> as arguments.
/// This handler detects that mode, builds the Spectre command model via a lightweight
/// app run (no Dataverse connection, no telemetry, no NuGet checks), and writes
/// the completion candidates — one per line — to stdout.
/// </para>
/// </summary>
internal static class DotnetSuggestHandler
{
    /// <summary>Returns <see langword="true"/> when the first argument is a dotnet-suggest directive.</summary>
    public static bool IsSuggestMode(string[] args) =>
        args.Length >= 1 && SuggestDirective.IsMatch(args[0]);

    /// <summary>
    /// Runs the completion pipeline and writes candidates to stdout.
    /// </summary>
    /// <param name="args">Original process arguments (args[0] = directive, args[1] = command line).</param>
    /// <param name="registerCommands">
    /// The same command-registration delegate used for the real app, so the model is always in sync.
    /// </param>
    /// <returns>Process exit code (0 = success).</returns>
    public static async Task<int> HandleAsync(string[] args, Action<IConfigurator> registerCommands)
    {
        if (!SuggestDirective.TryParse(args[0], out var directive))
            return 1;

        var model = await CaptureModelAsync(registerCommands);
        if (model is null)
            return 0; // graceful: no completions rather than an error

        var commandLine = args.Length > 1 ? args[1].Trim('"') : string.Empty;
        var completions = CompletionEngine.GetCompletions(model, commandLine, directive.Position);

        foreach (var completion in completions)
            Console.WriteLine(completion);

        return 0;
    }

    private static async Task<ICommandModel?> CaptureModelAsync(Action<IConfigurator> registerCommands)
    {
        var capture = new ModelCaptureHelpProvider();

        // Minimal DI: only what Spectre needs to parse --help (no Dataverse, no telemetry, no NuGet).
        var services = new ServiceCollection();
        var captureApp = new CommandApp(new TypeRegistrar(services));

        // Provide a silent console so that nothing leaks to stdout during the capture run.
        var silentConsole = AnsiConsole.Create(new AnsiConsoleSettings
        {
            Out = new AnsiConsoleOutput(TextWriter.Null)
        });

        captureApp.Configure(config =>
        {
            config.SetHelpProvider(capture);
            config.ConfigureConsole(silentConsole);
            registerCommands(config);
        });

        // Redirect stdout as an extra safety net in case anything bypasses IAnsiConsole.
        var savedOut = Console.Out;
        Console.SetOut(TextWriter.Null);
        try
        {
            await captureApp.RunAsync(["--help"]);
        }
#pragma warning disable CA1031 // Model capture is best-effort; any failure means no completions.
        catch (Exception)
#pragma warning restore CA1031
        {
            // Intentionally swallowed: an exception here results in no completions (graceful degradation).
        }
        finally
        {
            Console.SetOut(savedOut);
        }

        return capture.Model;
    }
}
