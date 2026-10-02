// Copyright (c) DIGITALL Nature. All rights reserved
// DIGITALL Nature licenses this file to you under the Microsoft Public License.

using dgt.power.webresource.Planning;
using Spectre.Console;

namespace dgt.power.webresource.Commands;

internal static class WebResourcePushConfirmation
{
    public static bool ShouldPrompt(bool confirm, bool nonInteractive, bool isCiAgent) =>
        confirm && !nonInteractive && !isCiAgent;

    public static bool HasChanges(WebResourcePushPlan plan) =>
        plan.Resources.Any(resource => resource.Action != WebResourceAction.Unchanged || resource.AddToSolution) ||
        plan.Obsolete.Count > 0;

    public static bool Confirm(IAnsiConsole console, string targetName) =>
        console.Confirm($"Proceed with deployment of '{Markup.Escape(targetName)}'?", defaultValue: false);
}
