// Copyright (c) DIGITALL Nature. All rights reserved
// DIGITALL Nature licenses this file to you under the Microsoft Public License.

using System.ComponentModel;
using dgt.power.maintenance.Base;
using Spectre.Console.Cli;

namespace dgt.power.maintenance.Model.Settings;

public class IncrementSolutionVersionSettings : MaintenanceVerb
{
    [CommandArgument(1, "<Solution>")]
    [Description("The unique name of the solution to increment version")]
    public string Solution { get; set; }

    [CommandOption("--major")]
    [Description("Increment the major version eg 1.0.0.0 -> 2.0.0.0")]
    [DefaultValue(false)]
    public bool Major { get; set; }

    [CommandOption("--minor")]
    [Description("Increment the minor version eg 1.0.0.0 -> 1.1.0.0")]
    [DefaultValue(false)]
    public bool Minor { get; set; }

    [CommandOption("--build")]
    [Description("Increment the build version eg 1.0.0.0 -> 1.0.1.0")]
    [DefaultValue(false)]
    public bool Build { get; set; }

    [CommandOption("--revision")]
    [Description("Increment the revision version eg 1.0.0.0 -> 1.0.0.1")]
    [DefaultValue(true)]
    public bool Revision { get; set; } = true;
}
