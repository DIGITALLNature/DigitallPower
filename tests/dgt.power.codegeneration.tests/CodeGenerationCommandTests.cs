// Copyright (c) DIGITALL Nature. All rights reserved
// DIGITALL Nature licenses this file to you under the Microsoft Public License.

using dgt.power.codegeneration.Base;
using dgt.power.codegeneration.Generators.Contracts;
using dgt.power.codegeneration.Services.Contracts;
using dgt.power.common;
using dgt.power.tests;
using Spectre.Console;
using Spectre.Console.Cli;

namespace dgt.power.codegeneration.tests;


[NotInParallel("AnsiConsole")]
public class CodeGenerationCommandTests
{
    private readonly ICommand<CodeGenerationVerb> _command;

    public CodeGenerationCommandTests()
    {
        var dotNetGeneratorMock = Mock.Of<IDotNetGenerator>();
        dotNetGeneratorMock.GenerateAsync(Any<CodeGenerationVerb>(), Any<DotNetCodeGenerationConfig>(), Any<CancellationToken>()).Returns(true);

        var typeScriptGeneratorMock = Mock.Of<ITypeScriptGenerator>();
        typeScriptGeneratorMock.GenerateAsync(Any<CodeGenerationVerb>(), Any<TypeScriptCodeGenerationConfig>(), Any<CancellationToken>()).Returns(true);
        typeScriptGeneratorMock.GenerateAsync(Any<CodeGenerationVerb>(), Any<CodeGenerationConfig>(), Any<CancellationToken>()).Returns(true);

        var metadataServiceMock = Mock.Of<IMetadataService>();

        _command = new CodeGenerationCommand(new TestSpanFactory(),
            dotNetGeneratorMock.Object, typeScriptGeneratorMock.Object,
            metadataServiceMock.Object, AnsiConsole.Console);
    }

    [Test]
    public async Task ShouldExecuteALlCommands() =>
        await Assert.That(
            _command.ExecuteAsync(new CommandContext(Enumerable.Empty<string>(), new EmptyRemainingArguments(), "codegeneration", null),
                new CodeGenerationVerb
                {
                    Config = "Resources/CodeGenerationCommand/config.json"
                }, CancellationToken.None
            ).GetAwaiter().GetResult()).IsEqualTo(0);

    [Test]
    public async Task ShouldHonorCancellationBeforeLoadingConfiguration()
    {
        using var cancellationTokenSource = new CancellationTokenSource();
        cancellationTokenSource.Cancel();

        await Assert.That(
            () => _command.ExecuteAsync(
                new CommandContext(Enumerable.Empty<string>(), new EmptyRemainingArguments(), "codegeneration", null),
                new CodeGenerationVerb { Config = "missing.json" },
                cancellationTokenSource.Token)).Throws<OperationCanceledException>();
    }

    [Test]
    public async Task ShouldFailOnMissingConfiguration() =>
        await Assert.That(
            () => _command.ExecuteAsync(new CommandContext(Enumerable.Empty<string>(), new EmptyRemainingArguments(), "codegeneration", null),
                new CodeGenerationVerb
                {
                    Config = "missing.json"
                }, CancellationToken.None
            )).Throws<FileNotFoundException>();
}
