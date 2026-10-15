// Copyright (c) DIGITALL Nature. All rights reserved
// DIGITALL Nature licenses this file to you under the Microsoft Public License.

using System.Text.Json;
using System.Text.Json.Serialization;
using System.Text.RegularExpressions;

namespace dgt.power.export.Base;

internal static class ExportJson
{
    private static readonly JsonSerializerOptions s_options = new()
    {
        Converters =
        {
            new JsonStringEnumConverter(JsonNamingPolicy.CamelCase)
        }
    };

    public static string GetJson(object obj) =>
        Regex.Unescape(JsonSerializer.Serialize(JsonSerializer.Serialize(obj, s_options))).Trim('"');
}
