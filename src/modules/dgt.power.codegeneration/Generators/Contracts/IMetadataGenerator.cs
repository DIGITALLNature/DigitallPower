// SPDX-FileCopyrightText: DIGITALL Nature
// SPDX-License-Identifier: MS-RL

using dgt.power.codegeneration.Base;

namespace dgt.power.codegeneration.Generators.Contracts;

public interface IMetadataGenerator
{
    void PrepareDirectory(CodeGenerationVerb args);
    Task GenerateEntitiesAsync(CodeGenerationVerb args, DotNetCodeGenerationConfig config, CancellationToken cancellationToken = default);
}
