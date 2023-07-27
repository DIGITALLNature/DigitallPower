// Copyright (c) DIGITALL Nature. All rights reserved
// DIGITALL Nature licenses this file to you under the Microsoft Public License.

namespace dgt.power.codegeneration.Extensions;

public static class StringExtensions
{
    public static string ReplaceAll(this string value, IEnumerable<char> oldChars, char newChar)
    {
        return oldChars.Aggregate(value, (str, character) => str.Replace(character, newChar));
    }
}