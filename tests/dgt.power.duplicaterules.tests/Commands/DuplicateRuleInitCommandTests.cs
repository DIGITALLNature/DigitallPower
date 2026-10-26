// Copyright (c) DIGITALL Nature. All rights reserved

using dgt.power.common;
using dgt.power.dataverse;
using dgt.power.duplicaterules.Commands;
using dgt.power.duplicaterules.Configuration;
using dgt.power.tests;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Microsoft.Xrm.Sdk;
using Spectre.Console.Testing;

namespace dgt.power.duplicaterules.tests.Commands;

public class DuplicateRuleInitCommandTests
{
    [Test]
    public async Task Init_BothCaptureWritesStateOwnerKeyAndInformationalName()
    {
        using var fixture = new DuplicateRuleInitFixture();
        var mappingPath = fixture.WriteMapping(
            """{"schemaVersion":1,"owners":{"integrationUser":{"kind":"user","domainName":"owner@example.org"}}}""");
        var outputPath = fixture.PathFor("capture.json");
        using var console = new TestConsole();
        console.Profile.Width = 1000;
        var context = fixture.CreateCommandContext(console);

        var succeeded = context.Execute(DuplicateRuleInitFixture.Settings(outputPath, mappingPath, capture: "both"));
        var result = DuplicateRuleConfiguration.Load(outputPath);

        await Assert.That(succeeded).IsTrue();
        await Assert.That(result.Rules).Count().IsEqualTo(1);
        await Assert.That(result.Rules[0].RuleId).IsEqualTo(fixture.RuleId);
        await Assert.That(result.Rules[0].Name).IsEqualTo("Contacts with the same e-mail address");
        await Assert.That(result.Rules[0].Published).IsTrue();
        await Assert.That(result.Rules[0].Owner).IsEqualTo("integrationUser");
        await Assert.That(console.Output).Contains("Captured 1 duplicate rule(s)");
    }

    [Test]
    public async Task Init_StateCaptureOmitsOwner()
    {
        using var fixture = new DuplicateRuleInitFixture();
        var outputPath = fixture.PathFor("state.json");
        using var console = new TestConsole();
        console.Profile.Width = 1000;
        var context = fixture.CreateCommandContext(console);

        var succeeded = context.Execute(DuplicateRuleInitFixture.Settings(outputPath));
        var result = DuplicateRuleConfiguration.Load(outputPath);

        await Assert.That(succeeded).IsTrue();
        await Assert.That(result.Rules[0].Published).IsTrue();
        await Assert.That(result.Rules[0].Owner).IsNull();
    }

    [Test]
    public async Task Init_UnmappedOwnerLeavesExistingOutputUnchanged()
    {
        using var fixture = new DuplicateRuleInitFixture();
        var mappingPath = fixture.WriteMapping(
            """{"schemaVersion":1,"owners":{"different":{"kind":"user","domainName":"other@example.org"}}}""");
        var outputPath = fixture.PathFor("existing.json");
        await File.WriteAllTextAsync(outputPath, "preserve this output");
        using var console = new TestConsole();
        console.Profile.Width = 1000;
        var context = fixture.CreateCommandContext(console);

        var succeeded = context.Execute(DuplicateRuleInitFixture.Settings(outputPath, mappingPath, capture: "owner", overwrite: true));

        await Assert.That(succeeded).IsFalse();
        await Assert.That(await File.ReadAllTextAsync(outputPath)).IsEqualTo("preserve this output");
    }

    [Test]
    public async Task Init_PlatformOwnedRuleIsCapturedWithoutOwnerAndWarns()
    {
        using var fixture = new DuplicateRuleInitFixture();
        var mappingPath = fixture.WriteMapping(
            """{"schemaVersion":1,"owners":{"integrationUser":{"kind":"user","domainName":"owner@example.org"}}}""");
        var outputPath = fixture.PathFor("platform.json");
        using var console = new TestConsole();
        console.Profile.Width = 1000;
        var context = fixture.CreateCommandContext(console, includeSystemRule: true);

        var succeeded = context.Execute(DuplicateRuleInitFixture.Settings(outputPath, mappingPath, capture: "both"));
        var result = DuplicateRuleConfiguration.Load(outputPath);

        await Assert.That(succeeded).IsTrue();
        await Assert.That(result.Rules).Count().IsEqualTo(2);
        var mapped = result.Rules.Single(rule => rule.RuleId == fixture.RuleId);
        var platform = result.Rules.Single(rule => rule.RuleId == fixture.SystemRuleId);
        await Assert.That(mapped.Owner).IsEqualTo("integrationUser");
        await Assert.That(platform.Owner).IsNull();
        await Assert.That(platform.Published).IsTrue();
        await Assert.That(console.Output).Contains($"owned by platform principal SYSTEM systemuser({fixture.SystemUserId:D}): {fixture.SystemRuleId:D}");
        await Assert.That(console.Output).Contains("Captured 2 duplicate rule(s)");
    }

    [Test]
    public async Task Init_OwnerOnlyCaptureOmitsPlatformOwnedRule()
    {
        using var fixture = new DuplicateRuleInitFixture();
        var mappingPath = fixture.WriteMapping(
            """{"schemaVersion":1,"owners":{"integrationUser":{"kind":"user","domainName":"owner@example.org"}}}""");
        var outputPath = fixture.PathFor("owner.json");
        using var console = new TestConsole();
        console.Profile.Width = 1000;
        var context = fixture.CreateCommandContext(console, includeSystemRule: true);

        var succeeded = context.Execute(DuplicateRuleInitFixture.Settings(outputPath, mappingPath, capture: "owner"));
        var result = DuplicateRuleConfiguration.Load(outputPath);

        await Assert.That(succeeded).IsTrue();
        await Assert.That(result.Rules).Count().IsEqualTo(1);
        await Assert.That(result.Rules[0].RuleId).IsEqualTo(fixture.RuleId);
        await Assert.That(console.Output).Contains("Captured 1 duplicate rule(s)");
    }

    [Test]
    public async Task Init_SkipUnmappedOwnersOmitsOwnerAndWarns()
    {
        using var fixture = new DuplicateRuleInitFixture();
        var mappingPath = fixture.WriteMapping(
            """{"schemaVersion":1,"owners":{"different":{"kind":"user","domainName":"other@example.org"}}}""");
        var outputPath = fixture.PathFor("skip.json");
        using var console = new TestConsole();
        console.Profile.Width = 1000;
        var context = fixture.CreateCommandContext(console, includeOtherUser: true);

        var succeeded = context.Execute(DuplicateRuleInitFixture.Settings(outputPath, mappingPath, capture: "both", unmappedOwners: "skip"));
        var result = DuplicateRuleConfiguration.Load(outputPath);

        await Assert.That(succeeded).IsTrue();
        await Assert.That(result.Rules[0].Published).IsTrue();
        await Assert.That(result.Rules[0].Owner).IsNull();
        await Assert.That(console.Output).Contains($"owned by unmapped principal systemuser({fixture.OwnerId:D}): {fixture.RuleId:D}");
    }

    [Test]
    public async Task Init_MissingRuleIdLeavesExistingOutputUnchanged()
    {
        using var fixture = new DuplicateRuleInitFixture();
        var outputPath = fixture.PathFor("existing.json");
        await File.WriteAllTextAsync(outputPath, "preserve this output");
        using var console = new TestConsole();
        console.Profile.Width = 1000;
        var context = fixture.CreateCommandContext(console);
        var settings = new DuplicateRuleInitSettings
        {
            File = outputPath,
            RuleIds = [Guid.NewGuid().ToString("D")],
            Overwrite = true
        };

        var succeeded = context.Execute(settings);

        await Assert.That(succeeded).IsFalse();
        await Assert.That(console.Output).Contains("were not found");
        await Assert.That(await File.ReadAllTextAsync(outputPath)).IsEqualTo("preserve this output");
    }

    [Test]
    public async Task Init_ExistingOutputWithoutOverwriteFailsUnchanged()
    {
        using var fixture = new DuplicateRuleInitFixture();
        var outputPath = fixture.PathFor("existing.json");
        await File.WriteAllTextAsync(outputPath, "preserve this output");
        using var console = new TestConsole();
        console.Profile.Width = 1000;
        var context = fixture.CreateCommandContext(console);

        var succeeded = context.Execute(DuplicateRuleInitFixture.Settings(outputPath));

        await Assert.That(succeeded).IsFalse();
        await Assert.That(await File.ReadAllTextAsync(outputPath)).IsEqualTo("preserve this output");
    }

    [Test]
    public async Task Init_EmptySelectionWritesEmptyRulesArray()
    {
        using var fixture = new DuplicateRuleInitFixture();
        var outputPath = fixture.PathFor("empty.json");
        using var console = new TestConsole();
        console.Profile.Width = 1000;
        var context = fixture.CreateCommandContext(console, includeRule: false);

        var succeeded = context.Execute(DuplicateRuleInitFixture.Settings(outputPath));

        await Assert.That(succeeded).IsTrue();
        await Assert.That(DuplicateRuleConfiguration.Load(outputPath).Rules).IsEmpty();
    }

    [Test]
    public async Task Init_OutputWriteFailureIsReturnedWithoutLeakingTemporaryFile()
    {
        using var fixture = new DuplicateRuleInitFixture();
        var outputPath = fixture.PathFor("blocked-output");
        Directory.CreateDirectory(outputPath);
        using var console = new TestConsole();
        console.Profile.Width = 1000;
        var context = fixture.CreateCommandContext(console);

        var succeeded = context.Execute(DuplicateRuleInitFixture.Settings(outputPath, overwrite: true));

        await Assert.That(succeeded).IsFalse();
        await Assert.That(Directory.EnumerateFiles(fixture.DirectoryPath, ".blocked-output.*.tmp")).IsEmpty();
    }

    private sealed class DuplicateRuleInitFixture : IDisposable
    {
        private readonly string _directory = Path.Combine(Environment.CurrentDirectory, $"duplicaterules-init-{Guid.NewGuid():N}");

        public DuplicateRuleInitFixture()
        {
            Directory.CreateDirectory(_directory);
        }

        public Guid RuleId { get; } = Guid.NewGuid();

        public Guid OwnerId { get; } = Guid.NewGuid();

        public Guid SystemRuleId { get; } = Guid.NewGuid();

        public Guid SystemUserId { get; } = Guid.NewGuid();

        public string DirectoryPath => _directory;

        public string PathFor(string name) => Path.Combine(_directory, name);

        public string WriteMapping(string json)
        {
            var path = PathFor("owners.json");
            File.WriteAllText(path, json);
            return path;
        }

        public static DuplicateRuleInitSettings Settings(
            string output,
            string? mapping = null,
            string capture = "state",
            bool overwrite = false,
            string unmappedOwners = "fail") => new()
        {
            File = output,
            Entity = "contact",
            Capture = capture,
            OwnerMapping = mapping,
            UnmappedOwners = unmappedOwners,
            Overwrite = overwrite
        };

        public CommandTestContext<DuplicateRuleInitCommand, DuplicateRuleInitSettings> CreateCommandContext(
            TestConsole console,
            bool includeRule = true,
            bool includeSystemRule = false,
            bool includeOtherUser = false)
        {
            var services = new TestServiceCollection();
            services.RemoveAll<IDataverseConnection>();
            services.AddSingleton<IDataverseConnection, TestConnection>();
            var data = new List<Entity>
            {
                new("systemuser", OwnerId)
                {
                    ["domainname"] = "owner@example.org",
                    ["isdisabled"] = false
                }
            };
            if (includeRule)
            {
                data.Add(new Entity(DuplicateRule.EntityLogicalName, RuleId)
                {
                    ["name"] = "Contacts with the same e-mail address",
                    ["baseentityname"] = "contact",
                    ["statecode"] = new OptionSetValue(DuplicateRule.Options.StateCode.Active),
                    ["statuscode"] = new OptionSetValue(DuplicateRule.Options.StatusCode.Published),
                    ["ownerid"] = new EntityReference("systemuser", OwnerId)
                });
            }

            if (includeOtherUser)
            {
                data.Add(new Entity("systemuser", Guid.NewGuid())
                {
                    ["domainname"] = "other@example.org",
                    ["isdisabled"] = false
                });
            }

            if (includeSystemRule)
            {
                data.Add(new Entity("systemuser", SystemUserId)
                {
                    ["fullname"] = "SYSTEM",
                    ["isdisabled"] = true
                });
                data.Add(new Entity(DuplicateRule.EntityLogicalName, SystemRuleId)
                {
                    ["name"] = "Accounts with the same account name",
                    ["baseentityname"] = "contact",
                    ["statecode"] = new OptionSetValue(DuplicateRule.Options.StateCode.Active),
                    ["statuscode"] = new OptionSetValue(DuplicateRule.Options.StatusCode.Published),
                    ["ownerid"] = new EntityReference("systemuser", SystemUserId)
                });
            }

            return new CommandTestContextBuilder<DuplicateRuleInitCommand, DuplicateRuleInitSettings>()
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
