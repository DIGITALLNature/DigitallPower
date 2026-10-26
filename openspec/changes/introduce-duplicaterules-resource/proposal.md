# Proposal

## Why

Duplicate-rule DTOs and publish/unpublish test executors exist, but the current CLI has no productive duplicate-rule command. A dedicated resource should manage existing rules without mixing their lifecycle with process behavior.

## What Changes

- Introduce a `dgt.power.duplicaterules` module with resource-oriented `init` and `apply` operations for existing rules.
- Capture and apply publish/unpublish state and ownership; specify independent optional properties without implicit changes to omitted rules.
- Provide preflight validation, dry-run, and explicit partial-failure reporting.
- Use a strict V1 configuration with GUID identity and independently optional publication and logical-owner intent.
- Treat a valid empty apply as a successful no-op without Dataverse access, consistent with the command-execution contract.
- Capture state, ownership, or both; select existing rules by base table and/or explicit GUIDs.
- Validate publish capacity, complete explicit unpublishes first, then assign owners and submit publishes sequentially. Observe submitted jobs together rather than waiting separately for each rule.
- Confirm final publish states by default with a five-minute shared budget; allow explicit submission-only publishing with `--no-wait`. Unpublish is synchronous and confirmed by immediate read-back.
- Identify rules by GUID only, relying on solution-stable IDs; do not fall back to name matching.
- Consume the shared owner-resolution contract from `introduce-portable-owner-resolution`; keep rule-specific owner admissibility and assignment sequencing here.
- Preserve publication state during owner-only changes without implicit unpublish/republish; verify platform support before implementation.
- Let shared owner capture skip unmapped SYSTEM/INTEGRATION platform principals, which own the standard rules, and add an opt-in `--unmapped-owners skip` to `duplicaterules init` and `process init`. Skipped records are written without `owner` (preserving the target owner on apply) and reported as warnings; ambiguity and other unmapped owners still fail by default.
- Out of scope: creating rules, transporting definitions or conditions, and deleting rules.

## Capabilities

### New Capabilities

- `duplicate-rule-management`: Capture and application of existing duplicate-rule state and ownership.

### Modified Capabilities

- `owner-resolution`: Capture reports platform principals, and optionally unmapped owners, as typed skips instead of failing.
- `process-management`: `process init` gains `--unmapped-owners` and writes skipped owners without `owner`.

## Impact

Touches host registration and DI, duplicate-rule DTO ownership, `schemas/duplicaterules/v1/schema.json`, module and CLI tests, and publish/unpublish test helpers. README documents configuration, capture, owner policy, job submission versus completion, and partial effects. Independent of process implementation; ownership depends on `introduce-portable-owner-resolution`, not on a generic state engine. No existing registered duplicate-rule command requires a compatibility alias.
