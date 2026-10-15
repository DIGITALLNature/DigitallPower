// Copyright (c) DIGITALL Nature. All rights reserved
// DIGITALL Nature licenses this file to you under the Microsoft Public License.

using dgt.power.common;

namespace dgt.power.tests;

public sealed class TestSpanFactory : ICommandSpanFactory
{
    public ICommandSpan Start(string commandName) => new NoOpSpan();

    private sealed class NoOpSpan : ICommandSpan
    {
        public void Complete(bool success)
        {
        }

        public void Record(Exception exception)
        {
        }

        public void Dispose()
        {
        }
    }
}
