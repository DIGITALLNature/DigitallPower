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
public class TslJestFixtureGenerator : CodeGenerationTestsBase
{
    private readonly EntityMetadata _testTableMetadata;

    protected override string ResourceDirectory => Path.Combine("Resources", "TypescriptWorker");

    public TslJestFixtureGenerator()
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
    public async Task ShouldGenerateJestFixtureForTslMockFormHelpers()
    {
        var args = new CodeGenerationVerb
        {
            Config = GetScenarioFilePath("GenerateEntitiesForJest", "model.config.json"),
            TargetDirectory = GetFixtureOutputDirectory(),
            Folder = ""
        };

        var context = GetBuilder()
            .WithData(GetTestTableMainForm())
            .Build();

        await Assert.That(context.ExecuteTypescriptFromConfigFile(args)).IsTrue();

        var typescriptPath = Path.Combine(args.TargetDirectory, Folders.Typescript);
        var generatedFiles = new DirectoryInfo(typescriptPath)
            .GetFiles("*.ts", SearchOption.AllDirectories)
            .Select(file => Path.GetRelativePath(typescriptPath, file.FullName))
            .OrderBy(name => name, StringComparer.Ordinal)
            .ToList();

        await Assert.That(generatedFiles).Contains("xrm_mock_form_test_context_builder.ts");
        await Assert.That(generatedFiles).Contains("xrm_mock_form_test_context_types.ts");
        await Assert.That(generatedFiles).Contains("xrm_mock_form_odata_filter.ts");

        var mockFormFiles = generatedFiles.Where(f => f.EndsWith(".mock.form.ts", StringComparison.Ordinal)).ToList();
        await Assert.That(mockFormFiles).Count().IsGreaterThan(0);
    }

    private static string GetFixtureOutputDirectory()
    {
        var assemblyLocation = typeof(TslJestFixtureGenerator).Assembly.Location;
        var assemblyDirectory = Path.GetDirectoryName(assemblyLocation)
            ?? throw new InvalidOperationException("Could not determine assembly directory.");

        // Navigate from bin/Debug/net10.0 up to the test project root, then into the Jest fixture.
        var projectRoot = new DirectoryInfo(assemblyDirectory);
        while (projectRoot != null && !projectRoot.GetFiles("*.csproj").Any())
        {
            projectRoot = projectRoot.Parent;
        }

        if (projectRoot == null)
        {
            throw new InvalidOperationException("Could not locate the test project root.");
        }

        return Path.Combine(projectRoot.FullName, "Fixtures", "TslJest", "generated");
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
}
