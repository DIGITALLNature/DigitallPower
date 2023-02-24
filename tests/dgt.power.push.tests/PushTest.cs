using dgt.power.push.Base;
using dgt.power.push.tests.Base;
using dgt.power.push.tests.sample;
using FluentAssertions;
using Xunit.Abstractions;

namespace dgt.power.push.tests;

public class PushTest : PushTestsBase<PushCommand>
{
    public PushTest(ITestOutputHelper testOutputHelper) : base(testOutputHelper)
    {
    }

    [Fact]
    public void ShouldImportSample() =>
        GetContext()
            .Execute(new PushVerb
            {
                DllFile = typeof(SamplePlugin).Assembly.Location,
                Solution = "TestSolution"
            }).Should().BeTrue();
}
