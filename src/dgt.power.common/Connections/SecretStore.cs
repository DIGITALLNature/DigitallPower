// Copyright (c) DIGITALL Nature. All rights reserved
// DIGITALL Nature licenses this file to you under the Microsoft Public License.

using System.Text.Json;
using dgt.power.common.Storage;
using Microsoft.Identity.Client.Extensions.Msal;
using MsalStorage = Microsoft.Identity.Client.Extensions.Msal.Storage;
using IOFileAccess = System.IO.FileAccess;

namespace dgt.power.common.Connections;

public sealed class SecretStore : ISecretStore
{
    private const string StorageFileName = "secrets.bin";
    private static readonly JsonSerializerOptions s_jsonOptions = new();

    private readonly string _directory;
    private readonly string _lockPath;
    private readonly Lazy<MsalStorage> _storage;
    private readonly StorageSecurityNotice? _storageSecurityNotice;

    public SecretStore(
        string directory,
        bool allowUnencryptedStorage = false,
        StorageSecurityNotice? storageSecurityNotice = null)
    {
        ArgumentException.ThrowIfNullOrWhiteSpace(directory);
        _directory = directory;
        Directory.CreateDirectory(directory);
        DgtpHome.RestrictDirectoryPermissions(directory);
        _lockPath = Path.Combine(directory, "secrets.lock");
        _storageSecurityNotice = storageSecurityNotice;
        _storage = new Lazy<MsalStorage>(
            () => CreateStorage(allowUnencryptedStorage),
            LazyThreadSafetyMode.ExecutionAndPublication);
    }

    public string? ReadSecret(string connectionName, string key)
    {
        ArgumentException.ThrowIfNullOrWhiteSpace(connectionName);
        ArgumentException.ThrowIfNullOrWhiteSpace(key);
        using var fileLock = AcquireLock();
        return Read().GetValueOrDefault(GetSecretKey(connectionName, key));
    }

    public void WriteSecret(string connectionName, string key, string value)
    {
        ArgumentException.ThrowIfNullOrWhiteSpace(connectionName);
        ArgumentException.ThrowIfNullOrWhiteSpace(key);
        ArgumentNullException.ThrowIfNull(value);

        using var fileLock = AcquireLock();
        var values = Read();
        values[GetSecretKey(connectionName, key)] = value;
        Write(values);
    }

    public void Delete(string connectionName)
    {
        ArgumentException.ThrowIfNullOrWhiteSpace(connectionName);
        using var fileLock = AcquireLock();
        var values = Read();
        var prefix = $"{connectionName}\0";
        foreach (var key in values.Keys.Where(key => key.StartsWith(prefix, StringComparison.Ordinal)).ToList())
        {
            values.Remove(key);
        }

        Write(values);
    }

    public void DeleteSecret(string connectionName, string key)
    {
        ArgumentException.ThrowIfNullOrWhiteSpace(connectionName);
        ArgumentException.ThrowIfNullOrWhiteSpace(key);
        using var fileLock = AcquireLock();
        var values = Read();
        values.Remove(GetSecretKey(connectionName, key));
        Write(values);
    }

    private Dictionary<string, string> Read()
    {
        var storage = _storage.Value;
        var data = storage.ReadData();
        return data.Length == 0
            ? new Dictionary<string, string>(StringComparer.Ordinal)
            : JsonSerializer.Deserialize<Dictionary<string, string>>(data, s_jsonOptions)
              ?? throw new InvalidDataException("Secret data is empty or invalid.");
    }

    private void Write(Dictionary<string, string> values)
    {
        var storage = _storage.Value;
        if (values.Count == 0)
        {
            storage.Clear(ignoreExceptions: false);
            return;
        }

        storage.WriteData(JsonSerializer.SerializeToUtf8Bytes(values, s_jsonOptions));
        if (OperatingSystem.IsLinux() && File.Exists(Path.Combine(_directory, StorageFileName)))
        {
            File.SetUnixFileMode(
                Path.Combine(_directory, StorageFileName),
                UnixFileMode.UserRead | UnixFileMode.UserWrite);
        }
    }

    private MsalStorage CreateStorage(bool allowUnencryptedStorage)
    {
        var builder = new StorageCreationPropertiesBuilder(StorageFileName, _directory)
            .WithMacKeyChain("dgtp", "secrets")
            .WithLinuxKeyring(
                "com.digitall.dgtp",
                "default",
                "dgtp connection secrets",
                new KeyValuePair<string, string>("application", "dgtp"),
                new KeyValuePair<string, string>("version", "1"));

        var usesUnencryptedFile = OperatingSystem.IsLinux() && allowUnencryptedStorage;
        if (usesUnencryptedFile)
        {
            builder.WithLinuxUnprotectedFile();
        }

        var storage = MsalStorage.Create(builder.Build());
        if (usesUnencryptedFile)
        {
            _storageSecurityNotice?.WarnIfUnencryptedStorageIsUsed();
        }

        return storage;
    }

    private FileStream AcquireLock()
    {
        var deadline = DateTime.UtcNow.AddSeconds(10);
        while (true)
        {
            try
            {
                var fileLock = new FileStream(
                    _lockPath,
                    FileMode.OpenOrCreate,
                    IOFileAccess.ReadWrite,
                    FileShare.None);
                if (OperatingSystem.IsLinux())
                {
                    File.SetUnixFileMode(_lockPath, UnixFileMode.UserRead | UnixFileMode.UserWrite);
                }

                return fileLock;
            }
            catch (IOException) when (DateTime.UtcNow < deadline)
            {
                Thread.Sleep(50);
            }
            catch (IOException exception)
            {
                throw new IOException($"Timed out waiting for secret-store lock '{_lockPath}'.", exception);
            }
        }
    }

    private static string GetSecretKey(string connectionName, string key) => $"{connectionName}\0{key}";
}
