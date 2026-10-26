// Copyright (c) DIGITALL Nature. All rights reserved
// DIGITALL Nature licenses this file to you under the Microsoft Public License.

using dgt.power.analyzer.Base;
using dgt.power.cli.tests.TestDoubles;
using dgt.power.codegeneration.Base;
using dgt.power.codegeneration.Constants;
using dgt.power.Commands.Complete;
using dgt.power.connection.Commands;
using dgt.power.duplicaterules.Commands;
using dgt.power.documenttemplates.Commands;
using dgt.power.plugin.Commands;
using dgt.power.plugin.Models;
using dgt.power.process.Commands;
using dgt.power.solution.Base;
using dgt.power.telemetry.Base;
using dgt.power.teamtemplates.Commands;
using dgt.power.webresource.Commands;
using Spectre.Console.Cli.Testing;
using Spectre.Console.Cli;

namespace dgt.power.cli.tests;

/// <summary>
/// Tier 2: settings-parsing tests. One test per distinct <c>CommandSettings</c> type used by
/// the command tree, verifying that Spectre.Console.Cli maps CLI arguments (positional
/// arguments, options, aliases, comma-separated lists, defaults) onto the settings
/// properties as expected.
/// <para>
/// These tests run against a dependency-free <see cref="NoOpCommand{TSettings}"/> registered as
/// a single top-level command, decoupled from the production command tree and its DI graph
/// (see <see cref="CommandTreeTests"/> for the tree-wiring tests). Business logic is
/// intentionally not exercised here - it is already covered via <c>CommandTestContext</c>.
/// </para>
/// </summary>
public class SettingsParsingTests
{
    [Test]
    public async Task ImportDocumentTemplatesSettings_ParsesOptions()
    {
        var result = Parse<ImportDocumentTemplatesSettings>(
            "./templates/documenttemplates.json", "--dry-run", "--non-interactive", "--no-telemetry");
        var settings = (ImportDocumentTemplatesSettings)result.Settings!;
        await Assert.That(result.ExitCode).IsEqualTo(0);
        await Assert.That(settings.Manifest).IsEqualTo("./templates/documenttemplates.json");
        await Assert.That(settings.DryRun).IsTrue();
        var defaults = Parse<ImportDocumentTemplatesSettings>("./templates/documenttemplates.json");
        await Assert.That(((ImportDocumentTemplatesSettings)defaults.Settings!).DryRun).IsFalse();
    }

    [Test]
    public async Task ExportDocumentTemplatesSettings_ParsesOptions()
    {
        var result = Parse<ExportDocumentTemplatesSettings>(
            "./templates", "--entity", "account", "--type", "word", "--overwrite");
        var settings = (ExportDocumentTemplatesSettings)result.Settings!;
        await Assert.That(result.ExitCode).IsEqualTo(0);
        await Assert.That(settings.Directory).IsEqualTo("./templates");
        await Assert.That(settings.Entity).IsEqualTo("account");
        await Assert.That(settings.Type).IsEqualTo("word");
        await Assert.That(settings.Overwrite).IsTrue();
        var defaults = (ExportDocumentTemplatesSettings)Parse<ExportDocumentTemplatesSettings>("./templates").Settings!;
        await Assert.That(defaults.Entity).IsNull();
        await Assert.That(defaults.Type).IsNull();
        await Assert.That(defaults.Overwrite).IsFalse();
    }

    private static CommandAppResult Parse<TSettings>(params string[] args)
        where TSettings : CommandSettings
    {
        var tester = new CommandAppTester();
        tester.Configure(config => config.AddCommand<NoOpCommand<TSettings>>("test"));
        return tester.Run(["test", .. args]);
    }

    [Test]
    public async Task WebResourcePushSettings_ParsesOptions()
    {
        var result = Parse<WebResourcePushSettings>(
            ".",
            "--solution", "samplesolution",
            "--publisher-prefix", "sample",
            "--mapping-file", "mappings.json",
            "--confirm",
            "--delete-obsolete",
            "--dry-run");

        var settings = (WebResourcePushSettings)result.Settings!;

        await Assert.That(settings.Target).IsEqualTo(".");
        await Assert.That(settings.Solution).IsEqualTo("samplesolution");
        await Assert.That(settings.PublisherPrefix).IsEqualTo("sample");
        await Assert.That(settings.MappingFile).IsEqualTo("mappings.json");
        await Assert.That(settings.Name).IsNull();
        await Assert.That(settings.Confirm).IsTrue();
        await Assert.That(settings.DeleteObsolete).IsTrue();
        await Assert.That(settings.DryRun).IsTrue();
    }

    [Test]
    public async Task WebResourcePushSettings_ParsesSingleFileName()
    {
        var directory = Directory.CreateTempSubdirectory();
        try
        {
            var target = Path.Combine(directory.FullName, "main.js");
            await File.WriteAllTextAsync(target, "console.log('test');");
            var result = Parse<WebResourcePushSettings>(
                target,
                "--name", "contoso_/scripts/main.js");

            var settings = (WebResourcePushSettings)result.Settings!;

            await Assert.That(settings.Target).IsEqualTo(target);
            await Assert.That(settings.Name).IsEqualTo("contoso_/scripts/main.js");
        }
        finally
        {
            directory.Delete(true);
        }
    }

    [Test]
    public async Task AnalyzeVerb_ParsesOptions()
    {
        var result = Parse<AnalyzeVerb>(
            "--inline", "solution1,solution2",
            "--note-patches",
            "--generate-report",
            "--generate-summary",
            "-c", "myconfig.json",
            "--no-telemetry",
            "--non-interactive");

        var settings = (AnalyzeVerb)result.Settings!;

        await Assert.That(settings.InlineData).IsEqualTo("solution1,solution2");
        await Assert.That(settings.NotePatch).IsTrue();
        await Assert.That(settings.GenerateReportFile).IsTrue();
        await Assert.That(settings.GenerateSummaryFile).IsTrue();
        await Assert.That(settings.Config).IsEqualTo("myconfig.json");
        await Assert.That(settings.NoTelemetry).IsTrue();
        await Assert.That(settings.NonInteractive).IsTrue();
    }

    [Test]
    public async Task AnalyzeVerb_DefaultConfig_IsConfigJson()
    {
        var result = Parse<AnalyzeVerb>();

        var settings = (AnalyzeVerb)result.Settings!;

        await Assert.That(settings.Config).IsEqualTo("config.json");
    }

    [Test]
    public async Task SolutionLintSettings_ParsesPositionalArgumentAndOptions()
    {
        var result = Parse<SolutionLintSettings>(
            "sample_solution",
            "-c", "myconfig.json",
            "--rules", "ruleA,ruleB",
            "--report", "report.json",
            "--sarif-output", "lint.sarif.json",
            "--baseline", "baseline.sarif.json",
            "--update-baseline",
            "--fail-on", "Warning");

        var settings = (SolutionLintSettings)result.Settings!;

        await Assert.That(settings.Solution).IsEqualTo("sample_solution");
        await Assert.That(settings.Config).IsEqualTo("myconfig.json");
        await Assert.That(settings.Rules).IsEqualTo("ruleA,ruleB");
        await Assert.That(settings.Report).IsEqualTo("report.json");
        await Assert.That(settings.SarifOutput).IsEqualTo("lint.sarif.json");
        await Assert.That(settings.Baseline).IsEqualTo("baseline.sarif.json");
        await Assert.That(settings.UpdateBaseline).IsTrue();
        await Assert.That(settings.FailOn).IsEqualTo("Warning");

        var defaults = Parse<SolutionLintSettings>("sample_solution");
        var defaultSettings = (SolutionLintSettings)defaults.Settings!;
        await Assert.That(defaultSettings.Config).IsEqualTo("lint.config.json");
        await Assert.That(defaultSettings.Rules).IsEqualTo(string.Empty);
        await Assert.That(defaultSettings.Report).IsEqualTo(string.Empty);
        await Assert.That(defaultSettings.SarifOutput).IsEqualTo(string.Empty);
        await Assert.That(defaultSettings.Baseline).IsEqualTo(string.Empty);
        await Assert.That(defaultSettings.UpdateBaseline).IsFalse();
        await Assert.That(defaultSettings.FailOn).IsEqualTo("Error");
    }

    [Test]
    public async Task CopyComponentsSettings_ParsesPositionalArgumentAndOptions()
    {
        var result = Parse<CopyComponentsSettings>(
            "target_solution",
            "-s", "source_a,source_b",
            "--dry-run",
            "--raw",
            "--apps", "strip");

        var settings = (CopyComponentsSettings)result.Settings!;

        await Assert.That(settings.Target).IsEqualTo("target_solution");
        await Assert.That(settings.Source).IsEqualTo("source_a,source_b");
        await Assert.That(settings.DryRun).IsTrue();
        await Assert.That(settings.Raw).IsTrue();
        await Assert.That(settings.Apps).IsEqualTo(AppHandling.Strip);

        var allow = (CopyComponentsSettings)Parse<CopyComponentsSettings>("target_solution", "--source", "source_a", "--apps", "allow").Settings!;
        await Assert.That(allow.Apps).IsEqualTo(AppHandling.Allow);

        var defaults = Parse<CopyComponentsSettings>("target_solution", "--source", "source_a");
        var defaultSettings = (CopyComponentsSettings)defaults.Settings!;
        await Assert.That(defaultSettings.Source).IsEqualTo("source_a");
        await Assert.That(defaultSettings.DryRun).IsFalse();
        await Assert.That(defaultSettings.Raw).IsFalse();
        await Assert.That(defaultSettings.Apps).IsEqualTo(AppHandling.Skip);
    }

    [Test]
    public async Task ProcessInitSettings_ParsesPathRepeatedSolutionsAndOptions()
    {
        var directory = Directory.CreateTempSubdirectory();
        try
        {
            var file = Path.GetRelativePath(Environment.CurrentDirectory, Path.Combine(directory.FullName, "processes.json"));
            var mapping = Path.Combine(directory.FullName, "owners.source.json");
            await File.WriteAllTextAsync(mapping, """{"schemaVersion":1,"owners":{}}""");
            var result = Parse<ProcessInitSettings>(
                file, "--solution", "ContosoCore", "--solution", "ContosoFlows",
                "--capture", "both", "--owner-mapping", mapping, "--unmapped-owners", "skip", "--overwrite");
            var defaults = Parse<ProcessInitSettings>(file, "--solution", "ContosoCore");

            await Assert.That(result.ExitCode).IsEqualTo(0).Because(result.Output);
            var settings = (ProcessInitSettings)result.Settings!;
            await Assert.That(settings.File).IsEqualTo(file);
            await Assert.That(settings.Solutions).IsEquivalentTo(["ContosoCore", "ContosoFlows"]);
            await Assert.That(settings.Capture).IsEqualTo("both");
            await Assert.That(settings.OwnerMapping).IsEqualTo(mapping);
            await Assert.That(settings.UnmappedOwners).IsEqualTo("skip");
            await Assert.That(settings.Overwrite).IsTrue();
            await Assert.That(defaults.ExitCode).IsEqualTo(0).Because(defaults.Output);
            var defaultSettings = (ProcessInitSettings)defaults.Settings!;
            await Assert.That(defaultSettings.Capture).IsEqualTo("state");
            await Assert.That(defaultSettings.UnmappedOwners).IsEqualTo("fail");
            await Assert.That(defaultSettings.OwnerMapping).IsNull();
            await Assert.That(defaultSettings.Overwrite).IsFalse();
        }
        finally
        {
            directory.Delete(true);
        }
    }

    [Test]
    public async Task ProcessApplySettings_ParsesPathImpersonationAndDefaults()
    {
        var directory = Directory.CreateTempSubdirectory();
        try
        {
            var absoluteFile = Path.Combine(directory.FullName, "processes.json");
            var file = Path.GetRelativePath(Environment.CurrentDirectory, absoluteFile);
            var mapping = Path.Combine(directory.FullName, "owners.target.json");
            await File.WriteAllTextAsync(absoluteFile, """{"schemaVersion":1,"processes":[]}""");
            await File.WriteAllTextAsync(mapping, """{"schemaVersion":1,"owners":{}}""");
            var result = Parse<ProcessApplySettings>(
                file, "--owner-mapping", mapping, "--activate-as", "integrationUser", "--dry-run");
            var asOwner = Parse<ProcessApplySettings>(file, "--owner-mapping", mapping, "--activate-as-owner");
            var defaults = Parse<ProcessApplySettings>(file);

            await Assert.That(result.ExitCode).IsEqualTo(0).Because(result.Output);
            var settings = (ProcessApplySettings)result.Settings!;
            await Assert.That(settings.File).IsEqualTo(file);
            await Assert.That(settings.OwnerMapping).IsEqualTo(mapping);
            await Assert.That(settings.ActivateAs).IsEqualTo("integrationUser");
            await Assert.That(settings.ActivateAsOwner).IsFalse();
            await Assert.That(settings.DryRun).IsTrue();
            await Assert.That(asOwner.ExitCode).IsEqualTo(0).Because(asOwner.Output);
            await Assert.That(((ProcessApplySettings)asOwner.Settings!).ActivateAsOwner).IsTrue();
            await Assert.That(defaults.ExitCode).IsEqualTo(0).Because(defaults.Output);
            var defaultSettings = (ProcessApplySettings)defaults.Settings!;
            await Assert.That(defaultSettings.ActivateAs).IsNull();
            await Assert.That(defaultSettings.ActivateAsOwner).IsFalse();
            await Assert.That(defaultSettings.OwnerMapping).IsNull();
            await Assert.That(defaultSettings.DryRun).IsFalse();
        }
        finally
        {
            directory.Delete(true);
        }
    }

    [Test]
    public async Task TeamTemplateImportSettings_ParsesRepeatedCleanupTablesAndDryRun()
    {
        const string file = "./teamtemplates.json";
        var result = Parse<TeamTemplateImportSettings>(
            file,
            "--dry-run",
            "--cleanup",
            "--cleanup-table", "account",
            "--cleanup-table", "contact");

        await Assert.That(result.ExitCode).IsEqualTo(0).Because(result.Output);
        var settings = (TeamTemplateImportSettings)result.Settings!;
        await Assert.That(settings.File).IsEqualTo(file);
        await Assert.That(settings.DryRun).IsTrue();
        await Assert.That(settings.Cleanup).IsTrue();
        await Assert.That(settings.CleanupTables).IsEquivalentTo(["account", "contact"]);
    }

    [Test]
    public async Task TeamTemplateExportSettings_ParsesRepeatedTablesOverwriteAndDryRun()
    {
        const string file = "./teamtemplates.json";
        var result = Parse<TeamTemplateExportSettings>(
            file,
            "--dry-run",
            "--overwrite",
            "--table", "account",
            "--table", "contact");

        await Assert.That(result.ExitCode).IsEqualTo(0).Because(result.Output);
        var settings = (TeamTemplateExportSettings)result.Settings!;
        await Assert.That(settings.File).IsEqualTo(file);
        await Assert.That(settings.DryRun).IsTrue();
        await Assert.That(settings.Overwrite).IsTrue();
        await Assert.That(settings.Tables).IsEquivalentTo(["account", "contact"]);
    }

    [Test]
    public async Task TeamTemplateSettings_DefaultOptionsAndRequiredFile()
    {
        var importDefaults = Parse<TeamTemplateImportSettings>("./teamtemplates.json");
        var exportDefaults = Parse<TeamTemplateExportSettings>("./teamtemplates.json");
        var importMissingFile = Parse<TeamTemplateImportSettings>();
        var exportMissingFile = Parse<TeamTemplateExportSettings>();

        await Assert.That(importDefaults.ExitCode).IsEqualTo(0).Because(importDefaults.Output);
        var import = (TeamTemplateImportSettings)importDefaults.Settings!;
        await Assert.That(import.File).IsEqualTo("./teamtemplates.json");
        await Assert.That(import.DryRun).IsFalse();
        await Assert.That(import.Cleanup).IsFalse();
        await Assert.That(import.CleanupTables).IsNull();

        await Assert.That(exportDefaults.ExitCode).IsEqualTo(0).Because(exportDefaults.Output);
        var export = (TeamTemplateExportSettings)exportDefaults.Settings!;
        await Assert.That(export.File).IsEqualTo("./teamtemplates.json");
        await Assert.That(export.DryRun).IsFalse();
        await Assert.That(export.Overwrite).IsFalse();
        await Assert.That(export.Tables).IsNull();

        await Assert.That(importMissingFile.ExitCode).IsNotEqualTo(0);
        await Assert.That(exportMissingFile.ExitCode).IsNotEqualTo(0);
    }

    [Test]
    public async Task TeamTemplateImportSettings_RejectsInvalidCleanupScope()
    {
        var missingTables = Parse<TeamTemplateImportSettings>("./teamtemplates.json", "--cleanup");
        var cleanupNotRequested = Parse<TeamTemplateImportSettings>(
            "./teamtemplates.json", "--cleanup-table", "account");
        var commaSeparated = Parse<TeamTemplateImportSettings>(
            "./teamtemplates.json", "--cleanup", "--cleanup-table", "account,contact");

        await Assert.That(missingTables.ExitCode).IsNotEqualTo(0);
        await Assert.That(cleanupNotRequested.ExitCode).IsNotEqualTo(0);
        await Assert.That(commaSeparated.ExitCode).IsNotEqualTo(0);
    }

    [Test]
    [Arguments("init", "--publishers")]
    [Arguments("init", "--solutions")]
    [Arguments("init", "--detailed")]
    [Arguments("apply", "--tablereport")]
    public async Task ProcessSettings_RejectLegacyOptions(string command, string option)
    {
        var tester = new CommandAppTester();
        tester.Configure(CommandTree.Register);

        var result = tester.Run("process", command, "./processes.json", option, "legacy");

        await Assert.That(result.ExitCode).IsNotEqualTo(0);
    }

    [Test]
    public async Task DuplicateRuleInitSettings_ParsesPathRepeatedRuleIdsAndOptions()
    {
        var directory = Directory.CreateTempSubdirectory();
        try
        {
            var file = Path.GetRelativePath(Environment.CurrentDirectory, Path.Combine(directory.FullName, "duplicaterules.json"));
            var mapping = Path.Combine(directory.FullName, "owners.source.json");
            await File.WriteAllTextAsync(mapping, """{"schemaVersion":1,"owners":{}}""");
            const string firstId = "6f1c2b4e-8d3a-4f5b-9c7d-1e2f3a4b5c6d";
            const string secondId = "0a9b8c7d-6e5f-4a3b-2c1d-0e9f8a7b6c5d";
            var result = Parse<DuplicateRuleInitSettings>(
                file, "--entity", "contact", "--rule-id", firstId, "--rule-id", secondId,
                "--capture", "both", "--owner-mapping", mapping, "--unmapped-owners", "skip", "--overwrite");
            var defaults = Parse<DuplicateRuleInitSettings>(file);

            await Assert.That(result.ExitCode).IsEqualTo(0).Because(result.Output);
            var settings = (DuplicateRuleInitSettings)result.Settings!;
            await Assert.That(settings.File).IsEqualTo(file);
            await Assert.That(settings.Entity).IsEqualTo("contact");
            await Assert.That(settings.RuleIds).IsEquivalentTo([firstId, secondId]);
            await Assert.That(settings.Capture).IsEqualTo("both");
            await Assert.That(settings.OwnerMapping).IsEqualTo(mapping);
            await Assert.That(settings.UnmappedOwners).IsEqualTo("skip");
            await Assert.That(settings.Overwrite).IsTrue();
            await Assert.That(defaults.ExitCode).IsEqualTo(0).Because(defaults.Output);
            var defaultSettings = (DuplicateRuleInitSettings)defaults.Settings!;
            await Assert.That(defaultSettings.Capture).IsEqualTo("state");
            await Assert.That(defaultSettings.UnmappedOwners).IsEqualTo("fail");
            await Assert.That(defaultSettings.Entity).IsNull();
            await Assert.That(defaultSettings.RuleIds ?? []).IsEmpty();
            await Assert.That(defaultSettings.OwnerMapping).IsNull();
            await Assert.That(defaultSettings.Overwrite).IsFalse();
        }
        finally
        {
            directory.Delete(true);
        }
    }

    [Test]
    public async Task DuplicateRuleApplySettings_ParsesPathWaitOptionsAndDefaults()
    {
        var directory = Directory.CreateTempSubdirectory();
        try
        {
            var absoluteFile = Path.Combine(directory.FullName, "duplicaterules.json");
            var file = Path.GetRelativePath(Environment.CurrentDirectory, absoluteFile);
            var mapping = Path.Combine(directory.FullName, "owners.target.json");
            await File.WriteAllTextAsync(absoluteFile, """{"schemaVersion":1,"rules":[]}""");
            await File.WriteAllTextAsync(mapping, """{"schemaVersion":1,"owners":{}}""");
            var result = Parse<DuplicateRuleApplySettings>(
                file, "--owner-mapping", mapping, "--dry-run", "--no-wait", "--timeout", "600");
            var defaults = Parse<DuplicateRuleApplySettings>(file);

            await Assert.That(result.ExitCode).IsEqualTo(0).Because(result.Output);
            var settings = (DuplicateRuleApplySettings)result.Settings!;
            await Assert.That(settings.File).IsEqualTo(file);
            await Assert.That(settings.OwnerMapping).IsEqualTo(mapping);
            await Assert.That(settings.DryRun).IsTrue();
            await Assert.That(settings.NoWait).IsTrue();
            await Assert.That(settings.Timeout).IsEqualTo(600);
            await Assert.That(defaults.ExitCode).IsEqualTo(0).Because(defaults.Output);
            var defaultSettings = (DuplicateRuleApplySettings)defaults.Settings!;
            await Assert.That(defaultSettings.OwnerMapping).IsNull();
            await Assert.That(defaultSettings.DryRun).IsFalse();
            await Assert.That(defaultSettings.NoWait).IsFalse();
            await Assert.That(defaultSettings.Timeout).IsEqualTo(DuplicateRuleApplySettings.DefaultTimeoutSeconds);
        }
        finally
        {
            directory.Delete(true);
        }
    }

    [Test]
    [Arguments("init", "--rule-id", "not-a-guid")]
    [Arguments("init", "--capture", "none")]
    [Arguments("init", "--unmapped-owners", "ignore")]
    [Arguments("apply", "--timeout", "0")]
    [Arguments("apply", "--timeout", "abc")]
    [Arguments("apply", "--solution", "ContosoCore")]
    public async Task DuplicateRuleSettings_RejectInvalidOptions(string command, string option, string value)
    {
        var directory = Directory.CreateTempSubdirectory();
        try
        {
            var file = Path.Combine(directory.FullName, "duplicaterules.json");
            if (command == "apply")
            {
                await File.WriteAllTextAsync(file, """{"schemaVersion":1,"rules":[]}""");
            }

            var tester = new CommandAppTester();
            tester.Configure(CommandTree.Register);

            var result = tester.Run("duplicaterules", command, file, option, value);

            await Assert.That(result.ExitCode).IsNotEqualTo(0);
        }
        finally
        {
            directory.Delete(true);
        }
    }

    [Test]
    public async Task SolutionVersionSettings_ParsesPositionalArgumentAndFlag()
    {
        var result = Parse<SolutionVersionSettings>("sample_solution", "--minor");

        var settings = (SolutionVersionSettings)result.Settings!;

        await Assert.That(settings.Solution).IsEqualTo("sample_solution");
        await Assert.That(settings.Minor).IsTrue();
        await Assert.That(settings.Major).IsFalse();
        await Assert.That(settings.Build).IsFalse();
        await Assert.That(settings.Revision).IsTrue(); // DefaultValue(true)
    }

    [Test]
    public async Task CodeGenerationVerb_ParsesOptionalPositionalArgumentAndOptions()
    {
        var result = Parse<CodeGenerationVerb>("c:/TargetDir", "-f", "MyModel", "-c", "genconfig.json");
        var defaults = Parse<CodeGenerationVerb>();

        var settings = (CodeGenerationVerb)result.Settings!;
        await Assert.That(settings.TargetDirectory).IsEqualTo("c:/TargetDir");
        await Assert.That(settings.Folder).IsEqualTo("MyModel");
        await Assert.That(settings.Config).IsEqualTo("genconfig.json");

        var defaultSettings = (CodeGenerationVerb)defaults.Settings!;
        await Assert.That(defaultSettings.TargetDirectory).IsEqualTo(".");
        await Assert.That(defaultSettings.Folder).IsEqualTo(Folders.Model);
        await Assert.That(defaultSettings.Config).IsEqualTo("config.json");
    }

    [Test]
    public async Task PluginPushSettings_ParsesPositionalArgumentAndOptions()
    {
        var result = Parse<PluginPushSettings>(
            "c:/TargetDir/plugin.dll",
            "--solution", "samplesolution",
            "--publisher-prefix", "sample",
            "--dry-run",
            "--confirm");

        var settings = (PluginPushSettings)result.Settings!;

        await Assert.That(settings.Target).IsEqualTo("c:/TargetDir/plugin.dll");
        await Assert.That(settings.Solution).IsEqualTo("samplesolution");
        await Assert.That(settings.PublisherPrefix).IsEqualTo("sample");
        await Assert.That(settings.DryRun).IsTrue();
        await Assert.That(settings.Confirm).IsTrue();
    }

    [Test]
    public async Task PluginPushSettings_Defaults()
    {
        var result = Parse<PluginPushSettings>("c:/TargetDir/plugin.dll");

        var settings = (PluginPushSettings)result.Settings!;

        await Assert.That(settings.Solution).IsNull();
        await Assert.That(settings.PublisherPrefix).IsNull();
        await Assert.That(settings.DryRun).IsFalse();
        await Assert.That(settings.Confirm).IsFalse();
    }

    [Test]
    public async Task CreateConnectionSettings_ParsesMsalOptions()
    {
        var result = Parse<CreateConnectionSettings>(
            "myconnection",
            "--url", "https://org.crm.dynamics.com",
            "--tenant", "contoso.onmicrosoft.com",
            "--no-verify");

        var settings = (CreateConnectionSettings)result.Settings!;

        await Assert.That(settings.Name).IsEqualTo("myconnection");
        await Assert.That(settings.Url).IsEqualTo("https://org.crm.dynamics.com");
        await Assert.That(settings.TenantId).IsEqualTo("contoso.onmicrosoft.com");
        await Assert.That(settings.AzureDevOpsFederated).IsFalse();
        await Assert.That(settings.NoVerify).IsTrue();
    }

    [Test]
    public async Task CreateConnectionSettings_ParsesClientSecretValue()
    {
        var result = Parse<CreateConnectionSettings>(
            "prod", "--url", "https://org.crm.dynamics.com",
            "--tenant", "tenant", "--client-id", "client",
            "--client-secret", "test-secret", "--non-interactive");

        var settings = (CreateConnectionSettings)result.Settings!;
        await Assert.That(settings.ClientSecret).IsEqualTo("test-secret");
        await Assert.That(settings.NonInteractive).IsTrue();
    }

    [Test]
    public async Task CreateConnectionSettings_ParsesCertificatePassword()
    {
        var result = Parse<CreateConnectionSettings>(
            "prod", "--url", "https://org.crm.dynamics.com",
            "--tenant", "tenant", "--client-id", "client",
            "--certificate-path", "certificate.pfx", "--certificate-password", "pfx-password");

        var settings = (CreateConnectionSettings)result.Settings!;
        await Assert.That(settings.CertificatePath).IsEqualTo("certificate.pfx");
        await Assert.That(settings.CertificatePassword).IsEqualTo("pfx-password");
    }

    [Test]
    public async Task CreateConnectionSettings_PasswordlessCertificateDefaultsToNull()
    {
        var result = Parse<CreateConnectionSettings>(
            "prod", "--url", "https://org.crm.dynamics.com",
            "--tenant", "tenant", "--client-id", "client",
            "--certificate-path", "certificate.pfx");

        var settings = (CreateConnectionSettings)result.Settings!;
        await Assert.That(settings.CertificatePassword).IsNull();
    }

    [Test]
    public async Task GlobalConnectionSettings_ParsesConnectionSelectionAndAdHocString()
    {
        var result = Parse<AnalyzeVerb>(
            "--connection", "dev",
            "--connection-string", "AuthType=ClientSecret;Url=https://org.crm.dynamics.com;");

        var settings = (AnalyzeVerb)result.Settings!;

        await Assert.That(settings.Connection).IsEqualTo("dev");
        await Assert.That(settings.ConnectionString).IsEqualTo("AuthType=ClientSecret;Url=https://org.crm.dynamics.com;");
    }

    [Test]
    public async Task CreateConnectionSettings_ParsesAzureDevOpsFederatedOptions()
    {
        var result = Parse<CreateConnectionSettings>(
            "myconnection",
            "--url", "https://org.crm.dynamics.com",
            "--azure-devops-federated",
            "--tenant", "11111111-1111-1111-1111-111111111111",
            "--client-id", "22222222-2222-2222-2222-222222222222",
            "--service-connection-id", "33333333-3333-3333-3333-333333333333",
            "--no-verify");

        var settings = (CreateConnectionSettings)result.Settings!;

        await Assert.That(settings.AzureDevOpsFederated).IsTrue();
        await Assert.That(settings.TenantId).IsEqualTo("11111111-1111-1111-1111-111111111111");
        await Assert.That(settings.ClientId).IsEqualTo("22222222-2222-2222-2222-222222222222");
        await Assert.That(settings.ServiceConnectionId).IsEqualTo("33333333-3333-3333-3333-333333333333");
    }

    [Test]
    public async Task CreateConnectionSettings_ParsesServiceConnectionName()
    {
        var result = Parse<CreateConnectionSettings>(
            "myconnection",
            "--azure-devops-federated",
            "--service-connection-name", "MyPowerPlatformConnection",
            "--no-verify");

        var settings = (CreateConnectionSettings)result.Settings!;

        await Assert.That(settings.AzureDevOpsFederated).IsTrue();
        await Assert.That(settings.ServiceConnectionName).IsEqualTo("MyPowerPlatformConnection");
        await Assert.That(settings.TenantId).IsNull();
        await Assert.That(settings.ClientId).IsNull();
        await Assert.That(settings.ServiceConnectionId).IsNull();
    }

    [Test]
    public async Task CreateConnectionSettings_ParsesAzureDevOpsFederatedAlias()
    {
        var result = Parse<CreateConnectionSettings>(
            "myconnection",
            "--url", "https://org.crm.dynamics.com",
            "--adof",
            "--tenant", "11111111-1111-1111-1111-111111111111",
            "--client-id", "22222222-2222-2222-2222-222222222222",
            "--service-connection-id", "33333333-3333-3333-3333-333333333333");

        var settings = (CreateConnectionSettings)result.Settings!;

        await Assert.That(settings.AzureDevOpsFederated).IsTrue();
    }

    [Test]
    public async Task CompleteSetupSettings_ParsesOptions()
    {
        var result = Parse<CompleteSetupSettings>("--all", "--shell", "zsh");

        var settings = (CompleteSetupSettings)result.Settings!;

        await Assert.That(settings.All).IsTrue();
        await Assert.That(settings.Shell).IsEqualTo("zsh");
    }

    [Test]
    public async Task CompleteInstallShellSettings_ParsesOptions()
    {
        var result = Parse<CompleteInstallShellSettings>("--shell", "bash", "--dry-run");

        var settings = (CompleteInstallShellSettings)result.Settings!;

        await Assert.That(settings.Shell).IsEqualTo("bash");
        await Assert.That(settings.DryRun).IsTrue();
    }

    [Test]
    public async Task PluginStepConfigSetSettings_ParsesDirectStepIdWithConfig()
    {
        var stepId = Guid.NewGuid();
        var result = Parse<PluginStepConfigSetSettings>(
            "--step-id", stepId.ToString(),
            "--unsecure", "myconfig");

        var settings = (PluginStepConfigSetSettings)result.Settings!;

        await Assert.That(settings.StepId).IsEqualTo(stepId);
        await Assert.That(settings.UnsecureConfig).IsEqualTo("myconfig");
        await Assert.That(settings.PluginType).IsNull();
        await Assert.That(settings.Message).IsNull();
    }

    [Test]
    public async Task PluginStepConfigSetSettings_ParsesCompositeKeyWithConfig()
    {
        var result = Parse<PluginStepConfigSetSettings>(
            "--plugin-type", "MyNamespace.MyPlugin",
            "--message", "Create",
            "--stage", "PreOperation",
            "--entity", "account",
            "--secure", "mysecret");

        var settings = (PluginStepConfigSetSettings)result.Settings!;

        await Assert.That(settings.StepId).IsNull();
        await Assert.That(settings.PluginType).IsEqualTo("MyNamespace.MyPlugin");
        await Assert.That(settings.Message).IsEqualTo("Create");
        await Assert.That(settings.Stage).IsEqualTo(PluginStepStage.PreOperation);
        await Assert.That(settings.Entity).IsEqualTo("account");
        await Assert.That(settings.SecureConfig).IsEqualTo("mysecret");
    }

    [Test]
    [Arguments("10", PluginStepStage.PreValidation)]
    [Arguments("20", PluginStepStage.PreOperation)]
    [Arguments("30", PluginStepStage.MainOperation)]
    [Arguments("40", PluginStepStage.PostOperation)]
    [Arguments("MainOperation", PluginStepStage.MainOperation)]
    [Arguments("PreOperation", PluginStepStage.PreOperation)]
    public async Task PluginStepConfigSetSettings_ParsesNamedAndNumericStages(string stage, PluginStepStage expectedStage)
    {
        var result = Parse<PluginStepConfigSetSettings>(
            "--plugin-type", "MyNamespace.MyPlugin",
            "--message", "Create",
            "--stage", stage,
            "--entity", "account",
            "--unsecure", "myconfig");

        var settings = (PluginStepConfigSetSettings)result.Settings!;

        await Assert.That(settings.Stage).IsEqualTo(expectedStage);
    }

    [Test]
    [Arguments("80")]
    [Arguments("90")]
    [Arguments("PreCommitStage")]
    [Arguments("PostCommitStage")]
    public async Task PluginStepConfigSetSettings_RejectsInternalStages(string stage)
    {
        var result = Parse<PluginStepConfigSetSettings>(
            "--plugin-type", "MyNamespace.MyPlugin",
            "--message", "Create",
            "--stage", stage,
            "--entity", "account",
            "--unsecure", "myconfig");

        await Assert.That(result.ExitCode).IsNotEqualTo(0);
    }

    [Test]
    public async Task PluginStepConfigSetSettings_ParsesMultipleConfigOptions()
    {
        var stepId = Guid.NewGuid();
        var result = Parse<PluginStepConfigSetSettings>(
            "--step-id", stepId.ToString(),
            "--unsecure", "unsecureValue",
            "--secure", "secureValue");

        var settings = (PluginStepConfigSetSettings)result.Settings!;

        await Assert.That(settings.StepId).IsEqualTo(stepId);
        await Assert.That(settings.UnsecureConfig).IsEqualTo("unsecureValue");
        await Assert.That(settings.SecureConfig).IsEqualTo("secureValue");
        await Assert.That(settings.PluginType).IsNull();
    }

    [Test]
    public async Task PluginStepEnableDisableSettings_ParsesDirectStepId()
    {
        var stepId = Guid.NewGuid();
        var result = Parse<PluginStepEnableDisableSettings>("--step-id", stepId.ToString());

        var settings = (PluginStepEnableDisableSettings)result.Settings!;

        await Assert.That(settings.StepId).IsEqualTo(stepId);
        await Assert.That(settings.PluginType).IsNull();
        await Assert.That(settings.Message).IsNull();
    }

    [Test]
    public async Task PluginStepEnableDisableSettings_ParsesReason()
    {
        var stepId = Guid.NewGuid();
        var result = Parse<PluginStepEnableDisableSettings>(
            "--step-id", stepId.ToString(),
            "--reason", "Maintenance mode");

        var settings = (PluginStepEnableDisableSettings)result.Settings!;

        await Assert.That(settings.StepId).IsEqualTo(stepId);
        await Assert.That(settings.Reason).IsEqualTo("Maintenance mode");
    }

    [Test]
    public async Task PluginStepEnableDisableSettings_ParsesSolution()
    {
        var result = Parse<PluginStepEnableDisableSettings>(
            "--solution", "mysolution",
            "--reason", "Bulk operation");

        var settings = (PluginStepEnableDisableSettings)result.Settings!;

        await Assert.That(settings.Solution).IsEqualTo("mysolution");
        await Assert.That(settings.Reason).IsEqualTo("Bulk operation");
    }

    [Test]
    public async Task PluginStepEnableDisableSettings_ParsesCompositeKey()
    {
        var result = Parse<PluginStepEnableDisableSettings>(
            "--plugin-type", "MyNamespace.MyPlugin",
            "--message", "Create",
            "--stage", "PreOperation",
            "--entity", "account");

        var settings = (PluginStepEnableDisableSettings)result.Settings!;

        await Assert.That(settings.StepId).IsNull();
        await Assert.That(settings.PluginType).IsEqualTo("MyNamespace.MyPlugin");
        await Assert.That(settings.Message).IsEqualTo("Create");
        await Assert.That(settings.Stage).IsEqualTo(PluginStepStage.PreOperation);
        await Assert.That(settings.Entity).IsEqualTo("account");
    }

    [Test]
    [Arguments("10", PluginStepStage.PreValidation)]
    [Arguments("20", PluginStepStage.PreOperation)]
    [Arguments("30", PluginStepStage.MainOperation)]
    [Arguments("40", PluginStepStage.PostOperation)]
    [Arguments("PreValidation", PluginStepStage.PreValidation)]
    [Arguments("PreOperation", PluginStepStage.PreOperation)]
    public async Task PluginStepEnableDisableSettings_ParsesNamedAndNumericStages(string stage, PluginStepStage expectedStage)
    {
        var result = Parse<PluginStepEnableDisableSettings>(
            "--plugin-type", "MyNamespace.MyPlugin",
            "--message", "Create",
            "--stage", stage,
            "--entity", "account");

        var settings = (PluginStepEnableDisableSettings)result.Settings!;

        await Assert.That(settings.Stage).IsEqualTo(expectedStage);
    }

    [Test]
    [Arguments("80")]
    [Arguments("90")]
    [Arguments("PreCommitStage")]
    [Arguments("PostCommitStage")]
    public async Task PluginStepEnableDisableSettings_RejectsInternalStages(string stage)
    {
        var result = Parse<PluginStepEnableDisableSettings>(
            "--plugin-type", "MyNamespace.MyPlugin",
            "--message", "Create",
            "--stage", stage,
            "--entity", "account");

        await Assert.That(result.ExitCode).IsNotEqualTo(0);
    }

    [Test]
    public async Task PluginStepStateSetSettings_ParsesConfigOption()
    {
        var result = Parse<PluginStepStateSetSettings>(
            "-c", "step-states.json");

        var settings = (PluginStepStateSetSettings)result.Settings!;

        await Assert.That(settings.Config).IsEqualTo("step-states.json");
        await Assert.That(settings.DryRun).IsFalse();
    }

    [Test]
    public async Task PluginStepStateSetSettings_ParsesDryRun()
    {
        var result = Parse<PluginStepStateSetSettings>("-c", "step-states.json", "--dry-run");

        var settings = (PluginStepStateSetSettings)result.Settings!;

        await Assert.That(settings.DryRun).IsTrue();
    }

    [Test]
    public async Task PluginStepEnableDisableSettings_ParsesDryRun()
    {
        var result = Parse<PluginStepEnableDisableSettings>("--solution", "mysolution", "--dry-run");

        var settings = (PluginStepEnableDisableSettings)result.Settings!;

        await Assert.That(settings.DryRun).IsTrue();
    }

    [Test]
    public async Task TelemetrySettings_ParsesInheritedOptions()
    {
        var result = Parse<TelemetrySettings>(
            "--no-telemetry",
            "--non-interactive",
            "--connection", "myconn");

        var settings = (TelemetrySettings)result.Settings!;

        await Assert.That(settings.NoTelemetry).IsTrue();
        await Assert.That(settings.NonInteractive).IsTrue();
        await Assert.That(settings.Connection).IsEqualTo("myconn");
        await Assert.That(settings.ConnectionString).IsNull();
    }

    [Test]
    public async Task TelemetrySettings_Defaults()
    {
        var result = Parse<TelemetrySettings>();

        var settings = (TelemetrySettings)result.Settings!;

        await Assert.That(settings.NoTelemetry).IsFalse();
        await Assert.That(settings.NonInteractive).IsFalse();
        await Assert.That(settings.Connection).IsNull();
        await Assert.That(settings.ConnectionString).IsNull();
    }
}
