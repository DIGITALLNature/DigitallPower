// SPDX-FileCopyrightText: DIGITALL Nature
// SPDX-License-Identifier: MS-RL

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
    public async Task<bool> GenerateAsync(CodeGenerationVerb args, DotNetCodeGenerationConfig config, CancellationToken cancellationToken = default)
    {
        cancellationToken.ThrowIfCancellationRequested();
        PrepareDirectory(args);
        await GenerateRequestsAsync(args, config, cancellationToken);
        await GenerateSdkMessageNamesAsync(args, config, cancellationToken);
        await GenerateOptionSetsAsync(args, config, cancellationToken);
        GenerateContext(args, config);
        await GenerateEntitiesAsync(args, config, cancellationToken);
        await GenerateMetadataAsync(args, config, cancellationToken);
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

    private async Task GenerateRequestsAsync(CodeGenerationVerb args, DotNetCodeGenerationConfig config, CancellationToken cancellationToken)
    {
        Debug.Assert(args != null, nameof(args) + " != null");
        Debug.Assert(config != null, nameof(config) + " != null");

        if (config.Requests.Count == 0)
        {
            return;
        }

        var requests = await metadataService.RetrieveRequestsAsync(config.Requests, cancellationToken);
       
        var fileName = Path.Combine(args.TargetDirectory, args.Folder, Folders.DotNet, $"{DotNet.Actions}.cs");
        console.MarkupLine($"Creating File: [bold green]{fileName}[/]");

        await using var file = File.CreateText(fileName);
        var context = DotNetLiquidRenderer.CreateContext();
        context.SetValue("NameSpace", config.Namespace);
        context.SetValue("Actions", requests);

        var content = DotNetLiquidRenderer.Render("Action.dotnet.liquid", context);
        await file.WriteAsync(content.AsMemory(), cancellationToken);
    }

    private async Task GenerateSdkMessageNamesAsync(CodeGenerationVerb args, DotNetCodeGenerationConfig config, CancellationToken cancellationToken)
    {
        Debug.Assert(args != null, nameof(args) + " != null");
        Debug.Assert(config != null, nameof(config) + " != null");

        var sdkMessages = await metadataService.RetrieveSdkMessageNamesAsync(config.Requests, cancellationToken);

        var fileName = Path.Combine(args.TargetDirectory, args.Folder, Folders.DotNet, $"{DotNet.SdkMessageNames}.cs");
        await using var file = File.CreateText(fileName);
        console.MarkupLine($"Creating File: [bold green]{fileName}[/]");

        var context = DotNetLiquidRenderer.CreateContext();
        context.SetValue("NameSpace", config.Namespace);
        context.SetValue("SdkMessages", sdkMessages.Select(s => new SdkMessageViewModel(s)).ToArray());

        var content = DotNetLiquidRenderer.Render("SdkMessages.dotnet.liquid", context);
        await file.WriteAsync(content.AsMemory(), cancellationToken);
    }

    private async Task GenerateOptionSetsAsync(CodeGenerationVerb args, DotNetCodeGenerationConfig config, CancellationToken cancellationToken)
    {
        Debug.Assert(args != null, nameof(args) + " != null");
        Debug.Assert(config != null, nameof(config) + " != null");

        if (config.OptionSets.Count == 0)
        {
            return;
        }

        var optionSets = await metadataService.RetrieveOptionSetsAsync(config.OptionSets, cancellationToken);
        var fileName = Path.Combine(args.TargetDirectory, args.Folder, Folders.DotNet, $"{DotNet.OptionSetValues}.cs");

        await using var file = File.CreateText(fileName);
        console.MarkupLine($"Creating File: [bold green]{fileName}[/]");

        var context = DotNetLiquidRenderer.CreateContext();
        context.SetValue("NameSpace", config.Namespace);
        context.SetValue("OptionSets", optionSets.Select(kvp => new OptionViewModel(kvp)).ToArray());

        var content = DotNetLiquidRenderer.Render("OptionSets.dotnet.liquid", context);
        await file.WriteAsync(content.AsMemory(), cancellationToken);
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

    private async Task GenerateEntitiesAsync(CodeGenerationVerb args, DotNetCodeGenerationConfig config, CancellationToken cancellationToken)
    {
        Debug.Assert(args != null, nameof(args) + " != null");
        Debug.Assert(config != null, nameof(config) + " != null");

        var entityMetadataByName = new Dictionary<string, EntityMetadata>(StringComparer.OrdinalIgnoreCase);
        foreach (var entity in config.Entities.Names)
        {
            entityMetadataByName[entity] = await metadataService.RetrieveEntityMetadataAsync(entity,
                EntityFilters.Attributes | EntityFilters.Relationships | EntityFilters.Entity, cancellationToken);
        }

        var systemLanguage = await metadataService.RetrieveOrganizationLanguageAsync(cancellationToken);
        foreach (var entity in config.Entities.Names)
        {
            var metadata = entityMetadataByName[entity];

            var fileName = Path.Combine(args.TargetDirectory, args.Folder, Folders.DotNet,
                $"{Formatter.CamelCase(metadata.SchemaName)}.cs");

            await using var file = File.CreateText(fileName);
            console.MarkupLine($"Creating File: [bold green]{fileName}[/]");

            var builder = new DotNetEntityViewModelBuilder(
                metadata,
                logicalName => entityMetadataByName[logicalName].SchemaName,
                config,
                systemLanguage,
                console);

            var context = DotNetLiquidRenderer.CreateContext();
            foreach (var (key, value) in builder.Build())
            {
                context.SetValue(key, value);
            }

            await file.WriteAsync(DotNetLiquidRenderer.Render("Entity.dotnet.liquid", context).AsMemory(), cancellationToken);
        }
    }

    private async Task GenerateMetadataAsync(CodeGenerationVerb args, DotNetCodeGenerationConfig config, CancellationToken cancellationToken)
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
            var metadata = await metadataService.RetrieveEntityMetadataAsync(entity, EntityFilters.All, cancellationToken);
            var fileName = Path.Combine(args.TargetDirectory, args.Folder, Folders.Metadata,
                $"{metadata.LogicalName.ToLowerInvariant()}.xml");
            var serializer = new System.Runtime.Serialization.DataContractSerializer(typeof(EntityMetadata));
            var settings = new System.Xml.XmlWriterSettings { Indent = true, IndentChars = "\t", Encoding = System.Text.Encoding.UTF8, Async = true };

            await using var file = System.Xml.XmlWriter.Create(fileName, settings);
            console.MarkupLine($"Creating File: [bold green]{fileName}[/]");
            serializer.WriteObject(file, metadata);
        }
    }
}
