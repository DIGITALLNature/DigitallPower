// Copyright (c) DIGITALL Nature. All rights reserved
// DIGITALL Nature licenses this file to you under the Microsoft Public License.

using System.Text.Json;

namespace dgt.power.solution.Base;

public class LintRuleConfigEntry
{
    private static readonly JsonSerializerOptions s_optionsSerializerSettings = new(JsonSerializerDefaults.Web);

    public bool Enabled { get; init; } = true;

    public LintSeverity? Severity { get; init; }

    public JsonElement Options { get; init; } = JsonDocument.Parse("{}").RootElement.Clone();

    /// <summary>Deserializes <see cref="Options"/> into a rule's own strongly-typed options shape.</summary>
    public T ReadOptions<T>() where T : class, new() =>
        Options.ValueKind == JsonValueKind.Object
            ? Options.Deserialize<T>(s_optionsSerializerSettings) ?? new T()
            : new T();
}
