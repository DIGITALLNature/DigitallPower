// Copyright (c) DIGITALL Nature. All rights reserved

using System.Text.Json;
using System.Text.Json.Serialization;
using dgt.power.common.FileAccess;

namespace dgt.power.common.tests.FileAccess;

public class JsonFileOutputTests
{
    [Test]
    public async Task Serialize_PrintableNonAscii_IsWrittenLiterally()
    {
        const string value = "Kontakte mit derselben gesch\u00E4ftlichen Telefonnummer\u00A0\u00DF \u00E9";

        var json = JsonFileOutput.Serialize(new { name = value });

        await Assert.That(json).Contains("gesch\u00E4ftlichen");
        await Assert.That(json).Contains("\u00A0");
        await Assert.That(json).DoesNotContain("\\u00");
        await Assert.That(JsonDocument.Parse(json).RootElement.GetProperty("name").GetString()).IsEqualTo(value);
    }

    [Test]
    public async Task Serialize_JsonRequiredCharacters_AreEscapedAndRoundTrip()
    {
        const string value = "quote\" backslash\\ tab\t newline\n control\u0001";

        var json = JsonFileOutput.Serialize(new { name = value });

        await Assert.That(json).Contains("\\\"");
        await Assert.That(json).Contains("\\\\");
        await Assert.That(json).Contains("\\t");
        await Assert.That(json).Contains("\\n");
        await Assert.That(json).Contains("\\u0001");
        await Assert.That(JsonDocument.Parse(json).RootElement.GetProperty("name").GetString()).IsEqualTo(value);
    }

    [Test]
    public async Task Serialize_HtmlSensitiveCharacters_AreNotEscaped()
    {
        var json = JsonFileOutput.Serialize(new { name = "<a & b> 'c'" });

        await Assert.That(json).Contains("<a & b> 'c'");
    }

    [Test]
    public async Task Serialize_SupplementaryCharacters_RoundTrip()
    {
        const string value = "emoji \U0001F600 and \U00020BB7";

        var json = JsonFileOutput.Serialize(new { name = value });

        await Assert.That(JsonDocument.Parse(json).RootElement.GetProperty("name").GetString()).IsEqualTo(value);
    }

    [Test]
    public async Task Serialize_UnpairedSurrogate_IsReplacedAndOutputStaysValid()
    {
        var json = JsonFileOutput.Serialize(new { name = "broken\uD800" });

        await Assert.That(JsonDocument.Parse(json).RootElement.GetProperty("name").GetString()).IsEqualTo("broken\uFFFD");
    }

    [Test]
    public async Task Serialize_EscapedBackslashBeforeNoBreakSpaceText_IsNotAltered()
    {
        const string value = "literal \\u00A0 text and real\u00A0space";

        var json = JsonFileOutput.Serialize(new { name = value });

        await Assert.That(json).Contains("literal \\\\u00A0 text");
        await Assert.That(JsonDocument.Parse(json).RootElement.GetProperty("name").GetString()).IsEqualTo(value);
    }

    [Test]
    public async Task Serialize_CallerOptions_ArePreservedAndNotMutated()
    {
        var options = new JsonSerializerOptions
        {
            WriteIndented = true,
            DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingNull
        };

        var json = JsonFileOutput.Serialize(new { name = "\u00E4", missing = (string?)null }, options);

        await Assert.That(json).Contains("\n");
        await Assert.That(json).DoesNotContain("missing");
        await Assert.That(options.Encoder).IsNull();
    }
}
