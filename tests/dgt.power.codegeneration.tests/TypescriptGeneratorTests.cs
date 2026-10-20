// Copyright (c) DIGITALL Nature. All rights reserved
// DIGITALL Nature licenses this file to you under the Microsoft Public License.

using dgt.power.codegeneration.Base;
using dgt.power.codegeneration.Constants;
using dgt.power.codegeneration.Generators;
using dgt.power.codegeneration.Services.Contracts;
using dgt.power.codegeneration.tests.Base;
using dgt.power.dataverse;
using dgt.power.tests.FakeExecutor;
using Microsoft.Xrm.Sdk;
using Microsoft.Xrm.Sdk.Metadata;

namespace dgt.power.codegeneration.tests;

[NotInParallel("Win_Shared_File_Issue")]
public class TypeScriptGeneratorTests : CodeGenerationTestsBase
{
    private readonly EntityMetadata _testTableMetadata;

    protected override string ResourceDirectory => Path.Combine("Resources", "TypescriptWorker");

    public TypeScriptGeneratorTests()
    {
        _testTableMetadata = GetEntityMetadataResource("dgt_test_table");
    }

    protected override CodeGenerationContextBuilder GetBuilder()
    {
        var organization = new Organization(Guid.NewGuid()) { LanguageCode = 1031 };
        return base.GetBuilder()
            .WithFakeMessageExecutor(new RetrieveOptionSetExecutor())
            .WithMetaData(_testTableMetadata)
            .WithData(organization)
            .WithData(BuiltInSdkMessages);
    }

    private static IEnumerable<Entity> BuiltInSdkMessages =>
        new[]
        {
            "Assign", "Create", "Delete", "GrantAccess", "ModifyAccess", "Retrieve", "RetrieveMultiple",
            "RetrievePrincipalAccess", "RetrieveSharedPrincipalsAndAccess", "RevokeAccess", "SetState", "Update"
        }
        .Select(name => new SdkMessage(Guid.NewGuid()) { Name = name });

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

        await Assert.That(await context.ExecuteTypescriptFromConfigFileAsync(args)).IsTrue();

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
        await Assert.That(entityCode).Contains("getAttribute(name: \"dgt_text_rich\")");
        await Assert.That(entityCode).Contains("dgt_text_rich?: string;");

        await Assert.That(formCode).Contains("declare namespace XrmForm.");
        await Assert.That(formCode).Contains("export interface FormContext extends Xrm.FormContext");
        await Assert.That(formCode).Contains("export interface Ui extends Xrm.Ui");
        await Assert.That(formCode).Contains("getAttribute(name: \"statuscode\"): Xrm.Attributes.OptionSetAttribute");
        await Assert.That(formCode).Contains("getControl(name: \"header_statuscode\"): Xrm.Controls.OptionSetControl");
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
        await Assert.That(await context.ExecuteTypescriptFromConfigFileAsync(args)).IsTrue();

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
        await Assert.That(await context.ExecuteTypescriptFromConfigFileAsync(args)).IsTrue();

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

    [Test]
    public async Task ShouldGenerateTypedCustomApiRequestAndResponseWrappers()
    {
        var sdkMessage = new SdkMessage(Guid.NewGuid()) { Name = "contoso_ParameterizedApi" };
        var customApi = new CustomAPI(Guid.NewGuid())
        {
            UniqueName = sdkMessage.Name,
            SdkMessageId = sdkMessage.ToEntityReference()
        };
        var requestParameter = new CustomAPIRequestParameter(Guid.NewGuid())
        {
            UniqueName = "InputValue",
            Name = "Input Value",
            Description = "Input value",
            Type = new OptionSetValue(CustomAPIRequestParameter.Options.Type.String),
            CustomAPIId = customApi.ToEntityReference(),
            IsOptional = false
        };
        var responseProperty = new CustomAPIResponseProperty(Guid.NewGuid())
        {
            UniqueName = "OutputValue",
            Name = "Output Value",
            Description = "Output value",
            Type = new OptionSetValue(CustomAPIResponseProperty.Options.Type.String),
            CustomAPIId = customApi.ToEntityReference()
        };
        var context = GetBuilder()
            .WithData(sdkMessage)
            .WithData(customApi)
            .WithData(requestParameter)
            .WithData(responseProperty)
            .Build();
        var args = new CodeGenerationVerb { TargetDirectory = ArtifactDirectory };
        var config = new TypeScriptCodeGenerationConfig { Requests = [customApi.UniqueName] };

        await Assert.That(await context.TypeScriptGenerator.GenerateAsync(args, config)).IsTrue();

        var customApiPath = Path.Combine(
            ArtifactDirectory,
            args.Folder,
            Folders.Typescript,
            Folders.TypescriptCustomApis,
            "contoso_parameterizedapi.customapi.d.ts");
        await Assert.That(File.Exists(customApiPath)).IsTrue();
        var generated = await File.ReadAllTextAsync(customApiPath);
        await Assert.That(generated).Contains("InputValue");
        await Assert.That(generated).Contains("OutputValue");

        var noCustomApiArgs = new CodeGenerationVerb
        {
            Config = args.Config,
            Folder = args.Folder,
            TargetDirectory = Path.Combine(ArtifactDirectory, "without-custom-apis")
        };
        var noCustomApiConfig = new TypeScriptCodeGenerationConfig
        {
            Requests = [customApi.UniqueName],
            Output = new TypeScriptOutput { CustomApis = false }
        };
        await Assert.That(await context.TypeScriptGenerator.GenerateAsync(noCustomApiArgs, noCustomApiConfig)).IsTrue();

        var noCustomApiOutputPath = Path.Combine(
            noCustomApiArgs.TargetDirectory,
            noCustomApiArgs.Folder,
            Folders.Typescript);
        await Assert.That(Directory.Exists(Path.Combine(noCustomApiOutputPath, Folders.TypescriptCustomApis))).IsFalse();
        await Assert.That(File.Exists(Path.Combine(noCustomApiOutputPath, FileNames.Typescript.FileNames.XrmWebApiTypingsFileName + ".d.ts"))).IsFalse();
    }

    [Test]
    public async Task ShouldPropagateCancellationFromSessionLanguageLookup()
    {
        using var cancellationTokenSource = new CancellationTokenSource();
        var metadataService = Mock.Of<IMetadataService>();
        metadataService.RetrieveConnectionUserLanguageAsync(cancellationTokenSource.Token)
            .Throws(new OperationCanceledException(cancellationTokenSource.Token));
        var generator = new TypeScriptGenerator(metadataService.Object, TestConsole);

        await Assert.That(() => generator.GenerateAsync(
            new CodeGenerationVerb { TargetDirectory = ArtifactDirectory },
            new TypeScriptCodeGenerationConfig { Language = 1031 },
            cancellationTokenSource.Token)).Throws<OperationCanceledException>();
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
