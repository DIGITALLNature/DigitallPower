// Copyright (c) DIGITALL Nature. All rights reserved
// DIGITALL Nature licenses this file to you under the Microsoft Public License.

using dgt.power.linter;
using Spectre.Console.Cli;

namespace dgt.power.linter.tests.Base;

public class LintTestsBase<TCommand> : CommandTestsBase<TCommand, LintVerb>
    where TCommand : class, ICommand<LintVerb>;
