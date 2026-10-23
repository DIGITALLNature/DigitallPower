// Copyright (c) DIGITALL Nature. All rights reserved
// DIGITALL Nature licenses this file to you under the Microsoft Public License.

using System.Runtime.Caching;
using dgt.power.codegeneration.Base;
using dgt.power.codegeneration.Generators;
using dgt.power.codegeneration.Generators.Contracts;
using dgt.power.codegeneration.Services;
using dgt.power.codegeneration.Services.Contracts;
using dgt.power.common;
using dgt.power.tests;
using Microsoft.PowerPlatform.Dataverse.Client;
using Microsoft.Xrm.Sdk;
using Microsoft.Xrm.Sdk.Messages;
using Microsoft.Xrm.Sdk.Metadata;
using Spectre.Console;
using Spectre.Console.Cli;
using Spectre.Console.Testing;

namespace dgt.power.codegeneration.tests;


[NotInParallel]
public class CodeGenerationCommandTests
{
    private readonly ICommand<CodeGenerationVerb> _command;
    private readonly StubConnection _connection;
    private readonly StubDotNetGenerator _dotNetGenerator = new();
    private readonly StubTypeScriptGenerator _typeScriptGenerator = new();

    public CodeGenerationCommandTests()
    {
        var metadataServiceMock = Mock.Of<IMetadataService>();

        _connection = new StubConnection();
        _command = new CodeGenerationCommand(new TestSpanFactory(),
            _dotNetGenerator, _typeScriptGenerator,
            metadataServiceMock.Object, _connection, AnsiConsole.Console);
    }

    private sealed class StubDotNetGenerator : IDotNetGenerator
    {
        public int GenerateCallCount { get; private set; }
        public CancellationToken LastCancellationToken { get; private set; }

        public Task<bool> GenerateAsync(CodeGenerationVerb args, DotNetCodeGenerationConfig config, CancellationToken cancellationToken = default)
        {
            GenerateCallCount++;
            LastCancellationToken = cancellationToken;
            return Task.FromResult(true);
        }
    }

    private sealed class StubTypeScriptGenerator : ITypeScriptGenerator
    {
        public int GenerateCallCount { get; private set; }
        public CancellationToken LastCancellationToken { get; private set; }

        public Task<bool> GenerateAsync(CodeGenerationVerb args, TypeScriptCodeGenerationConfig config, CancellationToken cancellationToken = default)
        {
            GenerateCallCount++;
            LastCancellationToken = cancellationToken;
            return Task.FromResult(true);
        }
    }

    private sealed class StubConnection(IAnsiConsole? console = null, IOrganizationServiceAsync2? service = null) : IDataverseConnection
    {
        private bool _connected;

        public int GetServiceCallCount { get; private set; }

        public async Task<IOrganizationServiceAsync2> GetServiceAsync()
        {
            GetServiceCallCount++;
            if (!_connected && console is not null)
            {
                await console.Status().StartAsync("Authenticating...", _ => Task.CompletedTask);
            }

            _connected = true;
            return service ?? Mock.Of<IOrganizationServiceAsync2>().Object;
        }

        public Task<bool> CheckAuthAsync() => Task.FromResult(true);

        public Task RefreshAuthAsync() => Task.CompletedTask;
    }

    [Test]
    [Arguments("dotnet")]
    [Arguments("typescript")]
    public async Task ExecuteAsync_ValidConfiguration_ConnectsBeforeGenerationDisplay(string target)
    {
        using var output = new TestConsole();
        // TestConsole disables exclusivity; use a real console over its captured output.
        var console = AnsiConsole.Create(new AnsiConsoleSettings
        {
            Out = output.Profile.Out,
            Ansi = AnsiSupport.Yes,
            Interactive = InteractionSupport.Yes
        });
        var service = Mock.Of<IOrganizationServiceAsync2>();
        service.ExecuteAsync(Any<OrganizationRequest>(), Any<CancellationToken>())
            .Returns(new RetrieveAllEntitiesResponse
            {
                Results = { ["EntityMetadata"] = Array.Empty<EntityMetadata>() }
            });
        var connection = new StubConnection(console, service.Object);
        using var cache = new MemoryCache(nameof(ExecuteAsync_ValidConfiguration_ConnectsBeforeGenerationDisplay));
        var metadataService = new MetadataService(connection, cache, console);
        var command = new CodeGenerationCommand(new TestSpanFactory(),
            _dotNetGenerator, _typeScriptGenerator, metadataService, connection, console);
        var configPath = Path.Combine(Path.GetTempPath(), Path.GetRandomFileName());
        try
        {
            await File.WriteAllTextAsync(configPath, $$"""{"version":2,"type":"{{target}}"}""");

            var result = await command.ExecuteAsync(
                new CommandContext([], new EmptyRemainingArguments(), "codegeneration", null),
                new CodeGenerationVerb { Config = configPath }, CancellationToken.None);

            await Assert.That(result).IsEqualTo(0);
            await Assert.That(connection.GetServiceCallCount).IsEqualTo(2);
            await Assert.That(_dotNetGenerator.GenerateCallCount).IsEqualTo(target == "dotnet" ? 1 : 0);
            await Assert.That(_typeScriptGenerator.GenerateCallCount).IsEqualTo(target == "typescript" ? 1 : 0);
        }
        finally
        {
            File.Delete(configPath);
        }
    }

    [Test]
    public async Task ExecuteAsync_ConnectionFails_DoesNotPopulateMetadataOrGenerate()
    {
        using var console = new TestConsole();
        var connection = Mock.Of<IDataverseConnection>();
        connection.GetServiceAsync().Throws(new InvalidOperationException("Connection unavailable"));
        var metadataService = Mock.Of<IMetadataService>();
        metadataService.PopulateEntitiesAndSolutionsAsync(Any<CodeGenerationConfigBase>(), Any<CancellationToken>())
            .Throws(new InvalidOperationException("Metadata should not run"));
        var command = new CodeGenerationCommand(new TestSpanFactory(),
            _dotNetGenerator, _typeScriptGenerator, metadataService.Object, connection.Object, console);

        await Assert.That(() => command.ExecuteAsync(
            new CommandContext([], new EmptyRemainingArguments(), "codegeneration", null),
            new CodeGenerationVerb { Config = "Resources/CodeGenerationCommand/config.json" },
            CancellationToken.None)).Throws<InvalidOperationException>()
            .WithMessageContaining("Connection unavailable");

        await Assert.That(_dotNetGenerator.GenerateCallCount).IsEqualTo(0);
        await Assert.That(_typeScriptGenerator.GenerateCallCount).IsEqualTo(0);
    }

    [Test]
    public async Task ShouldExecuteALlCommands() =>
        await Assert.That(
            await _command.ExecuteAsync(new CommandContext(Enumerable.Empty<string>(), new EmptyRemainingArguments(), "codegeneration", null),
                new CodeGenerationVerb
                {
                    Config = "Resources/CodeGenerationCommand/config.json"
                }, CancellationToken.None
            )).IsEqualTo(0);

    [Test]
    public async Task ShouldFailOnMissingConfiguration() =>
        await Assert.That(
            () => _command.ExecuteAsync(new CommandContext(Enumerable.Empty<string>(), new EmptyRemainingArguments(), "codegeneration", null),
                new CodeGenerationVerb
                {
                    Config = "missing.json"
                }, CancellationToken.None
            )).Throws<FileNotFoundException>();

    [Test]
    public async Task ShouldHonorCancellationBeforeLoadingConfiguration()
    {
        using var cancellationTokenSource = new CancellationTokenSource();
        await cancellationTokenSource.CancelAsync();
        var cancellationToken = cancellationTokenSource.Token;

        await Assert.That(
            () => _command.ExecuteAsync(
                new CommandContext(Enumerable.Empty<string>(), new EmptyRemainingArguments(), "codegeneration", null),
                new CodeGenerationVerb { Config = "missing.json" },
                cancellationToken)).Throws<OperationCanceledException>();

        await Assert.That(_connection.GetServiceCallCount).IsEqualTo(0);
    }

    [Test]
    [Arguments("dotnet")]
    [Arguments("typescript")]
    public async Task ShouldForwardCancellationToSelectedGenerator(string target)
    {
        var configPath = Path.Combine(Path.GetTempPath(), Path.GetRandomFileName());
        using var cancellationTokenSource = new CancellationTokenSource();
        try
        {
            await File.WriteAllTextAsync(configPath, $$"""{"version":2,"type":"{{target}}"}""", cancellationTokenSource.Token);

            var result = await _command.ExecuteAsync(
                new CommandContext([], new EmptyRemainingArguments(), "codegeneration", null),
                new CodeGenerationVerb { Config = configPath }, cancellationTokenSource.Token);

            await Assert.That(result).IsEqualTo(0);
            await Assert.That(_dotNetGenerator.GenerateCallCount).IsEqualTo(target == "dotnet" ? 1 : 0);
            await Assert.That(_typeScriptGenerator.GenerateCallCount).IsEqualTo(target == "typescript" ? 1 : 0);
            var actualToken = target == "dotnet"
                ? _dotNetGenerator.LastCancellationToken
                : _typeScriptGenerator.LastCancellationToken;
            await Assert.That(actualToken).IsEqualTo(cancellationTokenSource.Token);
        }
        finally
        {
            File.Delete(configPath);
        }
    }

    [Test]
    public async Task ShouldPropagateMetadataCancellationBeforeGenerating()
    {
        using var cancellationTokenSource = new CancellationTokenSource();
        var cancellationToken = cancellationTokenSource.Token;
        var metadataService = Mock.Of<IMetadataService>();
        metadataService.PopulateEntitiesAndSolutionsAsync(Any<CodeGenerationConfigBase>(), cancellationToken)
            .Throws(new OperationCanceledException(cancellationToken));
        var command = new CodeGenerationCommand(new TestSpanFactory(),
            _dotNetGenerator, _typeScriptGenerator, metadataService.Object, _connection, AnsiConsole.Console);

        await Assert.That(() => command.ExecuteAsync(
            new CommandContext([], new EmptyRemainingArguments(), "codegeneration", null),
            new CodeGenerationVerb { Config = "Resources/CodeGenerationCommand/config.json" },
            cancellationToken)).Throws<OperationCanceledException>();

        await Assert.That(_dotNetGenerator.GenerateCallCount).IsEqualTo(0);
        await Assert.That(_typeScriptGenerator.GenerateCallCount).IsEqualTo(0);
    }

    [Test]
    public async Task InvalidConfiguration_ShouldNotConnectOrModifyExistingOutput()
    {
        var tempDirectory = Path.Combine(Path.GetTempPath(), $"dgtp-codegeneration-{Guid.NewGuid():N}");
        var configPath = Path.Combine(tempDirectory, "config.json");
        var outputDirectory = Path.Combine(tempDirectory, "Model", "TypeScript");
        var existingOutput = Path.Combine(outputDirectory, "custom-file.ts");
        Directory.CreateDirectory(outputDirectory);

        try
        {
            await File.WriteAllTextAsync(configPath, """{"version":1,"type":"typescript"}""");
            await File.WriteAllTextAsync(existingOutput, "keep this file");
            using var cache = new MemoryCache(nameof(InvalidConfiguration_ShouldNotConnectOrModifyExistingOutput));
            var command = new CodeGenerationCommand(new TestSpanFactory(),
                _dotNetGenerator, _typeScriptGenerator,
                new MetadataService(_connection, cache, AnsiConsole.Console), _connection, AnsiConsole.Console);

            await Assert.That(
                () => command.ExecuteAsync(
                    new CommandContext(Enumerable.Empty<string>(), new EmptyRemainingArguments(), "codegeneration", null),
                    new CodeGenerationVerb
                    {
                        Config = configPath,
                        TargetDirectory = tempDirectory
                    },
                    CancellationToken.None
                )).Throws<InvalidOperationException>();

            await Assert.That(_connection.GetServiceCallCount).IsEqualTo(0);
            await Assert.That(_dotNetGenerator.GenerateCallCount).IsEqualTo(0);
            await Assert.That(_typeScriptGenerator.GenerateCallCount).IsEqualTo(0);
            await Assert.That(await File.ReadAllTextAsync(existingOutput)).IsEqualTo("keep this file");
        }
        finally
        {
            Directory.Delete(tempDirectory, true);
        }
    }

    [Test]
    public async Task InvalidTypeScriptOptions_ShouldNotPreventDotNetGeneration()
    {
        var originalMaxSteps = Environment.GetEnvironmentVariable("DGTP_TSL_MAX_STEPS");
        try
        {
            Environment.SetEnvironmentVariable("DGTP_TSL_MAX_STEPS", "0");
            var metadataService = Mock.Of<IMetadataService>().Object;
            var command = new CodeGenerationCommand(new TestSpanFactory(),
                _dotNetGenerator, new TypeScriptGenerator(metadataService, AnsiConsole.Console),
                metadataService, _connection, AnsiConsole.Console);

            var result = await command.ExecuteAsync(
                new CommandContext(Enumerable.Empty<string>(), new EmptyRemainingArguments(), "codegeneration", null),
                new CodeGenerationVerb { Config = "Resources/CodeGenerationCommand/config.json" },
                CancellationToken.None);

            await Assert.That(result).IsEqualTo(0);
            await Assert.That(_dotNetGenerator.GenerateCallCount).IsEqualTo(1);
        }
        finally
        {
            Environment.SetEnvironmentVariable("DGTP_TSL_MAX_STEPS", originalMaxSteps);
        }
    }

    [Test]
    public async Task InvalidTypeScriptOptions_ShouldNotModifyExistingOutput()
    {
        var originalMaxSteps = Environment.GetEnvironmentVariable("DGTP_TSL_MAX_STEPS");
        var tempDirectory = Path.Combine(Path.GetTempPath(), $"dgtp-codegeneration-{Guid.NewGuid():N}");
        var outputDirectory = Path.Combine(tempDirectory, "Model", "TypeScript");
        var existingOutput = Path.Combine(outputDirectory, "custom-file.ts");
        Directory.CreateDirectory(outputDirectory);

        try
        {
            Environment.SetEnvironmentVariable("DGTP_TSL_MAX_STEPS", "0");
            await File.WriteAllTextAsync(existingOutput, "keep this file");
            var generator = new TypeScriptGenerator(Mock.Of<IMetadataService>().Object, AnsiConsole.Console);

            await Assert.That(() => generator.GenerateAsync(
                    new CodeGenerationVerb { TargetDirectory = tempDirectory },
                    new TypeScriptCodeGenerationConfig()))
                .Throws<InvalidOperationException>();

            await Assert.That(await File.ReadAllTextAsync(existingOutput)).IsEqualTo("keep this file");
        }
        finally
        {
            Environment.SetEnvironmentVariable("DGTP_TSL_MAX_STEPS", originalMaxSteps);
            Directory.Delete(tempDirectory, true);
        }

    }

    [Test]
    public async Task CancelledTypeScriptGeneration_ShouldNotModifyExistingOutput()
    {
        var tempDirectory = Path.Combine(Path.GetTempPath(), $"dgtp-codegeneration-{Guid.NewGuid():N}");
        var outputDirectory = Path.Combine(tempDirectory, "Model", "TypeScript");
        var existingOutput = Path.Combine(outputDirectory, "custom-file.ts");
        Directory.CreateDirectory(outputDirectory);
        using var cancellationTokenSource = new CancellationTokenSource();
        await cancellationTokenSource.CancelAsync();
        var cancellationToken = cancellationTokenSource.Token;

        try
        {
            await File.WriteAllTextAsync(existingOutput, "keep this file", CancellationToken.None);
            var generator = new TypeScriptGenerator(Mock.Of<IMetadataService>().Object, AnsiConsole.Console);

            await Assert.That(() => generator.GenerateAsync(
                new CodeGenerationVerb { TargetDirectory = tempDirectory },
                new TypeScriptCodeGenerationConfig(),
                cancellationToken)).Throws<OperationCanceledException>();

            await Assert.That(await File.ReadAllTextAsync(existingOutput, CancellationToken.None)).IsEqualTo("keep this file");
        }
        finally
        {
            Directory.Delete(tempDirectory, true);
        }
    }
}
