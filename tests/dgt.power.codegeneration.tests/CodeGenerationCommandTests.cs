// Copyright (c) DIGITALL Nature. All rights reserved
// DIGITALL Nature licenses this file to you under the Microsoft Public License.

using dgt.power.codegeneration.Base;
using dgt.power.codegeneration.Logic;
using dgt.power.codegeneration.Services.Contracts;
using dgt.power.common.Logic;
using dgt.power.tests;
using FakeItEasy;
using FluentAssertions;
using Spectre.Console.Cli;
using Xunit.Abstractions;

namespace dgt.power.codegeneration.tests;

public class CodeGenerationCommandTests
{
    private readonly CodeGenerationCommand _command;

    public CodeGenerationCommandTests(ITestOutputHelper testOutputHelper)
    {
        var tracer = new TestTracer(testOutputHelper);
        var configResolver = new ConfigResolver(tracer);
        var dotNetCommand = A.Fake<DotNetCommand>();
        var typescriptCommand = A.Fake<TypescriptCommand>();
        var metadataCommand = A.Fake<MetadataCommand>();
        var metadataService = A.Fake<IMetadataService>();
        _command = new CodeGenerationCommand(tracer, configResolver,
            dotNetCommand, typescriptCommand, metadataCommand, metadataService);
    }

    [Fact]
    public void ShouldExecuteALlCommands() =>
        _command.Execute(new CommandContext(Enumerable.Empty<string>(), A.Dummy<IRemainingArguments>(), "codegeneration", null),
            new CodeGenerationVerb
            {
                Config = "Resources/CodeGenerationCommand/config.json"
            }
        ).Should().Be(0);

    [Fact]
    public void ShouldFailOnMissingConfiguration() =>
        _command.Execute(new CommandContext(Enumerable.Empty<string>(),A.Dummy<IRemainingArguments>(), "codegeneration", null),
            new CodeGenerationVerb
            {
                Config = "missing.json"
            }
        ).Should().Be(-1);
}
