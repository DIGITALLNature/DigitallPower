// Copyright (c) DIGITALL Nature. All rights reserved
// DIGITALL Nature licenses this file to you under the Microsoft Public License.

using System.ComponentModel;
using Spectre.Console.Cli;

namespace dgt.power.common;

public abstract class BaseProgramSettings : CommandSettings
{
    [CommandOption("--no-telemetry")]
    [Description("Disable telemetry for this invocation")]
    public bool NoTelemetry { get; init; }

    [CommandOption("--non-interactive")]
    [Description(
        "Disable interactive prompts (e.g. MSAL browser login). " +
        "When set, commands that require interactive authentication will exit with code 2 (AuthRequired) " +
        "instead of opening a browser. " +
        "Alternatively set the environment variable DGTP_NON_INTERACTIVE=true. " +
        "Recommended for coding agents and CI pipelines.")]
    public bool NonInteractive { get; init; }
}
