// Copyright (c) DIGITALL Nature. All rights reserved
// DIGITALL Nature licenses this file to you under the Microsoft Public License.

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
