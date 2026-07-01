// Copyright (c) DIGITALL Nature. All rights reserved
// DIGITALL Nature licenses this file to you under the Microsoft Public License.

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
