// Copyright (c) DIGITALL Nature. All rights reserved
// DIGITALL Nature licenses this file to you under the Microsoft Public License.

using System.Runtime.Caching;
using System.Runtime.Serialization;
using System.Xml;
using dgt.power.codegeneration.Base;
using dgt.power.codegeneration.Constants;
using dgt.power.codegeneration.Generators;
using dgt.power.codegeneration.Generators.Contracts;
using dgt.power.codegeneration.Services;
using dgt.power.codegeneration.Services.Contracts;
using dgt.power.common;
using dgt.power.tests;
using dgt.power.tests.FakeExecutor;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Xrm.Sdk.Metadata;

namespace dgt.power.codegeneration.tests.Base;

public abstract class CodeGenerationTestsBase<TWorker> : WorkerTestsBase<TWorker, CodeGenerationVerb>
    where TWorker : PowerWorker<CodeGenerationVerb>
{
    protected CodeGenerationVerb DefaultVerb { get; } = new();
    protected CodeGenerationConfig DefaultConfig { get; } = new();
    private readonly DataContractSerializer _metadataSerializer = new(typeof(EntityMetadata));
    protected readonly IServiceCollection ServiceCollection = new TestServiceCollection()
        .AddScoped<IMetadataGenerator, MetadataGenerator>()
        .AddScoped<ITypescriptGeneratorFascade, TypescriptGeneratorFascade>()
        .AddScoped<IDotNetGenerator, DotNetGenerator>()
        .AddScoped<ObjectCache>(_ => MemoryCache.Default)
        .AddScoped<IMetadataService, MetadataService>();

    protected override WorkerTestContextBuilder<TWorker, CodeGenerationVerb> GetBuilder()
    {
        return base.GetBuilder()
            .WithServiceCollection(ServiceCollection)
            .WithFakeMessageExecutor(new RetrieveAllEntitiesExecutor());
    }

    protected string GetMetadataArtifactPath(string entityLogicalName) =>
        GetArtifactPath($"{DefaultVerb.Folder}/{Folders.Metadata}/{entityLogicalName}.xml");


    protected EntityMetadata GetEntityMetadataArtifact(string entityLogicalName)
    {
        var metadataArtifactPath = GetMetadataArtifactPath(entityLogicalName);
        using var fileStream = new FileStream(metadataArtifactPath, FileMode.Open, FileAccess.Read, FileShare.ReadWrite | FileShare.Delete);
        using var xmlReader1 = new XmlTextReader(fileStream);
        var metadata = (EntityMetadata?) _metadataSerializer.ReadObject(xmlReader1);
        if (metadata == null)
        {
            throw new FileNotFoundException(
                $"Metdata for entity '{entityLogicalName}' doesn't exist under path '{metadataArtifactPath}'");
        }

        return metadata;
    }

    protected EntityMetadata GetEntityMetadataResource(string entityLogicalName)
    {
        var metadataArtifactPath = GetMetadataResourcePath(entityLogicalName);
        using var fileStream =
            new FileStream(metadataArtifactPath, FileMode.Open, FileAccess.Read, FileShare.ReadWrite | FileShare.Delete);
        using var xmlReader2 = new XmlTextReader(fileStream);
        var metadata = (EntityMetadata?) _metadataSerializer.ReadObject(xmlReader2);
        if (metadata == null)
        {
            throw new FileNotFoundException(
                $"Metdata for entity '{entityLogicalName}' doesn't exist under path '{metadataArtifactPath}'");
        }

        return metadata;
    }

    protected string GetResourceAsString(string fileName) => File.ReadAllText(GetResourcePath(fileName));

    protected string GetMetadataResourcePath(string entityLogicalName) =>
        GetResourcePath($"{DefaultVerb.Folder}/{Folders.Metadata}/{entityLogicalName}.xml");
}
