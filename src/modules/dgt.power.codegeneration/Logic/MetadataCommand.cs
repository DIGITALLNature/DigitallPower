﻿// Copyright (c) DIGITALL Nature. All rights reserved
// DIGITALL Nature licenses this file to you under the Microsoft Public License.

using System.Diagnostics;
using dgt.power.codegeneration.Base;
using dgt.power.codegeneration.Generators;
using dgt.power.common;
using Microsoft.Xrm.Sdk;

namespace dgt.power.codegeneration.Logic;

public class MetadataCommand : PowerLogic<CodeGenerationVerb>
{
    private readonly IMetadataGenerator _generator;

    public MetadataCommand(ITracer tracer, IOrganizationService connection, IConfigResolver configResolver,
        IMetadataGenerator generator)
        : base(tracer,connection, configResolver)
    {
        _generator = generator;
    }


    protected override bool Invoke(CodeGenerationVerb args)
    {
        Debug.Assert(args != null, nameof(args) + " != null");
        if (!ConfigResolver.TryGetConfigFile<CodeGenerationConfig>(args.Config, out var config))
        {
            return Tracer.End(this, false);
        }

        _generator.PrepareDirectory(args);
        _generator.GenerateEntities(args, config);

        return true;
    }
}
