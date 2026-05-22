// Copyright (c) DIGITALL Nature. All rights reserved
// DIGITALL Nature licenses this file to you under the Microsoft Public License.

using dgt.power.common;
using dgt.power.export.Base;
using dgt.power.tests;

namespace dgt.power.export.tests.Base;

public abstract class ExportTestBase<TCommand> : CommandTestsBase<TCommand, ExportVerb> where TCommand : PowerLogic<ExportVerb>
{
}
