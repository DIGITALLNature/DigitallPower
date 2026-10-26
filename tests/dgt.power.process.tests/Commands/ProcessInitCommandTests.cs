// Copyright (c) DIGITALL Nature. All rights reserved

using dgt.power.common;
using dgt.power.dataverse;
using dgt.power.process.Commands;
using dgt.power.process.Configuration;
using dgt.power.tests;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Microsoft.Xrm.Sdk;
using Spectre.Console.Testing;

namespace dgt.power.process.tests.Commands;

public class ProcessInitCommandTests
{
    [Test]
    public async Task Init_OwnerCaptureWritesMappedKeyAndInformationalName()
    {
        using var fixture = new ProcessInitFixture();
        var mappingPath = fixture.WriteMapping(
            """{"schemaVersion":1,"owners":{"integrationUser":{"kind":"user","domainName":"owner@example.org"}}}""");
        var outputPath = fixture.PathFor("capture.json");
        using var console = new TestConsole();
        var context = fixture.CreateCommandContext(console);

        var succeeded = context.Execute(ProcessInitFixture.Settings(outputPath, mappingPath));
        var result = ProcessConfiguration.Load(outputPath);

        await Assert.That(succeeded).IsTrue();
        await Assert.That(result.Processes).Count().IsEqualTo(1);
        await Assert.That(result.Processes[0].Owner).IsEqualTo("integrationUser");
        await Assert.That(result.Processes[0].Name).IsEqualTo("Process for capture");
        await Assert.That(result.Processes[0].Active).IsTrue();
    }

    [Test]
    [Arguments("""{"schemaVersion":1,"owners":{"different":{"kind":"user","domainName":"other@example.org"}}}""")]
    [Arguments("""{"schemaVersion":1,"owners":{"first":{"kind":"user","domainName":"owner@example.org"},"second":{"kind":"user","domainName":"OWNER@example.org"}}}""")]
    public async Task Init_UnmappedOrAmbiguousOwnerLeavesExistingOutputUnchanged(string mappingJson)
    {
        using var fixture = new ProcessInitFixture();
        var mappingPath = fixture.WriteMapping(mappingJson);
        var outputPath = fixture.PathFor("existing.json");
        await File.WriteAllTextAsync(outputPath, "preserve this output");
        using var console = new TestConsole();
        var context = fixture.CreateCommandContext(console);

        var succeeded = context.Execute(ProcessInitFixture.Settings(outputPath, mappingPath, overwrite: true));

        await Assert.That(succeeded).IsFalse();
        await Assert.That(await File.ReadAllTextAsync(outputPath)).IsEqualTo("preserve this output");
    }

    [Test]
    public async Task Init_PlatformOwnedProcessIsCapturedWithoutOwnerAndWarns()
    {
        using var fixture = new ProcessInitFixture();
        var mappingPath = fixture.WriteMapping(
            """{"schemaVersion":1,"owners":{"other":{"kind":"user","domainName":"other@example.org"}}}""");
        var outputPath = fixture.PathFor("platform.json");
        using var console = new TestConsole();
        console.Profile.Width = 1000;
        var context = fixture.CreateCommandContext(console, platformOwner: true, includeOtherUser: true);

        var succeeded = context.Execute(ProcessInitFixture.Settings(outputPath, mappingPath));
        var result = ProcessConfiguration.Load(outputPath);

        await Assert.That(succeeded).IsTrue();
        await Assert.That(result.Processes[0].Owner).IsNull();
        await Assert.That(result.Processes[0].Active).IsTrue();
        await Assert.That(console.Output).Contains($"owned by platform principal SYSTEM systemuser({fixture.OwnerUserId:D}): {fixture.ProcessId:D}");
    }

    [Test]
    public async Task Init_SkipUnmappedOwnersOmitsOwnerOnlyEntriesAndWarns()
    {
        using var fixture = new ProcessInitFixture();
        var mappingPath = fixture.WriteMapping(
            """{"schemaVersion":1,"owners":{"other":{"kind":"user","domainName":"other@example.org"}}}""");
        var outputPath = fixture.PathFor("skip.json");
        using var console = new TestConsole();
        console.Profile.Width = 1000;
        var context = fixture.CreateCommandContext(console, includeOtherUser: true);

        var succeeded = context.Execute(ProcessInitFixture.Settings(outputPath, mappingPath, capture: "owner", unmappedOwners: "skip"));

        await Assert.That(succeeded).IsTrue();
        await Assert.That(ProcessConfiguration.Load(outputPath).Processes).IsEmpty();
        await Assert.That(console.Output).Contains($"owned by unmapped principal systemuser({fixture.OwnerUserId:D}): {fixture.ProcessId:D}");
        await Assert.That(console.Output).Contains("Captured 0 processes");
    }

    [Test]
    public async Task Init_UnknownSolutionDoesNotCreateOrReplaceOutput()
    {
        using var fixture = new ProcessInitFixture();
        var outputPath = fixture.PathFor("existing.json");
        await File.WriteAllTextAsync(outputPath, "preserve this output");
        using var console = new TestConsole();
        var context = fixture.CreateCommandContext(console, includeSolution: false);

        var succeeded = context.Execute(ProcessInitFixture.Settings(outputPath, capture: "state", overwrite: true));

        await Assert.That(succeeded).IsFalse();
        await Assert.That(await File.ReadAllTextAsync(outputPath)).IsEqualTo("preserve this output");
    }

    [Test]
    public async Task Init_ExistingOutputWithoutOverwriteFailsUnchanged()
    {
        using var fixture = new ProcessInitFixture();
        var outputPath = fixture.PathFor("existing.json");
        await File.WriteAllTextAsync(outputPath, "preserve this output");
        using var console = new TestConsole();
        var context = fixture.CreateCommandContext(console);

        var succeeded = context.Execute(ProcessInitFixture.Settings(outputPath, capture: "state"));

        await Assert.That(succeeded).IsFalse();
        await Assert.That(await File.ReadAllTextAsync(outputPath)).IsEqualTo("preserve this output");
    }

    [Test]
    public async Task Init_EmptySelectionWritesEmptyProcessArray()
    {
        using var fixture = new ProcessInitFixture();
        var outputPath = fixture.PathFor("empty.json");
        using var console = new TestConsole();
        var context = fixture.CreateCommandContext(console, includeProcess: false);

        var succeeded = context.Execute(ProcessInitFixture.Settings(outputPath, capture: "state"));

        await Assert.That(succeeded).IsTrue();
        await Assert.That(ProcessConfiguration.Load(outputPath).Processes).IsEmpty();
    }

    [Test]
    public async Task Init_OutputWriteFailureIsReturnedWithoutLeakingTemporaryFile()
    {
        using var fixture = new ProcessInitFixture();
        var outputPath = fixture.PathFor("blocked-output");
        Directory.CreateDirectory(outputPath);
        using var console = new TestConsole();
        var context = fixture.CreateCommandContext(console);

        var succeeded = context.Execute(ProcessInitFixture.Settings(outputPath, capture: "state", overwrite: true));

        await Assert.That(succeeded).IsFalse();
        await Assert.That(Directory.EnumerateFiles(fixture.DirectoryPath, ".blocked-output.*.tmp")).IsEmpty();
    }

    private sealed class ProcessInitFixture : IDisposable
    {
        private readonly string _directory = Path.Combine(Environment.CurrentDirectory, $"process-init-{Guid.NewGuid():N}");
        private readonly Guid _solutionId = Guid.NewGuid();

        public ProcessInitFixture()
        {
            Directory.CreateDirectory(_directory);
        }

        public Guid ProcessId { get; } = Guid.NewGuid();

        public Guid OwnerUserId { get; } = Guid.NewGuid();

        public string DirectoryPath => _directory;

        public string PathFor(string name) => Path.Combine(_directory, name);

        public string WriteMapping(string json)
        {
            var path = PathFor("owners.json");
            File.WriteAllText(path, json);
            return path;
        }

        public static ProcessInitSettings Settings(
            string output,
            string? mapping = null,
            string capture = "both",
            bool overwrite = false,
            string unmappedOwners = "fail") => new()
        {
            File = output,
            Solutions = ["Core"],
            Capture = capture,
            OwnerMapping = mapping,
            UnmappedOwners = unmappedOwners,
            Overwrite = overwrite
        };

        public CommandTestContext<ProcessInitCommand, ProcessInitSettings> CreateCommandContext(
            TestConsole console,
            bool includeSolution = true,
            bool includeProcess = true,
            bool platformOwner = false,
            bool includeOtherUser = false)
        {
            var services = new TestServiceCollection();
            services.RemoveAll<IDataverseConnection>();
            services.AddSingleton<IDataverseConnection, TestConnection>();
            var data = new List<Entity>();
            if (includeSolution)
            {
                data.Add(new Entity("solution", _solutionId) { ["uniquename"] = "Core" });
            }

            if (includeProcess)
            {
                data.AddRange(
                [
                    new Workflow(ProcessId)
                    {
                        Name = "Process for capture",
                        Type = new OptionSetValue(Workflow.Options.Type.Definition),
                        Category = new OptionSetValue(Workflow.Options.Category.Workflow),
                        StateCode = new OptionSetValue(Workflow.Options.StateCode.Activated),
                        OwnerId = new EntityReference("systemuser", OwnerUserId)
                    },
                    new Entity("solutioncomponent", Guid.NewGuid())
                    {
                        ["objectid"] = ProcessId,
                        ["componenttype"] = new OptionSetValue(SolutionComponent.Options.ComponentType.Workflow),
                        ["solutionid"] = new EntityReference("solution", _solutionId)
                    },
                    platformOwner
                        ? new Entity("systemuser", OwnerUserId)
                        {
                            ["fullname"] = "SYSTEM",
                            ["isdisabled"] = true
                        }
                        : new Entity("systemuser", OwnerUserId)
                        {
                            ["domainname"] = "owner@example.org",
                            ["isdisabled"] = false
                        }
                ]);
            }

            if (includeOtherUser)
            {
                data.Add(new Entity("systemuser", Guid.NewGuid())
                {
                    ["domainname"] = "other@example.org",
                    ["isdisabled"] = false
                });
            }

            return new CommandTestContextBuilder<ProcessInitCommand, ProcessInitSettings>()
                .WithServiceCollection(services)
                .WithAnsiConsole(console)
                .WithData(data)
                .Build();
        }

        public void Dispose()
        {
            if (Directory.Exists(_directory))
            {
                Directory.Delete(_directory, recursive: true);
            }
        }
    }
}
