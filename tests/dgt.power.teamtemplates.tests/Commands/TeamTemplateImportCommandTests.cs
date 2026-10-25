// Copyright (c) DIGITALL Nature. All rights reserved

using dgt.power.dataverse;
using dgt.power.teamtemplates.Commands;
using dgt.power.teamtemplates.tests.Logic;
using Spectre.Console.Testing;

namespace dgt.power.teamtemplates.tests.Commands;

public class TeamTemplateImportCommandTests
{
    [Test]
    [Arguments("[]", false, false)]
    [Arguments("{", false, false)]
    [Arguments("""{"schemaVersion":1,"templates":[]}""", false, true)]
    [Arguments("""{"schemaVersion":1,"templates":[]}""", true, false)]
    public async Task Import_LocalInput_RejectsOrNoOpsBeforeAuth(string json, bool cleanup, bool expected)
    {
        using var fixture = new TeamTemplateCommandFixture();
        using var console = new TestConsole();
        var context = fixture.Builder<TeamTemplateImportCommand, TeamTemplateImportSettings>(console).Build();
        var result = context.Execute(new TeamTemplateImportSettings
        {
            File = fixture.Write(json), Cleanup = cleanup, CleanupTables = cleanup ? ["account"] : null
        });
        await Assert.That(result).IsEqualTo(expected);
        await Assert.That(fixture.AuthCalls).IsZero();
    }

    [Test]
    public async Task Import_MissingFile_FailsWithoutAuth()
    {
        using var fixture = new TeamTemplateCommandFixture();
        using var console = new TestConsole();
        var context = fixture.Builder<TeamTemplateImportCommand, TeamTemplateImportSettings>(console).Build();
        await Assert.That(context.Execute(new TeamTemplateImportSettings { File = fixture.PathFor("missing.json") })).IsFalse();
        await Assert.That(fixture.AuthCalls).IsZero();
        await Assert.That(console.Output).Contains("Invalid teamtemplate input");
    }

    [Test]
    public async Task Import_CreateThenRepeat_PreservesGuidAndSkipsUpdates()
    {
        using var fixture = new TeamTemplateCommandFixture();
        using var console = new TestConsole();
        var entry = TeamTemplateTransferTests.Entry();
        var missing = TeamTemplateTransferTests.Target(TeamTemplateTransferTests.Entry(name: "Unlisted"));
        var context = fixture.Builder<TeamTemplateImportCommand, TeamTemplateImportSettings>(console).WithData(missing).Build();
        var settings = new TeamTemplateImportSettings { File = fixture.Write(entry) };
        await Assert.That(context.Execute(settings)).IsTrue();
        await Assert.That(context.Get<TeamTemplate>()).Count().IsEqualTo(2);
        await Assert.That(context.GetById<TeamTemplate>(entry.Id).ObjectTypeCode).IsEqualTo(10000);
        console.Clear(false);
        await Assert.That(context.Execute(settings)).IsTrue();
        await Assert.That(console.Output).Contains("0 create, 0 update, 1 unchanged, 0 delete");
        await Assert.That(console.Output).Contains($"unchanged {entry.Id:D}");
    }

    [Test]
    [Arguments(false)]
    [Arguments(true)]
    public async Task Import_LaterConflict_PreventsAllWritesAndCleanup(bool dryRun)
    {
        using var fixture = new TeamTemplateCommandFixture();
        using var console = new TestConsole();
        var conflict = TeamTemplateTransferTests.Entry(name: "Conflict");
        var target = TeamTemplateTransferTests.Target(conflict with { Id = Guid.NewGuid() });
        var missing = TeamTemplateTransferTests.Target(TeamTemplateTransferTests.Entry(name: "Missing"));
        var context = fixture.Builder<TeamTemplateImportCommand, TeamTemplateImportSettings>(console).WithData([target, missing]).Build();
        var first = TeamTemplateTransferTests.Entry(name: "Valid new template");
        await Assert.That(context.Execute(new TeamTemplateImportSettings
        {
            File = fixture.Write(first, conflict), Cleanup = true, CleanupTables = ["account"], DryRun = dryRun
        })).IsFalse();
        await Assert.That(context.Get<TeamTemplate>()).Count().IsEqualTo(2);
        await Assert.That(context.Get<TeamTemplate>().Any(template => template.Id == first.Id)).IsFalse();
        await Assert.That(context.GetById<TeamTemplate>(missing.Id).TeamTemplateName).IsEqualTo("Missing");
    }

    [Test]
    [Arguments(false)]
    [Arguments(true)]
    public async Task Import_CleanupScope_ProtectsOutsideAndSystem(bool dryRun)
    {
        using var fixture = new TeamTemplateCommandFixture();
        using var console = new TestConsole();
        var missing = TeamTemplateTransferTests.Target(TeamTemplateTransferTests.Entry(name: "Missing"));
        var system = TeamTemplateTransferTests.Target(TeamTemplateTransferTests.Entry(name: "System"));
        system["issystem"] = true;
        var outside = TeamTemplateTransferTests.Target(TeamTemplateTransferTests.Entry(table: "contact", name: "Outside"), 10001);
        var entry = TeamTemplateTransferTests.Entry(table: "contact");
        var context = fixture.Builder<TeamTemplateImportCommand, TeamTemplateImportSettings>(console).WithData([missing, system, outside]).Build();
        await Assert.That(context.Execute(new TeamTemplateImportSettings
        {
            File = fixture.Write(entry), Cleanup = true, CleanupTables = ["account"], DryRun = dryRun
        })).IsTrue();
        await Assert.That(context.Get<TeamTemplate>().Any(template => template.Id == missing.Id)).IsEqualTo(dryRun);
        await Assert.That(context.Get<TeamTemplate>().Any(template => template.Id == entry.Id)).IsEqualTo(!dryRun);
        await Assert.That(context.GetById<TeamTemplate>(system.Id).IsSystem).IsTrue();
        await Assert.That(context.GetById<TeamTemplate>(outside.Id).TeamTemplateName).IsEqualTo("Outside");
    }

    [Test]
    [Arguments(null)]
    [Arguments("*")]
    [Arguments("all")]
    [Arguments("")]
    [Arguments("account,contact")]
    public async Task Import_InvalidCleanupScope_FailsWithoutWrites(string? scope)
    {
        using var fixture = new TeamTemplateCommandFixture();
        using var console = new TestConsole();
        var entry = TeamTemplateTransferTests.Entry();
        var context = fixture.Builder<TeamTemplateImportCommand, TeamTemplateImportSettings>(console).Build();
        var result = context.Execute(new TeamTemplateImportSettings
        {
            File = fixture.Write(entry), Cleanup = true, CleanupTables = scope is null ? null : [scope]
        });
        await Assert.That(result).IsFalse();
        await Assert.That(context.Get<TeamTemplate>()).IsEmpty();
        if (scope != "all")
        {
            await Assert.That(fixture.AuthCalls).IsZero();
        }
    }

    [Test]
    public async Task Import_Update_ClearsDescription()
    {
        using var fixture = new TeamTemplateCommandFixture();
        using var console = new TestConsole();
        var entry = TeamTemplateTransferTests.Entry();
        var existing = TeamTemplateTransferTests.Target(entry);
        existing["description"] = "Old description";
        var context = fixture.Builder<TeamTemplateImportCommand, TeamTemplateImportSettings>(console).WithData(existing).Build();
        await Assert.That(context.Execute(new TeamTemplateImportSettings { File = fixture.Write(entry with { Name = "Renamed" }) })).IsTrue();
        var actual = context.GetById<TeamTemplate>(entry.Id);
        await Assert.That(actual.Description).IsNull();
        await Assert.That(actual.TeamTemplateName).IsEqualTo("Renamed");
    }
}
