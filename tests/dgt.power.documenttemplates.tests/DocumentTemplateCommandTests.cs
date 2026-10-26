// Copyright (c) DIGITALL Nature. All rights reserved

using dgt.power.common;
using dgt.power.documenttemplates.Commands;
using dgt.power.tests;
using Microsoft.Extensions.DependencyInjection;
using Spectre.Console;
using Spectre.Console.Cli;
using Spectre.Console.Testing;

namespace dgt.power.documenttemplates.tests;

public class DocumentTemplateCommandTests
{
    [Test]
    [Arguments("import", "--cleanup")]
    [Arguments("import", "--force")]
    [Arguments("import", "--status")]
    [Arguments("import", "extra-position")]
    [Arguments("export", "--cleanup")]
    [Arguments("export", "--force")]
    [Arguments("export", "--status")]
    [Arguments("export", "extra-position")]
    public async Task Run_UnknownArgumentsRejectBeforeConnectionOrWrites(string operation, string argument)
    {
        var directory = TemplateTestData.DirectoryPath();
        try
        {
            var manifest = TemplateTestData.WriteManifest(directory, TemplateTestData.Entry());
            var (service, state) = TemplateServiceProxy.Create();
            var acquisitions = 0;
            var connection = TemplateTestData.Connection(service, () => acquisitions++);
            using var console = new TestConsole();
            using var commandConsole = new TestConsole();
            var services = new ServiceCollection();
            services.AddSingleton<ICommandSpanFactory, TestSpanFactory>();
            services.AddSingleton(connection);
            services.AddSingleton<IAnsiConsole>(commandConsole);
            var app = new CommandApp(new TypeRegistrar(services));
            string? error = null;
            ConfigureApp(app, console, message => error = message);

            var result = await app.RunAsync(["documenttemplates", operation,
                operation == "import" ? manifest : directory, argument]);

            await Assert.That(result).IsNotEqualTo(0);
            await Assert.That(acquisitions).IsZero();
            await Assert.That(state.Writes).IsEmpty();
            await Assert.That(error).Contains(argument);
        }
        finally
        {
            Directory.Delete(directory, true);
        }
    }

    private static void ConfigureApp(CommandApp app, IAnsiConsole console, Action<string> onError)
    {
        app.Configure(config =>
        {
            config.Settings.Console = console;
            config.SetExceptionHandler((exception, _) =>
            {
                onError(exception.Message);
                return 1;
            });
            config.AddBranch("documenttemplates", branch =>
            {
                branch.AddCommand<DocumentTemplateImportCommand>("import");
                branch.AddCommand<DocumentTemplateExportCommand>("export");
            });
        });
    }
}
