# Research: Persistent MSAL token-cache removal

Azure.Identity turns `TokenCachePersistenceOptions.Name` into the actual cache filename by adding
`.nocae` when CAE is not enabled (the default used by the typed user credentials). For a configured
name of `dgtp`, MSAL therefore uses `dgtp.nocae` under
`LocalApplicationData/.IdentityService`.

To operate on that same cache with `Microsoft.Identity.Client.Extensions.Msal`, match Azure.Core's
platform settings:

- macOS Keychain service: `Microsoft.Developer.IdentityService`; account name is the actual cache
  filename.
- Linux Secret Service: schema `msal.cache`, collection `default`, and the attributes
  `MsalClientID=Microsoft.Developer.IdentityService` and
  `Microsoft.Developer.IdentityService=1.0.0.0`.
- Windows uses the MSAL Extensions default DPAPI-protected file.
- When `UnsafeAllowUnencryptedStorage` is enabled, Azure.Identity first tries protected storage and
  only falls back to `WithLinuxUnprotectedFile()` after `MsalCachePersistenceException`. Selecting
  the unprotected backend immediately would address a different cache than the protected cache
  already in use.

When deleting the final stored connection for an account, register the persistent helper against a
public-client application's user token cache, locate `IAccount` by
`AuthenticationRecord.HomeAccountId`, and call `RemoveAsync(account)`. Never clear the entire
shared cache: multiple named connections may reference different users or share one account.

References: [Azure.Core 1.62.0 TokenCache.cs](https://github.com/Azure/azure-sdk-for-net/blob/Azure.Core_1.62.0/sdk/core/Azure.Core/src/Identity/TokenCache.cs),
[Azure.Core 1.62.0 Constants.cs](https://github.com/Azure/azure-sdk-for-net/blob/Azure.Core_1.62.0/sdk/core/Azure.Core/src/Identity/Constants.cs),
and `src/dgt.power.common/Connections/PersistentUserTokenCache.cs`.
