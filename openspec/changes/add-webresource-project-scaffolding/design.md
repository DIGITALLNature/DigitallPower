# Design

## Context

See `proposal.md` for motivation and scope. The existing `webresource` branch registers only `push`, uses `WebResourceSettings`, and already separates local validation from lazy Dataverse acquisition. Its module targets .NET 10, and its tests live in `tests/dgt.power.webresource.tests/`. CLI wiring and parsing are independently covered by `CommandTreeTests.cs` and `SettingsParsingTests.cs`.

The template is a public, independently maintained TypeScript/Webpack/Biome project. Inspection of commit `4004d36265dc5fc07d03a92e5e0f7978bc2aa731` found:

- The package name and repository identity are in `package.json`; the exposed global is a constant in `webpack.config.js`.
- The lockfile and sample account form/ribbon models support a frontend build without live model generation.
- The local tool manifest pins dgtp 2.2.0. Deployment scripts and Azure Pipelines use the old `dgtp push` syntax, and the pipeline contains environment-specific placeholders.
- No template tags were returned during discovery. The default must work without requiring tags or GitHub Releases.

That inspected commit is evidence of current layout, not a template pin. The proposed download default is the current `main`. The template must first adopt the marker convention and compatible deployment scripts.

The open `DIGITALLNature/webresources-template#13` migration moves the global constant to `rolldown.config.js` while retaining `pnpm run build:prod`. This illustrates why customization must follow markers rather than bundler-specific filenames.

## Goals / Non-Goals

**Goals:**

- Keep template acquisition a runtime operation; ordinary dgtp builds and tests must not fetch another repository.
- Put the feature in the existing webresource module, using small independently testable acquisition, customization, and publication boundaries.
- Treat user-selected revisions as untrusted input and downloaded archives as untrusted data.
- Offer a narrow first-version command rather than a framework for arbitrary project generators.

**Non-Goals:**

- No template cache, offline mode, authenticated private-template downloads, configurable source URL, or template catalog.
- No automatic package-manager discovery or execution.
- No new deployment settings model or JSON schema; existing model-generation configuration remains unchanged.
- No automatic choice of solution, publisher prefix, connection, or CI environment.

## Decisions

### 1. Command shape and proposed first-version defaults

```text
dgtp webresource init <directory> --name contoso-webresources --global Contoso
dgtp webresource init <directory> --name contoso-webresources --global Contoso --dgtp-connection dev --solution ContosoCore --publisher-prefix contoso
dgtp webresource init <directory> --name contoso-webresources --global Contoso --template-version v1.0.0
```

Add dedicated init settings under the existing webresource branch. Require both customization values rather than deriving one from the destination or another identifier. Restrict package names to a conservative, unscoped lowercase npm-compatible subset and globals to single ASCII JavaScript identifiers, excluding reserved words. Dotted namespace expressions are not supported initially.

The essential tier requires only project identity and produces a buildable starter. Accept `--dgtp-connection`, `--solution`, and `--publisher-prefix` as optional, independent local workflow defaults. Supplying all three configures the model-generation and local-deployment scripts, while installation, locally configured credentials, adapting example code and models, and running the scripts remain the developer's responsibility. Apply only explicitly supplied values; never infer them from project identity. Reject explicitly empty or whitespace-only values before acquisition. If any is omitted, replace its marker with `REPLACE_ME` throughout the project; generation and frontend builds still succeed, and users must replace or override the placeholder before using the affected workflow.

`--dgtp-connection` supplies a shared named connection default to both generated scripts; explicit script arguments override it. The name is project configuration and may be shared, but each developer must configure that connection locally. The inherited `--connection` option remains invocation-only and does not populate this default; explain the distinction in help and documentation. Initialization must not resolve, create, select, or verify a connection. Connection strings and credentials must never be copied into output.

No prompting is needed: incomplete input fails equally in interactive and non-interactive use. The command derives from `PowerCommand<TSettings>` and requests telemetry/console and acquisition collaborators, but no `IDataverseConnection`. Constructors perform no I/O. Register it through `CommandTree.Register` so runtime and completion share the metadata.

Alternative: ask interactively for missing values or infer names. Explicit parameters are easier to automate and validate, and avoid silently generating invalid npm or JavaScript names.

### 2. Runtime acquisition from a fixed public repository

Keep the repository identity fixed to `DIGITALLNature/webresources-template`. Without `--template-version`, resolve `main` once to its current commit and download that commit. Do not discover the latest tag/release or maintain a default commit pin inside dgtp.

Keep `--template-version` as an optional convenience accepting an exact tag name or full 40-character commit SHA. User-selected branches other than the implicit default `main`, arbitrary URLs, and abbreviated hashes are not supported. Tags and GitHub Releases are not prerequisites for using or maintaining the template.

Resolve tags through the GitHub tag-ref API, including bounded dereferencing of annotated tags. Fetch the archive using the resolved commit, not a mutable branch or tag name, so one generation run always uses one snapshot. A supplied full SHA can address the archive directly. Encode ref values as URL data, use HTTPS and a product User-Agent, and do not forward Dataverse credentials or stored connection values. Do not require `gh` or Git.

Report the source selection and resolved SHA. Never substitute another revision after a failure. Explain rate limits and connectivity errors without suggesting that Dataverse sign-in would help.

Alternative: pin a tested template commit per dgtp release or require semantic-version tags. Both add publishing/release coupling that is unnecessary when the marker convention is stable. Explicit revision selection and the reported SHA remain available when reproducibility matters.

### 3. Filename-independent marker replacement

Maintain a small marker-to-value set in dgtp:

| Marker | Value |
|---|---|
| `__DGTP_PROJECT_NAME__` | Required `--name` |
| `__DGTP_GLOBAL_NAME__` | Required `--global` |
| `__DGTP_CONNECTION_NAME__` | Optional `--dgtp-connection`, otherwise `REPLACE_ME` |
| `__DGTP_SOLUTION__` | Optional `--solution`, otherwise `REPLACE_ME` |
| `__DGTP_PUBLISHER_PREFIX__` | Optional `--publisher-prefix`, otherwise `REPLACE_ME` |

All five markers must occur in the template even though the last three values are optional. The placeholder is a visible reminder, not a validation guard: scripts may pass it to dgtp if users do not replace or override it. Explicitly empty script arguments retain the template's existing validation. Template revisions lacking the connection marker are incompatible with this convention.

Scan extracted regular UTF-8 text files, including hidden files, rather than locating specific configuration filenames. Leave binary and unsupported-encoding files untouched. Do not execute hooks or introduce a template manifest, bundler adapter, shared project-settings file, or version registry.

Marker validation consists of two checks:

1. Before replacement, every required configured marker occurs somewhere in the scanned text files.
2. After replacement, no reserved `__DGTP_*__` marker remains in those files.

Do not impose exact occurrence counts or require particular file locations. Replace all intended occurrences, including documentation examples. A global marker can move from Webpack to Rolldown without changing dgtp. Ordinary TODOs for manual configuration do not use the reserved prefix and are not validation failures. Report missing or unresolved marker names before publishing a project.

Markers have a stable insertion convention, not arbitrary executable semantics. The template places configuration values in documented string-literal positions; escape replacements for the text format and documented quoting convention, including PowerShell and YAML, rather than interpolating raw command expressions. Input validation and safe insertion are separate from the two marker checks. Prevent recursive marker expansion.

The template owns its build scripts, tool pins, optional CI example, and scaffold-ready onboarding. It must not claim that generated projects belong to the template repository; provenance links and license attribution remain legitimate. dgtp does not patch particular bundler files, rename provider-specific pipelines, rewrite dependency versions, or change lockfiles. The template's README documents the two setup tiers, supplied local workflow defaults, execution-time overrides, local connection prerequisites, model filters, and CI setup. No omitted value falls back to `WebResources`, `sample-app`, or a sample prefix.

Coordinate new or changed reserved markers and their insertion conventions with dgtp support before adopting them on `main`. Ordinary source, tooling, layout, and dependency changes need no dgtp update if the marker convention remains intact. A separately reviewed upstream preparation must first add the markers, correct old commands/options, and provide safe CI/onboarding defaults. Existing version-2 model configuration requires no unrelated redesign.

Alternatives: file-specific replacement couples scaffolding to bundler choices; a shared settings file adds a runtime configuration mechanism solely for generation. Filename-independent markers are sufficient for this feature.

### 4. Stage completely, then publish without merging

```text
Validate arguments and destination
    --> Resolve revision and download bounded archive
    --> Extract into command-owned staging directory
    --> Check required markers, replace, and check for unresolved markers
    --> Recheck destination and publish complete directory
    --> Report next steps
```

Create staging beside the target, on the same filesystem, and strip only the single expected archive root. Validate destination ancestors for reparse points/symlinks and reject unsafe destinations. Before extraction, normalize all entry paths for the destination platform; reject absolute/traversal paths, links, duplicate or colliding paths, and platform-invalid entries. Preserve dotfiles such as the local tool manifest.

Use bounded streaming downloads, cancellation-aware I/O, and finite timeouts. Initial internal limits are 20 MiB compressed, 100 MiB total expanded data, and 10,000 entries; enforce actual streamed sizes as well as archive metadata. Keep archive/extraction failures outside the destination.

For an existing empty destination, remove only that still-empty directory immediately before a same-filesystem move; never remove a directory recursively to make publication succeed. A competing writer causes failure, not a merge or overwrite. On handled failure, clean only command-owned temporary paths and restore an originally empty destination when still safe. Do not delete anything another process has created. A process kill or machine failure may leave staging storage; crash-proof recovery and hostile concurrent filesystem mutation are not promised.

Alternative: extract directly into the target. Staging makes ordinary download, validation, and customization failures recoverable without exposing half-generated projects.

### 5. Separate generation from execution

Success output includes the destination, exact source revision, and commands to enter the project, run `pnpm install --frozen-lockfile`, and run `pnpm run build:prod`. End with an explicit hint: "See README.md in the generated project for additional configuration and setup steps." Identify the generated project's README, not the dgtp repository README. Do not execute any suggested commands. Preserve generated scripts as user-invoked steps. The CI example disables automatic triggers, but a manual `main` run attempts deployment after a successful build; the README warns users to configure and review the target first.

This makes generation possible with only dgtp and network access. The template owns documented frontend runtime prerequisites; the CLI does not have to provision Node.js or pnpm.

Use the existing command result/exit-code behavior. Escape all user-controlled values in console markup. A network failure is a command failure, not an authentication-required result.

### 6. Keep verification independent of live GitHub in ordinary tests

Use injected HTTP behavior and small in-memory archive fixtures for `main`/tag/SHA resolution, acquisition errors, extraction, and customization tests. Exercise real temporary filesystem publication for destination protection and cleanup. Fixtures are test inputs, not a production bundled template.

Extend CLI tests for the new settings, help, required options, template-version parsing, optional local workflow defaults, and shared command registration. Test all supplied and independently omitted defaults, including safely escaped connection names, script overrides and explicitly empty argument validation, the distinction from inherited connection flags, and absence of embedded connection credentials. Assert that success output points to the generated README. Add a no-Dataverse command test and verify no child process is started.

Add fixtures with the same markers in differently named Webpack/Rolldown configuration files and in multiple configuration/documentation locations. Verify those succeed without filename adapters, while missing required markers and unresolved unknown markers fail. Verify ordinary TODOs remain and omitted optional values do not leave markers behind.

For initial feature acceptance and changes to the marker convention, perform a separate real-template check: scaffold representative custom names into a path containing spaces, record the downloaded commit, install with the frozen lockfile, run the production build, and inspect the sample bundles for the requested global. Cover projects with and without local workflow defaults; verify consistent substitution, visible placeholders, script overrides, explicitly empty argument checks without Dataverse access, the success hint, tool-manifest/script compatibility, disabled automatic CI triggers, and the manual-deployment warning. Template maintainers should keep checking their own changes, but dgtp does not approve each template commit or release. Document the acceptance procedure rather than adding network or frontend installation to every .NET build/test.

## Risks / Trade-offs

- [GitHub availability and anonymous API limits] -> Fail clearly, without fallback; direct SHA selection avoids branch/tag lookup. Offline support can be considered separately.
- [Moving main or tags] -> Resolve once per run and report the downloaded SHA. Different invocations can intentionally generate different snapshots; an explicit SHA reproduces a previous selection.
- [Template and dgtp drift] -> Keep marker names/insertion conventions stable, coordinate changes, and fail on missing or unresolved markers. These checks do not prove that the template's frontend build is correct.
- [Initial upstream work is required] -> Obtain a separately authorized marker/script/onboarding update before shipping the command; do not claim the currently incompatible template is ready.
- [Dependency and managed-runtime downloads during frontend setup] -> Do not execute installation during init; retain the lockfile and distinguish generation from build prerequisites.
- [Optional CI needs environment-specific setup] -> Disable automatic triggers and provide precise setup instructions and a manual-deployment warning instead of accepting environment credentials.
- [Finite archive limits exclude unusually large future templates] -> Report the limit and review it when needed.

## Migration Plan

1. Coordinate a separately reviewed template-repository change establishing the marker convention and compatible scripts/onboarding on `main`. Do not modify that repository as an implicit part of applying this change.
2. Implement and verify the dgtp command against fixtures and the prepared real template. Record the acceptance-check SHA without embedding it as a release-specific default.
3. Ship the additive command and user-facing documentation. Existing `webresource push` behavior and existing projects are unchanged.
4. On template regressions, fix or revert the template change in its repository; users can select a previous compatible commit explicitly in the meantime. No generated project is automatically upgraded.
