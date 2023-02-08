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
    public ExportCarrierInfoTests(ITestOutputHelper testOutputHelper) : base(testOutputHelper)
    {
    }

    [Fact]
    public void ShouldFailValidationOnNotExistingCarrierEntity()
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
        validationResult.Successful.Should().BeFalse();
        validationResult.Message.Should().Be(ExportCarrierInfo.ValidationErrorMessage);
    }

    [Fact]
    public void ShouldSucceedValidation()
    {
        var context = GetContext();

        var validationResult = context.Validate(new CarrierInfoSettings());

        validationResult.Successful.Should().BeTrue();
    }

    [Fact]
    public void ShouldFailOnMissingActiveCarriers()
    {
        var context = GetContext();

        context.Execute(new CarrierInfoSettings()).Should().Fail();
    }

    [Fact]
    public void ShouldExportCarrierInfoInAscendingOrder()
    {
        var carrierSolution1 = new Solution(Guid.NewGuid())
        {
            UniqueName = "solution_a",
            FriendlyName = "Solution A",
            Version = "1.0.0.1"
        };
        var activeCarrier1 = new Ec4uCarrier(Guid.NewGuid())
        {
            Ec4uCarSolutionuniquename = carrierSolution1.UniqueName,
            Ec4uCarSolutionfriendlyname = carrierSolution1.FriendlyName,
            Ec4uCarSolutionversion = carrierSolution1.Version,
            Ec4uCarSolutionid = carrierSolution1.Id.ToString(),
            Statecode = new OptionSetValue(Ec4uCarrier.Options.Statecode.Active),
            Ec4uCarTransportOrderNo = 1,
        };
        var carrierSolution2 = new Solution(Guid.NewGuid())
        {
            UniqueName = "solution_b",
            FriendlyName = "Solution B",
            Version = "1.0.1.1"
        };
        var activeCarrier2 = new Ec4uCarrier(Guid.NewGuid())
        {
            Ec4uCarSolutionuniquename = carrierSolution2.UniqueName,
            Ec4uCarSolutionfriendlyname = carrierSolution2.FriendlyName,
            Ec4uCarSolutionversion = carrierSolution2.Version,
            Ec4uCarSolutionid = carrierSolution2.Id.ToString(),
            Statecode = new OptionSetValue(Ec4uCarrier.Options.Statecode.Active),
            Ec4uCarTransportOrderNo = 2,
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
        context.Execute(settings).Should().Succeed();

        var carriers = GetConfigurationTestArtifact<Carriers>(settings.FileName);

        carriers.Should().HaveCount(2);
        carriers.Should().BeInAscendingOrder(x => x.Order);
        carriers.Should().Contain(x =>
            x.UniqueName == carrierSolution1.UniqueName
            && x.FriendlyName == carrierSolution1.FriendlyName
            && x.Version == carrierSolution1.Version
            && x.SolutionId == carrierSolution1.SolutionId
            && x.CarrierId == activeCarrier1.Id
        );
        carriers.Should().Contain(x =>
            x.UniqueName == carrierSolution2.UniqueName
            && x.FriendlyName == carrierSolution2.FriendlyName
            && x.Version == carrierSolution2.Version
            && x.SolutionId == carrierSolution2.SolutionId
            && x.CarrierId == activeCarrier2.Id
        );
    }
}
