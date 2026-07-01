// Copyright (c) DIGITALL Nature. All rights reserved
// DIGITALL Nature licenses this file to you under the Microsoft Public License.

using System.Diagnostics;
using System.Reflection;

namespace dgt.power.Telemetry;

/// <summary>
/// Central ActivitySource for all DIGITALL Power CLI telemetry spans.
/// </summary>
internal static class DgtpActivitySource
{
    public const string Name = "dgt.power";

    public static readonly ActivitySource Instance = new(Name, GetVersion());

    private static string GetVersion()
    {
        var assembly = typeof(DgtpActivitySource).Assembly;
        var informationalVersion = assembly.GetCustomAttribute<AssemblyInformationalVersionAttribute>()?.InformationalVersion;

        if (!string.IsNullOrWhiteSpace(informationalVersion))
        {
            return informationalVersion.Split('+', StringSplitOptions.RemoveEmptyEntries)[0];
        }

        return assembly.GetName().Version?.ToString() ?? "0.0.0";
    }
}
