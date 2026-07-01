// Copyright (c) DIGITALL Nature. All rights reserved
// DIGITALL Nature licenses this file to you under the Microsoft Public License.

namespace dgt.power.common.Exceptions;

/// <summary>
/// Thrown when MSAL requires interactive user login but the tool is running
/// in non-interactive mode (--non-interactive flag or DGTP_NON_INTERACTIVE env var).
/// Exit code: <see cref="dgt.power.common.Commands.ExitCode.AuthRequired"/> (2).
/// </summary>
[Serializable]
public sealed class InteractiveLoginRequiredException : AbstractPowerException
{
    public InteractiveLoginRequiredException()
        : base(DefaultMessage)
    {
    }

    public InteractiveLoginRequiredException(string environment)
        : base(EnvironmentMessage(environment))
    {
    }

    public InteractiveLoginRequiredException(string environment, Exception innerException)
        : base(EnvironmentMessage(environment), innerException)
    {
    }

    public const string DefaultMessage =
        "AUTH_REQUIRED: Interactive login is required but the tool is running in non-interactive mode. " +
        "Ask the user to re-authenticate by running: dgtp profile create <name> <url> --msal";

    public static string EnvironmentMessage(string environment) =>
        $"AUTH_REQUIRED: Interactive login is required for '{environment}' but the tool is running in " +
        "non-interactive mode. Ask the user to re-authenticate by running: dgtp profile create <name> <url> --msal";
}

