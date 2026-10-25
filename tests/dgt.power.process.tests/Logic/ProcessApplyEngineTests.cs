// Copyright (c) DIGITALL Nature. All rights reserved

using dgt.power.common;
using dgt.power.common.Ownership;
using dgt.power.dataverse;
using dgt.power.process.Configuration;
using dgt.power.process.Logic;
using Digitall.Dataverse.Testing;
using Digitall.Dataverse.Testing.OrganizationRequests;
using Microsoft.Crm.Sdk.Messages;
using Microsoft.PowerPlatform.Dataverse.Client;
using Microsoft.Xrm.Sdk;
using Microsoft.Xrm.Sdk.Messages;
using Spectre.Console.Testing;

namespace dgt.power.process.tests.Logic;

public class ProcessApplyEngineTests
{
    [Test]
    public async Task ApplyAsync_EmptyConfigurationDoesNotAuthenticate()
    {
        var connection = new TestDataverseConnection();
        using var console = new TestConsole();
        var engine = new ProcessApplyEngine(connection, new OwnerResolver(), console);

        var result = await engine.ApplyAsync(new ProcessConfiguration([]), null, null, false, false, CancellationToken.None);

        await Assert.That(result).IsTrue();
        await Assert.That(connection.StandardCalls).IsZero();
    }

    [Test]
    public async Task ApplyAsync_OwnerOnlyChangePreservesActiveStateAndIsIdempotent()
    {
        var service = CreateService();
        var oldOwnerId = AddUser(service, "old@example.org");
        var newOwnerId = AddUser(service, "new@example.org");
        var processId = Guid.NewGuid();
        AddProcess(service, processId, oldOwnerId, active: true);
        var connection = new TestDataverseConnection(service);
        var mapping = OwnerMappingLoader.Parse(
            """{"schemaVersion":1,"owners":{"target":{"kind":"user","domainName":"new@example.org"}}}""");
        var configuration = ProcessConfiguration.Parse(
            $$"""{"schemaVersion":1,"processes":[{"processId":"{{processId:D}}","owner":"target"}]}""");
        using var console = new TestConsole();
        var engine = new ProcessApplyEngine(connection, new OwnerResolver(), console);

        var firstResult = await engine.ApplyAsync(configuration, mapping, null, false, false, CancellationToken.None);
        var secondResult = await engine.ApplyAsync(configuration, mapping, null, false, false, CancellationToken.None);
        var updated = service.Retrieve(Workflow.EntityLogicalName, processId, new Microsoft.Xrm.Sdk.Query.ColumnSet(true));

        await Assert.That(firstResult).IsTrue();
        await Assert.That(secondResult).IsTrue();
        await Assert.That(updated.GetAttributeValue<EntityReference>("ownerid").Id).IsEqualTo(newOwnerId);
        await Assert.That(updated.GetAttributeValue<OptionSetValue>("statecode")?.Value).IsEqualTo(Workflow.Options.StateCode.Activated);
        await Assert.That(connection.CallerIds).IsEmpty();
    }

    [Test]
    public async Task ApplyAsync_DryRunResolvesPlanWithoutChangingTarget()
    {
        var service = CreateService();
        var ownerId = AddUser(service, "owner@example.org");
        var processId = Guid.NewGuid();
        AddProcess(service, processId, ownerId, active: false);
        var connection = new TestDataverseConnection(service);
        var configuration = ProcessConfiguration.Parse(
            $$"""{"schemaVersion":1,"processes":[{"processId":"{{processId:D}}","active":true}]}""");
        using var console = new TestConsole();
        var engine = new ProcessApplyEngine(connection, new OwnerResolver(), console);

        var result = await engine.ApplyAsync(configuration, null, null, false, true, CancellationToken.None);
        var unchanged = service.Retrieve(Workflow.EntityLogicalName, processId, new Microsoft.Xrm.Sdk.Query.ColumnSet(true));

        await Assert.That(result).IsTrue();
        await Assert.That(unchanged.GetAttributeValue<OptionSetValue>("statecode")?.Value).IsEqualTo(Workflow.Options.StateCode.Draft);
        await Assert.That(console.Output).Contains("DRY RUN");
        await Assert.That(console.Output).Contains("activate");
    }

    [Test]
    [Arguments(false)]
    [Arguments(true)]
    public async Task ApplyAsync_BracketedOwnerKeyIsReportedLiterally(bool dryRun)
    {
        var service = CreateService();
        var oldOwnerId = AddUser(service, "old@example.org");
        var newOwnerId = AddUser(service, "new@example.org");
        var processId = Guid.NewGuid();
        AddProcess(service, processId, oldOwnerId, active: false);
        var mapping = OwnerMappingLoader.Parse(
            """{"schemaVersion":1,"owners":{"ops[prod]":{"kind":"user","domainName":"new@example.org"}}}""");
        var configuration = ProcessConfiguration.Parse(
            $$"""{"schemaVersion":1,"processes":[{"processId":"{{processId:D}}","owner":"ops[prod]"}]}""");
        using var console = new TestConsole();

        var result = await new ProcessApplyEngine(new TestDataverseConnection(service), new OwnerResolver(), console)
            .ApplyAsync(configuration, mapping, null, false, dryRun, CancellationToken.None);
        var process = service.Retrieve(Workflow.EntityLogicalName, processId, new Microsoft.Xrm.Sdk.Query.ColumnSet(true));

        await Assert.That(result).IsTrue();
        await Assert.That(console.Output).Contains("assign owner 'ops[prod]'");
        await Assert.That(process.GetAttributeValue<EntityReference>("ownerid").Id)
            .IsEqualTo(dryRun ? oldOwnerId : newOwnerId);
    }

    [Test]
    public async Task ApplyAsync_DryRunDoesNotAttemptActivationOrRetries()
    {
        var service = new FakeOrganizationServiceAsync();
        var activations = new ActivationRetryExecutor(alwaysFail: true);
        service.AddRequests(activations);
        service.AddDefaultRequests();
        var ownerId = AddUser(service, "owner@example.org");
        var processId = Guid.NewGuid();
        AddProcess(service, processId, ownerId, active: false);
        var configuration = ProcessConfiguration.Parse(
            $$"""{"schemaVersion":1,"processes":[{"processId":"{{processId:D}}","active":true}]}""");
        using var console = new TestConsole();

        var result = await new ProcessApplyEngine(new TestDataverseConnection(service), new OwnerResolver(), console)
            .ApplyAsync(configuration, null, null, false, true, CancellationToken.None);

        await Assert.That(result).IsTrue();
        await Assert.That(activations.ActivationOrder).IsEmpty();
        await Assert.That(console.Output).Contains("DRY RUN");
    }

    [Test]
    public async Task ApplyAsync_DisabledUnchangedOwnerIsAllowedButImpersonationIsRejected()
    {
        var service = CreateService();
        var ownerId = AddUser(service, "disabled@example.org", disabled: true);
        var processId = Guid.NewGuid();
        AddProcess(service, processId, ownerId, active: true);
        var mapping = OwnerMappingLoader.Parse(
            """{"schemaVersion":1,"owners":{"disabled":{"kind":"user","domainName":"disabled@example.org"}}}""");
        var ownerOnly = ProcessConfiguration.Parse(
            $$"""{"schemaVersion":1,"processes":[{"processId":"{{processId:D}}","owner":"disabled"}]}""");
        var connection = new TestDataverseConnection(service);
        using var console = new TestConsole();
        var engine = new ProcessApplyEngine(connection, new OwnerResolver(), console);

        var unchangedResult = await engine.ApplyAsync(ownerOnly, mapping, null, false, false, CancellationToken.None);
        var impersonationResult = await engine.ApplyAsync(ownerOnly, mapping, "disabled", false, false, CancellationToken.None);
        var ownerImpersonationNoTransitionResult =
            await engine.ApplyAsync(ownerOnly, mapping, null, true, false, CancellationToken.None);

        await Assert.That(unchangedResult).IsTrue();
        await Assert.That(impersonationResult).IsFalse();
        await Assert.That(ownerImpersonationNoTransitionResult).IsTrue();
        await Assert.That(connection.CallerIds).IsEmpty();
    }

    [Test]
    public async Task ApplyAsync_UsesImpersonatedServiceForStateAndStandardServiceForOwnerAssignment()
    {
        var standard = CreateService();
        var impersonated = CreateService();
        var oldOwnerId = AddUser(standard, "old@example.org");
        var newOwnerId = AddUser(standard, "new@example.org");
        var callerId = AddUser(standard, "caller@example.org");
        var processId = Guid.NewGuid();
        AddProcess(standard, processId, oldOwnerId, active: true);
        AddProcess(impersonated, processId, oldOwnerId, active: true);
        var mapping = OwnerMappingLoader.Parse(
            """{"schemaVersion":1,"owners":{"target":{"kind":"user","domainName":"new@example.org"},"caller":{"kind":"user","domainName":"caller@example.org"}}}""");
        var configuration = ProcessConfiguration.Parse(
            $$"""{"schemaVersion":1,"processes":[{"processId":"{{processId:D}}","owner":"target","active":true}]}""");
        var connection = new TestDataverseConnection(standard, impersonated);
        using var console = new TestConsole();
        var engine = new ProcessApplyEngine(connection, new OwnerResolver(), console);

        var result = await engine.ApplyAsync(configuration, mapping, "caller", false, false, CancellationToken.None);
        var standardProcess = standard.Retrieve(Workflow.EntityLogicalName, processId, new Microsoft.Xrm.Sdk.Query.ColumnSet(true));
        var impersonatedProcess = impersonated.Retrieve(Workflow.EntityLogicalName, processId, new Microsoft.Xrm.Sdk.Query.ColumnSet(true));

        await Assert.That(result).IsTrue();
        await Assert.That(connection.CallerIds).IsEquivalentTo([callerId]);
        await Assert.That(standardProcess.GetAttributeValue<EntityReference>("ownerid").Id).IsEqualTo(newOwnerId);
        await Assert.That(standardProcess.GetAttributeValue<OptionSetValue>("statecode")?.Value)
            .IsEqualTo(Workflow.Options.StateCode.Activated);
        await Assert.That(impersonatedProcess.GetAttributeValue<EntityReference>("ownerid").Id).IsEqualTo(oldOwnerId);
        await Assert.That(impersonatedProcess.GetAttributeValue<OptionSetValue>("statecode")?.Value)
            .IsEqualTo(Workflow.Options.StateCode.Activated);
    }

    [Test]
    public async Task ApplyAsync_UnavailableImpersonationFailsBeforeWrites()
    {
        var service = CreateService();
        var ownerId = AddUser(service, "owner@example.org");
        var callerId = AddUser(service, "caller@example.org");
        var processId = Guid.NewGuid();
        AddProcess(service, processId, ownerId, active: false);
        var mapping = OwnerMappingLoader.Parse(
            """{"schemaVersion":1,"owners":{"caller":{"kind":"user","domainName":"caller@example.org"}}}""");
        var configuration = ProcessConfiguration.Parse(
            $$"""{"schemaVersion":1,"processes":[{"processId":"{{processId:D}}","active":true}]}""");
        var connection = new TestDataverseConnection(service) { FailCallerAcquisition = true };
        using var console = new TestConsole();
        var engine = new ProcessApplyEngine(connection, new OwnerResolver(), console);

        var result = await engine.ApplyAsync(configuration, mapping, "caller", false, false, CancellationToken.None);
        var unchanged = service.Retrieve(Workflow.EntityLogicalName, processId, new Microsoft.Xrm.Sdk.Query.ColumnSet(true));

        await Assert.That(result).IsFalse();
        await Assert.That(unchanged.GetAttributeValue<OptionSetValue>("statecode")?.Value).IsEqualTo(Workflow.Options.StateCode.Draft);
        await Assert.That(connection.CallerIds).IsEquivalentTo([callerId]);
    }

    [Test]
    public async Task ApplyAsync_ActivateAsOwnerUsesConfiguredOwnerForStateTransitions()
    {
        var standard = CreateService();
        var impersonated = CreateService();
        var oldOwnerId = AddUser(standard, "old@example.org");
        var targetOwnerId = AddUser(standard, "target@example.org");
        var processId = Guid.NewGuid();
        AddProcess(standard, processId, oldOwnerId, active: true);
        AddProcess(impersonated, processId, oldOwnerId, active: true);
        var mapping = OwnerMappingLoader.Parse(
            """{"schemaVersion":1,"owners":{"target":{"kind":"user","domainName":"target@example.org"}}}""");
        var configuration = ProcessConfiguration.Parse(
            $$"""{"schemaVersion":1,"processes":[{"processId":"{{processId:D}}","owner":"target","active":true}]}""");
        var connection = new TestDataverseConnection(standard, impersonated);
        using var console = new TestConsole();
        var engine = new ProcessApplyEngine(connection, new OwnerResolver(), console);

        var result = await engine.ApplyAsync(configuration, mapping, null, true, false, CancellationToken.None);

        await Assert.That(result).IsTrue();
        await Assert.That(connection.CallerIds).IsEquivalentTo([targetOwnerId]);
        await Assert.That(standard.Retrieve(Workflow.EntityLogicalName, processId, new Microsoft.Xrm.Sdk.Query.ColumnSet(true))
            .GetAttributeValue<EntityReference>("ownerid").Id).IsEqualTo(targetOwnerId);
    }

    [Test]
    public async Task ApplyAsync_UnsupportedProcessFailsPreflightWithoutChangingIt()
    {
        var service = CreateService();
        var ownerId = AddUser(service, "owner@example.org");
        var processId = Guid.NewGuid();
        service.Create(new Workflow(processId)
        {
            Type = new OptionSetValue(Workflow.Options.Type.Definition),
            Category = new OptionSetValue(Workflow.Options.Category.Dialog),
            StateCode = new OptionSetValue(Workflow.Options.StateCode.Draft),
            StatusCode = new OptionSetValue(Workflow.Options.StatusCode.Draft),
            OwnerId = new EntityReference("systemuser", ownerId)
        });
        var connection = new TestDataverseConnection(service);
        var configuration = ProcessConfiguration.Parse(
            $$"""{"schemaVersion":1,"processes":[{"processId":"{{processId:D}}","active":true}]}""");
        using var console = new TestConsole();
        var engine = new ProcessApplyEngine(connection, new OwnerResolver(), console);

        var result = await engine.ApplyAsync(configuration, null, null, false, false, CancellationToken.None);
        var unchanged = service.Retrieve(Workflow.EntityLogicalName, processId, new Microsoft.Xrm.Sdk.Query.ColumnSet(true));

        await Assert.That(result).IsFalse();
        await Assert.That(unchanged.GetAttributeValue<OptionSetValue>("statecode")?.Value)
            .IsEqualTo(Workflow.Options.StateCode.Draft);
    }

    [Test]
    public async Task ApplyAsync_MissingProcessIdFailsPreflightWithoutWrites()
    {
        var service = CreateService();
        var decoyOwner = AddUser(service, "decoy@example.org");
        var decoyId = Guid.NewGuid();
        service.Create(new Workflow(decoyId)
        {
            Name = "Localized process name",
            Type = new OptionSetValue(Workflow.Options.Type.Definition),
            Category = new OptionSetValue(Workflow.Options.Category.Workflow),
            StateCode = new OptionSetValue(Workflow.Options.StateCode.Draft),
            OwnerId = new EntityReference("systemuser", decoyOwner)
        });
        var connection = new TestDataverseConnection(service);
        var missingId = Guid.NewGuid();
        var configuration = ProcessConfiguration.Parse(
            $$"""{"schemaVersion":1,"processes":[{"processId":"{{missingId:D}}","name":"Localized process name","active":true}]}""");
        using var console = new TestConsole();

        var result = await new ProcessApplyEngine(connection, new OwnerResolver(), console)
            .ApplyAsync(configuration, null, null, false, false, CancellationToken.None);

        await Assert.That(result).IsFalse();
        await Assert.That(connection.CallerIds).IsEmpty();
    }

    [Test]
    public async Task ApplyAsync_NonDefinitionFailsPreflightWithoutWrites()
    {
        var service = CreateService();
        var ownerId = AddUser(service, "owner@example.org");
        var processId = Guid.NewGuid();
        service.Create(new Workflow(processId)
        {
            Type = new OptionSetValue(Workflow.Options.Type.Activation),
            Category = new OptionSetValue(Workflow.Options.Category.Workflow),
            StateCode = new OptionSetValue(Workflow.Options.StateCode.Draft),
            OwnerId = new EntityReference("systemuser", ownerId)
        });
        var configuration = ProcessConfiguration.Parse(
            $$"""{"schemaVersion":1,"processes":[{"processId":"{{processId:D}}","active":true}]}""");
        using var console = new TestConsole();

        var result = await new ProcessApplyEngine(new TestDataverseConnection(service), new OwnerResolver(), console)
            .ApplyAsync(configuration, null, null, false, false, CancellationToken.None);

        await Assert.That(result).IsFalse();
        await Assert.That(service.Retrieve(Workflow.EntityLogicalName, processId, new Microsoft.Xrm.Sdk.Query.ColumnSet(true))
            .GetAttributeValue<OptionSetValue>("statecode")?.Value).IsEqualTo(Workflow.Options.StateCode.Draft);
    }

    [Test]
    public async Task ApplyAsync_MissingOwnerKeyFailsPreflightBeforeWrites()
    {
        var service = CreateService();
        var ownerId = AddUser(service, "owner@example.org");
        var processId = Guid.NewGuid();
        AddProcess(service, processId, ownerId, active: true);
        var configuration = ProcessConfiguration.Parse(
            $$"""{"schemaVersion":1,"processes":[{"processId":"{{processId:D}}","owner":"missing"}]}""");
        var mapping = OwnerMappingLoader.Parse(
            """{"schemaVersion":1,"owners":{"other":{"kind":"user","domainName":"other@example.org"}}}""");
        using var console = new TestConsole();

        var result = await new ProcessApplyEngine(new TestDataverseConnection(service), new OwnerResolver(), console)
            .ApplyAsync(configuration, mapping, null, false, false, CancellationToken.None);

        await Assert.That(result).IsFalse();
        await Assert.That(service.Retrieve(Workflow.EntityLogicalName, processId, new Microsoft.Xrm.Sdk.Query.ColumnSet(true))
            .GetAttributeValue<EntityReference>("ownerid").Id).IsEqualTo(ownerId);
    }

    [Test]
    public async Task ApplyAsync_ActivateAsOwnerWithActiveIntentRequiresConfiguredOwner()
    {
        var service = CreateService();
        var ownerId = AddUser(service, "owner@example.org");
        var processId = Guid.NewGuid();
        AddProcess(service, processId, ownerId, active: false);
        var configuration = ProcessConfiguration.Parse(
            $$"""{"schemaVersion":1,"processes":[{"processId":"{{processId:D}}","active":true}]}""");
        using var console = new TestConsole();

        var result = await new ProcessApplyEngine(new TestDataverseConnection(service), new OwnerResolver(), console)
            .ApplyAsync(configuration, null, null, true, false, CancellationToken.None);

        await Assert.That(result).IsFalse();
        await Assert.That(service.Retrieve(Workflow.EntityLogicalName, processId, new Microsoft.Xrm.Sdk.Query.ColumnSet(true))
            .GetAttributeValue<OptionSetValue>("statecode")?.Value).IsEqualTo(Workflow.Options.StateCode.Draft);
    }

    [Test]
    public async Task ApplyAsync_TeamOwnerFailsPreflightBeforeAnyWrite()
    {
        var service = CreateService();
        var currentOwnerId = AddUser(service, "current@example.org");
        var processId = Guid.NewGuid();
        AddProcess(service, processId, currentOwnerId, active: true);
        var team = new ResolvedOwner("team", new EntityReference("team", Guid.NewGuid()), OwnerKind.Team);
        var ownerResolver = new StubOwnerResolver(team);
        var mapping = Mapping("team");
        var configuration = ProcessConfiguration.Parse(
            $$"""{"schemaVersion":1,"processes":[{"processId":"{{processId:D}}","owner":"team"}]}""");
        var connection = new TestDataverseConnection(service);
        using var console = new TestConsole();
        var engine = new ProcessApplyEngine(connection, ownerResolver, console);

        var result = await engine.ApplyAsync(configuration, mapping, null, false, false, CancellationToken.None);

        await Assert.That(result).IsFalse();
        await Assert.That(service.Retrieve(Workflow.EntityLogicalName, processId, new Microsoft.Xrm.Sdk.Query.ColumnSet(true))
            .GetAttributeValue<EntityReference>("ownerid").Id).IsEqualTo(currentOwnerId);
        await Assert.That(console.Output).Contains("team");
    }

    [Test]
    public async Task ApplyAsync_DisabledOwnerIsRejectedOnlyForActualOwnerChange()
    {
        var service = CreateService();
        var currentOwnerId = AddUser(service, "current@example.org");
        var processId = Guid.NewGuid();
        AddProcess(service, processId, currentOwnerId, active: true);
        var disabledOwner = new ResolvedOwner(
            "disabled",
            new EntityReference("systemuser", Guid.NewGuid()),
            OwnerKind.User,
            IsDisabled: true);
        var mapping = Mapping("disabled");
        var configuration = ProcessConfiguration.Parse(
            $$"""{"schemaVersion":1,"processes":[{"processId":"{{processId:D}}","owner":"disabled"}]}""");
        var connection = new TestDataverseConnection(service);
        using var console = new TestConsole();

        var result = await new ProcessApplyEngine(connection, new StubOwnerResolver(disabledOwner), console)
            .ApplyAsync(configuration, mapping, null, false, false, CancellationToken.None);

        await Assert.That(result).IsFalse();
        await Assert.That(console.Output).Contains("disabled user");
        await Assert.That(service.Retrieve(Workflow.EntityLogicalName, processId, new Microsoft.Xrm.Sdk.Query.ColumnSet(true))
            .GetAttributeValue<EntityReference>("ownerid").Id).IsEqualTo(currentOwnerId);
    }

    [Test]
    public async Task ApplyAsync_ActivateAsOwnerResolvesAndUsesTwoDistinctCallers()
    {
        var service = CreateService();
        var firstCurrentOwner = AddUser(service, "first-current@example.org");
        var secondCurrentOwner = AddUser(service, "second-current@example.org");
        var firstCallerId = Guid.NewGuid();
        var secondCallerId = Guid.NewGuid();
        var firstProcessId = Guid.NewGuid();
        var secondProcessId = Guid.NewGuid();
        AddProcess(service, firstProcessId, firstCurrentOwner, active: false);
        AddProcess(service, secondProcessId, secondCurrentOwner, active: false);
        var owners = new[]
        {
            new ResolvedOwner("first", new EntityReference("systemuser", firstCallerId), OwnerKind.User, IsDisabled: false),
            new ResolvedOwner("second", new EntityReference("systemuser", secondCallerId), OwnerKind.ApplicationUser, IsDisabled: false)
        };
        var mapping = Mapping("first", "second");
        var configuration = ProcessConfiguration.Parse(
            $$"""{"schemaVersion":1,"processes":[{"processId":"{{firstProcessId:D}}","owner":"first","active":true},{"processId":"{{secondProcessId:D}}","owner":"second","active":true}]}""");
        var connection = new TestDataverseConnection(service);
        using var console = new TestConsole();

        var result = await new ProcessApplyEngine(connection, new StubOwnerResolver(owners), console)
            .ApplyAsync(configuration, mapping, null, true, false, CancellationToken.None);

        await Assert.That(result).IsTrue();
        await Assert.That(connection.CallerIds).IsEquivalentTo([firstCallerId, secondCallerId]);
    }

    [Test]
    public async Task ApplyAsync_CancellationDuringPreflightIsReportedWithNoTransitions()
    {
        var service = CreateService();
        var ownerId = AddUser(service, "owner@example.org");
        var processId = Guid.NewGuid();
        AddProcess(service, processId, ownerId, active: false);
        var configuration = ProcessConfiguration.Parse(
            $$"""{"schemaVersion":1,"processes":[{"processId":"{{processId:D}}","active":true}]}""");
        using var console = new TestConsole();
        using var cancellation = new CancellationTokenSource();
        await cancellation.CancelAsync();

        var result = await new ProcessApplyEngine(
                new TestDataverseConnection(service),
                new OwnerResolver(),
                console)
            .ApplyAsync(configuration, null, null, false, false, cancellation.Token);

        await Assert.That(result).IsFalse();
        await Assert.That(console.Output).Contains("Cancelled during preflight");
    }

    [Test]
    public async Task ApplyAsync_AssignmentFailureRestoresOnceAndContinuesIndependentProcesses()
    {
        var service = new FakeOrganizationServiceAsync();
        var updateRequests = new UpdateRequestCounter();
        var assignmentRequests = new FailedAssignmentExecutor();
        service.AddRequests(updateRequests, assignmentRequests);
        service.AddDefaultRequests();
        var oldOwner = AddUser(service, "old@example.org");
        var newOwner = AddUser(service, "new@example.org");
        var activeId = Guid.NewGuid();
        var inactiveId = Guid.NewGuid();
        AddProcess(service, activeId, oldOwner, active: true);
        AddProcess(service, inactiveId, oldOwner, active: false);
        var owners = new[]
        {
            new ResolvedOwner("new", new EntityReference("systemuser", newOwner), OwnerKind.User, IsDisabled: false)
        };
        var configuration = ProcessConfiguration.Parse(
            $$"""{"schemaVersion":1,"processes":[{"processId":"{{activeId:D}}","owner":"new"},{"processId":"{{inactiveId:D}}","active":true}]}""");
        using var console = new TestConsole();
        var result = await new ProcessApplyEngine(new TestDataverseConnection(service), new StubOwnerResolver(owners), console)
            .ApplyAsync(configuration, Mapping("new"), null, false, false, CancellationToken.None);

        await Assert.That(result).IsFalse();
        await Assert.That(updateRequests.Count).IsEqualTo(3);
        await Assert.That(assignmentRequests.Count).IsEqualTo(1);
        await Assert.That(console.Output).Contains("original active state restored");
        await Assert.That(console.Output).Contains("owner assignment failed");
        await Assert.That(console.Output).Contains($"{inactiveId:D}: activated");
    }

    [Test]
    public async Task ApplyAsync_FailedRestorationIsReportedWithoutRetry()
    {
        var service = new FakeOrganizationServiceAsync();
        var restoration = new FailedRestorationExecutor();
        service.AddRequests(restoration, new FailedAssignmentExecutor());
        service.AddDefaultRequests();
        var oldOwner = AddUser(service, "old@example.org");
        var newOwner = AddUser(service, "new@example.org");
        var processId = Guid.NewGuid();
        AddProcess(service, processId, oldOwner, active: true);
        var owners = new[]
        {
            new ResolvedOwner("new", new EntityReference("systemuser", newOwner), OwnerKind.User, IsDisabled: false)
        };
        var configuration = ProcessConfiguration.Parse(
            $$"""{"schemaVersion":1,"processes":[{"processId":"{{processId:D}}","owner":"new"}]}""");
        using var console = new TestConsole();

        var result = await new ProcessApplyEngine(new TestDataverseConnection(service), new StubOwnerResolver(owners), console)
            .ApplyAsync(configuration, Mapping("new"), null, false, false, CancellationToken.None);

        await Assert.That(result).IsFalse();
        await Assert.That(restoration.ActivationAttempts).IsEqualTo(1);
        await Assert.That(console.Output).Contains("restoration failed");
    }

    [Test]
    public async Task ApplyAsync_StateOnlyDeactivationAndAlreadySatisfiedIntentAvoidUnneededWrites()
    {
        var service = new FakeOrganizationServiceAsync();
        var updateRequests = new UpdateRequestCounter();
        service.AddRequests(updateRequests);
        service.AddDefaultRequests();
        var ownerId = AddUser(service, "owner@example.org");
        var activeId = Guid.NewGuid();
        var alreadyInactiveId = Guid.NewGuid();
        AddProcess(service, activeId, ownerId, active: true);
        AddProcess(service, alreadyInactiveId, ownerId, active: false);
        using var console = new TestConsole();
        var engine = new ProcessApplyEngine(new TestDataverseConnection(service), new OwnerResolver(), console);

        var deactivateResult = await engine.ApplyAsync(
            ProcessConfiguration.Parse(
                $$"""{"schemaVersion":1,"processes":[{"processId":"{{activeId:D}}","active":false}]}"""),
            null,
            null,
            false,
            false,
            CancellationToken.None);
        var idempotentResult = await engine.ApplyAsync(
            ProcessConfiguration.Parse(
                $$"""{"schemaVersion":1,"processes":[{"processId":"{{alreadyInactiveId:D}}","active":false}]}"""),
            null,
            null,
            false,
            false,
            CancellationToken.None);

        await Assert.That(deactivateResult).IsTrue();
        await Assert.That(idempotentResult).IsTrue();
        await Assert.That(updateRequests.Count).IsEqualTo(1);
    }

    [Test]
    public async Task ApplyAsync_OwnerChangeWithExplicitInactiveIntentDoesNotReactivate()
    {
        var service = new FakeOrganizationServiceAsync();
        var updateRequests = new UpdateRequestCounter();
        service.AddRequests(updateRequests);
        service.AddDefaultRequests();
        var oldOwner = AddUser(service, "old@example.org");
        AddUser(service, "new@example.org");
        var processId = Guid.NewGuid();
        AddProcess(service, processId, oldOwner, active: true);
        var mapping = OwnerMappingLoader.Parse(
            """{"schemaVersion":1,"owners":{"new":{"kind":"user","domainName":"new@example.org"}}}""");
        var configuration = ProcessConfiguration.Parse(
            $$"""{"schemaVersion":1,"processes":[{"processId":"{{processId:D}}","owner":"new","active":false}]}""");
        using var console = new TestConsole();

        var result = await new ProcessApplyEngine(new TestDataverseConnection(service), new OwnerResolver(), console)
            .ApplyAsync(configuration, mapping, null, false, false, CancellationToken.None);

        await Assert.That(result).IsTrue();
        await Assert.That(updateRequests.Count).IsEqualTo(1);
    }

    [Test]
    public async Task ApplyAsync_RetriesOnlyActivationUntilChildFlowSucceeds()
    {
        var service = new FakeOrganizationServiceAsync();
        var activationRequests = new ActivationRetryExecutor();
        service.AddRequests(activationRequests);
        service.AddDefaultRequests();
        var ownerId = AddUser(service, "owner@example.org");
        var parentId = Guid.NewGuid();
        var childId = Guid.NewGuid();
        AddProcess(service, parentId, ownerId, active: false);
        AddProcess(service, childId, ownerId, active: false);
        var configuration = ProcessConfiguration.Parse(
            $$"""{"schemaVersion":1,"processes":[{"processId":"{{parentId:D}}","active":true},{"processId":"{{childId:D}}","active":true}]}""");
        using var console = new TestConsole();

        var result = await new ProcessApplyEngine(new TestDataverseConnection(service), new OwnerResolver(), console)
            .ApplyAsync(configuration, null, null, false, false, CancellationToken.None);

        await Assert.That(result).IsTrue();
        await Assert.That(activationRequests.ActivationOrder).IsEquivalentTo([parentId, childId, parentId]);
    }

    [Test]
    public async Task ApplyAsync_StopsRetryingWhenActivationRoundMakesNoProgress()
    {
        var service = new FakeOrganizationServiceAsync();
        var activationRequests = new ActivationRetryExecutor(alwaysFail: true);
        service.AddRequests(activationRequests);
        service.AddDefaultRequests();
        var ownerId = AddUser(service, "owner@example.org");
        var processId = Guid.NewGuid();
        AddProcess(service, processId, ownerId, active: false);
        var configuration = ProcessConfiguration.Parse(
            $$"""{"schemaVersion":1,"processes":[{"processId":"{{processId:D}}","active":true}]}""");
        using var console = new TestConsole();

        var result = await new ProcessApplyEngine(new TestDataverseConnection(service), new OwnerResolver(), console)
            .ApplyAsync(configuration, null, null, false, false, CancellationToken.None);

        await Assert.That(result).IsFalse();
        await Assert.That(activationRequests.ActivationOrder).IsEquivalentTo([processId]);
    }

    [Test]
    public async Task ApplyAsync_CancellationAfterTransitionReportsCompletedWorkAndStops()
    {
        using var cancellation = new CancellationTokenSource();
        var service = new FakeOrganizationServiceAsync();
        var updateRequests = new UpdateRequestCounter(cancellation.Cancel);
        service.AddRequests(updateRequests);
        service.AddDefaultRequests();
        var ownerId = AddUser(service, "owner@example.org");
        var firstId = Guid.NewGuid();
        var secondId = Guid.NewGuid();
        AddProcess(service, firstId, ownerId, active: false);
        AddProcess(service, secondId, ownerId, active: false);
        var configuration = ProcessConfiguration.Parse(
            $$"""{"schemaVersion":1,"processes":[{"processId":"{{firstId:D}}","active":true},{"processId":"{{secondId:D}}","active":true}]}""");
        using var console = new TestConsole();

        var result = await new ProcessApplyEngine(new TestDataverseConnection(service), new OwnerResolver(), console)
            .ApplyAsync(configuration, null, null, false, false, cancellation.Token);

        await Assert.That(result).IsFalse();
        await Assert.That(updateRequests.Count).IsEqualTo(1);
        await Assert.That(console.Output).Contains("Cancelled after 1 completed");
    }

    private static OwnerMapping Mapping(params string[] keys) => new(
        1,
        keys.ToDictionary(
            key => key,
            _ => new OwnerMappingEntry { Kind = OwnerKind.User, DomainName = "owner@example.org" },
            StringComparer.Ordinal));

    private static FakeOrganizationServiceAsync CreateService()
    {
        var service = new FakeOrganizationServiceAsync();
        service.AddDefaultRequests();
        return service;
    }

    private static Guid AddUser(FakeOrganizationServiceAsync service, string domainName, bool disabled = false)
    {
        var id = Guid.NewGuid();
        service.Create(new Entity("systemuser", id)
        {
            ["domainname"] = domainName,
            ["isdisabled"] = disabled
        });
        return id;
    }

    private static void AddProcess(FakeOrganizationServiceAsync service, Guid id, Guid ownerId, bool active) =>
        service.Create(new Workflow(id)
        {
            Type = new OptionSetValue(Workflow.Options.Type.Definition),
            Category = new OptionSetValue(Workflow.Options.Category.ModernFlow),
            StateCode = new OptionSetValue(active ? Workflow.Options.StateCode.Activated : Workflow.Options.StateCode.Draft),
            StatusCode = new OptionSetValue(active ? Workflow.Options.StatusCode.Activated : Workflow.Options.StatusCode.Draft),
            OwnerId = new EntityReference("systemuser", ownerId)
        });

    private sealed class TestDataverseConnection : IDataverseConnection
    {
        private readonly IOrganizationServiceAsync2 _service;
        private readonly IOrganizationServiceAsync2 _callerService;

        public TestDataverseConnection(
            IOrganizationServiceAsync2? service = null,
            IOrganizationServiceAsync2? callerService = null)
        {
            _service = service ?? CreateService();
            _callerService = callerService ?? _service;
        }

        public int StandardCalls { get; private set; }
        public List<Guid> CallerIds { get; } = [];
        public bool FailCallerAcquisition { get; init; }

        public Task<IOrganizationServiceAsync2> GetServiceAsync()
        {
            StandardCalls++;
            return Task.FromResult(_service);
        }

        public Task<IOrganizationServiceAsync2> GetServiceAsync(Guid callerId)
        {
            CallerIds.Add(callerId);
            if (FailCallerAcquisition)
            {
                throw new NotSupportedException("Caller-specific service is unavailable.");
            }

            return Task.FromResult(_callerService);
        }

        public Task<bool> CheckAuthAsync() => Task.FromResult(true);

        public Task RefreshAuthAsync() => Task.CompletedTask;
    }

    private sealed class StubOwnerResolver(params ResolvedOwner[] owners) : IOwnerResolver
    {
        private readonly IReadOnlyDictionary<string, ResolvedOwner> _owners =
            owners.ToDictionary(owner => owner.LogicalKey, StringComparer.Ordinal);

        public Task<IReadOnlyDictionary<string, ResolvedOwner>> ResolveAsync(
            IOrganizationServiceAsync2 service,
            OwnerMapping mapping,
            IEnumerable<string> requestedKeys,
            CancellationToken cancellationToken = default) =>
            Task.FromResult<IReadOnlyDictionary<string, ResolvedOwner>>(
                requestedKeys.Distinct(StringComparer.Ordinal).ToDictionary(key => key, key => _owners[key], StringComparer.Ordinal));

        public Task<IReadOnlyList<OwnerCapture>> ResolveForCaptureAsync(
            IOrganizationServiceAsync2 service,
            OwnerMapping mapping,
            IEnumerable<EntityReference> capturedOwners,
            CancellationToken cancellationToken = default) =>
            Task.FromResult<IReadOnlyList<OwnerCapture>>([]);
    }

    private sealed class FailedAssignmentExecutor : IOrganizationRequestFake
    {
        public Type ForType => typeof(AssignRequest);

        public int Count { get; private set; }

        public OrganizationResponse Execute(OrganizationRequest organizationRequest, FakeOrganizationService fakeOrganizationService)
        {
            Count++;
            throw new InvalidOperationException("Assignment denied.");
        }
    }

    private sealed class UpdateRequestCounter(Action? onUpdate = null) : IOrganizationRequestFake
    {
        public Type ForType => typeof(UpdateRequest);
        public int Count { get; private set; }

        public OrganizationResponse Execute(OrganizationRequest organizationRequest, FakeOrganizationService fakeOrganizationService)
        {
            var target = ((UpdateRequest)organizationRequest).Target;
            if (target.Attributes.Contains("statecode"))
            {
                Count++;
                onUpdate?.Invoke();
            }

            return new UpdateResponse();
        }
    }

    private sealed class ActivationRetryExecutor(bool alwaysFail = false) : IOrganizationRequestFake
    {
        private Guid? _failedId;
        public Type ForType => typeof(UpdateRequest);
        public List<Guid> ActivationOrder { get; } = [];

        public OrganizationResponse Execute(OrganizationRequest organizationRequest, FakeOrganizationService fakeOrganizationService)
        {
            var target = ((UpdateRequest)organizationRequest).Target;
            if (target.GetAttributeValue<OptionSetValue>("statecode")?.Value != Workflow.Options.StateCode.Activated)
            {
                return new UpdateResponse();
            }

            ActivationOrder.Add(target.Id);
            if (alwaysFail || (_failedId is null && target.Id == ActivationOrder[0]))
            {
                _failedId ??= target.Id;
                throw new InvalidOperationException("Activation dependency is not ready.");
            }

            return new UpdateResponse();
        }
    }

    private sealed class FailedRestorationExecutor : IOrganizationRequestFake
    {
        public Type ForType => typeof(UpdateRequest);
        public int ActivationAttempts { get; private set; }

        public OrganizationResponse Execute(OrganizationRequest organizationRequest, FakeOrganizationService fakeOrganizationService)
        {
            var state = ((UpdateRequest)organizationRequest).Target.GetAttributeValue<OptionSetValue>("statecode")?.Value;
            if (state == Workflow.Options.StateCode.Activated)
            {
                ActivationAttempts++;
                throw new InvalidOperationException("Restore denied.");
            }

            return new UpdateResponse();
        }
    }
}
