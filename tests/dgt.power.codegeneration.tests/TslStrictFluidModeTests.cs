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

[NotInParallel("Process_Environment")]
public class TslStrictFluidModeTests : CodeGenerationTestsBase
{
    private readonly EntityMetadata _testTableMetadata;

    protected override string ResourceDirectory => Path.Combine("Resources", "TypescriptWorker");

    public TslStrictFluidModeTests()
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
    public async Task ShouldGenerateArtifactsWhenStrictFluidValidationModeIsEnabled()
    {
        using var _ = new EnvironmentVariableScope("DGT_POWER_TSL_STRICT_MODE", "true");

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
        await Assert.That(File.Exists(Path.Combine(typescriptPath, "xrm_types_ext.d.ts"))).IsTrue();
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

    private sealed class EnvironmentVariableScope : IDisposable
    {
        private readonly string _name;
        private readonly string? _originalValue;

        public EnvironmentVariableScope(string name, string value)
        {
            _name = name;
            _originalValue = Environment.GetEnvironmentVariable(name);
            Environment.SetEnvironmentVariable(name, value);
        }

        public void Dispose()
        {
            Environment.SetEnvironmentVariable(_name, _originalValue);
        }
    }
}
