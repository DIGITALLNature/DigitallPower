// Copyright (c) DIGITALL Nature. All rights reserved
// DIGITALL Nature licenses this file to you under the Microsoft Public License.

using dgt.power.codegeneration.Base;
using dgt.power.codegeneration.Constants;
using dgt.power.codegeneration.Logic;
using dgt.power.codegeneration.tests.Base;
using dgt.power.dataverse;
using dgt.power.tests;
using DiffPlex;
using DiffPlex.DiffBuilder;
using DiffPlex.DiffBuilder.Model;
using FluentAssertions;
using Microsoft.Xrm.Sdk;
using Microsoft.Xrm.Sdk.Metadata;
using Spectre.Console;
using Xunit.Abstractions;
using ChangeType = DiffPlex.DiffBuilder.Model.ChangeType;
#pragma warning disable CS8602

namespace dgt.power.codegeneration.tests;

public class TypescriptCommandLightTests : CodeGenerationTestsBase<TypescriptCommand>
{
    private readonly EntityMetadata _accountMetadata;

    public TypescriptCommandLightTests(ITestOutputHelper testOutputHelper) : base(testOutputHelper)
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
            TypingPath = "../node_modules/@types/xrm/index.d.ts",
            TypescriptGeneratorVersion = TypescriptGeneratorVersion.Light,
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
            TypingPath = "../node_modules/@types/xrm/index.d.ts",
            TypescriptGeneratorVersion = TypescriptGeneratorVersion.Light,
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
            .HaveCount(1); // There is 1 boiler plate file which will always be created
        File.Exists(existingFilePath).Should().BeFalse();
    }

    [Fact]
    public void ShouldGenerateBoilerPlateFiles()
    {
        var config = new CodeGenerationConfig
        {
            TypingPath = "../node_modules/@types/xrm/index.d.ts",
            TypescriptGeneratorVersion = TypescriptGeneratorVersion.Light
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

        var sdkMessagesPath = $"{typescriptPath}/{FileNames.Typescript.SdkMessageNames}.ts";
        File.Exists(sdkMessagesPath);
    }

    [Fact]
    public void ShouldGenerateEntitiesAndEntityReferences()
    {
        var config = new CodeGenerationConfig
        {
            TypingPath = "../node_modules/@types/xrm/index.d.ts",
            TypescriptGeneratorVersion = TypescriptGeneratorVersion.Light,
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
            TypescriptGeneratorVersion = TypescriptGeneratorVersion.Light,
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
                $"{typescriptPath}/{_accountMetadata.LogicalName}.form.account_main.ts")
            .Should()
            .BeTrue();
        var quickViewName = forms.quickView.Name.ToLowerInvariant().Replace(' ', '_');
        File.Exists(
                $"{typescriptPath}/{_accountMetadata.LogicalName}.form.{quickViewName}_quickview.ts")
            .Should()
            .BeFalse();
        var quickCreateName = forms.quickCreate.Name.ToLowerInvariant().Replace(' ', '_');
        File.Exists(
                $"{typescriptPath}/{_accountMetadata.LogicalName}.form.{quickCreateName}_quickcreate.ts")
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
            TypescriptGeneratorVersion = TypescriptGeneratorVersion.Light,
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
                $"{typescriptPath}/{_accountMetadata.LogicalName}.form.{mainFormName}_main.ts")
            .Should()
            .BeTrue();
        var quickViewName = forms.quickView.Name.ToLowerInvariant().Replace(' ', '_');
        File.Exists(
                $"{typescriptPath}/{_accountMetadata.LogicalName}.form.{quickViewName}_quickview.ts")
            .Should()
            .BeTrue();
        var quickCreateName = forms.quickCreate.Name.ToLowerInvariant().Replace(' ', '_');
        File.Exists(
                $"{typescriptPath}/{_accountMetadata.LogicalName}.form.{quickCreateName}_quickcreate.ts")
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
            TypescriptGeneratorVersion = TypescriptGeneratorVersion.Light,
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
                $"{typescriptPath}/{_accountMetadata.LogicalName}.form.{mainFormName}_main.ts")
            .Should()
            .BeTrue();
        var quickViewName = forms.quickView.Name.ToLowerInvariant().Replace(' ', '_');
        File.Exists(
                $"{typescriptPath}/{_accountMetadata.LogicalName}.form.{quickViewName}_quickview.ts")
            .Should()
            .BeFalse();
        var quickCreateName = forms.quickCreate.Name.ToLowerInvariant().Replace(' ', '_');
        File.Exists(
                $"{typescriptPath}/{_accountMetadata.LogicalName}.form.{quickCreateName}_quickcreate.ts")
            .Should()
            .BeFalse();
    }

    [Fact]
    public void ShouldGenerateSdkMessages()
    {
        var config = new CodeGenerationConfig
        {
            TypingPath = "../node_modules/@types/xrm/index.d.ts",
            TypescriptGeneratorVersion = TypescriptGeneratorVersion.Light,
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
            TypescriptGeneratorVersion = TypescriptGeneratorVersion.Light,
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
            TypescriptGeneratorVersion = TypescriptGeneratorVersion.Light,
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
            TypescriptGeneratorVersion = TypescriptGeneratorVersion.Light,
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

    [Theory]
    [MemberData(nameof(GetTestScenariosDirectories))]
    public void ShouldGenerateExactFiles(string directoryName)
    {
        var args = new CodeGenerationVerb
        {
            Config = Path.Combine(directoryName, "model.config.json"),
            TargetDirectory = ArtifactDirectory,
        };

        var context = GetBuilder()
            .Build();

        context
            .Execute(args)
            .Should().BeTrue();

        var typescriptPath = GetArtifactPath($"{args.Folder}/{Folders.Typescript}");

        var expectedFiles = new DirectoryInfo(directoryName).GetFiles("*.ts").Select(f => new { f.Name, f.FullName }).ToList();
        var actualFiles = new DirectoryInfo(typescriptPath).GetFiles("*.ts").Select(f => new { f.Name, f.FullName }).ToList();

        actualFiles.Select(f => f.Name).Should().BeEquivalentTo(expectedFiles.Select(f => f.Name));

        foreach (var expectedFile in expectedFiles)
        {
            var actualFile = actualFiles.Single(f => f.Name == expectedFile.Name);

            var expectedFileContent = File.ReadAllText(expectedFile.FullName);
            var actualFileContent = File.ReadAllText(actualFile.FullName);

            if (expectedFileContent == actualFileContent)
            {
                continue;
            }

            var diffBuidler = new InlineDiffBuilder(new Differ());
            var diff = diffBuidler.BuildDiffModel(expectedFileContent, actualFileContent);

            if (diff.HasDifferences == false)
            {
                continue;
            }

            var diffOutput = GenerateDiffOutput(diff);

            Assert.Fail($"File {expectedFile.Name} is different:\n{diffOutput}");
        }
    }

    public static IEnumerable<object[]> GetTestScenariosDirectories()
    {
        var testScenariosDirectory = new DirectoryInfo(Path.Combine("Resources", nameof(TypescriptCommand), "TestScenarios"));

        return testScenariosDirectory.EnumerateDirectories()
            .Select(d => new object[] { d.FullName });
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

    private string GenerateDiffOutput(DiffPaneModel diff, int contextLines = 2)
    {
        List<string> output = new List<string>();
        int lastChangeLine = -1;
        int maxDigits = diff.Lines.Count.ToString().Length;

        for (int i = 0; i < diff.Lines.Count; i++)
        {
            var line = diff.Lines[i];
            if (line.Type != ChangeType.Unchanged)
            {
                // Add context lines before the change
                if (lastChangeLine != -1 && i - lastChangeLine > contextLines)
                {
                    output.Add("...");
                }

                for (int j = Math.Max(lastChangeLine + 1, i - contextLines); j < i; j++)
                {
                    if (diff.Lines[j].Type == ChangeType.Unchanged)
                    {
                        output.Add($"{(j + 1).ToString().PadLeft(maxDigits, '0')}: {diff.Lines[j].Text}");
                    }
                }
                // Add the changed line with line number
                switch (line.Type)
                {
                    case ChangeType.Inserted:
                        output.Add($"{(i + 1).ToString().PadLeft(maxDigits, '0')}: + {line.Text}");
                        break;
                    case ChangeType.Deleted:
                        output.Add($"{(i + 1).ToString().PadLeft(maxDigits, '0')}: - {line.Text}");
                        break;
                    case ChangeType.Modified:
                        output.Add($"{(i + 1).ToString().PadLeft(maxDigits, '0')}: {line.Text}");
                        break;
                }

                lastChangeLine = i;
            }
        }

        // Add context lines after the last change
        for (int i = lastChangeLine + 1; i < Math.Min(lastChangeLine + 1 + contextLines, diff.Lines.Count); i++)
        {
            if (diff.Lines[i].Type == ChangeType.Unchanged)
            {
                output.Add($"{(i + 1).ToString().PadLeft(maxDigits, '0')}: {diff.Lines[i].Text}");
            }
        }

        // Print the output
        return string.Join(Environment.NewLine, output);
    }
}
