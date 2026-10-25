// Copyright (c) DIGITALL Nature. All rights reserved

using dgt.power.teamtemplates.Commands;
using dgt.power.teamtemplates.Configuration;
using dgt.power.teamtemplates.tests.Logic;
using Spectre.Console.Testing;

namespace dgt.power.teamtemplates.tests.Commands;

public class TeamTemplateExportCommandTests
{
    [Test]
    public async Task Export_FilteredVersionedData_IsImportableAndExcludesSystem()
    {
        using var fixture = new TeamTemplateCommandFixture();
        using var console = new TestConsole();
        var entry = TeamTemplateTransferTests.Entry();
        var system = TeamTemplateTransferTests.Target(TeamTemplateTransferTests.Entry(name: "System"));
        system["issystem"] = true;
        var context = fixture.Builder<TeamTemplateExportCommand, TeamTemplateExportSettings>(console).WithData([
            TeamTemplateTransferTests.Target(entry), system,
            TeamTemplateTransferTests.Target(TeamTemplateTransferTests.Entry(table: "contact"), 10001)
        ]).Build();
        var path = fixture.PathFor("export.json");
        await Assert.That(context.Execute(new TeamTemplateExportSettings { File = path, Tables = ["account"] })).IsTrue();
        var configuration = TeamTemplateConfiguration.Parse(await File.ReadAllTextAsync(path));
        await Assert.That(configuration.Templates.Single()).IsEqualTo(entry);
        await Assert.That(configuration.SchemaVersion).IsEqualTo(1);
    }

    [Test]
    public async Task Export_Empty_ProducesEmptyEnvelope()
    {
        using var fixture = new TeamTemplateCommandFixture();
        using var console = new TestConsole();
        var context = fixture.Builder<TeamTemplateExportCommand, TeamTemplateExportSettings>(console).Build();
        var path = fixture.PathFor("empty.json");
        await Assert.That(context.Execute(new TeamTemplateExportSettings { File = path })).IsTrue();
        await Assert.That(TeamTemplateConfiguration.Parse(await File.ReadAllTextAsync(path)).Templates).IsEmpty();
    }

    [Test]
    public async Task Export_ExistingFile_RequiresOverwrite()
    {
        using var fixture = new TeamTemplateCommandFixture();
        using var console = new TestConsole();
        var path = fixture.Write("original");
        var context = fixture.Builder<TeamTemplateExportCommand, TeamTemplateExportSettings>(console).Build();
        await Assert.That(context.Execute(new TeamTemplateExportSettings { File = path })).IsFalse();
        await Assert.That(await File.ReadAllTextAsync(path)).IsEqualTo("original");
        await Assert.That(fixture.AuthCalls).IsZero();
        await Assert.That(context.Execute(new TeamTemplateExportSettings { File = path, Overwrite = true })).IsTrue();
        await Assert.That(TeamTemplateConfiguration.Parse(await File.ReadAllTextAsync(path)).Templates).IsEmpty();
    }

    [Test]
    [Arguments(false)]
    [Arguments(true)]
    public async Task Export_DryRun_DoesNotCreateOrReplace(bool exists)
    {
        using var fixture = new TeamTemplateCommandFixture();
        using var console = new TestConsole();
        var path = exists ? fixture.Write("original") : fixture.PathFor("absent.json");
        var context = fixture.Builder<TeamTemplateExportCommand, TeamTemplateExportSettings>(console).Build();
        await Assert.That(context.Execute(new TeamTemplateExportSettings { File = path, DryRun = true, Overwrite = exists })).IsTrue();
        await Assert.That(File.Exists(path)).IsEqualTo(exists);
        if (exists)
        {
            await Assert.That(await File.ReadAllTextAsync(path)).IsEqualTo("original");
        }
        else
        {
            await Assert.That(Directory.Exists(fixture.DirectoryPath)).IsFalse();
        }
        await Assert.That(console.Output).Contains("DRY RUN");
    }

    [Test]
    public async Task Export_InvalidTable_DoesNotReplace()
    {
        using var fixture = new TeamTemplateCommandFixture();
        using var console = new TestConsole();
        var path = fixture.Write("original");
        var context = fixture.Builder<TeamTemplateExportCommand, TeamTemplateExportSettings>(console).Build();
        await Assert.That(context.Execute(new TeamTemplateExportSettings { File = path, Overwrite = true, Tables = ["unknown"] })).IsFalse();
        await Assert.That(await File.ReadAllTextAsync(path)).IsEqualTo("original");
    }

    [Test]
    public async Task Export_InvalidSourceRights_DoesNotReplace()
    {
        using var fixture = new TeamTemplateCommandFixture();
        using var console = new TestConsole();
        var path = fixture.Write("original");
        var template = TeamTemplateTransferTests.Target(TeamTemplateTransferTests.Entry());
        template["defaultaccessrightsmask"] = 32;
        var context = fixture.Builder<TeamTemplateExportCommand, TeamTemplateExportSettings>(console).WithData(template).Build();
        await Assert.That(context.Execute(new TeamTemplateExportSettings { File = path, Overwrite = true })).IsFalse();
        await Assert.That(await File.ReadAllTextAsync(path)).IsEqualTo("original");
    }
}
