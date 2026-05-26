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
using Spectre.Console;
#pragma warning disable CS8602

namespace dgt.power.codegeneration.tests;

[NotInParallel("AnsiConsole")]
public class TypescriptWorkerTests : CodeGenerationTestsBase<TypescriptWorker>
{
    private readonly EntityMetadata _accountMetadata;

    public TypescriptWorkerTests()
    {
        _accountMetadata = GetEntityMetadataResource(Account.EntityLogicalName);
    }

    protected override WorkerTestContextBuilder<TypescriptWorker, CodeGenerationVerb> GetBuilder()
    {
        var organization = new Organization(Guid.NewGuid())
        {
            LanguageCode = 1031
        };
        return base.GetBuilder()
            .WithFakeMessageExecutor(new RetrieveOptionSetExecutor())
            .WithMetaData(_accountMetadata)
            .WithData(organization);
    }

    [Test]
    public async Task ShouldFailOnMissingConfiguration() =>
        await Assert.That(GetContext()
            .Execute(new CodeGenerationVerb
            {
                Config = "missing.json"
            })).IsFalse();

    [Test]
    public async Task ShouldCreateModelDirectoryStructureIfNotExistent()
    {
        var config = new CodeGenerationConfig
        {
            TypingPath = "../node_modules/@types/xrm/index.d.ts",
            TypescriptGeneratorVersion = TypescriptGeneratorVersion.Full
        };
        var args = new CodeGenerationVerb
        {
            Config = WriteConfigurationArtifact(config).FullName,
            TargetDirectory = ArtifactDirectory
        };
        await Assert.That(GetContext()
            .Execute(args)).IsTrue();

        await Assert.That(Directory.Exists(GetArtifactPath(args.Folder))).IsTrue();
        await Assert.That(Directory.Exists($"{GetArtifactPath(args.Folder)}/{Folders.Typescript}")).IsTrue();
    }

    [Test]
    public async Task ShouldDeleteAllExistingFilesInModelFolder()
    {
        var config = new CodeGenerationConfig
        {
            TypingPath = "../node_modules/@types/xrm/index.d.ts",
            TypescriptGeneratorVersion = TypescriptGeneratorVersion.Full
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

        await Assert.That(GetContext()
            .Execute(args)).IsTrue();

        await Assert.That(Directory.Exists(modelPath)).IsTrue();
        await Assert.That(Directory.Exists(typescriptPath)).IsTrue();
        await Assert.That(Directory.GetFileSystemEntries(typescriptPath)).HasCount().EqualTo(6); // There are 6 boiler plate files which will always be created
        await Assert.That(File.Exists(existingFilePath)).IsFalse();
    }

    [Test]
    public async Task ShouldGenerateBoilerPlateFiles()
    {
        var config = new CodeGenerationConfig
        {
            TypingPath = "../node_modules/@types/xrm/index.d.ts",
            TypescriptGeneratorVersion = TypescriptGeneratorVersion.Full
        };
        var args = new CodeGenerationVerb
        {
            Config = WriteConfigurationArtifact(config).FullName,
            TargetDirectory = ArtifactDirectory
        };
        var typescriptPath = GetArtifactPath($"{args.Folder}/{Folders.Typescript}");

        await Assert.That(GetContext()
            .Execute(args)).IsTrue();

        var servicePath = $"{typescriptPath}/{FileNames.Typescript.FileNamePart.Services}.ts";
        File.Exists(servicePath);
        await Assert.That(File.ReadAllText(servicePath)).Contains($@"/// <reference path=""{config.TypingPath}"" />");


        var odataPath = $"{typescriptPath}/{FileNames.Typescript.FileNamePart.Odata}.ts";
        File.Exists(odataPath);
        await Assert.That(File.ReadAllText(odataPath)).Contains($@"/// <reference path=""{config.TypingPath}"" />");

        var webapiPath = $"{typescriptPath}/{FileNames.Typescript.FileNamePart.Webapi}.ts";
        File.Exists(webapiPath);
        await Assert.That(File.ReadAllText(webapiPath)).Contains($@"/// <reference path=""{config.TypingPath}"" />");

        var modelPath = $"{typescriptPath}/{FileNames.Typescript.FileNamePart.Model}.ts";
        File.Exists(modelPath);
        await Assert.That(File.ReadAllText(modelPath)).Contains($@"/// <reference path=""{config.TypingPath}"" />");

        var utilsPath = $"{typescriptPath}/{FileNames.Typescript.FileNamePart.Utils}.ts";
        File.Exists(utilsPath);
        await Assert.That(File.ReadAllText(utilsPath)).Contains($@"/// <reference path=""{config.TypingPath}"" />");
    }

    [Test]
    public async Task ShouldGenerateEntitiesAndEntityReferences()
    {
        var config = new CodeGenerationConfig
        {
            TypingPath = "../node_modules/@types/xrm/index.d.ts",
            TypescriptGeneratorVersion = TypescriptGeneratorVersion.Full,
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

        await Assert.That(context
            .Execute(args)).IsTrue();

        var typescriptPath = GetArtifactPath($"{args.Folder}/{Folders.Typescript}");

        await Assert.That(File.Exists($"{typescriptPath}/{_accountMetadata.LogicalName}.{FileNames.Typescript.FileNamePart.Entity}.ts")).IsTrue();
        await Assert.That(File.Exists($"{typescriptPath}/{_accountMetadata.LogicalName}.{FileNames.Typescript.FileNamePart.EntityRef}.ts")).IsTrue();
    }

    [Test]
    public async Task ShouldWarnUserAboutSpecificFormWithTsFileExension()
    {
        var forms = GetForms();
        var mainFormFileName =
            $"{_accountMetadata.LogicalName}.{forms.mainForm.Name.ToLowerInvariant().Replace(' ', '_')}_main.form.ts";
        var config = new CodeGenerationConfig
        {
            TypingPath = "../node_modules/@types/xrm/index.d.ts",
            TypescriptGeneratorVersion = TypescriptGeneratorVersion.Full,
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

        await Assert.That(context
            .Execute(args)).IsTrue();

        var typescriptPath = GetArtifactPath($"{args.Folder}/{Folders.Typescript}");

        await Assert.That(File.Exists(
                $"{typescriptPath}/{mainFormFileName}")).IsTrue();
        var consoleOutput = AnsiConsole.ExportText();

        // Remove linebreaks from console output
        await Assert.That(consoleOutput.ReplaceLineEndings(" ")).Contains(Warnings.TsExtensionDeprecation);
    }

    [Test]
    public async Task ShouldGenerateSpecificForms()
    {
        var forms = GetForms();
        var mainFormFileName =
            $"{_accountMetadata.LogicalName}.{forms.mainForm.Name.ToLowerInvariant().Replace(' ', '_')}_main.form";
        var config = new CodeGenerationConfig
        {
            TypingPath = "../node_modules/@types/xrm/index.d.ts",
            TypescriptGeneratorVersion = TypescriptGeneratorVersion.Full,
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

        await Assert.That(context
            .Execute(args)).IsTrue();

        var typescriptPath = GetArtifactPath($"{args.Folder}/{Folders.Typescript}");

        await Assert.That(File.Exists(
                $"{typescriptPath}/{mainFormFileName}.ts")).IsTrue();
        var quickViewName = forms.quickView.Name.ToLowerInvariant().Replace(' ', '_');
        await Assert.That(File.Exists(
                $"{typescriptPath}/{_accountMetadata.LogicalName}.{quickViewName}_quickview.form.ts")).IsFalse();
        var quickCreateName = forms.quickCreate.Name.ToLowerInvariant().Replace(' ', '_');
        await Assert.That(File.Exists(
                $"{typescriptPath}/{_accountMetadata.LogicalName}.{quickCreateName}_quickcreate.form.ts")).IsFalse();
    }

    [Test]
    public async Task ShouldGenerateAllForms()
    {
        var forms = GetForms();
        var config = new CodeGenerationConfig
        {
            TypingPath = "../node_modules/@types/xrm/index.d.ts",
            TypescriptGeneratorVersion = TypescriptGeneratorVersion.Full,
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

        await Assert.That(context
            .Execute(args)).IsTrue();

        var typescriptPath = GetArtifactPath($"{args.Folder}/{Folders.Typescript}");

        var mainFormName = forms.mainForm.Name.ToLowerInvariant().Replace(' ', '_');
        await Assert.That(File.Exists(
                $"{typescriptPath}/{_accountMetadata.LogicalName}.{mainFormName}_main.form.ts")).IsTrue();
        var quickViewName = forms.quickView.Name.ToLowerInvariant().Replace(' ', '_');
        await Assert.That(File.Exists(
                $"{typescriptPath}/{_accountMetadata.LogicalName}.{quickViewName}_quickview.form.ts")).IsTrue();
        var quickCreateName = forms.quickCreate.Name.ToLowerInvariant().Replace(' ', '_');
        await Assert.That(File.Exists(
                $"{typescriptPath}/{_accountMetadata.LogicalName}.{quickCreateName}_quickcreate.form.ts")).IsTrue();
    }

    [Test]
    public async Task ShouldGenerateFormsFromSolution()
    {
        var forms = GetForms();
        var solution = new Solution
        {
            Id = Guid.NewGuid(),
            UniqueName = "Test"
        };
        var solutionFormComponent = new SolutionComponent
        {
            Id = Guid.NewGuid(),
            [SolutionComponent.LogicalNames.SolutionId] = solution.ToEntityReference(),
            [SolutionComponent.LogicalNames.ObjectId] = forms.mainForm.Id,
            [SolutionComponent.LogicalNames.ComponentType] = new OptionSetValue(SolutionComponent.Options.ComponentType.SystemForm)
        };
        var config = new CodeGenerationConfig
        {
            TypingPath = "../node_modules/@types/xrm/index.d.ts",
            TypescriptGeneratorVersion = TypescriptGeneratorVersion.Full,
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

        var entityMetadata = new EntityMetadata();
        entityMetadata.GetType().GetProperty(nameof(EntityMetadata.LogicalName))!.SetValue(entityMetadata, "entity");

        var context = GetBuilder()
            .WithData(forms)
            .WithData(solution)
            .WithData(solutionFormComponent)
            .WithMetaData(entityMetadata)
            .WithRelationship(new OneToManyRelationshipMetadata
            {
                SchemaName = "system_form_entity",
                ReferencingEntity = SystemForm.EntityLogicalName,
                ReferencingAttribute = SystemForm.LogicalNames.ObjectTypeCode,
                ReferencedEntity = "entity",
                ReferencedAttribute = "objecttypecode"
            })
            .Build();

        await Assert.That(context
            .Execute(args)).IsTrue();

        var typescriptPath = GetArtifactPath($"{args.Folder}/{Folders.Typescript}");

        var mainFormName = forms.mainForm.Name.ToLowerInvariant().Replace(' ', '_');
        await Assert.That(File.Exists(
                $"{typescriptPath}/{_accountMetadata.LogicalName}.{mainFormName}_main.form.ts")).IsTrue();
        var quickViewName = forms.quickView.Name.ToLowerInvariant().Replace(' ', '_');
        await Assert.That(File.Exists(
                $"{typescriptPath}/{_accountMetadata.LogicalName}.{quickViewName}_quickview.form.ts")).IsFalse();
        var quickCreateName = forms.quickCreate.Name.ToLowerInvariant().Replace(' ', '_');
        await Assert.That(File.Exists(
                $"{typescriptPath}/{_accountMetadata.LogicalName}.{quickCreateName}_quickcreate.form.ts")).IsFalse();
    }

    [Test]
    public async Task ShouldGenerateSdkMessages()
    {
        var config = new CodeGenerationConfig
        {
            TypingPath = "../node_modules/@types/xrm/index.d.ts",
            TypescriptGeneratorVersion = TypescriptGeneratorVersion.Full
        };
        var args = new CodeGenerationVerb
        {
            Config = WriteConfigurationArtifact(config).FullName,
            TargetDirectory = ArtifactDirectory
        };

        var context = GetBuilder()
            .Build();

        await Assert.That(context
            .Execute(args)).IsTrue();

        var typescriptPath = GetArtifactPath($"{args.Folder}/{Folders.Typescript}");

        await Assert.That(File.Exists($"{typescriptPath}/{FileNames.Typescript.FileNames.SdkMessageNames}.ts")).IsTrue();
    }

    [Test]
    public async Task ShouldGenerateAdditionalSdkMessages()
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
            TypescriptGeneratorVersion = TypescriptGeneratorVersion.Full,
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

        await Assert.That(context
            .Execute(args)).IsTrue();

        var typescriptPath = GetArtifactPath($"{args.Folder}/{Folders.Typescript}");

        var messagesPath = $"{typescriptPath}/{FileNames.Typescript.FileNames.SdkMessageNames}.ts";
        await Assert.That(File.Exists(messagesPath)).IsTrue();
        var messagesCode = File.ReadAllText(messagesPath);
        await Assert.That(messagesCode).Contains($"public static {Formatter.CamelCase(action.Name)}: string = \"{action.Name}\";");
        await Assert.That(messagesCode).Contains($"public static {Formatter.CamelCase(customApi.Name)}: string = \"{customApi.Name}\";");
        await Assert.That(messagesCode).Contains($"public static {Formatter.CamelCase(additionalMessage.Name)}: string = \"{additionalMessage.Name}\";");
    }


    [Test]
    public async Task ShouldSuppressSdkMessagesGeneration()
    {
        var config = new CodeGenerationConfig
        {
            TypingPath = "../node_modules/@types/xrm/index.d.ts",
            TypescriptGeneratorVersion = TypescriptGeneratorVersion.Full,
            SuppressSdkMessages = true
        };
        var args = new CodeGenerationVerb
        {
            Config = WriteConfigurationArtifact(config).FullName,
            TargetDirectory = ArtifactDirectory
        };

        var context = GetBuilder()
            .Build();

        await Assert.That(context
            .Execute(args)).IsTrue();

        var typescriptPath = GetArtifactPath($"{args.Folder}/{Folders.Typescript}");

        await Assert.That(File.Exists($"{typescriptPath}/{FileNames.Typescript.FileNames.SdkMessageNames}.ts")).IsFalse();
    }

    [Test]
    public async Task ShouldGenerateSpecifiedGlobalOptionSets()
    {
        var activityPointerMetadata = GetEntityMetadataResource("activitypointer");
        var globalOptionSet = activityPointerMetadata.Attributes.OfType<PicklistAttributeMetadata>()
            .Select(x => x.OptionSet)
            .First(x => x.IsGlobal == true);
        var config = new CodeGenerationConfig
        {
            TypingPath = "../node_modules/@types/xrm/index.d.ts",
            TypescriptGeneratorVersion = TypescriptGeneratorVersion.Full,
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

        await Assert.That(context
            .Execute(args)).IsTrue();

        var typescriptPath = GetArtifactPath($"{args.Folder}/{Folders.Typescript}");

        await Assert.That(File.Exists($"{typescriptPath}/{FileNames.Typescript.FileNames.OptionSetValues}.ts")).IsTrue();
    }

    [Test]
    public async Task ShouldGenerateSpecifiedBusinessProcessFlows()
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
            TypescriptGeneratorVersion = TypescriptGeneratorVersion.Full,
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

        await Assert.That(context
            .Execute(args)).IsTrue();

        var typescriptPath = GetArtifactPath($"{args.Folder}/{Folders.Typescript}");

        await Assert.That(File.Exists($"{typescriptPath}/{businessProcessFlow.UniqueName}.bpf.ts")).IsTrue();
    }

    private (SystemForm mainForm, SystemForm quickCreate, SystemForm quickView) GetForms()
    {
        var mainForm = new SystemForm
        {
            Id = Guid.NewGuid(),
            Name = "Account",
            FormXml = GetResourceAsString("account.main.form.xml"),
            ObjectTypeCode = Account.EntityLogicalName,
            Type = new OptionSetValue(SystemForm.Options.Type.Main),
            FormActivationState = new OptionSetValue(SystemForm.Options.FormActivationState.Active)
        };
        var quickCreateForm = new SystemForm
        {
            Id = Guid.NewGuid(),
            Name = "Account Quick Create",
            FormXml = GetResourceAsString("account.quick.create.form.xml"),
            ObjectTypeCode = Account.EntityLogicalName,
            Type = new OptionSetValue(SystemForm.Options.Type.QuickCreate),
            FormActivationState = new OptionSetValue(SystemForm.Options.FormActivationState.Active)
        };
        var quickViewForm = new SystemForm
        {
            Id = Guid.NewGuid(),
            Name = "App for Outlook Account Quick View",
            FormXml = GetResourceAsString("account.quick.view.form.xml"),
            ObjectTypeCode = Account.EntityLogicalName,
            Type = new OptionSetValue(SystemForm.Options.Type.QuickViewForm),
            FormActivationState = new OptionSetValue(SystemForm.Options.FormActivationState.Active)
        };
        return (mainForm, quickCreateForm, quickViewForm);
    }
}
