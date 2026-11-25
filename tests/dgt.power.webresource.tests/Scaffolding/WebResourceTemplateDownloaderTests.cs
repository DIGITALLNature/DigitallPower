// SPDX-FileCopyrightText: DIGITALL Nature
// SPDX-License-Identifier: MS-RL

using System.Net;
using System.Diagnostics.CodeAnalysis;
using System.Text;
using System.Text.Json;
using dgt.power.webresource.Scaffolding;

namespace dgt.power.webresource.tests.Scaffolding;

public class WebResourceTemplateDownloaderTests
{
    private const string CommitSha = "aaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaa";

    [Test]
    public async Task Download_DefaultsToMainAndDownloadsResolvedCommit()
    {
        var requests = new List<CapturedRequest>();
        using var client = CreateClient(request =>
        {
            requests.Add(Capture(request));
            return request.RequestUri!.AbsolutePath.EndsWith("/commits/main", StringComparison.Ordinal)
                ? JsonResponse(CommitResponse(CommitSha))
                : BytesResponse([1, 2, 3]);
        });

        var downloaded = await new WebResourceTemplateDownloader(client).DownloadAsync(null, CancellationToken.None);

        await Assert.That(downloaded.CommitSha).IsEqualTo(CommitSha);
        await Assert.That(downloaded.Archive).IsEquivalentTo(new byte[] { 1, 2, 3 });
        await Assert.That(requests.Count).IsEqualTo(2);
        await Assert.That(requests[0].Url.AbsolutePath).EndsWith("/commits/main");
        await Assert.That(requests[1].Url.AbsolutePath).EndsWith($"/zipball/{CommitSha}");
        await Assert.That(requests.All(request => !request.HasAuthorization)).IsTrue();
        await Assert.That(requests.All(request => request.HasUserAgent)).IsTrue();
    }

    [Test]
    public async Task Download_RejectsCommitResponseWithoutSha()
    {
        using var client = CreateClient(_ => JsonResponse("{}"));

        var exception = await Assert.That(async () =>
                await new WebResourceTemplateDownloader(client).DownloadAsync(null, CancellationToken.None))
            .Throws<WebResourceScaffoldingException>();

        await Assert.That(exception!.Message).Contains("invalid template commit SHA");
    }

    [Test]
    public async Task Download_FullCommitShaDoesNotResolveTagOrBranch()
    {
        var requests = new List<CapturedRequest>();
        using var client = CreateClient(request =>
        {
            requests.Add(Capture(request));
            return BytesResponse([7]);
        });

        var downloaded = await new WebResourceTemplateDownloader(client)
            .DownloadAsync(CommitSha, CancellationToken.None);

        await Assert.That(downloaded.CommitSha).IsEqualTo(CommitSha);
        await Assert.That(requests.Count).IsEqualTo(1);
        await Assert.That(requests[0].Url.AbsolutePath).EndsWith($"/zipball/{CommitSha}");
    }

    [Test]
    public async Task Download_LightweightTagResolvesCommit()
    {
        using var client = CreateClient(request =>
            request.RequestUri!.AbsolutePath.Contains("/git/ref/tags/v1.0", StringComparison.Ordinal)
                ? JsonResponse(TagResponse("commit", CommitSha))
                : BytesResponse([1]));

        var downloaded = await new WebResourceTemplateDownloader(client)
            .DownloadAsync("v1.0", CancellationToken.None);

        await Assert.That(downloaded.CommitSha).IsEqualTo(CommitSha);
    }

    [Test]
    public async Task Download_RejectsTagReferenceWithoutObject()
    {
        using var client = CreateClient(_ => JsonResponse("{}"));

        var exception = await Assert.That(async () =>
                await new WebResourceTemplateDownloader(client).DownloadAsync("v1.0", CancellationToken.None))
            .Throws<WebResourceScaffoldingException>();

        await Assert.That(exception!.Message).Contains("invalid template tag reference");
    }

    [Test]
    public async Task Download_AnnotatedTagDereferencesToCommit()
    {
        const string tagObjectSha = "bbbbbbbbbbbbbbbbbbbbbbbbbbbbbbbbbbbbbbbb";
        using var client = CreateClient(request =>
        {
            var path = request.RequestUri!.AbsolutePath;
            if (path.EndsWith("/git/ref/tags/v2.0", StringComparison.Ordinal))
            {
                return JsonResponse(TagResponse("tag", tagObjectSha));
            }

            if (path.EndsWith($"/git/tags/{tagObjectSha}", StringComparison.Ordinal))
            {
                return JsonResponse(TagResponse("commit", CommitSha));
            }

            return BytesResponse([1]);
        });

        var downloaded = await new WebResourceTemplateDownloader(client)
            .DownloadAsync("v2.0", CancellationToken.None);

        await Assert.That(downloaded.CommitSha).IsEqualTo(CommitSha);
    }

    [Test]
    public async Task Download_EncodesTagNameAsSingleUriSegment()
    {
        var requests = new List<CapturedRequest>();
        using var client = CreateClient(request =>
        {
            requests.Add(Capture(request));
            return request.RequestUri!.AbsolutePath.Contains("/git/ref/tags/", StringComparison.Ordinal)
                ? JsonResponse(TagResponse("commit", CommitSha))
                : BytesResponse([1]);
        });

        await new WebResourceTemplateDownloader(client).DownloadAsync("release/v1.2", CancellationToken.None);

        await Assert.That(requests[0].Url.AbsoluteUri).Contains("release%2Fv1.2");
    }

    [Test]
    public async Task Download_RejectsBranchSelectionWithoutDownloadingArchive()
    {
        var requests = new List<CapturedRequest>();
        using var client = CreateClient(request =>
        {
            requests.Add(Capture(request));
            return request.RequestUri!.AbsolutePath.Contains("/git/ref/heads/", StringComparison.Ordinal)
                ? JsonResponse("""{"ref":"refs/heads/feature"}""")
                : new HttpResponseMessage(HttpStatusCode.NotFound);
        });

        var exception = await Assert.That(async () =>
                await new WebResourceTemplateDownloader(client).DownloadAsync("feature", CancellationToken.None))
            .Throws<WebResourceScaffoldingException>();

        await Assert.That(exception!.Message).Contains("is a branch");
        await Assert.That(requests.Count).IsEqualTo(2);
    }

    [Test]
    public async Task Download_ReportsMissingTagWithoutFallingBack()
    {
        var archiveRequested = false;
        using var client = CreateClient(request =>
        {
            if (request.RequestUri!.AbsolutePath.EndsWith("/zipball/", StringComparison.Ordinal))
            {
                archiveRequested = true;
                return BytesResponse([1]);
            }

            return new HttpResponseMessage(HttpStatusCode.NotFound);
        });

        var exception = await Assert.That(async () =>
                await new WebResourceTemplateDownloader(client).DownloadAsync("missing", CancellationToken.None))
            .Throws<WebResourceScaffoldingException>();

        await Assert.That(exception!.Message).Contains("was not found");
        await Assert.That(archiveRequested).IsFalse();
    }

    [Test]
    public async Task Download_ReportsHttpFailureWithoutFallingBack()
    {
        var archiveRequested = false;
        using var client = CreateClient(request =>
        {
            if (request.RequestUri!.AbsolutePath.Contains("/zipball/", StringComparison.Ordinal))
            {
                archiveRequested = true;
                return BytesResponse([1]);
            }

            return new HttpResponseMessage(HttpStatusCode.ServiceUnavailable);
        });

        var exception = await Assert.That(async () =>
                await new WebResourceTemplateDownloader(client).DownloadAsync(null, CancellationToken.None))
            .Throws<WebResourceScaffoldingException>();

        await Assert.That(exception!.Message).Contains("HTTP 503");
        await Assert.That(archiveRequested).IsFalse();
    }

    [Test]
    public async Task Download_ReportsAnonymousApiRateLimit()
    {
        using var client = CreateClient(_ =>
        {
            var response = new HttpResponseMessage(HttpStatusCode.Forbidden);
            response.Headers.TryAddWithoutValidation("X-RateLimit-Remaining", "0");
            return response;
        });

        var exception = await Assert.That(async () =>
                await new WebResourceTemplateDownloader(client).DownloadAsync(null, CancellationToken.None))
            .Throws<WebResourceScaffoldingException>();

        await Assert.That(exception!.Message).Contains("rate limit");
    }

    [Test]
    public async Task Download_RejectsCompressedArchiveOverLimit()
    {
        using var client = CreateClient(request =>
        {
            if (request.RequestUri!.AbsolutePath.EndsWith("/commits/main", StringComparison.Ordinal))
            {
                return JsonResponse(CommitResponse(CommitSha));
            }

            var response = BytesResponse([]);
            response.Content.Headers.ContentLength = 20 * 1024 * 1024 + 1;
            return response;
        });

        var exception = await Assert.That(async () =>
                await new WebResourceTemplateDownloader(client).DownloadAsync(null, CancellationToken.None))
            .Throws<WebResourceScaffoldingException>();

        await Assert.That(exception!.Message).Contains("20 MiB limit");
    }

    [Test]
    public async Task Download_EnforcesCompressedLimitWhileStreaming()
    {
        using var client = CreateClient(request =>
        {
            if (request.RequestUri!.AbsolutePath.EndsWith("/commits/main", StringComparison.Ordinal))
            {
                return JsonResponse(CommitResponse(CommitSha));
            }

            var response = BytesResponse(new byte[20 * 1024 * 1024 + 1]);
            response.Content.Headers.ContentLength = null;
            return response;
        });

        var exception = await Assert.That(async () =>
                await new WebResourceTemplateDownloader(client).DownloadAsync(null, CancellationToken.None))
            .Throws<WebResourceScaffoldingException>();

        await Assert.That(exception!.Message).Contains("20 MiB limit");
    }

    [Test]
    public async Task Download_ReportsInterruptedArchiveTransfer()
    {
        using var client = CreateClient((request, _) =>
        {
            if (request.RequestUri!.AbsolutePath.EndsWith("/commits/main", StringComparison.Ordinal))
            {
                return Task.FromResult(JsonResponse(CommitResponse(CommitSha)));
            }

            throw new HttpRequestException("connection reset");
        });

        var exception = await Assert.That(async () =>
                await new WebResourceTemplateDownloader(client).DownloadAsync(null, CancellationToken.None))
            .Throws<WebResourceScaffoldingException>();

        await Assert.That(exception!.Message).Contains("Could not reach GitHub");
    }

    [Test]
    public async Task Download_StopsWhenRequestTimeoutExpires()
    {
        using var client = CreateClient(async (_, cancellationToken) =>
        {
            await Task.Delay(Timeout.InfiniteTimeSpan, cancellationToken);
            return new HttpResponseMessage(HttpStatusCode.OK);
        });

        var exception = await Assert.That(async () =>
                await new WebResourceTemplateDownloader(client, TimeSpan.FromMilliseconds(10))
                    .DownloadAsync(null, CancellationToken.None))
            .Throws<WebResourceScaffoldingException>();

        await Assert.That(exception!.Message).Contains("timed out");
    }

    private static HttpClient CreateClient(Func<HttpRequestMessage, HttpResponseMessage> respond) =>
        CreateClient((request, _) => Task.FromResult(respond(request)));

    [SuppressMessage(
        "Reliability",
        "CA2000:Dispose objects before losing scope",
        Justification = "HttpClient owns and disposes its handler.")]
    private static HttpClient CreateClient(
        Func<HttpRequestMessage, CancellationToken, Task<HttpResponseMessage>> respond) =>
        new(new StubHttpMessageHandler(respond));

    private static CapturedRequest Capture(HttpRequestMessage request) =>
        new(
            request.RequestUri!,
            request.Headers.Authorization is not null,
            request.Headers.UserAgent.Any());

    private static HttpResponseMessage JsonResponse(string json) =>
        new(HttpStatusCode.OK) { Content = new StringContent(json, Encoding.UTF8, "application/json") };

    private static string CommitResponse(string sha) => JsonSerializer.Serialize(new { sha });

    private static string TagResponse(string type, string sha) =>
        JsonSerializer.Serialize(new { @object = new { type, sha } });

    private static HttpResponseMessage BytesResponse(byte[] bytes) =>
        new(HttpStatusCode.OK) { Content = new ByteArrayContent(bytes) };

    private sealed class StubHttpMessageHandler(
        Func<HttpRequestMessage, CancellationToken, Task<HttpResponseMessage>> respond) : HttpMessageHandler
    {
        protected override Task<HttpResponseMessage> SendAsync(
            HttpRequestMessage request,
            CancellationToken cancellationToken) =>
            respond(request, cancellationToken);
    }

    private sealed record CapturedRequest(Uri Url, bool HasAuthorization, bool HasUserAgent);
}
