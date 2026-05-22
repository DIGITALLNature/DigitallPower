// Copyright (c) DIGITALL Nature. All rights reserved
// DIGITALL Nature licenses this file to you under the Microsoft Public License.

using dgt.power.dataverse;
using dgt.power.maintenance.Logic;
using dgt.power.maintenance.Model.Serialization;
using dgt.power.maintenance.Model.Settings;
using dgt.power.tests;
using dgt.power.tests.Extensions;
using Microsoft.Xrm.Sdk;
using Microsoft.Xrm.Sdk.Metadata;

namespace dgt.power.maintenance.tests;

public class ExportCarrierInfoTests : CommandTestsBase<ExportCarrierInfo, CarrierInfoSettings>
{
    protected override CommandTestContext<ExportCarrierInfo, CarrierInfoSettings> GetContext()
    {
        return GetBuilder()
            .WithMetaData(new EntityMetadata
            {
                LogicalName = DgtCarrier.EntityLogicalName
            })
            .Build();
    }

    [Test]
    public async Task ShouldFailValidationOnNotExistingCarrierEntity()
    {
        // Arrange
        var context = GetBuilder()
            .WithMetaData(new EntityMetadata
            {
                LogicalName = TestEntity.EntityLogicalName
            }) // don't load earlybound metadata.
            .Build();

        // Act
        var validationResult = context.Validate(new CarrierInfoSettings());

        // Assert
        await Assert.That(validationResult.Successful).IsFalse();
        await Assert.That(validationResult.Message).IsEqualTo(ExportCarrierInfo.ValidationErrorMessage);
    }

    [Test]
    public async Task ShouldSucceedValidation()
    {
        var context = GetContext();

        var validationResult = context.Validate(new CarrierInfoSettings());

        await Assert.That(validationResult.Successful).IsTrue();
    }

    [Test]
    public async Task ShouldFailOnMissingActiveCarriers()
    {
        var context = GetContext();

        await context.Execute(new CarrierInfoSettings()).Fail();
    }

    [Test]
    public async Task ShouldExportCarrierInfoInAscendingOrder()
    {
        var solutionId1 = Guid.NewGuid();
        var carrierSolution1 = new Solution(solutionId1)
        {
            SolutionId = solutionId1,
            UniqueName = "solution_a",
            FriendlyName = "Solution A",
            Version = "1.0.0.1"
        };
        var activeCarrier1 = new DgtCarrier(Guid.NewGuid())
        {
            DgtSolutionuniquename = carrierSolution1.UniqueName,
            DgtSolutionfriendlyname = carrierSolution1.FriendlyName,
            DgtSolutionversion = carrierSolution1.Version,
            DgtSolutionid = carrierSolution1.Id.ToString(),
            Statecode = new OptionSetValue(DgtCarrier.Options.Statecode.Active),
            DgtTransportOrderNo = 1,
        };
        var solutionId2 = Guid.NewGuid();
        var carrierSolution2 = new Solution(solutionId2)
        {
            SolutionId = solutionId2,
            UniqueName = "solution_b",
            FriendlyName = "Solution B",
            Version = "1.0.1.1"
        };
        var activeCarrier2 = new DgtCarrier(Guid.NewGuid())
        {
            DgtSolutionuniquename = carrierSolution2.UniqueName,
            DgtSolutionfriendlyname = carrierSolution2.FriendlyName,
            DgtSolutionversion = carrierSolution2.Version,
            DgtSolutionid = carrierSolution2.Id.ToString(),
            Statecode = new OptionSetValue(DgtCarrier.Options.Statecode.Active),
            DgtTransportOrderNo = 2,
        };
        var context = GetBuilder()
            .WithData(activeCarrier1)
            .WithData(carrierSolution1)
            .WithData(activeCarrier2)
            .WithData(carrierSolution2)
            .Build();

        var settings = new CarrierInfoSettings
        {
            FileDir = ArtifactDirectory
        };
        await context.Execute(settings).Succeed();

        var carriers = GetConfigurationTestArtifact<Carriers>(settings.FileName);

        await Assert.That(carriers).HasCount().EqualTo(2);
        await Assert.That(carriers.SequenceEqual(carriers.OrderBy(x => x.Order))).IsTrue();
        await Assert.That(carriers.Any(x =>
            x.UniqueName == carrierSolution1.UniqueName
            && x.FriendlyName == carrierSolution1.FriendlyName
            && x.Version == carrierSolution1.Version
            && x.SolutionId == carrierSolution1.SolutionId
            && x.CarrierId == activeCarrier1.Id
        )).IsTrue();
        await Assert.That(carriers.Any(x =>
            x.UniqueName == carrierSolution2.UniqueName
            && x.FriendlyName == carrierSolution2.FriendlyName
            && x.Version == carrierSolution2.Version
            && x.SolutionId == carrierSolution2.SolutionId
            && x.CarrierId == activeCarrier2.Id
        )).IsTrue();
    }
}
