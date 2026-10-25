// Copyright (c) DIGITALL Nature. All rights reserved

using System.ServiceModel;
using dgt.power.teamtemplates.Configuration;
using dgt.power.teamtemplates.Logic;
using Microsoft.PowerPlatform.Dataverse.Client;
using Microsoft.Xrm.Sdk;
using Microsoft.Xrm.Sdk.Messages;
using Microsoft.Xrm.Sdk.Metadata;
using Spectre.Console.Testing;

namespace dgt.power.teamtemplates.tests.Logic;

public class TeamTemplateTransferTests
{
    [Test]
    public async Task Plan_GuidMatching_PreservesUnlistedAndSkipsUnchanged()
    {
        var entry = Entry();
        var existing = Target(entry);
        var unlisted = Target(Entry(name: "Other"));
        var plan = TeamTemplateTransfer.Plan(Config(entry), [Metadata()], [existing, unlisted], []);
        await Assert.That(plan.Unchanged.Single()).IsEqualTo(entry.Id);
        await Assert.That(plan.Creates).IsEmpty();
        await Assert.That(plan.Updates).IsEmpty();
        await Assert.That(plan.Deletes).IsEmpty();
    }

    [Test]
    public async Task Plan_RenameRightsAndDescription_AreDesiredState()
    {
        var entry = Entry();
        var existing = Target(entry);
        existing["description"] = "clear me";
        var plan = TeamTemplateTransfer.Plan(Config(entry with { Name = "Renamed", Rights = 3 }), [Metadata()], [existing], []);
        var update = plan.Updates.Single();
        await Assert.That(update.Id).IsEqualTo(entry.Id);
        await Assert.That(update.GetAttributeValue<string>("teamtemplatename")).IsEqualTo("Renamed");
        await Assert.That(update.GetAttributeValue<int>("defaultaccessrightsmask")).IsEqualTo(3);
        await Assert.That(update.Attributes.Contains("description")).IsTrue();
        await Assert.That(update["description"]).IsNull();
        await Assert.That(update.Attributes.Contains("objecttypecode")).IsFalse();
    }

    [Test]
    [Arguments(false)]
    [Arguments(true)]
    public async Task Plan_NameCollision_RejectsWithoutFallback(bool system)
    {
        var entry = Entry();
        var target = Target(entry with { Id = Guid.NewGuid() });
        target["issystem"] = system;
        await Assert.That(() => TeamTemplateTransfer.Plan(Config(entry), [Metadata()], [target], []))
            .Throws<TeamTemplateConfigurationException>();
    }

    [Test]
    [Arguments(false)]
    [Arguments(true)]
    public async Task Plan_SystemOrTableChange_Rejects(bool system)
    {
        var entry = Entry();
        var target = Target(entry, system ? 10000 : 10001);
        target["issystem"] = system;
        await Assert.That(() => TeamTemplateTransfer.Plan(Config(entry), [Metadata()], [target], []))
            .Throws<TeamTemplateConfigurationException>();
    }

    [Test]
    [Arguments(false, false)]
    [Arguments(null, false)]
    [Arguments(true, true)]
    public async Task Plan_MetadataRequiresActualEnablementAndCode(bool? enabled, bool missingCode)
    {
        var metadata = Metadata(enabled: enabled, code: missingCode ? null : 10000);
        await Assert.That(() => TeamTemplateTransfer.Plan(Config(Entry()), [metadata], [], []))
            .Throws<TeamTemplateConfigurationException>();
    }

    [Test]
    public async Task Plan_UnknownInputOrCleanupTable_Rejects()
    {
        await Assert.That(() => TeamTemplateTransfer.Plan(Config(Entry(table: "unknown")), [Metadata()], [], []))
            .Throws<TeamTemplateConfigurationException>();
        await Assert.That(() => TeamTemplateTransfer.Plan(Config(Entry()), [Metadata()], [], ["unknown"]))
            .Throws<TeamTemplateConfigurationException>();
    }

    [Test]
    [Arguments(false, false)]
    [Arguments(null, false)]
    [Arguments(true, true)]
    public async Task Plan_MetadataValidation_AlsoAppliesToUpdatesAndCleanup(bool? enabled, bool missingCode)
    {
        var entry = Entry();
        var metadata = Metadata(enabled: enabled, code: missingCode ? null : 10000);
        await Assert.That(() => TeamTemplateTransfer.Plan(Config(entry), [metadata], [Target(entry)], []))
            .Throws<TeamTemplateConfigurationException>();
        await Assert.That(() => TeamTemplateTransfer.Plan(Config(Entry(table: "contact")),
                [metadata, Metadata("contact", 10001)], [], ["account"]))
            .Throws<TeamTemplateConfigurationException>();
    }

    [Test]
    public async Task Plan_Cleanup_ProtectsSystemAndOtherTablesWithoutFilteringInput()
    {
        var input = Entry(table: "contact");
        var missing = Target(Entry(name: "Missing"));
        var system = Target(Entry(name: "System"));
        system["issystem"] = true;
        var outside = Target(Entry(table: "contact", name: "Outside"), 10001);
        var plan = TeamTemplateTransfer.Plan(Config(input), [Metadata(), Metadata("contact", 10001)],
            [missing, system, outside], ["account"]);
        await Assert.That(plan.Deletes.Single()).IsEqualTo(missing.Id);
        await Assert.That(plan.Creates.Single().Id).IsEqualTo(input.Id);
        await Assert.That(plan.Creates[0].GetAttributeValue<int>("objecttypecode")).IsEqualTo(10001);
    }

    [Test]
    public async Task Export_PortableRoundTrip_UsesTargetCode()
    {
        var entry = Entry();
        var system = Target(Entry(name: "System"));
        system["issystem"] = true;
        var exported = TeamTemplateTransfer.Export([Metadata()], [Target(entry), system], []);
        var imported = TeamTemplateConfiguration.Parse(exported.Serialize());
        var plan = TeamTemplateTransfer.Plan(imported, [Metadata(code: 20000)], [], []);
        await Assert.That(imported.Templates.Single()).IsEqualTo(entry);
        await Assert.That(plan.Creates.Single().Id).IsEqualTo(entry.Id);
        await Assert.That(plan.Creates[0].GetAttributeValue<int>("objecttypecode")).IsEqualTo(20000);
    }

    [Test]
    public async Task Export_FilterAndEmpty_ExcludeOtherTables()
    {
        var selected = Entry();
        var exported = TeamTemplateTransfer.Export([Metadata(), Metadata("contact", 10001)],
            [Target(selected), Target(Entry(table: "contact"), 10001)], ["account"]);
        await Assert.That(exported.Templates.Single()).IsEqualTo(selected);
        await Assert.That(TeamTemplateTransfer.Export([Metadata()], [], []).Templates).IsEmpty();
        await Assert.That(() => TeamTemplateTransfer.Export([Metadata()], [], ["unknown"]))
            .Throws<TeamTemplateConfigurationException>();
    }

    [Test]
    [Arguments(false)]
    [Arguments(true)]
    public async Task Apply_Failure_StopsWithoutRemainingMutations(bool failDelete)
    {
        var calls = new List<string>();
        var service = Mock.Of<IOrganizationServiceAsync2>();
        service.ExecuteAsync(Any<OrganizationRequest>(), Any<CancellationToken>()).Returns((OrganizationRequest request, CancellationToken _) =>
        {
            calls.Add(request.RequestName);
            if (request is DeleteRequest || !failDelete && calls.Count == 2)
            {
                throw new FaultException<OrganizationServiceFault>(new OrganizationServiceFault { Message = "denied" });
            }
            return new OrganizationResponse();
        });
        using var console = new TestConsole();
        var plan = new TeamTemplatePlan([new Entity("teamtemplate", Guid.NewGuid()), new Entity("teamtemplate", Guid.NewGuid())],
            [], [], [Guid.NewGuid(), Guid.NewGuid()]);
        var success = await new TeamTemplateTransfer(service.Object, console).ApplyAsync(plan, false, CancellationToken.None);
        await Assert.That(success).IsFalse();
        await Assert.That(string.Join(",", calls)).IsEqualTo(failDelete ? "Create,Create,Delete" : "Create,Create");
        await Assert.That(console.Output).Contains(failDelete ? "2 completed mutations retained" : "1 completed mutations retained");
    }

    [Test]
    public async Task Apply_DryRun_NoWritesAndReportsActions()
    {
        var calls = 0;
        var service = Mock.Of<IOrganizationServiceAsync2>();
        service.ExecuteAsync(Any<OrganizationRequest>(), Any<CancellationToken>()).Returns(() =>
        {
            calls++;
            return new OrganizationResponse();
        });
        using var console = new TestConsole();
        var plan = new TeamTemplatePlan([new Entity("teamtemplate", Guid.NewGuid())],
            [new Entity("teamtemplate", Guid.NewGuid())], [Guid.NewGuid()], [Guid.NewGuid()]);
        await Assert.That(await new TeamTemplateTransfer(service.Object, console).ApplyAsync(plan, true, CancellationToken.None)).IsTrue();
        await Assert.That(calls).IsZero();
        await Assert.That(console.Output).Contains("DRY RUN");
        await Assert.That(console.Output).Contains("1 create, 1 update, 1 unchanged, 1 delete");
    }

    [Test]
    public async Task Apply_CancellationAfterFirstWrite_NoFurtherWrites()
    {
        using var cancellation = new CancellationTokenSource();
        var calls = 0;
        var service = Mock.Of<IOrganizationServiceAsync2>();
        service.ExecuteAsync(Any<OrganizationRequest>(), Any<CancellationToken>()).Returns(() =>
        {
            calls++;
            cancellation.Cancel();
            return new OrganizationResponse();
        });
        using var console = new TestConsole();
        var plan = new TeamTemplatePlan([new Entity("teamtemplate", Guid.NewGuid()), new Entity("teamtemplate", Guid.NewGuid())],
            [], [], [Guid.NewGuid()]);
        await Assert.That(async () => await new TeamTemplateTransfer(service.Object, console).ApplyAsync(plan, false, cancellation.Token))
            .Throws<OperationCanceledException>();
        await Assert.That(calls).IsEqualTo(1);
        await Assert.That(console.Output).Contains("1 completed mutations retained");
    }

    [Test]
    public async Task Apply_UpdateFailure_PreventsCleanup()
    {
        var requests = new List<string>();
        var service = Mock.Of<IOrganizationServiceAsync2>();
        service.ExecuteAsync(Any<OrganizationRequest>(), Any<CancellationToken>()).Returns((OrganizationRequest request, CancellationToken _) =>
        {
            requests.Add(request.RequestName);
            if (request is UpdateRequest)
            {
                throw new FaultException<OrganizationServiceFault>(new OrganizationServiceFault { Message = "update denied" });
            }
            return new OrganizationResponse();
        });
        using var console = new TestConsole();
        var plan = new TeamTemplatePlan([new Entity("teamtemplate", Guid.NewGuid())],
            [new Entity("teamtemplate", Guid.NewGuid())], [], [Guid.NewGuid()]);
        await Assert.That(await new TeamTemplateTransfer(service.Object, console).ApplyAsync(plan, false, CancellationToken.None)).IsFalse();
        await Assert.That(string.Join(",", requests)).IsEqualTo("Create,Update");
        await Assert.That(console.Output).Contains("1 completed mutations retained");
    }

    [Test]
    public async Task Apply_Success_CleanupIsLast()
    {
        var requests = new List<string>();
        var service = Mock.Of<IOrganizationServiceAsync2>();
        service.ExecuteAsync(Any<OrganizationRequest>(), Any<CancellationToken>()).Returns((OrganizationRequest request, CancellationToken _) =>
        {
            requests.Add(request.RequestName);
            return new OrganizationResponse();
        });
        using var console = new TestConsole();
        var plan = new TeamTemplatePlan([new Entity("teamtemplate", Guid.NewGuid())],
            [new Entity("teamtemplate", Guid.NewGuid())], [], [Guid.NewGuid()]);
        await Assert.That(await new TeamTemplateTransfer(service.Object, console).ApplyAsync(plan, false, CancellationToken.None)).IsTrue();
        await Assert.That(string.Join(",", requests)).IsEqualTo("Create,Update,Delete");
    }

    public static EntityMetadata Metadata(string table = "account", int? code = 10000, bool? enabled = true)
    {
        var metadata = new EntityMetadata { LogicalName = table, AutoCreateAccessTeams = enabled };
        metadata.GetType().GetProperty(nameof(EntityMetadata.ObjectTypeCode))!.SetValue(metadata, code);
        return metadata;
    }

    public static TeamTemplateEntry Entry(string table = "account", string name = "Readers") =>
        new() { Id = Guid.NewGuid(), Table = table, Name = name, Rights = 1 };

    public static TeamTemplateConfiguration Config(params TeamTemplateEntry[] entries) => new() { SchemaVersion = 1, Templates = entries };

    public static Entity Target(TeamTemplateEntry entry, int code = 10000) => new("teamtemplate", entry.Id)
    {
        ["teamtemplatename"] = entry.Name,
        ["description"] = entry.Description,
        ["objecttypecode"] = code,
        ["defaultaccessrightsmask"] = entry.Rights,
        ["issystem"] = false
    };
}
