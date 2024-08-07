﻿// Copyright (c) DIGITALL Nature. All rights reserved
// DIGITALL Nature licenses this file to you under the Microsoft Public License.

using dgt.power.codegeneration.Base;
using dgt.power.codegeneration.Templates.ts;

namespace dgt.power.codegeneration.Generators;

public interface ITypescriptGenerator
{
    void PrepareDirectory(CodeGenerationVerb args);
    void GenerateBoilerPlate(CodeGenerationVerb args, CodeGenerationConfig config);
    void CreateTemplateFile(ITemplate template, string name, CodeGenerationVerb args);
    void GenerateEntities(CodeGenerationVerb args, CodeGenerationConfig config);
    void GenerateEntityRefs(CodeGenerationVerb args, CodeGenerationConfig config);
    void GenerateEntityForms(CodeGenerationVerb args, CodeGenerationConfig config);
    void GenerateSdkMessages(CodeGenerationVerb args, CodeGenerationConfig config);
    void GenerateOptionSets(CodeGenerationVerb args, CodeGenerationConfig config);
    void GenerateBusinessProcessFlows(CodeGenerationVerb args, CodeGenerationConfig config);
}