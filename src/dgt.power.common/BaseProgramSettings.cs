// Copyright (c) DIGITALL Nature. All rights reserved
// DIGITALL Nature licenses this file to you under the Microsoft Public License.

using System.ComponentModel;
using Spectre.Console.Cli;

namespace dgt.power.common;

public abstract class BaseProgramSettings : CommandSettings
{
    [CommandOption("--no-telemetry")]
    [Description("Disable telemetry for this invocation")]
    public bool NoTelemetry { get; set; }
}
