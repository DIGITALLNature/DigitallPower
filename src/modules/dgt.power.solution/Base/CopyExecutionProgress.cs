// Copyright (c) DIGITALL Nature. All rights reserved
// DIGITALL Nature licenses this file to you under the Microsoft Public License.

using Spectre.Console;

namespace dgt.power.solution.Base;

internal sealed class CopyExecutionProgress
{
    public int CopiedComponents { get; set; }
    public int RemovedComponents { get; set; }
    public List<string> ProtectedFields { get; } = [];
    public string? PendingEffect { get; set; }
    public bool CopyCompleted { get; set; }

    public void ReportFailure(IAnsiConsole console, bool cancelled)
    {
        console.MarkupLine($"[red]{(cancelled ? "Cancelled" : "Failed")}: " +
            $"{CopiedComponents} confirmed copied component(s), {RemovedComponents} confirmed app-cleanup removal(s), " +
            $"{ProtectedFields.Count} confirmed protected column(s). " +
            $"Copy/app cleanup {(CopyCompleted ? "completed" : "did not complete")}. No rollback was attempted.[/]");
        foreach (var field in ProtectedFields)
        {
            console.MarkupLine($"[green]Confirmed protected: {Markup.Escape(field)}[/]");
        }
        if (PendingEffect is not null)
        {
            console.MarkupLine($"[yellow]{Markup.Escape(PendingEffect)}. In-flight server effects may be uncertain.[/]");
        }
    }
}
