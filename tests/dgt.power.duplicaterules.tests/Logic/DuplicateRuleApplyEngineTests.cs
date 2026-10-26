// Copyright (c) DIGITALL Nature. All rights reserved

using dgt.power.common.Ownership;
using dgt.power.duplicaterules.Configuration;
using dgt.power.duplicaterules.Logic;
using dgt.power.duplicaterules.tests.Fakes;
using Digitall.Dataverse.Testing;
using Digitall.Dataverse.Testing.OrganizationRequests;
using Microsoft.Crm.Sdk.Messages;
using Microsoft.Xrm.Sdk;
using Spectre.Console.Testing;

namespace dgt.power.duplicaterules.tests.Logic;

public class DuplicateRuleApplyEngineTests
{
    private const string Contact = "contact";

    [Test]
    public async Task ApplyAsync_EmptyConfiguration_DoesNotConnect()
    {
        var environment = new DuplicateRuleTestEnvironment();
        using var console = new TestConsole();
        console.Profile.Width = 1000;

        var result = await Engine(environment, console).ApplyAsync(new DuplicateRuleConfiguration([]), null, Wait(), CancellationToken.None);

        await Assert.That(result).IsTrue();
        await Assert.That(environment.Connection.StandardCalls).IsZero();
        await Assert.That(console.Output).Contains("0 rules, no changes");
    }

    [Test]
    public async Task ApplyAsync_MissingRuleId_FailsBeforeAnyWrite()
    {
        var environment = new DuplicateRuleTestEnvironment();
        var owner = environment.AddUser("owner@example.org");
        var existing = environment.AddRule(Contact, DuplicateRulePublicationState.Published, owner);
        var missing = Guid.NewGuid();
        using var console = new TestConsole();
        console.Profile.Width = 1000;

        var result = await Engine(environment, console)
            .ApplyAsync(Config(Rule(existing, published: false), Rule(missing, published: true)), null, Wait(), CancellationToken.None);

        await Assert.That(result).IsFalse();
        await Assert.That(console.Output).Contains(missing.ToString("D"));
        await Assert.That(environment.Unpublish.Requested).IsEmpty();
        await Assert.That(environment.StateOf(existing)).IsEqualTo(DuplicateRulePublicationState.Published);
    }

    [Test]
    [Arguments(DuplicateRulePublicationState.Publishing)]
    [Arguments(DuplicateRulePublicationState.Unknown)]
    public async Task ApplyAsync_UnstableListedRule_FailsBeforeAnyWrite(DuplicateRulePublicationState state)
    {
        var environment = new DuplicateRuleTestEnvironment();
        var owner = environment.AddUser("owner@example.org");
        var unstable = environment.AddRule(Contact, state, owner);
        var stable = environment.AddRule(Contact, DuplicateRulePublicationState.Unpublished, owner);
        using var console = new TestConsole();
        console.Profile.Width = 1000;

        var result = await Engine(environment, console)
            .ApplyAsync(Config(Rule(stable, published: true), Rule(unstable, published: true)), null, Wait(), CancellationToken.None);

        await Assert.That(result).IsFalse();
        await Assert.That(console.Output).Contains("not in a stable");
        await Assert.That(environment.Publish.Submitted).IsEmpty();
    }

    [Test]
    public async Task ApplyAsync_DisabledNewOwner_FailsBeforeAnyWrite()
    {
        var environment = new DuplicateRuleTestEnvironment();
        var owner = environment.AddUser("owner@example.org");
        environment.AddUser("disabled@example.org", disabled: true);
        var ruleId = environment.AddRule(Contact, DuplicateRulePublicationState.Unpublished, owner);
        using var console = new TestConsole();
        console.Profile.Width = 1000;

        var result = await Engine(environment, console).ApplyAsync(
            Config(Rule(ruleId, published: true, owner: "target")),
            DuplicateRuleTestEnvironment.UserMapping(("target", "disabled@example.org")),
            Wait(),
            CancellationToken.None);

        await Assert.That(result).IsFalse();
        await Assert.That(console.Output).Contains("disabled");
        await Assert.That(environment.OwnerOf(ruleId).Id).IsEqualTo(owner.Id);
        await Assert.That(environment.Publish.Submitted).IsEmpty();
    }

    [Test]
    public async Task ApplyAsync_UserOwnerWithoutReadPrivilege_FailsBeforeAnyWrite()
    {
        var environment = new DuplicateRuleTestEnvironment();
        var owner = environment.AddUser("owner@example.org");
        environment.AddUser("target@example.org", granted: false);
        var ruleId = environment.AddRule(Contact, DuplicateRulePublicationState.Published, owner);
        using var console = new TestConsole();
        console.Profile.Width = 1000;

        var result = await Engine(environment, console).ApplyAsync(
            Config(Rule(ruleId, published: false, owner: "target")),
            DuplicateRuleTestEnvironment.UserMapping(("target", "target@example.org")),
            Wait(),
            CancellationToken.None);

        await Assert.That(result).IsFalse();
        await Assert.That(console.Output).Contains(DuplicateRulePreflight.ReadPrivilegeName);
        await Assert.That(environment.Unpublish.Requested).IsEmpty();
        await Assert.That(environment.OwnerOf(ruleId).Id).IsEqualTo(owner.Id);
    }

    [Test]
    public async Task ApplyAsync_TeamOwnerWithoutReadPrivilege_ChecksTeamPrivilegesAndFails()
    {
        var environment = new DuplicateRuleTestEnvironment();
        var owner = environment.AddUser("owner@example.org");
        var team = new ResolvedOwner("team", new EntityReference("team", Guid.NewGuid()), OwnerKind.Team, TeamType: OwnerTeamType.Owner);
        var ruleId = environment.AddRule(Contact, DuplicateRulePublicationState.Unpublished, owner);
        using var console = new TestConsole();
        console.Profile.Width = 1000;

        var result = await new DuplicateRuleApplyEngine(environment.Connection, new StubOwnerResolver(team), console, new ManualTimeProvider())
            .ApplyAsync(Config(Rule(ruleId, owner: "team")), TeamMapping(), Wait(), CancellationToken.None);

        await Assert.That(result).IsFalse();
        await Assert.That(environment.TeamPrivileges.Requested).IsEquivalentTo([team.Principal.Id]);
        await Assert.That(console.Output).Contains(DuplicateRulePreflight.ReadPrivilegeName);
    }

    [Test]
    public async Task ApplyAsync_TeamOwnerWithReadPrivilege_AssignsWithoutChangingState()
    {
        var environment = new DuplicateRuleTestEnvironment();
        var owner = environment.AddUser("owner@example.org");
        var team = new ResolvedOwner("team", new EntityReference("team", Guid.NewGuid()), OwnerKind.Team, TeamType: OwnerTeamType.Owner);
        environment.Service.Create(new Entity("team", team.Principal.Id) { ["name"] = "Dup-Test" });
        environment.GrantedPrincipals.Add(team.Principal.Id);
        var ruleId = environment.AddRule(Contact, DuplicateRulePublicationState.Published, owner);
        using var console = new TestConsole();
        console.Profile.Width = 1000;

        var result = await new DuplicateRuleApplyEngine(environment.Connection, new StubOwnerResolver(team), console, new ManualTimeProvider())
            .ApplyAsync(Config(Rule(ruleId, owner: "team")), TeamMapping(), Wait(), CancellationToken.None);

        await Assert.That(result).IsTrue();
        await Assert.That(environment.OwnerOf(ruleId).LogicalName).IsEqualTo("team");
        await Assert.That(environment.OwnerOf(ruleId).Id).IsEqualTo(team.Principal.Id);
        await Assert.That(environment.StateOf(ruleId)).IsEqualTo(DuplicateRulePublicationState.Published);
        await Assert.That(environment.Publish.Submitted).IsEmpty();
        await Assert.That(environment.Unpublish.Requested).IsEmpty();
    }

    [Test]
    public async Task ApplyAsync_MissingPrivilegeDefinition_FailsBeforeAnyWrite()
    {
        var environment = new DuplicateRuleTestEnvironment(includePrivilege: false);
        var owner = environment.AddUser("owner@example.org");
        environment.AddUser("target@example.org");
        var ruleId = environment.AddRule(Contact, DuplicateRulePublicationState.Unpublished, owner);
        using var console = new TestConsole();
        console.Profile.Width = 1000;

        var result = await Engine(environment, console).ApplyAsync(
            Config(Rule(ruleId, owner: "target")),
            DuplicateRuleTestEnvironment.UserMapping(("target", "target@example.org")),
            Wait(),
            CancellationToken.None);

        await Assert.That(result).IsFalse();
        await Assert.That(console.Output).Contains("could not be resolved uniquely");
        await Assert.That(environment.OwnerOf(ruleId).Id).IsEqualTo(owner.Id);
    }

    [Test]
    public async Task ApplyAsync_ProjectedCapacityExceeded_FailsBeforeAnyWrite()
    {
        var environment = new DuplicateRuleTestEnvironment();
        var owner = environment.AddUser("owner@example.org");
        for (var i = 0; i < 4; i++)
        {
            environment.AddRule(Contact, DuplicateRulePublicationState.Published, owner);
        }

        environment.AddRule(Contact, DuplicateRulePublicationState.Publishing, owner);
        var released = environment.AddRule(Contact, DuplicateRulePublicationState.Published, owner);
        var first = environment.AddRule(Contact, DuplicateRulePublicationState.Unpublished, owner);
        var second = environment.AddRule(Contact, DuplicateRulePublicationState.Unpublished, owner);
        using var console = new TestConsole();
        console.Profile.Width = 1000;

        var result = await Engine(environment, console).ApplyAsync(
            Config(Rule(released, published: false), Rule(first, published: true), Rule(second, published: true)),
            null,
            Wait(),
            CancellationToken.None);

        await Assert.That(result).IsFalse();
        await Assert.That(console.Output).Contains("'contact' has 6 occupied, 1 requested unpublish(es), 2 requested publish(es), projected 7");
        await Assert.That(environment.Unpublish.Requested).IsEmpty();
        await Assert.That(environment.Publish.Submitted).IsEmpty();
    }

    [Test]
    [Arguments(false)]
    [Arguments(true)]
    public async Task ApplyAsync_DryRun_ReportsPlanWithoutWrites(bool noWait)
    {
        var environment = new DuplicateRuleTestEnvironment();
        var owner = environment.AddUser("owner@example.org");
        environment.AddUser("target@example.org");
        var unpublish = environment.AddRule(Contact, DuplicateRulePublicationState.Published, owner, "Old contact rule");
        var publish = environment.AddRule(Contact, DuplicateRulePublicationState.Unpublished, owner, "New contact rule");
        using var console = new TestConsole();
        console.Profile.Width = 1000;

        var result = await Engine(environment, console).ApplyAsync(
            Config(Rule(unpublish, published: false), Rule(publish, published: true, owner: "target")),
            DuplicateRuleTestEnvironment.UserMapping(("target", "target@example.org")),
            new DuplicateRuleApplyOptions(DryRun: true, NoWait: noWait, Timeout: TimeSpan.FromSeconds(300)),
            CancellationToken.None);

        await Assert.That(result).IsTrue();
        await Assert.That(console.Output).Contains("DRY RUN");
        await Assert.That(console.Output).Contains("'Old contact rule', contact): unpublish");
        await Assert.That(console.Output).Contains("'New contact rule', contact): assign owner 'target' (User) -> publish");
        await Assert.That(console.Output).Contains("Capacity 'contact': 1 occupied, -1 unpublish, +1 publish, projected 1/5");
        await Assert.That(console.Output).Contains(noWait ? "--no-wait" : "wait up to 300");
        await Assert.That(environment.Unpublish.Requested).IsEmpty();
        await Assert.That(environment.Publish.Submitted).IsEmpty();
        await Assert.That(environment.OwnerOf(publish).Id).IsEqualTo(owner.Id);
    }

    [Test]
    public async Task ApplyAsync_InformationalNameMismatch_MatchesByIdWithoutRename()
    {
        var environment = new DuplicateRuleTestEnvironment();
        var owner = environment.AddUser("owner@example.org");
        var ruleId = environment.AddRule(Contact, DuplicateRulePublicationState.Published, owner, "Target name");
        using var console = new TestConsole();
        console.Profile.Width = 1000;

        var result = await Engine(environment, console).ApplyAsync(
            DuplicateRuleConfiguration.Parse($$"""{"schemaVersion":1,"rules":[{"ruleId":"{{ruleId:D}}","name":"Source name","published":true}]}"""),
            null,
            Wait(),
            CancellationToken.None);

        await Assert.That(result).IsTrue();
        await Assert.That(console.Output).Contains("'Target name'");
        await Assert.That(environment.GetRule(ruleId).GetAttributeValue<string>("name")).IsEqualTo("Target name");
    }

    [Test]
    public async Task ApplyAsync_UnpublishThenPublish_SwapsSlotsAndWaitsForCompletion()
    {
        var environment = new DuplicateRuleTestEnvironment();
        var owner = environment.AddUser("owner@example.org");
        var occupying = Enumerable.Range(0, 4).Select(_ => environment.AddRule(Contact, DuplicateRulePublicationState.Published, owner)).ToArray();
        var released = environment.AddRule(Contact, DuplicateRulePublicationState.Published, owner);
        var added = environment.AddRule(Contact, DuplicateRulePublicationState.Unpublished, owner);
        var time = new ManualTimeProvider
        {
            OnDelay = delay =>
            {
                if (delay == 3)
                {
                    environment.Publish.CompleteAll(environment.Service);
                }
            }
        };
        using var console = new TestConsole();
        console.Profile.Width = 1000;

        var result = await new DuplicateRuleApplyEngine(environment.Connection, new OwnerResolver(), console, time)
            .ApplyAsync(Config(Rule(added, published: true), Rule(released, published: false)), null, Wait(), CancellationToken.None);

        await Assert.That(result).IsTrue();
        await Assert.That(environment.StateOf(released)).IsEqualTo(DuplicateRulePublicationState.Unpublished);
        await Assert.That(environment.StateOf(added)).IsEqualTo(DuplicateRulePublicationState.Published);
        await Assert.That(environment.StateOf(occupying[0])).IsEqualTo(DuplicateRulePublicationState.Published);
        await Assert.That(time.Delays).IsEqualTo(3);
        await Assert.That(console.Output).Contains("published (job");
        await Assert.That(console.Output).Contains("completed 2 operation(s); 0 rule(s) failed");
    }

    [Test]
    public async Task ApplyAsync_SecondRun_IsIdempotent()
    {
        var environment = new DuplicateRuleTestEnvironment();
        var owner = environment.AddUser("owner@example.org");
        environment.AddUser("target@example.org");
        var ruleId = environment.AddRule(Contact, DuplicateRulePublicationState.Unpublished, owner);
        var time = new ManualTimeProvider { OnDelay = _ => environment.Publish.CompleteAll(environment.Service) };
        var configuration = Config(Rule(ruleId, published: true, owner: "target"));
        var mapping = DuplicateRuleTestEnvironment.UserMapping(("target", "target@example.org"));
        using var firstConsole = new TestConsole();
        firstConsole.Profile.Width = 1000;
        using var secondConsole = new TestConsole();
        secondConsole.Profile.Width = 1000;

        var first = await new DuplicateRuleApplyEngine(environment.Connection, new OwnerResolver(), firstConsole, time)
            .ApplyAsync(configuration, mapping, Wait(), CancellationToken.None);
        var second = await new DuplicateRuleApplyEngine(environment.Connection, new OwnerResolver(), secondConsole, time)
            .ApplyAsync(configuration, mapping, Wait(), CancellationToken.None);

        await Assert.That(first).IsTrue();
        await Assert.That(second).IsTrue();
        await Assert.That(environment.Publish.Submitted).Count().IsEqualTo(1);
        await Assert.That(secondConsole.Output).Contains("unchanged (Published)");
        await Assert.That(secondConsole.Output).Contains("completed 0 operation(s)");
    }

    [Test]
    public async Task ApplyAsync_UnpublishNotConfirmed_SkipsDependentPublishAndContinues()
    {
        var environment = new DuplicateRuleTestEnvironment();
        var owner = environment.AddUser("owner@example.org");
        for (var i = 0; i < 4; i++)
        {
            environment.AddRule(Contact, DuplicateRulePublicationState.Published, owner);
        }

        var released = environment.AddRule(Contact, DuplicateRulePublicationState.Published, owner);
        var added = environment.AddRule(Contact, DuplicateRulePublicationState.Unpublished, owner);
        var otherTable = environment.AddRule("account", DuplicateRulePublicationState.Unpublished, owner);
        environment.Unpublish.UnconfirmedRules.Add(released);
        var time = new ManualTimeProvider { OnDelay = _ => environment.Publish.CompleteAll(environment.Service) };
        using var console = new TestConsole();
        console.Profile.Width = 1000;

        var result = await new DuplicateRuleApplyEngine(environment.Connection, new OwnerResolver(), console, time).ApplyAsync(
            Config(Rule(released, published: false), Rule(added, published: true), Rule(otherTable, published: true)),
            null,
            Wait(),
            CancellationToken.None);

        await Assert.That(result).IsFalse();
        await Assert.That(console.Output).Contains("unpublish was not confirmed");
        await Assert.That(console.Output).Contains("no publication slot available");
        await Assert.That(environment.Publish.Submitted).IsEquivalentTo([otherTable]);
        await Assert.That(environment.StateOf(otherTable)).IsEqualTo(DuplicateRulePublicationState.Published);
        await Assert.That(console.Output).Contains("2 rule(s) failed");
    }

    [Test]
    public async Task ApplyAsync_UnpublishFailure_SkipsAssignmentOfSameRule()
    {
        var environment = new DuplicateRuleTestEnvironment();
        var owner = environment.AddUser("owner@example.org");
        environment.AddUser("target@example.org");
        var ruleId = environment.AddRule(Contact, DuplicateRulePublicationState.Published, owner);
        environment.Unpublish.FailingRules.Add(ruleId);
        using var console = new TestConsole();
        console.Profile.Width = 1000;

        var result = await Engine(environment, console).ApplyAsync(
            Config(Rule(ruleId, published: false, owner: "target")),
            DuplicateRuleTestEnvironment.UserMapping(("target", "target@example.org")),
            Wait(),
            CancellationToken.None);

        await Assert.That(result).IsFalse();
        await Assert.That(console.Output).Contains("unpublish failed");
        await Assert.That(environment.OwnerOf(ruleId).Id).IsEqualTo(owner.Id);
    }

    [Test]
    public async Task ApplyAsync_AssignmentFailure_SkipsPublishAndContinuesWithLaterRules()
    {
        var assignments = new FailingAssignFake();
        var environment = new DuplicateRuleTestEnvironment(overrides: assignments);
        var owner = environment.AddUser("owner@example.org");
        environment.AddUser("target@example.org");
        var failing = environment.AddRule(Contact, DuplicateRulePublicationState.Unpublished, owner);
        var later = environment.AddRule(Contact, DuplicateRulePublicationState.Unpublished, owner);
        var time = new ManualTimeProvider { OnDelay = _ => environment.Publish.CompleteAll(environment.Service) };
        using var console = new TestConsole();
        console.Profile.Width = 1000;

        var result = await new DuplicateRuleApplyEngine(environment.Connection, new OwnerResolver(), console, time).ApplyAsync(
            Config(Rule(failing, published: true, owner: "target"), Rule(later, published: true)),
            DuplicateRuleTestEnvironment.UserMapping(("target", "target@example.org")),
            Wait(),
            CancellationToken.None);

        await Assert.That(result).IsFalse();
        await Assert.That(assignments.Count).IsEqualTo(1);
        await Assert.That(console.Output).Contains("publish skipped because owner assignment failed");
        await Assert.That(environment.Publish.Submitted).IsEquivalentTo([later]);
        await Assert.That(environment.StateOf(later)).IsEqualTo(DuplicateRulePublicationState.Published);
    }

    [Test]
    public async Task ApplyAsync_SynchronousPublishFault_ReportsMappedReason()
    {
        var environment = new DuplicateRuleTestEnvironment();
        var owner = environment.AddUser("owner@example.org");
        var ruleId = environment.AddRule(Contact, DuplicateRulePublicationState.Unpublished, owner);
        environment.Publish.RulesWithoutCriteria.Add(ruleId);
        using var console = new TestConsole();
        console.Profile.Width = 1000;

        var result = await Engine(environment, console).ApplyAsync(Config(Rule(ruleId, published: true)), null, Wait(), CancellationToken.None);

        await Assert.That(result).IsFalse();
        await Assert.That(console.Output).Contains("publish failed");
        await Assert.That(console.Output).Contains("criteria");
        await Assert.That(environment.StateOf(ruleId)).IsEqualTo(DuplicateRulePublicationState.Unpublished);
    }

    [Test]
    [Arguments(31, "publish job failed: Matchcode generation failed")]
    [Arguments(32, "publish job was canceled")]
    public async Task ApplyAsync_JobEndsUnsuccessfully_ReportsFailure(int status, string expected)
    {
        var environment = new DuplicateRuleTestEnvironment();
        var owner = environment.AddUser("owner@example.org");
        var ruleId = environment.AddRule(Contact, DuplicateRulePublicationState.Unpublished, owner);
        var time = new ManualTimeProvider
        {
            OnDelay = _ => environment.Publish.Complete(
                environment.Service,
                ruleId,
                status,
                status == 31 ? "Matchcode generation failed" : null)
        };
        using var console = new TestConsole();
        console.Profile.Width = 1000;

        var result = await new DuplicateRuleApplyEngine(environment.Connection, new OwnerResolver(), console, time)
            .ApplyAsync(Config(Rule(ruleId, published: true)), null, Wait(), CancellationToken.None);

        await Assert.That(result).IsFalse();
        await Assert.That(console.Output).Contains(expected);
    }

    [Test]
    public async Task ApplyAsync_Timeout_ReportsLastObservedStateWithSharedDeadline()
    {
        var environment = new DuplicateRuleTestEnvironment();
        var owner = environment.AddUser("owner@example.org");
        var first = environment.AddRule(Contact, DuplicateRulePublicationState.Unpublished, owner);
        var second = environment.AddRule("account", DuplicateRulePublicationState.Unpublished, owner);
        var time = new ManualTimeProvider();
        var start = time.Now;
        using var console = new TestConsole();
        console.Profile.Width = 1000;

        var result = await new DuplicateRuleApplyEngine(environment.Connection, new OwnerResolver(), console, time).ApplyAsync(
            Config(Rule(first, published: true), Rule(second, published: true)),
            null,
            new DuplicateRuleApplyOptions(DryRun: false, NoWait: false, Timeout: TimeSpan.FromSeconds(5)),
            CancellationToken.None);

        await Assert.That(result).IsFalse();
        await Assert.That(time.Now - start).IsEqualTo(TimeSpan.FromSeconds(5));
        await Assert.That(time.Delays).IsEqualTo(3);
        await Assert.That(console.Output).Contains("publish not confirmed within 5 second(s); last observed Publishing");
        await Assert.That(console.Output).Contains("may still run on the server");
        await Assert.That(console.Output).Contains("2 rule(s) failed");
    }

    [Test]
    public async Task ApplyAsync_NoWait_ReportsSubmissionWithoutObservation()
    {
        var environment = new DuplicateRuleTestEnvironment();
        var owner = environment.AddUser("owner@example.org");
        var ruleId = environment.AddRule(Contact, DuplicateRulePublicationState.Unpublished, owner);
        var time = new ManualTimeProvider();
        using var console = new TestConsole();
        console.Profile.Width = 1000;

        var result = await new DuplicateRuleApplyEngine(environment.Connection, new OwnerResolver(), console, time).ApplyAsync(
            Config(Rule(ruleId, published: true)),
            null,
            new DuplicateRuleApplyOptions(DryRun: false, NoWait: true, Timeout: TimeSpan.FromSeconds(300)),
            CancellationToken.None);

        await Assert.That(result).IsTrue();
        await Assert.That(time.Delays).IsZero();
        await Assert.That(environment.StateOf(ruleId)).IsEqualTo(DuplicateRulePublicationState.Publishing);
        await Assert.That(console.Output).Contains($"publish submitted (job {environment.Publish.JobsByRule[ruleId]:D}); completion not verified");
        await Assert.That(console.Output).Contains("later server-side outcomes are unverified");
    }

    [Test]
    public async Task ApplyAsync_CancellationDuringObservation_ReportsCompletedOperations()
    {
        var environment = new DuplicateRuleTestEnvironment();
        var owner = environment.AddUser("owner@example.org");
        var ruleId = environment.AddRule(Contact, DuplicateRulePublicationState.Unpublished, owner);
        using var cancellation = new CancellationTokenSource();
        // ReSharper disable once AccessToDisposedClosure
        environment.Publish.OnSubmit = _ => cancellation.Cancel();
        using var console = new TestConsole();
        console.Profile.Width = 1000;

        var result = await Engine(environment, console).ApplyAsync(Config(Rule(ruleId, published: true)), null, Wait(), cancellation.Token);

        await Assert.That(result).IsFalse();
        await Assert.That(console.Output).Contains("Cancelled after 1 completed operation(s)");
        await Assert.That(console.Output).Contains("Submitted publish jobs are not cancelled");
    }

    [Test]
    public async Task ApplyAsync_OwnerOnlyChange_DoesNotPublish()
    {
        var environment = new DuplicateRuleTestEnvironment();
        var owner = environment.AddUser("owner@example.org");
        var target = environment.AddUser("target@example.org");
        var ruleId = environment.AddRule(Contact, DuplicateRulePublicationState.Unpublished, owner);
        using var console = new TestConsole();
        console.Profile.Width = 1000;

        var result = await Engine(environment, console).ApplyAsync(
            Config(Rule(ruleId, owner: "target")),
            DuplicateRuleTestEnvironment.UserMapping(("target", "target@example.org")),
            Wait(),
            CancellationToken.None);

        await Assert.That(result).IsTrue();
        await Assert.That(environment.OwnerOf(ruleId).Id).IsEqualTo(target.Id);
        await Assert.That(environment.StateOf(ruleId)).IsEqualTo(DuplicateRulePublicationState.Unpublished);
        await Assert.That(environment.Publish.Submitted).IsEmpty();
        await Assert.That(environment.Connection.CallerIds).IsEmpty();
    }

    private static DuplicateRuleApplyEngine Engine(DuplicateRuleTestEnvironment environment, TestConsole console) =>
        new(environment.Connection, new OwnerResolver(), console, new ManualTimeProvider());

    private static DuplicateRuleApplyOptions Wait() => new(DryRun: false, NoWait: false, Timeout: TimeSpan.FromSeconds(300));

    private static DuplicateRuleIntent Rule(Guid id, bool? published = null, string? owner = null) => new(id, null, published, owner);

    private static DuplicateRuleConfiguration Config(params DuplicateRuleIntent[] rules) => new(rules);

    private static OwnerMapping TeamMapping() => new(
        1,
        new Dictionary<string, OwnerMappingEntry>(StringComparer.Ordinal)
        {
            ["team"] = new() { Kind = OwnerKind.Team, NamePattern = "Dup-Test" }
        });

    private sealed class FailingAssignFake : IOrganizationRequestFake
    {
        public Type ForType => typeof(AssignRequest);

        public int Count { get; private set; }

        public OrganizationResponse Execute(OrganizationRequest organizationRequest, FakeOrganizationService fakeOrganizationService)
        {
            Count++;
            throw new InvalidOperationException("Assignment denied.");
        }
    }
}
