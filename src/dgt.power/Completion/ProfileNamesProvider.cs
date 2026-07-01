// Copyright (c) DIGITALL Nature. All rights reserved
// DIGITALL Nature licenses this file to you under the Microsoft Public License.

using System.IO.IsolatedStorage;
using dgt.power.common.Logic;

namespace dgt.power.Completion;

/// <summary>
/// Provides dynamic tab completions for profile names stored in IsolatedStorage.
/// Handles <c>profile select &lt;name&gt;</c> and <c>profile delete &lt;name&gt;</c>.
/// </summary>
internal sealed class ProfileNamesProvider : IDynamicCompletionProvider
{
    // Commands under "profile" that accept an existing profile name as their first positional arg.
    private static readonly HashSet<string> s_profileNameCommands =
        new(StringComparer.OrdinalIgnoreCase) { "select", "delete" };

    public IReadOnlyList<string>? GetCompletions(IReadOnlyList<string> commandPath, string prefix)
    {
        if (commandPath.Count != 2
            || !string.Equals(commandPath[0], "profile", StringComparison.OrdinalIgnoreCase)
            || !s_profileNameCommands.Contains(commandPath[1]))
            return null;

        return LoadProfileNames(prefix);
    }

    private static List<string> LoadProfileNames(string prefix)
    {
        try
        {
            using var storage = IsolatedStorageFile.GetUserStoreForAssembly();
            var manager = new ProfileManager(storage);
            var identities = manager.LoadIdentities();

            return identities.Infos
                .Select(i => i.Name)
                .Where(n => string.IsNullOrEmpty(prefix) || n.StartsWith(prefix, StringComparison.OrdinalIgnoreCase))
                .ToList();
        }
#pragma warning disable CA1031 // Best-effort: a failure here just means no dynamic completions.
        catch (Exception)
#pragma warning restore CA1031
        {
            return [];
        }
    }
}
