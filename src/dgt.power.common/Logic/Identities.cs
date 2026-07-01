// Copyright (c) DIGITALL Nature. All rights reserved
// DIGITALL Nature licenses this file to you under the Microsoft Public License.

using System.Text.Json.Serialization;

namespace dgt.power.common.Logic;

public class Identities : IIdentities
{
    [JsonPropertyName("Storage")] public Dictionary<string, Identity> IdentityStore { get; init; } = new();

    [JsonPropertyName("Current")] public string Current { get; set; } = string.Empty;

    [JsonIgnore] public Identity? CurrentIdentity => IdentityStore.TryGetValue(Current, out var identity) ? identity : null;

    [JsonIgnore] public bool IsEmpty => IdentityStore.Count == 0;

    [JsonIgnore] public string CurrentConnectionString => IdentityStore[Current].ConnectionString;


    public void Upsert(string key, Identity identity)
    {
        ArgumentNullException.ThrowIfNull(key);
        var normalizedKey = key.ToUpperInvariant();
        IdentityStore[normalizedKey] = identity;
        Current = normalizedKey;
    }

    public void Remove(string key)
    {
        ArgumentNullException.ThrowIfNull(key);
        var normalizedKey = key.ToUpperInvariant();
        IdentityStore.Remove(normalizedKey);

        if (Current == normalizedKey)
        {
            Current = string.Empty;
        }
    }

    [JsonIgnore] public IEnumerable<IdentityInfo> Infos => IdentityStore.Select(i => new IdentityInfo(i.Key, i.Value is TokenIdentity ? "Token" : "Classic"));

    public void SetCurrent(string key)
    {
        ArgumentNullException.ThrowIfNull(key);
        var normalizedKey = key.ToUpperInvariant();
        if (!IdentityStore.ContainsKey(normalizedKey))
        {
            throw new ArgumentOutOfRangeException(nameof(key));
        }

        Current = normalizedKey;
    }

    public bool Contains(string key)
    {
        ArgumentNullException.ThrowIfNull(key);
        return IdentityStore.ContainsKey(key.ToUpperInvariant());
    }
}
