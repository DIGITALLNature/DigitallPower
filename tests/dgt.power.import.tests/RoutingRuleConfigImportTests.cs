// Copyright (c) DIGITALL Nature. All rights reserved
// DIGITALL Nature licenses this file to you under the Microsoft Public License.

using dgt.power.dataverse;
using dgt.power.dto;
using dgt.power.import.Base;
using dgt.power.import.Logic;
using dgt.power.import.tests.Base;
using Microsoft.Xrm.Sdk;
using Queue = dgt.power.dataverse.Queue;
using RoutingRuleItem = dgt.power.common.DTO.RoutingRuleItem;
#pragma warning disable CS8601
#pragma warning disable CS8602

namespace dgt.power.import.tests;

public class RoutingRuleConfigImportTests : ImportTestBase<RoutingRuleConfigImport>
{
    [Test]
    public async Task ShouldFailOnWrongConfiguration() =>
        await Assert.That(GetContext().Execute(new ImportVerb
            {
                FileName = string.Empty,
                FileDir = ArtifactDirectory
            }
        )).IsFalse();

    [Test]
    public async Task ShouldFailOnEmptyConfiguration() =>
        await Assert.That(GetContext().Execute(new ImportVerb
            {
                FileName = WriteConfigurationArtifact(new List<RoutingRuleConfig>()).Name,
                FileDir = ArtifactDirectory
            }
        )).IsFalse();

    [Test]
    public async Task ShouldUpdateRoutingRuleItems()
    {
        var data = GetData();
        var routingRule = data.routingRule;
        var teamRuleItem = data.teamRuleItem;
        var queueRuleItem = data.queueRuleItem;
        var userRuleItem = data.userRuleItem;
        var ruleConfig = new List<RoutingRuleConfig>
        {
            new()
            {
                Name = routingRule.Name,
                Active = false,
                RoutingRuleId = routingRule.Id,
                RoutingRuleItems = new[]
                {
                    new RoutingRuleItem
                    {
                        Name = $"{teamRuleItem.Name} Updated",
                        MsdynRouteto = teamRuleItem.MsdynRouteto.Value,
                        RoutingRuleId = teamRuleItem.RoutingRuleId.Id,
                        RoutingRuleItemId = teamRuleItem.Id,
                        AssignObjectIdType = teamRuleItem.AssignObjectId.LogicalName,
                        AssignObjectIdName = teamRuleItem.AssignObjectId.Name
                    },
                    new RoutingRuleItem
                    {
                        Name = $"{queueRuleItem.Name}Updated",
                        MsdynRouteto = queueRuleItem.MsdynRouteto.Value,
                        RoutingRuleId = queueRuleItem.RoutingRuleId.Id,
                        RoutingRuleItemId = queueRuleItem.Id,
                        RoutedQueueId = queueRuleItem.RoutedQueueId.Id
                    },
                    new RoutingRuleItem
                    {
                        Name = $"{userRuleItem.Name} Updated",
                        MsdynRouteto = userRuleItem.MsdynRouteto.Value,
                        RoutingRuleId = userRuleItem.RoutingRuleId.Id,
                        RoutingRuleItemId = userRuleItem.Id,
                        AssignObjectIdType = userRuleItem.AssignObjectId.LogicalName,
                        AssignObjectIdName = userRuleItem.AssignObjectId.Name
                    }
                }
            }
        };

        teamRuleItem.AssignObjectId = null;
        queueRuleItem.RoutedQueueId = null;
        userRuleItem.AssignObjectId = null;

        var context = GetBuilder()
            .WithData(routingRule)
            .WithData(queueRuleItem)
            .WithData(teamRuleItem)
            .WithData(userRuleItem)
            .WithData(data.entities)
            .Build();

        await Assert.That(context
            .Execute(new ImportVerb
                {
                    FileName = WriteConfigurationArtifact(ruleConfig).Name,
                    FileDir = ArtifactDirectory
                }
            )).IsTrue();

        var updatedRule = context.GetById<RoutingRule>(routingRule.Id);
        await Assert.That(updatedRule.OwnerId.Id).IsEqualTo(routingRule.OwnerId.Id);
    }

    [Test]
    public async Task ShouldSkipUpdateOfRoutingRuleItemsWhenRouteToIsMissing()
    {
        var data = GetData();
        var routingRule = data.routingRule;
        var teamRuleItem = data.teamRuleItem;
        var queueRuleItem = data.queueRuleItem;
        var userRuleItem = data.userRuleItem;

        var context = GetBuilder()
            .WithData(routingRule)
            .WithData(queueRuleItem)
            .WithData(teamRuleItem)
            .WithData(userRuleItem)
            .Build();

        await Assert.That(context
            .Execute(new ImportVerb
                {
                    FileName = WriteConfigurationArtifact(new List<RoutingRuleConfig> { new() { Name = routingRule.Name, Active = routingRule.StateCode.Value == RoutingRule.Options.StateCode.Active, RoutingRuleId = routingRule.Id, RoutingRuleItems = new[] { new RoutingRuleItem { Name = $"{teamRuleItem.Name} Updated", MsdynRouteto = teamRuleItem.MsdynRouteto.Value, RoutingRuleId = teamRuleItem.RoutingRuleId.Id, RoutingRuleItemId = teamRuleItem.Id, AssignObjectIdType = teamRuleItem.AssignObjectId.LogicalName, AssignObjectIdName = teamRuleItem.AssignObjectId.Name }, new RoutingRuleItem { Name = $"{queueRuleItem.Name}Updated", MsdynRouteto = queueRuleItem.MsdynRouteto.Value, RoutingRuleId = queueRuleItem.RoutingRuleId.Id, RoutingRuleItemId = queueRuleItem.Id, RoutedQueueId = queueRuleItem.RoutedQueueId.Id }, new RoutingRuleItem { Name = $"{userRuleItem.Name} Updated", MsdynRouteto = userRuleItem.MsdynRouteto.Value, RoutingRuleId = userRuleItem.RoutingRuleId.Id, RoutingRuleItemId = userRuleItem.Id, AssignObjectIdType = userRuleItem.AssignObjectId.LogicalName, AssignObjectIdName = userRuleItem.AssignObjectId.Name } } } }).Name,
                    FileDir = ArtifactDirectory
                }
            )).IsTrue();

        var updatedRule = context.GetById<RoutingRule>(routingRule.Id);
        await Assert.That(updatedRule.OwnerId.Id).IsEqualTo(routingRule.OwnerId.Id);
    }

    [Test]
    public async Task ShouldSkipAssignmentIfOwnerIsNotFoundInOrganization()
    {
        var data = GetData();
        var routingRule = data.routingRule;
        var context = GetBuilder()
            .WithData(routingRule)
            .Build();

        await Assert.That(context
            .Execute(new ImportVerb
                {
                    Assignee = "owner@test.de",
                    FileName = WriteConfigurationArtifact(new List<RoutingRuleConfig>
                        {
                            new()
                            {
                                Name = routingRule.Name,
                                Active = routingRule.StateCode.Value == RoutingRule.Options.StateCode.Active,
                                RoutingRuleId = routingRule.Id,
                                RoutingRuleItems = Array.Empty<RoutingRuleItem>()
                            }
                        }).Name,
                    FileDir = ArtifactDirectory
                }
            )).IsTrue();

        var updatedRule = context.GetById<RoutingRule>(routingRule.Id);
        await Assert.That(updatedRule.OwnerId.Id).IsEqualTo(routingRule.OwnerId.Id);
    }

    [Test]
    public async Task ShouldDeactivateActiveRoutingRule()
    {
        var data = GetData();
        var routingRule = data.routingRule;
        var context = GetBuilder()
            .WithData(routingRule)
            .Build();

        await Assert.That(context
            .Execute(new ImportVerb
                {
                    Assignee = "owner@test.de",
                    FileName = WriteConfigurationArtifact(new List<RoutingRuleConfig> { new() { Name = routingRule.Name, Active = false, RoutingRuleId = routingRule.Id, RoutingRuleItems = Array.Empty<RoutingRuleItem>() } }).Name,
                    FileDir = ArtifactDirectory
                }
            )).IsTrue();

        var updatedRule = context.GetById<RoutingRule>(routingRule.Id);
        await Assert.That(updatedRule.OwnerId.Id).IsEqualTo(routingRule.OwnerId.Id);
        await Assert.That(updatedRule.StateCode.Value).IsEqualTo(RoutingRule.Options.StateCode.Draft);
    }

    [Test]
    public async Task ShouldFailOnMissingRoutingRuleItemInOrganization()
    {
        var data = GetData();
        var routingRule = data.routingRule;
        var context = GetBuilder()
            .WithData(routingRule)
            .WithData(data.owner)
            .Build();

        await Assert.That(context
            .Execute(new ImportVerb
                {
                    Assignee = data.owner.DomainName,
                    FileName = WriteConfigurationArtifact(new List<RoutingRuleConfig> { new() { Name = routingRule.Name, Active = routingRule.StateCode.Value == RoutingRule.Options.StateCode.Active, RoutingRuleId = routingRule.Id, RoutingRuleItems = new[] { new RoutingRuleItem { Name = "No Existing Rule Item", RoutingRuleId = Guid.NewGuid(), MsdynRouteto = dataverse.RoutingRuleItem.Options.MsdynRouteto.Queue, RoutingRuleItemId = Guid.NewGuid() } } } }).Name,
                    FileDir = ArtifactDirectory
                }
            )).IsFalse();
    }

    private static (RoutingRule routingRule, SystemUser owner, dataverse.RoutingRuleItem queueRuleItem,
        dataverse.RoutingRuleItem teamRuleItem, dataverse.RoutingRuleItem userRuleItem, Entity[] entities)
        GetData()
    {
        var routingRule = new RoutingRule(Guid.NewGuid())
        {
            Name = "Existing Rule",
            StateCode = new OptionSetValue(RoutingRule.Options.StateCode.Active),
            StatusCode = new OptionSetValue(RoutingRule.Options.StatusCode.Active),
            OwnerId = new EntityReference(SystemUser.EntityLogicalName, Guid.NewGuid())
            {
                Name = "someone@test.de"
            }
        };
        var owner = new SystemUser(Guid.NewGuid())
        {
            DomainName = "owner@test.de"
        };
        var queue = new Queue(Guid.NewGuid())
        {
            Name = "Queue"
        };
        var team = new Team(Guid.NewGuid())
        {
            Name = "Team"
        };
        var user = new SystemUser(Guid.NewGuid())
        {
            [SystemUser.LogicalNames.FullName] = "Ulf User",
            DomainName = "user@test.de"
        };
        var queueRuleItem = new dataverse.RoutingRuleItem(Guid.NewGuid())
        {
            Name = "Existing Item Queue",
            MsdynRouteto = new OptionSetValue(dataverse.RoutingRuleItem.Options.MsdynRouteto.Queue),
            RoutingRuleId = routingRule.ToEntityReference(),
            RoutedQueueId = queue.ToEntityReference()
        };

        var teamRuleItem = new dataverse.RoutingRuleItem(Guid.NewGuid())
        {
            Name = "Existing Item Team",
            RoutingRuleId = routingRule.ToEntityReference(),
            MsdynRouteto = new OptionSetValue(dataverse.RoutingRuleItem.Options.MsdynRouteto.User_Team),
            AssignObjectId = new EntityReference(team.LogicalName, team.Id)
            {
                Name = team.Name
            }
        };

        var userRuleItem = new dataverse.RoutingRuleItem(Guid.NewGuid())
        {
            Name = "Existing Item Team",
            RoutingRuleId = routingRule.ToEntityReference(),
            MsdynRouteto = new OptionSetValue(dataverse.RoutingRuleItem.Options.MsdynRouteto.User_Team),
            AssignObjectId = new EntityReference(user.LogicalName, user.Id)
            {
                Name = user.DomainName
            }
        };

        return (routingRule, owner, queueRuleItem, teamRuleItem, userRuleItem, new Entity[]
        {
            queue,
            team,
            user
        });
    }


    [Test]
    public async Task ShouldFailOnMissingRuleInOrganization()
    {
        await Assert.That(GetContext().Execute(new ImportVerb
            {
                FileName = WriteConfigurationArtifact(new List<RoutingRuleConfig> { new() { Name = "Not Existing Rule", Active = true, RoutingRuleId = Guid.NewGuid(), RoutingRuleItems = new[] { new RoutingRuleItem { Name = "Rule Item 1", RoutingRuleId = Guid.NewGuid(), MsdynRouteto = dataverse.RoutingRuleItem.Options.MsdynRouteto.Queue, RoutingRuleItemId = Guid.NewGuid() } } } }).Name,
                FileDir = ArtifactDirectory
            }
        )).IsFalse();
    }
}
