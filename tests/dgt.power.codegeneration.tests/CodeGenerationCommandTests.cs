// Copyright (c) DIGITALL Nature. All rights reserved
// DIGITALL Nature licenses this file to you under the Microsoft Public License.

using dgt.power.codegeneration.Base;
using dgt.power.codegeneration.Generators.Contracts;
using dgt.power.codegeneration.Services.Contracts;
using dgt.power.common.Logic;
using dgt.power.tests;
using Spectre.Console;
using Spectre.Console.Cli;

namespace dgt.power.codegeneration.tests;

#pragma warning disable CS0618 // Tests use legacy V1 CodeGenerationConfig

[NotInParallel("AnsiConsole")]
public class CodeGenerationCommandTests
{
    private readonly ICommand<CodeGenerationVerb> _command;

    public CodeGenerationCommandTests()
    {
        var tracer = new TestTracer();
        var configResolver = new ConfigResolver(tracer);

        var dotNetGeneratorMock = Mock.Of<IDotNetGenerator>();
        dotNetGeneratorMock.Generate(Any<CodeGenerationVerb>(), Any<DotNetCodeGenerationConfig>()).Returns(true);

        var typeScriptGeneratorMock = Mock.Of<ITypeScriptGenerator>();
        typeScriptGeneratorMock.Generate(Any<CodeGenerationVerb>(), Any<TypeScriptCodeGenerationConfig>()).Returns(true);
        typeScriptGeneratorMock.Generate(Any<CodeGenerationVerb>(), Any<CodeGenerationConfig>()).Returns(true);

        var metadataServiceMock = Mock.Of<IMetadataService>();

        _command = new CodeGenerationCommand(tracer, configResolver,
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
    public async Task ShouldFailOnMissingConfiguration() =>
        await Assert.That(
            _command.ExecuteAsync(new CommandContext(Enumerable.Empty<string>(), new EmptyRemainingArguments(), "codegeneration", null),
                new CodeGenerationVerb
                {
                    Config = "missing.json"
                }, CancellationToken.None
            ).GetAwaiter().GetResult()).IsEqualTo(-1);
}
