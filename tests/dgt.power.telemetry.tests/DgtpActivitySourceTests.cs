// Copyright (c) DIGITALL Nature. All rights reserved
// DIGITALL Nature licenses this file to you under the Microsoft Public License.

using dgt.power.Telemetry;

namespace dgt.power.telemetry.tests;

public class DgtpActivitySourceTests
{
    [Test]
    public async Task Instance_HasCorrectName()
    {
        await Assert.That(DgtpActivitySource.Name).IsEqualTo("dgt.power");
    }

    [Test]
    public async Task Instance_HasCorrectSourceName()
    {
        await Assert.That(DgtpActivitySource.Instance.Name).IsEqualTo("dgt.power");
    }

    [Test]
    public async Task Instance_HasNonNullVersion()
    {
        await Assert.That(DgtpActivitySource.Instance.Version).IsNotNull();
    }
}
