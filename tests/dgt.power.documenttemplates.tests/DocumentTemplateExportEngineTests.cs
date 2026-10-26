// Copyright (c) DIGITALL Nature. All rights reserved

using dgt.power.documenttemplates.Configuration;
using dgt.power.documenttemplates.Logic;
using Spectre.Console.Testing;

namespace dgt.power.documenttemplates.tests;

public class DocumentTemplateExportEngineTests
{
    [Test]
    public async Task ExportAsync_PreCancelledNeverConnectsOrWrites()
    {
        using var cancellation = new CancellationTokenSource();
        await cancellation.CancelAsync();
        var connection = Mock.Of<dgt.power.common.IDataverseConnection>();
        connection.GetServiceAsync().Throws(new InvalidOperationException("Must not connect"));
        using var console = new TestConsole();
        await Assert.That(() => new DocumentTemplateExportEngine(connection.Object, console)
            .ExportAsync("not-written", null, null, false, cancellation.Token)).Throws<OperationCanceledException>();
    }

    [Test]
    [Arguments(false)]
    [Arguments(true)]
    public async Task ExportAsync_LaterWriteFailureReportsPartialOutputAndCancellationPropagates(bool cancel)
    {
        var directory = TemplateTestData.DirectoryPath();
        try
        {
            var (service, state) = TemplateServiceProxy.Create();
            var entry = TemplateTestData.Entry();
            state.Targets.Add(TemplateTestData.Target(entry, draft: false));
            var calls = 0;
            async Task Write(string path, byte[] content, bool overwrite, CancellationToken token)
            {
                if (++calls == 2)
                {
                    if (cancel) throw new OperationCanceledException(token);
                    throw new IOException("Deliberate write failure");
                }
                await File.WriteAllBytesAsync(path, content, token);
            }
            using var console = new TestConsole();
            var engine = new DocumentTemplateExportEngine(TemplateTestData.Connection(service), console, Write);
            if (cancel)
                await Assert.That(() => engine.ExportAsync(directory, null, null, false, CancellationToken.None))
                    .Throws<OperationCanceledException>();
            else
                await Assert.That(await engine.ExportAsync(directory, null, null, false, CancellationToken.None)).IsFalse();
            await Assert.That(File.Exists(Path.Combine(directory, $"{entry.SourceId:D}.xlsx"))).IsTrue();
            await Assert.That(File.Exists(Path.Combine(directory, "documenttemplates.json"))).IsFalse();
            await Assert.That(console.Output).Contains("partial output", StringComparison.OrdinalIgnoreCase);
            await Assert.That(console.Output).Contains("not rolled back");
        }
        finally { Directory.Delete(directory, true); }
    }

    [Test]
    [Arguments(null, null, 3)]
    [Arguments("account", null, 2)]
    [Arguments(null, "word", 1)]
    [Arguments("account", "excel", 1)]
    [Arguments("contact", "word", 0)]
    public async Task ExportAsync_ActiveOnlyAndFiltersCombined(string? entity, string? type, int count)
    {
        var directory = TemplateTestData.DirectoryPath();
        try
        {
            var (service, state) = TemplateServiceProxy.Create();
            state.Targets.AddRange(
            [
                TemplateTestData.Target(TemplateTestData.Entry("Word", "word"), draft: false),
                TemplateTestData.Target(TemplateTestData.Entry("Excel"), draft: false),
                TemplateTestData.Target(TemplateTestData.Entry("Contact", entity: "contact"), draft: false),
                TemplateTestData.Target(TemplateTestData.Entry("Draft", "word"))
            ]);
            using var console = new TestConsole();
            var result = await new DocumentTemplateExportEngine(TemplateTestData.Connection(service), console)
                .ExportAsync(directory, entity, type, false, CancellationToken.None);
            await Assert.That(result).IsTrue();
            var manifest = DocumentTemplateManifest.Parse(await File.ReadAllTextAsync(Path.Combine(directory, "documenttemplates.json")));
            await Assert.That(manifest.Templates.Count).IsEqualTo(count);
            foreach (var entry in manifest.Templates)
            {
                await Assert.That(entry.File).IsEqualTo($"{entry.SourceId:D}.{(entry.DocumentType == "word" ? "docx" : "xlsx")}");
                await Assert.That(entry.LanguageCode).IsEqualTo(1033);
                await Assert.That(entry.Description).IsEqualTo("old");
            }
            await Assert.That(console.Output).Contains($"Exported {count}");
        }
        finally { Directory.Delete(directory, true); }
    }

    [Test]
    [Arguments(false)]
    [Arguments(true)]
    public async Task ExportAsync_CollisionsCheckedBeforeAnyWrite(bool manifestCollision)
    {
        var directory = TemplateTestData.DirectoryPath();
        try
        {
            var entry = TemplateTestData.Entry();
            var (service, state) = TemplateServiceProxy.Create();
            state.Targets.Add(TemplateTestData.Target(entry, draft: false));
            var collision = Path.Combine(directory, manifestCollision ? "documenttemplates.json" : $"{entry.SourceId:D}.xlsx");
            await File.WriteAllTextAsync(collision, "existing");
            using var console = new TestConsole();
            var result = await new DocumentTemplateExportEngine(TemplateTestData.Connection(service), console)
                .ExportAsync(directory, null, null, false, CancellationToken.None);
            await Assert.That(result).IsFalse();
            await Assert.That(await File.ReadAllTextAsync(collision)).IsEqualTo("existing");
            await Assert.That(Directory.GetFiles(directory).Length).IsEqualTo(1);
            await Assert.That(console.Output).Contains("No output files written");
        }
        finally { Directory.Delete(directory, true); }
    }

    [Test]
    public async Task ExportAsync_OverwriteOnlyGeneratedAndExportedPackageImports()
    {
        var directory = TemplateTestData.DirectoryPath();
        try
        {
            var entry = TemplateTestData.Entry();
            var (service, state) = TemplateServiceProxy.Create();
            state.Targets.Add(TemplateTestData.Target(entry, draft: false));
            var unrelated = Path.Combine(directory, "unrelated.txt");
            await File.WriteAllTextAsync(unrelated, "preserve");
            await File.WriteAllTextAsync(Path.Combine(directory, "documenttemplates.json"), "old");
            using var console = new TestConsole();
            await Assert.That(await new DocumentTemplateExportEngine(TemplateTestData.Connection(service), console)
                .ExportAsync(directory, null, null, true, CancellationToken.None)).IsTrue();
            await Assert.That(await File.ReadAllTextAsync(unrelated)).IsEqualTo("preserve");
            var (targetService, targetState) = TemplateServiceProxy.Create();
            foreach (var item in TemplateTestData.Metadata()) targetState.Metadata.Add(item.Key, item.Value);
            await Assert.That(await new DocumentTemplateImportEngine(TemplateTestData.Connection(targetService), console)
                .ImportAsync(Path.Combine(directory, "documenttemplates.json"), false, CancellationToken.None)).IsTrue();
            await Assert.That(targetState.Targets.Single().Id).IsEqualTo(entry.SourceId);
        }
        finally { Directory.Delete(directory, true); }
    }

    [Test]
    public async Task ExportAsync_GeneratedCollisionFailsBeforeWrites()
    {
        var directory = TemplateTestData.DirectoryPath();
        try
        {
            var (service, state) = TemplateServiceProxy.Create();
            var entry = TemplateTestData.Entry();
            state.Targets.Add(TemplateTestData.Target(entry, draft: false));
            state.Targets.Add(TemplateTestData.Target(entry with { Name = "Other" }, draft: false));
            using var console = new TestConsole();
            await Assert.That(await new DocumentTemplateExportEngine(TemplateTestData.Connection(service), console)
                .ExportAsync(directory, null, null, false, CancellationToken.None)).IsFalse();
            await Assert.That(Directory.GetFiles(directory)).IsEmpty();
        }
        finally { Directory.Delete(directory, true); }
    }

    [Test]
    public async Task ExportAsync_SymlinkDestinationNeverOverwritten()
    {
        var directory = TemplateTestData.DirectoryPath();
        var outside = TemplateTestData.DirectoryPath();
        try
        {
            var (service, state) = TemplateServiceProxy.Create();
            var target = Path.Combine(outside, "preserve.json");
            await File.WriteAllTextAsync(target, "preserve");
            File.CreateSymbolicLink(Path.Combine(directory, "documenttemplates.json"), target);
            using var console = new TestConsole();
            await Assert.That(await new DocumentTemplateExportEngine(TemplateTestData.Connection(service), console)
                .ExportAsync(directory, null, null, true, CancellationToken.None)).IsFalse();
            await Assert.That(await File.ReadAllTextAsync(target)).IsEqualTo("preserve");
        }
        finally
        {
            Directory.Delete(directory, true);
            Directory.Delete(outside, true);
        }
    }
}
