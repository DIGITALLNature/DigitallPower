# Proposal

## Why

Starting a DIGITALL-based Dataverse plugin project currently requires assembling the framework references, registration example, and deployable-package build configuration manually. A standard .NET template provides that substantive value directly; a dgtp wrapper adds convenience and discoverability, not unique parameterization or generation capabilities.

## What Changes

### First milestone: native template

- Coordinate an owned standard .NET template, maintained in a separate repository and published as a NuGet template package. Finalize repository, package, and template identities; example names are not existing published assets.
- Support `dotnet new install <template-package-id>` followed by `dotnet new <template-short-name>` with documented name, namespace, and output parameters. Use standard template features for content, filenames, and project/solution references; require no dgtp installation to generate or build.
- Generate a project that builds a Dataverse plugin package (`.nupkg`) and references `Digitall.Plugins` and `Digitall.Plugins.Registration`. Verify physical runtime dependency contents and registration metadata, not merely successful source generation.
- Provide generated README guidance for build prerequisites, package location, explicit deployment, native template updates, and optional `dotnet sln <solution> add <generated-project>`. Leave existing solutions untouched.
- Publish and validate the template independently of dgtp releases. No CLI command or embedded template-engine dependencies are required for this milestone.

### dgtp initialization

- Add `dgtp plugin init <directory> --name <project-name> [--namespace <namespace>]`, defaulting the namespace to the project name. The resource-oriented entry point provides convenient discovery and consistent onboarding without replacing the native workflow.
- Host the engine through .NET APIs rather than invoking `dotnet new` or PAC. Use the normal shared per-user installation: announce/install latest stable only when missing, otherwise reuse the installed stable version.
- Check for stable updates and print a targeted native update hint without updating an existing installation. Add neither update flags nor template-management or solution-integration commands.
- Generate only into a new/empty directory without authentication, deployment, generated-project restore/build, template post-actions, or Git initialization; report version, build, README, and native solution-add guidance.
- Verify hosting/dependency compatibility and shared installation before releasing the dgtp command. These checks are not prerequisites for native template delivery and have not yet been performed.

Webresource scaffolding remains a separate change and implementation PR. Arbitrary template sources, prerelease selection, version pinning options, a general template manager, automatic changes to generated projects, and a PAC wrapper are out of scope.

## Capabilities

### New Capabilities

- `plugin-scaffolding`: Native plugin template usability, package-oriented output, and resource-oriented dgtp initialization.

### Modified Capabilities

None. Existing plugin deployment, command execution, and shared command-tree contracts remain unchanged.

## Impact

- First milestone: coordinate separately authorized external template creation, CI, packaging, publication, and native-workflow documentation. This planning change creates no external repository or NuGet publication and changes no dgtp source or dependencies.
- After publication, document the native starting workflow in `docs/guides/plugin-deployment.md` and its acceptance procedure in `docs/development/build-and-test.md`; do not advertise an unimplemented dgtp command.
- Add plugin-module command/settings/hosting services and compatible engine dependencies, synchronize the application lock file, register the command, and add module/CLI tests and command-reference documentation.
- A suitable .NET SDK is assumed for native development. The dgtp command requires NuGet for first installation/update checks, with warning-only update-check failure when a usable template is already installed.
