// Copyright (c) DIGITALL Nature. All rights reserved
// DIGITALL Nature licenses this file to you under the Microsoft Public License.

using System.Text.Json;
using IOFileAccess = System.IO.FileAccess;

namespace dgt.power.common.Storage;

public sealed class StateStore(DgtpHome home)
{
    private static readonly JsonSerializerOptions s_jsonOptions = new() { WriteIndented = true };
    private static readonly TimeSpan s_lockTimeout = TimeSpan.FromSeconds(10);
    private readonly string _path = home.StatePath;
    private readonly string _lockPath = Path.Combine(home.Path, "state.lock");

    public string GetOrCreateTelemetryInstallId()
    {
        using var fileLock = AcquireLock();
        var state = Read();
        if (Guid.TryParse(state.TelemetryInstallId, out _))
        {
            return state.TelemetryInstallId;
        }

        state.TelemetryInstallId = Guid.NewGuid().ToString("D");
        Write(state);
        return state.TelemetryInstallId;
    }

    public bool TelemetryNoticeShown => Read().TelemetryNoticeShownOn is not null;

    public void MarkTelemetryNoticeShown()
    {
        using var fileLock = AcquireLock();
        var state = Read();
        state.TelemetryNoticeShownOn ??= DateTimeOffset.UtcNow;
        Write(state);
    }

    public DateTime LastVersionCheckOn => Read().LastVersionCheckOn?.DateTime ?? DateTime.MinValue;

    public void SetLastVersionCheckOn(DateTime value)
    {
        using var fileLock = AcquireLock();
        var state = Read();
        state.LastVersionCheckOn = value;
        Write(state);
    }

    private StateDocument Read()
    {
        if (!File.Exists(_path))
        {
            return new StateDocument();
        }

        using var stream = File.OpenRead(_path);
        return JsonSerializer.Deserialize<StateDocument>(stream, s_jsonOptions)
            ?? throw new InvalidDataException($"State file '{_path}' is empty or invalid.");
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

    private sealed class StateDocument
    {
        public string? TelemetryInstallId { get; set; }

        public DateTimeOffset? TelemetryNoticeShownOn { get; set; }

        public DateTimeOffset? LastVersionCheckOn { get; set; }
    }
}
