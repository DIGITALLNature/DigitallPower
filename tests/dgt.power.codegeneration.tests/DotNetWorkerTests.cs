// Copyright (c) DIGITALL Nature. All rights reserved
// DIGITALL Nature licenses this file to you under the Microsoft Public License.

using dgt.power.codegeneration.Base;
using dgt.power.codegeneration.Constants;
using dgt.power.codegeneration.Logic;
using dgt.power.codegeneration.tests.Base;
using dgt.power.dataverse;
using dgt.power.tests;
using dgt.power.tests.FakeExecutor;
using Microsoft.Xrm.Sdk;
using Microsoft.Xrm.Sdk.Metadata;
using Attribute = dgt.power.dataverse.Attribute;

namespace dgt.power.codegeneration.tests;

[NotInParallel("AnsiConsole")]
public class DotNetWorkerTests : CodeGenerationTestsBase<DotNetWorker>
{

    protected override WorkerTestContextBuilder<DotNetWorker, CodeGenerationVerb> GetBuilder()
    {
        var organization = new Organization(Guid.NewGuid()) {LanguageCode = 1033};
        return base.GetBuilder()
            .WithFakeMessageExecutor(new RetrieveOptionSetExecutor())
            .WithData(organization);
    }

    [Test]
    public async Task ShouldFailOnMissingConfiguration() =>
        await Assert.That(GetContext()
            .Execute(new CodeGenerationVerb {Config = "missing.json"})).IsFalse();

    [Test]
    public async Task ShouldUseDefaultOptionLabelsWhenLabelNotSet()
    {
        var attributeMetadata = GetEntityMetadataResource(Attribute.EntityLogicalName);

        // This attribute has no True- or FalseOption
        var validForReadOptionMetadata = attributeMetadata
            .Attributes
            .Where(x => x.LogicalName == Attribute.LogicalNames.ValidForReadAPI)
            .OfType<BooleanAttributeMetadata>()
            .Select(x => x.OptionSet)
            .Single();
        await Assert.That(validForReadOptionMetadata.TrueOption).IsNull();
        await Assert.That(validForReadOptionMetadata.FalseOption).IsNull();

        var context = GetBuilder()
            .WithMetaData(attributeMetadata)
            .Build();

        CodeGenerationVerb args = new()
        {
            Config = WriteConfigurationArtifact(new CodeGenerationConfig
            {
                Entities = new[] {attributeMetadata.LogicalName}, UseBaseLanguage = true
            }).FullName,
            TargetDirectory = ArtifactDirectory
        };
        await Assert.That(context.Execute(args)).IsTrue();

        var dotNetPath = GetArtifactPath(Path.Combine(args.Folder, Folders.DotNet));

        var attributePath = Path.Combine(dotNetPath, "Attribute.cs");
        await Assert.That(File.Exists(attributePath)).IsTrue();
        var attributeCode = await File.ReadAllTextAsync(attributePath);

        // TODO: Use Analyzer or compiler to assert this
        await Assert.That(attributeCode).Contains($"public const bool {Labels.DefaultFalse} = false;");
        await Assert.That(attributeCode).Contains($"public const bool {Labels.DefaultTrue} = true;");
    }

    [Test]
    public async Task ShouldCreateModelDirectoryStructureIfNotExistent()
    {
        var config = new CodeGenerationConfig();
        var args = new CodeGenerationVerb {Config = WriteConfigurationArtifact(config).FullName, TargetDirectory = ArtifactDirectory};
        await Assert.That(GetContext()
            .Execute(args)).IsTrue();

        await Assert.That(Directory.Exists(GetArtifactPath(args.Folder))).IsTrue();
        await Assert.That(Directory.Exists($"{GetArtifactPath(args.Folder)}/{Folders.DotNet}")).IsTrue();
    }

    [Test]
    public async Task ShouldDeleteAllExistingFilesInModelFolder()
    {
        var config = new CodeGenerationConfig();
        var args = new CodeGenerationVerb {Config = WriteConfigurationArtifact(config).FullName, TargetDirectory = ArtifactDirectory};

        var modelPath = GetArtifactPath(args.Folder);
        var dotNetPath = GetArtifactPath($"{args.Folder}/{Folders.DotNet}");
        Directory.CreateDirectory(modelPath);
        Directory.CreateDirectory(dotNetPath);
        var existingFilePath = $"{dotNetPath}/Testentity.cs";
        await File.WriteAllTextAsync(existingFilePath, "public class TestEntity : Entity { }");

        await Assert.That(GetContext()
            .Execute(args)).IsTrue();

        await Assert.That(Directory.Exists(modelPath)).IsTrue();
        await Assert.That(Directory.Exists(dotNetPath)).IsTrue();
        await Assert.That(Directory.GetFileSystemEntries(dotNetPath)).Count().IsEqualTo(3); // SdkMessageNames.cs, Actions.cs, Context.cs
        await Assert.That(File.Exists(existingFilePath)).IsFalse();
    }

    [Test]
    public async Task ShouldSuppressSdkMessagesGeneration()
    {
        var config = new CodeGenerationConfig {SuppressSdkMessages = true};
        var args = new CodeGenerationVerb {Config = WriteConfigurationArtifact(config).FullName, TargetDirectory = ArtifactDirectory};

        var context = GetBuilder()
            .Build();

        await Assert.That(context
            .Execute(args)).IsTrue();

        var dotNetPath = GetArtifactPath($"{args.Folder}/{Folders.DotNet}");

        await Assert.That(File.Exists($"{dotNetPath}/{FileNames.DotNet.SdkMessageNames}.cs")).IsFalse();
    }

    [Test]
    public async Task ShouldGenerateDataContext()
    {
        var config = new CodeGenerationConfig {SuppressSdkMessages = true, SuppressActions = true};
        var args = new CodeGenerationVerb {Config = WriteConfigurationArtifact(config).FullName, TargetDirectory = ArtifactDirectory};

        var context = GetBuilder()
            .Build();

        await Assert.That(context
            .Execute(args)).IsTrue();

        var dotNetPath = GetArtifactPath($"{args.Folder}/{Folders.DotNet}");

        await Assert.That(File.Exists($"{dotNetPath}/{FileNames.DotNet.Context}.cs")).IsTrue();
    }

    [Test]
    public async Task ShouldGenerateAdditionalSdkMessages()
    {
        var customApi = new SdkMessage(Guid.NewGuid()) {Name = "Custom API Message"};
        var action = new SdkMessage(Guid.NewGuid()) {Name = "Action"};
        var additionalMessage = new SdkMessage(Guid.NewGuid()) {Name = "AdditionalMessage"};
        var config = new CodeGenerationConfig
        {
            Actions = new[] {action.Name}, CustomAPIs = new[] {customApi.Name}, AdditionalSdkMessages = new[] {additionalMessage.Name}
        };
        var args = new CodeGenerationVerb {Config = WriteConfigurationArtifact(config).FullName, TargetDirectory = ArtifactDirectory};

        var context = GetBuilder()
            .WithData(customApi)
            .WithData(action)
            .WithData(additionalMessage)
            .Build();

        await Assert.That(context
            .Execute(args)).IsTrue();

        var dotNetPath = GetArtifactPath($"{args.Folder}/{Folders.DotNet}");

        var messagesPath = $"{dotNetPath}/{FileNames.DotNet.SdkMessageNames}.cs";
        await Assert.That(File.Exists(messagesPath)).IsTrue();
        var messagesCode = await File.ReadAllTextAsync(messagesPath);
        await Assert.That(messagesCode).Contains($"public const string {Formatter.CamelCase(action.Name)} = \"{action.Name}\";");
        await Assert.That(messagesCode).Contains($"public const string {Formatter.CamelCase(customApi.Name)} = \"{customApi.Name}\";");
        await Assert.That(messagesCode).Contains($"public const string {Formatter.CamelCase(additionalMessage.Name)} = \"{additionalMessage.Name}\";");
    }

    [Test]
    public async Task ShouldGenerateActionsAndCustomApis()
    {
        var actionMessage = new SdkMessage(Guid.NewGuid()) {Name = "Action"};
        var actionProcess = new Workflow(Guid.NewGuid())
        {
            UniqueName = actionMessage.Name,
            Type = new OptionSetValue(Workflow.Options.Type.Definition),
            Category = new OptionSetValue(Workflow.Options.Category.Action),
            Xaml = GetResourceAsString("action.xaml"),
            [Workflow.LogicalNames.SdkMessageId] = actionMessage.ToEntityReference()
        };
        var customApiMessage = new SdkMessage(Guid.NewGuid()) {Name = "CustomApi"};
        var customApi = new CustomAPI(Guid.NewGuid()) {UniqueName = customApiMessage.Name, SdkMessageId = customApiMessage.ToEntityReference()};
        var customApiRequestParameter = new CustomAPIRequestParameter(Guid.NewGuid())
        {
            UniqueName = "StringParameter",
            Name = "String Parameter",
            Description = "String",
            Type = new OptionSetValue(CustomAPIRequestParameter.Options.Type.String),
            CustomAPIId = customApi.ToEntityReference(),
            IsOptional = false
        };
        var customApiResponseProperty = new CustomAPIResponseProperty(Guid.NewGuid())
        {
            UniqueName = "StringResult",
            Name = "String Result",
            Description = "string",
            Type = new OptionSetValue(CustomAPIResponseProperty.Options.Type.String),
            CustomAPIId = customApi.ToEntityReference()
        };
        var config = new CodeGenerationConfig {Actions = new[] {actionMessage.Name}, CustomAPIs = new[] {customApi.UniqueName}};
        var args = new CodeGenerationVerb {Config = WriteConfigurationArtifact(config).FullName, TargetDirectory = ArtifactDirectory};

        var context = GetBuilder()
            .WithData(actionMessage)
            .WithData(actionProcess)
            .WithData(customApiMessage)
            .WithData(customApi)
            .WithData(customApiRequestParameter)
            .WithData(customApiResponseProperty)
            .Build();

        await Assert.That(context
            .Execute(args)).IsTrue();

        var dotNetPath = GetArtifactPath($"{args.Folder}/{Folders.DotNet}");

        var messagesPath = $"{dotNetPath}/{FileNames.DotNet.Actions}.cs";
        await Assert.That(File.Exists(messagesPath)).IsTrue();
        var messagesCode = await File.ReadAllTextAsync(messagesPath);
        await Assert.That(messagesCode).Contains($"public class {Formatter.CamelCase(actionProcess.UniqueName)}Request : OrganizationRequest");
        await Assert.That(messagesCode).Contains($"public class {Formatter.CamelCase(customApi.UniqueName)}Request : OrganizationRequest");
    }

    [Test]
    public async Task ShouldSuppressGenerationOfActions()
    {
        var config = new CodeGenerationConfig {SuppressActions = true};
        var args = new CodeGenerationVerb {Config = WriteConfigurationArtifact(config).FullName, TargetDirectory = ArtifactDirectory};

        var context = GetBuilder()
            .Build();

        await Assert.That(context
            .Execute(args)).IsTrue();

        var dotNetPath = GetArtifactPath($"{args.Folder}/{Folders.DotNet}");

        var messagesPath = $"{dotNetPath}/{FileNames.DotNet.Actions}.cs";
        await Assert.That(File.Exists(messagesPath)).IsFalse();
    }

    [Test]
    public async Task ShouldGenerateSpecifiedGlobalOptionSets()
    {
        var activityPointerMetadata = GetEntityMetadataResource("activitypointer");
        var globalOptionSet = activityPointerMetadata.Attributes.OfType<PicklistAttributeMetadata>()
            .Select(x => x.OptionSet)
            .First(x => x.IsGlobal == true);
        var config = new CodeGenerationConfig {GlobalOptionSets = new HashSet<string> {globalOptionSet.Name}};
        var args = new CodeGenerationVerb {Config = WriteConfigurationArtifact(config).FullName, TargetDirectory = ArtifactDirectory};

        var context = GetBuilder()
            .WithMetaData(activityPointerMetadata)
            .Build();

        await Assert.That(context
            .Execute(args)).IsTrue();

        var dotnetPath = GetArtifactPath(Path.Combine(args.Folder, Folders.DotNet));
        var optionSetPath = Path.Combine(dotnetPath, $"{FileNames.DotNet.OptionSetValues}.cs");
        await Assert.That(File.Exists(optionSetPath)).IsTrue();
    }

    [Test]
    public async Task ShouldGenerateSpecificEntity()
    {
        var accountMetadata = GetEntityMetadataResource(Account.EntityLogicalName);
        var config = new CodeGenerationConfig {Entities = new[] {accountMetadata.LogicalName}};
        var args = new CodeGenerationVerb {Config = WriteConfigurationArtifact(config).FullName, TargetDirectory = ArtifactDirectory};

        var context = GetBuilder()
            .WithMetaData(accountMetadata)
            .Build();

        await Assert.That(context
            .Execute(args)).IsTrue();

        var typescriptPath = GetArtifactPath(Path.Combine(args.Folder, Folders.DotNet));

        var accountPath = Path.Combine(typescriptPath, "Account.cs");

        await Assert.That(File.Exists(accountPath)).IsTrue();
    }
}
