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
                .Build();

            // TODO enrich metadata 
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
