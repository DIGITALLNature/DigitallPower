// Copyright (c) DIGITALL Nature. All rights reserved
// DIGITALL Nature licenses this file to you under the Microsoft Public License.

using dgt.power.analyzer.Base;
using dgt.power.analyzer.Logic;
using dgt.power.codegeneration;
using dgt.power.Commands.Complete;
using dgt.power.export.Base;
using dgt.power.export.Logic;
using dgt.power.import.Base;
using dgt.power.import.Logic;
using dgt.power.maintenance.Logic;
using dgt.power.profile.Base;
using dgt.power.profile.Commands;
using dgt.power.push;
using Spectre.Console.Cli;

namespace dgt.power;

/// <summary>
/// Builds the full dgtp command tree.
/// Shared by the normal app path (<see cref="Program"/>) and the dotnet-suggest capture
/// path (<see cref="Completion.DotnetSuggestHandler"/>), and directly testable via
/// <c>Spectre.Console.Cli.Testing</c> without bootstrapping the rest of the application
/// (DI, telemetry, NuGet, ...).
/// Keep in sync with any changes to the command tree.
/// </summary>
internal static class CommandTree
{
    public static void Register(IConfigurator config)
    {
        config.Settings.ApplicationName = "dgtp";

        config.AddBranch<ProfileSettings>("profile", profile =>
        {
            profile.SetDescription("Handles Authentication");
            profile.AddCommand<ListProfileCommand>("list").WithDescription("List profiles");
            profile.AddCommand<CreateProfileCommand>("create").WithDescription("Create a new profile")
                .WithExample("profile", "create", "<Name>", "<Url>", "--msal");
            profile.AddCommand<DeleteProfileCommand>("delete").WithDescription("Delete a profile");
            profile.AddCommand<SelectProfileCommand>("select").WithDescription("Select a profile");
            profile.AddCommand<PurgeProfileCommand>("purge").WithDescription("Purge all profiles");
            profile.AddCommand<AuthCheckCommand>("auth-check")
                .WithDescription(
                    "Checks whether the current MSAL token is still valid without opening a browser. " +
                    "Exit code 0 = token valid, 2 = interactive login required. " +
                    "Intended as a pre-flight check for coding agents.");
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
}
