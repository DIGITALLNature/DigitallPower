// Copyright (c) DIGITALL Nature. All rights reserved
// DIGITALL Nature licenses this file to you under the Microsoft Public License.

using System.ComponentModel;
using dgt.power.common;
using Spectre.Console.Cli;

namespace dgt.power.linter.Base;

public class LintVerb : BaseProgramSettings
{
    [CommandOption("--solutions")]
    [Description("Comma-separated list of solution unique names to lint")]
    public string Solutions { get; init; } = string.Empty;

    [CommandOption("-c|--config")]
    [Description("Full path to the linter configuration file")]
    public string Config { get; init; } = "lint.config.json";

    [CommandOption("--rules")]
    [Description("Comma-separated list of rule ids to run")]
    public string Rules { get; init; } = string.Empty;

    [CommandOption("--report")]
    [Description("Write the JSON findings report to this file path")]
    public string Report { get; init; } = string.Empty;

    [CommandOption("--fail-on")]
    [Description("Minimum severity that fails the command (None|Info|Warning|Error)")]
    public string FailOn { get; init; } = "Error";
}
