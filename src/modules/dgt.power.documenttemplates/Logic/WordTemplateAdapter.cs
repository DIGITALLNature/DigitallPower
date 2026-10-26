// Copyright (c) DIGITALL Nature. All rights reserved

using System.Globalization;
using System.IO.Compression;
using System.Text.RegularExpressions;
using System.Xml.Linq;
using DocumentFormat.OpenXml.Packaging;

namespace dgt.power.documenttemplates.Logic;

public static partial class WordTemplateAdapter
{
    private const string WordNamespace = "http://schemas.openxmlformats.org/wordprocessingml/2006/main";
    private const string Word15Namespace = "http://schemas.microsoft.com/office/word/2012/wordml";
    private const string DataStoreNamespace = "http://schemas.openxmlformats.org/officeDocument/2006/customXml";

    public static Task<byte[]> PrepareAsync(byte[] bytes, string primaryTable, TemplateMetadata metadata,
        CancellationToken cancellationToken)
    {
        ArgumentNullException.ThrowIfNull(bytes);
        ArgumentNullException.ThrowIfNull(metadata);
        ArgumentException.ThrowIfNullOrWhiteSpace(primaryTable);
        return PrepareCoreAsync(bytes, primaryTable, metadata, cancellationToken);
    }

    private static async Task<byte[]> PrepareCoreAsync(byte[] bytes, string primaryTable, TemplateMetadata metadata,
        CancellationToken cancellationToken)
    {
        cancellationToken.ThrowIfCancellationRequested();
        DocumentPackage.Validate(bytes, "word");
        var stores = ResolveStores(bytes, out var wordParts);
        var partPaths = wordParts.Concat(stores.SelectMany(store => new[] { store.Path, store.PropertiesPath }))
            .ToHashSet(StringComparer.Ordinal);
        using var stream = new MemoryStream();
        await stream.WriteAsync(bytes, cancellationToken);
        var documents = new Dictionary<string, XDocument>(StringComparer.Ordinal);
        await using (var archive = new ZipArchive(stream, ZipArchiveMode.Update, true))
        {
            foreach (var entry in archive.Entries.Where(entry => partPaths.Contains(entry.FullName)))
            {
                await using var input = await entry.OpenAsync(cancellationToken);
                documents.Add(entry.FullName, await XDocument.LoadAsync(input, LoadOptions.PreserveWhitespace, cancellationToken));
            }
            var replacements = new Dictionary<string, string>(StringComparer.Ordinal);
            var referencedParts = new HashSet<string>(StringComparer.Ordinal);
            var crmBindings = new HashSet<XElement>();
            foreach (var (path, document) in documents.Where(pair => wordParts.Contains(pair.Key)))
            {
                foreach (var binding in document.Descendants().Where(element => element.Name is
                             { LocalName: "dataBinding", NamespaceName: WordNamespace or Word15Namespace }))
                {
                    cancellationToken.ThrowIfCancellationRequested();
                    var mappings = Attribute(binding, "prefixMappings") ?? "";
                    var xpath = Attribute(binding, "xpath") ?? "";
                    var storeId = Attribute(binding, "storeItemID");
                    var storeMatches = stores.Where(store => Guid.TryParse(storeId, out var id) && store.Id == id).ToArray();
                    var storeIsCrm = Array.Exists(storeMatches, store => documents[store.Path].Root?.Name.NamespaceName.StartsWith(
                        "urn:microsoft-crm/document-template/", StringComparison.Ordinal) == true);
                    if (!storeIsCrm && !mappings.Contains("urn:microsoft-crm/document-template/", StringComparison.Ordinal))
                        continue;
                    try
                    {
                        var namespaces = ParseMappings(mappings);
                        var crmNamespaces = namespaces.Values.Where(value => value.StartsWith(
                            "urn:microsoft-crm/document-template/", StringComparison.Ordinal)).Distinct().ToArray();
                        var rootStep = xpath.Split('/', StringSplitOptions.RemoveEmptyEntries).FirstOrDefault();
                        var rootMatch = rootStep is null ? Match.Empty : PathStep().Match(rootStep);
                        var rootPrefix = rootMatch.Groups["prefix"].Value;
                        var rootIsCrm = rootMatch.Success && namespaces.TryGetValue(rootPrefix, out var rootNamespace) &&
                                        rootNamespace.StartsWith("urn:microsoft-crm/document-template/", StringComparison.Ordinal);
                        if (!storeIsCrm && !rootIsCrm)
                            continue;
                        if (crmNamespaces.Length != 1 || storeMatches.Length != 1)
                            throw new InvalidDataException("CRM binding must resolve one namespace and one custom XML store.");
                        var sourceNamespace = crmNamespaces[0];
                        var namespaceMatch = CrmNamespace().Match(sourceNamespace);
                        if (!namespaceMatch.Success ||
                            !string.Equals(namespaceMatch.Groups["table"].Value, primaryTable, StringComparison.OrdinalIgnoreCase))
                            throw new InvalidDataException("CRM namespace does not identify the manifest primary table.");
                        var store = storeMatches[0];
                        if (documents[store.Path].Root?.Name != XName.Get("DocumentTemplate", sourceNamespace))
                            throw new InvalidDataException("CRM custom XML store has an incompatible root.");
                        await ValidateBindingAsync(xpath, namespaces, sourceNamespace, primaryTable, metadata, cancellationToken);
                        var table = await metadata.GetAsync(primaryTable, cancellationToken);
                        if (table.ObjectTypeCode is not { } code)
                            throw new InvalidDataException($"Missing object type code for '{primaryTable}'.");
                        replacements[sourceNamespace] = $"urn:microsoft-crm/document-template/{table.LogicalName}/{code.ToString(CultureInfo.InvariantCulture)}/";
                        referencedParts.Add(store.Path);
                        referencedParts.Add(store.PropertiesPath);
                        crmBindings.Add(binding);
                    }
                    catch (InvalidDataException exception)
                    {
                        throw new InvalidDataException($"Invalid CRM binding '{xpath}' in '{path}': {exception.Message}", exception);
                    }
                }
            }
            foreach (var (path, document) in documents.Where(pair => wordParts.Contains(pair.Key) || referencedParts.Contains(pair.Key)))
            {
                if (!Rewrite(document, replacements, wordParts.Contains(path) ? crmBindings : null))
                    continue;
                await using var output = await archive.GetEntry(path)!.OpenAsync(cancellationToken);
                output.SetLength(0);
                await document.SaveAsync(output, SaveOptions.DisableFormatting, cancellationToken);
            }
        }
        var prepared = stream.ToArray();
        DocumentPackage.Validate(prepared, "word");
        return prepared;
    }

    private static async Task ValidateBindingAsync(string xpath, Dictionary<string, string> namespaces, string crmNamespace,
        string primaryTable, TemplateMetadata metadata, CancellationToken cancellationToken)
    {
        if (!xpath.StartsWith('/') || xpath.Contains("//", StringComparison.Ordinal))
            throw new InvalidDataException("Only absolute generated CRM paths are supported.");
        var steps = xpath[1..].Split('/');
        if (steps.Length < 3)
            throw new InvalidDataException("CRM binding has no used field or collection.");
        var names = new List<string>();
        for (var index = 0; index < steps.Length; index++)
        {
            var match = PathStep().Match(steps[index]);
            if (!match.Success)
                throw new InvalidDataException($"Unsupported XPath step '{steps[index]}'.");
            var prefix = match.Groups["prefix"].Value;
            if (index == 0 && (match.Groups["name"].Value != "DocumentTemplate" ||
                               !namespaces.TryGetValue(prefix, out var uri) || uri != crmNamespace))
                throw new InvalidDataException("CRM binding must start at its namespaced DocumentTemplate root.");
            if (index > 0 && prefix.Length > 0 &&
                (!namespaces.TryGetValue(prefix, out var stepNamespace) || stepNamespace != crmNamespace))
                throw new InvalidDataException("Unsupported namespace in CRM path.");
            names.Add(match.Groups["name"].Value);
        }
        if (!string.Equals(names[1], primaryTable, StringComparison.OrdinalIgnoreCase))
            throw new InvalidDataException("CRM binding primary table differs from the manifest.");
        var current = primaryTable;
        for (var index = 2; index < names.Count; index++)
        {
            var name = names[index];
            if (index == names.Count - 1 && await metadata.HasAttributeAsync(current, name, cancellationToken))
                return;
            current = await metadata.ResolveRelationshipAsync(current, name, cancellationToken);
        }
    }

    private static List<(Guid Id, string Path, string PropertiesPath)> ResolveStores(byte[] bytes,
        out HashSet<string> wordParts)
    {
        using var stream = new MemoryStream(bytes, false);
        using var package = WordprocessingDocument.Open(stream, false);
        var main = package.MainDocumentPart ?? throw new InvalidDataException("Missing Word main document part.");
        OpenXmlPart[] parts = [main, .. main.HeaderParts, .. main.FooterParts];
        wordParts = parts.Select(part => part.Uri.OriginalString.TrimStart('/')).ToHashSet(StringComparer.Ordinal);
        var stores = new List<(Guid, string, string)>();
        foreach (var custom in parts.SelectMany(part => part.Parts).Select(pair => pair.OpenXmlPart)
                     .OfType<CustomXmlPart>().DistinctBy(part => part.Uri))
        {
            if (custom.CustomXmlPropertiesPart is not { } properties)
                continue;
            using var input = properties.GetStream(FileMode.Open, FileAccess.Read);
            var document = XDocument.Load(input);
            if (document.Root?.Name != XName.Get("datastoreItem", DataStoreNamespace))
                continue;
            var idValue = document.Root.Attributes().SingleOrDefault(attribute => attribute.Name.LocalName == "itemID")?.Value;
            if (Guid.TryParse(idValue, out var id))
                stores.Add((id, custom.Uri.OriginalString.TrimStart('/'), properties.Uri.OriginalString.TrimStart('/')));
        }
        return stores;
    }

    private static Dictionary<string, string> ParseMappings(string mappings)
    {
        try
        {
            return XElement.Parse("<m " + mappings + "/>").Attributes().Where(attribute => attribute.IsNamespaceDeclaration)
                .ToDictionary(attribute => attribute.Name.LocalName == "xmlns" ? "" : attribute.Name.LocalName,
                    attribute => attribute.Value, StringComparer.Ordinal);
        }
        catch (System.Xml.XmlException exception)
        {
            throw new InvalidDataException("Invalid Word binding prefixMappings.", exception);
        }
    }

    private static string? Attribute(XElement element, string name)
    {
        var matches = element.Attributes().Where(attribute => attribute.Name.LocalName == name).ToArray();
        if (matches.Length > 1)
            throw new InvalidDataException($"Ambiguous binding attribute '{name}'.");
        return matches.SingleOrDefault()?.Value;
    }

    private static bool Rewrite(XDocument document, Dictionary<string, string> replacements, HashSet<XElement>? crmBindings)
    {
        var changed = false;
        foreach (var element in document.Descendants())
        {
            if (replacements.TryGetValue(element.Name.NamespaceName, out var targetNamespace))
            {
                element.Name = XName.Get(element.Name.LocalName, targetNamespace);
                changed = true;
            }
            foreach (var attribute in element.Attributes().ToArray())
            {
                var currentAttribute = attribute;
                if (!attribute.IsNamespaceDeclaration && replacements.TryGetValue(attribute.Name.NamespaceName, out var attributeNamespace))
                {
                    currentAttribute = new XAttribute(XName.Get(attribute.Name.LocalName, attributeNamespace), attribute.Value);
                    attribute.Remove();
                    element.Add(currentAttribute);
                    changed = true;
                }
                if (replacements.TryGetValue(currentAttribute.Value, out var replacement))
                {
                    currentAttribute.Value = replacement;
                    changed = true;
                }
                else if (element.Name.LocalName == "dataBinding" && currentAttribute.Name.LocalName == "prefixMappings" &&
                         (crmBindings is null || crmBindings.Contains(element)))
                {
                    foreach (var (source, target) in replacements)
                    {
                        var updated = currentAttribute.Value.Replace("'" + source + "'", "'" + target + "'", StringComparison.Ordinal)
                            .Replace("\"" + source + "\"", "\"" + target + "\"", StringComparison.Ordinal);
                        changed |= updated != currentAttribute.Value;
                        currentAttribute.Value = updated;
                    }
                }
            }
        }
        return changed;
    }

    [GeneratedRegex("^urn:microsoft-crm/document-template/(?<table>[A-Za-z_][A-Za-z0-9_]*)/[0-9]+/$", RegexOptions.CultureInvariant)]
    private static partial Regex CrmNamespace();

    [GeneratedRegex(@"^(?:(?<prefix>[A-Za-z_][A-Za-z0-9_]*):)?(?<name>[A-Za-z_][A-Za-z0-9_]*)(?:\[[1-9][0-9]*\])?$", RegexOptions.CultureInvariant)]
    private static partial Regex PathStep();
}
