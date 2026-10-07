// Copyright (c) DIGITALL Nature. All rights reserved
// DIGITALL Nature licenses this file to you under the Microsoft Public License.

using System.Text.Json;
using System.Text.Json.Nodes;
using IOFileAccess = System.IO.FileAccess;

namespace dgt.power.common.Storage;

public sealed class StateStore(DgtpHome home)
{
    private static readonly JsonSerializerOptions s_jsonOptions = new()
    {
        WriteIndented = true,
        PropertyNamingPolicy = JsonNamingPolicy.CamelCase
    };
    private static readonly TimeSpan s_lockTimeout = TimeSpan.FromSeconds(10);
    private readonly string _path = home.StatePath;
    private readonly string _lockPath = Path.Combine(home.Path, "state.lock");

    public bool TelemetryEnabled => Read().Telemetry.Enabled;

    public void SetTelemetryEnabled(bool enabled)
    {
        using var fileLock = AcquireLock();
        var state = Read();
        state.Telemetry.Enabled = enabled;
        Write(state);
    }

    public string GetOrCreateTelemetryInstallId() => GetOrCreateTelemetryInstallId(out _);

    public string GetOrCreateTelemetryInstallId(out bool created)
    {
        using var fileLock = AcquireLock();
        var state = Read();
        if (state.Telemetry.InstallId is not null)
        {
            created = false;
            return state.Telemetry.InstallId;
        }

        state.Telemetry.InstallId = Guid.NewGuid().ToString("D");
        Write(state);
        created = true;
        return state.Telemetry.InstallId;
    }

    public DateTime LastVersionCheckOn => Read().Updates.LastCheckOn?.DateTime ?? DateTime.MinValue;

    public void SetLastVersionCheckOn(DateTime value)
    {
        using var fileLock = AcquireLock();
        var state = Read();
        state.Updates.LastCheckOn = value;
        Write(state);
    }

    private StateDocument Read()
    {
        if (!File.Exists(_path))
        {
            return new StateDocument();
        }

        using var stream = File.OpenRead(_path);
        var document = JsonNode.Parse(stream) as JsonObject
            ?? throw new InvalidDataException($"State file '{_path}' must contain a JSON object.");
        if (!document.ContainsKey("schemaVersion"))
        {
            MigrateLegacyDocument(document);
        }

        if (document["schemaVersion"] is not JsonValue version
            || !version.TryGetValue<int>(out var schemaVersion)
            || schemaVersion != 1)
        {
            throw new InvalidDataException($"State file '{_path}' has an unsupported schema version.");
        }

        var state = document.Deserialize<StateDocument>(s_jsonOptions)
            ?? throw new InvalidDataException($"State file '{_path}' is empty or invalid.");
        if (state.Telemetry is null || state.Updates is null
            || (state.Telemetry.InstallId is not null && !Guid.TryParseExact(state.Telemetry.InstallId, "D", out _)))
        {
            throw new InvalidDataException($"State file '{_path}' contains invalid telemetry or update state.");
        }

        return state;
    }

    private static void MigrateLegacyDocument(JsonObject document)
    {
        if (document.ContainsKey("telemetry") || document.ContainsKey("updates"))
        {
            throw new InvalidDataException("Nested application state must declare a schemaVersion.");
        }

        document["schemaVersion"] = 1;
        document.Remove("TelemetryInstallId", out var installId);
        document.Remove("LastVersionCheckOn", out var lastCheckOn);
        document.Remove("TelemetryNoticeShownOn");
        document["telemetry"] = new JsonObject { ["enabled"] = true, ["installId"] = installId };
        document["updates"] = new JsonObject { ["lastCheckOn"] = lastCheckOn };
    }

    private void Write(StateDocument state)
    {
        var temporaryPath = $"{_path}.{Guid.NewGuid():N}.tmp";
        try
        {
            using (var stream = new FileStream(temporaryPath, FileMode.CreateNew, IOFileAccess.Write, FileShare.None))
            {
                JsonSerializer.Serialize(stream, state, s_jsonOptions);
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
                return new FileStream(_lockPath, FileMode.OpenOrCreate, IOFileAccess.ReadWrite, FileShare.None);
            }
            catch (IOException) when (DateTime.UtcNow < deadline)
            {
                Thread.Sleep(50);
            }
            catch (IOException exception)
            {
                throw new IOException($"Timed out waiting for state-store lock '{_lockPath}'.", exception);
            }
        }
    }
}
