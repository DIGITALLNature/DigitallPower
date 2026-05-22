// Copyright (c) DIGITALL Nature. All rights reserved
// DIGITALL Nature licenses this file to you under the Microsoft Public License.

using dgt.power.push.Base;
using dgt.power.tests;
using Spectre.Console.Cli;

namespace dgt.power.push.tests.Base;

public class PushTestsBase<TCommand> : CommandTestsBase<TCommand, PushVerb>
    where TCommand : class, ICommand<PushVerb>
{
}
