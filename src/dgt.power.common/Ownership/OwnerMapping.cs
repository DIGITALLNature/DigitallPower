// Copyright (c) DIGITALL Nature. All rights reserved

namespace dgt.power.common.Ownership;

public sealed class OwnerMapping(
    int schemaVersion,
    IReadOnlyDictionary<string, OwnerMappingEntry> owners,
    string? schema = null)
{
    public int SchemaVersion { get; } = schemaVersion;

    public IReadOnlyDictionary<string, OwnerMappingEntry> Owners { get; } =
        new Dictionary<string, OwnerMappingEntry>(owners, StringComparer.Ordinal);

    public string? Schema { get; } = schema;
}
