// Copyright (c) DIGITALL Nature. All rights reserved

using System.IO.Compression;
using System.Text;
using System.Xml.Linq;
using dgt.power.common;
using dgt.power.documenttemplates.Configuration;
using dgt.power.documenttemplates.Logic;
using Microsoft.PowerPlatform.Dataverse.Client;
using Microsoft.Xrm.Sdk;
using Microsoft.Xrm.Sdk.Metadata;

namespace dgt.power.documenttemplates.tests;

public static class TemplateTestData
{
    public static DocumentTemplateEntry Entry(string name = "Example", string type = "excel", string entity = "account") =>
        new(Guid.NewGuid(), name, type, entity, type == "word" ? "template.docx" : "template.xlsx", 1031, null);

    public static byte[] Fixture(string name) => File.ReadAllBytes(Path.Combine(AppContext.BaseDirectory, "Resources", name));

    public static string DirectoryPath()
    {
        var path = Path.Combine(Environment.CurrentDirectory, ".documenttemplates-tests", Guid.NewGuid().ToString("N"));
        Directory.CreateDirectory(path);
        return path;
    }

    public static string WriteManifest(string directory, params DocumentTemplateEntry[] entries)
    {
        var path = Path.Combine(directory, "documenttemplates.json");
        File.WriteAllText(path, new DocumentTemplateManifest(entries).ToJson());
        foreach (var entry in entries)
            File.WriteAllBytes(Path.Combine(directory, entry.File), Fixture(entry.DocumentType == "word" ? "AccountContactRepeating.docx" : "Accounts.xlsx"));
        return path;
    }

    public static IDataverseConnection Connection(IOrganizationServiceAsync2 service, Action? acquired = null)
    {
        var mock = Mock.Of<IDataverseConnection>();
        mock.GetServiceAsync().Returns(() =>
        {
            acquired?.Invoke();
            return service;
        });
        return mock.Object;
    }

    public static Entity Target(DocumentTemplateEntry entry, Guid? id = null, bool draft = true) => new("documenttemplate", id ?? entry.SourceId)
    {
        ["name"] = entry.Name, ["documenttype"] = new OptionSetValue(entry.TypeCode),
        ["associatedentitytypecode"] = entry.Entity, ["status"] = draft,
        ["description"] = "old", ["languagecode"] = 1033,
        ["content"] = Convert.ToBase64String(Fixture(entry.DocumentType == "word" ? "AccountContactRepeating.docx" : "Accounts.xlsx"))
    };

    public static Dictionary<string, EntityMetadata> Metadata()
    {
        var relationship = new ManyToManyRelationshipMetadata
        {
            SchemaName = "accountleads_association", Entity1LogicalName = "account", Entity2LogicalName = "lead",
            Entity1NavigationPropertyName = "account_lead_navigation", Entity2NavigationPropertyName = "lead_account_navigation",
            IntersectEntityName = "accountleads"
        };
        return new Dictionary<string, EntityMetadata>(StringComparer.OrdinalIgnoreCase)
        {
            ["account"] = Table("account", 10001,
                [
                    new StringAttributeMetadata { LogicalName = "name" },
                    new UniqueIdentifierAttributeMetadata { LogicalName = "accountid" },
                    new StringAttributeMetadata { LogicalName = "accountnumber" },
                    new LookupAttributeMetadata { LogicalName = "parentaccountid" },
                    Projection("parentaccountidname", "parentaccountid")
                ],
                [
                    new() { SchemaName = "contact_customer_accounts", ReferencedEntity = "account", ReferencingEntity = "contact",
                        ReferencedEntityNavigationPropertyName = "contacts_navigation" }
                ],
                [
                    new() { SchemaName = "account_primary_contact", ReferencingEntity = "account", ReferencedEntity = "contact",
                        ReferencingEntityNavigationPropertyName = "primarycontactid" }
                ], [relationship]),
            ["contact"] = Table("contact", 10002,
                [new StringAttributeMetadata { LogicalName = "fullname" }, new StringAttributeMetadata { LogicalName = "emailaddress1" }]),
            ["lead"] = Table("lead", 10003,
                [
                    new StringAttributeMetadata { LogicalName = "fullname" },
                    new LookupAttributeMetadata { LogicalName = "accountid" },
                    Projection("accountidname", "accountid")
                ], manyToMany: [relationship])
        };
    }

    private static StringAttributeMetadata Projection(string name, string parent)
    {
        var attribute = new StringAttributeMetadata { LogicalName = name };
        attribute.SetSealedPropertyValue("AttributeOf", parent);
        return attribute;
    }

    private static EntityMetadata Table(string name, int code, AttributeMetadata[] attributes,
        OneToManyRelationshipMetadata[]? oneToMany = null, OneToManyRelationshipMetadata[]? manyToOne = null,
        ManyToManyRelationshipMetadata[]? manyToMany = null)
    {
        var table = new EntityMetadata { LogicalName = name };
        table.SetSealedPropertyValue("ObjectTypeCode", code);
        table.SetAttributeCollection(attributes);
        table.SetSealedPropertyValue("OneToManyRelationships", oneToMany ?? []);
        table.SetSealedPropertyValue("ManyToOneRelationships", manyToOne ?? []);
        table.SetSealedPropertyValue("ManyToManyRelationships", manyToMany ?? []);
        return table;
    }

    public static TemplateMetadata Catalog(Dictionary<string, EntityMetadata>? tables = null)
    {
        tables ??= Metadata();
        return new TemplateMetadata((name, token) =>
        {
            token.ThrowIfCancellationRequested();
            return Task.FromResult(tables[name]);
        });
    }

    public static byte[] Transform(byte[] bytes, Func<string, string, string> rewrite)
    {
        using var stream = new MemoryStream();
        stream.Write(bytes);
        using (var archive = new ZipArchive(stream, ZipArchiveMode.Update, true))
            foreach (var entry in archive.Entries.Where(entry => entry.FullName.EndsWith(".xml", StringComparison.Ordinal)))
            {
                string xml;
                using (var reader = new StreamReader(entry.Open()))
                    xml = reader.ReadToEnd();
                var result = rewrite(entry.FullName, xml);
                if (result == xml)
                    continue;
                using var output = entry.Open();
                output.SetLength(0);
                output.Write(Encoding.UTF8.GetBytes(result));
            }
        return stream.ToArray();
    }

    public static XDocument Xml(byte[] bytes, string part)
    {
        using var stream = new MemoryStream(bytes);
        using var archive = new ZipArchive(stream, ZipArchiveMode.Read);
        using var input = archive.GetEntry(part)!.Open();
        return XDocument.Load(input);
    }
}
