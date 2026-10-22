// Copyright (c) DIGITALL Nature. All rights reserved

namespace dgt.power.common.Ownership;

public sealed class OwnerMapping
{
    public OwnerMapping(int schemaVersion, IReadOnlyDictionary<string, OwnerMappingEntry> owners, string? schema = null)
    {
        SchemaVersion = schemaVersion;
        Owners = new Dictionary<string, OwnerMappingEntry>(owners, StringComparer.Ordinal);
        Schema = schema;
    }

    public int SchemaVersion { get; }

    public IReadOnlyDictionary<string, OwnerMappingEntry> Owners { get; }

    public string? Schema { get; }
}
