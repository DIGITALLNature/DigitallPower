// SPDX-FileCopyrightText: DIGITALL Nature
// SPDX-License-Identifier: MS-RL

using System.Net;
using System.Text;
using dgt.power.webresource.Scaffolding;

namespace dgt.power.webresource.tests.Scaffolding;

public class WebResourceProjectScaffolderTests
{
    [Test]
    [Arguments(false)]
    [Arguments(true)]
    public async Task Create_PublishesProjectToAbsentOrEmptyDestination(bool createDestination)
    {
        var parent = Directory.CreateTempSubdirectory("scaffold with spaces ");
        try
        {
            var destination = Path.Combine(parent.FullName, "generated project");
            if (createDestination)
            {
                Directory.CreateDirectory(destination);
            }

            using var client = CreateTemplateClient();
            var progress = new List<WebResourceScaffoldingProgress>();
            var sha = await new WebResourceProjectScaffolder(client).CreateAsync(
                destination,
                null,
                Values,
                CancellationToken.None,
                progress.Add);

            await Assert.That(sha).IsEqualTo(CommitSha);
            await Assert.That(progress[^1].CurrentStage).IsEqualTo(WebResourceScaffoldingProgress.Stage.Creating);
            await Assert.That(await File.ReadAllTextAsync(Path.Combine(destination, "README.md")))
                .IsEqualTo("contoso-webresources Contoso");
            await Assert.That(Directory.EnumerateFileSystemEntries(parent.FullName)
                    .Any(path => Path.GetFileName(path).StartsWith(".dgtp-webresource-", StringComparison.Ordinal)))
                .IsFalse();
        }
        finally
        {
            parent.Delete(recursive: true);
        }
    }

    [Test]
    public async Task Create_RejectsHiddenDestinationContentWithoutChangingIt()
    {
        var parent = Directory.CreateTempSubdirectory();
        try
        {
            var destination = Directory.CreateDirectory(Path.Combine(parent.FullName, "existing"));
            const string hiddenFile = ".user-config";
            await File.WriteAllTextAsync(Path.Combine(destination.FullName, hiddenFile), "preserve");
            using var client = CreateTemplateClient();
            var scaffolder = new WebResourceProjectScaffolder(client);

            var exception = await Assert.That(async () =>
                    await scaffolder.CreateAsync(
                        destination.FullName,
                        null,
                        Values,
                        CancellationToken.None))
                .Throws<WebResourceScaffoldingException>();

            await Assert.That(exception!.Message).Contains("absent or an empty directory");
            await Assert.That(await File.ReadAllTextAsync(Path.Combine(destination.FullName, hiddenFile)))
                .IsEqualTo("preserve");
        }
        finally
        {
            parent.Delete(recursive: true);
        }
    }

    [Test]
    public async Task Create_RejectsFileDestinationAndFileAncestor()
    {
        var parent = Directory.CreateTempSubdirectory();
        try
        {
            var filePath = Path.Combine(parent.FullName, "destination");
            await File.WriteAllTextAsync(filePath, "preserve");
            var nestedPath = Path.Combine(filePath, "project");
            using var client = CreateTemplateClient();
            var scaffolder = new WebResourceProjectScaffolder(client);

            var destinationException = await Assert.That(async () =>
                    await scaffolder.CreateAsync(filePath, null, Values, CancellationToken.None))
                .Throws<WebResourceScaffoldingException>();
            var ancestorException = await Assert.That(async () =>
                    await scaffolder.CreateAsync(nestedPath, null, Values, CancellationToken.None))
                .Throws<WebResourceScaffoldingException>();

            await Assert.That(destinationException!.Message).Contains("is a file");
            await Assert.That(ancestorException!.Message).Contains("non-directory ancestor");
            await Assert.That(await File.ReadAllTextAsync(filePath)).IsEqualTo("preserve");
        }
        finally
        {
            parent.Delete(recursive: true);
        }
    }

    [Test]
    public async Task Create_RejectsSymlinkDestinationWithoutChangingTarget()
    {
        var parent = Directory.CreateTempSubdirectory();
        try
        {
            var target = Directory.CreateDirectory(Path.Combine(parent.FullName, "target"));
            const string content = "preserve";
            await File.WriteAllTextAsync(Path.Combine(target.FullName, "existing.txt"), content);
            var link = Path.Combine(parent.FullName, "link");
            try
            {
                Directory.CreateSymbolicLink(link, target.FullName);
            }
            catch (Exception exception) when (
                exception is UnauthorizedAccessException or IOException or PlatformNotSupportedException)
            {
                return;
            }

            using var client = CreateTemplateClient();
            var scaffolder = new WebResourceProjectScaffolder(client);
            var destinationFailure = await Assert.That(async () =>
                    await scaffolder.CreateAsync(
                        link,
                        null,
                        Values,
                        CancellationToken.None))
                .Throws<WebResourceScaffoldingException>();
            await Assert.That(destinationFailure!.Message).Contains("symbolic link or reparse point");
            await Assert.That(await File.ReadAllTextAsync(Path.Combine(target.FullName, "existing.txt")))
                .IsEqualTo(content);
        }
        finally
        {
            parent.Delete(recursive: true);
        }
    }

    [Test]
    [Arguments(false)]
    [Arguments(true)]
    public async Task Create_AllowsSymlinkedAncestor(bool createDestination)
    {
        var parent = Directory.CreateTempSubdirectory();
        try
        {
            var target = Directory.CreateDirectory(Path.Combine(parent.FullName, "target"));
            var link = Path.Combine(parent.FullName, "link");
            try
            {
                Directory.CreateSymbolicLink(link, target.FullName);
            }
            catch (Exception exception) when (
                exception is UnauthorizedAccessException or IOException or PlatformNotSupportedException)
            {
                return;
            }

            var destination = Path.Combine(link, "project");
            if (createDestination)
            {
                Directory.CreateDirectory(destination);
            }

            using var client = CreateTemplateClient();
            await new WebResourceProjectScaffolder(client).CreateAsync(
                destination, null, Values, CancellationToken.None);

            await Assert.That(await File.ReadAllTextAsync(Path.Combine(target.FullName, "project", "README.md")))
                .IsEqualTo("contoso-webresources Contoso");
            await Assert.That(Directory.EnumerateFileSystemEntries(target.FullName)
                    .Any(path => Path.GetFileName(path).StartsWith(".dgtp-webresource-", StringComparison.Ordinal)))
                .IsFalse();
        }
        finally
        {
            parent.Delete(recursive: true);
        }
    }

    [Test]
    public async Task Create_PreservesCompetingWriteMadeDuringDownload()
    {
        var parent = Directory.CreateTempSubdirectory();
        try
        {
            var destination = Path.Combine(parent.FullName, "competing");
            var requestCount = 0;
            using var client = CreateTemplateClient(respond: (request, _) =>
            {
                requestCount++;
                if (request.RequestUri!.AbsolutePath.EndsWith("/commits/main", StringComparison.Ordinal))
                {
                    Directory.CreateDirectory(destination);
                    File.WriteAllText(Path.Combine(destination, "other-process.txt"), "preserve");
                }

                return Task.FromResult(TemplateResponse(request));
            });
            var scaffolder = new WebResourceProjectScaffolder(client);

            var failure = await Assert.That(async () =>
                    await scaffolder.CreateAsync(
                        destination,
                        null,
                        Values,
                        CancellationToken.None))
                .Throws<WebResourceScaffoldingException>();

            await Assert.That(failure!.Message).Contains("absent or an empty directory");
            await Assert.That(await File.ReadAllTextAsync(Path.Combine(destination, "other-process.txt")))
                .IsEqualTo("preserve");
            await Assert.That(requestCount).IsEqualTo(2);
        }
        finally
        {
            parent.Delete(recursive: true);
        }
    }

    [Test]
    public async Task Create_FailureLeavesDestinationAbsentOrEmptyAndCleansStaging()
    {
        var parent = Directory.CreateTempSubdirectory();
        try
        {
            var absentDestination = Path.Combine(parent.FullName, "absent");
            var emptyDestination = Directory.CreateDirectory(Path.Combine(parent.FullName, "empty"));
            using var client = CreateTemplateClient(archive: TemplateArchiveFixture.Create(
                new TemplateArchiveFixture.FixtureEntry("README.md", "__DGTP_PROJECT_NAME__")));
            var scaffolder = new WebResourceProjectScaffolder(client);

            await Assert.That(async () =>
                    await scaffolder.CreateAsync(absentDestination, null, Values, CancellationToken.None))
                .Throws<WebResourceScaffoldingException>();
            await Assert.That(async () =>
                    await scaffolder.CreateAsync(emptyDestination.FullName, null, Values, CancellationToken.None))
                .Throws<WebResourceScaffoldingException>();

            await Assert.That(Directory.Exists(absentDestination)).IsFalse();
            await Assert.That(Directory.EnumerateFileSystemEntries(emptyDestination.FullName).Any()).IsFalse();
            await Assert.That(Directory.EnumerateFileSystemEntries(parent.FullName)
                    .Any(path => Path.GetFileName(path).StartsWith(".dgtp-webresource-", StringComparison.Ordinal)))
                .IsFalse();
        }
        finally
        {
            parent.Delete(recursive: true);
        }
    }

    private const string CommitSha = "aaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaa";

    private static WebResourceProjectValues Values { get; } =
        new("contoso-webresources", "Contoso", null, null);

    [System.Diagnostics.CodeAnalysis.SuppressMessage(
        "Reliability",
        "CA2000:Dispose objects before losing scope",
        Justification = "HttpClient owns and disposes its handler.")]
    private static HttpClient CreateTemplateClient(
        byte[]? archive = null,
        Func<HttpRequestMessage, CancellationToken, Task<HttpResponseMessage>>? respond = null)
    {
        var templateArchive = archive ?? TemplateArchiveFixture.Create(
            new TemplateArchiveFixture.FixtureEntry("README.md", "__DGTP_PROJECT_NAME__ __DGTP_GLOBAL_NAME__"),
            new TemplateArchiveFixture.FixtureEntry("deployment.ps1", "'__DGTP_CONNECTION_NAME__' '__DGTP_SOLUTION__' '__DGTP_PUBLISHER_PREFIX__'"));

        return new HttpClient(new StubHttpMessageHandler(respond ?? ((request, _) =>
            Task.FromResult(TemplateResponse(request, templateArchive)))));
    }

    private static HttpResponseMessage TemplateResponse(HttpRequestMessage request, byte[]? archive = null)
    {
        if (request.RequestUri!.AbsolutePath.EndsWith("/commits/main", StringComparison.Ordinal))
        {
            return new HttpResponseMessage(HttpStatusCode.OK)
            {
                Content = new StringContent($"{{\"sha\":\"{CommitSha}\"}}", Encoding.UTF8, "application/json")
            };
        }

        return new HttpResponseMessage(HttpStatusCode.OK)
        {
            Content = new ByteArrayContent(archive ?? TemplateArchiveFixture.Create(
                new TemplateArchiveFixture.FixtureEntry("README.md", "__DGTP_PROJECT_NAME__ __DGTP_GLOBAL_NAME__"),
                new TemplateArchiveFixture.FixtureEntry("deployment.ps1", "'__DGTP_CONNECTION_NAME__' '__DGTP_SOLUTION__' '__DGTP_PUBLISHER_PREFIX__'")))
        };
    }

    private sealed class StubHttpMessageHandler(
        Func<HttpRequestMessage, CancellationToken, Task<HttpResponseMessage>> respond) : HttpMessageHandler
    {
        protected override Task<HttpResponseMessage> SendAsync(
            HttpRequestMessage request,
            CancellationToken cancellationToken) =>
            respond(request, cancellationToken);
    }
}
