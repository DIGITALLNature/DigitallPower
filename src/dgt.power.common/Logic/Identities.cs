// Copyright (c) DIGITALL Nature. All rights reserved
// DIGITALL Nature licenses this file to you under the Microsoft Public License.

using System.Text.Json.Serialization;

namespace dgt.power.common.Logic;

public class Identities : IIdentities
{
    [JsonPropertyName("Storage")] public Dictionary<string, Identity> IdentityStore { get; set; } = new();

    [JsonPropertyName("Current")] public string Current { get; set; } = string.Empty;

    [JsonIgnore] public Identity? CurrentIdentity => IdentityStore.TryGetValue(Current, out var identity) ? identity : null;

    [JsonIgnore] public bool IsEmpty => IdentityStore.Count == 0;

    [JsonIgnore] public string CurrentConnectionString => IdentityStore[Current].ConnectionString;

    [JsonIgnore] public string SecurityProtocol => IdentityStore[Current].SecurityProtocol;

    [JsonIgnore] public bool Insecure => IdentityStore[Current].Insecure;


    public void Upsert(string key, Identity identity)
    {
        IdentityStore[key] = identity;
        Current = key;
    }

    public void Remove(string key)
    {
        IdentityStore.Remove(key);

        if (Current == key)
        {
            Current = string.Empty;
        }
    }

    [JsonIgnore] public IEnumerable<string> Keys => IdentityStore.Keys;

    public void SetCurrent(string key)
    {
        if (!IdentityStore.ContainsKey(key))
        {
            throw new ArgumentOutOfRangeException(nameof(key));
        }

        Current = key;
    }
}
