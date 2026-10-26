// Copyright (c) DIGITALL Nature. All rights reserved

using dgt.power.documenttemplates.Logic;
using DocumentFormat.OpenXml.Packaging;
using DocumentFormat.OpenXml.Spreadsheet;

namespace dgt.power.documenttemplates.tests;

public class DocumentPackageTests
{
    [Test]
    [Arguments(false)]
    [Arguments(true)]
    public async Task Validate_ExcelWithChartsheetAccepted(bool chartsOnly)
    {
        await using var stream = new MemoryStream();
        var original = TemplateTestData.Fixture("Accounts.xlsx");
        await stream.WriteAsync(original);
        using (var package = SpreadsheetDocument.Open(stream, true))
        {
            var workbook = package.WorkbookPart!;
            var sheets = workbook.Workbook.Sheets!;
            if (chartsOnly)
            {
                sheets.RemoveAllChildren();
                foreach (var worksheet in workbook.WorksheetParts.ToArray())
                    workbook.DeletePart(worksheet);
            }
            var chart = workbook.AddNewPart<ChartsheetPart>();
            chart.Chartsheet = new Chartsheet(new SheetViews(new SheetView { WorkbookViewId = 0U }));
            var nextId = sheets.Elements<Sheet>().Select(sheet => sheet.SheetId!.Value).DefaultIfEmpty(0U).Max() + 1;
            sheets.Append(new Sheet { Name = "Chart", SheetId = nextId, Id = workbook.GetIdOfPart(chart) });
            workbook.Workbook.Save();
        }
        DocumentPackage.Validate(stream.ToArray(), "excel");
    }

    [Test]
    public async Task ResolveFile_CasingUsesActualFilesystemSemantics()
    {
        var parent = TemplateTestData.DirectoryPath();
        try
        {
            var root = Path.Combine(parent, "Templates");
            var alternate = Path.Combine(parent, "templates");
            Directory.CreateDirectory(root);
            var target = Path.Combine(root, "actual.docx");
            await File.WriteAllBytesAsync(target, [1]);
            if (Directory.Exists(alternate))
            {
                File.CreateSymbolicLink(Path.Combine(root, "link.docx"), target);
                await Assert.That(DocumentPackage.ResolveFile(Path.Combine(alternate, "manifest.json"), "link.docx"))
                    .IsEqualTo(target);
            }
            else
            {
                Directory.CreateDirectory(alternate);
                var outside = Path.Combine(alternate, "outside.docx");
                await File.WriteAllBytesAsync(outside, [2]);
                File.CreateSymbolicLink(Path.Combine(root, "link.docx"), outside);
                await Assert.That(() => DocumentPackage.ResolveFile(Path.Combine(root, "manifest.json"), "link.docx"))
                    .Throws<InvalidDataException>();
            }
        }
        finally { Directory.Delete(parent, true); }
    }

    [Test]
    [Arguments("AccountContactRepeating.docx", "word")]
    [Arguments("Accounts.xlsx", "excel")]
    public async Task Validate_ValidPackageAccepted(string filename, string type)
    {
        DocumentPackage.Validate(TemplateTestData.Fixture(filename), type);
        await Task.CompletedTask;
    }

    [Test]
    [Arguments("AccountContactRepeating.docx", "excel")]
    [Arguments("Accounts.xlsx", "word")]
    public async Task Validate_TypeMismatchRejects(string filename, string type) =>
        await Assert.That(() => DocumentPackage.Validate(TemplateTestData.Fixture(filename), type)).Throws<InvalidDataException>();

    [Test]
    public async Task Validate_MalformedPackageRejects() =>
        await Assert.That(() => DocumentPackage.Validate([1, 2, 3], "word")).Throws<Exception>();

    [Test]
    public async Task Validate_MalformedCustomXmlFailsLocalPackageValidation()
    {
        var bytes = TemplateTestData.Transform(TemplateTestData.Fixture("AccountContactRepeating.docx"), (part, xml) =>
            part == "customXml/item1.xml" ? "<broken>" : xml);
        await Assert.That(() => DocumentPackage.Validate(bytes, "word")).Throws<InvalidDataException>();
    }

    [Test]
    public async Task Validate_ExcelMissingSheetRelationshipRejects()
    {
        var bytes = TemplateTestData.Transform(TemplateTestData.Fixture("Accounts.xlsx"), (part, xml) =>
        {
            if (part != "xl/workbook.xml") return xml;
            var document = System.Xml.Linq.XDocument.Parse(xml);
            var sheet = document.Descendants().First(element => element.Name.LocalName == "sheet");
            sheet.Attributes().Single(attribute => attribute.Name.LocalName == "id").Value = "missing-id";
            return document.ToString();
        });
        await Assert.That(() => DocumentPackage.Validate(bytes, "excel")).Throws<Exception>();
    }

    [Test]
    public async Task ResolveFile_NestedPathRelativeToManifest()
    {
        var directory = TemplateTestData.DirectoryPath();
        try
        {
            Directory.CreateDirectory(Path.Combine(directory, "nested"));
            var expected = Path.Combine(directory, "nested", "document.docx");
            await File.WriteAllBytesAsync(expected, [1]);
            await Assert.That(DocumentPackage.ResolveFile(Path.Combine(directory, "manifest.json"), "nested/document.docx"))
                .IsEqualTo(expected);
        }
        finally { Directory.Delete(directory, true); }
    }

    [Test]
    [Arguments("../outside.docx")]
    [Arguments("/outside.docx")]
    [Arguments("missing.docx")]
    [Arguments("C:\\outside.docx")]
    [Arguments("nested\\..\\outside.docx")]
    public async Task ResolveFile_MissingOrEscapingPathRejects(string relativePath)
    {
        var directory = TemplateTestData.DirectoryPath();
        try
        {
            await Assert.That(() => DocumentPackage.ResolveFile(Path.Combine(directory, "manifest.json"), relativePath))
                .Throws<InvalidDataException>();
        }
        finally { Directory.Delete(directory, true); }
    }

    [Test]
    [Arguments(false)]
    [Arguments(true)]
    public async Task ResolveFile_FileAndDirectorySymlinkEscapesReject(bool directoryLink)
    {
        var directory = TemplateTestData.DirectoryPath();
        var outside = TemplateTestData.DirectoryPath();
        try
        {
            await File.WriteAllTextAsync(Path.Combine(outside, "document.docx"), "outside");
            if (directoryLink)
                Directory.CreateSymbolicLink(Path.Combine(directory, "link"), outside);
            else
                File.CreateSymbolicLink(Path.Combine(directory, "link.docx"), Path.Combine(outside, "document.docx"));
            await Assert.That(() => DocumentPackage.ResolveFile(Path.Combine(directory, "manifest.json"),
                directoryLink ? "link/document.docx" : "link.docx")).Throws<InvalidDataException>();
        }
        finally
        {
            Directory.Delete(directory, true);
            Directory.Delete(outside, true);
        }
    }

    [Test]
    public async Task ResolveFile_InternalSymlinkAllowed()
    {
        var directory = TemplateTestData.DirectoryPath();
        try
        {
            var target = Path.Combine(directory, "actual.docx");
            await File.WriteAllBytesAsync(target, [1]);
            File.CreateSymbolicLink(Path.Combine(directory, "link.docx"), target);
            await Assert.That(DocumentPackage.ResolveFile(Path.Combine(directory, "manifest.json"), "link.docx")).IsEqualTo(target);
        }
        finally { Directory.Delete(directory, true); }
    }
}
