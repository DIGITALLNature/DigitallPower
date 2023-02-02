using System.Runtime.CompilerServices;
using System.Text;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Text.RegularExpressions;
using Spectre.Console;
using Spectre.Console.Cli;
using Spectre.Console.Testing;
using Xunit.Abstractions;

namespace dgt.power.tests;

public abstract class CommandTestsBase<TCommand, TCommandSettings> : IDisposable
    where TCommand : class, ICommand<TCommandSettings>
    where TCommandSettings : CommandSettings
{
    private readonly ITestOutputHelper _testOutputHelper;
    protected IAnsiConsole TestConsole { get; } = new TestConsole();

    public CommandTestsBase(ITestOutputHelper testOutputHelper)
    {
        _testOutputHelper = testOutputHelper;
        AnsiConsole.Console = TestConsole;
    }

    protected virtual string ResourceDirectory => Path.Combine("Resources", typeof(TCommand).Name);

    protected virtual string ArtifactDirectory { get; } = $"{Guid.NewGuid():N}";

    protected string GetResourcePath(string fileName) => Path.Combine(ResourceDirectory, fileName);
    protected string GetArtifactPath(string fileName) => Path.Combine(ArtifactDirectory, fileName);

    /// <summary>
    /// Creates an file from an artifact. Uses System.Text.Json to serialize the artifact.
    /// </summary>
    /// <param name="artifact">The artifact to write to a file.</param>
    /// <param name="fileName">The name of the artifact file.</param>
    /// <typeparam name="TArtifact">The type of the artifact.</typeparam>
    /// <returns>The file info for the the artifact.</returns>
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


    /// <summary>
    /// Use this method to deserialize configuration artifacts, that were created during test runs.
    /// </summary>
    /// <param name="fileName">The file name of the configuration.</param>
    /// <typeparam name="TResource">The type to deserialize the configuration to.</typeparam>
    /// <returns>The deserialized configuration.</returns>
    /// <exception cref="FileNotFoundException">If the file is not found with the given fileName.</exception>
    protected TResource GetConfigurationTestArtifact<TResource>(string fileName) where TResource : new()
    {
        var filePath = GetArtifactPath(fileName);
        return GetConfigurationFile<TResource>(fileName, filePath);
    }

    /// <summary>
    /// Use this method to deserialize configuration resources, that are stored under the resource directory for this command.
    /// </summary>
    /// <param name="fileName">The file name of the resource.</param>
    /// <typeparam name="TResource">The type to deserialize the resource to.</typeparam>
    /// <returns>The deserialized resource.</returns>
    /// <exception cref="FileNotFoundException">If the file is not found with the given fileName.</exception>
    protected TResource GetConfigurationResource<TResource>(string fileName) where TResource : new()
    {
        var filePath = GetResourcePath(fileName);
        var isLoaded = GetContext().ConfigResolver
            .ConfigFromFile<TResource>(filePath, out var resource);

        return isLoaded
            ? resource
            : throw new FileNotFoundException($"Couldn't find resource '{fileName}' under '{filePath}'.", filePath);
    }


    protected string GetTestFileName([CallerMemberName] string methodName = "")
    {
        return $"{typeof(TCommand).Name}-{methodName}.json";
    }

    protected virtual CommandTestContextBuilder<TCommand, TCommandSettings> GetBuilder() =>
        new(_testOutputHelper);

    protected virtual CommandTestContext<TCommand, TCommandSettings> GetContext() =>
        GetBuilder()
            .Build();

    private TResource GetConfigurationFile<TResource>(string fileName, string filePath) where TResource : new()
    {
        var isLoaded = GetContext().ConfigResolver
            .ConfigFromFile<TResource>(filePath, out var resource);

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
    }
}
