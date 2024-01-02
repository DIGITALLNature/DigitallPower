// Copyright (c) DIGITALL Nature. All rights reserved
// DIGITALL Nature licenses this file to you under the Microsoft Public License.

using System.IO.IsolatedStorage;
using System.Runtime.Caching;
using System.Text.Json;
using System.Text.Json.Serialization;
using dgt.power;
using dgt.power.analyzer.Base;
using dgt.power.analyzer.Logic;
using dgt.power.codegeneration;
using dgt.power.codegeneration.Generators;
using dgt.power.codegeneration.Logic;
using dgt.power.codegeneration.Services;
using dgt.power.codegeneration.Services.Contracts;
using dgt.power.common;
using dgt.power.common.FileAccess;
using dgt.power.common.Logic;
using dgt.power.export.Base;
using dgt.power.export.Logic;
using dgt.power.import.Base;
using dgt.power.import.Logic;
using dgt.power.maintenance.Logic;
using dgt.power.profile.Base;
using dgt.power.profile.Commands;
using dgt.power.push;
using dgt.power.push.Logic;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Xrm.Sdk;
using NuGet.Protocol;
using NuGet.Protocol.Core.Types;
using Spectre.Console;
using Spectre.Console.Cli;

var configuration = new ConfigurationBuilder()
    .SetBasePath(Directory.GetCurrentDirectory())
    .AddJsonFile("dgtp.json", optional: true)
    .AddEnvironmentVariables("dgtp:")
    .Build();

var registrations = new ServiceCollection();
registrations.AddSingleton<PackageMetadataResource>(_ => Repository.Factory
    .GetCoreV3("https://api.nuget.org/v3/index.json")
    .GetResource<PackageMetadataResource>()
);
registrations.AddSingleton<VersionCheckInterceptor>();
registrations.AddSingleton<ITracer, Tracer>();
registrations.AddSingleton<IConfiguration>(configuration);
registrations.AddSingleton<IXrmConnection, XrmConnection>();
registrations.AddSingleton<TypescriptCommand, TypescriptCommand>();
registrations.AddSingleton<MetadataCommand, MetadataCommand>();
registrations.AddSingleton<IProfileManager, ProfileManager>();
registrations.AddSingleton<ObjectCache, MemoryCache>(_ => MemoryCache.Default);
registrations.AddSingleton<JsonSerializerOptions>(_ => new JsonSerializerOptions
{
    Converters =
    {
        new JsonStringEnumConverter(JsonNamingPolicy.CamelCase)
    }
});
registrations.AddSingleton<IsolatedStorageFile>(_ => IsolatedStorageFile.GetUserStoreForAssembly());
registrations.AddScoped<IConfigResolver, ConfigResolver>();
registrations.AddScoped<IMetadataService, MetadataService>();
registrations.AddScoped<IDotNetGenerator, DotNetGenerator>();
registrations.AddScoped<ITypescriptGenerator, TypescriptGenerator>();
registrations.AddScoped<IMetadataGenerator, MetadataGenerator>();
registrations.AddScoped<DotNetCommand, DotNetCommand>();
registrations.AddScoped<IFileService, FileService>();
registrations.AddSingleton<IOrganizationService>(provider => provider.GetRequiredService<IXrmConnection>().Connect());
registrations.AddScoped<WebresourcesProcessor>();
var registrar = new TypeRegistrar(registrations);
var app = new CommandApp(registrar);


app.Configure(config =>
{
    var versionCheckInterceptor = registrations.BuildServiceProvider().GetRequiredService<VersionCheckInterceptor>();
    config.SetInterceptor(versionCheckInterceptor);
    config.AddBranch<ProfileSettings>("profile", profile =>
    {
        profile.SetDescription("Handles Authentication");
        profile.AddCommand<ListProfileCommand>("list").WithDescription("List profiles");
        profile.AddCommand<CreateProfileCommand>("create").WithDescription("Create a new profile");
        profile.AddCommand<DeleteProfileCommand>("delete").WithDescription("Delete a profile");
        profile.AddCommand<SelectProfileCommand>("select").WithDescription("Select a profile");
        profile.AddCommand<PurgeProfileCommand>("purge").WithDescription("Purge all profiles");
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
            maintenance.AddCommand<UpdateWorkflowState>("workflowstate")
                .WithDescription("Updates workflows with given configuration")
                .WithExample("maintenance", "workflowstate", "--config", "./config.json");
            maintenance.AddCommand<RemoveRedundantComponents>("removeredundantcomponents")
                .WithDescription("Removes solution components that are already existing")
                .WithExample("maintenance","removeredundantcomponents","deploysolution","tmpsolution","--dryrun");
            maintenance.AddCommand<FilterPowerFxPluginSteps>("filterfxplugins")
                .WithDescription("Add Messagefiltering for PowerFx Plugins")
                .WithExample("maintenance","filterfxplugins","--config", "./config.json");

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

    config.Settings.ApplicationName = "dgtp";

#if RELEASE
    config.SetExceptionHandler(exception =>
    {
        var errorMessage = exception.Message;
        if (exception.IsDerivedFrom<AbstractPowerException>())
        {
            var innerException = exception.GetInnerException<AbstractPowerException>();
            errorMessage = innerException!.Message;
        }

        AnsiConsole.MarkupLineInterpolated($"[red]{errorMessage}[/]");
        return (int)ExitCode.Error;
    });
#endif
#if DEBUG
    config.PropagateExceptions();
    config.ValidateExamples();
#endif
});

if (args.Length == 0)
{
    AnsiConsole.Write(new FigletText("DIGITALL Power").Centered().Color(Color.Blue3));
}


return app.Run(args);
