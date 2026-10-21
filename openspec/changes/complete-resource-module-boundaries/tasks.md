# Tasks

Inventory may begin early. Final cleanup and acceptance depend on all approved resource migrations and both legacy retirement changes. Specs are deliberately skipped for behavior-preserving work.

## 1. Architecture assessment

- [ ] 1.1 Inventory actual source/test projects, solution and host references, and DI registration; verify the design contains a source-backed list distinguishing live modules from leftover directories.
- [ ] 1.2 Map resource logic, DTOs, schemas, and tests to owners and justify capability/infrastructure exceptions; verify every live module has a cohesive responsibility or a recorded residual issue.
- [ ] 1.3 Identify residual dependency and public-API impacts; verify every proposed cleanup has concrete consumers and any behavior/API change is routed to a separate reviewed proposal.

## 2. Dependent cleanup

- [ ] 2.1 Confirm all approved migrations and both retirement changes are complete before final cleanup; verify no pending feature decision or legacy runtime dependency remains.
- [ ] 2.2 Fix confirmed behavior-preserving ownership/reference/DI gaps and remove only proven orphaned artifacts; verify affected focused tests and project builds pass for each consistent slice.
- [ ] 2.3 Update README structure and directly affected architecture documentation, including the existing ADR/index only where needed; verify documentation matches actual ownership without publishing internal implementation notes.

## 3. Final integration

- [ ] 3.1 Recheck the final project graph against the ownership inventory; verify no resource depends on retired verb modules and every exception is justified.
- [ ] 3.2 Run CLI structural/settings tests, affected module tests, and `dotnet build`; verify public behavior remains unchanged by this cleanup.
- [ ] 3.3 Run `pnpm exec openspec validate --all` and check change status; verify specs remain intentionally skipped and no public behavior change was hidden inside the audit.
