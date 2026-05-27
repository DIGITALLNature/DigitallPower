// Copyright (c) DIGITALL Nature. All rights reserved
// DIGITALL Nature licenses this file to you under the Microsoft Public License.

using System.Diagnostics;
using dgt.power.codegeneration.Base;
using dgt.power.codegeneration.Generators.Contracts;
using dgt.power.codegeneration.Generators.Worker;
using dgt.power.codegeneration.Services.Contracts;
using Spectre.Console;

namespace dgt.power.codegeneration.Generators;

public class TypescriptGeneratorFascade : ITypescriptGeneratorFascade
{
    private readonly IMetadataService _metadataService;
    private readonly IAnsiConsole _console;
    private ITypescriptGenerator _generator;

    public TypescriptGeneratorFascade(IMetadataService metadataService, IAnsiConsole console)
    {
        _metadataService = metadataService;
        _console = console;
    }

    #region ITypescriptGeneratorFascade Members

    /// <summary>
    ///     Prepares the directory for code generation.
    /// </summary>
    /// <param name="args">The code generation arguments.</param>
    public void PrepareDirectory(CodeGenerationVerb args)
    {
        // Ensure that the arguments are not null
        Debug.Assert(args != null, nameof(args) + " != null");
        Debug.Assert(_generator != null, nameof(_generator) + " != null");

        _generator.PrepareDirectory(args);
    }

    public void GenerateBoilerPlateFull(CodeGenerationVerb args, CodeGenerationConfig config)
    {
        Debug.Assert(args != null, nameof(args) + " != null");
        Debug.Assert(config != null, nameof(config) + " != null");
        Debug.Assert(_generator != null, nameof(_generator) + " != null");

        _generator.GenerateBoilerPlateFull(args, config);
    }

    /// <summary>
    ///     Generates TypeScript entities based on the provided code generation arguments and configuration.
    /// </summary>
    /// <param name="args">The code generation arguments.</param>
    /// <param name="config">The code generation configuration.</param>
    public void GenerateEntities(CodeGenerationVerb args, CodeGenerationConfig config)
    {
        // Ensure that the arguments and configuration are not null
        Debug.Assert(args != null, nameof(args) + " != null");
        Debug.Assert(config != null, nameof(config) + " != null");
        Debug.Assert(_generator != null, nameof(_generator) + " != null");

        _generator.GenerateEntities(args, config);
    }

    public void GenerateEntityRefsFull(CodeGenerationVerb args, CodeGenerationConfig config)
    {
        Debug.Assert(args != null, nameof(args) + " != null");
        Debug.Assert(config != null, nameof(config) + " != null");
        Debug.Assert(_generator != null, nameof(_generator) + " != null");

        _generator.GenerateEntityRefsFull(args, config);
    }

    public void GenerateEntityForms(CodeGenerationVerb args, CodeGenerationConfig config)
    {
        Debug.Assert(args != null, nameof(args) + " != null");
        Debug.Assert(config != null, nameof(config) + " != null");
        Debug.Assert(_generator != null, nameof(_generator) + " != null");

        _generator.GenerateEntityForms(args, config);
    }

    /// <summary>
    ///     Generates SDK messages for code generation.
    /// </summary>
    /// <param name="args">The code generation verb arguments.</param>
    /// <param name="config">The code generation configuration.</param>
    public void GenerateSdkMessages(CodeGenerationVerb args, CodeGenerationConfig config)
    {
        // Check if the arguments and configuration are not null
        Debug.Assert(args != null, nameof(args) + " != null");
        Debug.Assert(config != null, nameof(config) + " != null");
        Debug.Assert(_generator != null, nameof(_generator) + " != null");

        _generator.GenerateSdkMessages(args, config);
    }

    /// <summary>
    ///     Generates option sets based on the provided arguments and configuration
    /// </summary>
    /// <param name="args">The code generation verb</param>
    /// <param name="config">The code generation configuration</param>
    public void GenerateOptionSets(CodeGenerationVerb args, CodeGenerationConfig config)
    {
        // Ensure that the arguments and configuration are not null
        Debug.Assert(args != null, nameof(args) + " != null");
        Debug.Assert(config != null, nameof(config) + " != null");
        Debug.Assert(_generator != null, nameof(_generator) + " != null");

        _generator.GenerateOptionSets(args, config);
    }

    public void GenerateBusinessProcessFlowsFull(CodeGenerationVerb args, CodeGenerationConfig config)
    {
        Debug.Assert(args != null, nameof(args) + " != null");
        Debug.Assert(config != null, nameof(config) + " != null");
        Debug.Assert(_generator != null, nameof(_generator) + " != null");

        _generator.GenerateBusinessProcessFlowsFull(args, config);
    }

    public void GenerateCustomApis(CodeGenerationVerb args, CodeGenerationConfig config)
    {
        Debug.Assert(args != null, nameof(args) + " != null");
        Debug.Assert(config != null, nameof(config) + " != null");
        Debug.Assert(_generator != null, nameof(_generator) + " != null");

        _generator.GenerateCustomApis(args, config);
    }

    public void SetGenerationVersion(TypescriptGeneratorVersion generatorVersion)
    {
        switch (generatorVersion)
        {
            case TypescriptGeneratorVersion.Full:
                _generator = new TypescriptGeneratorWorkerFull(_metadataService, _console);
                break;
            case TypescriptGeneratorVersion.Light:
                _generator = new TypescriptGeneratorWorkerLight(_metadataService, _console);
                break;
            default:
                throw new ArgumentOutOfRangeException(nameof(generatorVersion), generatorVersion, null);
        }
    }

    #endregion
}
