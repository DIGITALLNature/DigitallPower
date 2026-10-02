// Copyright (c) DIGITALL Nature. All rights reserved
// DIGITALL Nature licenses this file to you under the Microsoft Public License.

using dgt.power.webresource.Local;

namespace dgt.power.webresource.tests.Local;

public class WebResourceDiscoveryTests
{
    [Test]
    public async Task Discover_Directory_UsesPrefixAndMappings()
    {
        var root = Directory.CreateTempSubdirectory();
        try
        {
            Directory.CreateDirectory(Path.Combine(root.FullName, "app"));
            await File.WriteAllTextAsync(Path.Combine(root.FullName, "app", "main.js"), "console.log('test');");
            await File.WriteAllTextAsync(Path.Combine(root.FullName, "index.html"), "<html />");

            var resources = WebResourceDiscovery.Discover(
                root.FullName,
                "contoso",
                new Dictionary<string, string>
                {
                    ["app/main.js"] = "contoso_/scripts/main.js"
                });

            await Assert.That(resources).Count().IsEqualTo(2);
            await Assert.That(resources.Single(resource => resource.Type == 3).Name)
                .IsEqualTo("contoso_/scripts/main.js");
            await Assert.That(resources.Single(resource => resource.Type == 1).Name)
                .IsEqualTo("contoso_/index.html");
        }
        finally
        {
            root.Delete(true);
        }
    }

    [Test]
    public async Task Discover_SingleFile_RequiresAndUsesExplicitName()
    {
        var root = Directory.CreateTempSubdirectory();
        try
        {
            var file = Path.Combine(root.FullName, "main.js");
            await File.WriteAllTextAsync(file, "console.log('test');");

            var resources = WebResourceDiscovery.Discover(file, singleResourceName: "contoso_/app/main.js");

            await Assert.That(resources).Count().IsEqualTo(1);
            await Assert.That(resources[0].Name).IsEqualTo("contoso_/app/main.js");
            await Assert.That(resources[0].DisplayName).IsEqualTo("main.js");
        }
        finally
        {
            root.Delete(true);
        }
    }

    [Test]
    public async Task Discover_Directory_RejectsDuplicateMappedNames()
    {
        var root = Directory.CreateTempSubdirectory();
        try
        {
            await File.WriteAllTextAsync(Path.Combine(root.FullName, "one.js"), "one");
            await File.WriteAllTextAsync(Path.Combine(root.FullName, "two.js"), "two");

            var action = () => WebResourceDiscovery.Discover(
                root.FullName,
                "contoso",
                new Dictionary<string, string>
                {
                    ["one.js"] = "contoso_/same.js",
                    ["two.js"] = "contoso_/same.js"
                });

            await Assert.That(action).Throws<ArgumentException>();
        }
        finally
        {
            root.Delete(true);
        }
    }
}
