// Copyright (c) DIGITALL Nature. All rights reserved
// DIGITALL Nature licenses this file to you under the Microsoft Public License.

using System.Diagnostics;
using dgt.power.common.Storage;

namespace dgt.power.cli.tests;

public class TelemetryStartupTests
{
    [Test]
    [Arguments("--help")]
    [Arguments("--version")]
    public async Task InformationalInvocation_DoesNotInitializeState(string option)
    {
        var directory = Directory.CreateTempSubdirectory("dgtp-startup-");
        try
        {
            var output = await RunCliAsync(directory.FullName, null, option);
            await Assert.That(output.Contains("Telemetry Notice", StringComparison.Ordinal)).IsFalse();
            await Assert.That(File.Exists(Path.Combine(directory.FullName, "state.json"))).IsFalse();
        }
        finally
        {
            directory.Delete(true);
        }
    }

    [Test]
    [Arguments(null)]
    [Arguments("DGTP_TELEMETRY_OPTOUT")]
    [Arguments("DO_NOT_TRACK")]
    public async Task OptOutInvocation_DoesNotInitializeState(string? environmentOptOut)
    {
        var directory = Directory.CreateTempSubdirectory("dgtp-startup-");
        try
        {
            string[] args = environmentOptOut is null
                ? ["connection", "list", "--no-telemetry"]
                : ["connection", "list"];
            var output = await RunCliAsync(directory.FullName, environmentOptOut, args);
            await Assert.That(output.Contains("Telemetry Notice", StringComparison.Ordinal)).IsFalse();
            await Assert.That(File.Exists(Path.Combine(directory.FullName, "state.json"))).IsFalse();
        }
        finally
        {
            directory.Delete(true);
        }
    }

    [Test]
    public async Task EnabledInvocation_ShowsNoticeOnlyWhenCreatingIdAndRetainsIdAcrossPreferenceChanges()
    {
        var directory = Directory.CreateTempSubdirectory("dgtp-startup-");
        try
        {
            var firstOutput = await RunCliAsync(directory.FullName, null, "connection", "list");
            await Assert.That(firstOutput.Contains("Telemetry Notice", StringComparison.Ordinal)).IsTrue();
            var store = new StateStore(new DgtpHome(directory.FullName));
            var id = store.GetOrCreateTelemetryInstallId(out var created);
            await Assert.That(created).IsFalse();
            var secondOutput = await RunCliAsync(directory.FullName, null, "connection", "list");
            await Assert.That(secondOutput.Contains("Telemetry Notice", StringComparison.Ordinal)).IsFalse();
            store.SetTelemetryEnabled(false);
            var disabledOutput = await RunCliAsync(directory.FullName, null, "connection", "list");
            await Assert.That(disabledOutput.Contains("Telemetry Notice", StringComparison.Ordinal)).IsFalse();
            store.SetTelemetryEnabled(true);
            var reenabledOutput = await RunCliAsync(directory.FullName, null, "connection", "list");
            await Assert.That(reenabledOutput.Contains("Telemetry Notice", StringComparison.Ordinal)).IsFalse();
            await Assert.That(store.GetOrCreateTelemetryInstallId()).IsEqualTo(id);
        }
        finally
        {
            directory.Delete(true);
        }
    }

    [Test]
    public async Task SavedOptOut_DelaysIdAndNoticeUntilTelemetryIsEnabled()
    {
        var directory = Directory.CreateTempSubdirectory("dgtp-startup-");
        try
        {
            var store = new StateStore(new DgtpHome(directory.FullName));
            store.SetTelemetryEnabled(false);
            var originalState = await File.ReadAllTextAsync(Path.Combine(directory.FullName, "state.json"));
            var disabledOutput = await RunCliAsync(directory.FullName, null, "connection", "list");
            await Assert.That(disabledOutput.Contains("Telemetry Notice", StringComparison.Ordinal)).IsFalse();
            await Assert.That(await File.ReadAllTextAsync(Path.Combine(directory.FullName, "state.json"))).IsEqualTo(originalState);
            store.SetTelemetryEnabled(true);
            var enabledOutput = await RunCliAsync(directory.FullName, null, "connection", "list");
            await Assert.That(enabledOutput.Contains("Telemetry Notice", StringComparison.Ordinal)).IsTrue();
        }
        finally
        {
            directory.Delete(true);
        }
    }

    private static async Task<string> RunCliAsync(string home, string? environmentOptOut, params string[] args)
    {
        var testAssembly = typeof(TelemetryStartupTests).Assembly.Location;
        var startInfo = new ProcessStartInfo("dotnet")
        {
            RedirectStandardOutput = true,
            RedirectStandardError = true,
            UseShellExecute = false
        };
        startInfo.ArgumentList.Add("exec");
        startInfo.ArgumentList.Add("--runtimeconfig");
        startInfo.ArgumentList.Add(Path.ChangeExtension(testAssembly, ".runtimeconfig.json"));
        startInfo.ArgumentList.Add("--depsfile");
        startInfo.ArgumentList.Add(Path.ChangeExtension(testAssembly, ".deps.json"));
        startInfo.ArgumentList.Add(typeof(CommandTree).Assembly.Location);
        foreach (var arg in args)
        {
            startInfo.ArgumentList.Add(arg);
        }

        startInfo.Environment["DGTP_HOME"] = home;
        startInfo.Environment["CI"] = "true";
        startInfo.Environment["DGTP_TELEMETRY_CONNECTION_STRING"] = "";
        startInfo.Environment.Remove("DGTP_TELEMETRY_OPTOUT");
        startInfo.Environment.Remove("DO_NOT_TRACK");
        if (environmentOptOut is not null)
        {
            startInfo.Environment[environmentOptOut] = "true";
        }

        using var process = Process.Start(startInfo)
            ?? throw new InvalidOperationException("Could not start the CLI.");
        var outputTask = process.StandardOutput.ReadToEndAsync();
        var errorTask = process.StandardError.ReadToEndAsync();
        await process.WaitForExitAsync();
        var output = await outputTask;
        var errors = await errorTask;
        await Assert.That(process.ExitCode).IsEqualTo(0).Because(output + errors);
        return output;
    }
}
