# Standalone registration reconciliation

The replacement core lives in `src/modules/dgt.power.plugin/Reconciliation/`.
It is not wired into the CLI; current command behavior still uses the existing planner
and executors. Keep that distinction explicit until the final cutover.

## Responsibilities and representation

`PluginDeployment.PrepareAsync` validates local declarations, loads a scoped registration
snapshot, runs `RegistrationDiff`, and resolves messages for intended step writes.
`PreparePackageAsync` and `ApplyPackageAsync` use the same registration applier, adding only
package upload plus resolution of server-created assembly/type IDs. Preparation performs no
deployment writes; application consumes prepared registration decisions without re-running
matching.

`RegistrationChanges` contains flat create/write/delete collections. A write with a null
existing ID is a create. Its `HasChanges` derives from those same collections.
`StandaloneDeploymentRenderer` groups desired state and changes into a tree; it does not
compute registration comparisons or maintain a separate change-detection rule.

Step references used for message resolution and execution IDs include both CLR type name
and the desired step. Structurally identical step values can occur in different types:
step value alone is not a safe assembly-wide dictionary key.

## Ownership and supported boundary

Reconciliation owns supported declarative stages (10, 20, 40). Internal/platform-only
stages are retained and their images are not reconciled. Duplicate declarations and
ambiguous remote matches fail before writes. Type matching is ordinal/case-sensitive;
message/entity matching is case-insensitive.

The standalone slice supports new and same-major/minor artifacts, major/minor replacement,
type creation/removal, step create/update/reassignment/removal, and image
create/attribute-update/removal. Matching IDs and fields
outside the SDK registration payload, including configuration associations, enabled state,
and impersonation, are retained.

Unsupported identity/provider/API declarations and existing linked APIs fail explicitly.
Superseded assembly identity associations, platform-only steps on deleted types, and
provider references to deleted types also block replacement/cleanup. These guards prevent an incomplete replacement
slice from applying an apparently successful partial deployment. No solution option or
package entry point is exposed by this slice.

Assembly state is checked again before application. Before deleting types, dependent
steps, API links, and provider references are checked; before deleting the old assembly,
remaining types are checked. Preparation verifies that dependent step IDs are represented
in the joined snapshot rather than trusting an incomplete join.
Package reconciliation supports new, changed, and unchanged content while rejecting immutable
version changes, package identity associations, undeclared server assemblies/types, and asset,
step, or image removal before upload. Package upload is expected to create/update its owned
assembly/type records; missing or unexpected records fail without standalone fallback. Tests
use a dedicated fake for those server-owned side effects, which is not live Dataverse verification.
`RetrieveDependenciesForDelete` remains a candidate for broader component-deletion checks,
not a proven guard for server-managed package updates.

Full registration stale-state checks remain prerequisites for cutover.
Preparation/application separation is not a transaction or concurrency guarantee.

## Replacement and recovery policy

The diff accepts a destination snapshot and optional replacement-source snapshot. It selects
step candidates across both for each exact CLR type name, then calculates changes once.
Any matching old step becomes a write against its existing ID and the destination type.
Old types and assembly are removed only after step/image writes and obsolete-step cleanup.
No separate migration patching is used.

Two-assembly recovery permits exactly one desired major/minor train and one other same-name
standalone assembly only when the target bytes match the requested artifact's content hash.
More records, duplicate target trains, missing target trains, and mismatched content fail.
This hash guard is conservative and provisional; it does not prove a prior failed push.
Authoritative artifact ownership is the policy allowing the superseded record's cleanup.
Both assemblies supplying the same matching step tuple is ambiguous and fails rather than
choosing which configuration survives.

Fresh preparation after interrupted writes sees already-created types and already-moved steps,
so it can complete cleanup without creating duplicate registrations.
Tests explicitly set server-derived assembly metadata on uploaded fake records: the SDK
fake does not extract assembly identity from DLL bytes. Real package/assembly side effects
still require live verification.

## Reused remote access

`DataverseQueryReader` collects all pages and forwards paging cookies. Assembly lookup,
type lists, step lists, and image lists use it, so reconciliation is not based on truncated
first-page state. These shared read improvements also apply to the currently wired core.

Tests invoke the replacement use case and renderer directly rather than maintaining a
second preparation/application algorithm.
