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
`DeploymentRenderer` groups desired state and changes into standalone/package trees; it does not
compute registration comparisons or maintain a separate change-detection rule.
Both trees share registration presentation. Package assembly changes are labeled as managed
by upload rather than direct assembly writes; new package types are created via upload.

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

Unsupported provider declarations fail explicitly. Custom API associations use
shared target-wide preparation and application; see
[Custom API ownership](decision-plugin-custom-api-ownership.md).
Platform-only steps on deleted types and
provider references to deleted types also block replacement/cleanup. These guards prevent an incomplete replacement
slice from applying an apparently successful partial deployment. No solution option or
package entry point is exposed by this slice.

Assembly state is checked again before application. Before deleting types, dependent
steps, API links, and provider references are checked; before deleting the old assembly,
remaining types are checked. Preparation verifies that dependent step IDs are represented
in the joined snapshot rather than trusting an incomplete join.
Package reconciliation supports new, changed, and unchanged content while rejecting immutable
version changes, undeclared server assemblies/types, and asset,
step, or image removal before upload. Package upload is expected to create/update its owned
assembly/type records; missing or unexpected records fail without standalone fallback. Tests
use a dedicated fake for those server-owned side effects, which is not live Dataverse verification.
Fetched package versions are required strings because Dataverse versions retain NuGet SemVer;
package-file hashes remain nullable when the package record has no attached file.
Package resolution validates all assemblies/types, including exact assembly versions and
retained IDs, before registration writes. Additive uploads and fresh-preparation retries reuse
server-owned records and completed registrations without re-uploading identical content.
Cancellation is checked explicitly between registration writes in the shared applier.
`RetrieveDependenciesForDelete` remains a candidate for broader component-deletion checks,
not a proven guard for server-managed package updates.

Registration snapshots are read during preparation, not reloaded during application.
Repeated full snapshots add remote reads without guaranteeing consistency, so they are not
used as a substitute for locking. Concurrent deployment or registration editing against the
same target is unsupported. Retain artifact-state checks, post-upload assembly/type resolution,
and targeted dependency checks immediately before destructive cleanup. Stronger concurrency
protection would require a separate platform-supported design.
CLI integration of package tree presentation and platform-verified removal/type-move behavior are also outstanding;
the guarded package subset must not be treated as completion of all package acceptance gates.
Preparation/application separation is not a transaction or concurrency guarantee.

Managed identity provisioning and binding are outside push in both pipelines. Local declarations,
identity repositories, plan flags, and identity write/render phases are removed; there is no
retired-attribute parsing or detection path. Preserve existing target bindings, never copy an old
binding to a new standalone replacement, and never delete shared identity records. See
[environment identity boundary](decision-plugin-managed-identity-boundary.md).

## Replacement and recovery policy

The diff accepts a destination snapshot and optional replacement-source snapshot. It selects
step candidates across both for each exact CLR type name, then calculates changes once.
Any matching old step becomes a write against its existing ID and the destination type.
Old types and assembly are removed only after step/image writes, Custom API associations,
and obsolete-step cleanup.
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
type lists, step lists, image lists, and Custom API lookups use it, so reconciliation is not based on truncated
first-page state. These shared read improvements also apply to the currently wired core.

Tests invoke the replacement use case and renderer directly rather than maintaining a
second preparation/application algorithm.
