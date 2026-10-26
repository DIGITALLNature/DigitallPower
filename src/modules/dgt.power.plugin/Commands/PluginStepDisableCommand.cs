// SPDX-FileCopyrightText: DIGITALL Nature
// SPDX-License-Identifier: MS-RL

using dgt.power.common;
using Spectre.Console;

namespace dgt.power.plugin.Commands;

// ReSharper disable once ClassNeverInstantiated.Global
public class PluginStepDisableCommand(
    ICommandSpanFactory spans,
    IDataverseConnection connection,
    IAnsiConsole console)
    : PluginStepEnableDisableCommandBase(spans, connection, console)
{
    protected override bool Enable => false;
}
