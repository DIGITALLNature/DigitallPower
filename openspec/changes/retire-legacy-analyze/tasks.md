# Tasks

## 1. Retire the CLI surface

- [x] 1.1 Remove the `analyze` branch and all six command registrations; update command-tree smoke and rejection tests to verify the old paths fail without Dataverse work and unrelated branches remain available.
- [x] 1.2 Verify help and shell-completion metadata omit `analyze`, and run the focused CLI test project.

## 2. Remove analyzer-only implementation

- [x] 2.1 Remove the analyzer project and test project, their host and solution references, and the `SolutionComponentReader` DI registration; verify no project or runtime registration references remain.
- [x] 2.2 Search all source, tests, and documentation before deleting analyzer-only DTOs, report types, helpers, fixtures, and generated artifacts; retain any type with a remaining consumer and verify the consumer search is clean.

## 3. Document the breaking change

- [x] 3.1 Remove the `analyze` feature, command-reference, and repository-layout entries from the README; verify no active command documentation still advertises the retired paths.
- [x] 3.2 Add all six retired paths, the lack of replacements or file migration, and the unchanged Dataverse state to `docs/migrations/2.x-to-3.x.md`; synchronize `openspec/specs/cli-command-surface/spec.md` and verify migration guidance does not imply equivalence with other commands.

## 4. Integration validation

- [x] 4.1 Run affected CLI and solution tests, `dotnet build`, and `pnpm exec openspec validate --all`; verify all pass and the retired branch is absent from the built CLI.
