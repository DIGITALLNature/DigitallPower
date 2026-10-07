# Plugin core rewrite implementation plan

## Scope and delivery

This plan implements the directions in [the design proposal](plugin-module.md).
It is a sequence of implementation work packages and decision gates, not authorization
to resolve outstanding behavioral choices by assumption.

Use incremental implementation slices with one final CLI cutover. Keep the current
commands operational until the replacement covers the selected capabilities. Temporary
coexistence is an implementation technique, not a permanent legacy/new mode.

Selected scope:

- Replace reconciliation models and lifecycle orchestration; selectively reuse proven code.
- Authoritative reconciliation within an explicitly defined ownership scope.
- Stable step keys, coordinated with `DIGITALLNature/DigitallRegistrationPower`.
- Conservative retry/recovery for unambiguous partially completed upgrades.
- Keep the step configuration command's current behavior. Do not add combined
  secure/unsecure transactions merely for this rewrite; partial updates can be rerun.
- Provisionally require an existing compatible provider data-source table and maintain
  provider records/handler associations, not table metadata.
- Verify that provider cutoff after implementing the provider slice and before final
  cutover. Independent provisioning feasibility is not established.

No new runtime flags, persistent deployment journal, or key-storage mechanism are approved.
No dependency changes are assumed.

## Work packages and dependencies

| Package | Work | Prerequisites | Completion gate |
|---------|------|---------------|-----------------|
| P0 | Behavioral contract and acceptance matrix | None | Ownership, identity scope/adoption, recovery boundaries, omission semantics, and CLI implications agreed |
| P1 | Stable-key feasibility and registration-library change | P0 identity contract | Supported persistence/matching mechanism established; attribute contract and metadata-reading tests available |
| P2 | Replacement core and standalone create/update | P0, P1 | Production use case prepares/applies changes; previews faithful; repeat push is unchanged |
| P3 | Standalone replacement and retry/recovery | P2 | Stable step/config associations retained; phase failures and retries covered |
| P4 | Package deployment | P3 | Package-created IDs resolved; cross-assembly references and package removals safe |
| P5 | Custom APIs, identities, and solution membership | P2; P4 for package acceptance | Owned associations reconciled; conflicts and unsupported outcomes explicit |
| P6 | Provider-record deployment and cutoff verification | P4, P5 ownership/membership policies | Cross-assembly handlers work; independently provisioned table validated or fallback scope agreed |
| P7 | Batch handling and final presentation | P3-P6 | Conflicts detected, deterministic execution, correct confirmation/cancellation/results |
| P8 | CLI cutover, removal, documentation, release verification | P7 and all unresolved gates affecting release | One core remains; command tests/docs current; platform-sensitive behavior verified |

P0 and P1 are investigation/design work, not invitations to build speculative infrastructure.
P5 can proceed after P2 while package integration is completed separately. P6 must not
ship the existing-table requirement on an unverified assumption.

## P0: Establish the behavioral contract

Turn the design's open decisions into a resource-by-resource acceptance matrix.

Specify:

- How a target owns standalone assemblies, package assemblies, types, registrations,
  API associations, and provider operation associations.
- Which absent declarations remove owned resources and which environment fields survive.
- Stable-key scope, required/optional keys, duplicate handling, and unkeyed-step adoption.
- Permitted recovery states, ambiguous states, and cleanup authorization.
- Identity conflict/tenant validation and whether identity selection remains artifact-based.
- Strict or explicitly reported best-effort solution membership.
- Continue/stop behavior for independent batch failures.
- Guarding critical assumptions changed between preview and execution.
- Handling removed package assets and standalone/package ownership transitions.

Do not default to preserving incidental behavior simply because a test currently asserts it.
Map each existing test to retained behavior, deliberate change, or implementation-specific coverage.

Output: agreed contract tables in the proposal and scenario names with expected results.
Finalize any necessary CLI/attribute changes before depending on them.

## P1: Establish stable keys end to end

Bound the investigation to a supported way to persist and retrieve logical registration
identity across redeployments and assembly replacement. Establish required privileges,
solution behavior, naming collisions, and behavior on existing unkeyed steps.
Do not invent unsupported Dataverse fields or conflate a generated display name with identity.

Coordinate the external registration-library work:

- Define the attribute key and validation contract.
- Implement and test it in `DigitallRegistrationPower`.
- Make a released or otherwise agreed consumable dependency available.
- Update plugin metadata parsing, sample plugins, and required package references.
- Synchronize lock files if dependency manifests change.

Gate: demonstrate stable matching when execution order/display name changes and reject
duplicate keys. Prove configuration associations do not move to an arbitrary candidate.
If persistence is infeasible, return to P0 rather than silently substituting name heuristics.

## P2: Build the smallest complete standalone slice

Reuse metadata inspection and correct SDK helpers. Introduce only the models needed for
validated desired state, scoped current state, and intended changes.

Implement:

- Artifact reading and local validation, without internal console/error fallbacks.
- Complete paged snapshot reads.
- Pure matching/diff for types, steps, and images.
- Scoped references for resources created during application.
- One deployment use case with preparation and application entry points.
- Minimal tree presentation and progress from the same changes.
- Same-version standalone creation/content update.

Test the production use case, not a duplicate test pipeline. Establish zero-write dry-run,
unchanged repeat deployment, ambiguity rejection, preserved configuration on updates,
and accurate change detection. Tests exercise replacement components before CLI cutover.

## P3: Add replacement and recovery

Treat old-assembly steps as existing candidates in the same reconciliation algorithm.
Update handler references; do not build and patch a separate migration plan.

Implement one visible phase sequence with cleanup last. Reconcile retries from current state,
including registrations already reassigned by the prior attempt.

Inject failures after artifact creation, type creation, registration updates, association
changes, solution writes, and cleanup. Cover cancellation between writes.
Verify the next preparation either safely resumes or rejects a documented ambiguous state.
Never use cleanup to erase uncertain ownership or infer that all multiple-version states
are failed deployments.

Gate: replacement preserves matching step IDs/configuration associations and retry cannot
create duplicate active registrations or delete still-referenced types.

## P4: Add package deployment

Retain content-based package comparison and supported asset selection.
Resolve server-created assembly/type IDs after upload without silently changing approved intent.
Use assembly-scoped logical type references instead of package-wide type-name identity.

Cover new/updated/unchanged packages, assembly replacement/removal, duplicate type names
across assemblies, and moves between assemblies where supported.
Verify upload side effects and supported request ordering in Dataverse; classify any
server-driven effects that cannot be faithfully predicted.

Gate: package execution uses the same core decisions and cleanup rules as standalone
deployment, with only genuine package-specific resolution behavior.

## P5: Add associations and membership

Implement Custom API linking/unlinking within agreed ownership, identity compatibility
checks, conflict rejection for package declarations, and documented omission semantics.

Ensure requested solution membership without removing unrelated memberships.
Preview unsupported membership explicitly or fail according to the agreed policy.
Keep shared API/identity records separate from owned associations.

Gate: absent declarations and shared dependencies follow the P0 contract; no first-match
or first-assembly selection silently resolves conflicts.

## P6: Provider slice and independent-provisioning verification

Implement provider-record creation/update and operation-handler reconciliation against
an existing compatible data-source table. Keep table validation; do not initially implement
table creation, label updates, attribute updates, or publishing in the replacement core.

Resolve all package handler types before writing providers, and update replacement references
before deleting old types. Apply the agreed operation ownership/omission policy.

After implementing this slice, verify independent provisioning in a non-production environment:

1. Establish a documented way to create the required specialized data-source table independently
   of `plugin push`, including backing-provider configuration and relevant ID-column metadata.
2. Determine whether the approach is supported through solution provisioning, SDK tooling,
   or another practical route, and establish its required privileges.
3. Validate the independently provisioned table using the new module and deploy provider handlers.
4. Exercise representative provider operations and solution transport where relevant.

Do not equate ordinary-table creation with the required specialized configuration.
If no practical supported route exists, retain the provider cutoff as blocked and decide
whether to include a narrowly scoped provisioning helper or defer provider deployment.
Do not quietly restore the old schema lifecycle.

Gate: document a reproducible provisioning procedure or an explicitly agreed fallback
before making existing-table validation the release contract.

## P7: Batch behavior and final presentation

Preflight local directory inputs for overlap/conflicts and order targets deterministically.
Prepare remote state per target after earlier targets have applied; do not execute stale
batch snapshots.

Complete per-target confirmation, dry-run/non-interactive/CI precedence, destructive summaries,
and applied/unchanged/declined/failed batch reporting.
Keep cancellation distinct from a recoverable independent-target failure.
Exercise console behavior through production orchestration.

Gate: directory containing a package and its constituent DLL cannot accidentally perform
competing deployments; failed batches cannot produce success-shaped results.

## P8: Cutover and remove superseded implementation

Wire `CommandTree`/commands to the replacement use case once all selected capabilities work.
Preserve `plugin register` and existing CLI options unless P0 explicitly changes them.
Keep `plugin step config set` intact except for required integration changes.

Remove replaced comparison wrappers, migration patching, competing executors, unused
repository interfaces, and duplicate test harnesses. Do not remove reusable SDK logic merely
because it existed in the old architecture.

Update README, examples, CLI command-tree/settings tests, and relevant migration guidance
for user-facing changes. Update developer memory and registration-library documentation.
Never edit generated CHANGELOG or Qodana baseline manually.

Gate: one deployment core remains and no production legacy/new selection exists.

## Validation strategy

Run the smallest relevant build/test selection for each slice; expand at cutover to plugin
and CLI suites and affected shared tests. Build .NET test projects and execute their built
DLLs with `dotnet exec`, not direct executable invocation.
Use TUnit filtering where helpful and the existing analyzers/tooling.

Live verification requires an agreed non-production environment and representative artifacts.
No environment credentials or live execution are assumed by this plan.

Release verification must cover:

- Supported SDK reassignment/image payloads and package side effects.
- Stable-key persistence/adoption and required permissions.
- Provider provisioning cutoff or fallback.
- Retry/recovery after representative partial failures.
- Complete paging and safe refusal of stale/ambiguous destructive assumptions.
- Zero writes for previews/declines and idempotence after successful deployment.

## Completion definition

The rewrite is complete when the selected CLI capabilities operate through one core,
behavioral decisions are documented, platform-dependent promises are verified, and
superseded execution/model paths are removed.

Success is fewer places that must change together, not an arbitrary file-count target.
No delivery dates or effort estimates are assigned before P0/P1 feasibility gates.
