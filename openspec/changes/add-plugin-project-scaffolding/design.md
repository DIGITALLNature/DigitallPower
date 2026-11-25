# Design

## Context

See `proposal.md` for motivation and scope. The current `plugin` branch registers deployment and step-management commands, not project initialization. It uses `PluginSettings : BaseProgramSettings`; `PluginPushCommand` acquires Dataverse only after local validation. Native template generation is independently deliverable; the planned initialization command must remain local apart from template acquisition and update discovery.

The plugin module targets .NET 10 and already references `NuGet.Packaging`. Its deployment guide recommends `Digitall.Plugins.Registration` for packaged plugins and distinguishes it from `Digitall.Plugins.Registration.Source` for standalone assemblies. Initial scaffolding targets packages only; existing deployment behavior is not changed.

Microsoft documents `Microsoft.TemplateEngine.IDE.Bootstrapper` as a higher-level API over `Microsoft.TemplateEngine.Edge`, with discovery, installation, generation, and creation-effects operations. This establishes a possible integration, not a prerequisite for native template delivery or a verified match for dgtp's dependency graph.

## Goals / Non-Goals

**Goals:**

- Delegate .NET template transformations and filename/reference renaming to the standard engine.
- Provide a discoverable resource-oriented dgtp initialization command while retaining an independently usable native workflow.
- Separate the template's source/package lifecycle from dgtp's release lifecycle.
- Prove that generated output builds a deployable plugin package, not merely an ordinary NuGet package.

**Non-Goals:**

- No general template-hosting abstraction shared with webresource scaffolding.
- No subprocess wrapper for `dotnet new`, PAC, or solution manipulation.
- No template-management commands, update flag, version adapter registry, or edits to existing solutions.
- No changes to the deployment engine or automatic deployment of a sample plugin.
- No engine-hosting spike, new dgtp dependency, or CLI change in the native-template milestone.

## Decisions

### 1. Native template is independently deliverable

Maintain the template in a separate DIGITALL repository and publish it as a standard NuGet template package. Finalize the repository name, package ID, short name, and stable template identity before integration; names discussed in the conversation are provisional.

The template owns `.template.config\template.json`, project sources, dependency versions, packaging configuration, and README. Use standard `sourceName`, symbols, and filename replacement instead of teaching dgtp which `.csproj` or solution filenames to rewrite. Declare the generated plugin project as a primary output so onboarding can reference its actual path.

The initial starter includes a minimal plugin using the supported `Digitall.Plugins` base abstraction and a supported `Digitall.Plugins.Registration` attribute. Do not generate PAC's `PluginBase` or reference `Digitall.Plugins.Registration.Source`.

The generated project must build a Dataverse plugin `.nupkg`. Evaluate `Microsoft.PowerApps.MSBuild.Plugin` for the package build rather than inventing a packaging format. The template's target framework must be supported by Dataverse and the selected DIGITALL packages; it is not inherited from dgtp's .NET 10 target.

The package must physically contain the plugin and required non-platform runtime dependencies, including the framework and registration assemblies. A `.nuspec` dependency declaration alone is insufficient. Verify transitive runtime dependencies, exclusion of platform-provided SDK assemblies, and signing requirements against the selected versions.

The primary user workflow is native installation followed by generation:

```text
dotnet new install <template-package-id>
dotnet new <template-short-name> --name Contoso.Plugins --output .\Contoso.Plugins
```

These are placeholders for names to be finalized, not commands for an already published package. Document the template's namespace option and default alongside the built-in name/output options. Verify generation and building without dgtp or PAC installed and with no Dataverse connection.

Publishing this source-template `.nupkg` is distinct from building the generated deployment `.nupkg`. Template CI, release, README, and native usability must not depend on embedding the engine in dgtp. After publication, document this native workflow in the existing plugin development guide; document `plugin init` when it is implemented.

**Alternatives:** embedding starter files in dgtp couples releases; downloading raw sources requires reimplementing template semantics. A standard NuGet template delivers the conventions directly and supports both entry points.

### 2. Resource-oriented initialization alongside native tooling

The team chose dgtp as the shared initialization entry point: `plugin init` provides discovery alongside `plugin push`, consistent onboarding, and automatic first-use installation. The .NET template supplies parameterization, filename replacement, framework references, packaging, and README independently of dgtp.

The native workflow remains fully supported, and its publication need not wait for dgtp implementation. Walk through native installation, generation, build, update, and optional solution membership to verify the template contract, not to decide again whether to build the wrapper.

**Alternatives:** native-only tooling avoids wrapper maintenance but lacks the unified dgtp entry point. A generic template-management framework duplicates established .NET tooling without improving the initial resource workflow.

### 3. Resource-owned command with a narrow hosting adapter

Register:

```text
dgtp plugin init <directory> --name <project-name> [--namespace <namespace>]
```

Default the namespace to the name. Validate that the name is usable as a project/package identity, that the effective namespace is valid C#, and that neither value can introduce paths. Reject invalid arguments and occupied destinations before installation or network requests.

Implement a `PowerCommand<TSettings>` command and focused services in the plugin module. Inject the console, telemetry span factory, and template operations explicitly; do not request `IDataverseConnection` or `IConfigResolver`. Constructors and DI registration perform no I/O. Create/dispose engine resources inside the asynchronous operation.

Prefer the documented `Bootstrapper` API with a small testable boundary around template operations. Pin a compatible engine-package version in dgtp's dependencies, not a template-package version. Do not copy SDK binaries or bind to an arbitrary installed SDK's internal assemblies.

**Alternatives:** PAC plus postprocessing couples us to generated source layout; content-only markers do not handle project filenames/references; subprocess invocation adds process/output handling without being necessary for the chosen engine.

### 4. Shared-installation behavior

During native-only delivery, installation and updates use ordinary .NET tooling without a dgtp-managed lifecycle or custom update check. Once the dgtp command is available, the following behavior applies to it.

Use the normal per-user template installation and select only the official package and stable template identity, not an arbitrary matching short name.

| State | Behavior |
|---|---|
| Official template package absent | Resolve latest stable once, announce installation, install that exact version through the API, and generate with it. |
| Usable stable version installed | Reuse that version; attempt a bounded stable-update check without modifying the installation. |
| Newer stable version available | Report installed and available versions and print `dotnet new install <actual-package-id>`. Generate with the installed version. |
| Update check unavailable | Warn that checking failed and continue with the usable installed version; do not claim it is current. |
| Initial resolution/installation fails | Fail with an actionable diagnostic and no published project. |
| Existing installation is unusable, prerelease-only, or ambiguous | Fail with native remediation guidance rather than silently replacing or downgrading it. |

Use NuGet version semantics, not string comparison. No prerelease fallback or automatic downgrade is permitted. Retain the selected package version for the run; if another process changes it before generation, fail clearly rather than silently generating from a different version.

There is no `--update-template` or `plugin template` branch. Native tooling owns subsequent updates, uninstall, and other lifecycle operations. For updating only our package, the hint uses `dotnet new install <package-id>`: Microsoft documents that it also updates an installed package to latest stable. Avoid suggesting `dotnet new update` as a targeted operation because it updates installed packages generally.

Shared installation is an intentional persistent side effect. If installation succeeds but generation later fails, retain it and say so; project staging cleanup must not uninstall templates or alter unrelated packages.

**Alternatives:** private template storage adds unnecessary lifecycle machinery; automatically choosing latest on every run silently changes an existing installation; a dgtp update command duplicates native tooling.

### 5. Wrapper generation without executing the project

Validate the destination is absent or empty, including hidden entries, and reject linked destinations or linked ancestors that defeat containment. Use an owned sibling staging directory on the same filesystem. Pass validated parameters to the engine, check the creation result and expected primary output, then publish only after rechecking the destination.

Generation must not overwrite/merge existing content or publish outputs outside the selected directory. Validate generated relative paths and link handling; do not assume the engine alone establishes dgtp's destination-safety contract. On handled failure/cancellation, clean only owned staging artifacts and leave pre-existing user content untouched. Unexpected destination changes cause failure rather than overwrite.

Do not execute template post-actions, restore packages for the generated project, build, initialize Git, or authenticate to Dataverse. Engine installation is not permission to run arbitrary template extensions; use the supported built-in generator with the official template. Cancellation must flow through acquisition and generation.

**Alternatives:** direct generation into the target risks partial output; native post-action execution makes initialization unexpectedly run external commands.

### 6. Wrapper output instead of solution integration

Print the template package/version used, whether a first-use installation occurred, and the final project path. Escape user-controlled content for Spectre markup. Print the verified release-build command for the generated project and:

```text
See README.md in the generated project for additional configuration and setup steps.
```

Also show native solution-add guidance using the actual generated `.csproj` path and an explicitly user-supplied placeholder for the existing solution:

```powershell
dotnet sln <existing-solution.slnx> add ".\Contoso.Plugins\Contoso.Plugins.csproj"
```

Do not search for, choose, or modify an existing `.sln`/`.slnx`. Adding to a solution is optional and is not required to build the plugin project. Files belonging entirely to a newly generated template remain inside the target; they do not authorize existing-solution edits.

The generated README explains the SDK/build prerequisites, artifact location, and `dgtp plugin push` with explicit environment-specific solution/publisher values. Initialization embeds no credentials or deployment target and runs none of those steps.

### 7. Verification gates for the two milestones

For native template delivery, explicitly generate and release-build a consumer and inspect physical package contents and registration metadata with dgtp's existing local artifact reader. Verify native name/namespace/output customization, SDK prerequisites, signing, supported target framework, generated README guidance, and no dependency on dgtp for generation or building. These checks are independent of any wrapper.

Before releasing dgtp integration:

1. Select an engine release compatible with .NET 10, existing `NuGet.Packaging`, logging, and the application dependency graph. Prove async generation and filename/namespace/reference replacement without a CLI subprocess.
2. In a disposable user profile, prove native-to-API and API-to-native installation visibility, plus native-update visibility on subsequent dgtp runs. Verify cache behavior and preservation of unrelated installations; a custom host name or settings directory alone does not establish this.
3. Prove post-action suppression, containment, cancellation/cleanup, and consumption of the same published template without dgtp-only transformations.

All checks remain planned, not completed. Normal automated tests use local fixtures/fakes; public NuGet and SDK checks are explicit integration acceptance checks. If native packaging checks fail, fix the template before release. If wrapper checks fail, revisit the integration design rather than blocking a valid native template or silently switching to subprocess execution.

## Risks / Trade-offs

- [The wrapper duplicates some native functionality] -> Keep it resource-specific and limited to initialization; let native .NET tooling own template management.
- [Engine dependencies increase the shipped dependency graph] -> Select and lock a tested release and verify the full CLI build and targeted tests.
- [Shared installation and host-specific caches differ] -> Gate the wrapper release on isolated-profile interoperability checks, not native template publication.
- [An independently updated template becomes incompatible] -> Preserve installed versions, require the parameter/identity contract in template CI, and fail actionably on incompatibility rather than mutating the installation.
- [NuGet is unavailable during ordinary use] -> Attempt a bounded update check and warn; only missing-template installation requires successful acquisition.
- [A build produces a normal library package without runtime DLLs] -> Inspect the actual deployment artifact and verify the existing reader can consume its declared registrations.
- [First installation persists after a project failure] -> Report that side effect accurately; clean project staging, not shared user installations.

## Migration Plan

First coordinate separately authorized native template creation, CI, packaging, and publication. Update native-workflow documentation only once it is accurate and runnable. Existing `plugin push`, its `register` alias, and step commands remain unchanged, and no template-engine dependency or command registration is introduced by this milestone.

Deliver wrapper implementation in a separate PR from webresource scaffolding and complete its additional verification gates before release. Native template validation remains a prerequisite for the full integration acceptance, not a new product-approval gate. No existing project is migrated or updated.

Native template publication is not a dgtp release and does not satisfy this repository's archive gate. Keep the proposal active until a stable release containing the dgtp implementation has been published from `main`.

## Open Questions

- Final repository/package/short names and release ownership must be assigned before external template publication. They do not change the native-first approach or command contract.
- Template dependency versions and the exact generated artifact/build paths will be recorded from the gated packaging check and reflected in README examples.

## References

- [Template engine hosting API](https://github.com/dotnet/sdk/blob/main/documentation/TemplateEngine/api/Inside-the-Template-Engine.md)
- [Native template installation and updates](https://learn.microsoft.com/en-us/dotnet/core/tools/dotnet-new-install)
- [Native solution membership](https://learn.microsoft.com/en-us/dotnet/core/tools/dotnet-sln)
