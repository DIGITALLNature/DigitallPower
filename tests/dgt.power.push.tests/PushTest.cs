// Copyright (c) DIGITALL Nature. All rights reserved
// DIGITALL Nature licenses this file to you under the Microsoft Public License.

using dgt.power.dataverse;
using dgt.power.push.Base;
using dgt.power.push.Logic;
using dgt.power.push.tests.Base;
using dgt.power.tests;
using dgt.power.tests.FakeExecutor;
using Microsoft.Extensions.DependencyInjection;

namespace dgt.power.push.tests;

public class PushTest : PushTestsBase<PushCommand>
{
    protected override CommandTestContext<PushCommand, PushVerb> GetContext()
    {
        var serviceCollection = new TestServiceCollection()
            .AddScoped<WebresourcesProcessor>();

        return GetBuilder()
            .WithServiceCollection(serviceCollection)
            .WithFakeMessageExecutor(new AddSolutionComponentExecutor())
            .WithData(new SolutionComponentDefinition
            {
                Id = Guid.NewGuid(),
                SolutionComponentType = 10119,
                PrimaryEntityName = PluginPackage.EntityLogicalName
            })
            .Build();
    }

    [Test]
    public async Task ShouldImportSample() =>
        await Assert.That(GetContext()
            .Execute(new PushVerb
            {
                Target= "../../../../../samples/output/dgt.power.push.tests.sample.dll",
                Solution = "TestSolution"
            })).IsTrue();

    [Test]
    public async Task ShouldImportDependentSample() =>
        await Assert.That(GetContext()
            .Execute(new PushVerb
            {
                Target = "../../../../../samples/output/dgt.power.push.tests.sample.1.0.0.nupkg",
                Solution = "TestSolution"
            })).IsTrue();
}
