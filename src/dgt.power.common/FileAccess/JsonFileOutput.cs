// Copyright (c) DIGITALL Nature. All rights reserved

using System.Globalization;
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

    private const int UnicodeEscapeLength = 6; // \uXXXX

    public static string Serialize<T>(T value, JsonSerializerOptions? options = null)
    {
        var copy = options is null ? new JsonSerializerOptions() : new JsonSerializerOptions(options);
        copy.Encoder = s_encoder;
        return RestoreSpaceSeparators(JsonSerializer.Serialize(value, copy));
    }

    // System.Text.Json encoders always escape Unicode space separators (U+00A0, U+2003, U+202F, U+3000, ...)
    // although they are printable, so they are restored afterwards. Escape sequences are consumed as a unit so an
    // escaped backslash followed by "u00A0" stays untouched; line/paragraph separators and control characters
    // keep their escapes.
    private static string RestoreSpaceSeparators(string json)
    {
        if (!json.Contains("\\u", StringComparison.Ordinal))
        {
            return json;
        }

        var builder = new StringBuilder(json.Length);
        var position = 0;
        while (position < json.Length)
        {
            var length = EscapeLength(json, position);
            if (length == UnicodeEscapeLength &&
                ushort.TryParse(json.AsSpan(position + 2, 4), NumberStyles.AllowHexSpecifier, CultureInfo.InvariantCulture, out var code) &&
                char.GetUnicodeCategory((char)code) == UnicodeCategory.SpaceSeparator)
            {
                builder.Append((char)code);
            }
            else
            {
                builder.Append(json, position, length);
            }

            position += length;
        }

        return builder.ToString();
    }

    private static int EscapeLength(string json, int position)
    {
        if (json[position] != '\\' || position + 1 >= json.Length)
        {
            return 1;
        }

        return json[position + 1] == 'u' && position + UnicodeEscapeLength <= json.Length ? UnicodeEscapeLength : 2;
    }
}
