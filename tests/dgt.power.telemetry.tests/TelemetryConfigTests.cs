// Copyright (c) DIGITALL Nature. All rights reserved
// DIGITALL Nature licenses this file to you under the Microsoft Public License.

using System.IO.IsolatedStorage;
using dgt.power.Telemetry;

namespace dgt.power.telemetry.tests;

[NotInParallel(nameof(TelemetryConfigTests))]
public class TelemetryConfigTests
{
    [Test]
    public async Task IsOptedOut_ReturnsFalse_WhenEnvVarNotSet()
    {
        // Ensure env var is not set
        Environment.SetEnvironmentVariable("DGT_TELEMETRY_OPTOUT", null);

        await Assert.That(TelemetryConfig.IsOptedOut).IsFalse();
    }

    [Test]
    [Arguments("1")]
    [Arguments("true")]
    [Arguments("yes")]
    public async Task IsOptedOut_ReturnsTrue_WhenEnvVarSet(string value)
    {
        Environment.SetEnvironmentVariable("DGT_TELEMETRY_OPTOUT", value);
        try
        {
            await Assert.That(TelemetryConfig.IsOptedOut).IsTrue();
        }
        finally
        {
            Environment.SetEnvironmentVariable("DGT_TELEMETRY_OPTOUT", null);
        }
    }

    [Test]
    [Arguments("0")]
    [Arguments("false")]
    [Arguments("no")]
    [Arguments("")]
    public async Task IsOptedOut_ReturnsFalse_WhenEnvVarSetToNonOptOutValue(string value)
    {
        Environment.SetEnvironmentVariable("DGT_TELEMETRY_OPTOUT", value);
        try
        {
            await Assert.That(TelemetryConfig.IsOptedOut).IsFalse();
        }
        finally
        {
            Environment.SetEnvironmentVariable("DGT_TELEMETRY_OPTOUT", null);
        }
    }

    [Test]
    public async Task IsCi_ReturnsFalse_WhenNoCiEnvVarsSet()
    {
        // Save and clear all CI env vars
        var savedVars = new Dictionary<string, string?>
        {
            ["TF_BUILD"] = Environment.GetEnvironmentVariable("TF_BUILD"),
            ["BUILD_BUILDURI"] = Environment.GetEnvironmentVariable("BUILD_BUILDURI"),
            ["GITHUB_ACTIONS"] = Environment.GetEnvironmentVariable("GITHUB_ACTIONS"),
            ["GITLAB_CI"] = Environment.GetEnvironmentVariable("GITLAB_CI"),
            ["JENKINS_URL"] = Environment.GetEnvironmentVariable("JENKINS_URL"),
            ["CI"] = Environment.GetEnvironmentVariable("CI")
        };

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
        var ciVars = new[] { "TF_BUILD", "BUILD_BUILDURI", "GITHUB_ACTIONS", "GITLAB_CI", "JENKINS_URL", "CI" };
        var saved = ciVars.ToDictionary(k => k, k => Environment.GetEnvironmentVariable(k));

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
    public async Task GetOrCreateInstallId_CreatesNewGuid_WhenFileDoesNotExist()
    {
        using var store = IsolatedStorageFile.GetUserStoreForApplication();
        const string testFile = "telemetry-install-id";

        // Ensure clean state
        if (store.FileExists(testFile))
        {
            store.DeleteFile(testFile);
        }

        try
        {
            var id = TelemetryConfig.GetOrCreateInstallId(store);

            await Assert.That(Guid.TryParse(id, out _)).IsTrue();
        }
        finally
        {
            if (store.FileExists(testFile))
            {
                store.DeleteFile(testFile);
            }
        }
    }

    [Test]
    public async Task GetOrCreateInstallId_ReturnsSameId_OnSubsequentCalls()
    {
        using var store = IsolatedStorageFile.GetUserStoreForApplication();
        const string testFile = "telemetry-install-id";

        // Ensure clean state
        if (store.FileExists(testFile))
        {
            store.DeleteFile(testFile);
        }

        try
        {
            var id1 = TelemetryConfig.GetOrCreateInstallId(store);
            var id2 = TelemetryConfig.GetOrCreateInstallId(store);

            await Assert.That(id1).IsEqualTo(id2);
        }
        finally
        {
            if (store.FileExists(testFile))
            {
                store.DeleteFile(testFile);
            }
        }
    }
}
