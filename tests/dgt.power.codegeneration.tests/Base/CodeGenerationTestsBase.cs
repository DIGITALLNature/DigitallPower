// Copyright (c) DIGITALL Nature. All rights reserved
// DIGITALL Nature licenses this file to you under the Microsoft Public License.

using System.Runtime.Caching;
using System.Runtime.CompilerServices;
using System.Runtime.Serialization;
using System.Text;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Text.RegularExpressions;
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
using Digitall.Dataverse.Testing;
using Digitall.Dataverse.Testing.OrganizationRequests;
using Microsoft.Crm.Sdk.Messages;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Xrm.Sdk;
using Microsoft.Xrm.Sdk.Metadata;
using Spectre.Console;
using Spectre.Console.Testing;

namespace dgt.power.codegeneration.tests.Base;


/// <summary>
///     Base class for code generation tests. Provides fake service setup, DI resolution
///     of generators, and utility methods for artifact management.
/// </summary>
public abstract class CodeGenerationTestsBase : IDisposable
{
    protected TestConsole TestConsole { get; } = new();
    protected CodeGenerationVerb DefaultVerb { get; } = new();
    protected CodeGenerationConfig DefaultConfig { get; } = new();

    private readonly JsonSerializerOptions _jsonSerializerOptions = new()
    {
        Converters = { new JsonStringEnumConverter(JsonNamingPolicy.CamelCase) }
    };

    private readonly DataContractSerializer _metadataSerializer = new(typeof(EntityMetadata));

    /// <summary>
    ///     Override in subclasses to point to the correct resource directory.
    /// </summary>
    protected virtual string ResourceDirectory => "Resources";

    protected virtual string ArtifactDirectory { get; } = $"{Guid.NewGuid():N}";

    protected string GetResourcePath(string fileName) => Path.Combine(ResourceDirectory, fileName);
    protected string GetArtifactPath(string fileName) => Path.Combine(ArtifactDirectory, fileName);

    protected string GetMetadataArtifactPath(string entityLogicalName) =>
        GetArtifactPath($"{DefaultVerb.Folder}/{Folders.Metadata}/{entityLogicalName}.xml");

    protected string GetMetadataResourcePath(string entityLogicalName) =>
        GetResourcePath($"{DefaultVerb.Folder}/{Folders.Metadata}/{entityLogicalName}.xml");

    protected string GetResourceAsString(string fileName) => File.ReadAllText(GetResourcePath(fileName));

    protected FileInfo WriteConfigurationArtifact<TArtifact>(TArtifact artifact)
    {
        var json = Regex.Unescape(
            JsonSerializer.Serialize(JsonSerializer.Serialize(artifact, _jsonSerializerOptions))).Trim('"');

        var fileName = $"{Guid.NewGuid():N}.json";
        var artifactFqn = Path.Combine(Directory.GetCurrentDirectory(), ArtifactDirectory);

        if (!Directory.Exists(artifactFqn))
        {
            Directory.CreateDirectory(artifactFqn);
        }

        var path = Path.Combine(artifactFqn, fileName);
        File.WriteAllText(path, json, Encoding.UTF8);
        return new FileInfo(path);
    }

    protected EntityMetadata GetEntityMetadataArtifact(string entityLogicalName)
    {
        var metadataArtifactPath = GetMetadataArtifactPath(entityLogicalName);
        using var fileStream = new FileStream(metadataArtifactPath, FileMode.Open, FileAccess.Read,
            FileShare.ReadWrite | FileShare.Delete);
        using var xmlReader = new XmlTextReader(fileStream);
        var metadata = (EntityMetadata?)_metadataSerializer.ReadObject(xmlReader);
        return metadata ??
               throw new FileNotFoundException(
                   $"Metadata for entity '{entityLogicalName}' doesn't exist under path '{metadataArtifactPath}'");
    }

    protected EntityMetadata GetEntityMetadataResource(string entityLogicalName)
    {
        var metadataArtifactPath = GetMetadataResourcePath(entityLogicalName);
        using var fileStream = new FileStream(metadataArtifactPath, FileMode.Open, FileAccess.Read,
            FileShare.ReadWrite | FileShare.Delete);
        using var xmlReader = new XmlTextReader(fileStream);
        var metadata = (EntityMetadata?)_metadataSerializer.ReadObject(xmlReader);
        return metadata ??
               throw new FileNotFoundException(
                   $"Metadata for entity '{entityLogicalName}' doesn't exist under path '{metadataArtifactPath}'");
    }

    protected string GetTestFileName([CallerMemberName] string methodName = "")
    {
        return $"{GetType().Name}-{methodName}.json";
    }

    protected virtual CodeGenerationContextBuilder GetBuilder() => new(TestConsole);

    protected CodeGenerationContext GetContext() => GetBuilder().Build();

    public virtual void Dispose()
    {
        var artifactDirectory = ArtifactDirectory.TrimEnd('/');
        if (Directory.Exists(artifactDirectory))
        {
            Directory.Delete(artifactDirectory, true);
        }

        GC.SuppressFinalize(this);
    }
}

/// <summary>
///     Test context providing resolved generators and fake service.
/// </summary>
public class CodeGenerationContext
{
    private static readonly JsonSerializerOptions s_jsonOptions = new()
    {
        Converters = { new JsonStringEnumConverter(JsonNamingPolicy.CamelCase) },
        AllowTrailingCommas = true,
        PropertyNameCaseInsensitive = true
    };

    public IDotNetGenerator DotNetGenerator { get; }
    public ITypeScriptGenerator TypeScriptGenerator { get; }
    public IMetadataGenerator MetadataGenerator { get; }
    public IMetadataService MetadataService { get; }
    public IConfigResolver ConfigResolver { get; }
    public FakeOrganizationServiceAsync FakedService { get; }

    internal CodeGenerationContext(
        IDotNetGenerator dotNetGenerator,
        ITypeScriptGenerator typeScriptGenerator,
        IMetadataGenerator metadataGenerator,
        IMetadataService metadataService,
        IConfigResolver configResolver,
        FakeOrganizationServiceAsync fakedService)
    {
        DotNetGenerator = dotNetGenerator;
        TypeScriptGenerator = typeScriptGenerator;
        MetadataGenerator = metadataGenerator;
        MetadataService = metadataService;
        ConfigResolver = configResolver;
        FakedService = fakedService;
    }

    /// <summary>
    ///     Reads a V1 <see cref="CodeGenerationConfig"/> from the verb's config file path
    ///     and runs the TypeScript facade. Convenience method for tests that use on-disk config files.
    /// </summary>
    public bool ExecuteTypescriptFromConfigFile(CodeGenerationVerb verb)
    {
        var json = File.ReadAllText(verb.Config);
        var config = JsonSerializer.Deserialize<CodeGenerationConfig>(json, s_jsonOptions)
                     ?? throw new InvalidOperationException($"Failed to deserialize config from '{verb.Config}'.");
        return TypeScriptGenerator.Generate(verb, config);
    }
}

/// <summary>
///     Builder for <see cref="CodeGenerationContext"/>. Sets up fake org service, metadata, data, and DI.
/// </summary>
public class CodeGenerationContextBuilder
{
    private readonly List<Entity> _data = [];
    private readonly List<EntityMetadata> _metadata = [];
    private readonly List<RelationshipMetadataBase> _relationships = [];
    private readonly List<IOrganizationRequestFake> _requestFakes = [];
    private readonly List<Action<FakeOrganizationServiceAsync>> _customConfigurations = [];
    private Func<FakeOrganizationServiceAsync, IEnumerable<Entity>>? _dataPreparer;
    private readonly IAnsiConsole _console;

    public CodeGenerationContextBuilder(IAnsiConsole console)
    {
        _console = console;
        // Add default fakes
        _requestFakes.Add(new RetrieveAllEntitiesExecutor());
    }

    public CodeGenerationContext Build()
    {
        var service = new FakeOrganizationServiceAsync();

        // Merge project-specific fakes with custom fakes
        var allFakes = new Dictionary<Type, IOrganizationRequestFake>
        {
            [typeof(RetrieveCurrentOrganizationRequest)] = new RetrieveCurrentOrganizationExecutor()
        };

        foreach (var fake in _requestFakes)
        {
            allFakes[fake.ForType] = fake;
        }

        service.AddRequests(allFakes.Values);
        service.AddDefaultRequests();

        if (_metadata.Count != 0)
        {
            service.AddMetadata(_metadata);
        }

        if (_relationships.Count > 0)
        {
            service.AddRelationships(_relationships);
        }

        foreach (var config in _customConfigurations)
        {
            config(service);
        }

        if (_data.Count != 0 || _dataPreparer != null)
        {
            if (_dataPreparer != null)
            {
                _data.AddRange(_dataPreparer(service));
            }

            var registeredTypes = service.State.EntityMetadata.Keys.ToHashSet(StringComparer.OrdinalIgnoreCase);
            var missingTypes = _data
                .Select(e => e.LogicalName)
                .Where(name => !string.IsNullOrEmpty(name) && !registeredTypes.Contains(name))
                .Distinct(StringComparer.OrdinalIgnoreCase);

            foreach (var entityName in missingTypes)
            {
                var meta = new EntityMetadata();
                meta.GetType().GetProperty(nameof(EntityMetadata.LogicalName))!.SetValue(meta, entityName);
                service.AddMetadata(meta);
            }

            service.AddRange(_data);
        }

        var services = new TestServiceCollection()
            .AddScoped<IMetadataGenerator, MetadataGenerator>()
            .AddScoped<ITypeScriptGenerator, TypeScriptGenerator>()
            .AddScoped<IDotNetGenerator, DotNetGenerator>()
            .AddScoped<ObjectCache>(_ => MemoryCache.Default)
            .AddScoped<IMetadataService, MetadataService>()
            .AddScoped<IOrganizationService>(_ => service)
            .AddSingleton(_console)
            .AddSingleton<IConfiguration>(new ConfigurationBuilder()
                .AddInMemoryCollection(new Dictionary<string, string?>
                {
                    { "pollrate", TestFixtures.FakeCallDurations.ToString() }
                })
                .Build())
            .BuildServiceProvider();

        return new CodeGenerationContext(
            services.GetRequiredService<IDotNetGenerator>(),
            services.GetRequiredService<ITypeScriptGenerator>(),
            services.GetRequiredService<IMetadataGenerator>(),
            services.GetRequiredService<IMetadataService>(),
            services.GetRequiredService<IConfigResolver>(),
            service);
    }

    public CodeGenerationContextBuilder WithData(IEnumerable<Entity> data)
    {
        _data.AddRange(data);
        return this;
    }

    public CodeGenerationContextBuilder WithData(Entity data) => WithData([data]);

    public CodeGenerationContextBuilder WithData(ITuple tuple)
    {
        var data = Enumerable.Range(0, tuple.Length)
            .Select(i => tuple[i])
            .OfType<Entity>()
            .ToList();
        WithData(data);
        return this;
    }

    public CodeGenerationContextBuilder WithMetaData(EntityMetadata metadata) =>
        WithMetaData([metadata]);

    public CodeGenerationContextBuilder WithMetaData(IEnumerable<EntityMetadata> metadata)
    {
        _metadata.AddRange(metadata);
        return this;
    }

    public CodeGenerationContextBuilder WithRelationship(RelationshipMetadataBase relationship)
    {
        _relationships.Add(relationship);
        return this;
    }

    public CodeGenerationContextBuilder WithFakeMessageExecutor(IOrganizationRequestFake requestFake)
    {
        _requestFakes.Add(requestFake);
        return this;
    }

    public CodeGenerationContextBuilder WithCustomConfiguration(
        Action<FakeOrganizationServiceAsync> contextHandler)
    {
        _customConfigurations.Add(contextHandler);
        return this;
    }
}
