// Copyright (c) DIGITALL Nature. All rights reserved
// DIGITALL Nature licenses this file to you under the Microsoft Public License.

using dgt.power.codegeneration.Base;
using dgt.power.codegeneration.Logic;
using dgt.power.codegeneration.Services.Contracts;
using dgt.power.common.Logic;
using dgt.power.tests;
using Spectre.Console;
using Spectre.Console.Cli;

namespace dgt.power.codegeneration.tests;

public class CodeGenerationCommandTests
{
    private readonly ICommand<CodeGenerationVerb> _command;

    public CodeGenerationCommandTests()
    {
        var tracer = new TestTracer();
        var configResolver = new ConfigResolver(tracer);

        var dotNetWorkerMock = Mock.Of<DotNetWorker>(null!, null!, null!, null!);
        dotNetWorkerMock.Invoke(Any<CodeGenerationVerb>()).Returns(true);

        var typescriptWorkerMock = Mock.Of<TypescriptWorker>(null!, null!, null!, null!);
        typescriptWorkerMock.Invoke(Any<CodeGenerationVerb>()).Returns(true);

        var metadataWorkerMock = Mock.Of<MetadataWorker>(null!, null!, null!, null!);
        metadataWorkerMock.Invoke(Any<CodeGenerationVerb>()).Returns(true);

        var metadataServiceMock = Mock.Of<IMetadataService>();

        _command = new CodeGenerationCommand(tracer, configResolver,
            dotNetWorkerMock.Object, typescriptWorkerMock.Object, metadataWorkerMock.Object, metadataServiceMock.Object, AnsiConsole.Console);
    }

    [Test]
    public async Task ShouldExecuteALlCommands() =>
        await Assert.That(
            _command.ExecuteAsync(new CommandContext(Enumerable.Empty<string>(), new EmptyRemainingArguments(), "codegeneration", null),
                new CodeGenerationVerb
                {
                    Config = "Resources/CodeGenerationCommand/config.json"
                },CancellationToken.None
            ).GetAwaiter().GetResult()).IsEqualTo(0);

    [Test]
    public async Task ShouldFailOnMissingConfiguration() =>
        await Assert.That(
            _command.ExecuteAsync(new CommandContext(Enumerable.Empty<string>(), new EmptyRemainingArguments(), "codegeneration", null),
                new CodeGenerationVerb
                {
                    Config = "missing.json"
                },CancellationToken.None
            ).GetAwaiter().GetResult()).IsEqualTo(-1);
}
