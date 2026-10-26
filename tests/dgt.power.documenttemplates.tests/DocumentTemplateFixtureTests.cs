// Copyright (c) DIGITALL Nature. All rights reserved
// DIGITALL Nature licenses this file to you under the Microsoft Public License.

using System.IO.Compression;
using System.Xml.Linq;

namespace dgt.power.documenttemplates.tests;

public class DocumentTemplateFixtureTests
{
    [Test]
    [Arguments("AccountRelationships.docx", 6)]
    [Arguments("AccountLeadManyToMany.docx", 2)]
    public async Task RelationshipExamples_RetainGeneratedBindingsWithoutBusinessContent(string file, int expectedBindings)
    {
        using var package = ZipFile.OpenRead(Path.Combine(AppContext.BaseDirectory,
            "Resources", file));
        using var documentStream = package.GetEntry("word/document.xml")!.Open();
        var document = XDocument.Load(documentStream);
        XNamespace word = "http://schemas.openxmlformats.org/wordprocessingml/2006/main";
        XNamespace word15 = "http://schemas.microsoft.com/office/word/2012/wordml";
        var bindings = document.Descendants(word15 + "dataBinding").ToArray();
        await Assert.That(bindings.Length).IsEqualTo(expectedBindings);
        var paths = bindings.Select(element => element.Attribute(word + "xpath")?.Value
            ?? throw new InvalidDataException("Missing fixture XPath.")).ToArray();
        await Assert.That(paths).Contains("/ns0:DocumentTemplate[1]/account[1]/account_leads[1]/name[1]");
        await Assert.That(paths).Contains("/ns0:DocumentTemplate[1]/account[1]/lead_accounts[1]/fullname[1]");
        if (file == "AccountRelationships.docx")
        {
            await Assert.That(paths).Contains("/ns0:DocumentTemplate[1]/account[1]/account_primary_contact[1]/fullname[1]");
            await Assert.That(paths).Contains("/ns0:DocumentTemplate[1]/account[1]/lead_accounts[1]/accountidname[1]");
        }
        await Assert.That(bindings.All(element => element.Attribute(word + "prefixMappings")?.Value.Trim() ==
            "xmlns:ns0='urn:microsoft-crm/document-template/account/1/'")).IsTrue();
        foreach (var entry in package.Entries.Where(entry => entry.FullName.EndsWith(".xml", StringComparison.Ordinal)))
        {
            using var stream = entry.Open();
            var xml = XDocument.Load(stream);
            await Assert.That(xml.Descendants().Where(element => !element.HasElements)
                .All(element => string.IsNullOrWhiteSpace(element.Value) || element.Value == "Example")).IsTrue();
            if (entry.FullName.StartsWith("docProps/", StringComparison.Ordinal) ||
                entry.FullName.StartsWith("docMetadata/", StringComparison.Ordinal))
            {
                await Assert.That(xml.Root!.HasElements).IsFalse();
            }
        }
    }

    [Test]
    public async Task AccountContactRepeating_RetainsBindingsWithoutBusinessContent()
    {
        using var package = ZipFile.OpenRead(Path.Combine(AppContext.BaseDirectory,
            "Resources", "AccountContactRepeating.docx"));
        var document = ReadPart("word/document.xml");
        XNamespace word = "http://schemas.openxmlformats.org/wordprocessingml/2006/main";
        XNamespace word15 = "http://schemas.microsoft.com/office/word/2012/wordml";
        var bindings = document.Descendants().Where(element =>
            element.Name == word + "dataBinding" || element.Name == word15 + "dataBinding").ToArray();

        await Assert.That(bindings.Select(element => element.Attribute(word + "xpath")?.Value
                ?? throw new InvalidDataException("Fixture binding has no XPath.")).ToArray())
            .IsEquivalentTo(new[]
            {
                "/ns0:DocumentTemplate[1]/account[1]/name[1]",
                "/ns0:DocumentTemplate[1]/account[1]/parentaccountidname[1]",
                "/ns0:DocumentTemplate[1]/account[1]/contact_customer_accounts",
                "/ns0:DocumentTemplate[1]/account[1]/contact_customer_accounts[1]/fullname[1]",
                "/ns0:DocumentTemplate[1]/account[1]/contact_customer_accounts[1]/emailaddress1[1]"
            });
        await Assert.That(document.Descendants(word15 + "repeatingSection").Count()).IsEqualTo(1);
        await Assert.That(document.Descendants(word15 + "repeatingSectionItem").Count()).IsEqualTo(1);
        await Assert.That(document.Descendants(word + "t").All(element => element.Value == "Example")).IsTrue();

        var data = ReadPart("customXml/item1.xml");
        await Assert.That(data.Descendants().Where(element => !element.HasElements)
            .Select(element => element.Name.LocalName).ToArray())
            .IsEquivalentTo(new[] { "name", "parentaccountidname", "fullname", "emailaddress1" });
        await Assert.That(data.Descendants().Where(element => !element.HasElements)
            .All(element => element.Value is "Example Account" or "Example Contact" or "fixture@example.invalid")).IsTrue();

        var properties = ReadPart("customXml/itemProps1.xml");
        XNamespace store = "http://schemas.openxmlformats.org/officeDocument/2006/customXml";
        var storeId = (string?)properties.Root!.Attribute(store + "itemID");
        await Assert.That(storeId).IsEqualTo("{11111111-2222-4333-8444-555555555555}");
        await Assert.That(bindings.All(element => (string?)element.Attribute(word + "storeItemID") == storeId)).IsTrue();
        await Assert.That(bindings.All(element => element.Attribute(word + "prefixMappings")?.Value.Trim() ==
            "xmlns:ns0='urn:microsoft-crm/document-template/account/1/'")).IsTrue();
        foreach (var entry in package.Entries.Where(entry => entry.FullName.StartsWith("docProps/", StringComparison.Ordinal)))
        {
            await Assert.That(ReadPart(entry.FullName).Root!.HasElements).IsFalse();
        }

        XDocument ReadPart(string name)
        {
            var entry = package.GetEntry(name) ?? throw new InvalidDataException($"Missing fixture part: {name}");
            using var stream = entry.Open();
            return XDocument.Load(stream);
        }
    }
}
