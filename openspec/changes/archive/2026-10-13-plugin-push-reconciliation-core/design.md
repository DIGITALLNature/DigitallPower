# Design

## Context

Decisions and rationale are recorded in the ADRs `plugin-reconciliation-core`, `custom-api-association-ownership`, `data-provider-ownership`, and `environment-owned-plugin-identities`. This document keeps the implementation notes that guided the code in `Deployment/Reconciliation` and `Deployment/Batch`.

## Representation

- `PluginDeployment.PrepareAsync` validates local declarations, loads a scoped registration snapshot, runs `RegistrationDiff`, and resolves messages for intended step writes. `PreparePackageAsync` and `ApplyPackageAsync` reuse the same registration applier and add only package upload and resolution of server-created assembly/type IDs. Preparation performs no writes; application consumes the prepared decisions without re-matching.
- `RegistrationChanges` holds flat create/write/delete collections. A write with a null existing ID is a create, and `HasChanges` derives from the same collections.
- `DeploymentRenderer` groups desired state and changes into standalone/package trees. It never computes comparisons itself. Package assembly changes are labeled as managed by upload.
- Step references used for message resolution and execution IDs include the CLR type name and the desired step; a step value alone is not a safe assembly-wide key.
- Type matching is ordinal; message and entity matching are case-insensitive.

## Ownership boundary

- Reconciliation owns supported declarative stages (10, 20, 40). Internal stages are retained and their images not reconciled.
- Platform-managed steps of a removed type (created by a Custom API link) are not validated during preparation: the unlink that runs before cleanup removes them, and apply stops before deleting the type if a dependent step remains.
- Type creation/removal, step create/update/reassignment/removal and image create/attribute-update/removal are supported. Fields outside the SDK registration payload (configuration associations, enabled state, impersonation) are retained.

## Replacement and recovery

The diff takes a destination snapshot and an optional replacement-source snapshot, selects step candidates across both per CLR type name, and calculates changes once. Any matching old step becomes a write against its existing ID and the destination type. Old types and the assembly are removed only after step/image writes, associations, and obsolete-step cleanup. No separate migration patching exists.

Two-assembly recovery needs exactly one desired major/minor train and one other same-name standalone assembly whose target bytes match the artifact's content hash. The hash guard is conservative and provisional; it does not prove a prior failed push. Fresh preparation after interrupted writes sees already-created types and already-moved steps, so cleanup completes without duplicates.

## Packages

Package upload is expected to create or update its owned assembly/type records; missing or unexpected records fail without a standalone fallback. Package versions are strings because Dataverse retains NuGet SemVer; the file hash is nullable for records without an attached file. Resolution validates all assemblies and types (exact versions and retained IDs) before registration writes. Cancellation is checked between registration writes.

## Associations and membership

- `CustomApiChange` stores the API ID, previous handler, and an optional `PluginTypeReference` destination (null means unlink). `PluginTypeReference` identifies a CLR type within its assembly, so equal names in different assemblies stay distinct. Omission is computed across the whole package.
- `DataProviderChange` holds optional declared metadata, the selected existing record, and flat handler writes. A null handler reference is an explicit null SDK field, not `Guid.Empty` and not an omitted attribute. An omitted description is preserved; data-source labels and schema are never updated.
- All destination types are resolved before association writes, and associations are written before cleanup. Retries reconcile completed writes rather than replaying an old change set.
- Provider and package solution membership resolves component types from the environment and fails preparation when absent or ambiguous. `AddRequiredComponents` stays false. Membership-only changes count as work but never trigger uploads, and fresh preparation skips completed additions. Membership is applied after associations and before cleanup and never removes memberships.

## Remote access and concurrency

`DataverseQueryReader` reads all pages and forwards paging cookies. Snapshots are read during preparation and not reloaded during application; repeated snapshots would not provide consistency. Concurrent deployment or manual registration editing against the same target is unsupported. Preparation/application separation is neither a transaction nor a concurrency guarantee.

## Testing

Tests invoke the use case and renderer directly. The SDK fake does not extract assembly identity from DLL bytes, so tests set server-derived metadata explicitly and use a dedicated fake for package upload side effects. This does not prove platform behavior.

## Open Verification

These need a live Dataverse environment: existing-table provider provisioning, actual clearing and reassignment of provider handlers, platform-created step cleanup, package and provider solution transport, managed-solution rebinding of identities, and `RetrieveDependenciesForDelete` as a broader deletion guard (including server-managed package updates).
