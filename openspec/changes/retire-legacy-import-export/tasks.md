# Tasks

Review starts independently; final removal is blocked on every disposition and retained migration. Approving this plan is not approval to remove any pending resource.

## 1. Resource disposition gates

- [ ] 1.1 Review bulk-delete import/export together with maintenance execution; verify coordinated approved decisions and a single linked destination where retained.
- [ ] 1.2 Review queue import/export with the user; verify both directions have an approved disposition and rationale.
- [ ] 1.3 Review calendar import/export with the user; verify scope, path asymmetry, and each direction's disposition are recorded.
- [ ] 1.4 Review SLA configuration import/export with the user; verify retained behavior or removal and any replacement are explicit.
- [ ] 1.5 Review routing-rule configuration import/export with the user; verify each direction's approved disposition and destination.
- [ ] 1.6 Review user-role assignment import/export with the user; verify operational use cases and approved behavior are recorded.
- [ ] 1.7 Review Outlook template import/export with the user; verify both directions' approved disposition and rationale.
- [ ] 1.8 Create separately reviewed changes for additional retained resources and refine this plan; verify every matrix cell is decided and every retained migration is linked.

## 2. Approved transfer retirement

- [ ] 2.1 Verify both template migrations and all additional retained migrations are implemented before removing their dependencies; verify replacement paths and behavior with their focused tests.
- [ ] 2.2 Implement approved obsolete-resource removals with negative-path and unaffected-resource tests; verify no undecided direction is silently dropped.
- [ ] 2.3 Document old-to-new commands, intentional format breaks, and no-replacement retirements in README and `docs/migrations/2.x-to-3.x.md`; verify full matrix coverage.

## 3. Container removal and integration

- [ ] 3.1 Remove top-level import/export branches, legacy projects, DI, and proven orphaned base helpers/DTOs/schemas/tests; verify migrated modules no longer depend on them.
- [ ] 3.2 Update CLI smoke/settings/completion and old-branch rejection tests while preserving `import secureconfigs` rejection and `plugin step config set`; verify focused CLI tests pass.
- [ ] 3.3 Update README project structure and dependency documentation; verify documentation reflects the final graph and all affected lock files are synchronized when dependencies change.
- [ ] 3.4 Run affected resource/CLI tests, `dotnet build`, and `pnpm exec openspec validate --all`; verify success and merge the complete secure-config requirement plus transfer delta without losing maintenance changes before archive.
