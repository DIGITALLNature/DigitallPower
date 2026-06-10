// Copyright (c) DIGITALL Nature. All rights reserved
// DIGITALL Nature licenses this file to you under the Microsoft Public License.

using System.IO.IsolatedStorage;
using dgt.power.common;

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
    public static bool IsOptedOut =>
        ExecutionEnvironment.IsTruthy(Environment.GetEnvironmentVariable(OptOutEnvVar));

    /// <summary>
    /// True when running on a known CI/CD build agent.
    /// </summary>
    public static bool IsCi => ExecutionEnvironment.IsCiAgent;

    /// <summary>
    /// Retrieves or creates a persistent anonymous install ID from isolated storage.
    /// </summary>
    public static string GetOrCreateInstallId(IsolatedStorageFile store)
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
