# Tasks

## 1. Remove the legacy path and document migration

- [x] 1.1 Add regression tests in `tests/dgt.power.cli.tests/CommandTreeTests.cs` proving `import secureconfigs` is rejected before command execution, import help no longer lists it, other import paths remain available, and replacement help succeeds; verify the retirement assertions fail against the old registration.
- [x] 1.2 Remove the `SecureConfigImport` registration from `src/dgt.power/CommandTree.cs`, delete `src/modules/dgt.power.import/Logic/SecureConfigImport.cs`, `src/dgt.power.common/DTO/SecureConfig.cs`, and `tests/dgt.power.import.tests/SecureConfigImportTests.cs`; verify no live source/test references remain and the CLI retirement assertions pass. Preserve the generated Dataverse entity and unrelated imports.
- [x] 1.3 Remove the legacy command from README and document migration in `docs/migrations/2.x-to-3.x.md`: use the actual `plugin_step` / `secure_config` fields, explicit step selection, raw-text secure files, composite-key and inline alternatives, empty versus omitted values, and secret-handling cautions; verify examples match existing settings and retain the README migration link.
- [x] 1.4 Remove the resolved secure-import backlog section from `todo.md`; verify unrelated entries, generated changelog, archives, and Qodana baseline remain unchanged. Confirm no legacy schema exists rather than deleting an unrelated schema.

## 2. Preserve replacement behavior

- [x] 2.1 Extend `PluginStepConfigSetCommandTests.cs` only where coverage is missing for secure-file raw-text input, secure create/update, empty versus omitted values, and preservation of unsecure configuration; verify focused tests pass alongside existing composite-key, missing-match, ambiguity, and settings-validation coverage without changing production plugin behavior.
- [x] 2.2 Document the existing configuration command in the main `plugin-step-management` purpose when synchronizing the delta; verify the spec distinguishes configuration selectors from state-only solution selection, dry-run, and batch behavior.

## 3. Integration verification and specification synchronization

- [x] 3.1 Run `rtk dotnet build` with zero errors, then run the built CLI, import, and plugin test DLLs through `rtk dotnet exec`; verify all three affected suites pass without invoking their `.exe` files directly.
- [x] 3.2 Synchronize both capability deltas into the main specs using the OpenSpec sync workflow and run `rtk pnpm exec openspec validate --all`; verify the implemented CLI and replacement contracts are represented and validation succeeds.
- [x] 3.3 Review the final diff for removal-only scope, complete migration guidance, and absence of real secret values; verify no dependency changes, compatibility aliases, or unrelated legacy retirements were introduced.

## Workflow follow-up

- Archive the completed change after review and acceptance using the OpenSpec archive workflow.
- Refresh generated Copilot instructions with `rtk pnpm run openspec:update` and review generated changes.
- Ship as a major-version breaking change; use a Conventional Commit breaking marker when committing implementation.
