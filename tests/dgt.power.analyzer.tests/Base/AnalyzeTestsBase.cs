using dgt.power.analyzer.Base;
using dgt.power.tests;
using Spectre.Console.Cli;

namespace dgt.power.analyzer.tests.Base;

public class AnalyzeTestsBase<TCommand> : CommandTestsBase<TCommand, AnalyzeVerb>
    where TCommand : class, ICommand<AnalyzeVerb>
{
    public AnalyzeTestsBase(ITestOutputHelper testOutputHelper) : base(testOutputHelper)
    {
    }
}
