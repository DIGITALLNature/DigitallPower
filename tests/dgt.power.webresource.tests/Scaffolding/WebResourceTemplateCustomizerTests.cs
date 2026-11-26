// SPDX-FileCopyrightText: DIGITALL Nature
// SPDX-License-Identifier: MS-RL

using System.Text;
using dgt.power.webresource.Scaffolding;

namespace dgt.power.webresource.tests.Scaffolding;

public class WebResourceTemplateCustomizerTests
{
    [Test]
    public async Task Customize_ReplacesAllLocationsAndPreservesOtherContent()
    {
        var binary = new byte[] { 0 }
            .Concat(Encoding.UTF8.GetBytes("__DGTP_GLOBAL_NAME__"))
            .Append((byte)255)
            .ToArray();
        const string lockfile = "lockfile: unchanged\n";
        const string license = "template license\n";
        var archive = TemplateArchiveFixture.Create(
            new TemplateArchiveFixture.FixtureEntry("package.json", """{"name":"__DGTP_PROJECT_NAME__"}"""),
            new TemplateArchiveFixture.FixtureEntry("rolldown.config.js", "global: '__DGTP_GLOBAL_NAME__'; docs: '__DGTP_GLOBAL_NAME__';"),
            new TemplateArchiveFixture.FixtureEntry("README.md",
                "Package `__DGTP_PROJECT_NAME__` for `__DGTP_GLOBAL_NAME__`; defaults `__DGTP_SOLUTION__` / `__DGTP_PUBLISHER_PREFIX__`.\nTODO: configure models manually."),
            new TemplateArchiveFixture.FixtureEntry("scripts/push.ps1", "$connection = '__DGTP_CONNECTION_NAME__'; $solution = '__DGTP_SOLUTION__'; $prefix = '__DGTP_PUBLISHER_PREFIX__';"),
            new TemplateArchiveFixture.FixtureEntry("scripts/model.ps1", "$connection = '__DGTP_CONNECTION_NAME__';"),
            new TemplateArchiveFixture.FixtureEntry("LICENSE", license),
            new TemplateArchiveFixture.FixtureEntry("pnpm-lock.yaml", lockfile),
            new TemplateArchiveFixture.FixtureEntry("assets/icon.bin", binary));

        await WithExtractedArchive(archive, async directory =>
        {
            await WebResourceTemplateCustomizer.CustomizeAsync(
                directory,
                new WebResourceProjectValues("contoso-webresources", "Contoso", "ContosoCore", "contoso", "dev"),
                CancellationToken.None);

            var package = await File.ReadAllTextAsync(Path.Combine(directory, "package.json"));
            var bundler = await File.ReadAllTextAsync(Path.Combine(directory, "rolldown.config.js"));
            var readme = await File.ReadAllTextAsync(Path.Combine(directory, "README.md"));
            var scripts = await File.ReadAllTextAsync(Path.Combine(directory, "scripts", "push.ps1"));

            await Assert.That(package).IsEqualTo("""{"name":"contoso-webresources"}""");
            await Assert.That(bundler).IsEqualTo("global: 'Contoso'; docs: 'Contoso';");
            await Assert.That(readme).Contains("Package `contoso-webresources` for `Contoso`");
            await Assert.That(readme).Contains("defaults `ContosoCore` / `contoso`");
            await Assert.That(readme).Contains("TODO: configure models manually.");
            await Assert.That(scripts).IsEqualTo("$connection = 'dev'; $solution = 'ContosoCore'; $prefix = 'contoso';");
            await Assert.That(await File.ReadAllTextAsync(Path.Combine(directory, "scripts", "model.ps1")))
                .IsEqualTo("$connection = 'dev';");
            await Assert.That(await File.ReadAllTextAsync(Path.Combine(directory, "LICENSE"))).IsEqualTo(license);
            await Assert.That(await File.ReadAllTextAsync(Path.Combine(directory, "pnpm-lock.yaml"))).IsEqualTo(lockfile);
            await Assert.That(await File.ReadAllBytesAsync(Path.Combine(directory, "assets", "icon.bin")))
                .IsEquivalentTo(binary);
        });
    }

    [Test]
    [Arguments("webpack.config.js")]
    [Arguments("rolldown.config.js")]
    public async Task Customize_FindsGlobalMarkerWithoutBundlerFilenameAdapter(string configFile)
    {
        var archive = TemplateArchiveFixture.Create(
            new TemplateArchiveFixture.FixtureEntry(configFile, "global: '__DGTP_GLOBAL_NAME__';"),
            new TemplateArchiveFixture.FixtureEntry("package.json", "__DGTP_PROJECT_NAME__"),
            new TemplateArchiveFixture.FixtureEntry("setup.md", "__DGTP_CONNECTION_NAME__ __DGTP_SOLUTION__ __DGTP_PUBLISHER_PREFIX__"));

        await WithExtractedArchive(archive, async directory =>
        {
            await WebResourceTemplateCustomizer.CustomizeAsync(
                directory,
                new WebResourceProjectValues("contoso-webresources", "Contoso", null, null),
                CancellationToken.None);

            await Assert.That(await File.ReadAllTextAsync(Path.Combine(directory, configFile)))
                .IsEqualTo("global: 'Contoso';");
            await Assert.That(await File.ReadAllTextAsync(Path.Combine(directory, "setup.md")))
                .IsEqualTo("  ");
        });
    }

    [Test]
    public async Task Customize_ReportsMissingRequiredMarkerBeforeWriting()
    {
        const string original = "__DGTP_PROJECT_NAME__ __DGTP_GLOBAL_NAME__ __DGTP_SOLUTION__ __DGTP_PUBLISHER_PREFIX__";
        var archive = TemplateArchiveFixture.Create(new TemplateArchiveFixture.FixtureEntry("setup.txt", original));

        await WithExtractedArchive(archive, async directory =>
        {
            var exception = await Assert.That(async () =>
                    await WebResourceTemplateCustomizer.CustomizeAsync(
                        directory,
                        new WebResourceProjectValues("contoso-webresources", "Contoso", null, null),
                        CancellationToken.None))
                .Throws<WebResourceScaffoldingException>();

            await Assert.That(exception!.Message).Contains("__DGTP_CONNECTION_NAME__");
            await Assert.That(await File.ReadAllTextAsync(Path.Combine(directory, "setup.txt"))).IsEqualTo(original);
        });
    }

    [Test]
    public async Task Customize_ReportsUnknownReservedMarkersBeforeWriting()
    {
        const string original =
            "__DGTP_PROJECT_NAME__ __DGTP_GLOBAL_NAME__ __DGTP_CONNECTION_NAME__ __DGTP_SOLUTION__ __DGTP_PUBLISHER_PREFIX__ __DGTP_FUTURE__";
        var archive = TemplateArchiveFixture.Create(new TemplateArchiveFixture.FixtureEntry("setup.txt", original));

        await WithExtractedArchive(archive, async directory =>
        {
            var exception = await Assert.That(async () =>
                    await WebResourceTemplateCustomizer.CustomizeAsync(
                        directory,
                        new WebResourceProjectValues("contoso-webresources", "Contoso", null, null),
                        CancellationToken.None))
                .Throws<WebResourceScaffoldingException>();

            await Assert.That(exception!.Message).Contains("__DGTP_FUTURE__");
            await Assert.That(await File.ReadAllTextAsync(Path.Combine(directory, "setup.txt"))).IsEqualTo(original);
        });
    }

    [Test]
    public async Task Customize_EscapesValuesForPowerShellYamlAndMarkdownConventions()
    {
        var archive = TemplateArchiveFixture.Create(
            new TemplateArchiveFixture.FixtureEntry("package.json", "\"__DGTP_PROJECT_NAME__\""),
            new TemplateArchiveFixture.FixtureEntry("script.ps1", "'__DGTP_SOLUTION__' '__DGTP_CONNECTION_NAME__'"),
            new TemplateArchiveFixture.FixtureEntry("pipeline.yml", "\"__DGTP_PUBLISHER_PREFIX__\""),
            new TemplateArchiveFixture.FixtureEntry("README.md", "`__DGTP_SOLUTION__` __DGTP_GLOBAL_NAME__"));

        await WithExtractedArchive(archive, async directory =>
        {
            await WebResourceTemplateCustomizer.CustomizeAsync(
                directory,
                new WebResourceProjectValues("contoso-webresources", "Contoso", "O'Reilly \"Core\"", "Contoso \"Partners\"", "O'Reilly Dev"),
                CancellationToken.None);

            await Assert.That(await File.ReadAllTextAsync(Path.Combine(directory, "script.ps1")))
                .IsEqualTo("'O''Reilly \"Core\"' 'O''Reilly Dev'");
            await Assert.That(await File.ReadAllTextAsync(Path.Combine(directory, "pipeline.yml")))
                .IsEqualTo("\"Contoso \\u0022Partners\\u0022\"");
            await Assert.That(await File.ReadAllTextAsync(Path.Combine(directory, "README.md")))
                .IsEqualTo("`O'Reilly \"Core\"` Contoso");
        });
    }

    [Test]
    public async Task Customize_RejectsUnsafeValueOutsideQuotedInsertion()
    {
        var archive = TemplateArchiveFixture.Create(
            new TemplateArchiveFixture.FixtureEntry("package.json", "__DGTP_PROJECT_NAME__"),
            new TemplateArchiveFixture.FixtureEntry("setup.md",
                "__DGTP_GLOBAL_NAME__ __DGTP_CONNECTION_NAME__ __DGTP_SOLUTION__ __DGTP_PUBLISHER_PREFIX__"));

        await WithExtractedArchive(archive, async directory =>
        {
            var exception = await Assert.That(async () =>
                    await WebResourceTemplateCustomizer.CustomizeAsync(
                        directory,
                        new WebResourceProjectValues("contoso-webresources", "Contoso", "Contoso Core", null),
                        CancellationToken.None))
                .Throws<WebResourceScaffoldingException>();

            await Assert.That(exception!.Message).Contains("cannot be safely inserted");
            await Assert.That(await File.ReadAllTextAsync(Path.Combine(directory, "package.json")))
                .IsEqualTo("__DGTP_PROJECT_NAME__");
        });
    }

    [Test]
    [Arguments(null, null, null, "'' '' ''")]
    [Arguments("dev", null, null, "'dev' '' ''")]
    [Arguments(null, "solution", null, "'' 'solution' ''")]
    [Arguments(null, null, "prefix", "'' '' 'prefix'")]
    [Arguments("dev", "solution", null, "'dev' 'solution' ''")]
    [Arguments("dev", null, "prefix", "'dev' '' 'prefix'")]
    [Arguments(null, "solution", "prefix", "'' 'solution' 'prefix'")]
    [Arguments("dev", "solution", "prefix", "'dev' 'solution' 'prefix'")]
    public async Task Customize_OptionalWorkflowDefaultsAreIndependent(
        string? connection, string? solution, string? prefix, string expected)
    {
        var archive = TemplateArchiveFixture.Create(
            new TemplateArchiveFixture.FixtureEntry("setup.txt", "__DGTP_PROJECT_NAME__ __DGTP_GLOBAL_NAME__"),
            new TemplateArchiveFixture.FixtureEntry("defaults.ps1",
                "'__DGTP_CONNECTION_NAME__' '__DGTP_SOLUTION__' '__DGTP_PUBLISHER_PREFIX__'"));

        await WithExtractedArchive(archive, async directory =>
        {
            await WebResourceTemplateCustomizer.CustomizeAsync(
                directory,
                new WebResourceProjectValues("project", "Global", solution, prefix, connection),
                CancellationToken.None);

            await Assert.That(await File.ReadAllTextAsync(Path.Combine(directory, "defaults.ps1")))
                .IsEqualTo(expected);
        });
    }

    private static async Task WithExtractedArchive(byte[] archive, Func<string, Task> test)
    {
        var staging = Directory.CreateTempSubdirectory();
        try
        {
            await new WebResourceArchiveExtractor().ExtractAsync(archive, staging.FullName, CancellationToken.None);
            await test(staging.FullName);
        }
        finally
        {
            staging.Delete(recursive: true);
        }
    }
}
