// Copyright (c) DIGITALL Nature. All rights reserved
// DIGITALL Nature licenses this file to you under the Microsoft Public License.

using dgt.power.analyzer.Base;
using dgt.power.cli.tests.TestDoubles;
using dgt.power.codegeneration.Base;
using dgt.power.codegeneration.Constants;
using dgt.power.Commands.Complete;
using dgt.power.export.Base;
using dgt.power.import.Base;
using dgt.power.maintenance.Base;
using dgt.power.maintenance.Logic;
using dgt.power.maintenance.Model.Settings;
using dgt.power.profile.Commands;
using dgt.power.push.Base;
using Spectre.Console.Cli;
using Spectre.Console.Cli.Testing;

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
    private static CommandAppResult Parse<TSettings>(params string[] args)
        where TSettings : CommandSettings
    {
        var tester = new CommandAppTester();
        tester.Configure(config => config.AddCommand<NoOpCommand<TSettings>>("test"));
        return tester.Run(["test", .. args]);
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
    public async Task ExportVerb_ParsesOptions()
    {
        var result = Parse<ExportVerb>("--filedir", "c:/TargetDir", "--filename", "out.json", "--inline", "data");

        var settings = (ExportVerb)result.Settings!;

        await Assert.That(settings.FileDir).IsEqualTo("c:/TargetDir");
        await Assert.That(settings.FileName).IsEqualTo("out.json");
        await Assert.That(settings.InlineData).IsEqualTo("data");
    }

    [Test]
    public async Task ExportVerb_DefaultFileDir_IsCurrentDirectory()
    {
        var result = Parse<ExportVerb>();

        var settings = (ExportVerb)result.Settings!;

        await Assert.That(settings.FileDir).IsEqualTo(".");
    }

    [Test]
    public async Task ImportVerb_ParsesOptions()
    {
        var result = Parse<ImportVerb>(
            "--filedir", "c:/TargetDir",
            "--filename", "in.json",
            "--inline", "data",
            "--assignee", "user@contoso.com");

        var settings = (ImportVerb)result.Settings!;

        await Assert.That(settings.FileDir).IsEqualTo("c:/TargetDir");
        await Assert.That(settings.FileName).IsEqualTo("in.json");
        await Assert.That(settings.InlineData).IsEqualTo("data");
        await Assert.That(settings.Assignee).IsEqualTo("user@contoso.com");
    }

    [Test]
    public async Task MaintenanceVerb_ParsesConfigAliasAndDefault()
    {
        var withAlias = Parse<MaintenanceVerb>("-c", "myconfig.json");
        var defaults = Parse<MaintenanceVerb>();

        await Assert.That(((MaintenanceVerb)withAlias.Settings!).Config).IsEqualTo("myconfig.json");
        await Assert.That(((MaintenanceVerb)defaults.Settings!).Config).IsEqualTo("config.json");
    }

    [Test]
    public async Task CarrierInfoSettings_ParsesOptionsAndDefaults()
    {
        var result = Parse<CarrierInfoSettings>("--filedir", "./carriers", "--filename", "carrier.json");
        var defaults = Parse<CarrierInfoSettings>();

        var settings = (CarrierInfoSettings)result.Settings!;
        await Assert.That(settings.FileDir).IsEqualTo("./carriers");
        await Assert.That(settings.FileName).IsEqualTo("carrier.json");

        var defaultSettings = (CarrierInfoSettings)defaults.Settings!;
        await Assert.That(defaultSettings.FileDir).IsEqualTo(".");
        await Assert.That(defaultSettings.FileName).IsEqualTo("carrier.json");
    }

    [Test]
    public async Task IncrementSolutionVersionSettings_ParsesPositionalArgumentAndFlag()
    {
        var result = Parse<IncrementSolutionVersionSettings>("sample_solution", "--minor");

        var settings = (IncrementSolutionVersionSettings)result.Settings!;

        await Assert.That(settings.Solution).IsEqualTo("sample_solution");
        await Assert.That(settings.Minor).IsTrue();
        await Assert.That(settings.Major).IsFalse();
        await Assert.That(settings.Build).IsFalse();
        await Assert.That(settings.Revision).IsTrue(); // DefaultValue(true)
    }

    [Test]
    public async Task RemoveRedundantComponentsVerb_ParsesPositionalArgumentsAndFlags()
    {
        var result = Parse<RemoveRedundantComponentsVerb>("deploysolution", "tmpsolution", "--dryrun");

        var settings = (RemoveRedundantComponentsVerb)result.Settings!;

        await Assert.That(settings.SourceSolutions).IsEqualTo("deploysolution");
        await Assert.That(settings.TargetSolution).IsEqualTo("tmpsolution");
        await Assert.That(settings.DryRun).IsTrue();
        await Assert.That(settings.Entities).IsFalse();
    }

    [Test]
    public async Task EnsureSdkStepStatusSettings_ParsesAliasesAndDefaults()
    {
        var result = Parse<EnsureSdkStepStatusSettings>("-s", "mysolution", "-d", "--dry-run");
        var defaults = Parse<EnsureSdkStepStatusSettings>();

        var settings = (EnsureSdkStepStatusSettings)result.Settings!;
        await Assert.That(settings.Solution).IsEqualTo("mysolution");
        await Assert.That(settings.Disabled).IsTrue();
        await Assert.That(settings.DryRun).IsTrue();

        await Assert.That(((EnsureSdkStepStatusSettings)defaults.Settings!).Solution).IsEqualTo("assemblies");
    }

    [Test]
    public async Task CreateWorkflowStateConfigSettings_ParsesOptionsAndCommaSeparatedLists()
    {
        var result = Parse<CreateWorkflowStateConfigSettings>(
            "-o", "out.json",
            "--overwrite",
            "-s", "solution1,solution2",
            "-p", "publisher1,publisher2",
            "--detailed");

        var settings = (CreateWorkflowStateConfigSettings)result.Settings!;

        await Assert.That(settings.Config).IsEqualTo("out.json");
        await Assert.That(settings.Overwrite).IsTrue();
        await Assert.That(settings.Solutions).IsEqualTo("solution1,solution2");
        await Assert.That(settings.Publishers).IsEqualTo("publisher1,publisher2");
        await Assert.That(settings.TableReport).IsTrue(); // DefaultValue(true)
        await Assert.That(settings.Detailed).IsTrue();
    }

    [Test]
    public async Task UpdateWorkflowStateSettings_ParsesConfigAndDefaults()
    {
        var result = Parse<UpdateWorkflowStateSettings>("-c", "cfg.json");

        var settings = (UpdateWorkflowStateSettings)result.Settings!;

        await Assert.That(settings.Config).IsEqualTo("cfg.json");
        await Assert.That(settings.TableReport).IsTrue(); // DefaultValue(true)
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
    public async Task PushVerb_ParsesRequiredPositionalArgumentAndOptions()
    {
        var result = Parse<PushVerb>(
            "c:/TargetDir/plugin.dll",
            "--solution", "samplesolution",
            "--publish",
            "--delete-on-upgrade",
            "--no-migrate-custom-apis",
            "--config", "webresources.json");

        var settings = (PushVerb)result.Settings!;

        await Assert.That(settings.Target).IsEqualTo("c:/TargetDir/plugin.dll");
        await Assert.That(settings.Solution).IsEqualTo("samplesolution");
        await Assert.That(settings.Publish).IsTrue();
        await Assert.That(settings.DeleteOnUpgrade).IsTrue();
        await Assert.That(settings.NoMigrateCustomApis).IsTrue();
        await Assert.That(settings.DeleteObsolete).IsFalse();
        await Assert.That(settings.Config).IsEqualTo("webresources.json");
    }

    [Test]
    public async Task NamedProfileSettings_ParsesPositionalArgument()
    {
        var result = Parse<NamedProfileSettings>("myprofile");

        await Assert.That(((NamedProfileSettings)result.Settings!).Name).IsEqualTo("myprofile");
    }

    [Test]
    public async Task CreateProfileSettings_ParsesPositionalArgumentsAndFlags()
    {
        var result = Parse<CreateProfileSettings>(
            "myprofile",
            "https://org.crm.dynamics.com",
            "--msal");

        var settings = (CreateProfileSettings)result.Settings!;

        await Assert.That(settings.Name).IsEqualTo("myprofile");
        await Assert.That(settings.ConnectionString).IsEqualTo("https://org.crm.dynamics.com");
        await Assert.That(settings.TokenBased).IsTrue();
        await Assert.That(settings.SkipChecking).IsFalse();
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
}
