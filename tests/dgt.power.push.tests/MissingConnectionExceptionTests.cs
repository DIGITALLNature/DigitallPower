// Copyright (c) DIGITALL Nature. All rights reserved
// DIGITALL Nature licenses this file to you under the Microsoft Public License.

using dgt.power.common.Exceptions;

namespace dgt.power.push.tests;

public class MissingConnectionExceptionTests
{
    [Test]
    public async Task Constructor_WithMessageAndInnerException_ShouldKeepBothValues()
    {
        var innerException = new InvalidOperationException("inner");
        var exception = new MissingConnectionException("custom message", innerException);

        await Assert.That(exception.Message).IsEqualTo("custom message");
        await Assert.That(exception.InnerException).IsNotNull();
        await Assert.That(exception.InnerException!.Message).IsEqualTo("inner");
    }
}
