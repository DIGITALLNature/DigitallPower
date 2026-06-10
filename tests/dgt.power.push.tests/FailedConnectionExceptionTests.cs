// Copyright (c) DIGITALL Nature. All rights reserved
// DIGITALL Nature licenses this file to you under the Microsoft Public License.

using dgt.power.common.Exceptions;

namespace dgt.power.push.tests;

public class FailedConnectionExceptionTests
{
    [Test]
    public async Task Constructor_WithEnvironment_ShouldFormatMessage()
    {
        var exception = new FailedConnectionException("dev");

        await Assert.That(exception.Message).IsEqualTo(FailedConnectionException.ErrorMessage("dev"));
    }

    [Test]
    public async Task Constructor_WithEnvironmentAndInnerException_ShouldKeepBothValues()
    {
        var innerException = new InvalidOperationException("inner");
        var exception = new FailedConnectionException("dev", innerException);

        await Assert.That(exception.Message).IsEqualTo(FailedConnectionException.ErrorMessage("dev"));
        await Assert.That(exception.InnerException).IsNotNull();
        await Assert.That(exception.InnerException!.Message).IsEqualTo("inner");
    }
}
