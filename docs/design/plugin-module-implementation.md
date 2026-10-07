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
- Composite step matching without persisted keys or required registration-library changes
  in the first rewrite. Revisit generated/persisted identities later.
- Conservative retry/recovery for unambiguous partially completed upgrades.
- Keep the step configuration command's current behavior. Do not add combined
  secure/unsecure transactions merely for this rewrite; partial updates can be rerun.
- Provisionally require an existing compatible provider data-source table and maintain
  provider records/handler associations, not table metadata.
- Verify that provider cutoff after implementing the provider slice and before final
  cutover. Independent provisioning feasibility is not established.

No new runtime flags, persistent deployment journal, or key-storage mechanism are approved.
No dependency changes are assumed.
Managed identity provisioning/binding is excluded from push. Do not parse the removed
attribute or add a compatibility/detection path. Preserve bindings on existing target records,
but do not transfer them to new replacement assemblies.

## Work packages and dependencies

| Package | Work | Prerequisites | Completion gate |
|---------|------|---------------|-----------------|
| P0 | Behavioral contract and acceptance matrix | None | Ownership, identity scope/adoption, recovery boundaries, omission semantics, and CLI implications agreed |
| P1 | Composite matching and ambiguity safeguards | P0 identity contract | Duplicate declarations rejected; unambiguous existing steps reused; tuple changes do not preserve identity |
| P2 | Replacement core and standalone create/update | P0, P1 | Production use case prepares/applies changes; previews faithful; repeat push is unchanged |
| P3 | Standalone replacement and retry/recovery | P2 | Stable step/config associations retained; phase failures and retries covered |
| P4 | Package deployment | P3 | Package-created IDs resolved; cross-assembly references and package removals safe |
| P5 | Custom APIs and solution membership | P2; P4 for package acceptance | Owned API associations reconciled; membership errors explicit; identity bindings remain environment-owned |
| P6 | Provider-record deployment and cutoff verification | P4, P5 ownership/membership policies | Cross-assembly handlers work; independently provisioned table validated or fallback scope agreed |
| P7 | Batch handling and final presentation | P3-P6 | Conflicts detected, deterministic execution, correct confirmation/cancellation/results |
| P8 | CLI cutover, removal, documentation, release verification | P7 and all unresolved gates affecting release | One core remains; command tests/docs current; platform-sensitive behavior verified |

P0 is contract work; P1 is a bounded matching primitive, not speculative identity infrastructure.
P5 can proceed after P2 while package integration is completed separately. P6 must not
ship the existing-table requirement on an unverified assumption.

## P0: Establish the behavioral contract

Turn the design's open decisions into a resource-by-resource acceptance matrix.

Specify:

- How a target owns standalone assemblies, package assemblies, types, registrations,
  API associations, and provider operation associations.
- Shared-resource omission/removal policies; step configuration, enabled state, and
  impersonation are already selected as preserved environment state.
- Scoped tuple matching, duplicate handling, and unambiguous existing-step reuse.
- Permitted recovery states, ambiguous states, and cleanup authorization.
- Identity bindings remain environment-provisioned outside push; no copying on replacement.
- Verify/document the solution-aware component matrix; membership errors fail deployment.
- Continue independent batch targets after failure, return overall failure, and stop on cancellation.
- Guarding critical assumptions changed between preview and execution.
- Handling removed package assets and standalone/package ownership transitions.

Do not default to preserving incidental behavior simply because a test currently asserts it.
Map each existing test to retained behavior, deliberate change, or implementation-specific coverage.

Output: agreed contract tables in the proposal and scenario names with expected results.
Finalize any necessary CLI/attribute changes before depending on them.

## P1: Composite matching and ambiguity safeguards

Implement matching scoped to one plugin type, using message, stage, mode, primary entity,
and secondary entity. Normalize casing/unset entities. Exclude name, execution order,
attributes, images, and assembly version.

Reject duplicate local tuples and multiple matching remote candidates. Unrelated remote
tuples do not prevent a unique match. Changing a tuple field means a new registration.
The caller supplies candidates scoped to the desired type, including applicable superseded
assemblies; recovery candidate selection is P3 work, not a first-match heuristic.

Use `Reconciliation/PluginStepMatcher` as the first primitive for the replacement core.
Keep it independent from the currently wired planner until the core is ready for cutover.

Gate: unchanged tuples retain existing IDs across ordinary registration-content changes;
duplicate/ambiguous tuples fail with actionable diagnostics. No Name/Description markers,
mapping table, or registration-library dependency update is required.

Persisted identity and external registration-library changes are deferred. If revisited,
establish supported storage and adoption semantics before adding them.

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

The replacement entry point is `Reconciliation/PluginDeployment`; pure registration
decisions are held in flat `RegistrationChanges` collections and displayed by
`Output/DeploymentRenderer`. The same lifecycle owner handles standalone and package
deployment; package-specific behavior is limited to upload and resolution of server-owned
assembly/type records. This core is not wired to commands until P8.
Cross-resource features not yet implemented are explicitly rejected instead of ignored.
Platform-only stages remain outside declarative step cleanup and block type/assembly deletion
until their lifecycle is verified.

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

Selected recovery boundary: at most two same-name standalone records, exactly one in the
desired major/minor train, with target content matching the requested artifact. The hash
check is a provisional conservative guard, not proof of a failed push. More/duplicate trains,
content mismatch, and ambiguous step candidates fail. Provider dependencies
and platform-only steps block cleanup until their lifecycles are supported and verified.
Custom API associations are reassigned or unlinked before cleanup by the shared P5 slice.

## P4: Add package deployment

Retain content-based package comparison and supported asset selection.
Resolve server-created assembly/type IDs after upload without silently changing approved intent.
Use assembly-scoped logical type references instead of package-wide type-name identity.

The current slice covers new, updated, and unchanged packages. It requires the exact expected
server-owned assembly/type set after upload and never falls back to standalone assembly creation.
Duplicate type names in different assemblies remain assembly-scoped. Package version changes,
assembly/type removals and moves between assemblies fail before
upload until their behavior is supported. Step/image removals are also rejected pending
verification that package upload leaves those records available for explicit cleanup.
`RetrieveDependenciesForDelete` is a candidate for
future component-deletion checks, not yet validated as a complete package-update safeguard.

The test fake models the documented server ownership of package assemblies/types; it does not
prove live Dataverse upload behavior or failure timing. Verify upload side effects and request
ordering in Dataverse, especially for removals.

Package acceptance also exercises additive assembly/type uploads, retries after package/step/
image writes, upload failure before a write, cancellation between registration writes, and
rejection of changed package content after preparation. Resolve and validate every package
assembly/type before applying any registrations, including exact assembly versions and retained
IDs. Fresh preparation must reuse completed writes and avoid uploading unchanged content.

`DeploymentRenderer` renders standalone and package trees from those prepared decisions using
shared registration presentation. Package assemblies are labeled as server-managed during upload,
not as direct assembly writes. New types are labeled as created via package upload. Existing
type IDs and unchanged registrations remain visible; server-managed content changes are not
represented as independently approved per-assembly updates.

Gate: package execution uses the same core decisions and cleanup rules as standalone
deployment, with only genuine package-specific resolution behavior.

### Outstanding gates before package cutover

The guarded package subset does not complete P4. Keep these requirements explicit:

- Verify package replacement/removal and cross-assembly type-move semantics in Dataverse;
  dependency inspection alone does not prove upload ordering or side effects.
- Wire the prepared package tree into production confirmation/dry-run during P7/P8; the
  replacement renderer is currently exercised directly, not through the CLI.
- Concurrent deployment or registration editing against the same target is unsupported.
  Do not add full registration snapshot re-reads as a substitute for locking. Retain
  artifact-state checks, post-upload assembly/type resolution, and targeted dependency
  checks before destructive cleanup. Any stronger concurrency strategy requires a separate
  platform-supported design.
- Exercise the association, provider, and solution failure boundaries when P5/P6 add those
  phases; existing retry tests do not cover unimplemented phases.
- Harden/reuse artifact reading at final integration so local errors cannot become silent
  defaults or console-only fallbacks.

## P5: Add associations and membership

Implement Custom API linking/unlinking within agreed ownership, conflict rejection for
duplicate declarations, and documented omission semantics. Managed identity provisioning
and binding remain outside push in both current and replacement pipelines.

The Custom API slice uses shared flat `CustomApiChange` decisions for standalone/package
deployment. Explicit declarations authorize handler reassignment, including from outside
the target; omission unlinks only APIs currently handled by target-owned types. API records
are external prerequisites and never created/deleted. Duplicate declarations across a target
and missing/ambiguous API records fail during preparation. Links resolve against assembly-scoped
type IDs and execute before registration/type cleanup. Package-wide omission calculation avoids
unlinking an API that moves to another assembly in the same package.
The preview shows link/reassign/unlink/unchanged associations and counts handler reassignment
and unlinking as destructive changes. Retry reconciles completed association writes.
Solution membership is still pending; this does not complete P5 or wire the CLI.

Ensure requested solution membership without removing unrelated memberships.
Preview unsupported membership explicitly or fail according to the agreed policy.
Keep shared API records separate from owned associations and identity records outside push.

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
- Composite matching, duplicate rejection, and safe reuse of existing step IDs.
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
