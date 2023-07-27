// Copyright (c) DIGITALL Nature. All rights reserved
// DIGITALL Nature licenses this file to you under the Microsoft Public License.

using dgt.power.dataverse;
using dgt.power.dto;
using dgt.power.export.Base;
using dgt.power.export.Logic;
using dgt.power.export.tests.Base;
using dgt.power.tests;
using FluentAssertions;
using Microsoft.Xrm.Sdk;
using Xunit.Abstractions;

namespace dgt.power.export.tests;

public class SlaConfigExportTest : ExportTestBase<SlaConfigExport>
{
    public SlaConfigExportTest(ITestOutputHelper testOutputHelper) : base(testOutputHelper)
    {
    }

    protected override CommandTestContext<SlaConfigExport, ExportVerb> GetContext() =>
        GetBuilder()
            .WithData(new[]
            {
                new SLA(Guid.Parse("dd9dbcef-ca66-4edd-ba42-740f0c14f554"))
                {
                    Name = "Sla1 Draft",
                    StatusCode = new OptionSetValue(SLA.Options.StatusCode.Draft),
                    StateCode = new OptionSetValue(SLA.Options.StateCode.Draft),
                    BusinessHoursId = new EntityReference(dataverse.Calendar.EntityLogicalName, Guid.NewGuid()),
                },
                new SLA(Guid.Parse("6555bb4d-6c3a-454a-9bda-36ee77e7e0ed"))
                {
                    Name = "Sla1 Draft",
                    StatusCode = new OptionSetValue(SLA.Options.StatusCode.Active),
                    StateCode = new OptionSetValue(SLA.Options.StateCode.Active),
                }
            })
            .Build();

    [Fact]
    public void ShouldExportSlasWithDefaultConfiguration()
    {
        GetContext()
            .Execute(new ExportVerb
                {
                    FileName = GetTestFileName(),
                    FileDir = ArtifactDirectory,
                }
            ).Should().BeTrue();
        var slaConfigs = GetConfigurationTestArtifact<SlaConfigs>(GetTestFileName());
        slaConfigs.Should().HaveCount(2);
    }


    [Fact]
    public void ShouldUseDefaultOnEmptyFileName()
    {
        GetContext().Execute(new ExportVerb
            {
                FileName = string.Empty,
                FileDir = ArtifactDirectory,
            }
        ).Should().BeTrue();
        var slaConfigs = GetConfigurationTestArtifact<SlaConfigs>("slaconfig.json");
        slaConfigs.Should().HaveCount(2);
    }
}
