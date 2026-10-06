// Copyright (c) DIGITALL Nature. All rights reserved
// DIGITALL Nature licenses this file to you under the Microsoft Public License.

namespace dgt.power.common.Connections;

/// <summary>
/// Holds connection-related options parsed for the current CLI invocation.
/// </summary>
public sealed class ConnectionInvocationContext
{
    private readonly string? _environmentConnectionName = Environment.GetEnvironmentVariable("DGTP_CONNECTION");
    private readonly string? _environmentConnectionString = Environment.GetEnvironmentVariable("DGTP_CONNECTION_STRING");
    private readonly bool _environmentNonInteractive =
        ExecutionEnvironment.IsTruthy(Environment.GetEnvironmentVariable("DGTP_NON_INTERACTIVE"));

    /// <summary>
    /// Initializes the context with environment-provided defaults.
    /// </summary>
    public ConnectionInvocationContext()
    {
        ConnectionName = _environmentConnectionName;
        ConnectionString = _environmentConnectionString;
        NonInteractive = _environmentNonInteractive;
    }

    /// <summary>
    /// Gets the selected connection name, preferring the parsed command-line option over the environment.
    /// </summary>
    public string? ConnectionName { get; private set; }

    /// <summary>
    /// Gets the one-off connection string, preferring the parsed command-line option over the environment.
    /// </summary>
    public string? ConnectionString { get; private set; }

    /// <summary>
    /// Gets whether interactive authentication is disabled for this invocation.
    /// </summary>
    public bool NonInteractive { get; private set; }

    /// <summary>
    /// Gets whether the user explicitly permits unencrypted storage on supported systems.
    /// </summary>
    public bool AllowUnencryptedStorage { get; } =
        ExecutionEnvironment.IsTruthy(Environment.GetEnvironmentVariable("DGTP_ALLOW_UNENCRYPTED_STORAGE"));

    /// <summary>
    /// Captures the already-parsed shared settings for the current command.
    /// </summary>
    /// <param name="settings">Parsed settings for the selected command.</param>
    public void Capture(BaseProgramSettings settings)
    {
        ArgumentNullException.ThrowIfNull(settings);
        ConnectionName = settings.Connection ?? _environmentConnectionName;
        ConnectionString = settings.ConnectionString ?? _environmentConnectionString;
        NonInteractive = settings.NonInteractive || _environmentNonInteractive;
    }
}
