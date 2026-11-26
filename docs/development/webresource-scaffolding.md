# Webresource scaffolding acceptance

This opt-in procedure validates the dgtp scaffolder against the real webresource template. Run it
after the template's `main` contains the agreed markers and onboarding, before the command's initial
release; repeat it when the marker convention changes. It is not part of ordinary .NET builds or
tests.

## Marker convention

The template and dgtp share the reserved text markers `__DGTP_PROJECT_NAME__`,
`__DGTP_GLOBAL_NAME__`, `__DGTP_SOLUTION__`, and `__DGTP_PUBLISHER_PREFIX__`. Markers may appear
in any supported UTF-8 text file and may occur more than once. Keep their insertion quoting
conventions compatible with dgtp's escaping rules. Review template and dgtp changes together
whenever a marker or its insertion convention changes; ordinary file-layout or bundler changes do
not require a dgtp filename adapter.

## Real-template acceptance

1. Build projects into paths containing spaces, with and without deployment defaults:

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

## Focused test commands

The fixture-backed suite is the routine validation path and does not require GitHub access or
frontend package installation:

```shell
dotnet build tests/dgt.power.webresource.tests/dgt.power.webresource.tests.csproj
dotnet exec tests/dgt.power.webresource.tests/bin/Debug/net10.0/dgt.power.webresource.tests.dll
dotnet build tests/dgt.power.cli.tests/dgt.power.cli.tests.csproj
dotnet exec tests/dgt.power.cli.tests/bin/Debug/net10.0/dgt.power.cli.tests.dll
```
