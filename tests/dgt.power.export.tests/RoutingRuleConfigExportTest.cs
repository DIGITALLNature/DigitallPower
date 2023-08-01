// Copyright (c) DIGITALL Nature. All rights reserved
// DIGITALL Nature licenses this file to you under the Microsoft Public License.

using dgt.power.dataverse;
using dgt.power.dto;
using dgt.power.export.Base;
using dgt.power.export.Logic;
using dgt.power.export.tests.Base;
using FluentAssertions;
using Microsoft.Xrm.Sdk;
using Xunit.Abstractions;
using Queue = dgt.power.dataverse.Queue;
using RoutingRuleItem = dgt.power.dataverse.RoutingRuleItem;

namespace dgt.power.export.tests;

public class RoutingRuleConfigExportTest : ExportTestBase<RoutingRuleConfigExport>
{
    public RoutingRuleConfigExportTest(ITestOutputHelper testOutputHelper) : base(testOutputHelper)
    {
    }

    [Fact]
    public void ShouldExportRoutingRuleConfiguration()
    {
        var (draftRule, activeRule, draftItem1, draftItem2, activeItem1, team, user, queue) = GetData();
        GetBuilder()
            .WithData(new Entity[]
                {
                    draftRule,
                    draftItem1,
                    draftItem2,
                    activeRule,
                    activeItem1,
                    team,
                    queue,
                    user
                }
            ).Build()
            .Execute(new ExportVerb
                {
                    FileName = GetTestFileName(),
                    FileDir = ArtifactDirectory,
                }
            ).Should().BeTrue();

        var rules = GetConfigurationTestArtifact<List<RoutingRuleConfig>>(GetTestFileName());
        rules.Should().HaveCount(2);


        var rule1 = rules.Single(r => r.RoutingRuleId == draftRule.Id);
        rule1.Active.Should().BeFalse();
        rule1.RoutingRuleItems.Should().HaveCount(2);

        var item1Rule1 = rule1.RoutingRuleItems.Single(rri => rri.RoutingRuleItemId == draftItem1.Id);
        item1Rule1.MsdynRouteto.Should().Be(RoutingRuleItem.Options.MsdynRouteto.Queue);
        item1Rule1.RoutedQueueId.Should().Be(queue.Id);

        var item2Rule1 = rule1.RoutingRuleItems.Single(rri => rri.RoutingRuleItemId == draftItem2.Id);
        item2Rule1.MsdynRouteto.Should().Be(RoutingRuleItem.Options.MsdynRouteto.User_Team);
        item2Rule1.AssignObjectIdType.Should().Be(SystemUser.EntityLogicalName);
        item2Rule1.AssignObjectIdName.Should().Be(user.DomainName);

        var rule2 = rules.Single(r => r.RoutingRuleId == activeRule.Id);
        rule2.Active.Should().BeTrue();
        rule2.RoutingRuleItems.Should().ContainSingle();

        var item1Rule2 = rule2.RoutingRuleItems.Single(rri => rri.RoutingRuleItemId == activeItem1.Id);
        item1Rule2.MsdynRouteto.Should().Be(RoutingRuleItem.Options.MsdynRouteto.User_Team);
        item1Rule2.AssignObjectIdType.Should().Be(Team.EntityLogicalName);
        item1Rule2.AssignObjectIdName.Should().Be(team.Name);
    }

    private static (RoutingRule DraftRule, RoutingRule ActiveRule, RoutingRuleItem DraftItem1, RoutingRuleItem
        DraftItem2, RoutingRuleItem ActiveItem1, Team Team, SystemUser User, Queue Queue) GetData()
    {
        var draftRoutingRule = new RoutingRule(Guid.Parse("8a75523b-27eb-4385-9bf9-d6add312383b"))
        {
            Name = "Rule1 Draft",
            StatusCode = new OptionSetValue(RoutingRule.Options.StatusCode.Draft),
            StateCode = new OptionSetValue(RoutingRule.Options.StateCode.Draft),
        };
        var queue = new Queue(Guid.Parse("469005c9-ca23-4d53-a1ae-f909c7863f6b"))
        {
            Name = "Queue 1",
        };
        var draftItem1 = new RoutingRuleItem(Guid.Parse("da6d62aa-b358-49af-b07a-2269919a0a97"))
        {
            MsdynRouteto = new OptionSetValue(RoutingRuleItem.Options.MsdynRouteto.Queue),
            RoutingRuleId = draftRoutingRule.ToEntityReference(),
            RoutedQueueId = queue.ToEntityReference()
        };
        var user = new SystemUser(Guid.NewGuid())
        {
            DomainName = "user@test.de"
        };
        var draftItem2 = new RoutingRuleItem(Guid.Parse("8790f297-dc24-4bbf-bc8f-6727ad8bf23f"))
        {
            MsdynRouteto = new OptionSetValue(RoutingRuleItem.Options.MsdynRouteto.User_Team),
            RoutingRuleId = new RoutingRule(Guid.Parse("8a75523b-27eb-4385-9bf9-d6add312383b"))
            {
                Name = "Rule1 Draft",
                StatusCode = new OptionSetValue(RoutingRule.Options.StatusCode.Draft),
                StateCode = new OptionSetValue(RoutingRule.Options.StateCode.Draft),
            }.ToEntityReference(),
            AssignObjectId = user.ToEntityReference()
        };
        var activeRule = new RoutingRule(Guid.Parse("3a174e77-7c80-4b78-9ba0-bc1fd8464b62"))
        {
            Name = "Rule2 Active",
            StatusCode = new OptionSetValue(RoutingRule.Options.StatusCode.Active),
            StateCode = new OptionSetValue(RoutingRule.Options.StateCode.Active),
        };
        var team = new Team(Guid.NewGuid())
        {
            Name = "Team 1",
        };
        var activeItem1 = new RoutingRuleItem(Guid.Parse("52afc222-fb8b-46e6-856f-9e07618b91c7"))
        {
            MsdynRouteto = new OptionSetValue(RoutingRuleItem.Options.MsdynRouteto.User_Team),
            RoutingRuleId = new RoutingRule(Guid.Parse("3a174e77-7c80-4b78-9ba0-bc1fd8464b62"))
            {
                Name = "Rule2 Active",
                StatusCode = new OptionSetValue(RoutingRule.Options.StatusCode.Active),
                StateCode = new OptionSetValue(RoutingRule.Options.StateCode.Active),
            }.ToEntityReference(),
            AssignObjectId = new EntityReference(team.LogicalName, team.Id)
            {
                Name = team.Name
            }
        };
        return (draftRoutingRule, activeRule, draftItem1, draftItem2, activeItem1, team, user, queue);
    }


    [Fact]
    public void ShouldUseDefaultOnEmptyFileName()
    {
        var (draftRule, activeRule, draftItem1, draftItem2, activeItem1, team, user, queue) = GetData();
        GetBuilder()
            .WithData(new Entity[]
                {
                    draftRule,
                    draftItem1,
                    draftItem2,
                    activeRule,
                    activeItem1,
                    team,
                    queue,
                    user
                }
            ).Build().Execute(new ExportVerb
            {
                FileName = string.Empty,
                FileDir = ArtifactDirectory,
            }
        ).Should().BeTrue();

        var rules = GetConfigurationTestArtifact<List<RoutingRuleConfig>>("routingruleconfig.json");
        rules.Should().HaveCount(2);

        var rule1 = rules.Single(r => r.RoutingRuleId == draftRule.Id);
        rule1.Active.Should().BeFalse();
        rule1.RoutingRuleItems.Should().HaveCount(2);

        var item1Rule1 = rule1.RoutingRuleItems.Single(rri => rri.RoutingRuleItemId == draftItem1.Id);
        item1Rule1.MsdynRouteto.Should().Be(RoutingRuleItem.Options.MsdynRouteto.Queue);
        item1Rule1.RoutedQueueId.Should().Be(queue.Id);

        var item2Rule1 = rule1.RoutingRuleItems.Single(rri => rri.RoutingRuleItemId == draftItem2.Id);
        item2Rule1.MsdynRouteto.Should().Be(RoutingRuleItem.Options.MsdynRouteto.User_Team);
        item2Rule1.AssignObjectIdType.Should().Be(SystemUser.EntityLogicalName);
        item2Rule1.AssignObjectIdName.Should().Be(user.DomainName);

        var rule2 = rules.Single(r => r.RoutingRuleId == activeRule.Id);
        rule2.Active.Should().BeTrue();
        rule2.RoutingRuleItems.Should().ContainSingle();

        var item1Rule2 = rule2.RoutingRuleItems.Single(rri => rri.RoutingRuleItemId == activeItem1.Id);
        item1Rule2.MsdynRouteto.Should().Be(RoutingRuleItem.Options.MsdynRouteto.User_Team);
        item1Rule2.AssignObjectIdType.Should().Be(Team.EntityLogicalName);
        item1Rule2.AssignObjectIdName.Should().Be(team.Name);
    }
}
