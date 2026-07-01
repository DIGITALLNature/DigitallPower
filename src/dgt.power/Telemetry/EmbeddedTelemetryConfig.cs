// Copyright (c) DIGITALL Nature. All rights reserved
// DIGITALL Nature licenses this file to you under the Microsoft Public License.

using System.Reflection;

namespace dgt.power.Telemetry;

/// <summary>
/// Provides access to the telemetry connection string embedded at build time via AssemblyMetadata.
/// Returns null when built without DGT_TELEMETRY_CONNECTION_STRING environment variable (e.g. local dev builds).
/// </summary>
internal static class EmbeddedTelemetryConfig
{
    internal static string? ConnectionString { get; } =
        typeof(EmbeddedTelemetryConfig).Assembly
            .GetCustomAttributes<AssemblyMetadataAttribute>()
            .FirstOrDefault(a => a.Key == "TelemetryConnectionString")
            ?.Value is { Length: > 0 } value
            ? value
            : null;
}
