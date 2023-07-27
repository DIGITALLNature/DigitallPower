// Copyright (c) DIGITALL Nature. All rights reserved
// DIGITALL Nature licenses this file to you under the Microsoft Public License.

using System.Runtime.Serialization;
using System.Text;
using System.Xml;
using dgt.power.codegeneration.Base;
using dgt.power.codegeneration.Constants;
using dgt.power.codegeneration.Services.Contracts;
using Microsoft.Xrm.Sdk.Metadata;
using Spectre.Console;

namespace dgt.power.codegeneration.Generators;

public class MetadataGenerator : IMetadataGenerator
{
    private readonly IMetadataService _metadataService;

    public MetadataGenerator(IMetadataService metadataService)
    {
        _metadataService = metadataService;
    }

    public void PrepareDirectory(CodeGenerationVerb args)
    {
        if (!Directory.Exists(Path.Combine(args.TargetDirectory, args.Folder)))
        {
            Directory.CreateDirectory(Path.Combine(args.TargetDirectory, args.Folder));
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
    }

    public void GenerateEntities(CodeGenerationVerb args, CodeGenerationConfig config)
    {

        foreach (var entity in config.Entities)
        {
            var metadata = _metadataService.RetrieveEntityMetadata(entity, EntityFilters.All);
            var fileName = Path.Combine(args.TargetDirectory, args.Folder, "MetaData",
                $"{metadata.LogicalName.ToLowerInvariant()}.xml");
            var serializer = new DataContractSerializer(typeof(EntityMetadata));
            var settings = new XmlWriterSettings {Indent = true, IndentChars = "\t", Encoding = Encoding.UTF8};

            using var file = XmlWriter.Create(fileName, settings);
            AnsiConsole.MarkupLine($"Creating File: [bold green]{fileName}[/]");
            serializer.WriteObject(file, metadata);
        }
    }
}
