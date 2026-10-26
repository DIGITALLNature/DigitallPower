// SPDX-FileCopyrightText: DIGITALL Nature
// SPDX-License-Identifier: MS-RL

using Spectre.Console.Cli;

namespace dgt.power.solution.tests.Base;

public class LintTestsBase<TCommand> : CommandTestsBase<TCommand, SolutionLintSettings>
    where TCommand : class, ICommand<SolutionLintSettings>;
