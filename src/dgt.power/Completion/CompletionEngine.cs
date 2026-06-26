// Copyright (c) DIGITALL Nature. All rights reserved
// DIGITALL Nature licenses this file to you under the Microsoft Public License.

using System.Text;
using Spectre.Console.Cli.Help;

namespace dgt.power.Completion;

/// <summary>
/// Core completion logic: given a Spectre command model and the partial command-line string
/// at a given cursor position, returns the list of applicable completion candidates.
/// </summary>
/// <remarks>
/// Algorithm:
/// <list type="number">
///   <item>Trim the command-line to the cursor position and tokenise it.</item>
///   <item>Skip the application name (first token).</item>
///   <item>Walk the command tree, consuming tokens that match known commands/branches.</item>
///   <item>Use the last (partial) token as a prefix filter for either subcommand names or option names.</item>
/// </list>
/// Options (starting with <c>-</c>) are matched against <see cref="ICommandOption.LongNames"/>.
/// Sub-commands are matched against <see cref="ICommandInfo.Name"/>.
/// Hidden commands and hidden options are excluded from all results.
/// </remarks>
internal static class CompletionEngine
{
    public static IReadOnlyList<string> GetCompletions(
        ICommandModel model,
        string commandLine,
        int position,
        IDynamicCompletionProvider? dynamicProvider = null)
    {
        // Trim the line to the cursor position so we never look beyond where the user is typing.
        var line = position < commandLine.Length ? commandLine[..position] : commandLine;
        var tokens = Tokenize(line);
        var endsWithSpace = line.Length > 0 && char.IsWhiteSpace(line[^1]);

        // Walk the command tree consuming all tokens that are completed commands/branches.
        // When endsWithSpace=false the last token is still being typed, so we stop one token early.
        ICommandContainer container = model;
        var consumeCount = endsWithSpace ? tokens.Count : Math.Max(0, tokens.Count - 1);
        var encounteredUnknownToken = false;
        var commandPath = new List<string>();

        // Skip app-name token (index 0)
        for (var i = 1; i < consumeCount; i++)
        {
            var token = tokens[i];
            if (token.StartsWith('-'))
                break; // option arg encountered — stop navigating deeper

            var child = container.Commands.FirstOrDefault(c =>
                string.Equals(c.Name, token, StringComparison.OrdinalIgnoreCase));

            if (child is null)
            {
                encounteredUnknownToken = true;
                break; // unrecognised token — stop navigating
            }

            container = child;
            commandPath.Add(child.Name);
        }

        // An unrecognised command token means we are in an unknown context: return nothing.
        if (encounteredUnknownToken)
            return [];

        // Determine the prefix: what the user has typed for the current (incomplete) token.
        string prefix;
        if (endsWithSpace || tokens.Count == 0)
            prefix = string.Empty;
        else
            prefix = tokens[^1];

        var results = new List<string>();

        if (prefix.StartsWith('-'))
        {
            // Complete option names for the current container.
            results.AddRange(
                GetOptions(container)
                    .Where(o => o.StartsWith(prefix, StringComparison.OrdinalIgnoreCase)));
        }
        else
        {
            // Complete sub-command/branch names.
            results.AddRange(
                container.Commands
                    .Where(c => !c.IsHidden && !c.IsDefaultCommand
                                && c.Name.StartsWith(prefix, StringComparison.OrdinalIgnoreCase))
                    .Select(c => c.Name));

            // For leaf commands with positional arguments, delegate to the dynamic provider.
            if (results.Count == 0
                && container is ICommandInfo leafCmd
                && leafCmd.Parameters.OfType<ICommandArgument>().Any()
                && dynamicProvider?.GetCompletions(commandPath, prefix) is { } dynamic)
            {
                results.AddRange(dynamic);
            }
        }

        return results;
    }

    private static IEnumerable<string> GetOptions(ICommandContainer container)
    {
        if (container is not ICommandInfo info)
            yield break;

        foreach (var param in info.Parameters.OfType<ICommandOption>())
        {
            if (param.IsHidden)
                continue;

            foreach (var name in param.LongNames)
                yield return $"--{name}";
        }
    }

    /// <summary>Splits a command-line string into tokens, respecting single- and double-quoted strings.</summary>
    internal static List<string> Tokenize(string line)
    {
        var tokens = new List<string>();
        var current = new StringBuilder();
        var inQuote = false;
        var quoteChar = '"';

        foreach (var ch in line)
        {
            if (inQuote)
            {
                if (ch == quoteChar)
                    inQuote = false;
                else
                    current.Append(ch);
            }
            else if (ch is '"' or '\'')
            {
                inQuote = true;
                quoteChar = ch;
            }
            else if (char.IsWhiteSpace(ch))
            {
                if (current.Length > 0)
                {
                    tokens.Add(current.ToString());
                    current.Clear();
                }
            }
            else
            {
                current.Append(ch);
            }
        }

        if (current.Length > 0)
            tokens.Add(current.ToString());

        return tokens;
    }
}
