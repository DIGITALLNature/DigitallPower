// Copyright (c) DIGITALL Nature. All rights reserved
// DIGITALL Nature licenses this file to you under the Microsoft Public License.

using System.Collections;
using System.Globalization;
using System.Text.Json;
using CsvHelper;
using dgt.power.common.DTO;

namespace dgt.power.analyzer.Base;

internal static class AnalyzeResultWriter
{
    internal static string ResultFolder { get; } = "Analyze";

    internal static void WriteSummaryFile(string name, AnalyzerSummary summary)
    {
        var content = JsonSerializer.SerializeToUtf8Bytes(summary);

        if (!Directory.Exists(ResultFolder))
        {
            Directory.CreateDirectory(ResultFolder);
        }

        var work = Path.Combine(ResultFolder, $"{Guid.NewGuid():N}-{name}-summary");
        var file = Path.Combine(ResultFolder, $"{name}-summary.json");

        File.WriteAllBytes(work, content);
        if (File.Exists(file))
        {
            File.Replace(work, file, null, false);
        }
        else
        {
            File.Move(work, file);
        }
    }

    internal static void WriteReportFile<T>(string name, T resultTable) where T : IEnumerable
    {
        if (!Directory.Exists(ResultFolder))
        {
            Directory.CreateDirectory(ResultFolder);
        }

        var work = Path.Combine(ResultFolder, $"{Guid.NewGuid():N}-{name}-result");
        var file = Path.Combine(ResultFolder, $"{name}-result.csv");

        using (var writer = new StreamWriter(work))
        using (var csv = new CsvWriter(writer, CultureInfo.InvariantCulture))
        {
            csv.WriteRecords(resultTable);
        }

        if (File.Exists(file))
        {
            File.Replace(work, file, null, false);
        }
        else
        {
            File.Move(work, file);
        }
    }
}
