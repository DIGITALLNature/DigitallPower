// SPDX-FileCopyrightText: DIGITALL Nature
// SPDX-License-Identifier: MS-RL

using dgt.power.common.Telemetry;

namespace dgt.power.Telemetry;

/// <summary>
/// Single source of truth for whether telemetry is collected in this process.
/// </summary>
internal sealed class TelemetryState(bool enabled = false, string? installId = null)
    : ITelemetryNoticeTracker
{
    /// <summary>
    /// Set to true by the TelemetryInterceptor when --no-telemetry is passed.
    /// </summary>
    internal static bool SuppressForInvocation { get; set; }

    internal string? InstallId { get; private set; } = installId;

    internal bool IsActive => _enabled && !SuppressForInvocation;

    public bool IsNoticeShown { get; set; }

    private bool _enabled = enabled;

    internal void Enable(string id)
    {
        InstallId = id;
        _enabled = true;
    }
}
