// Copyright (c) DIGITALL Nature. All rights reserved
// DIGITALL Nature licenses this file to you under the Microsoft Public License.

using System.Diagnostics;
using dgt.power.codegeneration.Base;
using dgt.power.codegeneration.Constants;
using dgt.power.codegeneration.Logic;
using dgt.power.codegeneration.Services.Contracts;
using dgt.power.codegeneration.Templates;
using dgt.power.codegeneration.Templates.dotnet;
using dgt.power.codegeneration.Templates.tsl.ViewModels;
using Fluid;
using Fluid.Values;
using Microsoft.Xrm.Sdk.Metadata;
using Spectre.Console;
using static dgt.power.codegeneration.Constants.FileNames;

namespace dgt.power.codegeneration.Generators;

public class DotNetGenerator(IMetadataService metadataService, IAnsiConsole console) : IDotNetGenerator
{
    public void PrepareDirectory(CodeGenerationVerb args)
    {
        Debug.Assert(args != null, nameof(args) + " != null");

        if (!Directory.Exists(Path.Combine(args.TargetDirectory, args.Folder)))
        {
            Directory.CreateDirectory(Path.Combine(args.TargetDirectory, args.Folder));
        }

        if (!Directory.Exists(Path.Combine(args.TargetDirectory, args.Folder, Folders.DotNet)))
        {
            Directory.CreateDirectory(Path.Combine(args.TargetDirectory, args.Folder, Folders.DotNet));
        }
        else
        {
            Directory.GetFiles(Path.Combine(args.TargetDirectory, args.Folder, Folders.DotNet),
                "*.cs").ToList().ForEach(File.Delete);
        }
    }

    public void GenerateActions(CodeGenerationVerb args, CodeGenerationConfig config)
    {
        Debug.Assert(args != null, nameof(args) + " != null");
        Debug.Assert(config != null, nameof(config) + " != null");

        if (config.SuppressActions)
        {
            return;
        }


        var actions = metadataService.RetrieveActions(config);
        var apis = metadataService.RetrieveCustomAPIs(config);
        var fileName = Path.Combine(args.TargetDirectory, args.Folder, Folders.DotNet, $"{DotNet.Actions}.cs");

        console.MarkupLine($"Creating File: [bold green]{fileName}[/]");

        using var file = File.CreateText(fileName);

        var context = DotNetLiquidRenderer.CreateContext();
        context.SetValue("NameSpace", config.NameSpace);
        context.SetValue("Actions", actions.Concat(apis).ToArray());

        var content = DotNetLiquidRenderer.Render("Action.dotnet.liquid", context);
        file.Write(content);
    }

    public void GenerateSdkMessages(CodeGenerationVerb args, CodeGenerationConfig config)
    {
        Debug.Assert(args != null, nameof(args) + " != null");
        Debug.Assert(config != null, nameof(config) + " != null");

        if (config.SuppressSdkMessages)
        {
            return;
        }

        var sdkMessages = metadataService.RetrieveSdkMessageNames(config);

        // Apply filter
        IEnumerable<(string Name, string Message)> filtered = sdkMessages;
        if (config.SdkMessageFilters != null && config.SdkMessageFilters.Count > 0)
        {
            filtered = sdkMessages.Where(t => config.SdkMessageFilters.Contains(t.Message));
        }

        var fileName = Path.Combine(args.TargetDirectory, args.Folder, Folders.DotNet, $"{DotNet.SdkMessageNames}.cs");
        using var file = File.CreateText(fileName);
        console.MarkupLine($"Creating File: [bold green]{fileName}[/]");

        var context = DotNetLiquidRenderer.CreateContext();
        context.SetValue("NameSpace", config.NameSpace);
        context.SetValue("SdkMessages", filtered.Select(s => new SdkMessageViewModel(s)).ToArray());

        var content = DotNetLiquidRenderer.Render("SdkMessages.dotnet.liquid", context);
        file.Write(content);
    }

    public void GenerateOptionSets(CodeGenerationVerb args, CodeGenerationConfig config)
    {
        Debug.Assert(args != null, nameof(args) + " != null");
        Debug.Assert(config != null, nameof(config) + " != null");

        if (config.GlobalOptionSets.Count == 0)
        {
            return;
        }

        var optionSets = metadataService.RetrieveOptionSets(config);
        var fileName = Path.Combine(args.TargetDirectory, args.Folder, Folders.DotNet, $"{DotNet.OptionSetValues}.cs");

        using var file = File.CreateText(fileName);
        console.MarkupLine($"Creating File: [bold green]{fileName}[/]");

        var context = DotNetLiquidRenderer.CreateContext();
        context.SetValue("NameSpace", config.NameSpace);
        context.SetValue("OptionSets", optionSets.Select(kvp => new OptionViewModel(kvp)).ToArray());

        var content = DotNetLiquidRenderer.Render("OptionSets.dotnet.liquid", context);
        file.Write(content);
    }

    public void GenerateContext(CodeGenerationVerb args, CodeGenerationConfig config)
    {
        Debug.Assert(args != null, nameof(args) + " != null");
        Debug.Assert(config != null, nameof(config) + " != null");

        var contextFile = Path.Combine(args.TargetDirectory, args.Folder, Folders.DotNet, $"{DotNet.Context}.cs");

        using var file = File.CreateText(contextFile);
        console.MarkupLine($"Creating File: [bold green]{contextFile}[/]");

        var context = DotNetLiquidRenderer.CreateContext();
        context.SetValue("NameSpace", config.NameSpace);

        var content = DotNetLiquidRenderer.Render("Context.dotnet.liquid", context);
        file.Write(content);
    }

    public void GenerateEntities(CodeGenerationVerb args, CodeGenerationConfig config)
    {
        Debug.Assert(args != null, nameof(args) + " != null");
        Debug.Assert(config != null, nameof(config) + " != null");

        foreach (var entity in config.Entities)
        {
            var metadata = metadataService.RetrieveEntityMetadata(entity,
                EntityFilters.Attributes | EntityFilters.Relationships | EntityFilters.Entity);

            var fileName = Path.Combine(args.TargetDirectory, args.Folder, Folders.DotNet,
                $"{Formatter.CamelCase(metadata.SchemaName)}.cs");

            using var file = File.CreateText(fileName);
            console.MarkupLine($"Creating File: [bold green]{fileName}[/]");

            var builder = new DotNetEntityViewModelBuilder(
                metadata,
                logicalName => metadataService.RetrieveEntityMetadata(logicalName),
                config,
                metadataService.RetrieveOrganizationLanguage(),
                console);

            var context = DotNetLiquidRenderer.CreateContext();
            foreach (var (key, value) in builder.Build())
            {
                context.SetValue(key, value);
            }

            file.Write(DotNetLiquidRenderer.Render("Entity.dotnet.liquid", context));
        }
    }
}
