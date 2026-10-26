// Copyright (c) DIGITALL Nature. All rights reserved

using dgt.power.common;
using dgt.power.documenttemplates.Logic;
using Microsoft.Xrm.Sdk;
using Microsoft.Xrm.Sdk.Messages;
using Microsoft.Xrm.Sdk.Query;
using Spectre.Console.Testing;

namespace dgt.power.documenttemplates.tests;

public class DocumentTemplateImportEngineTests
{
    [Test]
    public async Task ImportAsync_PreCancelledNeverConnects()
    {
        using var cancellation = new CancellationTokenSource();
        await cancellation.CancelAsync();
        var connection = Mock.Of<IDataverseConnection>();
        connection.GetServiceAsync().Throws(new InvalidOperationException("Must not connect"));
        using var console = new TestConsole();
        var engine = new DocumentTemplateImportEngine(connection.Object, console);
        var token = cancellation.Token;
        await Assert.That(() => engine.ImportAsync("not-read.json", false, token)).Throws<OperationCanceledException>();
    }

    [Test]
    public async Task ImportAsync_EmptyDoesNotAuthenticate()
    {
        var directory = TemplateTestData.DirectoryPath();
        try
        {
            var connection = Mock.Of<IDataverseConnection>();
            connection.GetServiceAsync().Throws(new InvalidOperationException("Must not connect"));
            using var console = new TestConsole();
            var result = await new DocumentTemplateImportEngine(connection.Object, console).ImportAsync(
                TemplateTestData.WriteManifest(directory), false, CancellationToken.None);
            await Assert.That(result).IsTrue();
            await Assert.That(console.Output).Contains("0 document templates");
        }
        finally { Directory.Delete(directory, true); }
    }

    [Test]
    public async Task ImportAsync_AllLocalInputsValidatedBeforeConnection()
    {
        var directory = TemplateTestData.DirectoryPath();
        try
        {
            var first = TemplateTestData.Entry();
            var second = TemplateTestData.Entry("Second") with { File = "second.xlsx" };
            var path = TemplateTestData.WriteManifest(directory, first, second);
            await File.WriteAllTextAsync(Path.Combine(directory, second.File), "invalid");
            var connections = 0;
            var (service, state) = TemplateServiceProxy.Create();
            using var console = new TestConsole();
            var result = await new DocumentTemplateImportEngine(TemplateTestData.Connection(service, () => connections++), console)
                .ImportAsync(path, false, CancellationToken.None);
            await Assert.That(result).IsFalse();
            await Assert.That(connections).IsZero();
            await Assert.That(state.Writes).IsEmpty();
        }
        finally { Directory.Delete(directory, true); }
    }

    [Test]
    [Arguments(false)]
    [Arguments(true)]
    public async Task ImportAsync_CreateAndRepeatUsesSourceIdAndActivatesWithoutReplacing(bool word)
    {
        var directory = TemplateTestData.DirectoryPath();
        try
        {
            var entry = TemplateTestData.Entry(type: word ? "word" : "excel");
            var path = TemplateTestData.WriteManifest(directory, entry);
            var originalBytes = await File.ReadAllBytesAsync(Path.Combine(directory, entry.File));
            var (service, state) = TemplateServiceProxy.Create();
            foreach (var item in TemplateTestData.Metadata()) state.Metadata.Add(item.Key, item.Value);
            using var console = new TestConsole();
            var engine = new DocumentTemplateImportEngine(TemplateTestData.Connection(service), console);
            await Assert.That(await engine.ImportAsync(path, false, CancellationToken.None)).IsTrue();
            await Assert.That(await engine.ImportAsync(path, false, CancellationToken.None)).IsTrue();
            await Assert.That(state.Targets.Count).IsEqualTo(1);
            var target = state.Targets.Single();
            await Assert.That(target.Id).IsEqualTo(entry.SourceId);
            await Assert.That(target.GetAttributeValue<bool>("status")).IsFalse();
            await Assert.That(target.GetAttributeValue<int>("languagecode")).IsEqualTo(1031);
            await Assert.That(target.GetAttributeValue<string?>("description")).IsNull();
            var create = (CreateRequest)state.Writes.First();
            await Assert.That(create.Target.Contains("status")).IsFalse();
            var update = ((UpdateRequest)state.Writes[2]).Target;
            await Assert.That(update.Contains("documenttype")).IsFalse();
            await Assert.That(update.Contains("associatedentitytypecode")).IsFalse();
            if (!word)
                await Assert.That(Convert.ToBase64String(originalBytes)).IsEqualTo(target.GetAttributeValue<string>("content"));
        }
        finally { Directory.Delete(directory, true); }
    }

    [Test]
    [Arguments(false)]
    [Arguments(true)]
    public async Task ImportAsync_MatchedActiveOrDraftPreservesIdAndUnlistedRecords(bool draft)
    {
        var directory = TemplateTestData.DirectoryPath();
        try
        {
            var entry = TemplateTestData.Entry("EXAMPLE.docx");
            var path = TemplateTestData.WriteManifest(directory, entry);
            var (service, state) = TemplateServiceProxy.Create();
            foreach (var item in TemplateTestData.Metadata()) state.Metadata.Add(item.Key, item.Value);
            var targetId = Guid.NewGuid();
            state.Targets.Add(TemplateTestData.Target(entry with { Name = "example.docx" }, targetId, draft));
            var unlistedActive = TemplateTestData.Target(TemplateTestData.Entry("Unlisted active"), draft: false);
            var unlistedDraft = TemplateTestData.Target(TemplateTestData.Entry("Unlisted draft"));
            state.Targets.AddRange([unlistedActive, unlistedDraft]);
            using var console = new TestConsole();
            await Assert.That(await new DocumentTemplateImportEngine(TemplateTestData.Connection(service), console)
                .ImportAsync(path, false, CancellationToken.None)).IsTrue();
            await Assert.That(state.Targets[0].Id).IsEqualTo(targetId);
            await Assert.That(state.Targets[0].GetAttributeValue<string>("name")).IsEqualTo(entry.Name);
            await Assert.That(state.Targets[0].GetAttributeValue<bool>("status")).IsFalse();
            await Assert.That(unlistedDraft.GetAttributeValue<bool>("status")).IsTrue();
            await Assert.That(unlistedActive.GetAttributeValue<bool>("status")).IsFalse();
            await Assert.That(state.Writes.Count).IsEqualTo(2);
        }
        finally { Directory.Delete(directory, true); }
    }

    [Test]
    [Arguments("sourceId")]
    [Arguments("ambiguous")]
    [Arguments("renamed")]
    [Arguments("changedType")]
    [Arguments("changedEntity")]
    public async Task ImportAsync_IdentityConflictsFailBeforeWrites(string kind)
    {
        var directory = TemplateTestData.DirectoryPath();
        try
        {
            var entry = TemplateTestData.Entry();
            var path = TemplateTestData.WriteManifest(directory, entry);
            var (service, state) = TemplateServiceProxy.Create();
            foreach (var item in TemplateTestData.Metadata()) state.Metadata.Add(item.Key, item.Value);
            switch (kind)
            {
                case "sourceId": state.Targets.Add(TemplateTestData.Target(entry with { Name = "Other" })); break;
                case "renamed": state.Targets.Add(TemplateTestData.Target(entry with { Name = "Old name" })); break;
                case "changedType": state.Targets.Add(TemplateTestData.Target(entry with { DocumentType = "word" })); break;
                case "changedEntity": state.Targets.Add(TemplateTestData.Target(entry with { Entity = "contact" })); break;
                case "ambiguous":
                    state.Targets.Add(TemplateTestData.Target(entry, Guid.NewGuid()));
                    state.Targets.Add(TemplateTestData.Target(entry, Guid.NewGuid()));
                    break;
            }
            using var console = new TestConsole();
            await Assert.That(await new DocumentTemplateImportEngine(TemplateTestData.Connection(service), console)
                .ImportAsync(path, false, CancellationToken.None)).IsFalse();
            await Assert.That(state.Writes).IsEmpty();
        }
        finally { Directory.Delete(directory, true); }
    }

    [Test]
    public async Task ImportAsync_LaterTargetFailureLeavesEarlierPreparedUntouchedAndStops()
    {
        var directory = TemplateTestData.DirectoryPath();
        try
        {
            var first = TemplateTestData.Entry();
            var missing = TemplateTestData.Entry("Second", entity: "missing") with { File = "second.xlsx" };
            var third = TemplateTestData.Entry("Third", entity: "unvisited") with { File = "third.xlsx" };
            var path = TemplateTestData.WriteManifest(directory, first, missing, third);
            var (service, state) = TemplateServiceProxy.Create();
            foreach (var item in TemplateTestData.Metadata()) state.Metadata.Add(item.Key, item.Value);
            using var console = new TestConsole();
            await Assert.That(await new DocumentTemplateImportEngine(TemplateTestData.Connection(service), console)
                .ImportAsync(path, false, CancellationToken.None)).IsFalse();
            await Assert.That(state.Writes).IsEmpty();
            await Assert.That(string.Join(",", state.MetadataReads)).IsEqualTo("account,missing");
        }
        finally { Directory.Delete(directory, true); }
    }

    [Test]
    public async Task ImportAsync_DryRunUsesSameMetadataAndPreparationWithoutWrites()
    {
        var directory = TemplateTestData.DirectoryPath();
        try
        {
            var path = TemplateTestData.WriteManifest(directory, TemplateTestData.Entry(type: "word"));
            var (service, state) = TemplateServiceProxy.Create();
            foreach (var item in TemplateTestData.Metadata()) state.Metadata.Add(item.Key, item.Value);
            using var console = new TestConsole();
            await Assert.That(await new DocumentTemplateImportEngine(TemplateTestData.Connection(service), console)
                .ImportAsync(path, true, CancellationToken.None)).IsTrue();
            await Assert.That(state.MetadataReads.Contains("contact")).IsTrue();
            await Assert.That(state.Writes).IsEmpty();
            await Assert.That(console.Output).Contains("DRY RUN: create");
            await Assert.That(console.Output).Contains("activate");
            state.Metadata["contact"].SetAttributeCollection([]);
            await Assert.That(await new DocumentTemplateImportEngine(TemplateTestData.Connection(service), console)
                .ImportAsync(path, true, CancellationToken.None)).IsFalse();
            await Assert.That(state.Writes).IsEmpty();
        }
        finally { Directory.Delete(directory, true); }
    }

    [Test]
    [Arguments(false)]
    [Arguments(true)]
    public async Task ExecuteAsync_WriteOrActivationFailureContinuesIndependentItemAndFailsOverall(bool activation)
    {
        var (service, state) = TemplateServiceProxy.Create();
        var first = TemplateTestData.Entry("First");
        var second = TemplateTestData.Entry("Second");
        state.RejectWrite = entity => entity.Id == first.SourceId && entity.Contains("status") == activation;
        var content = TemplateTestData.Fixture("Accounts.xlsx");
        using var console = new TestConsole();
        var result = await new DocumentTemplateImportEngine(TemplateTestData.Connection(service), console).ExecuteAsync(
            [new(first, first.SourceId, true, content), new(second, second.SourceId, true, content)], service, CancellationToken.None);
        await Assert.That(result).IsFalse();
        await Assert.That(state.Targets.Single(target => target.Id == second.SourceId).GetAttributeValue<bool>("status")).IsFalse();
        await Assert.That(state.Writes.Count).IsEqualTo(activation ? 4 : 3);
        await Assert.That(console.Output).Contains("Failed 'First'");
        await Assert.That(console.Output).Contains(activation ? "Partial changes" : "Activation was not attempted");
        await Assert.That(console.Output).Contains("No rollback");
    }

    [Test]
    public async Task ExecuteAsync_CancellationStopsFurtherWorkAndPropagates()
    {
        using var cancellation = new CancellationTokenSource();
        var (service, state) = TemplateServiceProxy.Create();
        state.AfterWrite = CancelAfterWrite(cancellation);
        var first = TemplateTestData.Entry("First");
        var second = TemplateTestData.Entry("Second");
        var content = TemplateTestData.Fixture("Accounts.xlsx");
        using var console = new TestConsole();
        var engine = new DocumentTemplateImportEngine(TemplateTestData.Connection(service), console);
        var token = cancellation.Token;
        await Assert.That(() => engine.ExecuteAsync(
            [new(first, first.SourceId, true, content), new(second, second.SourceId, true, content)], service, token))
            .Throws<OperationCanceledException>();
        await Assert.That(state.Writes.Count).IsEqualTo(1);
        await Assert.That(state.Targets.Count).IsEqualTo(1);
    }

    private static Action<Entity> CancelAfterWrite(CancellationTokenSource cancellation) => _ => cancellation.Cancel();

    [Test]
    public async Task ReadAllAsync_FollowsPagingCookies()
    {
        var (service, state) = TemplateServiceProxy.Create();
        state.ReadPage = query => new EntityCollection([new Entity("documenttemplate", Guid.NewGuid())])
        {
            MoreRecords = query.PageInfo.PageNumber < 3,
            PagingCookie = $"cookie-{query.PageInfo.PageNumber}"
        };
        var result = await DocumentTemplateReader.ReadAllAsync(service, new QueryExpression("documenttemplate"), CancellationToken.None);
        await Assert.That(result.Count).IsEqualTo(3);
        await Assert.That(string.Join("|", state.Pages.Select(page => $"{page.Page}:{page.Cookie}")))
            .IsEqualTo("1:|2:cookie-1|3:cookie-2");
    }
}
