# Proposal

## Why

Starting a Dataverse webresource project currently requires copying the DIGITALL template and manually replacing project-specific values. A local scaffolding command can make that first step repeatable while leaving the template independently maintained and keeping deployment an explicit later action.

## What Changes

- Add `dgtp webresource init <directory> --name <package-name> --global <identifier>`.
- Offer two local setup levels: required project identity produces a buildable starter; optional `--dgtp-connection`, `--solution`, and `--publisher-prefix` together configure the local model-generation and deployment workflow. Missing values must be provided when the relevant script runs; never retain sample deployment targets.
- Download the project from `DIGITALLNature/webresources-template` when the command runs; do not bundle template files or couple the repositories' builds.
- Download `main` by default, with optional `--template-version <tag-or-commit>` for an explicit revision. Resolve the selection once and report the downloaded commit; do not maintain a per-dgtp-release pin, tag-publishing requirement, or version-to-adapter mapping.
- Customize values through reserved `__DGTP_*__` markers wherever they occur in supported text files. Check required markers exist, replace their occurrences, and reject unresolved markers before publishing. Do not depend on bundler filenames or exact marker occurrence counts.
- Generate into a new or empty directory without overwriting existing project files, executing downloaded code, installing dependencies, creating Git repositories, or contacting Dataverse.
- Report the resolved template revision and actionable build/configuration steps, ending with a hint to read the generated `README.md` for additional setup. Document both setup levels, execution-time script overrides, local connection prerequisites, and separate optional CI setup.
- Prepare and test the template's markers, scripts, and onboarding before shipping the command; the currently inspected template still pins dgtp 2.2.0 and uses legacy deployment syntax.

Plugin scaffolding, arbitrary template repositories or user-selected branches other than the default `main`, automatic updates of generated projects, dependency installation, CI/environment provisioning, and a general-purpose template engine are out of scope.

## Capabilities

### New Capabilities

- `webresource-scaffolding`: Runtime template acquisition, filename-independent marker customization, safe local generation, and onboarding through `webresource init`.

### Modified Capabilities

None. Existing deployment, command execution, and shared command-tree requirements remain unchanged; the new command must conform to them.

## Impact

- Add settings, a command, and focused scaffolding helpers to `src/modules/dgt.power.webresource/`; register the command in `src/dgt.power/CommandTree.cs`.
- Add module tests plus command-tree, help, and settings-parsing coverage in `tests/dgt.power.cli.tests/`.
- Update `docs/reference/commands/webresource.md` and add a user-facing scaffolding guide. Put the contributor-facing real-template acceptance procedure in `docs/development/webresource-scaffolding.md` and link to it from `docs/development/build-and-test.md`.
- Use .NET HTTP, JSON, and archive facilities where sufficient; no new NuGet dependency is planned.
- Coordinate a separately reviewed template-repository update for explicit customization tokens, compatible dgtp scripts/tool versions, and accurate generated-project onboarding. This proposal authorizes no writes to that repository.
- Require GitHub network access at scaffolding time, but no Git, PAC, Node.js, pnpm, or Dataverse credentials merely to generate files.
