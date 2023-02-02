﻿using System.ComponentModel;
using dgt.power.common;
using Spectre.Console.Cli;

// ReSharper disable ClassNeverInstantiated.Global
// ReSharper disable UnusedAutoPropertyAccessor.Global
#pragma warning disable CS8618

namespace dgt.power.import.Base;

public class ImportVerb : BaseProgramSettings
{
    [CommandArgument(1, "[Task]")]
    [Description("Possible tasks: userroles, duplicaterules, teamtemplates, bulkdeletes, savedquerys, queues, documenttemplates, calendars, slaconfigs, outlooktemplates")]
    public string Task { get; set; }

    [CommandOption("--filedir")]
    [Description("Full path to the file dir, e.g.C:\\temp")]
    public string FileDir { get; init; } = string.Empty;

    [CommandOption("--filename")]
    [Description("Importfile name, default is <task>.json")]
    public string FileName { get; init; }

    [CommandOption("--inline")]
    [Description("Inline data instead of files, only supported for some single tasks!")]
    public string InlineData { get; init; }

    [CommandOption("--assignee")]
    [Description("Assignee aka Record Owner")]
    public string Assignee { get; init; }
}
