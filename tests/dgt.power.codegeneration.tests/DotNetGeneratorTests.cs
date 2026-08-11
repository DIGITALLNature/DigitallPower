// Copyright (c) DIGITALL Nature. All rights reserved
// DIGITALL Nature licenses this file to you under the Microsoft Public License.

using dgt.power.codegeneration.Base;
using dgt.power.codegeneration.Constants;
using dgt.power.codegeneration.Logic;
using dgt.power.codegeneration.tests.Base;
using dgt.power.dataverse;
using dgt.power.tests.FakeExecutor;
using Microsoft.Xrm.Sdk;
using Microsoft.Xrm.Sdk.Metadata;
using Attribute = dgt.power.dataverse.Attribute;

namespace dgt.power.codegeneration.tests;


[NotInParallel("AnsiConsole")]
public class DotNetGeneratorTests : CodeGenerationTestsBase
{
    protected override string ResourceDirectory => Path.Combine("Resources", "DotNetWorker");

    protected override CodeGenerationContextBuilder GetBuilder()
    {
        var organization = new Organization(Guid.NewGuid()) { LanguageCode = 1033 };
        return base.GetBuilder()
            .WithFakeMessageExecutor(new RetrieveOptionSetExecutor())
            .WithData(organization);
    }

    [Test]
    public async Task ShouldCreateModelDirectoryStructureIfNotExistent()
    {
        var config = new CodeGenerationConfig();
        var args = new CodeGenerationVerb { TargetDirectory = ArtifactDirectory };
        var context = GetContext();
        context.DotNetGenerator.Generate(args, config.ToDotNetConfig() ?? throw new InvalidOperationException());

        await Assert.That(Directory.Exists(GetArtifactPath(args.Folder))).IsTrue();
        await Assert.That(Directory.Exists($"{GetArtifactPath(args.Folder)}/{Folders.DotNet}")).IsTrue();
    }

    [Test]
    public async Task ShouldDeleteAllExistingFilesInModelFolder()
    {
        var config = new CodeGenerationConfig();
        var args = new CodeGenerationVerb { TargetDirectory = ArtifactDirectory };

        var modelPath = GetArtifactPath(args.Folder);
        var dotNetPath = GetArtifactPath($"{args.Folder}/{Folders.DotNet}");
        Directory.CreateDirectory(modelPath);
        Directory.CreateDirectory(dotNetPath);
        var existingFilePath = $"{dotNetPath}/Testentity.cs";
        await File.WriteAllTextAsync(existingFilePath, "public class TestEntity : Entity { }");

        GetContext().DotNetGenerator.Generate(args,
            config.ToDotNetConfig());

        await Assert.That(Directory.Exists(modelPath)).IsTrue();
        await Assert.That(Directory.Exists(dotNetPath)).IsTrue();
        await Assert.That(Directory.GetFileSystemEntries(dotNetPath)).Count().IsEqualTo(2); // SdkMessageNames.cs, Context.cs
        await Assert.That(File.Exists(existingFilePath)).IsFalse();
    }

    [Test]
    public async Task ShouldAlwaysGenerateSdkMessageNames()
    {
        var config = new CodeGenerationConfig();
        var args = new CodeGenerationVerb { TargetDirectory = ArtifactDirectory };

        var context = GetBuilder().Build();
        context.DotNetGenerator.Generate(args,
            config.ToDotNetConfig());

        var dotNetPath = GetArtifactPath($"{args.Folder}/{Folders.DotNet}");
        await Assert.That(File.Exists($"{dotNetPath}/{FileNames.DotNet.SdkMessageNames}.cs")).IsTrue();
    }

    [Test]
    public async Task ShouldGenerateDataContext()
    {
        var config = new CodeGenerationConfig { SuppressSdkMessages = true, SuppressActions = true };
        var args = new CodeGenerationVerb { TargetDirectory = ArtifactDirectory };

        var context = GetBuilder().Build();
        context.DotNetGenerator.Generate(args,
            config.ToDotNetConfig());

        var dotNetPath = GetArtifactPath($"{args.Folder}/{Folders.DotNet}");
        await Assert.That(File.Exists($"{dotNetPath}/{FileNames.DotNet.Context}.cs")).IsTrue();
    }

    [Test]
    public async Task ShouldGenerateAdditionalSdkMessages()
    {
        var customApi = new SdkMessage(Guid.NewGuid()) { Name = "Custom API Message" };
        var action = new SdkMessage(Guid.NewGuid()) { Name = "Action" };
        var additionalMessage = new SdkMessage(Guid.NewGuid()) { Name = "AdditionalMessage" };
        var config = new CodeGenerationConfig
        {
            Actions = [action.Name],
            CustomAPIs = [customApi.Name],
            AdditionalSdkMessages = [additionalMessage.Name]
        };
        var args = new CodeGenerationVerb { TargetDirectory = ArtifactDirectory };

        var context = GetBuilder()
            .WithData(customApi)
            .WithData(action)
            .WithData(additionalMessage)
            .Build();

        context.DotNetGenerator.Generate(args,
            config.ToDotNetConfig());

        var dotNetPath = GetArtifactPath($"{args.Folder}/{Folders.DotNet}");

        var messagesPath = $"{dotNetPath}/{FileNames.DotNet.SdkMessageNames}.cs";
        await Assert.That(File.Exists(messagesPath)).IsTrue();
        var messagesCode = await File.ReadAllTextAsync(messagesPath);
        await Assert.That(messagesCode).Contains($"public const string {Formatter.CamelCase(action.Name)} = \"{action.Name}\";");
        await Assert.That(messagesCode).Contains($"public const string {Formatter.CamelCase(customApi.Name)} = \"{customApi.Name}\";");
        await Assert.That(messagesCode).Contains($"public const string {Formatter.CamelCase(additionalMessage.Name)} = \"{additionalMessage.Name}\";");

        // A plain SDK message (neither a Custom API nor a classic Action) has no parameter metadata
        // available, so no request/response class is generated for it — only its name constant above.
        var actionsPath = $"{dotNetPath}/{FileNames.DotNet.Actions}.cs";
        var actionsCode = await File.ReadAllTextAsync(actionsPath);
        await Assert.That(actionsCode).DoesNotContain($"{Formatter.CamelCase(additionalMessage.Name)}Request");
    }

    [Test]
    public async Task ShouldNotGenerateRequestResponseClassesForPlainSdkMessages()
    {
        var associateMessage = new SdkMessage(Guid.NewGuid()) { Name = "Associate" };
        var mergeMessage = new SdkMessage(Guid.NewGuid()) { Name = "Merge" };
        var config = new CodeGenerationConfig
        {
            AdditionalSdkMessages = [associateMessage.Name, mergeMessage.Name]
        };
        var args = new CodeGenerationVerb { TargetDirectory = ArtifactDirectory };

        var context = GetBuilder()
            .WithData(associateMessage)
            .WithData(mergeMessage)
            .Build();

        context.DotNetGenerator.Generate(args,
            config.ToDotNetConfig());

        var dotNetPath = GetArtifactPath($"{args.Folder}/{Folders.DotNet}");

        // Plain SDK messages (whether out-of-the-box, like Associate/Merge, or otherwise) have no
        // parameter metadata available, so generating a request/response class for them would always be
        // an empty, useless shell — and for out-of-the-box messages it would additionally conflict with
        // the SDK's own strongly typed request/response classes.
        var actionsPath = $"{dotNetPath}/{FileNames.DotNet.Actions}.cs";
        var actionsCode = await File.ReadAllTextAsync(actionsPath);
        await Assert.That(actionsCode).DoesNotContain($"{Formatter.CamelCase(associateMessage.Name)}Request");
        await Assert.That(actionsCode).DoesNotContain($"{Formatter.CamelCase(mergeMessage.Name)}Request");

        // The message name constants must still be generated regardless of the above.
        var messagesPath = $"{dotNetPath}/{FileNames.DotNet.SdkMessageNames}.cs";
        var messagesCode = await File.ReadAllTextAsync(messagesPath);
        await Assert.That(messagesCode).Contains($"public const string {Formatter.CamelCase(associateMessage.Name)} = \"{associateMessage.Name}\";");
        await Assert.That(messagesCode).Contains($"public const string {Formatter.CamelCase(mergeMessage.Name)} = \"{mergeMessage.Name}\";");

        // Both messages are real SdkMessage records, so no "unknown message" warning should be raised.
        await Assert.That(TestConsole.Output).DoesNotContain("is not a known SDK message");
    }

    [Test]
    public async Task ShouldNotGenerateRequestResponseClassForUnknownSdkMessage()
    {
        // "DoesNotExist" is not a Custom API, classic Action, or a real SdkMessage record in this
        // environment (no SdkMessage entity is registered for it below) — it should be dropped entirely
        // instead of silently producing an empty request/response class pair, and the user should be
        // warned so they can catch a possible typo.
        var config = new CodeGenerationConfig { AdditionalSdkMessages = ["DoesNotExist"] };
        var args = new CodeGenerationVerb { TargetDirectory = ArtifactDirectory };

        var context = GetBuilder().Build();
        context.DotNetGenerator.Generate(args,
            config.ToDotNetConfig());

        var dotNetPath = GetArtifactPath($"{args.Folder}/{Folders.DotNet}");

        var actionsPath = $"{dotNetPath}/{FileNames.DotNet.Actions}.cs";
        var actionsCode = await File.ReadAllTextAsync(actionsPath);
        await Assert.That(actionsCode).DoesNotContain("DoesNotExistRequest");

        // Since the message doesn't exist, its constant must not be fabricated in SdkMessageNames.cs either.
        var messagesPath = $"{dotNetPath}/{FileNames.DotNet.SdkMessageNames}.cs";
        var messagesCode = await File.ReadAllTextAsync(messagesPath);
        await Assert.That(messagesCode).DoesNotContain("DoesNotExist");

        // The user should be warned that the unknown message was skipped, hinting at a possible typo.
        await Assert.That(TestConsole.Output).Contains("'DoesNotExist' is not a known SDK message");
    }

    [Test]
    public async Task ShouldGenerateActionsAndCustomApis()
    {
        var actionMessage = new SdkMessage(Guid.NewGuid()) { Name = "Action" };
        var actionProcess = new Workflow(Guid.NewGuid())
        {
            UniqueName = actionMessage.Name,
            Type = new OptionSetValue(Workflow.Options.Type.Definition),
            Category = new OptionSetValue(Workflow.Options.Category.Action),
            Xaml = GetResourceAsString("action.xaml"),
            [Workflow.LogicalNames.SdkMessageId] = actionMessage.ToEntityReference()
        };
        var customApiMessage = new SdkMessage(Guid.NewGuid()) { Name = "CustomApi" };
        var customApi = new CustomAPI(Guid.NewGuid()) { UniqueName = customApiMessage.Name, SdkMessageId = customApiMessage.ToEntityReference() };
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
        var config = new CodeGenerationConfig { Actions = [actionMessage.Name], CustomAPIs = [customApi.UniqueName] };
        var args = new CodeGenerationVerb { TargetDirectory = ArtifactDirectory };

        var context = GetBuilder()
            .WithData(actionMessage)
            .WithData(actionProcess)
            .WithData(customApiMessage)
            .WithData(customApi)
            .WithData(customApiRequestParameter)
            .WithData(customApiResponseProperty)
            .Build();

        context.DotNetGenerator.Generate(args,
            config.ToDotNetConfig());

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
        var config = new CodeGenerationConfig { SuppressActions = true };
        var args = new CodeGenerationVerb { TargetDirectory = ArtifactDirectory };

        var context = GetBuilder().Build();
        context.DotNetGenerator.Generate(args,
            config.ToDotNetConfig());

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
        var config = new CodeGenerationConfig { GlobalOptionSets = [globalOptionSet.Name] };
        var args = new CodeGenerationVerb { TargetDirectory = ArtifactDirectory };

        var context = GetBuilder()
            .WithMetaData(activityPointerMetadata)
            .Build();

        context.DotNetGenerator.Generate(args,
            config.ToDotNetConfig());

        var dotnetPath = GetArtifactPath(Path.Combine(args.Folder, Folders.DotNet));
        var optionSetPath = Path.Combine(dotnetPath, $"{FileNames.DotNet.OptionSetValues}.cs");
        await Assert.That(File.Exists(optionSetPath)).IsTrue();
    }

    [Test]
    public async Task ShouldGenerateSpecificEntity()
    {
        var accountMetadata = GetEntityMetadataResource(Account.EntityLogicalName);
        var config = new CodeGenerationConfig { Entities = [accountMetadata.LogicalName] };
        var args = new CodeGenerationVerb { TargetDirectory = ArtifactDirectory };

        var context = GetBuilder()
            .WithMetaData(accountMetadata)
            .Build();

        context.DotNetGenerator.Generate(args,
            config.ToDotNetConfig());

        var dotnetPath = GetArtifactPath(Path.Combine(args.Folder, Folders.DotNet));
        var accountPath = Path.Combine(dotnetPath, "Account.cs");
        await Assert.That(File.Exists(accountPath)).IsTrue();
    }

    [Test]
    public async Task ShouldUseDefaultOptionLabelsWhenLabelNotSet()
    {
        var attributeMetadata = GetEntityMetadataResource(Attribute.EntityLogicalName);

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

        var config = new CodeGenerationConfig
        {
            Entities = [attributeMetadata.LogicalName], UseBaseLanguage = true
        };
        var args = new CodeGenerationVerb { TargetDirectory = ArtifactDirectory };
        context.DotNetGenerator.Generate(args,
            config.ToDotNetConfig());

        var dotNetPath = GetArtifactPath(Path.Combine(args.Folder, Folders.DotNet));
        var attributePath = Path.Combine(dotNetPath, "Attribute.cs");
        await Assert.That(File.Exists(attributePath)).IsTrue();
        var attributeCode = await File.ReadAllTextAsync(attributePath);

        await Assert.That(attributeCode).Contains($"public const bool {Labels.DefaultFalse} = false;");
        await Assert.That(attributeCode).Contains($"public const bool {Labels.DefaultTrue} = true;");
    }
}
