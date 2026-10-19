// Copyright (c) DIGITALL Nature. All rights reserved
// DIGITALL Nature licenses this file to you under the Microsoft Public License.

using dgt.power.codegeneration.Base;
using dgt.power.codegeneration.Generators.Contracts;
using dgt.power.codegeneration.Services.Contracts;
using dgt.power.common;
using dgt.power.tests;
using Microsoft.PowerPlatform.Dataverse.Client;
using Spectre.Console;
using Spectre.Console.Cli;

namespace dgt.power.codegeneration.tests;


[NotInParallel("AnsiConsole")]
public class CodeGenerationCommandTests
{
    private readonly ICommand<CodeGenerationVerb> _command;
    private readonly StubConnection _connection;

    public CodeGenerationCommandTests()
    {
        var dotNetGeneratorMock = Mock.Of<IDotNetGenerator>();
        dotNetGeneratorMock.Generate(Any<CodeGenerationVerb>(), Any<DotNetCodeGenerationConfig>()).Returns(true);

        var typeScriptGeneratorMock = Mock.Of<ITypeScriptGenerator>();
        typeScriptGeneratorMock.Generate(Any<CodeGenerationVerb>(), Any<TypeScriptCodeGenerationConfig>()).Returns(true);
        var metadataServiceMock = Mock.Of<IMetadataService>();

        _connection = new StubConnection();
        _command = new CodeGenerationCommand(new TestSpanFactory(), _connection,
            dotNetGeneratorMock.Object, typeScriptGeneratorMock.Object,
            metadataServiceMock.Object, AnsiConsole.Console);
    }

    private sealed class StubConnection : IDataverseConnection
    {
        public int GetServiceCallCount { get; private set; }

        public Task<IOrganizationServiceAsync2> GetServiceAsync()
        {
            GetServiceCallCount++;
            return Task.FromResult(Mock.Of<IOrganizationServiceAsync2>().Object);
        }

        public Task<bool> CheckAuthAsync() => Task.FromResult(true);

        public Task RefreshAuthAsync() => Task.CompletedTask;
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
    public async Task InvalidConfiguration_ShouldNotConnectOrModifyExistingOutput()
    {
        var tempDirectory = Path.Combine(Path.GetTempPath(), $"dgtp-codegeneration-{Guid.NewGuid():N}");
        var configPath = Path.Combine(tempDirectory, "config.json");
        var outputDirectory = Path.Combine(tempDirectory, "Model");
        var existingOutput = Path.Combine(outputDirectory, "custom-file.ts");
        Directory.CreateDirectory(outputDirectory);

        try
        {
            await File.WriteAllTextAsync(configPath, """{"version":1,"type":"typescript"}""");
            await File.WriteAllTextAsync(existingOutput, "keep this file");

            await Assert.That(
                () => _command.ExecuteAsync(
                    new CommandContext(Enumerable.Empty<string>(), new EmptyRemainingArguments(), "codegeneration", null),
                    new CodeGenerationVerb
                    {
                        Config = configPath,
                        TargetDirectory = tempDirectory
                    },
                    CancellationToken.None
                )).Throws<InvalidOperationException>();

            await Assert.That(_connection.GetServiceCallCount).IsEqualTo(0);
            await Assert.That(await File.ReadAllTextAsync(existingOutput)).IsEqualTo("keep this file");
        }
        finally
        {
            Directory.Delete(tempDirectory, true);
        }
    }
}
