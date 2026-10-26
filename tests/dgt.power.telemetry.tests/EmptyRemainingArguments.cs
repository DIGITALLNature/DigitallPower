// SPDX-FileCopyrightText: DIGITALL Nature
// SPDX-License-Identifier: MS-RL

using Spectre.Console.Cli;

namespace dgt.power.telemetry.tests;

internal sealed class EmptyRemainingArguments : IRemainingArguments
{
    public ILookup<string, string?> Parsed =>
        Array.Empty<KeyValuePair<string, string?>>().ToLookup(x => x.Key, x => x.Value);

    public IReadOnlyList<string> Raw => Array.Empty<string>();
}
