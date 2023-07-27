// Copyright (c) DIGITALL Nature. All rights reserved
// DIGITALL Nature licenses this file to you under the Microsoft Public License.

using dgt.power.dataverse;
using dgt.power.dto;
using dgt.power.import.Base;
using dgt.power.import.Logic;
using dgt.power.import.tests.Base;
using FluentAssertions;
using Microsoft.Xrm.Sdk;
using Xunit.Abstractions;
using Calendar = dgt.power.dataverse.Calendar;
#pragma warning disable CS8602

namespace dgt.power.import.tests;

public class SlaConfigImportTests : ImportTestBase<SlaConfigImport>
{
    public SlaConfigImportTests(ITestOutputHelper testOutputHelper) : base(testOutputHelper)
    {
    }

    [Fact]
    public void ShouldFailOnWrongConfiguration() =>
        GetContext().Execute(new ImportVerb
            {
                FileName = string.Empty,
                FileDir = ArtifactDirectory
            }
        ).Should().BeFalse();

    [Fact]
    public void ShouldFailOnEmptyConfiguration() =>
        GetContext().Execute(new ImportVerb
        {
            FileName = WriteConfigurationArtifact(new SlaConfigs()).Name,
            FileDir = ArtifactDirectory
        }).Should().BeFalse();

    [Fact]
    public void ShouldUpdateSla()
    {
        var data = GetData();
        var sla1 = data.slaWithBusinessHours;
        var sla2 = data.slaWithoutBusinessHours;
        var context = GetBuilder()
            .WithData(data.owner)
            .WithData(data.calendar)
            .WithData(sla1)
            .WithData(sla2)
            .Build();

        var slaConfig = new SlaConfigs
        {
            new()
            {
                Active = true,
                BusinessHours = data.calendar.Id,
                SlaId = sla1.Id,
                Owner = data.owner.DomainName
            },
            new()
            {
                Active = false,
                BusinessHours = null,
                SlaId = sla2.Id,
                Owner = data.owner.DomainName
            }
        };
        context.Execute(new ImportVerb
        {
            FileName = WriteConfigurationArtifact(slaConfig).FullName,
            FileDir = ArtifactDirectory
        });

        var postSla1 = context.GetById<SLA>(sla1.Id);

        postSla1.StatusCode.Value.Should().Be(SLA.Options.StatusCode.Active);
        postSla1.BusinessHoursId.Id.Should().Be(data.calendar.Id);
        postSla1.OwnerId.Id.Should().Be(data.owner.Id);


        var postSla2 = context.GetById<SLA>(sla2.Id);

        postSla2.StatusCode.Value.Should().Be(SLA.Options.StatusCode.Draft);
        postSla2.BusinessHoursId.Should().BeNull();
        postSla2.OwnerId.Id.Should().Be(data.owner.Id);
    }
    
    [Fact]
    public void ShouldntCreateSlaIfNotExist()
    {
        var context = GetBuilder()
            .Build();

        var slaConfig = new SlaConfigs
        {
            new()
            {
                Active = false,
                BusinessHours = null,
                SlaId = Guid.NewGuid(),
                Owner = "someone"
            }
        };
        context.Execute(new ImportVerb
        {
            FileName = WriteConfigurationArtifact(slaConfig).FullName,
            FileDir = ArtifactDirectory
        });

        context.Get<SLA>().Should().BeEmpty();
    }
    

    private static (SystemUser owner, Calendar calendar, SLA slaWithBusinessHours, SLA slaWithoutBusinessHours)
        GetData()
    {
        var owner = new SystemUser(Guid.NewGuid())
        {
            DomainName = "owner@test.de"
        };
        var calendar = new Calendar(Guid.NewGuid());
        var sla1 = new SLA(Guid.NewGuid())
        {
            StateCode = new OptionSetValue(SLA.Options.StateCode.Active),
            StatusCode = new OptionSetValue(SLA.Options.StatusCode.Active),
        };
        var sla2 = new SLA(Guid.NewGuid())
        {
            StateCode = new OptionSetValue(SLA.Options.StateCode.Draft),
            StatusCode = new OptionSetValue(SLA.Options.StatusCode.Draft)
        };
        return (owner, calendar, sla1, sla2);
    }
}
