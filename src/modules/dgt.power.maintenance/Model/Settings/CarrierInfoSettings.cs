// Copyright (c) DIGITALL Nature. All rights reserved
// DIGITALL Nature licenses this file to you under the Microsoft Public License.

using System.ComponentModel;
using dgt.power.maintenance.Base;
using Spectre.Console.Cli;

namespace dgt.power.maintenance.Model.Settings;

public class CarrierInfoSettings : MaintenanceVerb
{
    [CommandOption("--filedir")]
    [Description("Full path to the file dir, e.g.C:\\temp")]
    [DefaultValue(".")]
    public string FileDir { get; init; } = ".";

    [CommandOption("--filename")]
    [Description("Exportfile name, default is carrier.json")]
    [DefaultValue("carrier.json")]
    public string FileName { get; init; } = "carrier.json";
}
