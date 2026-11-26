// SPDX-FileCopyrightText: DIGITALL Nature
// SPDX-License-Identifier: MS-RL

using System.Net;
using System.Text;
using dgt.power.tests;
using dgt.power.webresource.Commands;
using dgt.power.webresource.Scaffolding;
using dgt.power.webresource.tests.Scaffolding;
using Microsoft.Extensions.DependencyInjection;
using Spectre.Console;
using Spectre.Console.Testing;

namespace dgt.power.webresource.tests.Commands;

public class WebResourceInitCommandTests : CommandTestsBase<WebResourceInitCommand, WebResourceInitSettings>
{
    [Test]
    public async Task Execute_CreatesProjectAndShowsGeneratedReadmeHint()
    {
        var parent = Directory.CreateTempSubdirectory();
        try
        {
            var destination = Path.Combine(parent.FullName, "generated project");
            using var client = CreateTemplateClient();
            var context = Build(client);
            var success = context.Execute(ValidSettings(destination));

            await Assert.That(success).IsTrue();
            await Assert.That(File.Exists(Path.Combine(destination, "README.md"))).IsTrue();
            await Assert.That(TestConsole.Output).Contains("README.md in the generated project");
            await Assert.That(TestConsole.Output).Contains("pnpm install --frozen-lockfile");
            await Assert.That(TestConsole.Output).Contains("pnpm run build:prod");
            await Assert.That(TestConsole.Output).Contains("aaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaa");
            var output = TestConsole.Output.Replace("\r\n", "\n", StringComparison.Ordinal);
            await Assert.That(output).Contains($"{Emoji.Known.CheckMark} Webresource project created at");
            await Assert.That(output).Contains("\nTemplate commit: aaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaa");
            await Assert.That(output).Contains($"\n\n{Emoji.Known.Rocket} Next steps:");
            await Assert.That(output).Contains($"\n\n{Emoji.Known.OpenBook} See README.md");
        }
        finally
        {
            parent.Delete(recursive: true);
        }
    }

    [Test]
    public async Task Execute_NonInteractiveShowsStagesWithoutAnimatedPercentages()
    {
        var parent = Directory.CreateTempSubdirectory();
        try
        {
            var destination = Path.Combine(parent.FullName, "project");
            using var client = CreateTemplateClient();
            var settings = new WebResourceInitSettings
            {
                Destination = destination,
                Name = "contoso-webresources",
                Global = "Contoso",
                NonInteractive = true
            };

            var success = Build(client).Execute(settings);

            await Assert.That(success).IsTrue();
            await Assert.That(TestConsole.Output).Contains("Resolving template...");
            await Assert.That(TestConsole.Output).Contains("Downloading template...");
            await Assert.That(TestConsole.Output).Contains("Creating project...");
            await Assert.That(TestConsole.Output).DoesNotContain("0%");
        }
        finally
        {
            parent.Delete(recursive: true);
        }
    }

    [Test]
    public async Task Execute_InteractiveDisplaysProgressWhileWaiting()
    {
        TestConsole.Interactive();
        using var client = CreateClient(async (_, cancellationToken) =>
        {
            await Task.Delay(250, cancellationToken);
            return new HttpResponseMessage(HttpStatusCode.NotFound);
        });

        var success = Build(client).Execute(ValidSettings("generated"));

        await Assert.That(success).IsFalse();
        await Assert.That(TestConsole.Output).Contains("Resolving template...");
        await Assert.That(TestConsole.Output).Contains("HTTP 404");
    }

    [Test]
    public async Task Execute_InvalidGlobalFailsBeforeTemplateAcquisition()
    {
        var requestCount = 0;
        using var client = CreateClient((_, _) =>
        {
            requestCount++;
            return Task.FromResult(new HttpResponseMessage(HttpStatusCode.OK));
        });
        var context = Build(client);
        var success = context.Execute(ValidSettings("generated", global: "Contoso.App"));

        await Assert.That(success).IsFalse();
        await Assert.That(requestCount).IsZero();
        await Assert.That(TestConsole.Output).Contains("--global");
    }

    [Test]
    [Arguments(null, "")]
    [Arguments("shared-dev", "shared-dev")]
    public async Task Execute_UsesOnlyTheExplicitDgtpConnectionDefault(
        string? dgtpConnection, string expected)
    {
        var parent = Directory.CreateTempSubdirectory();
        try
        {
            var destination = Path.Combine(parent.FullName, "project");
            using var client = CreateTemplateClient();
            var settings = new WebResourceInitSettings
            {
                Destination = destination,
                Name = "contoso-webresources",
                Global = "Contoso",
                DgtpConnection = dgtpConnection,
                Connection = "invocation-only",
                ConnectionString = "secret-connection-string"
            };

            var success = Build(client).Execute(settings);

            await Assert.That(success).IsTrue();
            await Assert.That(await File.ReadAllTextAsync(Path.Combine(destination, "deploy.ps1")))
                .IsEqualTo($"'{expected}' '' ''");
            await Assert.That(await File.ReadAllTextAsync(Path.Combine(destination, "README.md")))
                .DoesNotContain("secret-connection-string");
        }
        finally
        {
            parent.Delete(recursive: true);
        }
    }

    [Test]
    public async Task Execute_EmptyDgtpConnectionFailsBeforeTemplateAcquisition()
    {
        var requestCount = 0;
        using var client = CreateClient((_, _) =>
        {
            requestCount++;
            return Task.FromResult(new HttpResponseMessage(HttpStatusCode.OK));
        });
        var settings = new WebResourceInitSettings
        {
            Destination = "generated",
            Name = "contoso-webresources",
            Global = "Contoso",
            DgtpConnection = " "
        };

        var success = Build(client).Execute(settings);

        await Assert.That(success).IsFalse();
        await Assert.That(requestCount).IsZero();
        await Assert.That(TestConsole.Output).Contains("--dgtp-connection");
    }

    [Test]
    [Arguments("", "Contoso", null, null, "--name")]
    [Arguments("contoso-webresources", "", null, null, "--global")]
    [Arguments("contoso-webresources", "Contoso", "Contoso Core", null, "--solution")]
    [Arguments("contoso-webresources", "Contoso", null, "contoso!", "--publisher-prefix")]
    public async Task Execute_InvalidRequiredOrDeploymentValueFailsBeforeTemplateAcquisition(
        string name,
        string global,
        string? solution,
        string? publisherPrefix,
        string expectedError)
    {
        var requestCount = 0;
        using var client = CreateClient((_, _) =>
        {
            requestCount++;
            return Task.FromResult(new HttpResponseMessage(HttpStatusCode.OK));
        });
        var settings = new WebResourceInitSettings
        {
            Destination = "generated",
            Name = name,
            Global = global,
            Solution = solution,
            PublisherPrefix = publisherPrefix
        };
        var context = Build(client);

        var success = context.Execute(settings);

        await Assert.That(success).IsFalse();
        await Assert.That(requestCount).IsZero();
        await Assert.That(TestConsole.Output).Contains(expectedError);
    }

    [Test]
    public async Task Execute_TemplateFailureIsShownToUser()
    {
        using var client = CreateClient((_, _) =>
            Task.FromResult(new HttpResponseMessage(HttpStatusCode.NotFound)));
        var context = Build(client);

        var success = context.Execute(new WebResourceInitSettings
        {
            Destination = "generated",
            Name = "contoso-webresources",
            Global = "Contoso",
            NonInteractive = true
        });

        await Assert.That(success).IsFalse();
        await Assert.That(TestConsole.Output).Contains("HTTP 404");
        await Assert.That(TestConsole.Output).Contains("Resolving template...");
        await Assert.That(TestConsole.Output).DoesNotContain("Creating project...");
    }

    [Test]
    public async Task Scaffold_CancellationLeavesDestinationAbsent()
    {
        var parent = Directory.CreateTempSubdirectory();
        try
        {
            var destination = Path.Combine(parent.FullName, "cancelled");
            using var client = CreateClient(async (_, cancellationToken) =>
            {
                await Task.Delay(Timeout.InfiniteTimeSpan, cancellationToken);
                return new HttpResponseMessage(HttpStatusCode.OK);
            });
            using var cancellation = new CancellationTokenSource();
            await cancellation.CancelAsync();

            await Assert.That(async () =>
                    await new WebResourceProjectScaffolder(client).CreateAsync(
                        destination,
                        null,
                        new WebResourceProjectValues("contoso-webresources", "Contoso", null, null),
                        cancellation.Token))
                .Throws<OperationCanceledException>();

            await Assert.That(Directory.Exists(destination)).IsFalse();
        }
        finally
        {
            parent.Delete(recursive: true);
        }
    }

    private CommandTestContext<WebResourceInitCommand, WebResourceInitSettings> Build(HttpClient httpClient)
    {
        var services = new TestServiceCollection();
        services.AddSingleton(httpClient);
        return new CommandTestContextBuilder<WebResourceInitCommand, WebResourceInitSettings>()
            .WithServiceCollection(services)
            .WithAnsiConsole(TestConsole)
            .Build();
    }

    private static WebResourceInitSettings ValidSettings(string destination, string global = "Contoso") =>
        new()
        {
            Destination = destination,
            Name = "contoso-webresources",
            Global = global
        };

    private static HttpClient CreateTemplateClient()
    {
        const string sha = "aaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaa";
        var archive = TemplateArchiveFixture.Create(
            new TemplateArchiveFixture.FixtureEntry("README.md", "Project `__DGTP_PROJECT_NAME__`"),
            new TemplateArchiveFixture.FixtureEntry("package.json", """{"name":"__DGTP_PROJECT_NAME__"}"""),
            new TemplateArchiveFixture.FixtureEntry("config/rollup.js", "'__DGTP_GLOBAL_NAME__'"),
            new TemplateArchiveFixture.FixtureEntry("deploy.ps1", "'__DGTP_CONNECTION_NAME__' '__DGTP_SOLUTION__' '__DGTP_PUBLISHER_PREFIX__'"));

        return CreateClient((request, _) =>
        {
            var response = request.RequestUri!.AbsolutePath.EndsWith("/commits/main", StringComparison.Ordinal)
                ? new HttpResponseMessage(HttpStatusCode.OK)
                {
                    Content = new StringContent($$"""{"sha":"{{sha}}"}""", Encoding.UTF8, "application/json")
                }
                : new HttpResponseMessage(HttpStatusCode.OK) { Content = new ByteArrayContent(archive) };
            return Task.FromResult(response);
        });
    }

    [System.Diagnostics.CodeAnalysis.SuppressMessage(
        "Reliability",
        "CA2000:Dispose objects before losing scope",
        Justification = "HttpClient owns and disposes its handler.")]
    private static HttpClient CreateClient(
        Func<HttpRequestMessage, CancellationToken, Task<HttpResponseMessage>> respond) =>
        new(new StubHttpMessageHandler(respond));

    private sealed class StubHttpMessageHandler(
        Func<HttpRequestMessage, CancellationToken, Task<HttpResponseMessage>> respond) : HttpMessageHandler
    {
        protected override Task<HttpResponseMessage> SendAsync(
            HttpRequestMessage request,
            CancellationToken cancellationToken) =>
            respond(request, cancellationToken);
    }
}
