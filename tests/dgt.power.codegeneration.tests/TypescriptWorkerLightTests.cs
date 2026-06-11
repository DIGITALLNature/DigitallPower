// Copyright (c) DIGITALL Nature. All rights reserved
// DIGITALL Nature licenses this file to you under the Microsoft Public License.

using dgt.power.codegeneration.Base;
using dgt.power.codegeneration.Constants;
using dgt.power.codegeneration.tests.Base;
using dgt.power.dataverse;
using dgt.power.tests.FakeExecutor;
using Microsoft.Xrm.Sdk;
using Microsoft.Xrm.Sdk.Metadata;

namespace dgt.power.codegeneration.tests;

[NotInParallel("Win_Shared_File_Issue")]
public class TypescriptWorkerLightTests : CodeGenerationTestsBase
{
    private readonly EntityMetadata _testTableMetadata;

    protected override string ResourceDirectory => Path.Combine("Resources", "TypescriptWorker");

    public TypescriptWorkerLightTests()
    {
        _testTableMetadata = GetEntityMetadataResource("dgt_test_table");
    }

    protected override CodeGenerationContextBuilder GetBuilder()
    {
        var organization = new Organization(Guid.NewGuid()) { LanguageCode = 1031 };
        return base.GetBuilder()
            .WithFakeMessageExecutor(new RetrieveOptionSetExecutor())
            .WithMetaData(_testTableMetadata)
            .WithData(organization);
    }

    [Test]
    public async Task ShouldGenerateExpectedArtifactsForTslEntityScenario()
    {
        var args = new CodeGenerationVerb
        {
            Config = GetScenarioFilePath("GenerateEntities", "model.config.json"),
            TargetDirectory = ArtifactDirectory
        };

        var context = GetBuilder()
            .WithData(GetTestTableMainForm())
            .Build();

        await Assert.That(context.ExecuteTypescriptFromConfigFile(args)).IsTrue();

        var typescriptPath = GetArtifactPath($"{args.Folder}/{Folders.Typescript}");
        var generatedFiles = new DirectoryInfo(typescriptPath)
            .GetFiles("*.d.ts", SearchOption.AllDirectories)
            .Select(file => Path.GetRelativePath(typescriptPath, file.FullName))
            .OrderBy(name => name, StringComparer.Ordinal)
            .ToList();

        await Assert.That(generatedFiles).Contains("xrm_types_ext.d.ts");
        await Assert.That(generatedFiles.Count).IsEqualTo(3);

        var entityFile = new DirectoryInfo(typescriptPath).GetFiles("*.entity.d.ts", SearchOption.AllDirectories).Single();
        var formFile = new DirectoryInfo(typescriptPath).GetFiles("*.form.d.ts", SearchOption.AllDirectories).Single();

        var entityCode = await File.ReadAllTextAsync(entityFile.FullName);
        var formCode = await File.ReadAllTextAsync(formFile.FullName);

        await Assert.That(entityCode).Contains("declare namespace XrmTable.DgtTestTable");
        await Assert.That(entityCode).Contains("export interface FormContext extends Xrm.FormContext");
        await Assert.That(entityCode).Contains("LogicalName = \"dgt_test_table\"");

        await Assert.That(formCode).Contains("declare namespace XrmForm.");
        await Assert.That(formCode).Contains("export interface FormContext extends Xrm.FormContext");
        await Assert.That(formCode).Contains("export interface Ui extends Xrm.Ui");
    }

    [Test]
    public async Task ShouldGenerateExpectedArtifactsForTslGlobalOptionSetsScenario()
    {
        var args = new CodeGenerationVerb
        {
            Config = GetScenarioFilePath("GenerateGlobalOptionSets", "model.config.json"),
            TargetDirectory = ArtifactDirectory
        };

        var context = GetBuilder().Build();
        await Assert.That(context.ExecuteTypescriptFromConfigFile(args)).IsTrue();

        var typescriptPath = GetArtifactPath($"{args.Folder}/{Folders.Typescript}");
        var generatedFiles = new DirectoryInfo(typescriptPath)
            .GetFiles("*.d.ts")
            .Select(file => file.Name)
            .OrderBy(name => name, StringComparer.Ordinal)
            .ToList();

        await Assert.That(generatedFiles.Count).IsEqualTo(2);
        await Assert.That(generatedFiles).Contains("xrm_types_ext.d.ts");
        await Assert.That(generatedFiles).Contains("optionsetvalues.d.ts");

        var expectedContent = await File.ReadAllTextAsync(GetScenarioFilePath("GenerateGlobalOptionSets", "optionsetvalues.d.ts"));
        var actualContent = await File.ReadAllTextAsync(Path.Combine(typescriptPath, "optionsetvalues.d.ts"));

        await Assert.That(NormalizeLineEndings(actualContent)).IsEqualTo(NormalizeLineEndings(expectedContent));
    }

    [Test]
    public async Task ShouldGenerateExpectedArtifactsForTslSdkMessagesScenario()
    {
        var args = new CodeGenerationVerb
        {
            Config = GetScenarioFilePath("GenerateSdkMessageNames", "model.config.json"),
            TargetDirectory = ArtifactDirectory
        };

        var context = GetBuilder().Build();
        await Assert.That(context.ExecuteTypescriptFromConfigFile(args)).IsTrue();

        var typescriptPath = GetArtifactPath($"{args.Folder}/{Folders.Typescript}");
        var generatedFiles = new DirectoryInfo(typescriptPath)
            .GetFiles("*.d.ts")
            .Select(file => file.Name)
            .OrderBy(name => name, StringComparer.Ordinal)
            .ToList();

        await Assert.That(generatedFiles.Count).IsEqualTo(2);
        await Assert.That(generatedFiles).Contains("xrm_types_ext.d.ts");
        await Assert.That(generatedFiles).Contains("sdkmessagenames.d.ts");

        var expectedContent = await File.ReadAllTextAsync(GetScenarioFilePath("GenerateSdkMessageNames", "sdkmessagenames.d.ts"));
        var actualContent = await File.ReadAllTextAsync(Path.Combine(typescriptPath, "sdkmessagenames.d.ts"));

        await Assert.That(NormalizeLineEndings(actualContent)).IsEqualTo(NormalizeLineEndings(expectedContent));
    }

    private static string GetScenarioFilePath(string scenarioName, string fileName) =>
        Path.Combine("Resources", "TypescriptWorkerTsl", "TestScenarios", scenarioName, fileName);

    private SystemForm GetTestTableMainForm() =>
        new()
        {
            Id = Guid.NewGuid(),
            Name = "Test Table",
            FormXml = GetResourceAsString("dgt_test_table.main.form.xml"),
            ObjectTypeCode = "dgt_test_table",
            Type = new OptionSetValue(SystemForm.Options.Type.Main),
            FormActivationState = new OptionSetValue(SystemForm.Options.FormActivationState.Active)
        };

    private static string NormalizeLineEndings(string value) => value.ReplaceLineEndings("\n").Trim();
}
