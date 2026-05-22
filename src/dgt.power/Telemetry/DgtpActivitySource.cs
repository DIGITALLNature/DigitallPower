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
        return Assembly.GetExecutingAssembly().GetName().Version?.ToString() ?? "0.0.0";
    }
}
