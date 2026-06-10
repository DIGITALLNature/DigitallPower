// Copyright (c) DIGITALL Nature. All rights reserved
// DIGITALL Nature licenses this file to you under the Microsoft Public License.

using System.Diagnostics;
using dgt.power.codegeneration.Base;
using dgt.power.codegeneration.Generators.Contracts;
using dgt.power.common;
using Microsoft.Xrm.Sdk;

namespace dgt.power.codegeneration.Logic;

public class DotNetWorker(
    ITracer tracer,
    IOrganizationService connection,
    IConfigResolver configResolver,
    IDotNetGenerator generator)
    : PowerWorker<CodeGenerationVerb>(tracer, connection, configResolver)
{
    protected override bool InvokeCore(CodeGenerationVerb args)
    {
        Debug.Assert(args != null, nameof(args) + " != null");
        if (!ConfigResolver.TryGetConfigFile<CodeGenerationConfig>(args.Config, out var config))
        {
            return Tracer.End(this, false);
        }

        generator.PrepareDirectory(args);
        generator.GenerateActions(args, config);
        generator.GenerateSdkMessages(args, config);
        generator.GenerateOptionSets(args, config);
        generator.GenerateContext(args, config);
        generator.GenerateEntities(args, config);
        return true;
    }
}
