# Research: Isolated Storage is scoped by assembly major version

## Finding

`IsolatedStorageFile.GetUserStoreForAssembly()` resolves to a directory whose name is derived from
the assembly evidence. For **strong-named** assemblies (`SignAssembly=True` in
`Directory.Build.props`), the runtime uses
`System.Security.IdentityHelper.GetNormalizedStrongNameHash(AssemblyName)`
(`dotnet/runtime` `src/libraries/Common/src/System/Security/IdentityHelper.cs`), which hashes:

```text
publicKey bytes + (int32) AssemblyName.Version.Major + AssemblyName.Name
```

Layout: `<IsolatedStorage root>/<random>/<random>/StrongName.<hash>/AssemFiles/<file>`
(the root is `%LOCALAPPDATA%\IsolatedStorage` on Windows and `<LocalApplicationData>/IsolatedStorage` on Unix).

## Consequences for dgtp

- `AssemblyVersion` follows `<Version>`, so **every major release gets a new, empty store**.
  Minor and patch updates keep the same store.
- Affected: `ProfileManager` (`identities.dat`, all connections + MSAL token blobs),
  `TelemetryConfig` (install ID), `TelemetryNotice` (first-run marker), `VersionCheckInterceptor`,
  `ProfileNamesProvider` (completion).
- Do **not** use isolated storage for anything that must survive updates.

## Recoverability

Old stores remain on disk and are readable from a newer version:

- The path can be computed: reproduce the hash for previous majors and search the root for
  `StrongName.<hash>/AssemFiles/identities.dat`.
- The encryption does not depend on the version. Windows uses DPAPI CurrentUser with the fixed
  entropy `ProfileEnv.Seed`. Other platforms use ASP.NET DataProtection (`"dgtp"` /
  `"dgtp-Identity"`), with keys in `~/.aspnet/DataProtection-Keys`. Note that these keys are
  stored **unencrypted** on macOS/Linux.

## Related

- Replacement design: `CONNECTION-STORAGE-DESIGN.md` (repo root). Migration from the legacy store
  is deliberately deferred there (§10, §15); the recoverability notes above are its basis.
- Pinning `<AssemblyVersion>` would stop the major-version scoping, but the store would still
  depend on the assembly name and signing key. It is not a substitute for a stable app-data
  directory.
