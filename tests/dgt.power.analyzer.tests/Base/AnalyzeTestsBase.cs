// Copyright (c) DIGITALL Nature. All rights reserved
// DIGITALL Nature licenses this file to you under the Microsoft Public License.

using dgt.power.analyzer.Base;
using dgt.power.tests;
using Microsoft.Extensions.DependencyInjection;
using Spectre.Console.Cli;

namespace dgt.power.analyzer.tests.Base;

public class AnalyzeTestsBase<TCommand> : CommandTestsBase<TCommand, AnalyzeVerb>
    where TCommand : class, ICommand<AnalyzeVerb>
{
    protected override CommandTestContextBuilder<TCommand, AnalyzeVerb> GetBuilder()
    {
        var services = new TestServiceCollection();
        services.AddSingleton<SolutionComponentReader>();
        return new CommandTestContextBuilder<TCommand, AnalyzeVerb>().WithServiceCollection(services).WithAnsiConsole(TestConsole);
    }
}
