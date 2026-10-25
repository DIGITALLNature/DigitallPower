// Copyright (c) DIGITALL Nature. All rights reserved
// DIGITALL Nature licenses this file to you under the Microsoft Public License.

using dgt.power.common.Storage;
using dgt.power.common.Telemetry;

namespace dgt.power.cli.tests;

[NotInParallel("TelemetryEnvironmentVariables")]
public class TelemetryStateProviderTests
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
        ClearOptOutVariables();
        try
        {
            if (optOutVariable is not null)
            {
                Environment.SetEnvironmentVariable(optOutVariable, "true");
            }

            await WithProvider(async (provider, store) =>
            {
                store.SetTelemetryEnabled(savedEnabled);
                await Assert.That(provider.IsEnabled).IsEqualTo(expected);
            });
        }
        finally
        {
            RestoreOptOutVariables(original);
        }
    }

    [Test]
    public async Task HasOptOutEnvVar_ReturnsFalse_WhenEnvVarNotSet()
    {
        var original = SaveOptOutVariables();
        ClearOptOutVariables();

        try
        {
            await Assert.That(TelemetryStateProvider.HasOptOutEnvVar).IsFalse();
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
    public async Task HasOptOutEnvVar_ReturnsTrue_WhenEnvVarSet(string value)
    {
        var original = SaveOptOutVariables();
        ClearOptOutVariables();
        Environment.SetEnvironmentVariable("DGTP_TELEMETRY_OPTOUT", value);
        try
        {
            await Assert.That(TelemetryStateProvider.HasOptOutEnvVar).IsTrue();
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
    public async Task HasOptOutEnvVar_ReturnsFalse_WhenEnvVarSetToNonOptOutValue(string value)
    {
        var original = SaveOptOutVariables();
        ClearOptOutVariables();
        Environment.SetEnvironmentVariable("DGTP_TELEMETRY_OPTOUT", value);
        try
        {
            await Assert.That(TelemetryStateProvider.HasOptOutEnvVar).IsFalse();
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
    public async Task HasOptOutEnvVar_ReturnsTrue_WhenDoNotTrackIsSet(string value)
    {
        var original = SaveOptOutVariables();
        ClearOptOutVariables();
        Environment.SetEnvironmentVariable("DO_NOT_TRACK", value);
        try
        {
            await Assert.That(TelemetryStateProvider.HasOptOutEnvVar).IsTrue();
        }
        finally
        {
            RestoreOptOutVariables(original);
        }
    }

    [Test]
    public async Task HasOptOutEnvVar_ReturnsFalse_WhenLegacyVariableIsSet()
    {
        var original = SaveOptOutVariables();
        ClearOptOutVariables();
        Environment.SetEnvironmentVariable("DGT_TELEMETRY_OPTOUT", "true");
        try
        {
            await Assert.That(TelemetryStateProvider.HasOptOutEnvVar).IsFalse();
        }
        finally
        {
            RestoreOptOutVariables(original);
        }
    }

    [Test]
    public async Task SetEnabled_PersistsPreference()
    {
        await WithProvider(async (provider, store) =>
        {
            provider.SetEnabled(false);
            await Assert.That(store.TelemetryEnabled).IsFalse();

            provider.SetEnabled(true);
            await Assert.That(store.TelemetryEnabled).IsTrue();
        });
    }

    private static async Task WithProvider(Func<TelemetryStateProvider, StateStore, Task> action)
    {
        var directory = CreateTemporaryDirectory();
        try
        {
            var store = new StateStore(new DgtpHome(directory));
            var provider = new TelemetryStateProvider(store);
            await action(provider, store);
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
