// Copyright (c) DIGITALL Nature. All rights reserved
// DIGITALL Nature licenses this file to you under the Microsoft Public License.

using dgt.power.common.Connections;
using dgt.power.common.Storage;

namespace dgt.power.Completion;

internal sealed class ConnectionNamesProvider : IDynamicCompletionProvider
{
    private static readonly HashSet<string> s_connectionNameCommands =
        new(StringComparer.OrdinalIgnoreCase) { "select", "delete" };

    public IReadOnlyList<string>? GetCompletions(IReadOnlyList<string> commandPath, string prefix)
    {
        if (commandPath.Count != 2
            || !string.Equals(commandPath[0], "connection", StringComparison.OrdinalIgnoreCase)
            || !s_connectionNameCommands.Contains(commandPath[1]))
        {
            return null;
        }

        return LoadConnectionNames(prefix);
    }

    private static List<string> LoadConnectionNames(string prefix)
    {
        try
        {
            var store = new ConnectionStore(new DgtpHome());
            return store.GetAll().Keys
                .Where(name => string.IsNullOrEmpty(prefix) || name.StartsWith(prefix, StringComparison.OrdinalIgnoreCase))
                .ToList();
        }
#pragma warning disable CA1031 // Best-effort: a failure here means no dynamic completions.
        catch (Exception)
#pragma warning restore CA1031
        {
            return [];
        }
    }
}
