using dgt.power.push.Base;
using dgt.power.push.tests.Base;
using dgt.power.tests;
using dgt.power.tests.FakeExecutor;
using FluentAssertions;
using Microsoft.Crm.Sdk.Messages;
using Xunit.Abstractions;

namespace dgt.power.push.tests;

public class PushTest : PushTestsBase<PushCommand>
{
    public PushTest(ITestOutputHelper testOutputHelper) : base(testOutputHelper)
    {
    }

    protected override CommandTestContext<PushCommand, PushVerb> GetContext()
    {
        return GetBuilder()
            .WithFakeMessageExecutor<AddSolutionComponentRequest>(new AddSolutionComponentExecutor())
            .Build();
    }

    [Fact]
    public void ShouldImportSample() =>
        GetContext()
            .Execute(new PushVerb
            {
                DllFile = "../../../../../samples/output/dgt.power.push.tests.sample.dll",
                Solution = "TestSolution"
            }).Should().BeTrue();

    [Fact]
    public void ShouldImportDependentSample() =>
        GetContext()
            .Execute(new PushVerb
            {
                DllFile = "../../../../../samples/output/dgt.power.push.tests.sample.1.0.0.nupkg",
                Solution = "TestSolution"
            }).Should().BeTrue();
}
