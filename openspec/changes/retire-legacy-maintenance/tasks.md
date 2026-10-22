# Tasks

All six feature removals are approved. Two deferred Solution feature requests do not block retirement. Preserve process commands and existing Dataverse data/metadata.

## 1. Feature disposition gates

- [x] 1.1 Review bulk-delete execution together with transfer functionality; verify both designs approve complete removal without replacement, with existing Dataverse jobs unaffected.
- [x] 1.2 Review auto-number formatting with the user; verify removal without replacement and rationale are recorded.
- [x] 1.3 Review calculated-field protection with the user; verify standalone removal and a deferred `solution copy-components` best-practice request are recorded.
- [x] 1.4 Review carrier-information export with the user; verify removal without replacement and rationale are recorded.
- [x] 1.5 Review redundant-component removal with the user; verify standalone removal and a deferred Solution request are recorded.
- [x] 1.6 Review PowerFx plugin filtering with the user; verify removal without replacement is recorded.
- [x] 1.7 Capture the two requested Solution ideas as separate proposals and refine this plan; verify all dispositions are approved and neither deferred request is a retirement dependency or promised replacement.

## 2. Approved feature retirement

- [ ] 2.1 Verify the completed `introduce-process-resource` migration before deleting shared dependencies; verify resource-oriented process commands and documented behavior remain available without requiring either deferred Solution request.
- [ ] 2.2 Remove all six approved command registrations, implementations, and proven orphaned settings/helpers/tests, coordinating bulk-delete shared artifacts with transfer retirement; verify every retired path fails without a Dataverse operation, help/completion omit them, and retained resource commands remain available.
- [ ] 2.3 Document all six removals and configuration consequences in README and `docs/migrations/2.x-to-3.x.md`; verify no available replacement is promised, the two Solution requests are deferred-only, and existing Dataverse data/metadata/jobs remain unchanged.

## 3. Container removal and integration

- [ ] 3.1 Remove the maintenance branch, project references, DI, and proven orphaned schemas/models/helpers/tests; verify consumer searches show no required dependency was deleted.
- [ ] 3.2 Update command-tree smoke, completion, and old-branch rejection tests plus README project structure; verify all retained resource commands remain available.
- [ ] 3.3 Run affected resource/CLI tests, `dotnet build`, and `pnpm exec openspec validate --all`; verify success and merge the CLI delta without losing other changes' requirements before archive.
