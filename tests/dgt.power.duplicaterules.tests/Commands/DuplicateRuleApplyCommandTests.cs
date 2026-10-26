// Copyright (c) DIGITALL Nature. All rights reserved

using dgt.power.common;
using dgt.power.dataverse;
using dgt.power.duplicaterules.Commands;
using dgt.power.tests;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Microsoft.Xrm.Sdk;
using Spectre.Console.Testing;

namespace dgt.power.duplicaterules.tests.Commands;

public class DuplicateRuleApplyCommandTests
{
    [Test]
    public async Task Apply_DryRun_ReportsPlanWithoutChangingRule()
    {
        var ruleId = Guid.NewGuid();
        var path = WriteConfiguration($$"""{"schemaVersion":1,"rules":[{"ruleId":"{{ruleId:D}}","published":false}]}""");
        using var console = new TestConsole();
        console.Profile.Width = 1000;
        var context = CreateContext(console, ruleId);
        try
        {
            var succeeded = context.Execute(new DuplicateRuleApplySettings { File = path, DryRun = true });
            var rule = context.FakedService.Retrieve(DuplicateRule.EntityLogicalName, ruleId, new Microsoft.Xrm.Sdk.Query.ColumnSet(true));

            await Assert.That(succeeded).IsTrue();
            await Assert.That(console.Output).Contains("DRY RUN");
            await Assert.That(console.Output).Contains("unpublish");
            await Assert.That(rule.GetAttributeValue<OptionSetValue>("statuscode").Value).IsEqualTo(DuplicateRule.Options.StatusCode.Published);
        }
        finally
        {
            File.Delete(path);
        }
    }

    [Test]
    public async Task Apply_InvalidConfiguration_FailsWithoutChangingRule()
    {
        var ruleId = Guid.NewGuid();
        var path = WriteConfiguration("""{"schemaVersion":2,"rules":[]}""");
        using var console = new TestConsole();
        console.Profile.Width = 1000;
        var context = CreateContext(console, ruleId);
        try
        {
            var succeeded = context.Execute(new DuplicateRuleApplySettings { File = path });

            await Assert.That(succeeded).IsFalse();
            await Assert.That(console.Output).Contains("Invalid duplicate-rule input");
        }
        finally
        {
            File.Delete(path);
        }
    }

    private static string WriteConfiguration(string json)
    {
        var path = Path.Combine(Environment.CurrentDirectory, $"duplicaterules-apply-{Guid.NewGuid():N}.json");
        File.WriteAllText(path, json);
        return path;
    }

    private static CommandTestContext<DuplicateRuleApplyCommand, DuplicateRuleApplySettings> CreateContext(TestConsole console, Guid ruleId)
    {
        var services = new TestServiceCollection();
        services.RemoveAll<IDataverseConnection>();
        services.AddSingleton<IDataverseConnection, TestConnection>();
        var ownerId = Guid.NewGuid();
        return new CommandTestContextBuilder<DuplicateRuleApplyCommand, DuplicateRuleApplySettings>()
            .WithServiceCollection(services)
            .WithAnsiConsole(console)
            .WithData(
            [
                new Entity("systemuser", ownerId) { ["domainname"] = "owner@example.org", ["isdisabled"] = false },
                new Entity(DuplicateRule.EntityLogicalName, ruleId)
                {
                    ["name"] = "Contacts with the same e-mail address",
                    ["baseentityname"] = "contact",
                    ["statecode"] = new OptionSetValue(DuplicateRule.Options.StateCode.Active),
                    ["statuscode"] = new OptionSetValue(DuplicateRule.Options.StatusCode.Published),
                    ["ownerid"] = new EntityReference("systemuser", ownerId)
                }
            ])
            .Build();
    }
}
