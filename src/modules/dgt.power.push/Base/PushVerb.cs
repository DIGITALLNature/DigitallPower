// Copyright (c) DIGITALL Nature. All rights reserved
// DIGITALL Nature licenses this file to you under the Microsoft Public License.

using System.ComponentModel;
using dgt.power.common;
using Spectre.Console.Cli;

// ReSharper disable ClassNeverInstantiated.Global
// ReSharper disable UnusedAutoPropertyAccessor.Global
#pragma warning disable CS8618


namespace dgt.power.push.Base;

public class PushVerb : BaseProgramSettings
{
    [CommandArgument(1, "<FileOrFolder>")]
    [Description("Full path to the dll file, e.g. C:\\temp\\plugin.dll or to folder for webressources")]
    public required string Target { get; set; }

    [CommandOption("--solution")]
    [Description("Add Objects to Solution; default: none; mandatory for webressources")]
    public string Solution { get; set; }

    [CommandOption("--delete-obsolete")]
    [Description("Delete obsolete unmanaged webressources in solution; dangerous!")]
    public bool DeleteObsolete { get; set; }

    [CommandOption("--publish")]
    [Description("Publish changed objects")]
    public bool Publish { get; set; }
}
