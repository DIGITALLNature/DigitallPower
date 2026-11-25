// SPDX-FileCopyrightText: DIGITALL Nature
// SPDX-License-Identifier: MS-RL

using System.Net;
using System.Net.Http.Headers;
using System.Text.Json.Serialization;
using System.Text.Json;
using System.Text.RegularExpressions;

namespace dgt.power.webresource.Scaffolding;

internal sealed partial class WebResourceTemplateDownloader
{
    private const string Repository = "DIGITALLNature/webresources-template";
    private const int MaximumArchiveBytes = 20 * 1024 * 1024;
    private const int MaximumAnnotatedTagDepth = 8;
    private static readonly TimeSpan DefaultRequestTimeout = TimeSpan.FromSeconds(30);
    private static readonly Uri ApiBaseUri = new("https://api.github.com/");
    private readonly HttpClient _httpClient;
    private readonly TimeSpan _requestTimeout;

    public WebResourceTemplateDownloader(HttpClient httpClient, TimeSpan? requestTimeout = null)
    {
        ArgumentNullException.ThrowIfNull(httpClient);
        _requestTimeout = requestTimeout ?? DefaultRequestTimeout;
        if (_requestTimeout <= TimeSpan.Zero)
        {
            throw new ArgumentOutOfRangeException(nameof(requestTimeout), "Request timeout must be positive.");
        }

        _httpClient = httpClient;
    }

    public async Task<DownloadedWebResourceTemplate> DownloadAsync(
        string? templateVersion,
        CancellationToken cancellationToken)
    {
        using var timeout = CancellationTokenSource.CreateLinkedTokenSource(cancellationToken);
        timeout.CancelAfter(_requestTimeout);
        try
        {
            string commitSha;
            if (templateVersion is null)
            {
                commitSha = await ResolveDefaultBranchAsync(timeout.Token);
            }
            else if (FullCommitShaRegex().IsMatch(templateVersion))
            {
                commitSha = templateVersion.ToLowerInvariant();
            }
            else
            {
                commitSha = await ResolveTagAsync(templateVersion, timeout.Token);
            }

            var archive = await DownloadArchiveAsync(commitSha, timeout.Token);
            return new DownloadedWebResourceTemplate(commitSha, archive);
        }
        catch (OperationCanceledException exception) when (!cancellationToken.IsCancellationRequested)
        {
            throw new WebResourceScaffoldingException(
                $"The GitHub request timed out after {_requestTimeout.TotalSeconds:0} seconds.",
                exception);
        }
        catch (HttpRequestException exception)
        {
            throw new WebResourceScaffoldingException(
                "Could not reach GitHub to download the webresource template. Check network access and retry.",
                exception);
        }
        catch (JsonException exception)
        {
            throw new WebResourceScaffoldingException(
                "GitHub returned an invalid response while resolving the webresource template.",
                exception);
        }
    }

    private async Task<string> ResolveDefaultBranchAsync(CancellationToken cancellationToken)
    {
        using var response = await SendAsync($"repos/{Repository}/commits/main", cancellationToken);
        EnsureSuccess(response, "Could not resolve the template's current main commit.");
        var commit = await ReadJsonAsync<GitHubCommitResponse>(response, cancellationToken);
        return ReadSha(commit?.Sha);
    }

    private async Task<string> ResolveTagAsync(string tag, CancellationToken cancellationToken)
    {
        var escapedTag = Uri.EscapeDataString(tag);
        using var response = await SendAsync($"repos/{Repository}/git/ref/tags/{escapedTag}", cancellationToken);

        if (response.StatusCode == HttpStatusCode.NotFound)
        {
            return await ExplainMissingTagAsync(tag, escapedTag, cancellationToken);
        }

        EnsureSuccess(response, $"Could not resolve template tag '{tag}'.");
        var reference = await ReadJsonAsync<GitHubObjectResponse>(response, cancellationToken);
        var target = ReadObject(reference);
        var objectType = ReadObjectType(target.Type);
        var objectSha = ReadSha(target.Sha);

        for (var depth = 0; objectType == "tag" && depth < MaximumAnnotatedTagDepth; depth++)
        {
            using var tagResponse = await SendAsync($"repos/{Repository}/git/tags/{objectSha}", cancellationToken);
            EnsureSuccess(tagResponse, $"Could not dereference annotated template tag '{tag}'.");
            var annotatedTag = await ReadJsonAsync<GitHubObjectResponse>(tagResponse, cancellationToken);
            var annotatedTarget = ReadObject(annotatedTag);
            objectType = ReadObjectType(annotatedTarget.Type);
            objectSha = ReadSha(annotatedTarget.Sha);
        }

        if (objectType != "commit")
        {
            throw new WebResourceScaffoldingException(
                $"Template tag '{tag}' did not resolve to a commit within the supported tag depth.");
        }

        return objectSha;
    }

    private async Task<string> ExplainMissingTagAsync(
        string tag,
        string escapedTag,
        CancellationToken cancellationToken)
    {
        using var branchResponse = await SendAsync(
            $"repos/{Repository}/git/ref/heads/{escapedTag}",
            cancellationToken);

        if (branchResponse.IsSuccessStatusCode)
        {
            throw new WebResourceScaffoldingException(
                $"Template version '{tag}' is a branch. Select a tag or full commit SHA; " +
                "omit --template-version to use the current main branch.");
        }

        if (branchResponse.StatusCode != HttpStatusCode.NotFound)
        {
            EnsureSuccess(
                branchResponse,
                $"Could not determine whether template version '{tag}' is a branch.");
        }

        throw new WebResourceScaffoldingException(
            $"Template tag '{tag}' was not found. Select an existing tag or full commit SHA, " +
            "or omit --template-version to use the current main branch.");
    }

    private async Task<byte[]> DownloadArchiveAsync(string commitSha, CancellationToken cancellationToken)
    {
        using var response = await SendAsync($"repos/{Repository}/zipball/{commitSha}", cancellationToken);

        EnsureSuccess(response, $"Could not download template commit {commitSha}.");
        if (response.Content.Headers.ContentLength is > MaximumArchiveBytes)
        {
            throw new WebResourceScaffoldingException(
                $"The compressed template archive exceeds the {MaximumArchiveBytes / 1024 / 1024} MiB limit.");
        }

        await using var stream = await response.Content.ReadAsStreamAsync(cancellationToken);
        await using var archive = new MemoryStream();
        var buffer = new byte[81920];
        while (true)
        {
            var bytesRead = await stream.ReadAsync(buffer, cancellationToken);
            if (bytesRead == 0)
            {
                break;
            }

            if (archive.Length + bytesRead > MaximumArchiveBytes)
            {
                throw new WebResourceScaffoldingException(
                    $"The compressed template archive exceeds the {MaximumArchiveBytes / 1024 / 1024} MiB limit.");
            }

            await archive.WriteAsync(buffer.AsMemory(0, bytesRead), cancellationToken);
        }

        return archive.ToArray();
    }

    private async Task<HttpResponseMessage> SendAsync(
        string relativeUri,
        CancellationToken cancellationToken)
    {
        using (var request = CreateApiRequest(relativeUri))
        {
            try
            {
                request.Headers.Accept.Add(new MediaTypeWithQualityHeaderValue("application/vnd.github+json"));
                request.Headers.UserAgent.ParseAdd("dgtp");
                return await _httpClient.SendAsync(
                    request,
                    HttpCompletionOption.ResponseHeadersRead,
                    cancellationToken);
            }
            catch (HttpRequestException exception)
            {
                throw new WebResourceScaffoldingException(
                    "Could not reach GitHub to download the webresource template. Check network access and retry.",
                    exception);
            }
        }
    }

    private static HttpRequestMessage CreateApiRequest(string relativeUri) =>
        new(HttpMethod.Get, new Uri(ApiBaseUri, relativeUri));

    private static async Task<T?> ReadJsonAsync<T>(
        HttpResponseMessage response,
        CancellationToken cancellationToken)
        where T : class
    {
        await using var stream = await response.Content.ReadAsStreamAsync(cancellationToken);
        return await JsonSerializer.DeserializeAsync<T>(stream, cancellationToken: cancellationToken);
    }

    private static string ReadSha(string? sha)
    {
        if (sha is null || !FullCommitShaRegex().IsMatch(sha))
        {
            throw new WebResourceScaffoldingException("GitHub returned an invalid template commit SHA.");
        }

        return sha.ToLowerInvariant();
    }

    private static GitHubObject ReadObject(GitHubObjectResponse? response)
    {
        if (response?.Object is not { } target)
        {
            throw new WebResourceScaffoldingException("GitHub returned an invalid template tag reference.");
        }

        return target;
    }

    private static string ReadObjectType(string? type)
    {
        if (type is null)
        {
            throw new WebResourceScaffoldingException("GitHub returned an invalid template tag target.");
        }

        return type;
    }

    private sealed class GitHubCommitResponse
    {
        [JsonPropertyName("sha")]
        public string? Sha { get; init; }
    }

    private sealed class GitHubObjectResponse
    {
        [JsonPropertyName("object")]
        public GitHubObject? Object { get; init; }
    }

    private sealed class GitHubObject
    {
        [JsonPropertyName("type")]
        public string? Type { get; init; }

        [JsonPropertyName("sha")]
        public string? Sha { get; init; }
    }

    private static void EnsureSuccess(HttpResponseMessage response, string message)
    {
        if (response.IsSuccessStatusCode)
        {
            return;
        }

        if ((int)response.StatusCode == 429 ||
            response.StatusCode == HttpStatusCode.Forbidden &&
            response.Headers.TryGetValues("X-RateLimit-Remaining", out var remaining) &&
            remaining.Contains("0", StringComparer.Ordinal))
        {
            throw new WebResourceScaffoldingException(
                "GitHub's anonymous API rate limit was reached. Wait before retrying or select a commit SHA directly.");
        }

        throw new WebResourceScaffoldingException(
            $"{message} GitHub returned HTTP {(int)response.StatusCode} ({response.ReasonPhrase}).");
    }

    [GeneratedRegex("^[0-9a-fA-F]{40}$", RegexOptions.CultureInvariant)]
    private static partial Regex FullCommitShaRegex();
}
