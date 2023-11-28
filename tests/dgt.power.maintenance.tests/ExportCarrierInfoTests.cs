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
        var activeCarrier1 = new DgtCarrier(Guid.NewGuid())
        {
            DgtSolutionuniquename = carrierSolution1.UniqueName,
            DgtSolutionfriendlyname = carrierSolution1.FriendlyName,
            DgtSolutionversion = carrierSolution1.Version,
            DgtSolutionid = carrierSolution1.Id.ToString(),
            Statecode = new OptionSetValue(DgtCarrier.Options.Statecode.Active),
            DgtTransportOrderNo = 1,
        };
        var carrierSolution2 = new Solution(Guid.NewGuid())
        {
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
