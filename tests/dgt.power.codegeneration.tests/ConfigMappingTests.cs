// Copyright (c) DIGITALL Nature. All rights reserved
// DIGITALL Nature licenses this file to you under the Microsoft Public License.

using dgt.power.codegeneration.Base;

namespace dgt.power.codegeneration.tests;

public class ConfigMappingTests
{
    [Test]
    public async Task ToDotNetConfig_ShouldMapLegacyFieldsToNestedV2Shape()
    {
        var config = new CodeGenerationConfig
        {
            NameSpace = "Contoso.Dataverse.Model",
            Entities = ["account"],
            Solutions = ["ContosoCore"],
            EntityMask = "contoso_*",
            UseBaseLanguage = true,
            GlobalOptionSets = ["contoso_status"],
            Actions = ["ActionA"],
            CustomAPIs = ["ApiA"],
            AdditionalSdkMessages = ["WhoAmI"],
            SuppressNullableSupport = true,
            Virtual = true,
            EditableReadOnlyProperties = true,
            SuppressContext = true,
            SuppressOptions = true,
            SuppressLogicalNames = true,
            SuppressRelations = true,
            SuppressNavigationProperties = true,
            SuppressEntityTypeCode = true,
            SuppressAlternateKeys = true,
            SuppressMetaData = true
        };

        var mapped = config.ToDotNetConfig();

        await Assert.That(mapped.Namespace).IsEqualTo("Contoso.Dataverse.Model");
        await Assert.That(mapped.Entities.Names).IsEquivalentTo(new[] { "account" });
        await Assert.That(mapped.Entities.FromSolutions).IsEquivalentTo(new[] { "ContosoCore" });
        await Assert.That(mapped.Entities.Mask).IsEqualTo("contoso_*");
        await Assert.That(mapped.Language).IsNull();
        await Assert.That(mapped.OptionSets).IsEquivalentTo(new[] { "contoso_status" });
        await Assert.That(mapped.Requests).IsEquivalentTo(new[] { "ActionA", "ApiA", "WhoAmI" });
        await Assert.That(mapped.Output.Target).IsEqualTo(DotNetTarget.Framework);
        await Assert.That(mapped.Output.Virtual).IsTrue();
        await Assert.That(mapped.Output.EditableReadOnly).IsTrue();
        await Assert.That(mapped.Output.Include.Context).IsFalse();
        await Assert.That(mapped.Output.Include.Options).IsFalse();
        await Assert.That(mapped.Output.Include.LogicalNames).IsFalse();
        await Assert.That(mapped.Output.Include.Relations).IsFalse();
        await Assert.That(mapped.Output.Include.NavigationProps).IsFalse();
        await Assert.That(mapped.Output.Include.EntityTypeCode).IsFalse();
        await Assert.That(mapped.Output.Include.AlternateKeys).IsFalse();
        await Assert.That(mapped.Output.Include.Metadata).IsFalse();
    }

    [Test]
    public async Task ToTypeScriptConfig_ShouldMapLegacyFieldsToNestedV2Shape()
    {
        var config = new CodeGenerationConfig
        {
            Entities = ["account"],
            Solutions = ["ContosoCore"],
            EntityMask = "contoso_*",
            GlobalOptionSets = ["contoso_status"],
            Actions = ["ActionA"],
            CustomAPIs = ["ApiA"],
            AdditionalSdkMessages = ["WhoAmI"],
            Forms = ["account.main.form"],
            OnlyFormsFromSolutions = true,
            XrmMockFormHelpers = true,
            UseBaseLanguage = false
        };

        var mapped = config.ToTypeScriptConfig();

        await Assert.That(mapped.Namespace).IsNull();
        await Assert.That(mapped.Entities.Names).IsEquivalentTo(new[] { "account" });
        await Assert.That(mapped.Entities.FromSolutions).IsEquivalentTo(new[] { "ContosoCore" });
        await Assert.That(mapped.Entities.Mask).IsEqualTo("contoso_*");
        await Assert.That(mapped.Language).IsEqualTo(0);
        await Assert.That(mapped.OptionSets).IsEquivalentTo(new[] { "contoso_status" });
        await Assert.That(mapped.Requests).IsEquivalentTo(new[] { "ActionA", "ApiA", "WhoAmI" });
        await Assert.That(mapped.Output.CustomApis).IsTrue();
        await Assert.That(mapped.Output.Forms).IsNotNull();
        await Assert.That(mapped.Output.Forms!.Filter).IsEquivalentTo(new[] { "account.main.form" });
        await Assert.That(mapped.Output.Forms.FromSolutions).IsTrue();
        await Assert.That(mapped.Output.Forms.TestHelpers).IsTrue();
    }

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

        var result = CodeGenerationConfigFactory.ResolveFromFile(WriteConfigFile("v2-dotnet.json", json));
        var config = AssertV2<DotNetCodeGenerationConfig>(result);

        await Assert.That(config.Namespace).IsEqualTo("Contoso.Dataverse.Model");
        await Assert.That(config.Entities.Names).IsEquivalentTo(new[] { "account" });
        await Assert.That(config.Entities.FromSolutions).IsEquivalentTo(new[] { "ContosoCore" });
        await Assert.That(config.Entities.Mask).IsEqualTo("contoso_*");
        await Assert.That(config.OptionSets).IsEquivalentTo(new[] { "contoso_status" });
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

        var result = CodeGenerationConfigFactory.ResolveFromFile(WriteConfigFile("v2-typescript.json", json));
        var config = AssertV2<TypeScriptCodeGenerationConfig>(result);

        await Assert.That(config.Namespace).IsNull();
        await Assert.That(config.Entities.Names).IsEquivalentTo(new[] { "account" });
        await Assert.That(config.Entities.FromSolutions).IsEquivalentTo(new[] { "ContosoCore" });
        await Assert.That(config.Entities.Mask).IsEqualTo("contoso_*");
        await Assert.That(config.OptionSets).IsEquivalentTo(new[] { "contoso_status" });
        await Assert.That(config.Output.Forms).IsNotNull();
        await Assert.That(config.Output.Forms!.Filter).IsEquivalentTo(new[] { "account.main.form" });
        await Assert.That(config.Output.Forms.FromSolutions).IsTrue();
        await Assert.That(config.Output.Forms.TestHelpers).IsTrue();
        await Assert.That(config.Output.CustomApis).IsFalse();
    }

    private static TConfig AssertV2<TConfig>(CodeGenerationConfigResult result)
        where TConfig : CodeGenerationConfigBase
    {
        if (result is CodeGenerationConfigResult.V2 { Config: TConfig config })
        {
            return config;
        }

        throw new InvalidOperationException($"Expected V2 config {nameof(result)}.");
    }

    private static string WriteConfigFile(string fileName, string json)
    {
        var directory = Path.Combine(AppContext.BaseDirectory, "ConfigMappingTestsArtifacts");
        Directory.CreateDirectory(directory);
        var path = Path.Combine(directory, fileName);
        File.WriteAllText(path, json);
        return path;
    }
}
