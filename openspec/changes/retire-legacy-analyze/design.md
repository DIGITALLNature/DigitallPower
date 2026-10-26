# Design

## Context

See [proposal.md](./proposal.md) for motivation and scope. The current host registered six commands under `analyze`, referenced `dgt.power.analyzer`, and registered `SolutionComponentReader` in dependency injection. Repository-wide consumer checks confirmed that `AnalyzerSummary`, the entity-all-assets configuration model and schema, and the report helpers and DTOs were analyzer-only; these have been removed with the analyzer module.

## Goals / Non-Goals

**Goals:**
- Remove the command surface and its dedicated module consistently from the host, tests, and documentation.
- Keep unrelated commands and solution operations available.
- Make the breaking change and lack of replacement explicit to users.

**Non-Goals:**
- Recreate analysis through `solution lint`, `solution copy-components`, PAC, or another command.
- Delete previously generated local files or change Dataverse records or metadata.

## Decisions

- Retire the six commands and the top-level branch together. Keeping a hidden compatibility alias or a partially supported analyzer module would contradict the requested retirement and complicate CLI completion.
- Remove the analyzer project reference and `SolutionComponentReader` registration from the host, then remove the analyzer project and its test project. Delete common DTOs, reports, fixtures, or helpers only after searching all source, test, and documentation consumers.
- Treat analyzer-specific options, config input, and `Analyze/` output files as unsupported after retirement. Do not add a config or report conversion path; existing local files are left untouched.
- Update CLI structural/smoke coverage to reject the old paths and verify unrelated branches still register. Keep runtime registration and shell completion driven by `CommandTree`.
- Update the README and the 3.x migration guide with the six command paths, lack of replacements, and unchanged Dataverse state. Do not describe unrelated solution commands as equivalent.

## Risks / Trade-offs

- Existing scripts invoke a removed command → document the breaking paths and unknown-command behavior in the migration guide.
- A supposedly analyzer-only type has another consumer → search the full repository before deleting shared artifacts; retain any genuinely shared type and decouple its use.
- Documentation or completion still advertises the branch → update command-tree tests and README references in the same change.

## Migration Plan

Remove registration and analyzer-only dependencies, prune verified orphaned code and tests, then update documentation and CLI coverage. Validate the focused CLI and remaining solution tests, build the solution, and run `pnpm exec openspec validate --all`. Rollback is a source revert; retirement does not alter Dataverse state or delete existing local reports/configuration.
