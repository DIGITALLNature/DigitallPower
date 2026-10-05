// Copyright (c) DIGITALL Nature. All rights reserved
// DIGITALL Nature licenses this file to you under the Microsoft Public License.

using dgt.power.webresource.Commands;

namespace dgt.power.webresource.tests.Commands;

public class WebResourcePushSettingsTests
{
    [Test]
    public async Task Validate_DirectoryWithPublisherPrefix_Succeeds()
    {
        var directory = Directory.CreateTempSubdirectory();
        try
        {
            var result = new WebResourcePushSettings
            {
                Target = directory.FullName,
                PublisherPrefix = "contoso"
            }.Validate();

            await Assert.That(result.Successful).IsTrue();
        }
        finally
        {
            directory.Delete(true);
        }
    }

    [Test]
    public async Task Validate_DirectoryWithoutPublisherPrefix_Fails()
    {
        var directory = Directory.CreateTempSubdirectory();
        try
        {
            var result = new WebResourcePushSettings
            {
                Target = directory.FullName
            }.Validate();

            await Assert.That(result.Successful).IsFalse();
            await Assert.That(result.Message).IsEqualTo("A directory target requires --publisher-prefix.");
        }
        finally
        {
            directory.Delete(true);
        }
    }

    [Test]
    public async Task Validate_SingleFileWithoutName_Fails()
    {
        var directory = Directory.CreateTempSubdirectory();
        try
        {
            var file = Path.Combine(directory.FullName, "main.js");
            await File.WriteAllTextAsync(file, "console.log('test');");

            var result = new WebResourcePushSettings
            {
                Target = file
            }.Validate();

            await Assert.That(result.Successful).IsFalse();
            await Assert.That(result.Message).IsEqualTo("A single-file target requires --name.");
        }
        finally
        {
            directory.Delete(true);
        }
    }

    [Test]
    public async Task Validate_DeleteObsoleteWithoutSolution_Fails()
    {
        var directory = Directory.CreateTempSubdirectory();
        try
        {
            var result = new WebResourcePushSettings
            {
                Target = directory.FullName,
                PublisherPrefix = "contoso",
                DeleteObsolete = true
            }.Validate();

            await Assert.That(result.Successful).IsFalse();
            await Assert.That(result.Message)
                .IsEqualTo("--delete-obsolete requires a directory Target and --solution.");
        }
        finally
        {
            directory.Delete(true);
        }
    }

    [Test]
    public async Task Validate_DeleteObsoleteWithDirectoryAndSolution_Succeeds()
    {
        var directory = Directory.CreateTempSubdirectory();
        try
        {
            var result = new WebResourcePushSettings
            {
                Target = directory.FullName,
                PublisherPrefix = "contoso",
                Solution = "ContosoCore",
                DeleteObsolete = true
            }.Validate();

            await Assert.That(result.Successful).IsTrue();
        }
        finally
        {
            directory.Delete(true);
        }
    }

    [Test]
    public async Task Validate_DeleteObsoleteWithSingleFile_Fails()
    {
        var directory = Directory.CreateTempSubdirectory();
        try
        {
            var file = Path.Combine(directory.FullName, "main.js");
            await File.WriteAllTextAsync(file, "console.log('test');");

            var result = new WebResourcePushSettings
            {
                Target = file,
                Name = "contoso_/main.js",
                Solution = "ContosoCore",
                DeleteObsolete = true
            }.Validate();

            await Assert.That(result.Successful).IsFalse();
            await Assert.That(result.Message)
                .IsEqualTo("--delete-obsolete requires a directory Target and --solution.");
        }
        finally
        {
            directory.Delete(true);
        }
    }
}
