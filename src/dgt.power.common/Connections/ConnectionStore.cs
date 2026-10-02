// Copyright (c) DIGITALL Nature. All rights reserved
// DIGITALL Nature licenses this file to you under the Microsoft Public License.

using System.Text.Json;
using dgt.power.common.Storage;

namespace dgt.power.common.Connections;

public sealed class ConnectionStore(DgtpHome home) : IConnectionStore
{
    private static readonly JsonSerializerOptions s_jsonOptions = new()
    {
        WriteIndented = true,
        PropertyNameCaseInsensitive = true,
        PropertyNamingPolicy = JsonNamingPolicy.CamelCase
    };

    private static readonly TimeSpan s_lockTimeout = TimeSpan.FromSeconds(10);
    private readonly string _path = home.ConnectionsPath;
    private readonly string _lockPath = System.IO.Path.Combine(home.Path, "connections.lock");

    public string? Current => ReadDocument().Current;

    public IReadOnlyDictionary<string, ConnectionDefinition> GetAll() =>
        new Dictionary<string, ConnectionDefinition>(ReadDocument().Connections, StringComparer.OrdinalIgnoreCase);

    public ConnectionDefinition? Find(string name)
    {
        ArgumentException.ThrowIfNullOrWhiteSpace(name);
        return ReadDocument().Connections.GetValueOrDefault(name);
    }

    public void Upsert(string name, ConnectionDefinition connection, bool makeCurrent = true)
    {
        ArgumentException.ThrowIfNullOrWhiteSpace(name);
        ArgumentNullException.ThrowIfNull(connection);

        Update(document =>
        {
            document.Connections[name] = connection;
            if (makeCurrent || string.IsNullOrWhiteSpace(document.Current))
            {
                document.Current = name;
            }
        });
    }

    public void SetCurrent(string name)
    {
        ArgumentException.ThrowIfNullOrWhiteSpace(name);
        Update(document =>
        {
            if (!document.Connections.ContainsKey(name))
            {
                throw new KeyNotFoundException($"Connection '{name}' was not found.");
            }

            document.Current = document.Connections.Keys.First(key =>
                string.Equals(key, name, StringComparison.OrdinalIgnoreCase));
        });
    }

    public bool Remove(string name)
    {
        ArgumentException.ThrowIfNullOrWhiteSpace(name);
        var removed = false;
        Update(document =>
        {
            var key = document.Connections.Keys.FirstOrDefault(candidate =>
                string.Equals(candidate, name, StringComparison.OrdinalIgnoreCase));
            if (key is null)
            {
                return;
            }

            removed = document.Connections.Remove(key);
            if (string.Equals(document.Current, key, StringComparison.OrdinalIgnoreCase))
            {
                document.Current = document.Connections.Keys.FirstOrDefault();
            }
        });
        return removed;
    }

    public void RemoveAll() => Update(document =>
    {
        document.Connections.Clear();
        document.Current = null;
    });

    private void Update(Action<ConnectionDocument> mutate)
    {
        using var fileLock = AcquireLock();
        var document = ReadDocument();
        mutate(document);
        WriteDocument(document);
    }

    private ConnectionDocument ReadDocument()
    {
        if (!File.Exists(_path))
        {
            return new ConnectionDocument();
        }

        try
        {
            using var stream = File.OpenRead(_path);
            var document = JsonSerializer.Deserialize<ConnectionDocument>(stream, s_jsonOptions)
                ?? throw new InvalidDataException($"Connection file '{_path}' is empty or invalid.");

            if (document.SchemaVersion != 1)
            {
                throw new InvalidDataException(
                    $"Connection file schema version {document.SchemaVersion} is not supported.");
            }

            document.Connections = new Dictionary<string, ConnectionDefinition>(
                document.Connections,
                StringComparer.OrdinalIgnoreCase);
            return document;
        }
        catch (JsonException exception)
        {
            throw new InvalidDataException($"Connection file '{_path}' contains invalid JSON.", exception);
        }
    }

    private void WriteDocument(ConnectionDocument document)
    {
        var temporaryPath = $"{_path}.{Guid.NewGuid():N}.tmp";
        try
        {
            using (var stream = new FileStream(temporaryPath, FileMode.CreateNew, System.IO.FileAccess.Write, FileShare.None))
            {
                JsonSerializer.Serialize(stream, document, s_jsonOptions);
                stream.Flush(flushToDisk: true);
            }

            if (!OperatingSystem.IsWindows())
            {
                File.SetUnixFileMode(temporaryPath, UnixFileMode.UserRead | UnixFileMode.UserWrite);
            }

            File.Move(temporaryPath, _path, overwrite: true);
        }
        finally
        {
            if (File.Exists(temporaryPath))
            {
                File.Delete(temporaryPath);
            }
        }
    }

    private FileStream AcquireLock()
    {
        var deadline = DateTime.UtcNow + s_lockTimeout;
        while (true)
        {
            try
            {
                return new FileStream(_lockPath, FileMode.OpenOrCreate, System.IO.FileAccess.ReadWrite, FileShare.None);
            }
            catch (IOException) when (DateTime.UtcNow < deadline)
            {
                Thread.Sleep(50);
            }
            catch (IOException exception)
            {
                throw new IOException($"Timed out waiting for connection-store lock '{_lockPath}'.", exception);
            }
        }
    }

    private sealed class ConnectionDocument
    {
        public int SchemaVersion { get; init; } = 1;

        public string? Current { get; set; }

        public Dictionary<string, ConnectionDefinition> Connections { get; set; } =
            new(StringComparer.OrdinalIgnoreCase);
    }
}
