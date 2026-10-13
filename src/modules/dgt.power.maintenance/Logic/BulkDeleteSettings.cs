// Copyright (c) DIGITALL Nature. All rights reserved
// DIGITALL Nature licenses this file to you under the Microsoft Public License.

using System.ComponentModel;
using Spectre.Console;
using Spectre.Console.Cli;
using dgt.power.maintenance.Base;

namespace dgt.power.maintenance.Logic;

public class BulkDeleteSettings : MaintenanceVerb
{
    [CommandOption("--poll-interval")]
    [Description("Interval in seconds between bulk-delete job status checks")]
    [DefaultValue(5)]
    public int PollInterval { get; init; } = 5;

    public override ValidationResult Validate() =>
        PollInterval > 0
            ? ValidationResult.Success()
            : ValidationResult.Error("--poll-interval must be a positive number of seconds.");
}
