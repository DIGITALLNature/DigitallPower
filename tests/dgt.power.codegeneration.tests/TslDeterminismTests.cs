// Copyright (c) DIGITALL Nature. All rights reserved
// DIGITALL Nature licenses this file to you under the Microsoft Public License.

using System.Security.Cryptography;
using dgt.power.codegeneration.Base;
using dgt.power.codegeneration.Constants;
using dgt.power.codegeneration.tests.Base;
using dgt.power.dataverse;
using dgt.power.tests.FakeExecutor;
using Microsoft.Xrm.Sdk;
using Microsoft.Xrm.Sdk.Metadata;

namespace dgt.power.codegeneration.tests;

[NotInParallel("Win_Shared_File_Issue")]
public class TslDeterminismTests : CodeGenerationTestsBase
{
    private readonly EntityMetadata _testTableMetadata;

    protected override string ResourceDirectory => Path.Combine("Resources", "TypescriptWorker");

    public TslDeterminismTests()
    {
        _testTableMetadata = GetEntityMetadataResource("dgt_test_table");
    }

    protected override CodeGenerationContextBuilder GetBuilder()
    {
        var organization = new Organization(Guid.Parse("aaaaaaaa-aaaa-aaaa-aaaa-aaaaaaaaaaaa")) { LanguageCode = 1031 };
        return base.GetBuilder()
            .WithFakeMessageExecutor(new RetrieveOptionSetExecutor())
            .WithMetaData(_testTableMetadata)
            .WithData(organization);
    }

    [Test]
    public async Task ShouldProduceIdenticalArtifactsAcrossParallelRuns()
    {
        const int runCount = 6;
        var tasks = Enumerable.Range(0, runCount)
            .Select(index => ExecuteScenarioAndCaptureHashes($"parallel-run-{index}"));

        var results = await Task.WhenAll(tasks);
        var baseline = results[0];

        for (var i = 1; i < results.Length; i++)
        {
            await Assert.That(results[i].Count).IsEqualTo(baseline.Count);
            foreach (var (path, hash) in baseline)
            {
                await Assert.That(results[i].ContainsKey(path)).IsTrue();
                await Assert.That(results[i][path]).IsEqualTo(hash);
            }
        }
    }

    [Test]
    public async Task ShouldProduceIdenticalArtifactsAcrossRepeatedSerialRuns()
    {
        var first = await ExecuteScenarioAndCaptureHashes("serial-run-1");
        var second = await ExecuteScenarioAndCaptureHashes("serial-run-2");
        var third = await ExecuteScenarioAndCaptureHashes("serial-run-3");

        await AssertSnapshotsEqual(first, second);
        await AssertSnapshotsEqual(first, third);
    }

    private async Task<Dictionary<string, string>> ExecuteScenarioAndCaptureHashes(string runName)
    {
        var args = new CodeGenerationVerb
        {
            Config = GetScenarioFilePath("GenerateEntities", "model.config.json"),
            TargetDirectory = Path.Combine(ArtifactDirectory, runName)
        };

        var context = GetBuilder()
            .WithData(CreateDeterministicMainForm())
            .Build();

        await Assert.That(context.ExecuteTypescriptFromConfigFile(args)).IsTrue();

        var typescriptPath = Path.GetFullPath(Path.Combine(args.TargetDirectory, args.Folder, Folders.Typescript));
        return await CaptureFileHashes(typescriptPath);
    }

    private static async Task<Dictionary<string, string>> CaptureFileHashes(string rootPath)
    {
        var files = Directory.GetFiles(rootPath, "*", SearchOption.AllDirectories)
            .OrderBy(path => path, StringComparer.Ordinal)
            .ToList();

        var hashes = new Dictionary<string, string>(StringComparer.Ordinal);
        foreach (var file in files)
        {
            var relativePath = Path.GetRelativePath(rootPath, file);
            var content = await File.ReadAllBytesAsync(file);
            var hash = Convert.ToHexString(SHA256.HashData(content));
            hashes[relativePath] = hash;
        }

        return hashes;
    }

    private static async Task AssertSnapshotsEqual(
        IReadOnlyDictionary<string, string> expected,
        IReadOnlyDictionary<string, string> actual)
    {
        await Assert.That(actual.Count).IsEqualTo(expected.Count);
        foreach (var (path, hash) in expected)
        {
            await Assert.That(actual.ContainsKey(path)).IsTrue();
            await Assert.That(actual[path]).IsEqualTo(hash);
        }
    }

    private static string GetScenarioFilePath(string scenarioName, string fileName) =>
        Path.Combine("Resources", "TypescriptWorkerTsl", "TestScenarios", scenarioName, fileName);

    private SystemForm CreateDeterministicMainForm() =>
        new()
        {
            Id = Guid.Parse("bbbbbbbb-bbbb-bbbb-bbbb-bbbbbbbbbbbb"),
            Name = "Test Table",
            FormXml = GetResourceAsString("dgt_test_table.main.form.xml"),
            ObjectTypeCode = "dgt_test_table",
            Type = new OptionSetValue(SystemForm.Options.Type.Main),
            FormActivationState = new OptionSetValue(SystemForm.Options.FormActivationState.Active)
        };
}
