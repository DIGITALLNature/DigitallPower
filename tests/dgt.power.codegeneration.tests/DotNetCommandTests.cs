using dgt.power.codegeneration.Base;
using dgt.power.codegeneration.Constants;
using dgt.power.codegeneration.Logic;
using dgt.power.codegeneration.tests.Base;
using dgt.power.dataverse;
using dgt.power.tests;
using FluentAssertions;
using Microsoft.Xrm.Sdk;
using Microsoft.Xrm.Sdk.Metadata;
using Spectre.Console;
using Xunit.Abstractions;
using Attribute = dgt.power.dataverse.Attribute;

namespace dgt.power.codegeneration.tests;

public class DotNetCommandTests : CodeGenerationTestsBase<DotNetCommand>
{

    public DotNetCommandTests(ITestOutputHelper testOutputHelper) : base(testOutputHelper)
    {
    }

    protected override CommandTestContextBuilder<DotNetCommand, CodeGenerationVerb> GetBuilder()
    {
        var organization = new Organization(Guid.NewGuid()) {LanguageCode = 1033};
        return base.GetBuilder()
            .WithData(organization);
    }

    [Fact]
    public void ShouldFailOnMissingConfiguration() =>
        GetContext()
            .Execute(new CodeGenerationVerb {Config = "missing.json"})
            .Should().BeFalse();

    [Fact]
    public void ShouldUseDefaultOptionLabelsWhenLabelNotSet()
    {
        var attributeMetadata = GetEntityMetadataResource(Attribute.EntityLogicalName);

        // This attribute has no True- or FalseOption
        var validForReadOptionMetadata = attributeMetadata
            .Attributes
            .Where(x => x.LogicalName == Attribute.LogicalNames.ValidForReadAPI)
            .OfType<BooleanAttributeMetadata>()
            .Select(x => x.OptionSet)
            .Single();
        validForReadOptionMetadata.TrueOption.Should().BeNull();
        validForReadOptionMetadata.FalseOption.Should().BeNull();

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
        context.Execute(args).Should().BeTrue();

        var dotNetPath = GetArtifactPath(Path.Combine(args.Folder, Folders.DotNet));

        var attributePath = Path.Combine(dotNetPath, "Attribute.cs");
        File.Exists(attributePath)
            .Should()
            .BeTrue();
        var attributeCode = File.ReadAllText(attributePath);

        // TODO: Use Analyzer or compiler to assert this
        attributeCode.Should().Contain($"public const bool {Labels.DefaultFalse} = false;");
        attributeCode.Should().Contain($"public const bool {Labels.DefaultTrue} = true;");
    }

    [Fact]
    public void ShouldCreateModelDirectoryStructureIfNotExistent()
    {
        var config = new CodeGenerationConfig();
        var args = new CodeGenerationVerb {Config = WriteConfigurationArtifact(config).FullName, TargetDirectory = ArtifactDirectory};
        GetContext()
            .Execute(args)
            .Should().BeTrue();

        Directory.Exists(GetArtifactPath(args.Folder)).Should().BeTrue();
        Directory.Exists($"{GetArtifactPath(args.Folder)}/{Folders.DotNet}").Should().BeTrue();
    }

    [Fact]
    public void ShouldDeleteAllExistingFilesInModelFolder()
    {
        var config = new CodeGenerationConfig();
        var args = new CodeGenerationVerb {Config = WriteConfigurationArtifact(config).FullName, TargetDirectory = ArtifactDirectory};

        var modelPath = GetArtifactPath(args.Folder);
        var dotNetPath = GetArtifactPath($"{args.Folder}/{Folders.DotNet}");
        Directory.CreateDirectory(modelPath);
        Directory.CreateDirectory(dotNetPath);
        var existingFilePath = $"{dotNetPath}/Testentity.cs";
        File.WriteAllText(existingFilePath, "public class TestEntity : Entity { }");

        GetContext()
            .Execute(args)
            .Should().BeTrue();

        Directory.Exists(modelPath).Should().BeTrue();
        Directory.Exists(dotNetPath).Should().BeTrue();
        Directory.GetFileSystemEntries(dotNetPath).Should()
            .HaveCount(3); // SdkMessageNames.cs, Actions.cs, Context.cs
        File.Exists(existingFilePath).Should().BeFalse();
    }

    [Fact]
    public void ShouldSuppressSdkMessagesGeneration()
    {
        var config = new CodeGenerationConfig {SuppressSdkMessages = true};
        var args = new CodeGenerationVerb {Config = WriteConfigurationArtifact(config).FullName, TargetDirectory = ArtifactDirectory};

        var context = GetBuilder()
            .Build();

        context
            .Execute(args)
            .Should().BeTrue();

        var dotNetPath = GetArtifactPath($"{args.Folder}/{Folders.DotNet}");

        File.Exists($"{dotNetPath}/{FileNames.DotNet.SdkMessageNames}.cs")
            .Should()
            .BeFalse();
    }

    [Fact]
    public void ShouldGenerateDataContext()
    {
        var config = new CodeGenerationConfig {SuppressSdkMessages = true, SuppressActions = true,};
        var args = new CodeGenerationVerb {Config = WriteConfigurationArtifact(config).FullName, TargetDirectory = ArtifactDirectory};

        var context = GetBuilder()
            .Build();

        context
            .Execute(args)
            .Should().BeTrue();

        var dotNetPath = GetArtifactPath($"{args.Folder}/{Folders.DotNet}");

        File.Exists($"{dotNetPath}/{FileNames.DotNet.Context}.cs")
            .Should()
            .BeTrue();
    }

    [Fact]
    public void ShouldGenerateAdditionalSdkMessages()
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

        context
            .Execute(args)
            .Should().BeTrue();

        var dotNetPath = GetArtifactPath($"{args.Folder}/{Folders.DotNet}");

        var messagesPath = $"{dotNetPath}/{FileNames.DotNet.SdkMessageNames}.cs";
        File.Exists(messagesPath).Should().BeTrue();
        var messagesCode = File.ReadAllText(messagesPath);
        messagesCode.Should().Contain($"public const string {Formatter.CamelCase(action.Name)} = \"{action.Name}\";");
        messagesCode.Should()
            .Contain($"public const string {Formatter.CamelCase(customApi.Name)} = \"{customApi.Name}\";");
        messagesCode.Should()
            .Contain(
                $"public const string {Formatter.CamelCase(additionalMessage.Name)} = \"{additionalMessage.Name}\";");
    }

    [Fact]
    public void ShouldGenerateActionsAndCustomApis()
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
            CustomAPIId = customApi.ToEntityReference()
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

        context
            .Execute(args)
            .Should().BeTrue();

        var dotNetPath = GetArtifactPath($"{args.Folder}/{Folders.DotNet}");

        var messagesPath = $"{dotNetPath}/{FileNames.DotNet.Actions}.cs";
        File.Exists(messagesPath).Should().BeTrue();
        var messagesCode = File.ReadAllText(messagesPath);
        messagesCode.Should()
            .Contain($"public class {Formatter.CamelCase(actionProcess.UniqueName)}Request : OrganizationRequest");
        messagesCode.Should()
            .Contain($"public class {Formatter.CamelCase(customApi.UniqueName)}Request : OrganizationRequest");
    }

    [Fact]
    public void ShouldSuppressGenerationOfActions()
    {
        var config = new CodeGenerationConfig {SuppressActions = true};
        var args = new CodeGenerationVerb {Config = WriteConfigurationArtifact(config).FullName, TargetDirectory = ArtifactDirectory};

        var context = GetBuilder()
            .Build();

        context
            .Execute(args)
            .Should().BeTrue();

        var dotNetPath = GetArtifactPath($"{args.Folder}/{Folders.DotNet}");

        var messagesPath = $"{dotNetPath}/{FileNames.DotNet.Actions}.cs";
        File.Exists(messagesPath).Should().BeFalse();
    }

    [Fact]
    public void ShouldGenerateSpecifiedGlobalOptionSets()
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

        context
            .Execute(args)
            .Should().BeTrue();

        var dotnetPath = GetArtifactPath(Path.Combine(args.Folder, Folders.DotNet));
        var optionSetPath = Path.Combine(dotnetPath, $"{FileNames.DotNet.OptionSetValues}.cs");
        File.Exists(optionSetPath)
            .Should()
            .BeTrue();
    }

    [Fact]
    public void ShouldGenerateSpecificEntity()
    {
        var accountMetadata = GetEntityMetadataResource(Account.EntityLogicalName);
        var config = new CodeGenerationConfig {Entities = new[] {accountMetadata.LogicalName}};
        var args = new CodeGenerationVerb {Config = WriteConfigurationArtifact(config).FullName, TargetDirectory = ArtifactDirectory};

        AnsiConsole.Record();

        var context = GetBuilder()
            .WithMetaData(accountMetadata)
            .Build();

        context
            .Execute(args)
            .Should().BeTrue();

        var typescriptPath = GetArtifactPath(Path.Combine(args.Folder, Folders.DotNet));

        var accountPath = Path.Combine(typescriptPath, "Account.cs");

        File.Exists(accountPath)
            .Should()
            .BeTrue();
    }
}
