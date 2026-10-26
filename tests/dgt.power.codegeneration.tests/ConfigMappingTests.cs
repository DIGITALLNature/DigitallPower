// SPDX-FileCopyrightText: DIGITALL Nature
// SPDX-License-Identifier: MS-RL

using System.Text.Json;
using dgt.power.codegeneration.Base;

namespace dgt.power.codegeneration.tests;

public class ConfigMappingTests
{
    [Test]
    public async Task ResolveV2DotNetConfig_ShouldDeserializeNestedShape()
    {
        const string json = """
                            {
                              "version": 2,
                              "type": "dotnet",
                              "namespace": "Contoso.Dataverse.Model",
                              "entities": {
                                "names": ["account"],
                                "fromSolutions": ["ContosoCore"],
                                "mask": "contoso_*"
                              },
                              "optionSets": ["contoso_status"],
                              "output": {
                                "target": "Framework",
                                "virtual": true,
                                "editableReadOnly": true,
                                "include": {
                                  "navigationProps": false,
                                  "metadata": true
                                }
                              }
                            }
                            """;

        var config = CodeGenerationConfigLoader.LoadFromFile(WriteConfigFile("v2-dotnet.json", json))
            as DotNetCodeGenerationConfig ?? throw new InvalidOperationException("Expected V2 .NET config.");

        await Assert.That(config.Namespace).IsEqualTo("Contoso.Dataverse.Model");
        await Assert.That(config.Entities.Names).IsEquivalentTo(["account"]);
        await Assert.That(config.Entities.FromSolutions).IsEquivalentTo(["ContosoCore"]);
        await Assert.That(config.Entities.Mask).IsEqualTo("contoso_*");
        await Assert.That(config.OptionSets).IsEquivalentTo(["contoso_status"]);
        await Assert.That(config.Output.Target).IsEqualTo(DotNetTarget.Framework);
        await Assert.That(config.Output.Virtual).IsTrue();
        await Assert.That(config.Output.EditableReadOnly).IsTrue();
        await Assert.That(config.Output.Include.NavigationProps).IsFalse();
        await Assert.That(config.Output.Include.Metadata).IsTrue();
    }

    [Test]
    public async Task ResolveV2TypeScriptConfig_ShouldDeserializeNestedShape()
    {
        const string json = """
                            {
                              "version": 2,
                              "type": "typescript",
                              "$schema": "https://example.test/typescript.schema.json",
                              "namespace": null,
                              "entities": {
                                "names": ["account"],
                                "fromSolutions": ["ContosoCore"],
                                "mask": "contoso_*"
                              },
                              "optionSets": ["contoso_status"],
                              "output": {
                                "forms": {
                                  "filter": ["account.main.form"],
                                  "fromSolutions": true,
                                  "testHelpers": true
                                },
                                "customApis": false
                              }
                            }
                            """;

        var config = CodeGenerationConfigLoader.LoadFromFile(WriteConfigFile("v2-typescript.json", json))
            as TypeScriptCodeGenerationConfig ?? throw new InvalidOperationException("Expected V2 TypeScript config.");

        await Assert.That(config.Namespace).IsNull();
        await Assert.That(config.Entities.Names).IsEquivalentTo(["account"]);
        await Assert.That(config.Entities.FromSolutions).IsEquivalentTo(["ContosoCore"]);
        await Assert.That(config.Entities.Mask).IsEqualTo("contoso_*");
        await Assert.That(config.OptionSets).IsEquivalentTo(["contoso_status"]);
        await Assert.That(config.Output.Forms).IsNotNull();
        await Assert.That(config.Output.Forms!.Filter).IsEquivalentTo(["account.main.form"]);
        await Assert.That(config.Output.Forms.FromSolutions).IsTrue();
        await Assert.That(config.Output.Forms.TestHelpers).IsTrue();
        await Assert.That(config.Output.CustomApis).IsFalse();
    }

    [Test]
    [Arguments("""{"type":"dotnet","version":2}""", nameof(DotNetCodeGenerationConfig))]
    [Arguments("""{"$schema":"https://example.test/schema.json","version":2,"type":"typescript"}""", nameof(TypeScriptCodeGenerationConfig))]
    [Arguments("""{"version":2,"entities":{"names":["account"]},"type":"dotnet"}""", nameof(DotNetCodeGenerationConfig))]
    [Arguments("""{"version":2,"output":{"customApis":false},"type":"typescript"}""", nameof(TypeScriptCodeGenerationConfig))]
    [Arguments("""{"VERSION":2,"type":"dotnet","ENTITIES":{"NAMES":["account"]}}""", nameof(DotNetCodeGenerationConfig))]
    [Arguments("""{"version":2,"type":"dotnet",}""", nameof(DotNetCodeGenerationConfig))]
    public async Task ResolveConfig_ShouldDeserializePolymorphicallyRegardlessOfDiscriminatorOrder(
        string json,
        string expectedType)
    {
        var config = CodeGenerationConfigLoader.LoadFromFile(WriteConfigFile("polymorphic-config.json", json));

        await Assert.That(config.GetType().Name).IsEqualTo(expectedType);
    }

    [Test]
    [Arguments("""{"type":"dotnet"}""", "explicit config version is required")]
    [Arguments("""{"version":1,"type":"dotnet"}""", "Unsupported config version 1")]
    [Arguments("""{"version":3,"type":"dotnet"}""", "Unsupported config version 3")]
    [Arguments("""{"version":null,"type":"dotnet"}""", "version' must be the integer 2")]
    [Arguments("""{"version":"2","type":"dotnet"}""", "version' must be the integer 2")]
    [Arguments("""{"version":true,"type":"dotnet"}""", "version' must be the integer 2")]
    [Arguments("""{"version":{},"type":"dotnet"}""", "version' must be the integer 2")]
    [Arguments("""{"version":[],"type":"dotnet"}""", "version' must be the integer 2")]
    [Arguments("""{"version":2.5,"type":"dotnet"}""", "version' must be the integer 2")]
    public async Task ResolveInvalidVersion_ShouldFailActionably(string json, string expectedMessage)
    {
        var exception = ResolveAndCaptureException(json);

        await Assert.That(exception.Message).Contains(expectedMessage);
    }

    [Test]
    [Arguments("""{"version":2}""")]
    [Arguments("""{"version":2,"type":null}""")]
    [Arguments("""{"version":2,"type":1}""")]
    [Arguments("""{"version":2,"type":"full"}""")]
    [Arguments("""{"version":2,"Type":"dotnet"}""")]
    [Arguments("""{"version":2,"TYPE":"typescript"}""")]
    [Arguments("""{"version":2,"type":"DotNet"}""")]
    [Arguments("""{"version":2,"type":"TypeScript"}""")]
    public async Task ResolveInvalidTarget_ShouldExplainSupportedDiscriminators(string json)
    {
        var exception = ResolveAndCaptureException(json);

        await Assert.That(exception).IsTypeOf<InvalidOperationException>();
        await Assert.That(exception.Message)
            .IsEqualTo("Config requires a case-sensitive 'type' property set to 'dotnet' or 'typescript'.");
        await Assert.That(exception.InnerException is JsonException or NotSupportedException).IsTrue();
    }

    [Test]
    [Arguments("""{"version":2,"type":"dotnet","type":"typescript"}""")]
    [Arguments("""{"version":2,"type":"dotnet","type":"dotnet"}""")]
    [Arguments("""{"version":2,"version":2,"type":"dotnet"}""")]
    [Arguments("""{"version":2,"VERSION":2,"type":"dotnet"}""")]
    [Arguments("""{"version":2,"type":"dotnet","entities":{},"Entities":{}}""")]
    public async Task ResolveDuplicateProperty_ShouldFail(string json)
    {
        var exception = ResolveAndCaptureException(json);

        await Assert.That(exception is JsonException or NotSupportedException).IsTrue();
    }

    [Test]
    [Arguments("""{"version":2,"type":"typescript","EntityFilters":[]}""", "Attribute-level filters were removed without replacement")]
    [Arguments("""{"version":2,"type":"typescript","entityfilters":[]}""", "Attribute-level filters were removed without replacement")]
    [Arguments("""{"version":2,"type":"typescript","EntityRefFilters":[]}""", "Attribute-level filters were removed without replacement")]
    [Arguments("""{"version":2,"type":"typescript","EntityFormFilters":[]}""", "Attribute-level filters were removed without replacement")]
    [Arguments("""{"version":2,"type":"typescript","TypescriptGeneratorVersion":"Full"}""", "no longer has a mode selector")]
    public async Task ResolveRetiredSetting_ShouldFailActionably(string json, string expectedMessage)
    {
        var exception = ResolveAndCaptureException(json);

        await Assert.That(exception).IsTypeOf<InvalidOperationException>();
        await Assert.That(exception.Message).Contains(expectedMessage);
        await Assert.That(exception.Message).Contains("docs/guides/migrations/2.x-to-3.x.md");
    }

    private static string WriteConfigFile(string fileName, string json)
    {
        var directory = Path.Combine(AppContext.BaseDirectory, "ConfigMappingTestsArtifacts");
        Directory.CreateDirectory(directory);
        var path = Path.Combine(directory, $"{Path.GetFileNameWithoutExtension(fileName)}-{Guid.NewGuid():N}.json");
        File.WriteAllText(path, json);
        return path;
    }

    private static Exception ResolveAndCaptureException(string json)
    {
        var path = Path.Combine(Path.GetTempPath(), $"dgtp-codegen-{Guid.NewGuid():N}.json");
        File.WriteAllText(path, json);
        try
        {
            try
            {
                CodeGenerationConfigLoader.LoadFromFile(path);
            }
            catch (Exception exception) when (exception is InvalidOperationException or JsonException or NotSupportedException)
            {
                return exception;
            }

            throw new InvalidOperationException("Expected config resolution to fail.");
        }
        finally
        {
            File.Delete(path);
        }
    }
}
