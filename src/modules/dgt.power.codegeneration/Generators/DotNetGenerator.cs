// Copyright (c) DIGITALL Nature. All rights reserved
// DIGITALL Nature licenses this file to you under the Microsoft Public License.

using System.Diagnostics;
using dgt.power.codegeneration.Base;
using dgt.power.codegeneration.Constants;
using dgt.power.codegeneration.Generators.Contracts;
using dgt.power.codegeneration.Logic;
using dgt.power.codegeneration.Services.Contracts;
using dgt.power.codegeneration.Templates.dotnet;
using dgt.power.codegeneration.Templates.tsl.ViewModels;
using Fluid;
using Microsoft.Xrm.Sdk.Metadata;
using Spectre.Console;
using static dgt.power.codegeneration.Constants.FileNames;

namespace dgt.power.codegeneration.Generators;

public class DotNetGenerator(IMetadataService metadataService, IAnsiConsole console) : IDotNetGenerator
{
    /// <inheritdoc />
    public bool Generate(CodeGenerationVerb args, DotNetCodeGenerationConfig config)
    {
        PrepareDirectory(args);
        GenerateRequests(args, config);
        GenerateSdkMessageNames(args, config);
        GenerateOptionSets(args, config);
        GenerateContext(args, config);
        GenerateEntities(args, config);
        GenerateMetadata(args, config);
        return true;
    }

    private static void PrepareDirectory(CodeGenerationVerb args)
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

    private void GenerateRequests(CodeGenerationVerb args, DotNetCodeGenerationConfig config)
    {
        Debug.Assert(args != null, nameof(args) + " != null");
        Debug.Assert(config != null, nameof(config) + " != null");

        if (config.Requests.Count == 0)
        {
            return;
        }

        var requests = metadataService.RetrieveRequests(config.Requests);
       
        var fileName = Path.Combine(args.TargetDirectory, args.Folder, Folders.DotNet, $"{DotNet.Actions}.cs");
        console.MarkupLine($"Creating File: [bold green]{fileName}[/]");

        using var file = File.CreateText(fileName);
        var context = DotNetLiquidRenderer.CreateContext();
        context.SetValue("NameSpace", config.Namespace);
        context.SetValue("Actions", requests);

        var content = DotNetLiquidRenderer.Render("Action.dotnet.liquid", context);
        file.Write(content);
    }

    private void GenerateSdkMessageNames(CodeGenerationVerb args, DotNetCodeGenerationConfig config)
    {
        Debug.Assert(args != null, nameof(args) + " != null");
        Debug.Assert(config != null, nameof(config) + " != null");

        var sdkMessages = metadataService.RetrieveSdkMessageNames(config.Requests);

        var fileName = Path.Combine(args.TargetDirectory, args.Folder, Folders.DotNet, $"{DotNet.SdkMessageNames}.cs");
        using var file = File.CreateText(fileName);
        console.MarkupLine($"Creating File: [bold green]{fileName}[/]");

        var context = DotNetLiquidRenderer.CreateContext();
        context.SetValue("NameSpace", config.Namespace);
        context.SetValue("SdkMessages", sdkMessages.Select(s => new SdkMessageViewModel(s)).ToArray());

        var content = DotNetLiquidRenderer.Render("SdkMessages.dotnet.liquid", context);
        file.Write(content);
    }

    private void GenerateOptionSets(CodeGenerationVerb args, DotNetCodeGenerationConfig config)
    {
        Debug.Assert(args != null, nameof(args) + " != null");
        Debug.Assert(config != null, nameof(config) + " != null");

        if (config.OptionSets.Count == 0)
        {
            return;
        }

        var optionSets = metadataService.RetrieveOptionSets(config.OptionSets);
        var fileName = Path.Combine(args.TargetDirectory, args.Folder, Folders.DotNet, $"{DotNet.OptionSetValues}.cs");

        using var file = File.CreateText(fileName);
        console.MarkupLine($"Creating File: [bold green]{fileName}[/]");

        var context = DotNetLiquidRenderer.CreateContext();
        context.SetValue("NameSpace", config.Namespace);
        context.SetValue("OptionSets", optionSets.Select(kvp => new OptionViewModel(kvp)).ToArray());

        var content = DotNetLiquidRenderer.Render("OptionSets.dotnet.liquid", context);
        file.Write(content);
    }

    private void GenerateContext(CodeGenerationVerb args, DotNetCodeGenerationConfig config)
    {
        Debug.Assert(args != null, nameof(args) + " != null");
        Debug.Assert(config != null, nameof(config) + " != null");

        if (!config.Output.Include.Context)
        {
            return;
        }

        var contextFile = Path.Combine(args.TargetDirectory, args.Folder, Folders.DotNet, $"{DotNet.Context}.cs");

        using var file = File.CreateText(contextFile);
        console.MarkupLine($"Creating File: [bold green]{contextFile}[/]");

        var context = DotNetLiquidRenderer.CreateContext();
        context.SetValue("NameSpace", config.Namespace);

        var content = DotNetLiquidRenderer.Render("Context.dotnet.liquid", context);
        file.Write(content);
    }

    private void GenerateEntities(CodeGenerationVerb args, DotNetCodeGenerationConfig config)
    {
        Debug.Assert(args != null, nameof(args) + " != null");
        Debug.Assert(config != null, nameof(config) + " != null");

        foreach (var entity in config.Entities.Names)
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

    private void GenerateMetadata(CodeGenerationVerb args, DotNetCodeGenerationConfig config)
    {
        Debug.Assert(args != null, nameof(args) + " != null");
        Debug.Assert(config != null, nameof(config) + " != null");

        if (!config.Output.Include.Metadata)
        {
            return;
        }

        if (!Directory.Exists(Path.Combine(args.TargetDirectory, args.Folder, Folders.Metadata)))
        {
            Directory.CreateDirectory(Path.Combine(args.TargetDirectory, args.Folder, Folders.Metadata));
        }
        else
        {
            Directory.GetFiles(Path.Combine(args.TargetDirectory, args.Folder, Folders.Metadata), "*.xml")
                .ToList()
                .ForEach(File.Delete);
        }

        foreach (var entity in config.Entities.Names)
        {
            var metadata = metadataService.RetrieveEntityMetadata(entity, EntityFilters.All);
            var fileName = Path.Combine(args.TargetDirectory, args.Folder, Folders.Metadata,
                $"{metadata.LogicalName.ToLowerInvariant()}.xml");
            var serializer = new System.Runtime.Serialization.DataContractSerializer(typeof(EntityMetadata));
            var settings = new System.Xml.XmlWriterSettings { Indent = true, IndentChars = "\t", Encoding = System.Text.Encoding.UTF8 };

            using var file = System.Xml.XmlWriter.Create(fileName, settings);
            console.MarkupLine($"Creating File: [bold green]{fileName}[/]");
            serializer.WriteObject(file, metadata);
        }
    }
}
