// Copyright (c) DIGITALL Nature. All rights reserved
// DIGITALL Nature licenses this file to you under the Microsoft Public License.

using dgt.power.dataverse;
using dgt.power.dto;
using dgt.power.import.Base;
using dgt.power.import.Logic;
using dgt.power.import.tests.Base;
using Microsoft.Xrm.Sdk;
using Calendar = dgt.power.dataverse.Calendar;
#pragma warning disable CS8602

namespace dgt.power.import.tests;

public class SlaConfigImportTests : ImportTestBase<SlaConfigImport>
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
            FileName = WriteConfigurationArtifact(new SlaConfigs()).Name,
            FileDir = ArtifactDirectory
        })).IsFalse();

    [Test]
    public async Task ShouldUpdateSla()
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

        await Assert.That(postSla1.StatusCode.Value).IsEqualTo(SLA.Options.StatusCode.Active);
        await Assert.That(postSla1.BusinessHoursId.Id).IsEqualTo(data.calendar.Id);
        await Assert.That(postSla1.OwnerId.Id).IsEqualTo(data.owner.Id);


        var postSla2 = context.GetById<SLA>(sla2.Id);

        await Assert.That(postSla2.StatusCode.Value).IsEqualTo(SLA.Options.StatusCode.Draft);
        await Assert.That(postSla2.BusinessHoursId).IsNull();
        await Assert.That(postSla2.OwnerId.Id).IsEqualTo(data.owner.Id);
    }

    [Test]
    public async Task ShouldntCreateSlaIfNotExist()
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

        await Assert.That(context.Get<SLA>()).IsEmpty();
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
            StatusCode = new OptionSetValue(SLA.Options.StatusCode.Active)
        };
        var sla2 = new SLA(Guid.NewGuid())
        {
            StateCode = new OptionSetValue(SLA.Options.StateCode.Draft),
            StatusCode = new OptionSetValue(SLA.Options.StatusCode.Draft)
        };
        return (owner, calendar, sla1, sla2);
    }
}
