# Dependency ownership and cleanup boundaries

## Review scope

Treat dependency cleanup as a dedicated solution-wide change rather than incidental
feature work. Review direct dependency ownership, unused references, security
overrides, shared version management, build-tool alignment, production/test runtime
parity, and JavaScript package boundaries together. This keeps dependency policy
coherent and makes resolution changes independently reviewable.

Evaluate Central Package Management as part of that review; it is not yet an
adopted design. Preserve existing security fixes until their replacements are
verified across the complete graph.

## Direct dependencies versus transitive availability

Repeated package references do not produce duplicate copies of a package in the CLI.
Each project has its own restore graph; the executable resolves the combined graph.
Keep explicit references when a project consumes a package's API rather than removing
them solely because `dgt.power.common` also references the package.

- Webresource repositories consume `IOrganizationServiceAsync2` from
  `Microsoft.PowerPlatform.Dataverse.Client`.
- The common connection implementation constructs `ServiceClient`. Its constructor
  signatures also introduce a dependency on logging abstractions even though the
  source does not explicitly name `ILogger`. Namespace searches alone are insufficient
  to identify unused references.
- Production configuration and metadata code use `System.Runtime.Caching` directly.
  A newer reference in the shared test-helper project can cause module tests to use
  a different caching implementation from the CLI.

Evidence: `src/modules/dgt.power.webresource/Repositories/WebResourceRepository.cs:13`,
`src/dgt.power.common/Logic/DataverseConnectionVerifier.cs:31`,
`src/dgt.power.common/Logic/ConfigResolver.cs:50-63`,
`src/modules/dgt.power.codegeneration/Services/MetadataService.cs:23`,
`tests/dgt.power.tests/dgt.power.tests.csproj:20`.

## Security overrides and build tools

The XML cryptography security override in common flows to referencing modules.
Modules without their own XML reference already resolve the overridden version.
Repeating that override in the CLI, codegeneration, and webresource projects is
unnecessary under the current project-reference graph.

SourceLink and static-analysis packages use `PrivateAssets=all`. Their versions must
be managed at the project or shared build-props level: upgrading common does not
upgrade those tools in another project.

CI restores in locked mode. Any change to the production dependency graph must
update the CLI's `packages.lock.json` and remain compatible with locked restore.

Evidence: `src/dgt.power.common/dgt.power.common.csproj:18-28`,
`Directory.Build.props:38-45`, `.github/workflows/build.yml:41`,
`.github/workflows/checks.yml:48`.

## Evaluating removal candidates

Source searches and compiled assembly references identify high-confidence unused
reference candidates: Dataverse Client Dynamics in codegeneration, DiffPlex in
codegeneration tests, and Console CLI Testing in webresource tests. Confirm each
removal with restore/build and the affected tests before treating it as safe.

Version centralization and dependency removal are separate changes. Shared
`Directory.Build.props` files already manage build tools and the TUnit runner;
correcting their defaults can remove repeated `Update` entries without introducing
Central Package Management. A broader migration should preserve conditional
test-runner references and private build-tool metadata.

The root TypeScript compiler and the Jest fixture compiler must not be blindly
unified: the fixture's ts-jest peer dependency excludes TypeScript 7. Root
`@odata/parser` also supports generated fixture imports despite not being declared
in the fixture's own package manifest.

Evidence: `src/modules/dgt.power.codegeneration/dgt.power.codegeneration.csproj:46`,
`tests/dgt.power.codegeneration.tests/dgt.power.codegeneration.tests.csproj:18`,
`tests/dgt.power.webresource.tests/dgt.power.webresource.tests.csproj:15`,
`tests/Directory.Build.props:9-12`,
`tests/dgt.power.codegeneration.tests/Fixtures/TslJest/pnpm-lock.yaml:1267`,
`src/modules/dgt.power.codegeneration/Templates/tsl/xrm_mock_form_odata_filter.ts:1`.
