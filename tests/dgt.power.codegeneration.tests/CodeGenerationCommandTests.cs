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

    public CodeGenerationCommandTests()
    {
        var dotNetGeneratorMock = Mock.Of<IDotNetGenerator>();
        dotNetGeneratorMock.Generate(Any<CodeGenerationVerb>(), Any<DotNetCodeGenerationConfig>()).Returns(true);

        var typeScriptGeneratorMock = Mock.Of<ITypeScriptGenerator>();
        typeScriptGeneratorMock.Generate(Any<CodeGenerationVerb>(), Any<TypeScriptCodeGenerationConfig>()).Returns(true);
        typeScriptGeneratorMock.Generate(Any<CodeGenerationVerb>(), Any<CodeGenerationConfig>()).Returns(true);

        var metadataServiceMock = Mock.Of<IMetadataService>();

        _command = new CodeGenerationCommand(new TestSpanFactory(), new StubConnection(),
            dotNetGeneratorMock.Object, typeScriptGeneratorMock.Object,
            metadataServiceMock.Object, AnsiConsole.Console);
    }

    private sealed class StubConnection : IDataverseConnection
    {
        public Task<IOrganizationServiceAsync2> GetServiceAsync() => Task.FromResult(Mock.Of<IOrganizationServiceAsync2>().Object);

        public Task<bool> CheckAuthAsync() => Task.FromResult(true);

        public Task RefreshAuthAsync() => Task.CompletedTask;
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
            () => _command.ExecuteAsync(new CommandContext(Enumerable.Empty<string>(), new EmptyRemainingArguments(), "codegeneration", null),
                new CodeGenerationVerb
                {
                    Config = "missing.json"
                }, CancellationToken.None
            )).Throws<FileNotFoundException>();
}
