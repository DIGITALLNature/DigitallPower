# Plugin module redesign proposal

## Status and purpose

This is a design proposal, not an implementation specification or a description of
shipped behavior. The plugin module is pre-release; existing internal APIs and behavioral
restrictions are not compatibility constraints.

The starting feature baseline is the current CLI surface. The objective is to preserve
useful capabilities while reducing the number of representations, coordination points,
and implicit policies needed to implement them.

Core directions:

- Rewrite the deployment core while selectively retaining or adapting proven implementation
  pieces; do not rewrite the whole module or preserve the existing architecture by default.
- Reconcile registrations authoritatively within a defined ownership scope.
- Use conservative composite step matching in the first rewrite. Persisted/generated
  keys and registration-library changes are deferred for reconsideration.

Other recommendations below are proposals unless explicitly identified as selected.
Open decisions are collected at the end.
The phased [implementation plan](plugin-module-implementation.md) records delivery
dependencies, acceptance gates, and subsequent scope selections.

## Contents

- [Assessment of the current implementation](#assessment-of-the-current-implementation)
- [Goals and non-goals](#goals-and-non-goals)
- [CLI feature baseline](#cli-feature-baseline)
- [Architecture options](#architecture-options)
- [Recommended architecture](#recommended-architecture)
- [Change set and preview](#change-set-and-preview)
- [Registration identity and reconciliation](#registration-identity-and-reconciliation)
- [Ownership and removal](#ownership-and-removal)
- [Deployment lifecycle and recovery](#deployment-lifecycle-and-recovery)
- [Resource-specific policies](#resource-specific-policies)
- [Directory deployments](#directory-deployments)
- [Step configuration](#step-configuration)
- [Errors, cancellation, and operational safety](#errors-cancellation-and-operational-safety)
- [Testing and acceptance criteria](#testing-and-acceptance-criteria)
- [Implementation approach](#implementation-approach)
- [Open decisions](#open-decisions)
- [Current implementation references](#current-implementation-references)

## Assessment of the current implementation

The issue is fragmented ownership of behavior, not file count alone. The reviewed module
has 99 source files and approximately 4,450 nonblank source lines, excluding generated
code. Its capabilities justify some complexity, but the current structure adds avoidable
coordination overhead.

| Finding | Consequence |
|---------|-------------|
| Renderer, executors, and `HasChanges` independently interpret nested plan state | A new write must be wired into several consumers; omissions can affect confirmation |
| Multiple executors expose complete or partial deployment pipelines | Correct sequencing depends on callers knowing cross-resource lifecycle rules |
| Upgrade planning patches previously constructed registration plans | Ordinary and migrated steps have duplicated reconciliation paths |
| Planner combines remote reads, matching, validation, caching, and migration preparation | Feature changes involve a broad dependency bundle and several responsibilities |
| Some comparison/deployment wrappers add little semantic value | More navigation and invalid boolean/nullable combinations without stronger guarantees |
| Tests reproduce command orchestration in test harnesses | Component coverage does not establish production dry-run/confirmation wiring |
| Failed upgrades can retain two standalone assemblies that subsequent pushes reject | Dependency preservation does not provide usable retry/recovery |

Useful boundaries should remain: local inspection versus remote access, reconciliation
versus writes, pure matching, and dependency-safe cleanup.

## Goals and non-goals

### Goals

- Give every deployment decision and lifecycle rule one obvious implementation owner.
- Retain useful previews, dry-run, confirmation, and pre-write validation.
- Preserve existing step identities and environment configuration for matching registrations.
- Make ownership, omission, removal, and ambiguity explicit.
- Support deterministic deployment and a deliberate partial-failure recovery policy.
- Exercise production orchestration in scenario tests.
- Keep the implementation small enough to follow without a deployment framework.

### Non-goals

- A generic repository, mediator, workflow, or dependency-graph framework.
- A class with executable behavior for every individual deployment action.
- Persisted or replayable execution plans.
- A transaction spanning artifact upload, metadata provisioning, and all registration writes.
- Workflow activity deployment.
- Deployment of provider instance records, business virtual tables, mappings, custom
  configuration columns, or bulk-operation handlers.
- A rewrite of unrelated CLI, connection, telemetry, or solution infrastructure.

## CLI feature baseline

The baseline is a capability inventory, not a commitment to preserve every incidental
restriction, output string, or internal public type.

| Surface | Baseline capability |
|---------|---------------------|
| `plugin push <Target>` | DLL, NuGet package, or directory of supported files |
| `plugin register` | Alias of `plugin push` |
| `--solution` | Ensure documented resource membership in a target solution |
| `--publisher-prefix` | Explicit package naming prefix; required for package targets |
| `--dry-run` | Preview intended changes without writes |
| `--confirm` | Per-target confirmation after preview; suppressed in dry-run, CI, and non-interactive execution |
| Shared CLI settings | Preserve connection/authentication, non-interactive, and telemetry integration |
| Registration attributes | Steps/images, Custom API handlers, custom data providers, and managed identities |
| Artifact updates | Content-based idempotence; standalone build/revision updates in place and major/minor replacement |
| `plugin step config set` | Update secure and/or unsecure configuration on an existing step |
| Step selection | Direct GUID or complete composite criteria with optional secondary entity and execution order |
| Configuration input | Inline or UTF-8 file; omission preserves a value and empty string clears it |

Current declarative input requires supported `Digitall.Plugins.Registration` attributes
on every concrete plugin type. Missing Custom APIs, unresolved messages, incompatible
provider tables, and incomplete metadata reads fail before deployment writes.

The first rewrite requires no registration-library change for identity. This document
does not approve a new CLI option or change shared exit-code contracts.

## Architecture options

| Option | Shape | Strength | Cost |
|--------|-------|----------|------|
| A: Direct reconciler | One deployment service reads and reconciles resources sequentially | Smallest sequential implementation | Preview can become a separate simulation mode; late decisions can follow earlier writes |
| B: Snapshot -> change set -> apply | Load desired/current state, compute intended changes, preview, then apply | Clear decisions, faithful preview, strong unit tests | A modest intermediate data model |
| C: Resource synchronizers | Independent artifact, registration, provider, identity, and solution synchronizers | Cohesive resource implementation homes | Cross-resource orchestration can recreate current coordination problems |

Recommendation: **B, implemented conservatively**. Resource-specific helpers may exist,
but they do not own competing deployment pipelines.

## Recommended architecture

```text
Push command
    |
    +-- ArtifactReader -----------------> validated desired target
    |
    +-- PluginDeployment
            |
            +-- Dataverse access -------> scoped current snapshot
            |
            +-- RegistrationDiff -------> intended changes
            |
            +-- Console presentation ---> preview / confirmation
            |
            +-- Apply changes ----------> writes / progress / result

Step config command
    |
    +-- StepConfiguration --------------> resolve + update supplied values
```

These are responsibility boundaries, not a prescribed class count or folder hierarchy.

### Artifact reading

Own file selection, package inspection, metadata-only reflection, attribute parsing,
and local validation. Produce validated desired state. Downstream reconciliation should
not repeatedly validate basic declarations.

Readers throw contextual errors rather than printing errors and returning `null`.
The CLI boundary owns user-facing presentation. Assembly inspection must fail closed
when not all CLR types can be loaded.

Package asset selection must remain explicit: select one compatible framework group
for inspection, reject ambiguous DLL names, and do not confuse local inspection
compatibility with Dataverse deployment support.

### Dataverse access

Own SDK queries, complete paging, request payloads, package downloads, and metadata
operations. Start with cohesive concrete helpers for artifacts, registrations, and provider
metadata rather than one interface/repository pair per entity.

`IOrganizationServiceAsync2` already provides a substitution boundary. Additional
interfaces require a concrete use case, not a uniform layering rule.

Snapshots include relevant registrations and dependencies within the target's ownership
scope. Avoid loading the whole environment where scoped queries suffice. Cache only
within a clearly bounded snapshot/deployment lifetime.

### Reconciliation

Pure reconciliation takes validated desired state and a current snapshot and produces
intended changes:

```text
Compare(desiredTarget, currentSnapshot) -> DeploymentChanges
```

It owns matching, identity selection, update/delete decisions, migration selection, and
dependency validation. It performs no SDK writes and emits no console output.

Remote metadata/message lookups needed for decisions must be available before approval.
Select existing or migratable registrations first; compute their final changes once.

### Deployment orchestration

One owner coordinates preparation, approval, execution, and cleanup. Resource helpers
apply writes; they do not expose alternative complete lifecycle entry points.

Construct dependencies at a composition boundary. Commands should not rebuild a large
object graph per target. Commands retain CLI concerns; the deployment use case is directly
testable without duplicating its orchestration.

## Change set and preview

Keep the preview boundary. Its value extends beyond the tree: it makes dry-run, meaningful
confirmation, destructive-change inspection, and pre-write validation possible.

Most matching and dependency decisions are required even without preview. Removing the
change set would move those decisions between writes rather than eliminate them.

The tree is a view of intended changes, not the structure of the execution engine.

### Representation

- Keep desired input, current snapshot, and final intended changes.
- Treat comparisons as temporary calculations rather than another persistent object hierarchy.
- Use typed resource-specific change collections, not arbitrary executable delegates.
- Record selected existing IDs, destination references, intended field changes, and removals.
- Represent new resources using scoped logical references, resolved to GUIDs during application.
- Scope type references by owning assembly/artifact; type name alone is not a universal identity.
- Derive `HasChanges` and operation summaries from the same change collections used by execution.
- Supply unchanged resources for display from desired/snapshot state rather than pseudo-operations.
- Do not copy secrets into snapshots or change sets.

The exact record shapes should be selected during implementation. Prefer types that prevent
invalid states over independent flags such as `Link`, `Unchanged`, and nullable desired IDs.

### Presentation and approval

Group changes into package/assembly/type/step trees, with providers displayed separately.
Show solution additions and prominent destructive-operation summaries. Explain preservation,
replacement, and skipped/unsupported outcomes when relevant.

Render the same decisions that will be applied. Do not silently recompute intent after
confirmation. Package upload may require resolving newly created server IDs; that narrow
resolution step must not authorize additional unpreviewed destructive actions.

Dry-run performs the same preparation and reconciliation as execution, then stops before
writes. It is not a separate simulation implementation.

## Registration identity and reconciliation

### Preserve matching steps

Recommendation: preserve and reassign matching steps instead of deleting/recreating them
during assembly replacement.

| Concern | Preserve/reassign | Re-create |
|---------|-------------------|-----------|
| Step ID | Stable | Changes; stored `--step-id` references become stale |
| Secure/unsecure configuration | Remains associated without reading/copying values | Requires transfer or reprovisioning |
| Solution membership | Existing membership remains | Requires restoration |
| Untouched environment-owned fields | Remain on the record | Can revert to creation defaults |
| Matching images | Can retain records | Must be re-created |
| Cutover | Changes handler on an existing registration | Delete-first introduces a gap; create-first can introduce duplicate execution |

Re-creation is appropriate for a genuinely new registration, an explicit reset, or a
fully reproducible deployment where all state is provisioned and stable IDs are irrelevant.
It is not a simplification if preserving the same state requires extra transfer machinery.

Migration is ordinary reconciliation against an existing step whose destination type changes.
It may have a distinct progress label, but needs no parallel migration model or pipeline.

### First-version matching contract

Within a scoped plugin type, match by message, stage, mode, primary entity, and secondary
entity. Compare message/entity names case-insensitively and normalize unset entity names
to `none`. Exclude name, execution order, filtering attributes, images, and assembly version.
Changes to tuple fields or plugin type mean a new registration; matching configuration is
preserved only when the tuple still identifies the same registration.

Reject duplicate local declarations even when their execution orders differ. Reject multiple
remote matches rather than assigning an existing configuration arbitrarily. Existing unkeyed
steps can be reused by an unambiguous composite match.

Persisted keys are deferred, not required for implementation. A generated key from these
properties would have the same limitations as the tuple: prefixes/hashes cannot distinguish
duplicates or preserve identity when tuple fields change.

Name and Description are possible storage fields but neither is an established identity
contract. Category and RuntimeIntegrationProperties are explicitly documented for internal
use only and must not be repurposed. See the
[Microsoft step reference](https://learn.microsoft.com/en-us/power-apps/developer/data-platform/reference/entities/sdkmessageprocessingstep).

## Ownership and removal

Selected direction: **authoritative reconciliation within a defined ownership scope**.
Absence may mean removal only after ownership has been established.

The scope must distinguish resources owned by the target from shared/environment-owned
resources. Association ownership does not imply ownership of the associated record.

| Resource/state | Proposed treatment |
|----------------|--------------------|
| Owned plugin types, steps, and images | Converge to declarations; own all non-internal steps on selected plugin types and remove absent owned registrations |
| Secure/unsecure step configuration | Environment-owned; push neither reads nor reconciles values |
| Step operational overrides | Preserve enabled state and impersonation; reconcile only declared registration fields |
| Custom API records | External prerequisite; only the owned handler association is reconciled |
| Providers and individual operation handlers | Define ownership per provider/operation before authoritative removal |
| Managed identity records | Potentially shared; linking does not authorize deleting an identity |
| Solution membership | Ensure requested membership; do not implicitly remove other memberships |
| Provider tables | No deletion from absence without a separately approved schema ownership policy |

The current mixture of authoritative step/API behavior and preserving provider/identity
omissions must become documented policy, not incidental null-handling.

For comparison, upsert-by-default with explicit prune is safer for adoption but allows stale
registrations to accumulate. That alternative is not the selected direction.

## Deployment lifecycle and recovery

One visible lifecycle establishes replacements before removing dependencies:

1. Create/update the artifact.
2. Resolve package-created assemblies and establish missing plugin types across the target.
3. Establish provider-table prerequisites, if provisioning is retained, and replacement handler references.
4. Apply steps/images, Custom API associations, and managed identity associations.
5. Ensure requested solution membership.
6. Remove obsolete owned registrations, types, and assemblies.

This is a logical ordering proposal, not a platform-verified request sequence. Validate
Dataverse constraints and package upload side effects before implementing it.

### Failure and retry

- Stop execution of a target after a failed operation.
- Report completed work, failed phase, and that partial changes remain.
- Keep cleanup after required replacement associations succeed.
- Do not advertise global rollback or atomic cutover.
- Reconcile a retry from actual remote state, including already moved registrations.

Selected direction: recognize unambiguous partially completed replacements and complete
them conservatively. Reject genuinely ambiguous multiple-installation states.

The existing blanket rejection of multiple standalone version trains is an alternative,
but requires explicit manual-recovery guidance.

The first recovery implementation permits at most two standalone records: exactly one in
the desired major/minor train and one superseded record, with target content matching the
requested artifact. This hash guard is provisional and does not prove a failed deployment.
Duplicate/extra trains, target-content mismatch, and ambiguous matching registrations fail.
Unsupported cross-resource dependencies block cleanup until their owning phase is implemented.

Concurrent deployments or registration editing against the same target are unsupported.
Do not repeatedly reload full registration snapshots as a substitute for locking: they add
remote reads but cannot guarantee consistency. Retain artifact-state checks, validate
server-created assembly/type records after package upload, and check dependencies immediately
before destructive cleanup. Unexpected artifact/server-owned state must not silently expand
approved scope. Stronger concurrency protection requires a separately designed, platform-supported
strategy; preview is intended changes, not a concurrency guarantee.

## Resource-specific policies

### Artifacts

Preserve content-based idempotence. Package version alone does not imply changed content.
Standalone build/revision changes remain in-place updates when major/minor matches.
Major/minor replacement includes registration reassignment and dependency-safe cleanup.

Define handling of assemblies/types removed from a package and ownership changes between
standalone/package deployment. Server-side package upload effects require live verification;
the tool must not claim a preview of effects it cannot establish.

### Custom data providers

Retain handler deployment and validation of declared provider prerequisites.
Provisioning specialized configuration tables remains an open scope decision:

| Option | Benefit | Cost |
|--------|---------|------|
| Integrated creation/update/publish | Complete provider deployment in one command | Adds schema lifecycle and partial-failure cases |
| Require an existing compatible table | Smaller module; schema lifecycle belongs to solution deployment | Separate provisioning step |

Provisional direction: require an existing compatible table and maintain provider records
and handlers only. Verify independent provisioning after implementing that slice and before
final cutover. A practical supported provisioning route has not been established; agree a
fallback if verification fails.

If provisioning remains, preserve explicit JsonConverter backing-provider validation,
base-language handling, metadata compatibility checks, and solution behavior.

Providers can reference types from multiple assemblies. Resolve all replacement types
before updating those handlers and before deleting old types. Provider/operation ownership
and omission semantics must be settled independently of table provisioning.

### Managed identities

Reject conflicting package declarations rather than selecting the first attributed assembly.
Validate a declared tenant against an existing identity instead of matching application ID alone.

Decide whether identity selection belongs in build artifacts or environment deployment input.
Environment-specific identities can otherwise require environment-specific builds.
Decide what removing a declaration means for an existing association; never infer permission
to delete a shared identity record.

### Solutions

Define exactly what `--solution` guarantees for packages, standalone assemblies, steps,
providers, and provider-table schemas. Types/images/APIs/identities are not implicitly
included merely because they participate in deployment.

Selected direction: establish a verified component-awareness matrix, add documented supported
components, and fail actual membership errors. Do not introduce a user-selectable best-effort
mode or infer solution awareness from a table being writable. Verify package/provider support
before finalizing their guarantees; do not silently skip a promised membership.

## Directory deployments

Recommendation: treat a directory as a deterministic batch of independently executed targets,
not a transaction.

Inspect local inputs before writes. Detect overlaps such as a standalone DLL also owned by
a selected package, conflicting provider declarations, or competing registrations. Reject
conflicts rather than relying on filesystem enumeration order.

Apply explicit ordering. Prepare each target against current remote state when it is reached;
do not reuse stale snapshots after prior targets have written.

Selected direction: continue independent later targets after failure, return overall failure,
and stop on cancellation. Report failed, applied, unchanged, and declined targets separately;
a partially failed batch must not appear successful.

## Step configuration

Keep this as a separate use case, not part of artifact deployment or its change set.

- Resolve exactly one step by GUID or complete composite criteria.
- Retain optional secondary-entity/execution-order disambiguators and named/numeric stage parsing.
- Preserve ambiguity diagnostics listing step names and IDs.
- Read all supplied files and validate inputs before writes.
- Distinguish omission from an empty value.
- Never log supplied values or place them in deployment preview state.
- Keep secure configuration record creation and linking transactional.

Selected scope: retain the current command behavior rather than adding combined transactions.
Individual configuration updates are idempotent and a partially completed invocation can be
rerun. Report failures honestly; retain the existing transaction for secure-record creation
and linking.

## Errors, cancellation, and operational safety

Use async SDK operations and propagate cancellation through remote reads and writes.
Do not convert cancellation into a per-target validation failure and continue deploying.

Use contextual exceptions from internal components and one user-facing error boundary.
Retain useful Dataverse fault codes/messages without printing secrets, arbitrary fault details,
or stack traces by default. Report partial completion honestly.

Preserve metadata-only inspection, predictable package asset selection, and safe temporary-file
cleanup. Use complete paging for result sets needed for reconciliation; incomplete remote state
must never drive destructive conclusions.

Do not automatically retry non-idempotent writes without knowing whether they committed.
Command-level reconciliation after failure is different from transport retries.

## Testing and acceptance criteria

Use TUnit and the existing fake SDK infrastructure. Exercise actual production orchestration;
do not implement another plan/render/apply pipeline inside tests.

| Layer | Required coverage |
|-------|-------------------|
| Artifact reading | Registration parsing, duplicate declaration validation, package assets, missing dependencies, fail-closed metadata loading |
| Pure reconciliation | Identity/matching, ambiguity, ownership, omission, updates/removals, migrated and already migrated steps |
| SDK boundary | Exact payloads, complete paging, metadata operations, transactions, package ID resolution |
| Production orchestration | Preview/approval/apply, cancellation, execution ordering, partial progress, batch outcomes |
| CLI | Command tree, alias, all settings/argument parsing, stage values, defaults and validation |
| Live Dataverse | Platform-specific upload side effects, reassignment, image updates, providers, transactions, and solution constraints |

Acceptance criteria:

1. Dry-run and declined confirmation perform zero deployment writes.
2. Preview, change detection, and execution consume the same intended decisions.
3. A repeated successful push produces no unnecessary writes.
4. Matching-step replacement preserves step identity and configuration association.
5. Ambiguity and invalid ownership fail before destructive operations.
6. Cross-assembly provider references resolve before obsolete types are removed.
7. Injected failure at each lifecycle phase leaves documented recoverable or explicitly blocked state.
8. Retry behavior is verified against the selected recovery policy.
9. Configuration updates retain omission/empty-value semantics and can be rerun after partial failure.
10. Directory overlap detection and ordering are deterministic.
11. A new operation has one decision point and one write implementation; it does not
    require another independent `HasChanges` rule.

## Implementation approach

Selected approach: a controlled core rewrite, not another round of structural patches or
a wholesale module replacement. The rationale is that ownership, registration identity,
and recovery policies shape the core model and remain open to change before release.
The CLI capability inventory is the starting baseline, but resulting CLI changes are allowed.

Replace the comparison/deployment model stack, migration-plan patching, competing execution
pipelines, and independent change-detection rules. Retain or adapt metadata-only inspection,
package asset selection, correct SDK payload helpers, fault diagnostics, CLI integration,
and tests that establish the desired behavior. Existing tests are evidence, not immutable
specifications of policies that may deliberately change.

Do not begin by rearranging folders or consolidating repositories. First settle remaining
ownership, provider scope, and recovery semantics. Identity persistence is not a prerequisite
for the first rewrite.

Then establish scenario tests around the selected behavioral contract, build unified
registration reconciliation, introduce the minimal change set, and put sequencing under
one orchestration owner. Reuse correct SDK payload helpers where useful, but do not preserve
existing model hierarchies merely for internal compatibility.

Implement complete slices: standalone registration and replacement first, then package and
cross-resource capabilities. Switch command wiring once the replacement is ready and remove
the superseded core. Avoid permanent legacy/new modes or compatibility abstractions whose
only purpose is retaining both architectures.

Simplify dependencies and presentation after the lifecycle works. Remove superseded comparison,
migration, and executor entry points rather than leaving two deployment architectures.

Update README usage/behavior and registration-library documentation when implementation
changes public behavior. This proposal alone does not change the current command reference.

## Open decisions

| ID | Question | Proposed direction |
|----|----------|--------------------|
| D1 | What establishes target ownership, including shared provider operations and package removals? | Explicit artifact/resource scope; never infer ownership from a matching name alone |
| D2 | Should persistent keys be revisited later? | Deferred; first rewrite uses unambiguous tuple matching without attribute changes |
| D3 | Can specialized provider tables be provisioned independently through a practical supported route? | Provisional existing-table cutoff; post-implementation verification and fallback gate |
| D4 | Is the initial recovery recognition policy sufficient? | Two-record/one-target-train rule with a provisional target-content guard; verify platform behavior |
| D5 | Which environment overrides survive authoritative registration reconciliation? | Preserve configuration, enabled state, and impersonation; reconcile only declared fields |
| D6 | Does omitted provider/identity declaration remove an owned association? | Define ownership before deciding; shared records are not implicitly deleted |
| D7 | Is managed identity selection artifact-owned or environment-supplied? | Reject conflicts and validate tenant compatibility in either design |
| D8 | What is the verified solution-aware component matrix? | Add supported components; fail actual membership errors |
| D9 | Do independent directory targets continue after failure? | Yes, with overall failure; stop on cancellation |
| D10 | How are concurrent changes or stale previews detected? | Guard critical assumptions; require fresh approval when intent changes |
| D11 | Does configuration require redesign? | No; retain current rerunnable behavior and secure-create/link transaction |
| D12 | Which package upload side effects can be previewed, and which request sequences are supported? | Establish with live tests before promising guarantees |

These are decision gates, not approved features or a task-status tracker. Selecting a core
rewrite does not settle these behaviors or make this an implementation-ready specification.

## Current implementation references

Paths below are relative to `src/modules/dgt.power.plugin/` unless stated otherwise.

| Topic | Evidence |
|-------|----------|
| CLI surface | `Commands/PluginPushSettings.cs`, `Commands/PluginStepConfigSetSettings.cs`, `src/dgt.power/CommandTree.cs:152-173` |
| Independent plan interpretation | `Planning/Deployment/PluginDeploymentPlanExtensions.cs:14-90`, `Output/PluginPlanRenderer.cs:14-36,164-224` |
| Competing execution sequences | `Execution/PluginPushExecutor.cs:28-41,93-130`, `Execution/PluginTypeDeploymentExecutor.cs:25-31` |
| Migration reconstruction | `Planning/PluginDeploymentPlanner.cs:202-209,326-376` |
| Reassignment and cleanup | `Execution/PluginTypeDeploymentExecutor.cs:158-176`, `Execution/OutdatedAssemblyMigrator.cs:30-58` |
| Retry limitation | `Planning/PluginDeploymentPlanner.cs:27-35`, `tests/dgt.power.plugin.tests/Execution/DataProviderDeploymentTests.cs:132-144` |
| Separate test orchestration | `tests/dgt.power.plugin.tests/Execution/PluginPushExecutorTests.cs:25-68` |
| Package identity selection | `Planning/PluginDeploymentPlanner.cs:76-82` |
| Identity lookup | `Repositories/ManagedIdentityRepository.cs:14-40` |
| Configuration sequencing | `Commands/PluginStepConfigSetCommand.cs`, `Repositories/SdkMessageProcessingStepSecureConfigRepository.cs` |

Additional code-level caveats are indexed in
[`research-plugin-deployment-change-surfaces.md`](../../.memory/research-plugin-deployment-change-surfaces.md).
