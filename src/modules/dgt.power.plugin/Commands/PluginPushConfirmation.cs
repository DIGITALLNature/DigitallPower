// Copyright (c) DIGITALL Nature. All rights reserved
// DIGITALL Nature licenses this file to you under the Microsoft Public License.

using Spectre.Console;

namespace dgt.power.plugin.Commands;

internal static class PluginPushConfirmation
{
    public static bool ShouldPrompt(bool confirm, bool nonInteractive, bool isCiAgent) =>
        confirm && !nonInteractive && !isCiAgent;

    public static bool Confirm(IAnsiConsole console, string targetName) =>
        console.Confirm($"Proceed with deployment of '{Markup.Escape(targetName)}'?", defaultValue: false);
}
