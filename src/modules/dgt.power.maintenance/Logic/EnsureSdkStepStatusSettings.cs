// Copyright (c) DIGITALL Nature. All rights reserved
// DIGITALL Nature licenses this file to you under the Microsoft Public License.

using System.ComponentModel;
using dgt.power.common;
using Spectre.Console.Cli;

namespace dgt.power.maintenance.Logic;
// ReSharper disable UnusedAutoPropertyAccessor.Global

public class EnsureSdkStepStatusSettings : BaseProgramSettings
{
    [CommandOption("-s|--solution")]
    [Description("unique name of the solution to work on")]
    [DefaultValue("assemblies")]
    public string? Solution { get; set; }

    [CommandOption("-d|--disabled")]
    [Description("true if steps should be disabled, false otherwise")]
    [DefaultValue(false)]
    public bool Disabled { get; set; }

    [CommandOption("--dry-run")]
    [Description("do not perform any updates")]
    [DefaultValue(false)]
    public bool DryRun { get; set; }
}
