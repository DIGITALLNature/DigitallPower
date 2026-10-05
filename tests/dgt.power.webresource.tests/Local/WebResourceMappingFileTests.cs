// Copyright (c) DIGITALL Nature. All rights reserved
// DIGITALL Nature licenses this file to you under the Microsoft Public License.

using dgt.power.webresource.Local;

namespace dgt.power.webresource.tests.Local;

public class WebResourceMappingFileTests
{
    [Test]
    public async Task Load_ValidMappings_ReturnsMappings()
    {
        var directory = Directory.CreateTempSubdirectory();
        try
        {
            var path = Path.Combine(directory.FullName, "mappings.json");
            await File.WriteAllTextAsync(path, """{"mappings":{"app/main.js":"contoso_/main.js"}}""");

            var result = WebResourceMappingFile.Load(path);

            await Assert.That(result.Mappings).Count().IsEqualTo(1);
            await Assert.That(result.Mappings["app/main.js"]).IsEqualTo("contoso_/main.js");
        }
        finally
        {
            directory.Delete(true);
        }
    }

    [Test]
    public async Task Load_NullMappings_ThrowsMappingException()
    {
        var directory = Directory.CreateTempSubdirectory();
        try
        {
            var path = Path.Combine(directory.FullName, "mappings.json");
            await File.WriteAllTextAsync(path, """{"mappings":null}""");

            await Assert.That(() => WebResourceMappingFile.Load(path))
                .Throws<WebResourceMappingException>();
        }
        finally
        {
            directory.Delete(true);
        }
    }

    [Test]
    public async Task Load_MissingMappings_ThrowsMappingException()
    {
        var directory = Directory.CreateTempSubdirectory();
        try
        {
            var path = Path.Combine(directory.FullName, "mappings.json");
            await File.WriteAllTextAsync(path, "{}");

            await Assert.That(() => WebResourceMappingFile.Load(path))
                .Throws<WebResourceMappingException>();
        }
        finally
        {
            directory.Delete(true);
        }
    }
}
