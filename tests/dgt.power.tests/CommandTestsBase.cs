// SPDX-FileCopyrightText: DIGITALL Nature
// SPDX-License-Identifier: MS-RL

using System.Text;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Text.RegularExpressions;
using Spectre.Console.Cli;
using Spectre.Console.Testing;

namespace dgt.power.tests;

public abstract class CommandTestsBase<TCommand, TCommandSettings> : IDisposable
    where TCommand : class, ICommand<TCommandSettings>
    where TCommandSettings : CommandSettings
{
    protected TestConsole TestConsole { get; } = new();

    private readonly JsonSerializerOptions _jsonSerializerOptions = new() {Converters = {new JsonStringEnumConverter(JsonNamingPolicy.CamelCase)}};


    private string ArtifactDirectory { get; } = $"{Guid.NewGuid():N}";

    /// <summary>
    /// Creates an file from an artifact. Uses System.Text.Json to serialize the artifact.
    /// </summary>
    /// <param name="artifact">The artifact to write to a file.</param>
    /// <typeparam name="TArtifact">The type of the artifact.</typeparam>
    /// <returns>The file info for the the artifact.</returns>
    protected FileInfo WriteConfigurationArtifact<TArtifact>(TArtifact artifact)
    {
        var json = Regex.Unescape(JsonSerializer.Serialize(JsonSerializer.Serialize(artifact,
            _jsonSerializerOptions))).Trim('"');

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


    protected virtual CommandTestContextBuilder<TCommand, TCommandSettings> GetBuilder() =>
        new CommandTestContextBuilder<TCommand, TCommandSettings>().WithAnsiConsole(TestConsole);

    protected virtual CommandTestContext<TCommand, TCommandSettings> GetContext() =>
        GetBuilder()
            .Build();

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
