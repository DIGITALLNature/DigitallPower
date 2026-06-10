// Copyright (c) DIGITALL Nature. All rights reserved
// DIGITALL Nature licenses this file to you under the Microsoft Public License.

using dgt.power.codegeneration.Base;
using dgt.power.codegeneration.Generators.Worker;
using dgt.power.codegeneration.Services.Contracts;
using Spectre.Console.Testing;

namespace dgt.power.codegeneration.tests;

public class TypescriptGeneratorUnsupportedOperationsTests
{
    [Test]
    public async Task LightGenerator_GenerateBoilerPlateFull_ThrowsNotSupportedException()
    {
        using var testConsole = new TestConsole();
        var worker = new TypescriptGeneratorWorkerLight(Mock.Of<IMetadataService>().Object, testConsole);
        await AssertNotSupportedAsync(
            () => worker.GenerateBoilerPlateFull(new CodeGenerationVerb(), new CodeGenerationConfig()),
            "only available in full TypeScript generator mode");
    }

    [Test]
    public async Task LightGenerator_GenerateEntityRefsFull_ThrowsNotSupportedException()
    {
        using var testConsole = new TestConsole();
        var worker = new TypescriptGeneratorWorkerLight(Mock.Of<IMetadataService>().Object, testConsole);
        await AssertNotSupportedAsync(
            () => worker.GenerateEntityRefsFull(new CodeGenerationVerb(), new CodeGenerationConfig()),
            "only available in full TypeScript generator mode");
    }

    [Test]
    public async Task LightGenerator_GenerateBusinessProcessFlowsFull_ThrowsNotSupportedException()
    {
        using var testConsole = new TestConsole();
        var worker = new TypescriptGeneratorWorkerLight(Mock.Of<IMetadataService>().Object, testConsole);
        await AssertNotSupportedAsync(
            () => worker.GenerateBusinessProcessFlowsFull(new CodeGenerationVerb(), new CodeGenerationConfig()),
            "only available in full TypeScript generator mode");
    }

    [Test]
    public async Task FullGenerator_GenerateCustomApis_ThrowsNotSupportedException()
    {
        using var testConsole = new TestConsole();
        var worker = new TypescriptGeneratorWorkerFull(Mock.Of<IMetadataService>().Object, testConsole);
        await AssertNotSupportedAsync(
            () => worker.GenerateCustomApis(new CodeGenerationVerb(), new CodeGenerationConfig()),
            "Custom API generation is not supported");
    }

    private static async Task AssertNotSupportedAsync(Action action, string expectedMessageFragment)
    {
        NotSupportedException? exception = null;
        try
        {
            action();
        }
        catch (NotSupportedException ex)
        {
            exception = ex;
        }

        await Assert.That(exception).IsNotNull();
        await Assert.That(exception!.Message).Contains(expectedMessageFragment);
    }
}
