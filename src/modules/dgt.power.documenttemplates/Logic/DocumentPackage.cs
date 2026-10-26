// Copyright (c) DIGITALL Nature. All rights reserved

using DocumentFormat.OpenXml.Packaging;
using DocumentFormat.OpenXml;
using System.IO.Compression;
using System.Xml;
using System.Xml.Linq;

namespace dgt.power.documenttemplates.Logic;

public static class DocumentPackage
{
    public static string ResolveFile(string manifestPath, string relativeFile)
    {
        ArgumentException.ThrowIfNullOrWhiteSpace(manifestPath);
        ArgumentException.ThrowIfNullOrWhiteSpace(relativeFile);
        if (Path.IsPathRooted(relativeFile) || relativeFile.Contains('\\', StringComparison.Ordinal) ||
            relativeFile.Contains(':', StringComparison.Ordinal))
            throw new InvalidDataException($"Document path must be relative: '{relativeFile}'.");
        var root = ResolveLinks(Path.GetDirectoryName(Path.GetFullPath(manifestPath))!);
        var candidate = ResolveLinks(Path.GetFullPath(Path.Combine(root, relativeFile)));
        if (!candidate.StartsWith(root.TrimEnd(Path.DirectorySeparatorChar) + Path.DirectorySeparatorChar, StringComparison.Ordinal) ||
            !System.IO.File.Exists(candidate))
            throw new InvalidDataException($"Document is missing or escapes the manifest directory: '{relativeFile}'.");
        return candidate;
    }

    private static string ResolveLinks(string path)
    {
        var current = Path.GetPathRoot(path)!;
        foreach (var component in path[current.Length..].Split(Path.DirectorySeparatorChar, StringSplitOptions.RemoveEmptyEntries))
        {
            current = Path.Combine(current, component);
            FileSystemInfo info = Directory.Exists(current) ? new DirectoryInfo(current) : new FileInfo(current);
            if (info.LinkTarget is not null)
                current = info.ResolveLinkTarget(true)?.FullName ??
                          throw new InvalidDataException($"Unresolvable symbolic link '{current}'.");
        }
        return Path.GetFullPath(current);
    }

    public static void Validate(byte[] bytes, string documentType)
    {
        ArgumentNullException.ThrowIfNull(bytes);
        using var stream = new MemoryStream(bytes, false);
        try
        {
            using (var archive = new ZipArchive(stream, ZipArchiveMode.Read, true))
            {
                var names = new HashSet<string>(StringComparer.Ordinal);
                foreach (var entry in archive.Entries)
                {
                    if (!names.Add(entry.FullName))
                        throw new InvalidDataException($"Duplicate package part '{entry.FullName}'.");
                    if (!entry.FullName.EndsWith(".xml", StringComparison.Ordinal) &&
                        !entry.FullName.EndsWith(".rels", StringComparison.Ordinal))
                        continue;
                    using var input = entry.Open();
                    using var reader = XmlReader.Create(input, new XmlReaderSettings
                    {
                        DtdProcessing = DtdProcessing.Prohibit,
                        XmlResolver = null
                    });
                    _ = XDocument.Load(reader);
                }
            }
            stream.Position = 0;
            if (documentType == "word")
            {
                using var package = WordprocessingDocument.Open(stream, false);
                if (package.DocumentType != WordprocessingDocumentType.Document ||
                    package.MainDocumentPart?.Document?.Body is null)
                    throw new InvalidDataException("Expected a DOCX document with a main document body.");
                foreach (var part in package.MainDocumentPart.HeaderParts)
                    _ = part.Header ?? throw new InvalidDataException("Invalid Word header.");
                foreach (var part in package.MainDocumentPart.FooterParts)
                    _ = part.Footer ?? throw new InvalidDataException("Invalid Word footer.");
            }
            else if (documentType == "excel")
            {
                using var package = SpreadsheetDocument.Open(stream, false);
                if (package.DocumentType != SpreadsheetDocumentType.Workbook ||
                    package.WorkbookPart?.Workbook?.Sheets is null ||
                    !package.WorkbookPart.WorksheetParts.Any())
                    throw new InvalidDataException("Expected an XLSX workbook with worksheets.");
                foreach (var part in package.WorkbookPart.WorksheetParts)
                    _ = part.Worksheet ?? throw new InvalidDataException("Invalid Excel worksheet.");
                foreach (var sheet in package.WorkbookPart.Workbook.Sheets.Elements<DocumentFormat.OpenXml.Spreadsheet.Sheet>())
                    if (sheet.Id?.Value is not { } relationshipId ||
                        package.WorkbookPart.GetPartById(relationshipId) is not WorksheetPart)
                        throw new InvalidDataException("Excel sheet references a missing worksheet.");
            }
            else
                throw new InvalidDataException($"Unsupported document type '{documentType}'.");
        }
        catch (Exception exception) when (exception is OpenXmlPackageException or System.IO.FileFormatException or XmlException)
        {
            throw new InvalidDataException($"Invalid {documentType} package.", exception);
        }
    }
}
