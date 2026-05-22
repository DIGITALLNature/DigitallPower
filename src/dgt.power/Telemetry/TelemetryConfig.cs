// Copyright (c) DIGITALL Nature. All rights reserved
// DIGITALL Nature licenses this file to you under the Microsoft Public License.

namespace dgt.power.Telemetry;

/// <summary>
/// Central telemetry configuration. Determines whether telemetry is active
/// and provides helper methods for CI detection and anonymous install IDs.
/// </summary>
internal static class TelemetryConfig
{
    private const string OptOutEnvVar = "DGT_TELEMETRY_OPTOUT";
    private const string InstallIdFileName = "telemetry-install-id";

    /// <summary>
    /// True when the user has opted out of telemetry via environment variable.
    /// </summary>
    public static bool IsOptedOut
    {
        get
        {
            var value = Environment.GetEnvironmentVariable(OptOutEnvVar);
            return value is "1" or "true" or "yes";
        }
    }

    /// <summary>
    /// True when running on a known CI/CD build agent.
    /// </summary>
    public static bool IsCi =>
        // Azure DevOps
        Environment.GetEnvironmentVariable("TF_BUILD") != null ||
        Environment.GetEnvironmentVariable("BUILD_BUILDURI") != null ||
        // GitHub Actions
        Environment.GetEnvironmentVariable("GITHUB_ACTIONS") != null ||
        // GitLab CI
        Environment.GetEnvironmentVariable("GITLAB_CI") != null ||
        // Jenkins
        Environment.GetEnvironmentVariable("JENKINS_URL") != null ||
        // Generic CI marker
        Environment.GetEnvironmentVariable("CI") != null;

    /// <summary>
    /// Retrieves or creates a persistent anonymous install ID from isolated storage.
    /// </summary>
    public static string GetOrCreateInstallId(System.IO.IsolatedStorage.IsolatedStorageFile store)
    {
        if (store.FileExists(InstallIdFileName))
        {
            using var stream = store.OpenFile(InstallIdFileName, FileMode.Open, FileAccess.Read);
            using var reader = new StreamReader(stream);
            var id = reader.ReadToEnd().Trim();
            if (Guid.TryParse(id, out _))
            {
                return id;
            }
        }

        var newId = Guid.NewGuid().ToString("D");
        using var writeStream = store.OpenFile(InstallIdFileName, FileMode.Create, FileAccess.Write);
        using var writer = new StreamWriter(writeStream);
        writer.Write(newId);
        return newId;
    }
}
