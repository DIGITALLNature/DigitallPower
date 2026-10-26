# Tasks

All resource dispositions are approved. No additional resource migration is required. Implementation must preserve completed resource-oriented replacements and existing Dataverse data.

## 1. Resource disposition gates

- [x] 1.1 Review bulk-delete import/export together with maintenance execution; verify both designs approve complete removal without replacement, with existing Dataverse jobs unaffected.
- [x] 1.2 Review queue import/export with the user; verify removal of both directions without replacement and rationale are recorded.
- [x] 1.3 Review calendar import/export with the user; verify both asymmetric paths are approved for removal without replacement.
- [x] 1.4 Review SLA configuration import/export with the user; verify removal of both directions without replacement is explicit.
- [x] 1.5 Review routing-rule configuration import/export with the user; verify removal of both directions without replacement is explicit.
- [x] 1.6 Review user-role assignment import/export with the user; verify direct-role/BU synchronization is approved for removal without replacement.
- [x] 1.7 Review Outlook template import/export with the user; verify synchronization-filter transfer is approved for removal without replacement.
- [x] 1.8 Refine the plan to the approved removal of all seven resource pairs; verify every matrix cell is decided and no additional retained-resource migration is needed.

## 2. Approved transfer retirement

- [x] 2.1 Verify both completed template migrations before removing legacy dependencies; verify resource-oriented paths and behavior with focused tests.
- [x] 2.2 Remove all seven approved resource pairs and implementations, coordinating bulk-delete shared artifacts with maintenance retirement; verify all retired paths fail without a Dataverse operation, help/completion omit them, and migrated resource commands remain available.
- [x] 2.3 Document every retired path and unsupported legacy JSON contract in README and `docs/migrations/2.x-to-3.x.md`; verify all seven pairs have explicit no-replacement guidance, existing Dataverse data/jobs remain unchanged, and no exact PAC equivalence is promised.

## 3. Container removal and integration

- [x] 3.1 Remove top-level import/export branches, legacy projects, DI, and proven orphaned base helpers/DTOs/schemas/tests; verify migrated modules no longer depend on them.
- [x] 3.2 Update CLI smoke/settings/completion and old-branch rejection tests while preserving `import secureconfigs` rejection and `plugin step config set`; verify focused CLI tests pass.
- [x] 3.3 Update README project structure and dependency documentation; verify documentation reflects the final graph and all affected lock files are synchronized when dependencies change.
- [x] 3.4 Run affected resource/CLI tests, `dotnet build`, and `pnpm exec openspec validate --all`; verify success and merge the complete secure-config requirement plus transfer delta without losing maintenance changes before archive.
