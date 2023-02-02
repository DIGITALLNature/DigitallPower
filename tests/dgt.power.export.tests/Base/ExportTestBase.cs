using dgt.power.common;
using dgt.power.export.Base;
using dgt.power.tests;
using Xunit.Abstractions;

namespace dgt.power.export.tests.Base;

public abstract class ExportTestBase<TCommand> : CommandTestsBase<TCommand, ExportVerb> where TCommand : PowerLogic<ExportVerb>
{
    protected ExportTestBase(ITestOutputHelper testOutputHelper) : base(testOutputHelper)
    {
    }
}
