// Copyright (c) DIGITALL Nature. All rights reserved

using dgt.power.process.Commands;

namespace dgt.power.process.tests.Configuration;

public class ProcessSettingsValidationTests
{
    [Test]
    public async Task Init_StateCaptureWithoutMapping_AcceptsRelativeOutputAndRequiredSolution()
    {
        var settings = new ProcessInitSettings
        {
            File = $"process-output-{Guid.NewGuid():N}.json",
            Solutions = ["ContosoCore", "ContosoFlows"]
        };

        await Assert.That(settings.Validate().Successful).IsTrue();
    }

    [Test]
    public async Task Init_BothCaptureRequiresAndValidatesMapping()
    {
        var settings = new ProcessInitSettings
        {
            File = $"process-output-{Guid.NewGuid():N}.json",
            Solutions = ["ContosoCore"],
            Capture = "both",
            OwnerMapping = FixturePath("owners.source.json")
        };

        await Assert.That(settings.Validate().Successful).IsTrue();
        await Assert.That(new ProcessInitSettings
        {
            File = settings.File,
            Solutions = settings.Solutions,
            Capture = "both"
        }.Validate().Successful).IsFalse();
    }

    [Test]
    public async Task Apply_OwnerConfigAndActivateAsAcceptValidMappingFixture()
    {
        var settings = new ProcessApplySettings
        {
            File = FixturePath("valid-v1.json"),
            OwnerMapping = FixturePath("owners.source.json"),
            ActivateAs = "integrationUser",
            DryRun = true
        };

        await Assert.That(settings.Validate().Successful).IsTrue();
    }

    [Test]
    public async Task Apply_OwnerIntentWithoutMappingAndConflictingCallerOptionsFailLocally()
    {
        var input = FixturePath("valid-v1.json");
        await Assert.That(new ProcessApplySettings { File = input }.Validate().Successful).IsFalse();
        await Assert.That(new ProcessApplySettings
        {
            File = input,
            OwnerMapping = FixturePath("owners.source.json"),
            ActivateAs = "integrationUser",
            ActivateAsOwner = true
        }.Validate().Successful).IsFalse();
    }

    private static string FixturePath(string name) =>
        Path.Combine(AppContext.BaseDirectory, "Resources", "ProcessConfiguration", name);
}
