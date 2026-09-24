// Copyright (c) DIGITALL Nature. All rights reserved
// DIGITALL Nature licenses this file to you under the Microsoft Public License.

using System.Text.Json;
using dgt.power.dataverse;
using dgt.power.solution.Rules;
using dgt.power.solution.tests.Base;
using dgt.power.tests.Extensions;
using dgt.power.tests.FakeExecutor;
using Digitall.Dataverse.Testing;

namespace dgt.power.solution.tests;

public class UnmanagedFieldNamingRuleTests : LintTestsBase<SolutionLintCommand>
{
    private const string SolutionName = "sample_solution";

    protected override CommandTestContext<SolutionLintCommand, SolutionLintSettings> GetContext() => CreateContext(BuildEntityMetadata());

    [Test]
    public async Task EvaluateAsync_WithDefaultPrefix_FlagsExactlyTheExpectedViolations()
    {
        var findings = await EvaluateAsync(BuildEntityMetadata(), ["dgt_"]);

        var violatingFields = findings.Select(finding => finding.ComponentLogicalName!).ToHashSet(StringComparer.Ordinal);
        await Assert.That(violatingFields).IsEquivalentTo(
        [
            "dgt_owner", // Lookup missing the _id suffix
            "unknown_field_txt", // wrong publisher prefix
            "dgt_BadName_txt", // not snake_case
            "contoso_amount_cur", // wrong publisher prefix when only "dgt_" is configured
            "dgt_badcalc_txt" // calculated plain-Text field missing the required _cf modifier
        ]);
        foreach (var finding in findings)
        {
            await Assert.That(finding.RuleId).IsEqualTo("naming.unmanaged-field-logicalname");
        }
    }

    [Test]
    public async Task EvaluateAsync_SystemProvisionedFieldWithoutUnderscore_IsNeverFlagged()
    {
        var findings = await EvaluateAsync(BuildEntityMetadata(), ["dgt_"]);

        await Assert.That(findings.Select(finding => finding.ComponentLogicalName)).DoesNotContain("customentitydefault");
    }

    [Test]
    public async Task EvaluateAsync_ManagedOrNonCustomAttributes_AreSkipped()
    {
        var findings = await EvaluateAsync(BuildEntityMetadata(), ["dgt_"]);

        var flaggedNames = findings.Select(finding => finding.ComponentLogicalName).ToHashSet(StringComparer.Ordinal);
        await Assert.That(flaggedNames).DoesNotContain("dgt_managed_field_txt");
        await Assert.That(flaggedNames).DoesNotContain("address1_line1");
    }

    [Test]
    public async Task EvaluateAsync_WithMultiplePublisherPrefixes_AcceptsEitherPrefix()
    {
        var findings = await EvaluateAsync(BuildEntityMetadata(), ["dgt_", "contoso_"]);

        await Assert.That(findings.Select(finding => finding.ComponentLogicalName)).DoesNotContain("contoso_amount_cur");
    }

    [Test]
    public async Task EvaluateAsync_EntityAddedWithIncludeSubcomponents_ChecksAttributesWithoutIndividualComponentRows()
    {
        // RootComponentBehavior.IncludeSubcomponents ('whole table') never gets per-attribute
        // solutioncomponent rows - the same violations must still surface purely from metadata.
        var findings = await EvaluateAsync(BuildEntityMetadata(), ["dgt_"], SolutionComponent.Options.RootComponentBehavior.IncludeSubcomponents);

        var violatingFields = findings.Select(finding => finding.ComponentLogicalName!).ToHashSet(StringComparer.Ordinal);
        await Assert.That(violatingFields).IsEquivalentTo(
        [
            "dgt_owner",
            "unknown_field_txt",
            "dgt_BadName_txt",
            "contoso_amount_cur",
            "dgt_badcalc_txt"
        ]);
    }

    [Test]
    public async Task EvaluateAsync_EntityIncludedAsShellOnly_NoAttributesAreChecked()
    {
        var findings = await EvaluateAsync(BuildEntityMetadata(), ["dgt_"], SolutionComponent.Options.RootComponentBehavior.IncludeAsShellOnly);

        await Assert.That(findings).IsEmpty();
    }

    [Test]
    public async Task Execute_WhenFindingsExceedFailOnThreshold_ReturnsFalse()
    {
        var path = WriteConfig(["dgt_"]);

        var result = GetContext().Execute(new SolutionLintSettings
        {
            Solution = SolutionName,
            Config = path
        });

        await Assert.That(result).IsFalse();
    }

    [Test]
    public async Task Execute_WithHandAuthoredLowercaseJsonConfig_BindsRuleOptionsCorrectly()
    {
        // Regression test: LintConfig.Rules is a get-only Dictionary property (to preserve its
        // OrdinalIgnoreCase comparer). Without [JsonObjectCreationHandling(Populate)],
        // System.Text.Json silently skips read-only properties entirely - the whole "rules" node
        // (and with it "options"/"publisherPrefixes") was ignored, no exception, no rule ran with
        // its configured options.
        var configPath = Path.Combine(Directory.GetCurrentDirectory(), $"{Guid.NewGuid():N}.lint.config.json");
        await File.WriteAllTextAsync(configPath, """
            {
              "version": 1,
              "rules": {
                "naming.unmanaged-field-logicalname": {
                  "enabled": true,
                  "severity": "Error",
                  "options": {
                    "publisherPrefixes": ["dgt_", "contoso_"]
                  }
                }
              }
            }
            """);
        var reportPath = Path.Combine(Directory.GetCurrentDirectory(), $"{Guid.NewGuid():N}.report.json");

        GetContext().Execute(new SolutionLintSettings
        {
            Solution = SolutionName,
            Config = configPath,
            Report = reportPath,
            FailOn = "None"
        });

        var report = await File.ReadAllTextAsync(reportPath);
        await Assert.That(report).Contains("dgt_owner"); // proves "enabled" bound and the rule ran
        await Assert.That(report).DoesNotContain("contoso_amount_cur"); // proves "publisherPrefixes" bound
    }

    private async Task<IReadOnlyList<LintFinding>> EvaluateAsync(EntityMetadata entityMetadata, IReadOnlyList<string> publisherPrefixes, int rootComponentBehavior = SolutionComponent.Options.RootComponentBehavior.DoNotIncludeSubcomponents)
    {
        var testContext = CreateContext(entityMetadata, rootComponentBehavior);
        var context = await LintContext.CreateAsync(testContext.FakedService, [SolutionName], CancellationToken.None);
        var ruleConfig = CreateRuleConfig(publisherPrefixes);
        return await new UnmanagedFieldNamingRule().EvaluateAsync(context, ruleConfig, CancellationToken.None);
    }

    private CommandTestContext<SolutionLintCommand, SolutionLintSettings> CreateContext(EntityMetadata entityMetadata, int rootComponentBehavior = SolutionComponent.Options.RootComponentBehavior.DoNotIncludeSubcomponents)
    {
        return GetBuilder()
            .WithFakeMessageExecutor(new RetrieveEntityExecutor())
            .WithMetaData(entityMetadata)
            .WithData(service => PrepareData(service, rootComponentBehavior))
            .Build();
    }

    // Covers every field-type suffix from https://digitallnature.github.io/customizing/naming-conventions/
    // plus the deliberate violations/skip scenarios this rule must handle.
    private static EntityMetadata BuildEntityMetadata()
    {
        var attributes = new List<AttributeMetadata>
        {
            CreateAttribute(new LookupAttributeMetadata(), "dgt_customer_id"),
            CreateAttribute(new LookupAttributeMetadata(), "dgt_owner"), // missing _id suffix -> violation
            CreateAttribute(new PicklistAttributeMetadata(), "dgt_status_set"),
            CreateAttribute(new MultiSelectPicklistAttributeMetadata(), "dgt_categories_mset"),
            CreateAttribute(new MoneyAttributeMetadata(), "dgt_amount_cur"),
            CreateAttribute(new DateTimeAttributeMetadata(), "dgt_duedate_dt"),
            CreateAttribute(new LookupAttributeMetadata(), "dgt_customer_vid", a => a.SetSealedPropertyValue(nameof(AttributeMetadata.AttributeType), AttributeTypeCode.Customer)),
            CreateAttribute(new IntegerAttributeMetadata(), "dgt_quantity_int"),
            CreateAttribute(new IntegerAttributeMetadata { Format = IntegerFormat.Duration }, "dgt_runtime_dur"),
            CreateAttribute(new IntegerAttributeMetadata { Format = IntegerFormat.Language }, "dgt_preferredlanguage_lcid"),
            CreateAttribute(new IntegerAttributeMetadata { Format = IntegerFormat.TimeZone }, "dgt_timezone_tzid"),
            CreateAttribute(new DecimalAttributeMetadata(), "dgt_taxrate_dec"),
            CreateAttribute(new DoubleAttributeMetadata(), "dgt_measurement_flt"),
            CreateAttribute(new BooleanAttributeMetadata(), "dgt_isactive_bit"),
            CreateAttribute(new FileAttributeMetadata(), "dgt_attachment_file"),
            CreateAttribute(new ImageAttributeMetadata(), "dgt_logo_img"),
            CreateAttribute(new MemoAttributeMetadata(), "dgt_description_txt"),
            CreateAttribute(new StringAttributeMetadata { FormatName = StringFormatName.Text }, "dgt_name"), // plain Text: no suffix required
            CreateAttribute(new StringAttributeMetadata { FormatName = StringFormatName.Text }, "dgt_title_txt"),
            CreateAttribute(new StringAttributeMetadata { FormatName = StringFormatName.Url }, "dgt_website_url"),
            CreateAttribute(new StringAttributeMetadata { FormatName = StringFormatName.Phone }, "dgt_phone_number"),
            CreateAttribute(new StringAttributeMetadata { FormatName = StringFormatName.Email }, "dgt_email_email"),
            CreateAttribute(new MoneyAttributeMetadata { SourceType = 2 }, "dgt_totalamount_cur_rf"), // Rollup
            CreateAttribute(new StringAttributeMetadata { FormatName = StringFormatName.Text, SourceType = 1 }, "dgt_fullname_txt_cf"), // Calculated
            CreateAttribute(new StringAttributeMetadata { FormatName = StringFormatName.Text, SourceType = 1 }, "dgt_badcalc_txt"), // Calculated but missing the required _cf modifier -> violation
            CreateAttribute(new PicklistAttributeMetadata { SourceType = 3 }, "dgt_status_set_fx"), // Formula
            CreateAttribute(new StringAttributeMetadata { FormatName = StringFormatName.Text }, "unknown_field_txt"), // wrong prefix -> violation
            CreateAttribute(new StringAttributeMetadata { FormatName = StringFormatName.Text }, "dgt_BadName_txt"), // not snake_case -> violation
            CreateAttribute(new StringAttributeMetadata { FormatName = StringFormatName.Text }, "contoso_amount_cur"), // only valid with an extra configured prefix
            CreateAttribute(new StringAttributeMetadata(), "customentitydefault"), // Dataverse-provisioned, no prefix at all -> must never be flagged
            CreateAttribute(new StringAttributeMetadata(), "dgt_managed_field_txt", isManaged: true), // managed -> skipped
            CreateAttribute(new StringAttributeMetadata(), "address1_line1", isCustom: false) // OOB attribute -> skipped
        };

        var entity = new EntityMetadata
        {
            LogicalName = "account",
            MetadataId = Guid.NewGuid()
        };
        entity.SetAttributeCollection(attributes);
        return entity;
    }

    private static T CreateAttribute<T>(T attribute, string logicalName, Action<T>? configure = null, bool isCustom = true, bool isManaged = false)
        where T : AttributeMetadata
    {
        attribute.MetadataId = Guid.NewGuid();
        attribute.LogicalName = logicalName;
        attribute.SetSealedPropertyValue(nameof(AttributeMetadata.IsCustomAttribute), isCustom);
        attribute.SetSealedPropertyValue(nameof(AttributeMetadata.IsManaged), isManaged);
        attribute.SetSealedPropertyValue(nameof(AttributeMetadata.EntityLogicalName), "account");
        configure?.Invoke(attribute);
        return attribute;
    }

    private static IEnumerable<Entity> PrepareData(FakeOrganizationServiceAsync service, int rootComponentBehavior)
    {
        var solution = new Solution(Guid.NewGuid())
        {
            UniqueName = SolutionName
        };

        var metadata = service.State.EntityMetadata["account"];
        var entityComponent = new SolutionComponent(Guid.NewGuid())
        {
            [SolutionComponent.LogicalNames.ComponentType] = new OptionSetValue(SolutionComponent.Options.ComponentType.Entity),
            [SolutionComponent.LogicalNames.ObjectId] = metadata.MetadataId,
            [SolutionComponent.LogicalNames.SolutionId] = solution.ToEntityReference(),
            [SolutionComponent.LogicalNames.IsMetadata] = true,
            [SolutionComponent.LogicalNames.RootComponentBehavior] = new OptionSetValue(rootComponentBehavior)
        };

        // IncludeSubcomponents ('whole table') never gets per-attribute rows of its own.
        var attributeComponents = rootComponentBehavior == SolutionComponent.Options.RootComponentBehavior.DoNotIncludeSubcomponents
            ? metadata.Attributes?
                .Select(attribute => new SolutionComponent(Guid.NewGuid())
                {
                    [SolutionComponent.LogicalNames.ComponentType] = new OptionSetValue(SolutionComponent.Options.ComponentType.Attribute),
                    [SolutionComponent.LogicalNames.ObjectId] = attribute.MetadataId,
                    [SolutionComponent.LogicalNames.SolutionId] = solution.ToEntityReference(),
                    [SolutionComponent.LogicalNames.IsMetadata] = true,
                    [SolutionComponent.LogicalNames.RootSolutionComponentId] = entityComponent.Id
                })
                .Cast<Entity>()
                .ToList() ?? []
            : [];

        return [solution, entityComponent, .. attributeComponents];
    }

    private static LintRuleConfigEntry CreateRuleConfig(IReadOnlyList<string> publisherPrefixes)
    {
        var json = JsonSerializer.Serialize(new { publisherPrefixes });
        return new LintRuleConfigEntry
        {
            Enabled = true,
            Severity = LintSeverity.Error,
            Options = JsonDocument.Parse(json).RootElement.Clone()
        };
    }

    private static string WriteConfig(IReadOnlyList<string> publisherPrefixes)
    {
        var path = Path.Combine(Directory.GetCurrentDirectory(), $"{Guid.NewGuid():N}.lint.config.json");
        var config = new LintConfig
        {
            Version = 1,
            Rules =
            {
                ["naming.unmanaged-field-logicalname"] = CreateRuleConfig(publisherPrefixes)
            }
        };

        File.WriteAllText(path, JsonSerializer.Serialize(config, new JsonSerializerOptions { WriteIndented = true }));
        return path;
    }
}

