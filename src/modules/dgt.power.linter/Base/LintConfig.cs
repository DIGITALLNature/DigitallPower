// Copyright (c) DIGITALL Nature. All rights reserved
// DIGITALL Nature licenses this file to you under the Microsoft Public License.

using System.Text.Json;

namespace dgt.power.linter.Base;

public class LintConfig
{
    public int Version { get; set; } = 1;

    public Dictionary<string, LintRuleConfigEntry> Rules { get; } = new(StringComparer.OrdinalIgnoreCase);
}

public class LintRuleConfigEntry
{
    private static readonly JsonSerializerOptions OptionsSerializerSettings = new(JsonSerializerDefaults.Web);

    public bool Enabled { get; set; } = true;

    public LintSeverity? Severity { get; set; }

    public JsonElement Options { get; set; } = JsonDocument.Parse("{}").RootElement.Clone();

    /// <summary>Deserializes <see cref="Options"/> into a rule's own strongly-typed options shape.</summary>
    public T ReadOptions<T>() where T : class, new() =>
        Options.ValueKind == JsonValueKind.Object
            ? Options.Deserialize<T>(OptionsSerializerSettings) ?? new T()
            : new T();
}

