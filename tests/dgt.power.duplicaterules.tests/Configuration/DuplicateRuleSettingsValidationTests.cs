// Copyright (c) DIGITALL Nature. All rights reserved

using dgt.power.duplicaterules.Commands;

namespace dgt.power.duplicaterules.tests.Configuration;

public class DuplicateRuleSettingsValidationTests
{
    [Test]
    public async Task Init_StateCaptureWithoutSelectors_IsValid()
    {
        var settings = new DuplicateRuleInitSettings { File = OutputPath() };

        await Assert.That(settings.Validate().Successful).IsTrue();
        await Assert.That(settings.Capture).IsEqualTo("state");
    }

    [Test]
    public async Task Init_EntityAndRuleIds_AreValidatedAndDeduplicated()
    {
        var settings = new DuplicateRuleInitSettings
        {
            File = OutputPath(),
            Entity = "contact",
            RuleIds = ["6f1c2b4e-8d3a-4f5b-9c7d-1e2f3a4b5c6d", "6F1C2B4E-8D3A-4F5B-9C7D-1E2F3A4B5C6D"]
        };

        await Assert.That(settings.Validate().Successful).IsTrue();
        await Assert.That(settings.GetParsedRuleIds()).Count().IsEqualTo(1);
    }

    [Test]
    [Arguments("none", null, null)]
    [Arguments("state", " contact", null)]
    [Arguments("state", "", null)]
    [Arguments("state", null, "00000000-0000-0000-0000-000000000000")]
    [Arguments("state", null, "6f1c2b4e8d3a4f5b9c7d1e2f3a4b5c6d")]
    [Arguments("state", null, "not-a-guid")]
    [Arguments("owner", null, null)]
    [Arguments("both", null, null)]
    public async Task Init_InvalidOptions_FailLocally(string capture, string? entity, string? ruleId)
    {
        var settings = new DuplicateRuleInitSettings
        {
            File = OutputPath(),
            Capture = capture,
            Entity = entity,
            RuleIds = ruleId is null ? null : [ruleId]
        };

        await Assert.That(settings.Validate().Successful).IsFalse();
    }

    [Test]
    public async Task Init_OwnerCaptureWithMapping_IsValid()
    {
        var settings = new DuplicateRuleInitSettings
        {
            File = OutputPath(),
            Capture = "both",
            OwnerMapping = FixturePath("owners.source.json")
        };

        await Assert.That(settings.Validate().Successful).IsTrue();
    }

    [Test]
    public async Task Init_ExistingOutputRequiresOverwrite()
    {
        var existing = FixturePath("valid-v1.json");

        await Assert.That(new DuplicateRuleInitSettings { File = existing }.Validate().Successful).IsFalse();
        await Assert.That(new DuplicateRuleInitSettings { File = existing, Overwrite = true }.Validate().Successful).IsTrue();
    }

    [Test]
    public async Task Apply_OwnerIntentRequiresMapping()
    {
        var input = FixturePath("valid-v1.json");

        await Assert.That(new DuplicateRuleApplySettings { File = input }.Validate().Successful).IsFalse();
        await Assert.That(new DuplicateRuleApplySettings
        {
            File = input,
            OwnerMapping = FixturePath("owners.source.json")
        }.Validate().Successful).IsTrue();
    }

    [Test]
    public async Task Apply_StateOnlyConfigurationDoesNotRequireMapping()
    {
        var settings = new DuplicateRuleApplySettings { File = FixturePath("state-only.json") };

        await Assert.That(settings.Validate().Successful).IsTrue();
        await Assert.That(settings.Timeout).IsEqualTo(DuplicateRuleApplySettings.DefaultTimeoutSeconds);
    }

    [Test]
    [Arguments(0)]
    [Arguments(-1)]
    public async Task Apply_NonPositiveTimeout_FailsLocally(int timeout)
    {
        var settings = new DuplicateRuleApplySettings { File = FixturePath("state-only.json"), Timeout = timeout };

        await Assert.That(settings.Validate().Successful).IsFalse();
    }

    [Test]
    public async Task Apply_InvalidConfiguration_FailsLocally()
    {
        var path = Path.Combine(AppContext.BaseDirectory, $"invalid-{Guid.NewGuid():N}.json");
        await File.WriteAllTextAsync(path, """{"schemaVersion":2,"rules":[]}""");
        try
        {
            await Assert.That(new DuplicateRuleApplySettings { File = path }.Validate().Successful).IsFalse();
        }
        finally
        {
            File.Delete(path);
        }
    }

    private static string OutputPath() =>
        Path.Combine(AppContext.BaseDirectory, $"duplicaterules-output-{Guid.NewGuid():N}.json");

    private static string FixturePath(string name) =>
        Path.Combine(AppContext.BaseDirectory, "Resources", "DuplicateRuleConfiguration", name);
}
