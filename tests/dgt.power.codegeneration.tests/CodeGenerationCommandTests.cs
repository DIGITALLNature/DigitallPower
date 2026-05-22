// Copyright (c) DIGITALL Nature. All rights reserved
// DIGITALL Nature licenses this file to you under the Microsoft Public License.

using dgt.power.codegeneration.Base;
using dgt.power.codegeneration.Logic;
using dgt.power.codegeneration.Services.Contracts;
using dgt.power.common.Logic;
using dgt.power.tests;
using FakeItEasy;
using Spectre.Console.Cli;

namespace dgt.power.codegeneration.tests;

public class CodeGenerationCommandTests
{
    private readonly ICommand<CodeGenerationVerb> _command;

    public CodeGenerationCommandTests()
    {
        var tracer = new TestTracer();
        var configResolver = new ConfigResolver(tracer);
        var dotNetWorker = A.Fake<DotNetWorker>();
        var typescriptWorker = A.Fake<TypescriptWorker>();
        var metadataWorker = A.Fake<MetadataWorker>();
        var metadataService = A.Fake<IMetadataService>();
        _command = new CodeGenerationCommand(tracer, configResolver,
            dotNetWorker, typescriptWorker, metadataWorker, metadataService);
    }

    [Test]
    public async Task ShouldExecuteALlCommands() =>
        await Assert.That(
            _command.ExecuteAsync(new CommandContext(Enumerable.Empty<string>(), A.Dummy<IRemainingArguments>(), "codegeneration", null),
                new CodeGenerationVerb
                {
                    Config = "Resources/CodeGenerationCommand/config.json"
                },CancellationToken.None
            ).GetAwaiter().GetResult()).IsEqualTo(0);

    [Test]
    public async Task ShouldFailOnMissingConfiguration() =>
        await Assert.That(
            _command.ExecuteAsync(new CommandContext(Enumerable.Empty<string>(),A.Dummy<IRemainingArguments>(), "codegeneration", null),
                new CodeGenerationVerb
                {
                    Config = "missing.json"
                },CancellationToken.None
            ).GetAwaiter().GetResult()).IsEqualTo(-1);
}
