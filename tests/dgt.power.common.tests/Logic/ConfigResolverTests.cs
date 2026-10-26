// Copyright (c) DIGITALL Nature. All rights reserved
// DIGITALL Nature licenses this file to you under the Microsoft Public License.

using dgt.power.common.Logic;
using Spectre.Console.Testing;

namespace dgt.power.common.tests.Logic;

public class ConfigResolverTests
{
    [Test]
    public async Task TryConfigFromFile_EmptyPathThroughInterface_ReturnsEmptyConfiguration()
    {
        using var console = new TestConsole();
        IConfigResolver resolver = new ConfigResolver(console);

        var result = resolver.TryConfigFromFile<Dictionary<string, string>>(string.Empty, out var config);

        await Assert.That(result).IsTrue();
        await Assert.That(config).IsEmpty();
        await Assert.That(console.Output).IsEmpty();
    }
}
