// SPDX-FileCopyrightText: DIGITALL Nature
// SPDX-License-Identifier: MS-RL

using System.Text.Json.Serialization;

namespace dgt.power.solution.Base;

public class LintConfig
{
    public int Version { get; init; } = 1;

    // Get-only so the OrdinalIgnoreCase comparer always survives - without [JsonObjectCreationHandling(Populate)]
    // System.Text.Json silently skips read-only properties instead of populating the existing instance.
    [JsonObjectCreationHandling(JsonObjectCreationHandling.Populate)]
    public Dictionary<string, LintRuleConfigEntry> Rules { get; } = new(StringComparer.OrdinalIgnoreCase);
}


