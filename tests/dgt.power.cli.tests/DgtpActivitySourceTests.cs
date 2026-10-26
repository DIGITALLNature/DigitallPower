// SPDX-FileCopyrightText: DIGITALL Nature
// SPDX-License-Identifier: MS-RL

using System.Reflection;
using dgt.power.Telemetry;

namespace dgt.power.cli.tests;

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

    [Test]
    public async Task Instance_UsesInformationalVersion()
    {
        var expectedVersion = typeof(DgtpActivitySource).Assembly
            .GetCustomAttribute<AssemblyInformationalVersionAttribute>()?
            .InformationalVersion
            ?.Split('+', StringSplitOptions.RemoveEmptyEntries)[0]
            ?? typeof(DgtpActivitySource).Assembly.GetName().Version?.ToString()
            ?? "0.0.0";

        await Assert.That(DgtpActivitySource.Instance.Version).IsEqualTo(expectedVersion);
    }
}
