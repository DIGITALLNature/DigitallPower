// Copyright (c) DIGITALL Nature. All rights reserved
// DIGITALL Nature licenses this file to you under the Microsoft Public License.

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using dgt.power.maintenance.Base;
using dgt.power.maintenance.Logic;
using dgt.power.maintenance.tests.Base;
using dgt.power.tests.FakeExecutor;
using dgt.power.tests;
using Microsoft.Xrm.Sdk.Messages;
using Spectre.Console;
using Microsoft.Xrm.Sdk.Metadata;
using dgt.power.dataverse;
using FakeXrmEasy.Extensions;
using Microsoft.Xrm.Sdk;
using static dgt.power.dataverse.PluginAssembly.Options;

namespace dgt.power.maintenance.tests
{
    public class ProtectCalculatedFieldsTest : MaintenanceTestsBase<ProtectCalculatedFields>
    {
        public ProtectCalculatedFieldsTest(ITestOutputHelper testOutputHelper) : base(testOutputHelper)
        {

        }

        protected override CommandTestContext<ProtectCalculatedFields, MaintenanceVerb> GetContext()
        {
            return GetBuilder()
                .WithFakeMessageExecutor<UpdateAttributeRequest>(new UpdateAttributeExecutor())
                .WithFakeMessageExecutor<RetrieveAllEntitiesRequest>(new RetrieveAllEntitiesExecutor())
                .WithMetaData(BuildTestMetadata())
                .Build();

            // TODO enrich metadata
        }

        private static EntityMetadata BuildTestMetadata()
        {
            var metadata = new EntityMetadata
            {
                LogicalName = TestEntity.EntityLogicalName,
                MetadataId = Guid.NewGuid(),
            };

            metadata.SetSealedPropertyValue(nameof(metadata.ObjectTypeCode), TestEntity.EntityTypeCode);

            var nameAttributeMetadata = new StringAttributeMetadata { LogicalName = "name", SourceType = 0 };
            nameAttributeMetadata.SetSealedPropertyValue("IsManaged", false);
            nameAttributeMetadata.SetSealedPropertyValue("IsCustomizable", new BooleanManagedProperty(true){CanBeChanged = true});
            nameAttributeMetadata.SetSealedPropertyValue("EntityLogicalName", TestEntity.EntityLogicalName);

            var testMoneyCalcAttributeMetadata = new MoneyAttributeMetadata { LogicalName = "test_money_calc", SourceType = 1 };
            testMoneyCalcAttributeMetadata.SetSealedPropertyValue("IsManaged", false);
            testMoneyCalcAttributeMetadata.SetSealedPropertyValue("IsCustomizable", new BooleanManagedProperty(true) { CanBeChanged = true });
            testMoneyCalcAttributeMetadata.SetSealedPropertyValue("IsBaseCurrency", false);
            testMoneyCalcAttributeMetadata.SetSealedPropertyValue("EntityLogicalName", TestEntity.EntityLogicalName);

            var testMoneyCalcBaseAttributeMetadata = new MoneyAttributeMetadata { LogicalName = "test_money_base", SourceType = 1 };
            testMoneyCalcBaseAttributeMetadata.SetSealedPropertyValue("IsManaged", false);
            testMoneyCalcBaseAttributeMetadata.SetSealedPropertyValue("IsCustomizable", new BooleanManagedProperty(true) { CanBeChanged = true });
            testMoneyCalcBaseAttributeMetadata.SetSealedPropertyValue("IsBaseCurrency", true);
            testMoneyCalcBaseAttributeMetadata.SetSealedPropertyValue("EntityLogicalName", TestEntity.EntityLogicalName);

            var bpfDurationAttributeMetadata = new IntegerAttributeMetadata { LogicalName = "bpf_duration", SourceType = 1 };
            bpfDurationAttributeMetadata.SetSealedPropertyValue("IsManaged", false);
            bpfDurationAttributeMetadata.SetSealedPropertyValue("IsCustomizable", new BooleanManagedProperty(true) { CanBeChanged = true });
            bpfDurationAttributeMetadata.SetSealedPropertyValue("EntityLogicalName", TestEntity.EntityLogicalName);

            var testNumberCalcAttributeMetadata = new IntegerAttributeMetadata { LogicalName = "test_number_calc", SourceType = 1 };
            testNumberCalcAttributeMetadata.SetSealedPropertyValue("IsManaged", false);
            testNumberCalcAttributeMetadata.SetSealedPropertyValue("IsCustomizable", new BooleanManagedProperty(true) { CanBeChanged = true });
            testNumberCalcAttributeMetadata.SetSealedPropertyValue("EntityLogicalName", TestEntity.EntityLogicalName);

            var testNumberCalcManagedAttributeMetadata = new IntegerAttributeMetadata { LogicalName = "test_number_calc_managed", SourceType = 1 };
            testNumberCalcManagedAttributeMetadata.SetSealedPropertyValue("IsManaged", true);
            testNumberCalcManagedAttributeMetadata.SetSealedPropertyValue("IsCustomizable", new BooleanManagedProperty(true) { CanBeChanged = true });
            testNumberCalcManagedAttributeMetadata.SetSealedPropertyValue("EntityLogicalName", TestEntity.EntityLogicalName);

            var testNumberCalcAllreadySetAttributeMetadata = new IntegerAttributeMetadata { LogicalName = "test_number_calc_allready_set", SourceType = 1 };
            testNumberCalcAllreadySetAttributeMetadata.SetSealedPropertyValue("IsManaged", false);
            testNumberCalcAllreadySetAttributeMetadata.SetSealedPropertyValue("IsCustomizable", new BooleanManagedProperty(false) { CanBeChanged = true });
            testNumberCalcAllreadySetAttributeMetadata.SetSealedPropertyValue("EntityLogicalName", TestEntity.EntityLogicalName);

            var testNumberCalcNotCustomizableAttributeMetadata = new IntegerAttributeMetadata { LogicalName = "test_number_calc_not_customizable", SourceType = 1 };
            testNumberCalcNotCustomizableAttributeMetadata.SetSealedPropertyValue("IsManaged", false);
            testNumberCalcNotCustomizableAttributeMetadata.SetSealedPropertyValue("IsCustomizable", new BooleanManagedProperty(true){CanBeChanged = false});
            testNumberCalcNotCustomizableAttributeMetadata.SetSealedPropertyValue("EntityLogicalName", TestEntity.EntityLogicalName);

            metadata.SetAttributeCollection(new List<AttributeMetadata>
            {
                nameAttributeMetadata,
                testNumberCalcAttributeMetadata,
                testMoneyCalcAttributeMetadata,
                testMoneyCalcBaseAttributeMetadata,
                bpfDurationAttributeMetadata,
                testNumberCalcManagedAttributeMetadata,
                testNumberCalcAllreadySetAttributeMetadata,
                testNumberCalcNotCustomizableAttributeMetadata
            });

            return metadata;
        }

        [Fact]
        public void ShouldFindAndUpdateTwoFields()
        {
            AnsiConsole.Record();
            GetContext().Execute(new MaintenanceVerb()).Should().BeTrue();

            Assert.EndsWith("Protected 2 fields",AnsiConsole.ExportText());
        }
    }
}
