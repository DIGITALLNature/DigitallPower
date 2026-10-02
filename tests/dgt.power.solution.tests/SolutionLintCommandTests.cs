// Copyright (c) DIGITALL Nature. All rights reserved
// DIGITALL Nature licenses this file to you under the Microsoft Public License.

using System.Text.Json;
using dgt.power.dataverse;
using dgt.power.solution.tests.Base;
using dgt.power.tests.Extensions;
using dgt.power.tests.FakeExecutor;
using Digitall.Dataverse.Testing;

namespace dgt.power.solution.tests;

public class SolutionLintCommandTests : LintTestsBase<SolutionLintCommand>
{
    private const string SolutionName = "sample_solution";

    protected override CommandTestContext<SolutionLintCommand, SolutionLintSettings> GetContext() =>
        GetBuilder()
            .WithFakeMessageExecutor(new RetrieveEntityExecutor())
            .WithMetaData(BuildEntityMetadata())
            .WithData(PrepareData)
            .Build();

    [Test]
    public async Task Execute_WithFailOnNone_AlwaysSucceedsDespiteFindings()
    {
        var configPath = WriteConfig();

        var result = GetContext().Execute(new SolutionLintSettings
        {
            Solution = SolutionName,
            Config = configPath,
            FailOn = "None"
        });

        await Assert.That(result).IsTrue();
    }

    [Test]
    public async Task Execute_UpdateBaselineWithoutBaselinePath_Fails()
    {
        var configPath = WriteConfig();

        var result = GetContext().Execute(new SolutionLintSettings
        {
            Solution = SolutionName,
            Config = configPath,
            UpdateBaseline = true
        });

        await Assert.That(result).IsFalse();
    }

    [Test]
    public async Task Execute_UpdateBaseline_WritesSarifBaselineAndSucceeds()
    {
        var configPath = WriteConfig();
        var baselinePath = TempPath("baseline.sarif.json");

        var result = GetContext().Execute(new SolutionLintSettings
        {
            Solution = SolutionName,
            Config = configPath,
            Baseline = baselinePath,
            UpdateBaseline = true
        });

        await Assert.That(result).IsTrue();
        await Assert.That(File.Exists(baselinePath)).IsTrue();
        await Assert.That(await File.ReadAllTextAsync(baselinePath)).Contains("naming.unmanaged-field-logicalname");
    }

    [Test]
    public async Task Execute_WithBaselineContainingTheFinding_SuppressesItFromTheFailGate()
    {
        var configPath = WriteConfig();
        var baselinePath = TempPath("baseline.sarif.json");
        var context = GetContext(); // reused so the attribute's MetadataId stays stable across both runs

        // First pass: bake the current (still non-conformant) state into the baseline.
        var baselineRunResult = context.Execute(new SolutionLintSettings
        {
            Solution = SolutionName,
            Config = configPath,
            Baseline = baselinePath,
            UpdateBaseline = true
        });
        await Assert.That(baselineRunResult).IsTrue();

        // Second pass: the same violation still exists but is now baselined, so the gate must pass.
        var gatedResult = context.Execute(new SolutionLintSettings
        {
            Solution = SolutionName,
            Config = configPath,
            Baseline = baselinePath,
            FailOn = "Error"
        });

        await Assert.That(gatedResult).IsTrue();
    }

    [Test]
    public async Task Execute_WithoutBaseline_FailsOnTheSameFinding()
    {
        var configPath = WriteConfig();

        var result = GetContext().Execute(new SolutionLintSettings
        {
            Solution = SolutionName,
            Config = configPath,
            FailOn = "Error"
        });

        await Assert.That(result).IsFalse();
    }

    [Test]
    public async Task Execute_WithSarifOutput_WritesFindingsAsSarif()
    {
        var configPath = WriteConfig();
        var sarifPath = TempPath("findings.sarif.json");

        GetContext().Execute(new SolutionLintSettings
        {
            Solution = SolutionName,
            Config = configPath,
            SarifOutput = sarifPath,
            FailOn = "None"
        });

        var sarif = await File.ReadAllTextAsync(sarifPath);
        await Assert.That(sarif).Contains("\"ruleId\":\"naming.unmanaged-field-logicalname\"");
        await Assert.That(sarif).Contains("\"version\":\"2.1.0\"");
    }

    [Test]
    public async Task Execute_WithReport_MarksBaselinedFindingsInJson()
    {
        var configPath = WriteConfig();
        var baselinePath = TempPath("baseline.sarif.json");
        var reportPath = TempPath("report.json");
        var context = GetContext(); // reused so the attribute's MetadataId stays stable across both runs

        context.Execute(new SolutionLintSettings
        {
            Solution = SolutionName,
            Config = configPath,
            Baseline = baselinePath,
            UpdateBaseline = true
        });

        context.Execute(new SolutionLintSettings
        {
            Solution = SolutionName,
            Config = configPath,
            Baseline = baselinePath,
            Report = reportPath,
            FailOn = "None"
        });

        var report = await File.ReadAllTextAsync(reportPath);
        await Assert.That(report).Contains("\"Baselined\": true");
    }

    [Test]
    public async Task Execute_WithUnknownRuleIdInConfig_Fails()
    {
        var configPath = TempPath("lint.config.json");
        var config = new LintConfig
        {
            Version = 1,
            Rules =
            {
                ["naming.does-not-exist"] = new LintRuleConfigEntry { Enabled = true }
            }
        };
        await File.WriteAllTextAsync(configPath, JsonSerializer.Serialize(config, new JsonSerializerOptions { WriteIndented = true }));

        var result = GetContext().Execute(new SolutionLintSettings
        {
            Solution = SolutionName,
            Config = configPath,
            FailOn = "None"
        });

        await Assert.That(result).IsFalse();
    }

    [Test]
    public async Task Execute_WithInvalidOptionsShapeInConfig_Fails()
    {
        // "options" must be a JSON object (per-rule shape) - an array here is a typo/misconfiguration.
        var configPath = TempPath("lint.config.json");
        await File.WriteAllTextAsync(configPath, """
            {
              "version": 1,
              "rules": {
                "naming.unmanaged-field-logicalname": {
                  "enabled": true,
                  "options": ["dgt_"]
                }
              }
            }
            """);

        var result = GetContext().Execute(new SolutionLintSettings
        {
            Solution = SolutionName,
            Config = configPath,
            FailOn = "None"
        });

        await Assert.That(result).IsFalse();
    }

    [Test]
    public async Task Execute_WithUnknownRuleIdInRulesOption_Fails()
    {
        var configPath = WriteConfig();

        var result = GetContext().Execute(new SolutionLintSettings
        {
            Solution = SolutionName,
            Config = configPath,
            Rules = "naming.does-not-exist",
            FailOn = "None"
        });

        await Assert.That(result).IsFalse();
    }

    [Test]
    public async Task Execute_WithInvalidFailOnValue_Fails()
    {
        var configPath = WriteConfig();

        var result = GetContext().Execute(new SolutionLintSettings
        {
            Solution = SolutionName,
            Config = configPath,
            FailOn = "Warnng"
        });

        await Assert.That(result).IsFalse();
    }

    private static EntityMetadata BuildEntityMetadata()
    {
        var violatingField = new StringAttributeMetadata { MetadataId = Guid.NewGuid(), LogicalName = "unknown_field_txt", FormatName = StringFormatName.Text };
        violatingField.SetSealedPropertyValue(nameof(AttributeMetadata.IsCustomAttribute), true);
        violatingField.SetSealedPropertyValue(nameof(AttributeMetadata.IsManaged), false);
        violatingField.SetSealedPropertyValue(nameof(AttributeMetadata.EntityLogicalName), "account");

        var entity = new EntityMetadata { LogicalName = "account", MetadataId = Guid.NewGuid() };
        entity.SetAttributeCollection([violatingField]);
        return entity;
    }

    private static IEnumerable<Entity> PrepareData(FakeOrganizationServiceAsync service)
    {
        var solution = new Solution(Guid.NewGuid()) { UniqueName = SolutionName };
        var metadata = service.State.EntityMetadata["account"];
        var entityComponent = new SolutionComponent(Guid.NewGuid())
        {
            [SolutionComponent.LogicalNames.ComponentType] = new OptionSetValue(SolutionComponent.Options.ComponentType.Entity),
            [SolutionComponent.LogicalNames.ObjectId] = metadata.MetadataId,
            [SolutionComponent.LogicalNames.SolutionId] = solution.ToEntityReference(),
            [SolutionComponent.LogicalNames.IsMetadata] = true,
            [SolutionComponent.LogicalNames.RootComponentBehavior] = new OptionSetValue(SolutionComponent.Options.RootComponentBehavior.DoNotIncludeSubcomponents)
        };

        var attributeComponents = metadata.Attributes?
            .Select(attribute => new SolutionComponent(Guid.NewGuid())
            {
                [SolutionComponent.LogicalNames.ComponentType] = new OptionSetValue(SolutionComponent.Options.ComponentType.Attribute),
                [SolutionComponent.LogicalNames.ObjectId] = attribute.MetadataId,
                [SolutionComponent.LogicalNames.SolutionId] = solution.ToEntityReference(),
                [SolutionComponent.LogicalNames.IsMetadata] = true,
                [SolutionComponent.LogicalNames.RootSolutionComponentId] = entityComponent.Id
            })
            .Cast<Entity>()
            .ToList() ?? [];

        return [solution, entityComponent, .. attributeComponents];
    }

    private static string WriteConfig()
    {
        var path = TempPath("lint.config.json");
        var config = new LintConfig
        {
            Version = 1,
            Rules =
            {
                ["naming.unmanaged-field-logicalname"] = new LintRuleConfigEntry { Enabled = true, Severity = LintSeverity.Error }
            }
        };

        File.WriteAllText(path, JsonSerializer.Serialize(config, new JsonSerializerOptions { WriteIndented = true }));
        return path;
    }

    private static string TempPath(string fileName) => Path.Combine(Directory.GetCurrentDirectory(), $"{Guid.NewGuid():N}.{fileName}");
}
