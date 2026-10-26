// Copyright (c) DIGITALL Nature. All rights reserved

using System.Text;
using System.Text.Encodings.Web;
using System.Text.Json;

namespace dgt.power.common.FileAccess;

/// <summary>
///     Serializes JSON for files that users read, edit, and keep under version control.
/// </summary>
public static class JsonFileOutput
{
    // The relaxed encoder keeps printable non-ASCII text and <, >, & literal. Its name only warns against
    // embedding the output in HTML or JavaScript, which these files never are; JSON-required escapes remain.
    private static readonly JavaScriptEncoder s_encoder = JavaScriptEncoder.UnsafeRelaxedJsonEscaping;

    private const string NoBreakSpaceEscape = "\\u00A0";

    public static string Serialize<T>(T value, JsonSerializerOptions? options = null)
    {
        var copy = options is null ? new JsonSerializerOptions() : new JsonSerializerOptions(options);
        copy.Encoder = s_encoder;
        return RestoreNoBreakSpaces(JsonSerializer.Serialize(value, copy));
    }

    // System.Text.Json encoders always escape U+00A0 (a space separator), so it is restored afterwards.
    // Escape sequences are consumed pairwise so an escaped backslash followed by "u00A0" stays untouched.
    private static string RestoreNoBreakSpaces(string json)
    {
        if (!json.Contains(NoBreakSpaceEscape, StringComparison.OrdinalIgnoreCase))
        {
            return json;
        }

        var builder = new StringBuilder(json.Length);
        for (var i = 0; i < json.Length; i++)
        {
            if (json[i] != '\\' || i + 1 >= json.Length)
            {
                builder.Append(json[i]);
            }
            else if (string.Compare(json, i, NoBreakSpaceEscape, 0, NoBreakSpaceEscape.Length, StringComparison.OrdinalIgnoreCase) == 0)
            {
                builder.Append('\u00A0');
                i += NoBreakSpaceEscape.Length - 1;
            }
            else
            {
                builder.Append(json[i]).Append(json[i + 1]);
                i++;
            }
        }

        return builder.ToString();
    }
}
