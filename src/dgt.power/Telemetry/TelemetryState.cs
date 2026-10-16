// Copyright (c) DIGITALL Nature. All rights reserved
// DIGITALL Nature licenses this file to you under the Microsoft Public License.

namespace dgt.power.Telemetry;

/// <summary>
/// Single source of truth for whether telemetry is collected in this process.
/// </summary>
internal sealed class TelemetryState(bool enabled = false, string? installId = null)
{
    /// <summary>
    /// Set to true by the TelemetryInterceptor when --no-telemetry is passed.
    /// </summary>
    internal static bool SuppressForInvocation { get; set; }

    internal string? InstallId { get; private set; } = installId;

    internal bool IsActive => _enabled && !SuppressForInvocation;

    private bool _enabled = enabled;

    internal void Enable(string id)
    {
        InstallId = id;
        _enabled = true;
    }
}
