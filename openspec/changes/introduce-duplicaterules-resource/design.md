# Design

## Context

The current host has no duplicate-rule commands. `src/dgt.power.common/DTO/DuplicateRules.cs` and related DTOs provide historical configuration material; publish/unpublish executors exist under `tests/dgt.power.tests/FakeExecutor/`. They are evidence for investigation, not a complete existing feature to relocate.

## Goals / Non-Goals

**Goals:** Own existing-rule publication and ownership in one resource module; provide explicit preflight and result semantics.

**Non-Goals:** Transfer definitions or conditions, create/delete rules, or couple rule execution to process internals.

## Decisions

- Introduce `dgt.power.duplicaterules` with `duplicaterules init/apply`. Keep resource-specific DTOs and behavior here instead of expanding `common`.
- Apply only listed intent. Publishing, unpublishing, and ownership are distinct requested effects; an already satisfied property needs no write.
- Rule GUID is the sole matching identity. Rules are delivered separately with solution-stable IDs. Names may be shown in diagnostics but never serve as fallback identity. A missing GUID target is a preflight error, not a reason to create a rule.
- Publication state and desired owner are independently optional. State-only intent preserves ownership; owner-only intent preserves the requested rule's original publication state after any platform-required intermediate transitions, whose sequence still needs approval.
- The ownership portion depends on `introduce-portable-owner-resolution` for logical owner keys, environment mappings, and read-only target principal resolution. Regular users, application users, and owning teams are within that resolver's scope, not automatically approved for duplicate-rule assignment.
- Keep resource-specific owner eligibility, assignment execution, publish/unpublish sequencing, and partial-effect reporting in this module. Do not build another owner lookup or make this module depend on the process module.
- Resolve the entire requested set before writing and use the same plan for dry-run. Independent rules can continue after a final runtime failure; aggregate failures remain failures.
- Reuse stable infrastructure only when genuinely shared. Similar verb names do not justify a shared state engine or a dependency on the process assembly.

## Decision Gates Before Implementation

Obtain user approval and refine the contract before coding:

1. Define init selection/filter semantics and capture behavior using GUID identities. GUID-only application identity is decided; do not reopen name matching by default.
2. Finalize the shared contract in `introduce-portable-owner-resolution`, then confirm which resolved owner kinds Dataverse permits for duplicate rules and how init captures logical owner keys. Shared resolver coverage does not settle resource-specific platform constraints.
3. Specify init capture options, JSON/schema layout, CLI options, and empty-selection behavior.
4. Verify publish/unpublish completion semantics, owner-change sequencing, timeout/cancellation, and bounded retry eligibility with SDK/platform evidence.

Do not infer missing production logic from DTOs or silently expand into definition deployment.

### Resume point and platform evidence

The existing-rule-only scope, GUID identity, and independently optional state/owner effects are decided. Resume shared owner-contract design first, then return to the remaining gates above. No final JSON, CLI selection syntax, timeout, retry, or lifecycle-ordering contract has been approved.

Microsoft documents `PublishDuplicateRuleRequest` as submission of an asynchronous publishing job; request acceptance alone is not evidence that publication completed. Existing fake executors transition immediately and therefore do not establish production completion semantics. Microsoft also documents a maximum of five published rules per base table. Evaluate unpublish/publish ordering and capacity interaction before treating all rules as independent runtime work.

Evidence:
- https://learn.microsoft.com/dotnet/api/microsoft.crm.sdk.messages.publishduplicaterulerequest
- https://learn.microsoft.com/power-platform/admin/set-up-duplicate-detection-rules-keep-data-clean

## Risks / Trade-offs

- Historical DTOs imply unsupported functionality -> validate against actual SDK operations and the agreed narrow scope.
- Publication may involve asynchronous completion or platform limits -> explicitly define completion and failure reporting before implementation.
- Rule IDs can be missing and owner selectors ambiguous -> fail preflight instead of name fallback or choosing a candidate.

## Migration Plan

Finalize the shared owner contract first, then add this module, host wiring, schemas, and dedicated tests, reusing fake executors only where accurate. Remove orphaned duplicate-rule configuration only after checking its consumers. Document commands and any intentionally retired public format; there is no registered legacy command to alias. This change is independent of process migration; ownership implementation depends on the shared resolver, not process lifecycle code.
