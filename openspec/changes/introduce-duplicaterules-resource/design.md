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
- Publication state and desired owner are independently optional. State-only intent preserves ownership; owner-only intent preserves the original stable publication state through direct assignment, without implicit unpublish/republish.
- The ownership portion depends on `introduce-portable-owner-resolution` for logical owner keys, environment mappings, and read-only target principal resolution. Regular users, application users, and owning teams are within that resolver's scope, not automatically approved for duplicate-rule assignment.
- Keep resource-specific owner eligibility, assignment execution, publish/unpublish sequencing, and partial-effect reporting in this module. Do not build another owner lookup or make this module depend on the process module.
- Resolve the entire requested set before writing and use the same plan for dry-run. Independent rules can continue after a final runtime failure; aggregate failures remain failures.
- Reuse stable infrastructure only when genuinely shared. Similar verb names do not justify a shared state engine or a dependency on the process assembly.

### Configuration and CLI

```json
{
  "schemaVersion": 1,
  "rules": [
    {
      "ruleId": "11111111-1111-1111-1111-111111111111",
      "published": true,
      "owner": "salesOwner"
    }
  ]
}
```

Require `schemaVersion: 1` and a `rules` array. Each entry requires a nonempty `ruleId` and at least one of boolean `published` or logical-key string `owner`. Missing properties preserve their respective target values; explicit null is invalid. Reject duplicate IDs, duplicate JSON properties, unknown fields, unsupported versions, and blank/whitespace-padded owner keys. Keys follow the shared ordinal case-sensitive contract. Permit optional root `$schema` URI for editor tooling. Do not accept definitions, conditions, names as matching inputs, or implicit default owners.

Publish schema at `schemas/duplicaterules/v1/schema.json`; keep runtime duplicate-property checks as well as schema/model validation. A valid empty array is capturable and applies as a successful no-op: report zero rules and no changes, without authentication or target lookups. An explicitly supplied owner mapping is still loaded and structurally validated locally.

```text
duplicaterules init <File> [--capture state|owner|both]
    [--entity <BaseTable>] [--rule-id <Guid> ...]
    [--owner-mapping <File>] [--overwrite]

duplicaterules apply <File> [--owner-mapping <File>]
    [--dry-run] [--no-wait] [--timeout <Seconds>]
```

`--rule-id` is repeatable, each occurrence supplies one GUID. `--entity` is one exact base-table logical name, not the matching table. GUID selection is a set; combine the optional ID set and entity filter with AND. Paths are explicit, working-directory-relative when not absolute, with no default-file search. Shared connection/telemetry options remain available.

Default `--capture` is `state`. Capture `owner`/`both` requires the source mapping; state-only does not. Always structurally validate an explicitly supplied mapping, including when no ownership effect is requested. Apply resolves only desired owner keys; capture fully resolves its source mapping and requires one logical key per captured owner, following the shared contract.

### Capture

Retrieve the entire selected set with paging. Explicit missing IDs and IDs conflicting with the entity filter are errors, not silent exclusions. Duplicate requested IDs collapse to the selection set; duplicate configuration entries remain invalid. Reject selected rules in Publishing or unknown/inconsistent state before writing output, even for owner-only capture; do not capture a guessed boolean or adopt a foreign job.

Produce GUID entries with exactly the requested captured properties, deterministically ordered by GUID. Zero matches is successful capture of an empty rules array. Resolve all capture/owner mappings before output creation or replacement. Existing output fails unless `--overwrite`; never replace it after a selection or reverse-mapping error. Local write failure is an error, not a rollback guarantee. Do not invent owner keys or export raw owner IDs as substitutes.

### Ownership policy and sequencing

Use the common resolver in `dgt.power.common/Ownership` for regular users, application users, and owning teams. For an actual owner change, reject disabled users during resource preflight. An already satisfied owner request or capture may reference a disabled user; state-only changes do not force owner revalidation or reassignment.

Assign directly and preserve stable publication state. For an explicit unpublish plus owner change, confirm unpublish before assignment. For an owner change plus publish, complete assignment before submitting publish. Owner-only changes perform no publish/unpublish. If assignment fails, skip the rule's dependent publish; other independent work can continue. Do not silently unpublish/republish, impersonate, provision principals, grant roles, or fall back to another identity.

Verify direct assignment for all promised principal kinds and preservation of Published/Unpublished as a technical evidence gate. Permission checks are not a complete proof of server-side assignment success. Report actual failures and partial changes; do not promise rollback.

### Preflight and capacity

Validate config, options, and supplied mappings locally before connection acquisition. Reject nonpositive/noninteger timeout values locally. After local validation, empty apply succeeds without connecting; missing files, missing rules arrays, and invalid entries remain errors. For nonempty input, resolve every configured rule by GUID and every desired owner and required target fact before any writes. A listed rule already Publishing, or with unknown/inconsistent state, is a preflight error. Dry-run uses the same preparation and reports operations, phase ordering, owner changes, occupied/reserved slots, and wait/submission-only mode with no writes.

For each base table receiving a new publish request, retrieve all existing Published and Publishing rules, including unlisted rules, with complete paging. Count both states as occupied/reserved. Calculate projected occupancy by subtracting only explicitly requested unpublishes of occupied listed rules and adding newly requested publishes; do not double-count already published rules. Reject a projected occupancy above five before any writes. No unlisted rule is changed to free capacity. Unrelated tables with no requested publishes do not acquire a new global capacity policy.

Preflight is not a lock or authorization guarantee. Re-read relevant capacity before publish submission, account for accepted own requests not yet visible as Publishing, and avoid counting those requests twice. Failed or unconfirmed unpublish does not release a slot. If a slot is unavailable, report that publish as failed/skipped; do not queue it indefinitely or unpublish another rule. Concurrent server changes can still cause request failure.

### Execution phases

Use straightforward module-local preparation and sequential request submission, not a generic planner/repository/executor hierarchy or dependency scheduler. Use prepared identities and desired effects; read back state where completion/capacity requires it.

1. Submit all needed explicit unpublish requests sequentially, continuing independent submissions after an item failure.
2. Observe pending unpublishes together until confirmed Unpublished, failed, or timed out. Keep slots reserved until confirmation. Failed rules skip dependent ownership/publish work.
3. Process remaining prepared entries in manifest order: assign changed owners, then submit needed publishes sequentially without waiting for each publish to finish. Already satisfied effects produce no writes. Continue independent work after item failures.
4. In normal mode, observe all successfully submitted publishes together. In `--no-wait`, report accepted submissions and job IDs without observing their completion.

An unpublish/assignment error in one rule does not suppress all work for the same base table; use actual available capacity to decide which other publishes are possible. Preserve deterministic submission order without configurable priorities or graph scheduling.

### Completion, timeout, and cancellation

Default `--timeout` is 300 positive integer seconds per observation phase, not per rule or for the whole run. Each phase starts its common deadline after that phase's request-submission loop finishes. Unpublish and publish may each consume the full budget; request submission and owner assignment add runtime. No per-rule timeout reset. Poll pending rules together on an internal two-second cadence without a poll-interval option; implementations may query sets of IDs rather than spawn a worker per rule.

Publish responses expose `JobId`. Retain returned IDs, observe attributable terminal job failures where available, and confirm final rule state. Do not infer success solely from an accepted response or guess terminal failure from one transient Unpublished observation. Verify the real job/status behavior in the evidence gate. Report rule GUID, phase, job ID when returned, completed effects, last observed state, and failure/timeout. Unpublish correlation and read-back must also be grounded in actual response/platform behavior; do not invent a JobId.

`--no-wait` skips only final publish observation. It still confirms explicit unpublishes and completes owner assignments. Accepted publishes count as successful submissions, not proof of Published; server failures after return are not detectable by that run. Any observed item error still fails the command. `--timeout` remains relevant for unpublishes in this mode. `--dry-run --no-wait` is valid and reports intended submission-only semantics without writes.

On observation timeout, mark unresolved entries as unconfirmed failures, not as completed or rolled back. On cancellation, stop further client work and propagate cancellation rather than continuing best-effort. Already submitted jobs may continue server-side after timeout or cancellation; never promise cancellation of those jobs. Apply adds no automatic mutation retries or job resubmissions; existing SDK transport behavior is not reimplemented.

## Technical Evidence Gate

Product decisions are approved; implementation still requires a separate apply request. Within authorized implementation, evidence tasks run before their dependent ownership/lifecycle production code, not before independent model/schema/capture work. Verify direct assignment to regular users, application users, and owning teams for both stable rule states, including state read-back. Verify actual Publish/Unpublish acceptance, transitional states, final states, job correlation, terminal errors, and capacity behavior. Use focused test doubles for regressions, but do not treat the existing instantaneous fake transitions as server proof.

Live writes require separate explicit approval. If direct assignment cannot preserve state or a promised kind is unsupported, stop for design review; do not add hidden unpublish/republish or identity fallbacks.

Platform evidence:
- https://learn.microsoft.com/en-us/power-apps/developer/data-platform/duplicaterule-entities
- https://learn.microsoft.com/en-us/dotnet/api/microsoft.crm.sdk.messages.publishduplicateruleresponse.jobid
- https://learn.microsoft.com/en-us/dotnet/api/microsoft.crm.sdk.messages.unpublishduplicaterulerequest

## Open Questions

No unresolved product decisions. Platform-specific behavior above remains subject to the technical evidence gate, not implementation guesswork.

## Risks / Trade-offs

- Historical DTOs imply unsupported functionality -> validate against actual SDK operations and the agreed narrow scope.
- Publication and unpublication can outlive the client -> bounded shared observation phases, job diagnostics, and explicit unconfirmed/submitted outcomes.
- Five-slot capacity couples rules within a base table -> preflight projection, confirmed release only, runtime capacity checks, and no automatic eviction.
- Assignment may have platform-specific constraints -> direct-assignment evidence gate and no implicit lifecycle fallback.
- Rule IDs can be missing and owner selectors ambiguous -> fail preflight instead of name fallback or choosing a candidate.

## Migration Plan

Implement the shared owner resolver before dependent ownership/capture integration. Add this module, schema, host wiring, and dedicated tests, reusing or extending fake executors only where accurate. Remove orphaned duplicate-rule configuration only after checking its consumers; do not remove generated Dataverse models or broadly migrate other Assignee consumers. Document commands, strict V1 format, capture, limits, submission versus completion, and partial effects in README. There is no registered legacy command to alias. This change is independent of process migration and does not alter the shared owner contract.
