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
    private readonly EntityMetadata _testTableMetadata;

    public TypescriptCommandLightTests(ITestOutputHelper testOutputHelper) : base(testOutputHelper)
    {
        _testTableMetadata = GetEntityMetadataResource("dgt_test_table");
    }

    protected override CommandTestContextBuilder<TypescriptCommand, CodeGenerationVerb> GetBuilder()
    {
        var organization = new Organization(Guid.NewGuid())
        {
            LanguageCode = 1031
        };
        return base.GetBuilder()
            .WithMetaData(_testTableMetadata)
            .WithData(organization);
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
            .WithData(GetTestTableForm())
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
            actualFiles.Remove(actualFile);

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

        foreach (var actualFile in actualFiles)
        {
            Assert.Fail($"File {actualFile.Name} should not be generated");
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

    private SystemForm GetTestTableForm() =>
        new(Guid.NewGuid())
        {
            Name = "Test Table",
            FormXml = GetResourceAsString("dgt_test_table.main.form.xml"),
            ObjectTypeCode = "dgt_test_table",
            Type = new OptionSetValue(SystemForm.Options.Type.Main),
            FormActivationState = new OptionSetValue(SystemForm.Options.FormActivationState.Active)
        };

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
