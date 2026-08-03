// Copyright (c) DIGITALL Nature. All rights reserved
// DIGITALL Nature licenses this file to you under the Microsoft Public License.

using Spectre.Console.Cli;

namespace dgt.power.connection.tests;

internal sealed class EmptyRemainingArguments : IRemainingArguments
{
    public ILookup<string, string?> Parsed =>
        Array.Empty<KeyValuePair<string, string?>>().ToLookup(x => x.Key, x => x.Value);

    public IReadOnlyList<string> Raw => Array.Empty<string>();
}
