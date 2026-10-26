// SPDX-FileCopyrightText: DIGITALL Nature
// SPDX-License-Identifier: MS-RL

using dgt.power.codegeneration;
using dgt.power.Commands.Complete;
using dgt.power.connection.Base;
using dgt.power.connection.Commands;
using dgt.power.duplicaterules.Commands;
using dgt.power.documenttemplates.Commands;
using dgt.power.plugin.Base;
using dgt.power.plugin.Commands;
using dgt.power.process.Commands;
using dgt.power.solution;
using dgt.power.solution.Base;
using dgt.power.Telemetry;
using dgt.power.telemetry.Base;
using dgt.power.telemetry.Commands;
using dgt.power.teamtemplates.Commands;
using dgt.power.webresource.Base;
using dgt.power.webresource.Commands;
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
        config.Settings.ApplicationVersion = DgtpActivitySource.GetVersion();

        config.AddBranch<ConnectionSettings>("connection", connection =>
        {
            connection.SetDescription("Manages connections to Dataverse environments");
            RegisterConnectionCommands(connection);
        });

        config.AddBranch("teamtemplates", teamtemplates =>
        {
            teamtemplates.SetDescription("Imports and exports Dataverse team templates");
            teamtemplates.AddCommand<TeamTemplateImportCommand>("import")
                .WithDescription("Imports team templates from a versioned file")
                .WithExample("teamtemplates", "import", "./teamtemplates.json", "--dry-run");
            teamtemplates.AddCommand<TeamTemplateExportCommand>("export")
                .WithDescription("Exports team templates to a versioned file")
                .WithExample("teamtemplates", "export", "./teamtemplates.json", "--overwrite");
        });

        config.AddBranch("documenttemplates", templates =>
        {
            templates.SetDescription("Imports and exports active Word and Excel document templates");
            templates.AddCommand<DocumentTemplateImportCommand>("import")
                .WithDescription("Imports a portable document template manifest and activates listed templates")
                .WithExample("documenttemplates", "import", "./templates/documenttemplates.json", "--dry-run");
            templates.AddCommand<DocumentTemplateExportCommand>("export")
                .WithDescription("Exports active document templates as a portable package")
                .WithExample("documenttemplates", "export", "./templates", "--entity", "account", "--type", "word");
        });

        config.AddBranch("process", process =>
        {
            process.SetDescription("Captures and applies explicit process state and ownership");
            process.AddCommand<ProcessInitCommand>("init")
                .WithDescription("Captures process state or ownership from selected solutions")
                .WithExample("process", "init", "./processes.json", "--solution", "ContosoCore")
                .WithExample("process", "init", "./processes.json", "--solution", "ContosoCore",
                    "--capture", "both", "--owner-mapping", "./owners.source.json");
            process.AddCommand<ProcessApplyCommand>("apply")
                .WithDescription("Applies configured process state and ownership by workflow ID")
                .WithExample("process", "apply", "./processes.json", "--dry-run")
                .WithExample("process", "apply", "./processes.json", "--owner-mapping", "./owners.target.json",
                    "--activate-as", "integrationUser");
        });

        config.AddBranch("duplicaterules", duplicateRules =>
        {
            duplicateRules.SetDescription("Captures and applies publication state and ownership of existing duplicate-detection rules");
            duplicateRules.AddCommand<DuplicateRuleInitCommand>("init")
                .WithDescription("Captures publication state or ownership of existing duplicate rules")
                .WithExample("duplicaterules", "init", "./duplicaterules.json", "--entity", "contact")
                .WithExample("duplicaterules", "init", "./duplicaterules.json", "--rule-id", "11111111-1111-1111-1111-111111111111",
                    "--capture", "both", "--owner-mapping", "./owners.source.json");
            duplicateRules.AddCommand<DuplicateRuleApplyCommand>("apply")
                .WithDescription("Applies configured publication state and ownership by duplicate rule ID")
                .WithExample("duplicaterules", "apply", "./duplicaterules.json", "--dry-run")
                .WithExample("duplicaterules", "apply", "./duplicaterules.json", "--owner-mapping", "./owners.target.json",
                    "--timeout", "600");
        });

        config.AddBranch<PluginSettings>("plugin", plugin =>
        {
            plugin.SetDescription("Manages Dataverse plugin assemblies and packages");
            plugin.AddCommand<PluginPushCommand>("push")
                .WithAlias("register")
                .WithDescription("Registers a plugin assembly (.dll) or package (.nupkg), or all such files in a directory")
                .WithExample("plugin", "push", "c:/TargetDir/plugin.dll", "--solution", "samplesolution")
                .WithExample("plugin", "push", "c:/TargetDir/plugin.nupkg", "--publisher-prefix", "contoso", "--solution", "samplesolution")
                .WithExample("plugin", "push", "c:/TargetDir", "--solution", "samplesolution");

            plugin.AddBranch("step", step =>
            {
                step.AddCommand<PluginStepEnableCommand>("enable")
                    .WithDescription("Enables an sdkmessageprocessingstep")
                    .WithExample("plugin", "step", "enable", "--step-id", "<guid>")
                    .WithExample("plugin", "step", "enable", "--plugin-type", "MyNamespace.MyPlugin", "--message", "Update", "--stage", "PreOperation", "--entity", "account");

                step.AddCommand<PluginStepDisableCommand>("disable")
                    .WithDescription("Disables an sdkmessageprocessingstep")
                    .WithExample("plugin", "step", "disable", "--step-id", "<guid>")
                    .WithExample("plugin", "step", "disable", "--plugin-type", "MyNamespace.MyPlugin", "--message", "Update", "--stage", "PreOperation", "--entity", "account");

                step.AddBranch("config", cfg =>
                {
                    cfg.AddCommand<PluginStepConfigSetCommand>("set")
                        .WithDescription("Sets the unsecure and/or secure configuration on an sdkmessageprocessingstep")
                        .WithExample("plugin", "step", "config", "set", "--step-id", "<guid>", "--unsecure", "<value>")
#pragma warning disable S103
                        .WithExample("plugin", "step", "config", "set", "--plugin-type", "MyNamespace.MyPlugin", "--message", "Update", "--stage", "PreOperation", "--entity", "account", "--unsecure", "<value>")
#pragma warning restore S103
                        .WithExample("plugin", "step", "config", "set", "--step-id", "<guid>", "--unsecure-file", "config.json", "--secure-file", "secrets.json");
                });

                step.AddBranch("state", state =>
                {
                    state.AddCommand<PluginStepStateSetCommand>("set")
                        .WithDescription("Applies enable/disable state to multiple plugin steps from a config file")
                        .WithExample("plugin", "step", "state", "set", "-c", "step-states.json");
                });
            });
        });

        config.AddBranch<SolutionSettings>("solution", solution =>
        {
            solution.SetDescription("Commands that act on one or more Dataverse solutions");
            solution.AddCommand<SolutionVersionCommand>("version")
                .WithDescription("Increments the solution version by given flag")
                .WithExample("solution", "version", "sample_solution", "--minor");
            solution.AddCommand<SolutionLintCommand>("lint")
                .WithDescription("Runs the configured linter rule set against the selected solution")
                .WithExample("solution", "lint", "sample_solution", "-c", "lint.config.json")
                .WithExample("solution", "lint", "sample_solution", "-c", "lint.config.json", "--fail-on", "Warning")
                .WithExample("solution", "lint", "sample_solution", "-c", "lint.config.json", "--baseline", "lint-baseline.sarif.json", "--update-baseline")
                .WithExample("solution", "lint", "sample_solution", "-c", "lint.config.json", "--baseline", "lint-baseline.sarif.json", "--sarif-output", "lint.sarif.json");
            solution.AddCommand<CopyComponentsCommand>("copy-components")
                .WithDescription("Copies solution components from one or more source solutions into an unmanaged target solution")
                .WithExample("solution", "copy-components", "target_solution", "--source", "source_solution")
                .WithExample("solution", "copy-components", "target_solution", "--source", "source_solution_a,source_solution_b", "--dry-run")
                .WithExample("solution", "copy-components", "target_solution", "--source", "source_solution", "--raw")
                .WithExample("solution", "copy-components", "target_solution", "--source", "source_solution", "--apps", "strip");
        });

        config.AddCommand<CodeGenerationCommand>("codegeneration")
            .WithAlias("cg")
            .WithDescription("Generates .cs, .ts and metadata.xml modelfiles for Dataverse")
            .WithExample("codegeneration", "c:/TargetDir", "-c", "genconfig.json");

        config.AddBranch<WebResourceSettings>("webresource", webresource =>
        {
            webresource.SetDescription("Manages Dataverse webresources");
            webresource.AddCommand<WebResourcePushCommand>("push")
                .WithDescription("Pushes webresources from a directory or a single file")
                .WithExample("webresource", "push", "c:/TargetDir/webresources", "--publisher-prefix", "sample")
                .WithExample("webresource", "push", "c:/TargetDir/main.js", "--name", "sample_/main.js");
        });

        config.AddBranch<TelemetrySettings>("telemetry", telemetry =>
        {
            telemetry.SetDescription("Manages telemetry settings");
            telemetry.AddCommand<TelemetryStatusCommand>("status")
                .WithDescription("Shows the current telemetry state")
                .WithExample("telemetry", "status");
            telemetry.AddCommand<TelemetryEnableCommand>("enable")
                .WithDescription("Enables telemetry")
                .WithExample("telemetry", "enable");
            telemetry.AddCommand<TelemetryDisableCommand>("disable")
                .WithDescription("Disables telemetry")
                .WithExample("telemetry", "disable");
        });

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

    private static void RegisterConnectionCommands(IConfigurator<ConnectionSettings> branch)
    {
        branch.AddCommand<ListConnectionCommand>("list").WithDescription("List connections");
        branch.AddCommand<CreateConnectionCommand>("create").WithDescription("Create a new connection")
            .WithExample("connection", "create", "dev", "--url", "<Url>")
            .WithExample("connection", "create", "test", "--url", "<Url>", "--tenant", "<Tenant>", "--client-id", "<ClientId>", "--client-secret", "<Secret>")
            .WithExample("connection", "create", "prod", "--url", "<Url>", "--tenant", "<Tenant>", "--client-id", "<ClientId>", "--certificate-thumbprint", "<Thumbprint>");
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

}
