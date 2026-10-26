// SPDX-FileCopyrightText: DIGITALL Nature
// SPDX-License-Identifier: MS-RL

using dgt.power.webresource.Execution;
using Spectre.Console;

namespace dgt.power.webresource.Output;

public sealed class WebResourceExecutionReporter(IAnsiConsole console)
{
    public void ReportCompleted(WebResourceDeploymentProgress progress)
    {
        ArgumentNullException.ThrowIfNull(progress);
        console.MarkupLine(
            $"[green]{Emoji.Known.CheckMark}[/] {Markup.Escape(progress.Operation)} " +
            $"{Markup.Escape(progress.Resource)} {Markup.Escape(progress.Name)}");
    }
}
