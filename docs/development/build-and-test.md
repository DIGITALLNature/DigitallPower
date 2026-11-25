# Build & Test

## Prerequisites

- **.NET SDK 10.0** (the SDK version is pinned via [`global.json`](../../global.json))
- **pnpm 12** — install once via `curl -fsSL https://get.pnpm.io/install.sh | sh -`; Node.js 24 is managed automatically via `devEngines` in `package.json` (needed for the TypeScript compile gate in `dgt.power.codegeneration.tests`)
- Network access to `api.nuget.org` (for restore)

## Commands

```bash
pnpm install                 # Install JS tooling, TypeScript compiler, and local OpenSpec CLI (also provisions Node.js 24 via devEngines)
dotnet restore                # Restore dependencies (uses lock files)
dotnet build                  # Build the solution
dotnet test                   # Run all tests
dotnet test --treenode-filter "/*/*/Foo"   # Run a subset of tests
pnpm exec openspec validate --specs  # Validate current OpenSpec specifications
```

## Build a local NuGet package

```bash
dotnet pack src/dgt.power/dgt.power.csproj -c Release
```

The resulting `.nupkg` is placed in the `packages/` folder and can be installed locally with:

```bash
dotnet tool install --global --add-source ./packages dgt.power --version <version>
```

## Webresource scaffolding acceptance

The webresource template and dgtp share the reserved text markers
`__DGTP_PROJECT_NAME__`, `__DGTP_GLOBAL_NAME__`, `__DGTP_SOLUTION__`, and
`__DGTP_PUBLISHER_PREFIX__`. Markers may appear in any supported UTF-8 text file and may occur more
than once. Keep their insertion quoting conventions compatible with dgtp's escaping rules. Review
template and dgtp changes together whenever a marker or its insertion convention changes; ordinary
file-layout or bundler changes do not require a dgtp filename adapter.

Run this real-template acceptance check after the template's `main` contains the agreed markers and
onboarding, before the command's initial release; repeat it when the marker convention changes. It is
intentionally opt-in and does not add GitHub downloads or frontend installs to ordinary .NET builds
and tests.

1. Build a project into a path containing spaces, with and without deployment defaults:

   ```shell
   dgtp webresource init "./acceptance runs/with defaults" \
     --name contoso-webresources --global Contoso \
     --solution ContosoCore --publisher-prefix contoso
   dgtp webresource init "./acceptance runs/without defaults" \
     --name contoso-webresources --global Contoso
   ```

2. Record the resolved commit printed by dgtp. In each project, run
   `pnpm install --frozen-lockfile` and `pnpm run build:prod`; verify both builds succeed, the
   generated bundles expose the selected global, and the dependency lockfile is unchanged.
3. Check that the generated README explains the remaining model, connection, deployment, and CI
   setup. Verify the deployment script uses the template's pinned dgtp tooling, requires missing
   values and an execution-time connection before any Dataverse operation, and does not select or
   persist a connection during scaffolding. Confirm deployment CI stays disabled until explicitly
   configured.
4. Repeat initialization with `--template-version <resolved-commit-sha>` to verify revision
   reproducibility. This check does not create a dgtp release pin and does not require template
   tags or GitHub Releases.

The fixture-backed suite remains the routine validation path:

```shell
dotnet build tests/dgt.power.webresource.tests/dgt.power.webresource.tests.csproj
dotnet exec tests/dgt.power.webresource.tests/bin/Debug/net10.0/dgt.power.webresource.tests.dll
dotnet build tests/dgt.power.cli.tests/dgt.power.cli.tests.csproj
dotnet exec tests/dgt.power.cli.tests/bin/Debug/net10.0/dgt.power.cli.tests.dll
```
