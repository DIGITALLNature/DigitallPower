// SPDX-FileCopyrightText: DIGITALL Nature
// SPDX-License-Identifier: MS-RL

using System.ComponentModel;
using dgt.power.connection.Base;
using Spectre.Console.Cli;

// ReSharper disable ClassNeverInstantiated.Global
// ReSharper disable UnusedAutoPropertyAccessor.Global
#pragma warning disable CS8618

namespace dgt.power.connection.Commands;

public class NamedConnectionSettings : ConnectionSettings
{
    [CommandArgument(0, "<Name>")]
    [Description("Name")]
    public string Name { get; init; }
}
