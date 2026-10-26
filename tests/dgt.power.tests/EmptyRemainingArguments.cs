// SPDX-FileCopyrightText: DIGITALL Nature
// SPDX-License-Identifier: MS-RL

using Spectre.Console.Cli;

namespace dgt.power.tests;

/// <summary>
/// A null-object implementation of <see cref="IRemainingArguments"/> used in tests.
/// </summary>
internal class EmptyRemainingArguments : IRemainingArguments
{
    public ILookup<string, string?> Parsed =>
        Array.Empty<KeyValuePair<string, string?>>().ToLookup(x => x.Key, x => x.Value);

    public IReadOnlyList<string> Raw => Array.Empty<string>();
}
