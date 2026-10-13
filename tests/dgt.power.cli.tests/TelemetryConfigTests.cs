// Copyright (c) DIGITALL Nature. All rights reserved
// DIGITALL Nature licenses this file to you under the Microsoft Public License.

using dgt.power.common;
using dgt.power.common.Storage;
using dgt.power.Telemetry;

namespace dgt.power.cli.tests;

[NotInParallel("CiEnvironmentVariables")]
public class TelemetryConfigTests
{
    [Test]
    [Arguments(true, null, true)]
    [Arguments(false, null, false)]
    [Arguments(true, "DGTP_TELEMETRY_OPTOUT", false)]
    [Arguments(true, "DO_NOT_TRACK", false)]
    [Arguments(false, "DO_NOT_TRACK", false)]
    public async Task IsEnabled_RespectsSavedPreferenceAndEnvironment(bool savedEnabled, string? optOutVariable, bool expected)
    {
        var original = SaveOptOutVariables();
        var directory = CreateTemporaryDirectory();
        ClearOptOutVariables();
        try
        {
            if (optOutVariable is not null)
            {
                Environment.SetEnvironmentVariable(optOutVariable, "true");
            }

            var home = new DgtpHome(directory);
            var store = new StateStore(home);
            store.SetTelemetryEnabled(savedEnabled);
            await Assert.That(TelemetryConfig.IsEnabled(store)).IsEqualTo(expected);
            var state = System.Text.Json.Nodes.JsonNode.Parse(await File.ReadAllTextAsync(home.StatePath))!;
            await Assert.That(state["telemetry"]!["installId"]).IsNull();
        }
        finally
        {
            RestoreOptOutVariables(original);
            Directory.Delete(directory, recursive: true);
        }
    }

    [Test]
    public async Task IsOptedOut_ReturnsFalse_WhenEnvVarNotSet()
    {
        var original = SaveOptOutVariables();
        ClearOptOutVariables();

        try
        {
            await Assert.That(TelemetryConfig.IsOptedOut).IsFalse();
        }
        finally
        {
            RestoreOptOutVariables(original);
        }
    }

    [Test]
    [Arguments("1")]
    [Arguments("true")]
    [Arguments("yes")]
    public async Task IsOptedOut_ReturnsTrue_WhenEnvVarSet(string value)
    {
        var original = SaveOptOutVariables();
        ClearOptOutVariables();
        Environment.SetEnvironmentVariable("DGTP_TELEMETRY_OPTOUT", value);
        try
        {
            await Assert.That(TelemetryConfig.IsOptedOut).IsTrue();
        }
        finally
        {
            RestoreOptOutVariables(original);
        }
    }

    [Test]
    [Arguments("0")]
    [Arguments("false")]
    [Arguments("no")]
    [Arguments("")]
    public async Task IsOptedOut_ReturnsFalse_WhenEnvVarSetToNonOptOutValue(string value)
    {
        var original = SaveOptOutVariables();
        ClearOptOutVariables();
        Environment.SetEnvironmentVariable("DGTP_TELEMETRY_OPTOUT", value);
        try
        {
            await Assert.That(TelemetryConfig.IsOptedOut).IsFalse();
        }
        finally
        {
            RestoreOptOutVariables(original);
        }
    }

    [Test]
    [Arguments("1")]
    [Arguments("true")]
    [Arguments("yes")]
    public async Task IsOptedOut_ReturnsTrue_WhenDoNotTrackIsSet(string value)
    {
        var original = SaveOptOutVariables();
        ClearOptOutVariables();
        Environment.SetEnvironmentVariable("DO_NOT_TRACK", value);
        try
        {
            await Assert.That(TelemetryConfig.IsOptedOut).IsTrue();
        }
        finally
        {
            RestoreOptOutVariables(original);
        }
    }

    [Test]
    public async Task IsOptedOut_ReturnsFalse_WhenLegacyVariableIsSet()
    {
        var original = SaveOptOutVariables();
        ClearOptOutVariables();
        Environment.SetEnvironmentVariable("DGT_TELEMETRY_OPTOUT", "true");
        try
        {
            await Assert.That(TelemetryConfig.IsOptedOut).IsFalse();
        }
        finally
        {
            RestoreOptOutVariables(original);
        }
    }

    [Test]
    public async Task IsCi_ReturnsFalse_WhenNoCiEnvVarsSet()
    {
        // Save and clear all CI env vars
        var savedVars = ExecutionEnvironment.CiEnvironmentVariables
            .ToDictionary(environmentVariable => environmentVariable,
                environmentVariable => Environment.GetEnvironmentVariable(environmentVariable));

        try
        {
            foreach (var key in savedVars.Keys)
            {
                Environment.SetEnvironmentVariable(key, null);
            }

            await Assert.That(TelemetryConfig.IsCi).IsFalse();
        }
        finally
        {
            foreach (var (key, value) in savedVars)
            {
                Environment.SetEnvironmentVariable(key, value);
            }
        }
    }

    [Test]
    [Arguments("TF_BUILD", "True")]
    [Arguments("BUILD_BUILDURI", "vstfs:///Build/Build/123")]
    [Arguments("GITHUB_ACTIONS", "true")]
    [Arguments("GITLAB_CI", "true")]
    [Arguments("JENKINS_URL", "http://jenkins.local")]
    [Arguments("CI", "true")]
    public async Task IsCi_ReturnsTrue_WhenCiEnvVarSet(string envVar, string envValue)
    {
        // Save all CI env vars and clear them to isolate
        var ciVars = ExecutionEnvironment.CiEnvironmentVariables.ToArray();
        var saved = ciVars.ToDictionary(k => k, Environment.GetEnvironmentVariable);

        try
        {
            foreach (var key in ciVars)
            {
                Environment.SetEnvironmentVariable(key, null);
            }

            Environment.SetEnvironmentVariable(envVar, envValue);

            await Assert.That(TelemetryConfig.IsCi).IsTrue();
        }
        finally
        {
            foreach (var (key, value) in saved)
            {
                Environment.SetEnvironmentVariable(key, value);
            }
        }
    }

    [Test]
    public async Task GetOrCreateInstallId_CreatesNewGuid_WhenStateHasNoId()
    {
        var directory = CreateTemporaryDirectory();
        try
        {
            var id = TelemetryConfig.GetOrCreateInstallId(new StateStore(new DgtpHome(directory)));

            await Assert.That(Guid.TryParse(id, out _)).IsTrue();
        }
        finally
        {
            Directory.Delete(directory, recursive: true);
        }
    }

    [Test]
    public async Task GetOrCreateInstallId_ReturnsSameId_OnSubsequentCalls()
    {
        var directory = CreateTemporaryDirectory();
        try
        {
            var stateStore = new StateStore(new DgtpHome(directory));
            var id1 = TelemetryConfig.GetOrCreateInstallId(stateStore);
            var id2 = TelemetryConfig.GetOrCreateInstallId(stateStore);

            await Assert.That(id1).IsEqualTo(id2);
        }
        finally
        {
            Directory.Delete(directory, recursive: true);
        }
    }

    private static string CreateTemporaryDirectory()
    {
        var directory = Path.Combine(Path.GetTempPath(), $"dgtp-{Guid.NewGuid():N}");
        Directory.CreateDirectory(directory);
        return directory;
    }

    private static void ClearOptOutVariables()
    {
        Environment.SetEnvironmentVariable("DGTP_TELEMETRY_OPTOUT", null);
        Environment.SetEnvironmentVariable("DO_NOT_TRACK", null);
        Environment.SetEnvironmentVariable("DGT_TELEMETRY_OPTOUT", null);
    }

    private static Dictionary<string, string?> SaveOptOutVariables() =>
        new(StringComparer.Ordinal)
        {
            ["DGTP_TELEMETRY_OPTOUT"] = Environment.GetEnvironmentVariable("DGTP_TELEMETRY_OPTOUT"),
            ["DO_NOT_TRACK"] = Environment.GetEnvironmentVariable("DO_NOT_TRACK"),
            ["DGT_TELEMETRY_OPTOUT"] = Environment.GetEnvironmentVariable("DGT_TELEMETRY_OPTOUT")
        };

    private static void RestoreOptOutVariables(IReadOnlyDictionary<string, string?> values)
    {
        foreach (var (name, value) in values)
        {
            Environment.SetEnvironmentVariable(name, value);
        }
    }
}
