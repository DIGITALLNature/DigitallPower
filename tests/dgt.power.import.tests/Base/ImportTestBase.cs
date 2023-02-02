using dgt.power.common;
using dgt.power.import.Base;
using dgt.power.tests;
using Xunit.Abstractions;

namespace dgt.power.import.tests.Base;

public abstract class ImportTestBase<TCommand> : CommandTestsBase<TCommand, ImportVerb>
    where TCommand : PowerLogic<ImportVerb>
{
    protected ImportTestBase(ITestOutputHelper testOutputHelper) : base(testOutputHelper)
    {
    }
}
