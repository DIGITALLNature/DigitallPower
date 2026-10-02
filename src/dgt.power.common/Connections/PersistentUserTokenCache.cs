// Copyright (c) DIGITALL Nature. All rights reserved
// DIGITALL Nature licenses this file to you under the Microsoft Public License.

using Azure.Identity;
using Microsoft.Identity.Client;
using Microsoft.Identity.Client.Extensions.Msal;
using dgt.power.common.Storage;

namespace dgt.power.common.Connections;

public sealed class PersistentUserTokenCache(StorageSecurityNotice storageSecurityNotice) : IUserTokenCache
{
    private const string CacheName = "dgtp.nocae";
    private const string KeychainService = "Microsoft.Developer.IdentityService";
    private const string LinuxKeyringSchema = "msal.cache";
    private const string LinuxKeyringCollection = "default";

    private static readonly KeyValuePair<string, string> s_linuxKeyringAttribute1 =
        new("MsalClientID", KeychainService);

    private static readonly KeyValuePair<string, string> s_linuxKeyringAttribute2 =
        new(KeychainService, "1.0.0.0");

    public async Task<bool> RemoveAccountAsync(
        AuthenticationRecord authenticationRecord,
        bool allowUnencryptedStorage,
        CancellationToken cancellationToken)
    {
        ArgumentNullException.ThrowIfNull(authenticationRecord);
        cancellationToken.ThrowIfCancellationRequested();

        var cacheHelper = await CreateCacheHelperAsync(allowUnencryptedStorage);
        var application = PublicClientApplicationBuilder
            .Create(authenticationRecord.ClientId)
            .WithAuthority(authenticationRecord.Authority)
            .Build();
        cacheHelper.RegisterCache(application.UserTokenCache);

        var account = (await application.GetAccountsAsync()).FirstOrDefault(candidate =>
            string.Equals(
                candidate.HomeAccountId?.Identifier,
                authenticationRecord.HomeAccountId,
                StringComparison.OrdinalIgnoreCase));
        if (account is null)
        {
            return false;
        }

        await application.RemoveAsync(account);
        return true;
    }

    private async Task<MsalCacheHelper> CreateCacheHelperAsync(bool allowUnencryptedStorage)
    {
        try
        {
            var helper = await MsalCacheHelper.CreateAsync(CreateProtectedStorageProperties());
            helper.VerifyPersistence();
            return helper;
        }
        catch (MsalCachePersistenceException) when (allowUnencryptedStorage)
        {
            var helper = await MsalCacheHelper.CreateAsync(CreateFallbackStorageProperties());
            helper.VerifyPersistence();
            storageSecurityNotice.WarnIfUnencryptedStorageIsUsed();
            return helper;
        }
    }

    private static StorageCreationProperties CreateProtectedStorageProperties()
    {
        var cacheDirectory = Path.Combine(
            Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData),
            ".IdentityService");
        var builder = new StorageCreationPropertiesBuilder(CacheName, cacheDirectory)
            .WithMacKeyChain(KeychainService, CacheName)
            .WithLinuxKeyring(
                LinuxKeyringSchema,
                LinuxKeyringCollection,
                CacheName,
                s_linuxKeyringAttribute1,
                s_linuxKeyringAttribute2);

        return builder.Build();
    }

    private static StorageCreationProperties CreateFallbackStorageProperties()
    {
        var cacheDirectory = Path.Combine(
            Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData),
            ".IdentityService");
        return new StorageCreationPropertiesBuilder(CacheName, cacheDirectory)
            .WithMacKeyChain(KeychainService, CacheName)
            .WithLinuxUnprotectedFile()
            .Build();
    }
}
