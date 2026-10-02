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

## Historical consequences for dgtp

- `AssemblyVersion` follows `<Version>`, so **every major release gets a new, empty store**.
  Minor and patch updates keep the same store.
- The old connection/profile implementation stored `identities.dat` and token blobs there; telemetry
  install/notice state, version-check state and profile-name completion also used the store.
- Do **not** use isolated storage for anything that must survive updates.

## Replacement in the current codebase

The application no longer uses isolated storage. `DgtpHome` resolves a stable per-user directory
(`DGTP_HOME` override; otherwise `XDG_DATA_HOME/dgtp` or `~/.local/share/dgtp` on Linux and
`LocalApplicationData/dgtp` elsewhere). Typed connection metadata is stored in versioned
`connections.json`; telemetry and version-check state are stored in `state.json`.

Client secrets and PFX passwords are kept outside the JSON document in the OS-protected storage
provided by `Microsoft.Identity.Client.Extensions.Msal`. User-credential token caches use Azure
Identity's persistent cache named `dgtp`. No migration is performed from 2.x isolated storage;
users recreate named connections. The canonical implementation reference is
`CONNECTION-STORAGE-DESIGN.md`.

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
