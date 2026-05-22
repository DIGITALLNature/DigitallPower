// Copyright (c) DIGITALL Nature. All rights reserved
// DIGITALL Nature licenses this file to you under the Microsoft Public License.

using dgt.power.codegeneration.Base;
using dgt.power.codegeneration.Logic;
using dgt.power.codegeneration.Services.Contracts;
using dgt.power.common.Logic;
using dgt.power.tests;
using NSubstitute;
using Spectre.Console.Cli;

namespace dgt.power.codegeneration.tests;

public class CodeGenerationCommandTests
{
    private readonly ICommand<CodeGenerationVerb> _command;

    public CodeGenerationCommandTests()
    {
        var tracer = new TestTracer();
        var configResolver = new ConfigResolver(tracer);
        var dotNetWorker = Substitute.For<DotNetWorker>(null!, null!, null!, null!);
        var typescriptWorker = Substitute.For<TypescriptWorker>(null!, null!, null!, null!);
        var metadataWorker = Substitute.For<MetadataWorker>(null!, null!, null!, null!);
        var metadataService = Substitute.For<IMetadataService>();
        _command = new CodeGenerationCommand(tracer, configResolver,
            dotNetWorker, typescriptWorker, metadataWorker, metadataService);
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
