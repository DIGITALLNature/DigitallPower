using System.IO.IsolatedStorage;
using System.Security.Cryptography;
using System.Text.Json;
using Microsoft.AspNetCore.DataProtection;
using Spectre.Console;

namespace dgt.power.common.Logic;

public class ProfileManager : IProfileManager
{
    private readonly string _identityFileName;
    private readonly IsolatedStorageFile _storage;
    private Identities _identities = new();

    public ProfileManager(IsolatedStorageFile storage, string identityFileName = "identities.dat")
    {
        _storage = storage;
        _identityFileName = identityFileName;
    }

    public IIdentities GetIdentities()
    {
        if (!_identities.IsEmpty)
        {
            return _identities ??= new Identities();
        }

        if (!_storage.FileExists(_identityFileName))
        {
            return _identities ??= new Identities();
        }

        using var identityStream = new IsolatedStorageFileStream(_identityFileName, FileMode.Open, _storage);
        using var ms = new MemoryStream();
        identityStream.CopyTo(ms);
        var protectedMemory = ms.ToArray();
        protectedMemory = UnprotectData(protectedMemory);

        try
        {
#pragma warning disable CS8601
            _identities = JsonSerializer.Deserialize<Identities>(protectedMemory);
#pragma warning restore CS8601
        }
        catch (Exception e)
        {
            AnsiConsole.WriteException(e);
        }

        return _identities ??= new Identities();
    }

    public void Save()
    {
        // Use FileMode.Create to recreate a new file OpenOrCreate has weird behaviour that keeps fragment of the old file content sometimes
        using var identityStream = new IsolatedStorageFileStream(_identityFileName, FileMode.Create, _storage);
        var json = JsonSerializer.SerializeToUtf8Bytes(_identities);
        json = ProtectData(json);
        identityStream.Write(json, 0, json.Length);
    }

    public string Current
    {
        get
        {
            if (_identities.IsEmpty)
            {
                GetIdentities();
            }

            return _identities.Current;
        }
    }

    public Identity? CurrentIdentity
    {
        get
        {
            if (_identities.IsEmpty)
            {
                GetIdentities();
            }

            return _identities.CurrentIdentity;
        }
    }

    public string CurrentConnectionString
    {
        get
        {
            if (_identities.IsEmpty)
            {
                GetIdentities();
            }

            return _identities.CurrentConnectionString;
        }
    }

    public void Purge()
    {
        _identities = new Identities();
        Save();
    }

    private static byte[] ProtectData(byte[] json)
    {
        byte[] protectData;

        if (OperatingSystem.IsWindows())
        {
            protectData = ProtectedData.Protect(json, ProfileEnv.Seed, DataProtectionScope.CurrentUser);
        }
        else
        {
            protectData = GetDataProtector().Protect(json);
        }

        return protectData;
    }


    private static byte[] UnprotectData(byte[] protectedMemory)
    {
        byte[] unprotectedData;
        if (OperatingSystem.IsWindows())
        {
            unprotectedData = ProtectedData.Unprotect(protectedMemory, ProfileEnv.Seed, DataProtectionScope.CurrentUser);
        }
        else
        {
            unprotectedData = GetDataProtector().Unprotect(protectedMemory);
        }

        return unprotectedData;
    }

    private static IDataProtector GetDataProtector()
    {
        var applicationName = "dgtp";
        var protectionProvider = DataProtectionProvider.Create(applicationName);
        var protector = protectionProvider.CreateProtector($"{applicationName}-Identity");
        return protector;
    }

    private static class ProfileEnv
    {
        public static byte[] Seed { get; } = { 22, 4, 19, 8, 6 };
    }
}
