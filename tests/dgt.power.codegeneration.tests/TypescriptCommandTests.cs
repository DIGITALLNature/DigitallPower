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
#pragma warning disable CS8602

namespace dgt.power.codegeneration.tests;

public class TypescriptCommandTests : CodeGenerationTestsBase<TypescriptCommand>
{
    private readonly EntityMetadata _accountMetadata;

    public TypescriptCommandTests(ITestOutputHelper testOutputHelper) : base(testOutputHelper)
    {
        _accountMetadata = GetEntityMetadataResource(Account.EntityLogicalName);
    }

    protected override CommandTestContextBuilder<TypescriptCommand, CodeGenerationVerb> GetBuilder()
    {
        var organization = new Organization(Guid.NewGuid())
        {
            LanguageCode = 1031
        };
        return base.GetBuilder()
            .WithMetaData(_accountMetadata)
            .WithData(organization);
    }

    [Fact]
    public void ShouldFailOnMissingConfiguration() =>
        GetContext()
            .Execute(new CodeGenerationVerb
            {
                Config = "missing.json"
            })
            .Should().BeFalse();

    [Fact]
    public void ShouldCreateModelDirectoryStructureIfNotExistent()
    {
        var config = new CodeGenerationConfig
        {
            TypingPath = "../node_modules/@types/xrm/index.d.ts"
        };
        var args = new CodeGenerationVerb
        {
            Config = WriteConfigurationArtifact(config).FullName,
            TargetDirectory = ArtifactDirectory
        };
        GetContext()
            .Execute(args)
            .Should().BeTrue();

        Directory.Exists(GetArtifactPath(args.Folder)).Should().BeTrue();
        Directory.Exists($"{GetArtifactPath(args.Folder)}/{Folders.Typescript}").Should().BeTrue();
    }

    [Fact]
    public void ShouldDeleteAllExistingFilesInModelFolder()
    {
        var config = new CodeGenerationConfig
        {
            TypingPath = "../node_modules/@types/xrm/index.d.ts"
        };
        var args = new CodeGenerationVerb
        {
            Config = WriteConfigurationArtifact(config).FullName,
            TargetDirectory = ArtifactDirectory
        };

        var modelPath = GetArtifactPath(args.Folder);
        var typescriptPath = GetArtifactPath($"{args.Folder}/{Folders.Typescript}");
        Directory.CreateDirectory(modelPath);
        Directory.CreateDirectory(typescriptPath);
        var existingFilePath = $"{typescriptPath}/testentity.form.ts";
        File.WriteAllText(existingFilePath, "import * from '.'");

        GetContext()
            .Execute(args)
            .Should().BeTrue();

        Directory.Exists(modelPath).Should().BeTrue();
        Directory.Exists(typescriptPath).Should().BeTrue();
        Directory.GetFileSystemEntries(typescriptPath).Should()
            .HaveCount(6); // There are 6 boiler plate files which will always be created
        File.Exists(existingFilePath).Should().BeFalse();
    }

    [Fact]
    public void ShouldGenerateBoilerPlateFiles()
    {
        var config = new CodeGenerationConfig
        {
            TypingPath = "../node_modules/@types/xrm/index.d.ts"
        };
        var args = new CodeGenerationVerb
        {
            Config = WriteConfigurationArtifact(config).FullName,
            TargetDirectory = ArtifactDirectory
        };
        var typescriptPath = GetArtifactPath($"{args.Folder}/{Folders.Typescript}");

        GetContext()
            .Execute(args)
            .Should().BeTrue();

        var servicePath = $"{typescriptPath}/{FileNames.Typescript.Services}.ts";
        File.Exists(servicePath);
        File.ReadAllText(servicePath).Should().Contain($@"/// <reference path=""{config.TypingPath}"" />");


        var odataPath = $"{typescriptPath}/{FileNames.Typescript.Odata}.ts";
        File.Exists(odataPath);
        File.ReadAllText(odataPath).Should().Contain($@"/// <reference path=""{config.TypingPath}"" />");

        var webapiPath = $"{typescriptPath}/{FileNames.Typescript.Webapi}.ts";
        File.Exists(webapiPath);
        File.ReadAllText(webapiPath).Should().Contain($@"/// <reference path=""{config.TypingPath}"" />");

        var modelPath = $"{typescriptPath}/{FileNames.Typescript.Model}.ts";
        File.Exists(modelPath);
        File.ReadAllText(modelPath).Should().Contain($@"/// <reference path=""{config.TypingPath}"" />");


        var utilsPath = $"{typescriptPath}/{FileNames.Typescript.Utils}.ts";
        File.Exists(utilsPath);
        File.ReadAllText(utilsPath).Should().Contain($@"/// <reference path=""{config.TypingPath}"" />");
    }

    [Fact]
    public void ShouldGenerateEntitiesAndEntityReferences()
    {
        var config = new CodeGenerationConfig
        {
            TypingPath = "../node_modules/@types/xrm/index.d.ts",
            Entities = new[]
            {
                _accountMetadata.LogicalName
            }
        };
        var args = new CodeGenerationVerb
        {
            Config = WriteConfigurationArtifact(config).FullName,
            TargetDirectory = ArtifactDirectory
        };

        var context = GetBuilder()
            .Build();

        context
            .Execute(args)
            .Should().BeTrue();

        var typescriptPath = GetArtifactPath($"{args.Folder}/{Folders.Typescript}");

        File.Exists($"{typescriptPath}/{_accountMetadata.LogicalName}.{FileNames.Typescript.Entity}.ts").Should()
            .BeTrue();
        File.Exists($"{typescriptPath}/{_accountMetadata.LogicalName}.{FileNames.Typescript.EntityRef}.ts").Should()
            .BeTrue();
    }

    [Fact]
    public void ShouldWarnUserAboutSpecificFormWithTsFileExension()
    {
        var forms = GetForms();
        var mainFormFileName =
            $"{_accountMetadata.LogicalName}.{forms.mainForm.Name.ToLowerInvariant().Replace(' ', '_')}_main.form.ts";
        var config = new CodeGenerationConfig
        {
            TypingPath = "../node_modules/@types/xrm/index.d.ts",
            Entities = new[]
            {
                _accountMetadata.LogicalName
            },
            Forms = new[]
            {
                mainFormFileName
            }
        };
        var args = new CodeGenerationVerb
        {
            Config = WriteConfigurationArtifact(config).FullName,
            TargetDirectory = ArtifactDirectory
        };
        AnsiConsole.Record();

        var context = GetBuilder()
            .WithData(forms.mainForm)
            .Build();

        context
            .Execute(args)
            .Should().BeTrue();

        var typescriptPath = GetArtifactPath($"{args.Folder}/{Folders.Typescript}");

        File.Exists(
                $"{typescriptPath}/{mainFormFileName}")
            .Should()
            .BeTrue();
        var consoleOutput = AnsiConsole.ExportText();

        // Remove linebreaks from console output
        consoleOutput.ReplaceLineEndings(" ")
            .Should().Contain(Warnings.TsExtensionDeprecation);
    }

    [Fact]
    public void ShouldGenerateSpecificForms()
    {
        var forms = GetForms();
        var mainFormFileName =
            $"{_accountMetadata.LogicalName}.{forms.mainForm.Name.ToLowerInvariant().Replace(' ', '_')}_main.form";
        var config = new CodeGenerationConfig
        {
            TypingPath = "../node_modules/@types/xrm/index.d.ts",
            Entities = new[]
            {
                _accountMetadata.LogicalName
            },
            Forms = new[]
            {
                mainFormFileName
            }
        };
        var args = new CodeGenerationVerb
        {
            Config = WriteConfigurationArtifact(config).FullName,
            TargetDirectory = ArtifactDirectory
        };

        var context = GetBuilder()
            .WithData(forms)
            .Build();

        context
            .Execute(args)
            .Should().BeTrue();

        var typescriptPath = GetArtifactPath($"{args.Folder}/{Folders.Typescript}");

        File.Exists(
                $"{typescriptPath}/{mainFormFileName}.ts")
            .Should()
            .BeTrue();
        var quickViewName = forms.quickView.Name.ToLowerInvariant().Replace(' ', '_');
        File.Exists(
                $"{typescriptPath}/{_accountMetadata.LogicalName}.{quickViewName}_quickview.form.ts")
            .Should()
            .BeFalse();
        var quickCreateName = forms.quickCreate.Name.ToLowerInvariant().Replace(' ', '_');
        File.Exists(
                $"{typescriptPath}/{_accountMetadata.LogicalName}.{quickCreateName}_quickcreate.form.ts")
            .Should()
            .BeFalse();
    }

    [Fact]
    public void ShouldGenerateAllForms()
    {
        var forms = GetForms();
        var config = new CodeGenerationConfig
        {
            TypingPath = "../node_modules/@types/xrm/index.d.ts",
            Entities = new[]
            {
                _accountMetadata.LogicalName
            }
        };
        var args = new CodeGenerationVerb
        {
            Config = WriteConfigurationArtifact(config).FullName,
            TargetDirectory = ArtifactDirectory
        };

        var context = GetBuilder()
            .WithData(forms)
            .Build();

        context
            .Execute(args)
            .Should().BeTrue();

        var typescriptPath = GetArtifactPath($"{args.Folder}/{Folders.Typescript}");

        var mainFormName = forms.mainForm.Name.ToLowerInvariant().Replace(' ', '_');
        File.Exists(
                $"{typescriptPath}/{_accountMetadata.LogicalName}.{mainFormName}_main.form.ts")
            .Should()
            .BeTrue();
        var quickViewName = forms.quickView.Name.ToLowerInvariant().Replace(' ', '_');
        File.Exists(
                $"{typescriptPath}/{_accountMetadata.LogicalName}.{quickViewName}_quickview.form.ts")
            .Should()
            .BeTrue();
        var quickCreateName = forms.quickCreate.Name.ToLowerInvariant().Replace(' ', '_');
        File.Exists(
                $"{typescriptPath}/{_accountMetadata.LogicalName}.{quickCreateName}_quickcreate.form.ts")
            .Should()
            .BeTrue();
    }

    [Fact]
    public void ShouldGenerateFormsFromSolution()
    {
        var forms = GetForms();
        var solution = new Solution(Guid.NewGuid())
        {
            UniqueName = "Test"
        };
        var formComponent = new SolutionComponent(Guid.NewGuid())
        {
            [SolutionComponent.LogicalNames.SolutionId] = solution.ToEntityReference(),
            [SolutionComponent.LogicalNames.ObjectId] = forms.mainForm.Id,
            [SolutionComponent.LogicalNames.ComponentType] =
                new OptionSetValue(SolutionComponent.Options.ComponentType.SystemForm)
        };
        var config = new CodeGenerationConfig
        {
            TypingPath = "../node_modules/@types/xrm/index.d.ts",
            Entities = new[]
            {
                _accountMetadata.LogicalName
            },
            OnlyFormsFromSolutions = true,
            Solutions = new[]
            {
                solution.UniqueName
            }
        };
        var args = new CodeGenerationVerb
        {
            Config = WriteConfigurationArtifact(config).FullName,
            TargetDirectory = ArtifactDirectory
        };

        var context = GetBuilder()
            .WithData(forms)
            .WithData(solution)
            .WithData(formComponent)
            .Build();

        context
            .Execute(args)
            .Should().BeTrue();

        var typescriptPath = GetArtifactPath($"{args.Folder}/{Folders.Typescript}");

        var mainFormName = forms.mainForm.Name.ToLowerInvariant().Replace(' ', '_');
        File.Exists(
                $"{typescriptPath}/{_accountMetadata.LogicalName}.{mainFormName}_main.form.ts")
            .Should()
            .BeTrue();
        var quickViewName = forms.quickView.Name.ToLowerInvariant().Replace(' ', '_');
        File.Exists(
                $"{typescriptPath}/{_accountMetadata.LogicalName}.{quickViewName}_quickview.form.ts")
            .Should()
            .BeFalse();
        var quickCreateName = forms.quickCreate.Name.ToLowerInvariant().Replace(' ', '_');
        File.Exists(
                $"{typescriptPath}/{_accountMetadata.LogicalName}.{quickCreateName}_quickcreate.form.ts")
            .Should()
            .BeFalse();
    }

    [Fact]
    public void ShouldGenerateSdkMessages()
    {
        var config = new CodeGenerationConfig
        {
            TypingPath = "../node_modules/@types/xrm/index.d.ts",
        };
        var args = new CodeGenerationVerb
        {
            Config = WriteConfigurationArtifact(config).FullName,
            TargetDirectory = ArtifactDirectory
        };

        var context = GetBuilder()
            .Build();

        context
            .Execute(args)
            .Should().BeTrue();

        var typescriptPath = GetArtifactPath($"{args.Folder}/{Folders.Typescript}");

        File.Exists($"{typescriptPath}/{FileNames.Typescript.SdkMessageNames}.ts").Should().BeTrue();
    }

    [Fact]
    public void ShouldGenerateAdditionalSdkMessages()
    {
        var customApi = new SdkMessage(Guid.NewGuid())
        {
            Name = "Custom API Message"
        };
        var action = new SdkMessage(Guid.NewGuid())
        {
            Name = "Action"
        };
        var additionalMessage = new SdkMessage(Guid.NewGuid())
        {
            Name = "AdditionalMessage"
        };
        var config = new CodeGenerationConfig
        {
            TypingPath = "../node_modules/@types/xrm/index.d.ts",
            Actions = new[]
            {
                action.Name
            },
            CustomAPIs = new[]
            {
                customApi.Name
            },
            AdditionalSdkMessages = new[]
            {
                additionalMessage.Name
            }
        };
        var args = new CodeGenerationVerb
        {
            Config = WriteConfigurationArtifact(config).FullName,
            TargetDirectory = ArtifactDirectory
        };

        var context = GetBuilder()
            .WithData(customApi)
            .WithData(action)
            .WithData(additionalMessage)
            .Build();

        context
            .Execute(args)
            .Should().BeTrue();

        var typescriptPath = GetArtifactPath($"{args.Folder}/{Folders.Typescript}");

        var messagesPath = $"{typescriptPath}/{FileNames.Typescript.SdkMessageNames}.ts";
        File.Exists(messagesPath).Should().BeTrue();
        var messagesCode = File.ReadAllText(messagesPath);
        messagesCode.Should().Contain($"public static {Formatter.CamelCase(action.Name)}: string = \"{action.Name}\";");
        messagesCode.Should()
            .Contain($"public static {Formatter.CamelCase(customApi.Name)}: string = \"{customApi.Name}\";");
        messagesCode.Should()
            .Contain(
                $"public static {Formatter.CamelCase(additionalMessage.Name)}: string = \"{additionalMessage.Name}\";");
    }


    [Fact]
    public void ShouldSuppressSdkMessagesGeneration()
    {
        var config = new CodeGenerationConfig
        {
            TypingPath = "../node_modules/@types/xrm/index.d.ts",
            SuppressSdkMessages = true
        };
        var args = new CodeGenerationVerb
        {
            Config = WriteConfigurationArtifact(config).FullName,
            TargetDirectory = ArtifactDirectory
        };

        var context = GetBuilder()
            .Build();

        context
            .Execute(args)
            .Should().BeTrue();

        var typescriptPath = GetArtifactPath($"{args.Folder}/{Folders.Typescript}");

        File.Exists($"{typescriptPath}/{FileNames.Typescript.SdkMessageNames}.ts")
            .Should()
            .BeFalse();
    }

    [Fact]
    public void ShouldGenerateSpecifiedGlobalOptionSets()
    {
        var activityPointerMetadata = GetEntityMetadataResource("activitypointer");
        var globalOptionSet = activityPointerMetadata.Attributes.OfType<PicklistAttributeMetadata>()
            .Select(x => x.OptionSet)
            .First(x => x.IsGlobal == true);
        var config = new CodeGenerationConfig
        {
            TypingPath = "../node_modules/@types/xrm/index.d.ts",
            GlobalOptionSets = new HashSet<string>
            {
                globalOptionSet.Name
            }
        };
        var args = new CodeGenerationVerb
        {
            Config = WriteConfigurationArtifact(config).FullName,
            TargetDirectory = ArtifactDirectory
        };

        var context = GetBuilder()
            .WithMetaData(activityPointerMetadata)
            .Build();

        context
            .Execute(args)
            .Should().BeTrue();

        var typescriptPath = GetArtifactPath($"{args.Folder}/{Folders.Typescript}");

        File.Exists($"{typescriptPath}/{FileNames.Typescript.OptionSetValues}.ts")
            .Should()
            .BeTrue();
    }

    [Fact]
    public void ShouldGenerateSpecifiedBusinessProcessFlows()
    {
        var businessProcessFlow = new Workflow(Guid.NewGuid())
        {
            Name = "Test BPF",
            UniqueName = "test_bpf",
            Category = new OptionSetValue(Workflow.Options.Category.BusinessProcessFlow)
        };
        var processStage = new ProcessStage(Guid.NewGuid())
        {
            StageName = "Test Stage",
            ProcessId = businessProcessFlow.ToEntityReference()
        };
        var config = new CodeGenerationConfig
        {
            TypingPath = "../node_modules/@types/xrm/index.d.ts",
            BusinessProcessFlows = new HashSet<string>
            {
                businessProcessFlow.UniqueName
            }
        };
        var args = new CodeGenerationVerb
        {
            Config = WriteConfigurationArtifact(config).FullName,
            TargetDirectory = ArtifactDirectory
        };

        var context = GetBuilder()
            .WithData(businessProcessFlow)
            .WithData(processStage)
            .Build();

        context
            .Execute(args)
            .Should().BeTrue();

        var typescriptPath = GetArtifactPath($"{args.Folder}/{Folders.Typescript}");

        File.Exists($"{typescriptPath}/{businessProcessFlow.UniqueName}.bpf.ts")
            .Should()
            .BeTrue();
    }

    private (SystemForm mainForm, SystemForm quickCreate, SystemForm quickView) GetForms()
    {
        var mainForm = new SystemForm(Guid.NewGuid())
        {
            Name = "Account",
            FormXml = GetResourceAsString("account.main.form.xml"),
            ObjectTypeCode = Account.EntityLogicalName,
            Type = new OptionSetValue(SystemForm.Options.Type.Main),
            FormActivationState = new OptionSetValue(SystemForm.Options.FormActivationState.Active)
        };
        var quickCreateForm = new SystemForm(Guid.NewGuid())
        {
            Name = "Account Quick Create",
            FormXml = GetResourceAsString("account.quick.create.form.xml"),
            ObjectTypeCode = Account.EntityLogicalName,
            Type = new OptionSetValue(SystemForm.Options.Type.QuickCreate),
            FormActivationState = new OptionSetValue(SystemForm.Options.FormActivationState.Active)
        };
        var quickViewForm = new SystemForm(Guid.NewGuid())
        {
            Name = "App for Outlook Account Quick View",
            FormXml = GetResourceAsString("account.quick.view.form.xml"),
            ObjectTypeCode = Account.EntityLogicalName,
            Type = new OptionSetValue(SystemForm.Options.Type.QuickViewForm),
            FormActivationState = new OptionSetValue(SystemForm.Options.FormActivationState.Active)
        };
        return (mainForm, quickCreateForm, quickViewForm);
    }
}
