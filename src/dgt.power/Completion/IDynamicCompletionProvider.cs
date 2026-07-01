// Copyright (c) DIGITALL Nature. All rights reserved
// DIGITALL Nature licenses this file to you under the Microsoft Public License.

namespace dgt.power.Completion;

/// <summary>
/// Provides dynamic completions for positional arguments of specific commands.
/// Implementations read live data (e.g., profile names from IsolatedStorage)
/// and are only invoked on leaf commands that have no matching subcommand completions.
/// </summary>
internal interface IDynamicCompletionProvider
{
    /// <summary>
    /// Returns completion candidates for the positional argument of the command at
    /// <paramref name="commandPath"/>, filtered by <paramref name="prefix"/>.
    /// </summary>
    /// <returns>Matching candidates, or <see langword="null"/> if this provider does not handle the path.</returns>
    IReadOnlyList<string>? GetCompletions(IReadOnlyList<string> commandPath, string prefix);
}
