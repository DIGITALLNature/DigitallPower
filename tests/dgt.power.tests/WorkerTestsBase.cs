// Copyright (c) DIGITALL Nature. All rights reserved
// DIGITALL Nature licenses this file to you under the Microsoft Public License.

using System.Runtime.CompilerServices;
using System.Text;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Text.RegularExpressions;
using dgt.power.common;
using Spectre.Console.Testing;

namespace dgt.power.tests;

/// <summary>
/// Base class for unit tests targeting <see cref="PowerWorker{TConfig}"/> components.
/// Mirrors <see cref="CommandTestsBase{TCommand,TCommandSettings}"/> for the worker pattern.
/// </summary>
public abstract class WorkerTestsBase<TWorker, TWorkerSettings> : IDisposable
    where TWorker : PowerWorker<TWorkerSettings>
    where TWorkerSettings : BaseProgramSettings
{
    protected TestConsole TestConsole { get; } = new TestConsole();

    public WorkerTestsBase()
    {
    }

    protected virtual string ResourceDirectory => Path.Combine("Resources", typeof(TWorker).Name);

    protected virtual string ArtifactDirectory { get; } = $"{Guid.NewGuid():N}";

    protected string GetResourcePath(string fileName) => Path.Combine(ResourceDirectory, fileName);
    protected string GetArtifactPath(string fileName) => Path.Combine(ArtifactDirectory, fileName);

    protected FileInfo WriteConfigurationArtifact<TArtifact>(TArtifact artifact)
    {
        var json = Regex.Unescape(JsonSerializer.Serialize(JsonSerializer.Serialize(artifact,
            new JsonSerializerOptions {Converters = {new JsonStringEnumConverter(JsonNamingPolicy.CamelCase)}}))).Trim('"');

        var fileName = $"{Guid.NewGuid():N}.json";
        var artifactFqn = Path.Combine(Directory.GetCurrentDirectory(), ArtifactDirectory);

        if (!Directory.Exists(artifactFqn))
        {
            Directory.CreateDirectory(artifactFqn);
        }

        var path = Path.Combine(artifactFqn, fileName);
        File.WriteAllText(path, json, Encoding.UTF8);
        return new FileInfo(path);
    }

    protected TResource GetConfigurationTestArtifact<TResource>(string fileName) where TResource : new()
    {
        var filePath = GetArtifactPath(fileName);
        return GetConfigurationFile<TResource>(fileName, filePath);
    }

    protected TResource GetConfigurationResource<TResource>(string fileName) where TResource : new()
    {
        var filePath = GetResourcePath(fileName);
        var isLoaded = GetContext().ConfigResolver
            .TryConfigFromFile<TResource>(filePath, out var resource);

        return isLoaded
            ? resource
            : throw new FileNotFoundException($"Couldn't find resource '{fileName}' under '{filePath}'.", filePath);
    }

    protected string GetTestFileName([CallerMemberName] string methodName = "")
    {
        return $"{typeof(TWorker).Name}-{methodName}.json";
    }

    protected virtual WorkerTestContextBuilder<TWorker, TWorkerSettings> GetBuilder() =>
        new WorkerTestContextBuilder<TWorker, TWorkerSettings>().WithAnsiConsole(TestConsole);

    protected virtual WorkerTestContext<TWorker, TWorkerSettings> GetContext() =>
        GetBuilder().Build();

    private TResource GetConfigurationFile<TResource>(string fileName, string filePath) where TResource : new()
    {
        var isLoaded = GetContext().ConfigResolver
            .TryConfigFromFile<TResource>(filePath, out var resource);

        return isLoaded
            ? resource
            : throw new FileNotFoundException($"Couldn't find file '{fileName}' under '{filePath}'.", filePath);
    }

    public virtual void Dispose()
    {
        var artifactDirectory = ArtifactDirectory.TrimEnd('/');
        if (Directory.Exists(artifactDirectory))
        {
            Directory.Delete(artifactDirectory, true);
        }

        GC.SuppressFinalize(this);
    }
}
