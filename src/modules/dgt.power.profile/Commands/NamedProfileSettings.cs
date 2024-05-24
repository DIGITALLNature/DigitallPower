// Copyright (c) DIGITALL Nature. All rights reserved
// DIGITALL Nature licenses this file to you under the Microsoft Public License.

using System.ComponentModel;
using dgt.power.profile.Base;
using Spectre.Console.Cli;

// ReSharper disable ClassNeverInstantiated.Global
// ReSharper disable UnusedAutoPropertyAccessor.Global
#pragma warning disable CS8618

namespace dgt.power.profile.Commands;

public class NamedProfileSettings : ProfileSettings
{
    [CommandArgument(0, "<Name>")]
    [Description("Name")]
    public string Name { get; init; }
}
