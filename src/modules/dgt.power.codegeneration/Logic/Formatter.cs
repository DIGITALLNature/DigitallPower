// Copyright (c) DIGITALL Nature. All rights reserved
// DIGITALL Nature licenses this file to you under the Microsoft Public License.

using System.Globalization;
using System.Text;
using System.Text.RegularExpressions;
using Microsoft.Xrm.Sdk;

namespace dgt.power.codegeneration.Logic;

public static class Formatter
{
    public static readonly Regex NewlineRegex = new (@"\r\n|\r|\n", RegexOptions.Compiled);

    // TODO: This method is not really camel casing. The first character isn't lowered, instead the casing isn't changed.
    public static string CamelCase(string? phrase)
    {
        return ConvertCaseString(phrase, Case.CamelCase);
    }

    // TODO: This method is not really pascal casing. It is actually camel casing.
    public static string PascalCase(string? phrase)
    {
        return ConvertCaseString(phrase, Case.PascalCase);
    }

    private static string ConvertCaseString(string? phrase, Case cases)
    {
        if (string.IsNullOrWhiteSpace(phrase))
        {
            return CamelCase(Sanitize(phrase));
        }

        var leadingUnderscore = phrase.StartsWith("_", StringComparison.InvariantCulture);//"_AddMemberBatch" and "AddMemberBatch"

        var splittedPhrase = phrase.Split(' ', '-', '_');
        var sb = new StringBuilder();

        if (cases == Case.PascalCase)
        {
            sb.Append(splittedPhrase[0].ToLower(CultureInfo.InvariantCulture));
            splittedPhrase[0] = string.Empty;
        }

        foreach (var item in splittedPhrase.Where(item => item.Length > 0))
        {
            sb.Append(item[0].ToString(CultureInfo.InvariantCulture).ToUpperInvariant());
            if (item.Length > 1)
            {
                sb.Append(item.AsSpan(1));
            }
        }

        if (leadingUnderscore)
        {
            sb.Insert(0, "_");
        }

        return sb.ToString();
    }

    public static string Sanitize(string? value, bool allowWhiteSpace = false, bool allowSafeStringChars = false, bool allowFirstNumber = false)
    {
        // Empty Value (occures on Organization-entity Optionsets)
        if (string.IsNullOrWhiteSpace(value))
        {
            value = "_empty_";
        }

        // leading number
        var result = PreventFirstNumber(value, allowFirstNumber);
        result = result
            .Replace("...", "_ellipsis_")
            .Replace("@", "_euro_")
            .Replace("%", "_percent_");
        return value.ToLowerInvariant().Where(character => !IsValidFieldNameCharacter(character, allowWhiteSpace, allowSafeStringChars))
            .Aggregate(result, (current, character) => current.Replace(character, '_'));
    }

    public static string GetLocalizedLabel(Label label, bool useBaseLanguage, int systemLanguage)
    {
        var txt = useBaseLanguage
            ? label.LocalizedLabels?.SingleOrDefault(l => l.LanguageCode == systemLanguage)?.Label
            : label.UserLocalizedLabel?.Label;
        if (!string.IsNullOrEmpty(txt))
        {
            txt = NewlineRegex.Replace(txt, "\r\n");
        }
        return txt!;
    }

    private static bool IsValidFieldNameCharacter(char character, bool allowWhitespace, bool allowSafeStringChars)
    {
        var validValues = "abcdefghijklmnopqrstuvwxyz1234567890_äöüß";
        if (allowWhitespace)
        {
            validValues += " ";
        }

        if (allowSafeStringChars)
        {
            validValues += "(/)-.";
        }

        return validValues.Contains(character);
    }

    public static string PreventFirstNumber(string input, bool allowFirstNumber = false)
    {
        if (!allowFirstNumber && int.TryParse(input.FirstOrDefault().ToString(CultureInfo.InvariantCulture), out _))
        {
            return $"_{input}";
        }
        return input;
    }

    public static string MaskDoubleQuote(string input)
    {
        return input.Replace("\"", "\\\"");
    }

    #region Nested type: Case

    private enum Case
    {
        PascalCase,
        CamelCase
    }

    #endregion
}
