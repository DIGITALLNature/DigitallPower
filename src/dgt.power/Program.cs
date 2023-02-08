using System.IO.IsolatedStorage;
using System.Runtime.Caching;
using dgt.power;
using dgt.power.analyzer.Base;
using dgt.power.analyzer.Logic;
using dgt.power.codegeneration;
using dgt.power.codegeneration.Generators;
using dgt.power.codegeneration.Logic;
using dgt.power.codegeneration.Services;
using dgt.power.codegeneration.Services.Contracts;
using dgt.power.common;
using dgt.power.common.Commands;
using dgt.power.common.Exceptions;
using dgt.power.common.Extensions;
using dgt.power.common.Logic;
using dgt.power.dataverse;
using dgt.power.export.Base;
using dgt.power.export.Logic;
using dgt.power.import.Base;
using dgt.power.import.Logic;
using dgt.power.maintenance.Logic;
using dgt.power.profile.Base;
using dgt.power.profile.Commands;
using dgt.power.push;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Xrm.Sdk;
using Spectre.Console;
using Spectre.Console.Cli;

var configuration = new ConfigurationBuilder()
    .SetBasePath(Directory.GetCurrentDirectory())
    .AddJsonFile("dgtp.json", optional: true)
    .AddEnvironmentVariables("dgtp:")
    .Build();

var registrations = new ServiceCollection();
registrations.AddSingleton<ITracer, Tracer>();
registrations.AddSingleton<IConfiguration>(configuration);
registrations.AddSingleton<IXrmConnection, XrmConnection>();
registrations.AddSingleton<TypescriptCommand, TypescriptCommand>();
registrations.AddSingleton<MetadataCommand, MetadataCommand>();
registrations.AddSingleton<IProfileManager, ProfileManager>();
registrations.AddSingleton<ObjectCache, MemoryCache>(_ => MemoryCache.Default);
registrations.AddSingleton<IsolatedStorageFile>(_ => IsolatedStorageFile.GetUserStoreForApplication());
registrations.AddScoped<IConfigResolver, ConfigResolver>();
registrations.AddScoped<IMetadataService, MetadataService>();
registrations.AddScoped<IDotNetGenerator, DotNetGenerator>();
registrations.AddScoped<ITypescriptGenerator, TypescriptGenerator>();
registrations.AddScoped<IMetadataGenerator, MetadataGenerator>();
registrations.AddScoped<DotNetCommand, DotNetCommand>();
registrations.AddSingleton<IOrganizationService>(provider => provider.GetRequiredService<IXrmConnection>().Connect());
var registrar = new TypeRegistrar(registrations);
var app = new CommandApp(registrar);


app.Configure(config =>
{
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
        export.AddExample(new[] {"export", "bulkdeletes", "--filedir", "c:/TargetDir"});
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
            maintenance.AddExample(new[] {"maintenance", "bulkdelete"});
            maintenance.AddCommand<BulkDeleteUtil>("bulkdelete")
                .WithDescription("Starts an bulk delete job for the given fetchXml and waits for completion")
                .WithExample(new[] {"maintenance", "bulkdelete", "--inline", "<fetchxml>...</fetchxml"});
            maintenance.AddCommand<AutoNumberFormatAction>("autonumber")
                .WithDescription("Sets the auto number format for specified columns")
                .WithExample(new[] {"maintenance", "autonumber", "--config", "./config.json"});
            maintenance.AddCommand<ProtectCalculatedFields>("protectfields")
                .WithDescription("Prevents all calculated fields from receiving an active layer.")
                .WithExample(new[] { "maintenance", "protectfields" });
        });

    config.AddBranch<AnalyzeVerb>("analyze", analyze =>
    {
        analyze.SetDescription("Analyze specific Dataverse Artifacts in the current Environment");
        analyze.AddCommand<EntityAllAssetsAnalyze>("entityallassets")
            .WithDescription("Scans the specified solutions for entities containing with assets");
        analyze.AddCommand<NoActiveLayerAnalyze>("noactivelayer")
            .WithDescription("Scans the specified (unmanaged) solutions for components without active layer")
            .WithExample(new[] { "analyze", "noactivelayer", "--inline", "solution1,solution2" });
        analyze.AddCommand<RedundantComponentsAnalyze>("redundantcomponents")
            .WithDescription("Scans for components that are in multiple of the specified solutions");
        analyze.AddCommand<ActiveLayerAnalyze>("activelayer")
            .WithDescription("Scans the specified (managed) solutions for components with active layer")
            .WithExample(new[] { "analyze", "activelayer", "--inline", "solution1,solution2" });
        analyze.AddCommand<TopLayerAnalyze>("toplayer")
            .WithDescription("Scans the specified (managed) solutions for components where solution is not top layer")
            .WithExample(new[] { "analyze", "toplayer", "--inline", "solution1,solution2" });
    });

    config.AddBranch<ImportVerb>("import", import =>
    {
        import.SetDescription("import specific artifacts in the current Dataverse environment");
        import.AddExample(new[] {"import", "outlooktemplates", "--filedir", "c:/TargetDir"});
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
        .WithExample(new[] {"codegeneration", "c:/TargetDir", "-c", "genconfig.json"});

    config.AddCommand<PushCommand>("push")
        .WithDescription("Import specific Dataverse Artefacts")
        .WithExample(new[] {"push", "c:/TargetDir/plugin.dll", "--solution", "samplesolution"});

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

        AnsiConsole.MarkupLine($"[red]{errorMessage}[/]");
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
