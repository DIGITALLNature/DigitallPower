// Copyright (c) DIGITALL Nature. All rights reserved
// DIGITALL Nature licenses this file to you under the Microsoft Public License.

using System.Globalization;
using System.IO.IsolatedStorage;
using System.Runtime.Caching;
using System.Text;
using System.Text.Json;
using System.Text.Json.Serialization;
using Azure.Monitor.OpenTelemetry.Exporter;
using dgt.power;
using dgt.power.codegeneration.Generators;
using dgt.power.codegeneration.Generators.Contracts;
using dgt.power.codegeneration.Services;
using dgt.power.codegeneration.Services.Contracts;
using dgt.power.Commands.Complete;
using dgt.power.common;
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
using dgt.power.plugin;
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
    return await DotnetSuggestHandler.HandleAsync(args, CommandTree.Register);
}
// ─────────────────────────────────────────────────────────────────────────────

Console.OutputEncoding = Encoding.UTF8;

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
void FlushAndDisposeTelemetryProvider()
{
    var provider = Interlocked.Exchange(ref tracerProvider, null);
    if (provider is null)
    {
        return;
    }

    provider.ForceFlush(5000);
    provider.Dispose();
}

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

UnhandledExceptionEventHandler unhandledExceptionHandler = (_, e) =>
{
    if (e.ExceptionObject is Exception ex)
    {
        tracer.TrackFatalException(ex);
    }
};

EventHandler<UnobservedTaskExceptionEventArgs> unobservedTaskExceptionHandler = (_, e) =>
{
    tracer.TrackFatalException(e.Exception);
    e.SetObserved();
};

AppDomain.CurrentDomain.UnhandledException += unhandledExceptionHandler;
TaskScheduler.UnobservedTaskException += unobservedTaskExceptionHandler;

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
    CommandTree.Register(config);

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

        if ((inner ?? exception) is WorkflowActivityNotSupportedException notSupportedEx)
        {
            AnsiConsole.MarkupLineInterpolated(CultureInfo.InvariantCulture, $"[red]{notSupportedEx.Message}[/]");
            return (int)ExitCode.NotSupported;
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


try
{
    return await app.RunAsync(args);
}
finally
{
    AppDomain.CurrentDomain.UnhandledException -= unhandledExceptionHandler;
    TaskScheduler.UnobservedTaskException -= unobservedTaskExceptionHandler;
    FlushAndDisposeTelemetryProvider();
}
