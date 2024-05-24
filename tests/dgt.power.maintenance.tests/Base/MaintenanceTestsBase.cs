// Copyright (c) DIGITALL Nature. All rights reserved
// DIGITALL Nature licenses this file to you under the Microsoft Public License.

using dgt.power.maintenance.Base;
using dgt.power.tests;
using Spectre.Console.Cli;

namespace dgt.power.maintenance.tests.Base;

public abstract class MaintenanceTestsBase<TCommand> : CommandTestsBase<TCommand, MaintenanceVerb>
    where TCommand : class, ICommand<MaintenanceVerb>
{
    public MaintenanceTestsBase(ITestOutputHelper testOutputHelper) : base(testOutputHelper)
    {
    }
}
