// Copyright (c) DIGITALL Nature. All rights reserved
// DIGITALL Nature licenses this file to you under the Microsoft Public License.

using System.Globalization;
using Spectre.Console;

namespace dgt.power.webresource.Output;

public sealed class WebResourceExecutionReporter(IAnsiConsole console)
{
    public Task RunAsync(string operation, string resourceName, Func<Task> action)
    {
        return console.Status()
            .Spinner(Spinner.Known.Dots)
            .SpinnerStyle(Style.Parse("green bold"))
            .StartAsync(
                $"{Markup.Escape(operation)} [green]{Markup.Escape(resourceName)}[/]",
                _ => action());
    }

    public void ReportCompleted(string operation, string resourceName)
    {
        console.MarkupLine(
            CultureInfo.InvariantCulture,
            "[green]✔[/] {0} WebResource: [green]{1}[/]",
            operation,
            resourceName);
    }

    public void ReportAddedToSolution(string resourceName, string solutionName)
    {
        console.MarkupLine(
            CultureInfo.InvariantCulture,
            "[green]✔[/] Added WebResource [green]{0}[/] to solution [green]{1}[/]",
            resourceName,
            solutionName);
    }

    public void ReportPublished(int count, TimeSpan elapsed)
    {
        console.MarkupLine(
            CultureInfo.InvariantCulture,
            "[green]✔[/] Published [green]{0}[/] WebResource(s) in [green]{1:F2}s[/]",
            count,
            elapsed.TotalSeconds);
    }

    public void ReportPublishTotal(int count, TimeSpan elapsed)
    {
        console.MarkupLine(
            CultureInfo.InvariantCulture,
            "[green]✔[/] Total publish time for [green]{0}[/] WebResource(s): [green]{1:F2}s[/]",
            count,
            elapsed.TotalSeconds);
    }
}
