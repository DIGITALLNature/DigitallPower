// Copyright (c) DIGITALL Nature. All rights reserved

using System.Xml.Linq;
using dgt.power.documenttemplates.Logic;
using Microsoft.Xrm.Sdk.Metadata;

namespace dgt.power.documenttemplates.tests;

public class WordTemplateAdapterTests
{
    [Test]
    [Arguments("AccountContactRepeating.docx", 5)]
    [Arguments("AccountRelationships.docx", 6)]
    [Arguments("AccountLeadManyToMany.docx", 2)]
    public async Task PrepareAsync_GeneratedRelationshipsAndRepeatingBindingsAdapt(string fixture, int count)
    {
        var original = TemplateTestData.Fixture(fixture);
        var result = await WordTemplateAdapter.PrepareAsync(original, "account", TemplateTestData.Catalog(), CancellationToken.None);
        var document = TemplateTestData.Xml(result, "word/document.xml");
        var bindings = document.Descendants().Where(element => element.Name.LocalName == "dataBinding").ToArray();
        await Assert.That(bindings.Length).IsEqualTo(count);
        foreach (var binding in bindings)
            await Assert.That(binding.Attributes().Single(attribute => attribute.Name.LocalName == "prefixMappings").Value)
                .Contains("/account/10001/");
        var custom = TemplateTestData.Xml(result, "customXml/item1.xml");
        await Assert.That(custom.Root!.Name.NamespaceName).IsEqualTo("urn:microsoft-crm/document-template/account/10001/");
        await Assert.That(custom.Root.Elements().First().Name.NamespaceName).IsEqualTo("");
        await Assert.That(TemplateTestData.Xml(result, "customXml/itemProps1.xml").ToString()).Contains("/account/10001/");
        var oldPaths = TemplateTestData.Xml(original, "word/document.xml").Descendants()
            .Where(element => element.Name.LocalName == "dataBinding")
            .Select(element => element.Attributes().Single(attribute => attribute.Name.LocalName == "xpath").Value);
        await Assert.That(string.Join("|", bindings.Select(element => element.Attributes().Single(attribute => attribute.Name.LocalName == "xpath").Value)))
            .IsEqualTo(string.Join("|", oldPaths));
    }

    [Test]
    public async Task PrepareAsync_MainHeaderFooterAndSharedNamespaceAdapt()
    {
        var result = await WordTemplateAdapter.PrepareAsync(TemplateTestData.Fixture("Account.docx"),
            "account", TemplateTestData.Catalog(), CancellationToken.None);
        foreach (var part in new[] { "word/document.xml", "word/header2.xml", "word/footer2.xml", "customXml/item1.xml", "customXml/itemProps1.xml" })
        {
            var xml = TemplateTestData.Xml(result, part).ToString();
            await Assert.That(xml).Contains("/account/10001/");
            await Assert.That(xml).DoesNotContain("/account/1/");
            await Assert.That(xml).DoesNotContain("/contact/10002/");
        }
    }

    [Test]
    public async Task PrepareAsync_UnqualifiedBindingAttributesSupported()
    {
        var bytes = TemplateTestData.Transform(TemplateTestData.Fixture("AccountContactRepeating.docx"), (path, xml) =>
            path == "word/document.xml"
                ? xml.Replace("w:prefixMappings=", "prefixMappings=", StringComparison.Ordinal)
                    .Replace("w:xpath=", "xpath=", StringComparison.Ordinal)
                    .Replace("w:storeItemID=", "storeItemID=", StringComparison.Ordinal)
                : xml);
        var result = await WordTemplateAdapter.PrepareAsync(bytes, "account", TemplateTestData.Catalog(), CancellationToken.None);
        await Assert.That(TemplateTestData.Xml(result, "word/document.xml").ToString()).Contains("/account/10001/");
    }

    [Test]
    public async Task PrepareAsync_UnusedFieldsDoNotRequireMetadata()
    {
        var bytes = TemplateTestData.Transform(TemplateTestData.Fixture("AccountContactRepeating.docx"), (path, xml) =>
            path == "customXml/item1.xml" ? xml.Replace("</account>", "<unused_target_missing>Example</unused_target_missing></account>", StringComparison.Ordinal) : xml);
        var result = await WordTemplateAdapter.PrepareAsync(bytes, "account", TemplateTestData.Catalog(), CancellationToken.None);
        await Assert.That(TemplateTestData.Xml(result, "customXml/item1.xml").ToString()).Contains("unused_target_missing");
    }

    [Test]
    public async Task PrepareAsync_ForeignBindingsAndCustomXmlUnchanged()
    {
        var bytes = TemplateTestData.Transform(TemplateTestData.Fixture("AccountContactRepeating.docx"), (_, xml) =>
            xml.Replace("urn:microsoft-crm/document-template/account/1/", "urn:foreign:example", StringComparison.Ordinal));
        var result = await WordTemplateAdapter.PrepareAsync(bytes, "account", TemplateTestData.Catalog(), CancellationToken.None);
        foreach (var part in new[] { "word/document.xml", "customXml/item1.xml", "customXml/itemProps1.xml" })
            await Assert.That(TemplateTestData.Xml(result, part).ToString()).IsEqualTo(TemplateTestData.Xml(bytes, part).ToString());
    }

    [Test]
    public async Task PrepareAsync_ForeignBindingWithUnusedCrmPrefixRemainsUnchanged()
    {
        var bytes = TemplateTestData.Transform(TemplateTestData.Fixture("AccountContactRepeating.docx"), (part, xml) =>
        {
            if (part != "word/document.xml") return xml;
            var document = XDocument.Parse(xml);
            XNamespace word = "http://schemas.openxmlformats.org/wordprocessingml/2006/main";
            document.Root!.Add(new XElement(word + "dataBinding",
                new XAttribute(word + "prefixMappings", "xmlns:f='urn:foreign' xmlns:unused='urn:microsoft-crm/document-template/account/1/'"),
                new XAttribute(word + "xpath", "/f:root/name"),
                new XAttribute(word + "storeItemID", Guid.NewGuid().ToString("B"))));
            return document.ToString();
        });
        var result = await WordTemplateAdapter.PrepareAsync(bytes, "account", TemplateTestData.Catalog(), CancellationToken.None);
        var foreign = TemplateTestData.Xml(result, "word/document.xml").Descendants()
            .Single(element => element.Attributes().Any(attribute => attribute.Name.LocalName == "xpath" && attribute.Value == "/f:root/name"));
        await Assert.That(foreign.Attributes().Single(attribute => attribute.Name.LocalName == "prefixMappings").Value)
            .Contains("/account/1/");
    }

    [Test]
    public async Task PrepareAsync_CustomXmlNamespacedAttributesAdaptConsistently()
    {
        var bytes = TemplateTestData.Transform(TemplateTestData.Fixture("AccountContactRepeating.docx"), (part, xml) =>
        {
            if (part != "customXml/item1.xml") return xml;
            var document = XDocument.Parse(xml);
            document.Root!.Add(new XAttribute(document.Root.Name.Namespace + "marker", "Example"));
            return document.ToString();
        });
        var result = await WordTemplateAdapter.PrepareAsync(bytes, "account", TemplateTestData.Catalog(), CancellationToken.None);
        var attribute = TemplateTestData.Xml(result, "customXml/item1.xml").Root!.Attributes()
            .Single(value => value.Name.LocalName == "marker");
        await Assert.That(attribute.Name.NamespaceName).IsEqualTo("urn:microsoft-crm/document-template/account/10001/");
    }

    [Test]
    [Arguments("name[1]", "missing_field[1]")]
    [Arguments("contact_customer_accounts", "unknown_relationship")]
    [Arguments("contact_customer_accounts[1]/fullname[1]", "contact_customer_accounts[1]/*")]
    [Arguments("account[1]/name[1]", "account[1]//name[1]")]
    [Arguments("name[1]", "name[last()]")]
    [Arguments("name[1]", "@name")]
    public async Task PrepareAsync_MissingOrUnsupportedBindingsFailWithXPath(string source, string replacement)
    {
        var bytes = TemplateTestData.Transform(TemplateTestData.Fixture("AccountContactRepeating.docx"), (path, xml) =>
            path == "word/document.xml" ? xml.Replace(source, replacement, StringComparison.Ordinal) : xml);
        await Assert.That(async () => { await WordTemplateAdapter.PrepareAsync(bytes, "account", TemplateTestData.Catalog(), CancellationToken.None); })
            .Throws<InvalidDataException>().WithMessageContaining("binding");
    }

    [Test]
    public async Task PrepareAsync_MissingOrAmbiguousStoreFails()
    {
        var bytes = TemplateTestData.Transform(TemplateTestData.Fixture("AccountContactRepeating.docx"), (path, xml) =>
            path == "word/document.xml"
                ? xml.Replace("11111111-2222-4333-8444-555555555555", "22222222-2222-4333-8444-555555555555", StringComparison.Ordinal) : xml);
        await Assert.That(async () => { await WordTemplateAdapter.PrepareAsync(bytes, "account", TemplateTestData.Catalog(), CancellationToken.None); })
            .Throws<InvalidDataException>().WithMessageContaining("store");
    }

    [Test]
    [Arguments("missing")]
    [Arguments("intersect")]
    [Arguments("endpoints")]
    [Arguments("ambiguous")]
    public async Task PrepareAsync_UnverifiedAliasMetadataRejects(string issue)
    {
        var tables = TemplateTestData.Metadata();
        var relationship = tables["account"].ManyToManyRelationships[0];
        switch (issue)
        {
            case "missing": tables["account"].SetSealedPropertyValue("ManyToManyRelationships", Array.Empty<ManyToManyRelationshipMetadata>()); break;
            case "intersect": relationship.IntersectEntityName = "wrong"; break;
            case "endpoints": relationship.Entity2LogicalName = "contact"; break;
            case "ambiguous": tables["account"].SetSealedPropertyValue("ManyToManyRelationships", new[] { relationship, relationship }); break;
        }
        await Assert.That(async () => { await WordTemplateAdapter.PrepareAsync(TemplateTestData.Fixture("AccountLeadManyToMany.docx"),
            "account", TemplateTestData.Catalog(tables), CancellationToken.None); })
            .Throws<InvalidDataException>().WithMessageContaining("alias");
    }

    [Test]
    public async Task PrepareAsync_UnknownAliasNotInferredFromFields()
    {
        var bytes = TemplateTestData.Transform(TemplateTestData.Fixture("AccountLeadManyToMany.docx"),
            (path, xml) => path == "word/document.xml" ? xml.Replace("account_leads", "accounts_leads", StringComparison.Ordinal) : xml);
        await Assert.That(async () => { await WordTemplateAdapter.PrepareAsync(bytes, "account", TemplateTestData.Catalog(), CancellationToken.None); })
            .Throws<InvalidDataException>().WithMessageContaining("accounts_leads");
    }

    [Test]
    public async Task PrepareAsync_DisplayProjectionRequiresAttributeOfBase()
    {
        var tables = TemplateTestData.Metadata();
        tables["account"].SetAttributeCollection(tables["account"].Attributes.Where(attribute => attribute.LogicalName != "parentaccountid"));
        await Assert.That(async () => { await WordTemplateAdapter.PrepareAsync(TemplateTestData.Fixture("AccountContactRepeating.docx"),
            "account", TemplateTestData.Catalog(tables), CancellationToken.None); })
            .Throws<InvalidDataException>().WithMessageContaining("AttributeOf");
    }

    [Test]
    [Arguments("objectTypeCode")]
    [Arguments("primaryTable")]
    [Arguments("relatedField")]
    [Arguments("headerField")]
    [Arguments("footerField")]
    public async Task PrepareAsync_MissingTargetMetadataRejects(string kind)
    {
        var tables = TemplateTestData.Metadata();
        var fixture = "AccountContactRepeating.docx";
        switch (kind)
        {
            case "objectTypeCode": tables["account"].SetSealedPropertyValue("ObjectTypeCode", null); break;
            case "primaryTable": tables["account"].LogicalName = "different"; break;
            case "relatedField": tables["contact"].SetAttributeCollection([]); break;
            case "headerField":
                fixture = "Account.docx";
                tables["account"].SetAttributeCollection(tables["account"].Attributes.Where(attribute => attribute.LogicalName != "accountnumber"));
                break;
            case "footerField":
                fixture = "Account.docx";
                tables["account"].SetAttributeCollection(tables["account"].Attributes.Where(attribute => attribute.LogicalName != "accountid"));
                break;
        }
        await Assert.That(async () => { await WordTemplateAdapter.PrepareAsync(TemplateTestData.Fixture(fixture),
            "account", TemplateTestData.Catalog(tables), CancellationToken.None); }).Throws<InvalidDataException>();
    }

    [Test]
    [Arguments("contacts_navigation", "contact")]
    [Arguments("contact_customer_accounts", "contact")]
    [Arguments("primarycontactid", "contact")]
    [Arguments("account_primary_contact", "contact")]
    [Arguments("accountleads_association", "lead")]
    [Arguments("account_lead_navigation", "lead")]
    [Arguments("account_leads", "account")]
    [Arguments("lead_accounts", "lead")]
    public async Task ResolveRelationshipAsync_MetadataAndVerifiedAliasesResolve(string name, string expected)
    {
        var result = await TemplateTestData.Catalog().ResolveRelationshipAsync("account", name, CancellationToken.None);
        await Assert.That(result).IsEqualTo(expected);
    }

    [Test]
    public async Task ResolveRelationshipAsync_AmbiguousSchemaFails()
    {
        var tables = TemplateTestData.Metadata();
        tables["account"].SetSealedPropertyValue("ManyToOneRelationships", new OneToManyRelationshipMetadata[]
        {
            new() { SchemaName = "contact_customer_accounts", ReferencingEntity = "account", ReferencedEntity = "lead" }
        });
        await Assert.That(async () => { await TemplateTestData.Catalog(tables).ResolveRelationshipAsync("account", "contact_customer_accounts", CancellationToken.None); })
            .Throws<InvalidDataException>().WithMessageContaining("ambiguous");
    }

    [Test]
    public async Task PrepareAsync_CancellationPropagates()
    {
        using var cancellation = new CancellationTokenSource();
        await cancellation.CancelAsync();
        await Assert.That(async () => { await WordTemplateAdapter.PrepareAsync(TemplateTestData.Fixture("AccountContactRepeating.docx"),
            "account", TemplateTestData.Catalog(), cancellation.Token); }).Throws<OperationCanceledException>();
    }
}
