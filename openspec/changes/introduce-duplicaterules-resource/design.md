# Design

## Context

The current host has no duplicate-rule commands. `src/dgt.power.common/DTO/DuplicateRules.cs` and related DTOs provide historical configuration material; publish/unpublish executors exist under `tests/dgt.power.tests/FakeExecutor/`. They are evidence for investigation, not a complete existing feature to relocate.

## Goals / Non-Goals

**Goals:** Own existing-rule publication and ownership in one resource module; provide explicit preflight and result semantics.

**Non-Goals:** Transfer definitions or conditions, create/delete rules, or couple rule execution to process internals.

## Decisions

- Introduce `dgt.power.duplicaterules` with `duplicaterules init/apply`. Keep resource-specific DTOs and behavior here instead of expanding `common`.
- Apply only listed intent. Publishing, unpublishing, and ownership are distinct requested effects; an already satisfied property needs no write.
- Resolve the entire requested set before writing and use the same plan for dry-run. Independent rules can continue after a final runtime failure; aggregate failures remain failures.
- Reuse stable infrastructure only when genuinely shared. Similar verb names do not justify a shared state engine or a dependency on the process assembly.

## Decision Gates Before Implementation

Obtain user approval and refine the contract before coding:

1. Determine unique rule identity, selection/filter semantics, cross-environment requirements, and duplicate-name handling.
2. Confirm supported owner identity kinds and whether logical mappings should follow the process contract. Process owner decisions do not automatically settle this resource's platform constraints.
3. Specify init capture options, JSON/schema layout, CLI options, and empty-selection behavior.
4. Verify publish/unpublish completion semantics, owner-change sequencing, timeout/cancellation, and bounded retry eligibility with SDK/platform evidence.

Do not infer missing production logic from DTOs or silently expand into definition deployment.

## Risks / Trade-offs

- Historical DTOs imply unsupported functionality -> validate against actual SDK operations and the agreed narrow scope.
- Publication may involve asynchronous completion or platform limits -> explicitly define completion and failure reporting before implementation.
- Rule and owner names are ambiguous -> fail preflight instead of choosing a candidate.

## Migration Plan

Add the module, host wiring, schemas, and dedicated tests, reusing fake executors where accurate. Remove orphaned duplicate-rule configuration only after checking its consumers. Document commands and any intentionally retired public format; there is no registered legacy command to alias. This change is independent of process migration.
