// Copyright (c) DIGITALL Nature. All rights reserved
// DIGITALL Nature licenses this file to you under the Microsoft Public License.

using dgt.power.solution.Base;
using Spectre.Console.Cli;

namespace dgt.power.solution.tests.Base;

public class LintTestsBase<TCommand> : CommandTestsBase<TCommand, SolutionLintSettings>
    where TCommand : class, ICommand<SolutionLintSettings>;
