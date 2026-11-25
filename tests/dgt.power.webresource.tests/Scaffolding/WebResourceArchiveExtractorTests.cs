// SPDX-FileCopyrightText: DIGITALL Nature
// SPDX-License-Identifier: MS-RL

using dgt.power.webresource.Scaffolding;

namespace dgt.power.webresource.tests.Scaffolding;

public class WebResourceArchiveExtractorTests
{
    [Test]
    public async Task Extract_PreservesDotfilesLicenseLockfileAndUnrelatedContent()
    {
        var archive = TemplateArchiveFixture.Create(
            new TemplateArchiveFixture.FixtureEntry(".config/local.json", "{}"),
            new TemplateArchiveFixture.FixtureEntry("LICENSE", "license"),
            new TemplateArchiveFixture.FixtureEntry("pnpm-lock.yaml", "lockfile"),
            new TemplateArchiveFixture.FixtureEntry("webpack.config.js", "marker stays in its own file"),
            new TemplateArchiveFixture.FixtureEntry("assets/icon.bin", new byte[] { 0, 1, 2, 255 }));
        var staging = Directory.CreateTempSubdirectory();
        try
        {
            await new WebResourceArchiveExtractor().ExtractAsync(archive, staging.FullName, CancellationToken.None);

            await Assert.That(File.ReadAllText(Path.Combine(staging.FullName, ".config", "local.json")))
                .IsEqualTo("{}");
            await Assert.That(File.ReadAllText(Path.Combine(staging.FullName, "LICENSE"))).IsEqualTo("license");
            await Assert.That(File.ReadAllText(Path.Combine(staging.FullName, "pnpm-lock.yaml"))).IsEqualTo("lockfile");
            await Assert.That(File.ReadAllText(Path.Combine(staging.FullName, "webpack.config.js")))
                .IsEqualTo("marker stays in its own file");
            await Assert.That(File.ReadAllBytes(Path.Combine(staging.FullName, "assets", "icon.bin")))
                .IsEquivalentTo(new byte[] { 0, 1, 2, 255 });
            await Assert.That(Directory.Exists(Path.Combine(staging.FullName, "webresources-template-test")))
                .IsFalse();
        }
        finally
        {
            staging.Delete(recursive: true);
        }
    }

    [Test]
    public async Task Extract_RejectsTraversalWithoutWritingOutsideStaging()
    {
        var archive = TemplateArchiveFixture.Create(new TemplateArchiveFixture.FixtureEntry("../escaped.txt", "unsafe"));
        var parent = Directory.CreateTempSubdirectory();
        var staging = Path.Combine(parent.FullName, "stage");
        try
        {
            await Assert.That(async () =>
                    await new WebResourceArchiveExtractor().ExtractAsync(archive, staging, CancellationToken.None))
                .Throws<WebResourceScaffoldingException>();

            await Assert.That(File.Exists(Path.Combine(parent.FullName, "escaped.txt"))).IsFalse();
            await Assert.That(Directory.Exists(staging)).IsFalse();
        }
        finally
        {
            parent.Delete(recursive: true);
        }
    }

    [Test]
    public async Task Extract_RejectsRootedArchivePaths()
    {
        var archive = TemplateArchiveFixture.CreateRaw(
            new TemplateArchiveFixture.FixtureEntry("C:/outside.txt", "unsafe"));
        var staging = Directory.CreateTempSubdirectory();
        try
        {
            await Assert.That(async () =>
                    await new WebResourceArchiveExtractor().ExtractAsync(archive, staging.FullName, CancellationToken.None))
                .Throws<WebResourceScaffoldingException>();
        }
        finally
        {
            staging.Delete(recursive: true);
        }
    }

    [Test]
    public async Task Extract_RejectsCaseInsensitivePathCollisions()
    {
        var archive = TemplateArchiveFixture.Create(
            new TemplateArchiveFixture.FixtureEntry("Readme.txt", "one"),
            new TemplateArchiveFixture.FixtureEntry("README.TXT", "two"));
        var staging = Directory.CreateTempSubdirectory();
        try
        {
            await Assert.That(async () =>
                    await new WebResourceArchiveExtractor().ExtractAsync(archive, staging.FullName, CancellationToken.None))
                .Throws<WebResourceScaffoldingException>();
            await Assert.That(Directory.GetFiles(staging.FullName, "*", SearchOption.AllDirectories).Length).IsZero();
        }
        finally
        {
            staging.Delete(recursive: true);
        }
    }

    [Test]
    public async Task Extract_RejectsSymbolicLinkEntries()
    {
        const int unixSymbolicLinkMode = unchecked((int)(0xA000u << 16));
        var archive = TemplateArchiveFixture.Create(
            new TemplateArchiveFixture.FixtureEntry("linked", "target", unixSymbolicLinkMode));
        var staging = Directory.CreateTempSubdirectory();
        try
        {
            await Assert.That(async () =>
                    await new WebResourceArchiveExtractor().ExtractAsync(archive, staging.FullName, CancellationToken.None))
                .Throws<WebResourceScaffoldingException>();
            await Assert.That(File.Exists(Path.Combine(staging.FullName, "linked"))).IsFalse();
        }
        finally
        {
            staging.Delete(recursive: true);
        }
    }

    [Test]
    public async Task Extract_RejectsCorruptArchives()
    {
        var staging = Directory.CreateTempSubdirectory();
        try
        {
            await Assert.That(async () =>
                    await new WebResourceArchiveExtractor().ExtractAsync([1, 2, 3], staging.FullName, CancellationToken.None))
                .Throws<InvalidDataException>();
        }
        finally
        {
            staging.Delete(recursive: true);
        }
    }

    [Test]
    public async Task Extract_EnforcesEntryAndExpandedSizeLimits()
    {
        var archive = TemplateArchiveFixture.Create(
            new TemplateArchiveFixture.FixtureEntry("one.txt", "12345"),
            new TemplateArchiveFixture.FixtureEntry("two.txt", "67890"));
        var staging = Directory.CreateTempSubdirectory();
        try
        {
            await Assert.That(async () =>
                    await new WebResourceArchiveExtractor(maximumEntryCount: 2)
                        .ExtractAsync(archive, staging.FullName, CancellationToken.None))
                .Throws<WebResourceScaffoldingException>();
            await Assert.That(async () =>
                    await new WebResourceArchiveExtractor(maximumExpandedBytes: 8)
                        .ExtractAsync(archive, staging.FullName, CancellationToken.None))
                .Throws<WebResourceScaffoldingException>();
        }
        finally
        {
            staging.Delete(recursive: true);
        }
    }
}
