// Copyright (c) DIGITALL Nature. All rights reserved
// DIGITALL Nature licenses this file to you under the Microsoft Public License.

using System.Globalization;
using System.Text.RegularExpressions;

namespace dgt.power.Completion;

/// <summary>
/// Represents a parsed dotnet-suggest <c>[suggest:N]</c> directive.
/// dotnet-suggest invokes the CLI app with <c>[suggest:&lt;cursor-position&gt;] "&lt;command-line&gt;"</c>
/// as arguments when the user requests tab completion.
/// </summary>
internal readonly record struct SuggestDirective(int Position)
{
    private static readonly Regex s_pattern = new(@"^\[suggest:(\d+)\]$", RegexOptions.Compiled);

    public static bool IsMatch(string arg) => s_pattern.IsMatch(arg);

    public static bool TryParse(string arg, out SuggestDirective result)
    {
        var match = s_pattern.Match(arg);
        if (match.Success)
        {
            result = new SuggestDirective(int.Parse(match.Groups[1].Value, CultureInfo.InvariantCulture));
            return true;
        }

        result = default;
        return false;
    }
}
