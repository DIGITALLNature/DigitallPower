// Copyright (c) DIGITALL Nature. All rights reserved
// DIGITALL Nature licenses this file to you under the Microsoft Public License.

using dgt.power.codegeneration.Base;
using dgt.power.codegeneration.Generators.Contracts;
using dgt.power.codegeneration.Generators.Strategy;
using dgt.power.codegeneration.Services.Contracts;
using Spectre.Console;

namespace dgt.power.codegeneration.Generators;

public class TypeScriptGenerator(IMetadataService metadataService, IAnsiConsole console)
    : ITypeScriptGenerator
{
    /// <inheritdoc />
    public async Task<bool> GenerateAsync(CodeGenerationVerb args, TypeScriptCodeGenerationConfig config, CancellationToken cancellationToken = default)
    {
        cancellationToken.ThrowIfCancellationRequested();
        var strategy = new TypescriptLightGenerationStrategy(metadataService, console);
        return await strategy.GenerateAsync(args, config, cancellationToken);
    }

    /// <inheritdoc />
    public async Task<bool> GenerateAsync(CodeGenerationVerb args, CodeGenerationConfig config, CancellationToken cancellationToken = default)
    {
        cancellationToken.ThrowIfCancellationRequested();
        ArgumentNullException.ThrowIfNull(args);
        ArgumentNullException.ThrowIfNull(config);

        ITypescriptGenerationStrategy strategy = config.TypescriptGeneratorVersion switch
        {
            TypescriptGeneratorVersion.Full => new TypescriptFullGenerationStrategy(metadataService, console),
            TypescriptGeneratorVersion.Light => new TypescriptLightGenerationStrategy(metadataService, console),
            _ => throw new ArgumentOutOfRangeException(nameof(config), config.TypescriptGeneratorVersion, "Unknown TypeScript generator version.")
        };

        return await strategy.GenerateAsync(args, config, cancellationToken);
    }
}
