// Copyright (c) DIGITALL Nature. All rights reserved
// DIGITALL Nature licenses this file to you under the Microsoft Public License.

using System.Diagnostics;
using dgt.power.codegeneration.Base;
using dgt.power.codegeneration.Constants;
using dgt.power.codegeneration.Logic;
using dgt.power.codegeneration.tests.Base;
using dgt.power.dataverse;
using dgt.power.tests;
using dgt.power.tests.FakeExecutor;
using Microsoft.Xrm.Sdk.Metadata;

namespace dgt.power.codegeneration.tests;

[NotInParallel("Win_Shared_File_Issue")]
public class TslTypeScriptCompileGateTests : CodeGenerationTestsBase<TypescriptWorker>
{
    private readonly EntityMetadata _testTableMetadata;

    public TslTypeScriptCompileGateTests()
    {
        _testTableMetadata = GetEntityMetadataResource("dgt_test_table");
    }

    protected override WorkerTestContextBuilder<TypescriptWorker, CodeGenerationVerb> GetBuilder()
    {
        var organization = new Organization(Guid.NewGuid()) { LanguageCode = 1031 };
        return base.GetBuilder()
            .WithFakeMessageExecutor(new RetrieveOptionSetExecutor())
            .WithMetaData(_testTableMetadata)
            .WithData(organization);
    }

    [Test]
    public async Task ShouldCompileGeneratedTslDeclarationsWithTypeScript()
    {
        var args = new CodeGenerationVerb
        {
            Config = GetScenarioFilePath("CompileGate", "model.config.json"),
            TargetDirectory = ArtifactDirectory
        };

        var context = GetBuilder().Build();
        await Assert.That(context.Execute(args)).IsTrue();

        var typescriptPath = Path.GetFullPath(GetArtifactPath($"{args.Folder}/{Folders.Typescript}"));
        var optionSetsPath = Path.Combine(typescriptPath, "optionsetvalues.d.ts");
        var sdkMessagesPath = Path.Combine(typescriptPath, "sdkmessagenames.d.ts");

        await Assert.That(File.Exists(optionSetsPath)).IsTrue();
        await Assert.That(File.Exists(sdkMessagesPath)).IsTrue();

        var repositoryRoot = GetRepositoryRoot();
        var tscScriptPath = Path.Combine(repositoryRoot, "node_modules", "typescript", "lib", "tsc.js");
        if (!File.Exists(tscScriptPath))
        {
            throw new FileNotFoundException(
                $"TypeScript compiler script not found at '{tscScriptPath}'. Run 'pnpm install' before executing tests.");
        }

        var tsConfigPath = Path.Combine(typescriptPath, "tsconfig.tsl-compile-gate.json");
        const string tsConfigContent = """
                                       {
                                           "compilerOptions": {
                                               "target": "ES2022",
                                               "module": "ESNext",
                                               "strict": true,
                                               "noEmit": true,
                                               "skipLibCheck": false
                                           },
                                           "files": [
                                               "optionsetvalues.d.ts",
                                               "sdkmessagenames.d.ts"
                                           ]
                                       }
                                       """;
        await File.WriteAllTextAsync(tsConfigPath, tsConfigContent);

        var result = await RunTypeScriptCompiler(tscScriptPath, tsConfigPath, typescriptPath, TimeSpan.FromSeconds(30));
        if (result.ExitCode != 0)
        {
            throw new InvalidOperationException(
                $"TypeScript compile gate failed with exit code {result.ExitCode}.{Environment.NewLine}{result.Output}");
        }
    }

    private static async Task<ProcessResult> RunTypeScriptCompiler(
        string tscScriptPath,
        string tsConfigPath,
        string workingDirectory,
        TimeSpan timeout)
    {
        var startInfo = new ProcessStartInfo("node")
        {
            RedirectStandardOutput = true,
            RedirectStandardError = true,
            UseShellExecute = false,
            WorkingDirectory = workingDirectory
        };

        startInfo.ArgumentList.Add(tscScriptPath);
        startInfo.ArgumentList.Add("--project");
        startInfo.ArgumentList.Add(tsConfigPath);
        startInfo.ArgumentList.Add("--pretty");
        startInfo.ArgumentList.Add("false");

        using var process = Process.Start(startInfo)
            ?? throw new InvalidOperationException("Failed to start TypeScript compiler process.");

        var stdoutTask = process.StandardOutput.ReadToEndAsync();
        var stderrTask = process.StandardError.ReadToEndAsync();

        try
        {
            await process.WaitForExitAsync().WaitAsync(timeout);
        }
        catch (TimeoutException)
        {
            process.Kill(entireProcessTree: true);
            throw new TimeoutException(
                $"TypeScript compile gate timed out after {timeout.TotalSeconds} seconds.");
        }

        var stdout = await stdoutTask;
        var stderr = await stderrTask;
        var output = string.Join(
            Environment.NewLine,
            new[] { stdout, stderr }.Where(text => !string.IsNullOrWhiteSpace(text)));

        return new ProcessResult(process.ExitCode, output);
    }

    private static string GetRepositoryRoot()
    {
        var current = new DirectoryInfo(Directory.GetCurrentDirectory());
        while (current != null)
        {
            if (File.Exists(Path.Combine(current.FullName, "DigitallPower.slnx")))
            {
                return current.FullName;
            }

            current = current.Parent;
        }

        throw new InvalidOperationException("Could not locate repository root (DigitallPower.slnx).");
    }

    private static string GetScenarioFilePath(string scenarioName, string fileName) =>
        Path.Combine("Resources", "TypescriptWorkerTsl", "TestScenarios", scenarioName, fileName);

    private readonly record struct ProcessResult(int ExitCode, string Output);
}
