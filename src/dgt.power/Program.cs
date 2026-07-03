// Copyright (c) DIGITALL Nature. All rights reserved
// DIGITALL Nature licenses this file to you under the Microsoft Public License.

using System.Globalization;
using System.IO.IsolatedStorage;
using System.Runtime.Caching;
using System.Text.Json;
using System.Text.Json.Serialization;
using Azure.Monitor.OpenTelemetry.Exporter;
using dgt.power;
using dgt.power.analyzer.Base;
using dgt.power.analyzer.Logic;
using dgt.power.codegeneration;
using dgt.power.codegeneration.Generators;
using dgt.power.codegeneration.Generators.Contracts;
using dgt.power.codegeneration.Services;
using dgt.power.codegeneration.Services.Contracts;
using dgt.power.common;
using dgt.power.Commands.Complete;
using dgt.power.common.Commands;
#pragma warning disable IDE0005 // Used in #if RELEASE block
#pragma warning disable S1128
// ReSharper disable RedundantUsingDirective
using dgt.power.common.Exceptions;
using dgt.power.common.Extensions;
// ReSharper restore RedundantUsingDirective
#pragma warning restore S1128
#pragma warning restore IDE0005
using dgt.power.common.FileAccess;
using dgt.power.common.Logic;
using dgt.power.Completion;
using dgt.power.export.Base;
using dgt.power.export.Logic;
using dgt.power.import.Base;
using dgt.power.import.Logic;
using dgt.power.maintenance.Logic;
using dgt.power.profile.Base;
using dgt.power.profile.Commands;
using dgt.power.connection.Base;
using dgt.power.connection.Commands;
using dgt.power.push;
using dgt.power.push.Logic;
using dgt.power.Telemetry;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Xrm.Sdk;
using NuGet.Protocol;
using NuGet.Protocol.Core.Types;
using OpenTelemetry;
using OpenTelemetry.Resources;
using OpenTelemetry.Trace;
using Spectre.Console;
using Spectre.Console.Cli;
using Tracer = dgt.power.Tracer;

var defaultConfiguration = new Dictionary<string, string?>
{
    {"pollrate", "5000"}
};

// ── SUGGEST MODE: early exit before any I/O, telemetry or network calls ──────
// dotnet-suggest invokes the app as: dgtp [suggest:<position>] "<command-line>"
// Nothing must be written to stdout here except the completion candidates.
if (DotnetSuggestHandler.IsSuggestMode(args))
{
    return await DotnetSuggestHandler.HandleAsync(args, RegisterCommands);
}
// ─────────────────────────────────────────────────────────────────────────────

var configuration = new ConfigurationBuilder()
    .SetBasePath(Directory.GetCurrentDirectory())
    .AddInMemoryCollection(defaultConfiguration)
    .AddJsonFile("dgtp.json", optional: true)
    .AddEnvironmentVariables("dgtp:")
    .AddCommandLine(args)
    .Build();

var appConsole = AnsiConsole.Console;
var registrations = new ServiceCollection();
registrations.AddSingleton<PackageMetadataResource>(_ => Repository.Factory
    .GetCoreV3("https://api.nuget.org/v3/index.json")
    .GetResource<PackageMetadataResource>()
);
registrations.AddSingleton<VersionCheckInterceptor>();

registrations.AddSingleton<DeprecationInterceptor>();

// Telemetry setup
var isolatedStorage = IsolatedStorageFile.GetUserStoreForAssembly();
var telemetryEnabled = !TelemetryConfig.IsOptedOut;
string? installId = null;
TracerProvider? tracerProvider = null;

if (telemetryEnabled)
{
    TelemetryNotice.ShowIfFirstRun(isolatedStorage, appConsole);
    installId = TelemetryConfig.GetOrCreateInstallId(isolatedStorage);

    var connectionString = Environment.GetEnvironmentVariable("DGT_TELEMETRY_CONNECTION_STRING")
        ?? EmbeddedTelemetryConfig.ConnectionString;
    if (!string.IsNullOrEmpty(connectionString))
    {
        tracerProvider = Sdk.CreateTracerProviderBuilder()
            .AddSource(DgtpActivitySource.Name)
            .SetResourceBuilder(ResourceBuilder.CreateDefault()
                .AddService(DgtpActivitySource.Name, serviceVersion: DgtpActivitySource.Instance.Version))
            .AddAzureMonitorTraceExporter(o => o.ConnectionString = connectionString)
            .Build();
    }
}

var tracer = new Tracer(telemetryEnabled, installId, appConsole);
registrations.AddSingleton<ITracer>(tracer);

AppDomain.CurrentDomain.UnhandledException += (_, e) =>
{
    if (e.ExceptionObject is Exception ex)
    {
        tracer.TrackFatalException(ex);
    }
    tracerProvider?.ForceFlush(5000);
    tracerProvider?.Dispose();
};

TaskScheduler.UnobservedTaskException += (_, e) =>
{
    tracer.TrackFatalException(e.Exception);
    e.SetObserved();
};

registrations.AddSingleton<IConfiguration>(configuration);
registrations.AddSingleton<IXrmConnection, XrmConnection>();
registrations.AddSingleton<IProfileManager, ProfileManager>();
registrations.AddSingleton<ObjectCache, MemoryCache>(_ => MemoryCache.Default);
registrations.AddSingleton<JsonSerializerOptions>(_ => new JsonSerializerOptions
{
    Converters =
    {
        new JsonStringEnumConverter(JsonNamingPolicy.CamelCase)
    }
});
registrations.AddSingleton<IsolatedStorageFile>(_ => isolatedStorage);
registrations.AddScoped<IConfigResolver, ConfigResolver>();
registrations.AddScoped<IMetadataService, MetadataService>();
registrations.AddScoped<IDotNetGenerator, DotNetGenerator>();
registrations.AddScoped<ITypeScriptGenerator, TypeScriptGenerator>();
registrations.AddScoped<IMetadataGenerator, MetadataGenerator>();
registrations.AddScoped<IFileService, FileService>();
registrations.AddSingleton(appConsole);
registrations.AddSingleton<ShellShimInstaller>();
registrations.AddSingleton<IOrganizationService>(provider => provider.GetRequiredService<IXrmConnection>().ConnectAsync().GetAwaiter().GetResult());
registrations.AddScoped<WebresourcesProcessor>();
var registrar = new TypeRegistrar(registrations);
var app = new CommandApp(registrar);


app.Configure(config =>
{
    var serviceProvider = registrations.BuildServiceProvider();

    var versionCheckInterceptor = serviceProvider.GetRequiredService<VersionCheckInterceptor>();
    var deprecationInterceptor = serviceProvider.GetRequiredService<DeprecationInterceptor>();
    config.SetInterceptor(new CompositeInterceptor(new TelemetryInterceptor(), versionCheckInterceptor, deprecationInterceptor));
    RegisterCommands(config);

    config.SetExceptionHandler((exception, _) =>
    {
        tracer.TrackFatalException(exception);
        var inner = exception.IsDerivedFrom<AbstractPowerException>()
            ? exception.GetInnerException<AbstractPowerException>()
            : null;

        if ((inner ?? exception) is InteractiveLoginRequiredException interactiveEx)
        {
            AnsiConsole.MarkupLineInterpolated(CultureInfo.InvariantCulture, $"[red]{interactiveEx.Message}[/]");
            return (int)ExitCode.AuthRequired;
        }

#if RELEASE
        AnsiConsole.MarkupLineInterpolated(
            $"[red]{(inner?.Message ?? exception.Message)}[/]");
#elif DEBUG
        AnsiConsole.WriteException(exception, ExceptionFormats.ShortenEverything);
#endif

        return (int)ExitCode.Error;
    });

#if DEBUG
    config.ValidateExamples();
#endif
});

if (args.Length == 0)
{
    AnsiConsole.Write(new FigletText("DIGITALL Power").Centered().Color(Color.Blue3));
}


var exitCode = await app.RunAsync(args);

tracerProvider?.ForceFlush(5000);
tracerProvider?.Dispose();

return exitCode;

// ── Command registration ──────────────────────────────────────────────────────
// Shared by the normal app path and the dotnet-suggest capture path.
// Keep in sync with any changes to the command tree.
// ─────────────────────────────────────────────────────────────────────────────
void RegisterCommands(IConfigurator config)
{
    config.Settings.ApplicationName = "dgtp";

    config.AddBranch<ConnectionSettings>("connection", connection =>
    {
        connection.SetDescription("Manages connections to Dataverse environments");
        RegisterConnectionCommands(connection);
    });

    config.AddBranch<ProfileSettings>("profile", profile =>
    {
        profile.SetDescription("[[Deprecated]] Use 'connection' instead");
        RegisterProfileCommands(profile);
    });

    config.AddBranch<ExportVerb>("export", export =>
    {
        export.SetDescription("Exports artifacts from the current Dataverse Environment");
        export.AddExample("export", "bulkdeletes", "--filedir", "c:/TargetDir");
        export.AddCommand<TeamTemplateExport>("teamtemplates")
            .WithDescription("Exports the existing teamtemplates from the current environment");
        export.AddCommand<BulkDeleteExport>("bulkdeletes")
            .WithDescription("Exports the bulk delete jobs from the current environment");
        export.AddCommand<QueueExport>("queues")
            .WithDescription("Exports the existing queues from the current environment");
        export.AddCommand<DocumentTemplateExport>("documenttemplates")
            .WithDescription("Exports the existing document templates from the curent environment");
        export.AddCommand<CalendarExport>("calendars")
            .WithDescription("Exports the existing calendars from the current environment");
        export.AddCommand<SlaConfigExport>("slaconfigs")
            .WithDescription("exports the existing slas from the current environment");
        export.AddCommand<RoutingRuleConfigExport>("routingruleconfigs")
            .WithDescription("Exports the existing routing rules from the current environment");
        export.AddCommand<UserRoleExport>("userroles")
            .WithDescription("Exports the user assigned security roles from the current environment");
        export.AddCommand<OutlookTemplateExport>("outlooktemplates")
            .WithDescription("Exports the existing outlook templates from the current environment");
    });

    config.AddBranch("maintenance",
        maintenance =>
        {
            maintenance.SetDescription("Executes maintenance Tasks against the current Dataverse environment");
            maintenance.AddExample("maintenance", "bulkdelete");
            maintenance.AddCommand<BulkDeleteUtil>("bulkdelete")
                .WithDescription("Starts an bulk delete job for the given fetchXml and waits for completion")
                .WithExample("maintenance", "bulkdelete", "--inline", "<fetchxml>...</fetchxml");
            maintenance.AddCommand<AutoNumberFormatAction>("autonumber")
                .WithDescription("Sets the auto number format for specified columns")
                .WithExample("maintenance", "autonumber", "--config", "./config.json");
            maintenance.AddCommand<ProtectCalculatedFields>("protectfields")
                .WithDescription("Prevents all calculated fields from receiving an active layer.")
                .WithExample("maintenance", "protectfields");
            maintenance.AddCommand<ExportCarrierInfo>("carrierinfo")
                .WithDescription(
                    "Exports all active carriers from an environment to a json file. To see what an carrier is check " +
                    "[link]https://dev.azure.com/ec4u/Dynamics%20DevLab/_wiki/wikis/Dynamics-DevLab.wiki/111/Solution-Concept[/]")
                .WithExample("maintenance", "carrierinfo", "--filedir", "./carriers", "--filename", "carrier.json");
            maintenance.AddCommand<IncrementSolutionVersion>("solution-version")
                .WithDescription("Increments the solution version by given flag")
                .WithExample("maintenance", "solution-version", "sample_solution", "--minor");
            maintenance.AddCommand<CreateWorkflowStateConfig>("createworkflowstate")
                .WithDescription("Creates a workflowstate configuration file")
                .WithExample("maintenance", "createworkflowstate", "--output", "./config.json", "--solutions", "solution1,solution2")
                .WithExample("maintenance", "createworkflowstate", "--output", "./config.json", "--publishers", "publisher1,publisher2");
            maintenance.AddCommand<UpdateWorkflowState>("workflowstate")
                .WithDescription("Updates workflows with given configuration")
                .WithExample("maintenance", "workflowstate", "--config", "./config.json");
            maintenance.AddCommand<RemoveRedundantComponents>("removeredundantcomponents")
                .WithDescription("Removes solution components that are already existing")
                .WithExample("maintenance", "removeredundantcomponents", "deploysolution", "tmpsolution", "--dryrun");
            maintenance.AddCommand<FilterPowerFxPluginSteps>("filterfxplugins")
                .WithDescription("Add Messagefiltering for PowerFx Plugins")
                .WithExample("maintenance", "filterfxplugins", "--config", "./config.json");
            maintenance.AddCommand<EnsureSdkStepStatus>("ensuresdksteps")
                .WithDescription("Ensure sdk steps within a solution are enabled (or disabled)")
                .WithExample("maintenance", "ensuresdksteps", "--solution", "assemblies");

        });

    config.AddBranch<AnalyzeVerb>("analyze", analyze =>
    {
        analyze.SetDescription("Analyze specific Dataverse Artifacts in the current Environment");
        analyze.AddCommand<EntityAllAssetsAnalyze>("entityallassets")
            .WithDescription("Scans the specified solutions for entities containing with assets");
        analyze.AddCommand<NoActiveLayerAnalyze>("noactivelayer")
            .WithDescription("Scans the specified (unmanaged) solutions for components without active layer")
            .WithExample("analyze", "noactivelayer", "--inline", "solution1,solution2");
        analyze.AddCommand<RedundantComponentsAnalyze>("redundantcomponents")
            .WithDescription("Scans for components that are in multiple of the specified solutions");
        analyze.AddCommand<RedundantPatchAnalyze>("redundantpatches")
            .WithDescription("Scans for patch solutions that are not longer needed because all contained elements are not top-layer anymore");
        analyze.AddCommand<ActiveLayerAnalyze>("activelayer")
            .WithDescription("Scans the specified (managed) solutions for components with active layer")
            .WithExample("analyze", "activelayer", "--inline", "solution1,solution2");
        analyze.AddCommand<TopLayerAnalyze>("toplayer")
            .WithDescription("Scans the specified (managed) solutions for components where solution is not top layer")
            .WithExample("analyze", "toplayer", "--inline", "solution1,solution2");
    });

    config.AddBranch<ImportVerb>("import", import =>
    {
        import.SetDescription("import specific artifacts in the current Dataverse environment");
        import.AddExample("import", "outlooktemplates", "--filedir", "c:/TargetDir");
        import.AddCommand<OutlookTemplateImport>("outlooktemplates");
        import.AddCommand<UserRoleImport>("userroles");
        import.AddCommand<QueueImport>("queues");
        import.AddCommand<TeamTemplateImport>("teamtemplates");
        import.AddCommand<BulkDeleteImport>("bulkdeletes");
        import.AddCommand<DocumentTemplateImport>("documenttemplates");
        import.AddCommand<SecureConfigImport>("secureconfigs");
        import.AddCommand<CalendarImport>("calendar");
        import.AddCommand<SlaConfigImport>("slaconfigs");
        import.AddCommand<RoutingRuleConfigImport>("routingruleconfigs");
    });

    config.AddCommand<CodeGenerationCommand>("codegeneration")
        .WithAlias("cg")
        .WithDescription("Generates .cs, .ts and metadata.xml modelfiles for Dataverse")
        .WithExample("codegeneration", "c:/TargetDir", "-c", "genconfig.json");

    config.AddCommand<PushCommand>("push")
        .WithDescription("Import specific Dataverse Artefacts")
        .WithExample("push", "c:/TargetDir/plugin.dll", "--solution", "samplesolution");

    config.AddBranch<CompleteSettings>("complete", complete =>
    {
        complete.SetDescription("Manages shell tab completion for dgtp");
        complete.AddCommand<CompleteSetupCommand>("setup")
            .WithDescription("Registers dgtp with dotnet-suggest and optionally installs the shell shim")
            .WithExample("complete", "setup")
            .WithExample("complete", "setup", "--all")
            .WithExample("complete", "setup", "--all", "--shell", "bash");
        complete.AddCommand<CompleteInstallShellCommand>("install-shell")
            .WithDescription("Installs the dotnet-suggest shell shim into your shell's rc file")
            .WithExample("complete", "install-shell")
            .WithExample("complete", "install-shell", "--shell", "zsh")
            .WithExample("complete", "install-shell", "--dry-run");
    });
}

void RegisterConnectionCommands(IConfigurator<ConnectionSettings> branch)
{
    branch.AddCommand<ListConnectionCommand>("list").WithDescription("List connections");
    branch.AddCommand<CreateConnectionCommand>("create").WithDescription("Create a new connection")
        .WithExample("connection", "create", "<Name>", "--url", "<Url>")
        .WithExample("connection", "create", "<Name>", "--connection-string", "<ConnectionString>");
    branch.AddCommand<SelectConnectionCommand>("select").WithDescription("Select a connection");
    branch.AddCommand<DeleteConnectionCommand>("delete").WithDescription("Delete a connection. Use --all to delete all connections (prompts for confirmation unless --yes is passed).")
        .WithExample("connection", "delete", "<Name>")
        .WithExample("connection", "delete", "--all")
        .WithExample("connection", "delete", "--all", "--yes");
    branch.AddCommand<ConnectionStatusCommand>("status")
        .WithDescription(
            "Checks whether the current MSAL token is still valid without opening a browser. " +
            "Exit code 0 = token valid, 2 = interactive login required. " +
            "Intended as a pre-flight check for coding agents.");
    branch.AddCommand<ConnectionRefreshCommand>("refresh")
        .WithDescription("Forces an interactive MSAL browser login and saves the refreshed token.");
}

void RegisterProfileCommands(IConfigurator<ProfileSettings> branch)
{
    branch.AddCommand<ListProfileCommand>("list").WithDescription("List profiles");
    branch.AddCommand<CreateProfileCommand>("create").WithDescription("Create a new profile")
        .WithExample("profile", "create", "<Name>", "<Url>", "--msal");
    branch.AddCommand<DeleteProfileCommand>("delete").WithDescription("Delete a profile");
    branch.AddCommand<SelectProfileCommand>("select").WithDescription("Select a profile");
    branch.AddCommand<PurgeProfileCommand>("purge").WithDescription("Purge all profiles");
    branch.AddCommand<AuthCheckCommand>("auth-check")
        .WithDescription("Checks whether the current MSAL token is still valid without opening a browser. " +
            "Exit code 0 = token valid, 2 = interactive login required.");
}
