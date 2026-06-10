// Copyright (c) DIGITALL Nature. All rights reserved
// DIGITALL Nature licenses this file to you under the Microsoft Public License.

using dgt.power.push.Model;

namespace dgt.power.push.tests;

public class AssemblyExceptionTests
{
    [Test]
    public async Task Constructor_WithMessage_ShouldKeepMessage()
    {
        var exception = new AssemblyException("assembly failed");

        await Assert.That(exception.Message).IsEqualTo("assembly failed");
    }

    [Test]
    public async Task Constructor_WithMessageAndInnerException_ShouldKeepBothValues()
    {
        var innerException = new InvalidOperationException("inner");
        var exception = new AssemblyException("assembly failed", innerException);

        await Assert.That(exception.Message).IsEqualTo("assembly failed");
        await Assert.That(exception.InnerException).IsNotNull();
        await Assert.That(exception.InnerException!.Message).IsEqualTo("inner");
    }
}
