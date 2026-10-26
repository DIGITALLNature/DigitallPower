// Copyright (c) DIGITALL Nature. All rights reserved
// DIGITALL Nature licenses this file to you under the Microsoft Public License.

using dgt.power.Completion;
using Spectre.Console.Cli;
using Spectre.Console.Cli.Testing;

namespace dgt.power.cli.tests;

/// <summary>
/// Tier 1: structural wiring tests for the real dgtp command tree (<see cref="CommandTree"/>).
/// These tests exercise Spectre.Console.Cli's own model building (command/branch/alias
/// registration and example validation) against the exact same registration code path used
/// by <c>Program.cs</c> and the dotnet-suggest handler - without bootstrapping the rest of the
/// application (DI, telemetry, NuGet, Dataverse connection).
/// <para>
/// Building the <c>ICommandModel</c> (triggered by any <c>Run</c>/<c>--help</c> invocation)
/// walks the *entire* configured tree, so a single invocation is enough to catch:
/// duplicate command names/aliases, broken branch registration, and invalid
/// <c>WithExample(...)</c> calls.
/// </para>
/// </summary>
public class CommandTreeTests
{
    [Test]
    public async Task Register_WithValidateExamples_BuildsModelWithoutThrowing()
    {
        var tester = new CommandAppTester();
        tester.Configure(config =>
        {
            CommandTree.Register(config);
            config.ValidateExamples();
        });

        var result = tester.Run("--help");

        await Assert.That(result.ExitCode).IsEqualTo(0);
    }

    // Belt-and-braces: verify every top-level branch/command is independently reachable.
    // (Model building already covers the whole tree above; this additionally guards against
    // routing issues that only surface when actually navigating into a specific path.)
    [Test]
    [Arguments("connection")]
    [Arguments("maintenance")]
    [Arguments("analyze")]
    [Arguments("solution")]
    [Arguments("codegeneration")]
    [Arguments("cg")] // alias for codegeneration
    [Arguments("plugin")]
    [Arguments("process")]
    [Arguments("teamtemplates")]
    [Arguments("duplicaterules")]
    [Arguments("documenttemplates")]
    [Arguments("telemetry")]
    [Arguments("webresource")]
    [Arguments("complete")]
    public async Task TopLevelPath_HelpInvocation_Succeeds(string path)
    {
        var tester = new CommandAppTester();
        tester.Configure(CommandTree.Register);

        var result = tester.Run(path, "--help");

        await Assert.That(result.ExitCode).IsEqualTo(0);
    }

    [Test]
    [Arguments("push")]
    [Arguments("register")] // alias for push
    public async Task PluginSubCommand_HelpInvocation_Succeeds(string subCommand)
    {
        var tester = new CommandAppTester();
        tester.Configure(CommandTree.Register);

        var result = tester.Run("plugin", subCommand, "--help");

        await Assert.That(result.ExitCode).IsEqualTo(0);
    }

    [Test]
    public async Task PluginStepSubCommand_HelpInvocation_Succeeds()
    {
        var tester = new CommandAppTester();
        tester.Configure(CommandTree.Register);

        var result = tester.Run("plugin", "step", "--help");

        await Assert.That(result.ExitCode).IsEqualTo(0);
    }

    [Test]
    public async Task PluginStepConfigSet_HelpInvocation_Succeeds()
    {
        var tester = new CommandAppTester();
        tester.Configure(CommandTree.Register);

        var result = tester.Run("plugin", "step", "config", "set", "--help");

        await Assert.That(result.ExitCode).IsEqualTo(0);
    }

    [Test]
    public async Task ImportSecureConfigs_IsRejectedAsUnknownCommand()
    {
        var tester = new CommandAppTester();
        tester.Configure(CommandTree.Register);

        var result = tester.Run("import", "secureconfigs");

        await Assert.That(result.ExitCode).IsNotEqualTo(0);
        await Assert.That(result.Output).Contains("Unknown command");
    }

    [Test]
    [Arguments("import", "bulkdeletes")]
    [Arguments("export", "bulkdeletes")]
    [Arguments("import", "queues")]
    [Arguments("export", "queues")]
    [Arguments("import", "calendar")]
    [Arguments("export", "calendars")]
    [Arguments("import", "slaconfigs")]
    [Arguments("export", "slaconfigs")]
    [Arguments("import", "routingruleconfigs")]
    [Arguments("export", "routingruleconfigs")]
    [Arguments("import", "userroles")]
    [Arguments("export", "userroles")]
    [Arguments("import", "outlooktemplates")]
    [Arguments("export", "outlooktemplates")]
    public async Task RetiredTransferPath_IsRejectedWithoutResolvingServices(string branch, string resource)
    {
        var tester = new CommandAppTester();
        tester.Configure(CommandTree.Register);

        var result = tester.Run(branch, resource);

        await Assert.That(result.ExitCode).IsNotEqualTo(0);
        await Assert.That(result.Output).Contains("Unknown command");
    }

    [Test]
    [Arguments("import")]
    [Arguments("export")]
    public async Task RetiredTransferBranch_HelpIsRejected(string branch)
    {
        var tester = new CommandAppTester();
        tester.Configure(CommandTree.Register);

        var result = tester.Run(branch, "--help");

        await Assert.That(result.ExitCode).IsNotEqualTo(0);
        await Assert.That(result.Output).Contains("Unknown command");
    }

    [Test]
    public async Task TransferHelpAndCompletion_OnlyExposeResourceBranches()
    {
        var capture = new ModelCaptureHelpProvider();
        var tester = new CommandAppTester();
        tester.Configure(config =>
        {
            CommandTree.Register(config);
            config.SetHelpProvider(capture);
        });

        var result = tester.Run("--help");

        await Assert.That(result.ExitCode).IsEqualTo(0);
        await Assert.That(capture.Model).IsNotNull();
        var rootCommands = capture.Model!.Commands.Select(command => command.Name).ToArray();
        await Assert.That(rootCommands).DoesNotContain("import");
        await Assert.That(rootCommands).DoesNotContain("export");
        await Assert.That(rootCommands).Contains("teamtemplates");
        await Assert.That(rootCommands).Contains("documenttemplates");

        const string rootLine = "dgtp ";
        var root = CompletionEngine.GetCompletions(capture.Model, rootLine, rootLine.Length);
        await Assert.That(root).DoesNotContain("import");
        await Assert.That(root).DoesNotContain("export");
        await Assert.That(root).Contains("teamtemplates");
        await Assert.That(root).Contains("documenttemplates");

        foreach (var branch in new[] { "import", "export" })
        {
            var line = $"dgtp {branch} ";
            var completions = CompletionEngine.GetCompletions(capture.Model, line, line.Length);
            await Assert.That(completions.Count).IsEqualTo(0);
        }
    }

    [Test]
    [Arguments("import")]
    [Arguments("export")]
    public async Task TeamTemplatesSubCommand_HelpInvocation_Succeeds(string subCommand)
    {
        var tester = new CommandAppTester();
        tester.Configure(CommandTree.Register);

        var result = tester.Run("teamtemplates", subCommand, "--help");

        await Assert.That(result.ExitCode).IsEqualTo(0);
    }

    [Test]
    [Arguments("import")]
    [Arguments("export")]
    public async Task DocumentTemplateSubCommand_HelpInvocation_Succeeds(string operation)
    {
        var tester = new CommandAppTester();
        tester.Configure(CommandTree.Register);
        var result = tester.Run("documenttemplates", operation, "--help");
        await Assert.That(result.ExitCode).IsEqualTo(0);
    }

    [Test]
    [Arguments("import")]
    [Arguments("export")]
    public async Task LegacyTeamTemplatesCommand_IsRejected(string verb)
    {
        var tester = new CommandAppTester();
        tester.Configure(CommandTree.Register);

        var result = tester.Run(verb, "teamtemplates");

        await Assert.That(result.ExitCode).IsNotEqualTo(0);
        await Assert.That(result.Output).Contains("Unknown command");
    }

    [Test]
    [Arguments("import")]
    [Arguments("export")]
    public async Task LegacyDocumentTemplatePath_IsRejected(string operation)
    {
        var tester = new CommandAppTester();
        tester.Configure(CommandTree.Register);
        var result = tester.Run(operation, "documenttemplates");
        await Assert.That(result.ExitCode).IsNotEqualTo(0);
        await Assert.That(result.Output).Contains("Unknown command");
    }

    [Test]
    public async Task DocumentTemplateCompletion_ContainsResourcePathsNotLegacyPaths()
    {
        var capture = new ModelCaptureHelpProvider();
        var tester = new CommandAppTester();
        tester.Configure(config =>
        {
            CommandTree.Register(config);
            config.SetHelpProvider(capture);
        });
        var result = tester.Run("--help");
        await Assert.That(result.ExitCode).IsEqualTo(0);
        await Assert.That(capture.Model).IsNotNull();
        const string rootLine = "dgtp ";
        const string resourceLine = "dgtp documenttemplates ";
        const string importLine = "dgtp documenttemplates import --";
        const string exportLine = "dgtp documenttemplates export --";
        const string legacyImportLine = "dgtp import ";
        const string legacyExportLine = "dgtp export ";
        var root = CompletionEngine.GetCompletions(capture.Model!, rootLine, rootLine.Length);
        var resource = CompletionEngine.GetCompletions(capture.Model!, resourceLine, resourceLine.Length);
        var import = CompletionEngine.GetCompletions(capture.Model!, importLine, importLine.Length);
        var export = CompletionEngine.GetCompletions(capture.Model!, exportLine, exportLine.Length);
        var legacyImport = CompletionEngine.GetCompletions(capture.Model!, legacyImportLine, legacyImportLine.Length);
        var legacyExport = CompletionEngine.GetCompletions(capture.Model!, legacyExportLine, legacyExportLine.Length);
        await Assert.That(root).Contains("documenttemplates");
        await Assert.That(resource).Contains("import");
        await Assert.That(resource).Contains("export");
        await Assert.That(import).Contains("--dry-run");
        await Assert.That(export).Contains("--entity");
        await Assert.That(export).Contains("--type");
        await Assert.That(export).Contains("--overwrite");
        await Assert.That(legacyImport).DoesNotContain("documenttemplates");
        await Assert.That(legacyExport).DoesNotContain("documenttemplates");
    }

    [Test]
    [Arguments("init")]
    [Arguments("apply")]
    public async Task ProcessSubCommand_HelpInvocation_Succeeds(string subCommand)
    {
        var tester = new CommandAppTester();
        tester.Configure(CommandTree.Register);

        var result = tester.Run("process", subCommand, "--help");

        await Assert.That(result.ExitCode).IsEqualTo(0);
    }

    [Test]
    [Arguments("init")]
    [Arguments("apply")]
    public async Task DuplicateRulesSubCommand_HelpInvocation_Succeeds(string subCommand)
    {
        var tester = new CommandAppTester();
        tester.Configure(CommandTree.Register);

        var result = tester.Run("duplicaterules", subCommand, "--help");

        await Assert.That(result.ExitCode).IsEqualTo(0);
    }

    [Test]
    public async Task DuplicateRulesCompletion_UsesResourceCommandsAndOptions()
    {
        var capture = new ModelCaptureHelpProvider();
        var tester = new CommandAppTester();
        tester.Configure(config =>
        {
            CommandTree.Register(config);
            config.SetHelpProvider(capture);
        });
        var result = tester.Run("--help");

        await Assert.That(result.ExitCode).IsEqualTo(0);
        await Assert.That(capture.Model).IsNotNull();
        const string rootLine = "dgtp ";
        const string branchLine = "dgtp duplicaterules ";
        const string initLine = "dgtp duplicaterules init --";
        const string applyLine = "dgtp duplicaterules apply --";
        var root = CompletionEngine.GetCompletions(capture.Model!, rootLine, rootLine.Length);
        var branch = CompletionEngine.GetCompletions(capture.Model!, branchLine, branchLine.Length);
        var init = CompletionEngine.GetCompletions(capture.Model!, initLine, initLine.Length);
        var apply = CompletionEngine.GetCompletions(capture.Model!, applyLine, applyLine.Length);

        await Assert.That(root).Contains("duplicaterules");
        await Assert.That(branch).Contains("init");
        await Assert.That(branch).Contains("apply");
        await Assert.That(init).Contains("--entity");
        await Assert.That(init).Contains("--rule-id");
        await Assert.That(init).Contains("--capture");
        await Assert.That(init).Contains("--owner-mapping");
        await Assert.That(init).Contains("--overwrite");
        await Assert.That(apply).Contains("--owner-mapping");
        await Assert.That(apply).Contains("--dry-run");
        await Assert.That(apply).Contains("--no-wait");
        await Assert.That(apply).Contains("--timeout");
    }

    [Test]
    [Arguments("createworkflowstate")]
    [Arguments("workflowstate")]
    public async Task LegacyWorkflowCommand_IsRejected(string command)
    {
        var tester = new CommandAppTester();
        tester.Configure(CommandTree.Register);

        var result = tester.Run("maintenance", command);

        await Assert.That(result.ExitCode).IsNotEqualTo(0);
        await Assert.That(result.Output).Contains("Unknown command");
    }

    [Test]
    public async Task MaintenanceHelp_DoesNotListLegacyWorkflowCommands_AndKeepsOtherCommands()
    {
        var tester = new CommandAppTester();
        tester.Configure(CommandTree.Register);

        var result = tester.Run("maintenance", "--help");

        await Assert.That(result.ExitCode).IsEqualTo(0);
        await Assert.That(result.Output).DoesNotContain("createworkflowstate");
        await Assert.That(result.Output).DoesNotContain("workflowstate");
        await Assert.That(result.Output).Contains("bulkdelete");
    }

    [Test]
    public async Task ProcessCompletion_UsesResourceCommandsAndOptions_WithoutLegacyPaths()
    {
        var capture = new ModelCaptureHelpProvider();
        var tester = new CommandAppTester();
        tester.Configure(config =>
        {
            CommandTree.Register(config);
            config.SetHelpProvider(capture);
        });
        var result = tester.Run("--help");

        await Assert.That(result.ExitCode).IsEqualTo(0);
        await Assert.That(capture.Model).IsNotNull();
        const string rootLine = "dgtp ";
        const string branchLine = "dgtp process ";
        const string initLine = "dgtp process init --";
        const string applyLine = "dgtp process apply --";
        const string maintenanceLine = "dgtp maintenance ";
        var root = CompletionEngine.GetCompletions(capture.Model!, rootLine, rootLine.Length);
        var branch = CompletionEngine.GetCompletions(capture.Model!, branchLine, branchLine.Length);
        var init = CompletionEngine.GetCompletions(capture.Model!, initLine, initLine.Length);
        var apply = CompletionEngine.GetCompletions(capture.Model!, applyLine, applyLine.Length);
        var maintenance = CompletionEngine.GetCompletions(capture.Model!, maintenanceLine, maintenanceLine.Length);

        await Assert.That(root).Contains("process");
        await Assert.That(branch).Contains("init");
        await Assert.That(branch).Contains("apply");
        await Assert.That(init).Contains("--solution");
        await Assert.That(init).Contains("--capture");
        await Assert.That(init).Contains("--owner-mapping");
        await Assert.That(init).Contains("--overwrite");
        await Assert.That(apply).Contains("--owner-mapping");
        await Assert.That(apply).Contains("--activate-as");
        await Assert.That(apply).Contains("--activate-as-owner");
        await Assert.That(apply).Contains("--dry-run");
        await Assert.That(maintenance).DoesNotContain("createworkflowstate");
        await Assert.That(maintenance).DoesNotContain("workflowstate");
    }

    [Test]
    public async Task TeamTemplateCompletion_UsesResourceCommandsAndRepeatableOptions()
    {
        var capture = new ModelCaptureHelpProvider();
        var tester = new CommandAppTester();
        tester.Configure(config =>
        {
            CommandTree.Register(config);
            config.SetHelpProvider(capture);
        });
        var result = tester.Run("--help");

        await Assert.That(result.ExitCode).IsEqualTo(0);
        await Assert.That(capture.Model).IsNotNull();

        const string rootLine = "dgtp ";
        const string branchLine = "dgtp teamtemplates ";
        const string importLine = "dgtp teamtemplates import --";
        const string exportLine = "dgtp teamtemplates export --";
        var root = CompletionEngine.GetCompletions(capture.Model!, rootLine, rootLine.Length);
        var branch = CompletionEngine.GetCompletions(capture.Model!, branchLine, branchLine.Length);
        var import = CompletionEngine.GetCompletions(capture.Model!, importLine, importLine.Length);
        var export = CompletionEngine.GetCompletions(capture.Model!, exportLine, exportLine.Length);

        await Assert.That(root).Contains("teamtemplates");
        await Assert.That(branch).Contains("import");
        await Assert.That(branch).Contains("export");
        await Assert.That(import).Contains("--dry-run");
        await Assert.That(import).Contains("--cleanup-table");
        await Assert.That(export).Contains("--dry-run");
        await Assert.That(export).Contains("--table");
        await Assert.That(export).Contains("--overwrite");
    }

    [Test]
    public async Task RootHelp_DoesNotListRetiredTransfers_AndKeepsTemplateResources()
    {
        var tester = new CommandAppTester();
        tester.Configure(CommandTree.Register);

        var result = tester.Run("--help");

        await Assert.That(result.ExitCode).IsEqualTo(0);
        await Assert.That(result.Output).DoesNotContain("secureconfigs");
        await Assert.That(result.Output).DoesNotContain("outlooktemplates");
        await Assert.That(result.Output).DoesNotContain("bulkdeletes");
        await Assert.That(result.Output).Contains("teamtemplates");
        await Assert.That(result.Output).Contains("documenttemplates");
    }

    [Test]
    public async Task PluginStepConfigSet_HelpListsSecureConfigurationOptions()
    {
        var tester = new CommandAppTester();
        tester.Configure(CommandTree.Register);

        var result = tester.Run("plugin", "step", "config", "set", "--help");

        await Assert.That(result.ExitCode).IsEqualTo(0);
        await Assert.That(result.Output).Contains("--secure");
        await Assert.That(result.Output).Contains("--secure-file");
    }

    [Test]
    public async Task PluginStepEnable_HelpInvocation_Succeeds()
    {
        var tester = new CommandAppTester();
        tester.Configure(CommandTree.Register);

        var result = tester.Run("plugin", "step", "enable", "--help");

        await Assert.That(result.ExitCode).IsEqualTo(0);
    }

    [Test]
    public async Task PluginStepDisable_HelpInvocation_Succeeds()
    {
        var tester = new CommandAppTester();
        tester.Configure(CommandTree.Register);

        var result = tester.Run("plugin", "step", "disable", "--help");

        await Assert.That(result.ExitCode).IsEqualTo(0);
    }

    [Test]
    public async Task PluginStepStateSet_HelpInvocation_Succeeds()
    {
        var tester = new CommandAppTester();
        tester.Configure(CommandTree.Register);

        var result = tester.Run("plugin", "step", "state", "set", "--help");

        await Assert.That(result.ExitCode).IsEqualTo(0);
    }

    [Test]
    public async Task PluginStepStateSubCommand_HelpInvocation_Succeeds()
    {
        var tester = new CommandAppTester();
        tester.Configure(CommandTree.Register);

        var result = tester.Run("plugin", "step", "state", "--help");

        await Assert.That(result.ExitCode).IsEqualTo(0);
    }
}
