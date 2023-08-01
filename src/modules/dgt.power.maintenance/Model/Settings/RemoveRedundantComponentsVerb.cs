// Copyright (c) DIGITALL Nature. All rights reserved
// DIGITALL Nature licenses this file to you under the Microsoft Public License.

using System.ComponentModel;
using dgt.power.common;
using Spectre.Console.Cli;

namespace dgt.power.maintenance.Model.Settings;

public class RemoveRedundantComponentsVerb: BaseProgramSettings
{
    [CommandArgument(1, "<SourceSolution>")]
    [Description("The unique name of the solution where the elements are read out")]
    public string SourceSolution { get; set; }

    [CommandArgument(2, "<TargetSolution>")]
    [Description("The unique name of the solution where elements that are also contained in the source solution are removed")]
    public string TargetSolution { get; set; }

    [CommandOption("--dryrun")]
    [Description("Report only what would be removed")]
    [DefaultValue(false)]
    public bool DryRun { get; set; }

    [CommandOption("--includeEntities")]
    [Description("Also remove Entities (experimental)!")]
    [DefaultValue(false)]
    public bool Entities { get; set; }
}
