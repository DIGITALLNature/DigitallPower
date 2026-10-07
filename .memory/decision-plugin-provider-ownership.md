# Provider handler ownership

The replacement core reconciles provider records and handlers against an existing compatible
data-source table; it does not provision/publish table metadata. The currently wired CLI retains
its previous provider implementation until final cutover.

## Selected ownership

A declaration owns the complete handler configuration of the provider identified by its
data-source logical name. Set declared operations, allow explicit reassignment from external
handlers, and clear undeclared slots even if they reference external types.

If a provider is omitted entirely, clear only handlers referring to current/superseded types
owned by this target. Preserve external handlers, provider records, and data-source tables.
Do not infer authority to delete shared records from handler ownership.

Package assemblies can jointly declare a provider. Resolve each operation through
`PluginTypeReference`; equal CLR names in different assemblies are distinct. Reject competing
scoped claims rather than choosing the first. Separate deployment targets should not
independently declare the same provider; batch preflight must detect such conflicts.

## Execution boundary

`DataProviderChange` holds optional declared metadata, the selected existing record, and flat
handler writes. A null handler reference means an explicit null SDK field, not `Guid.Empty`
and not omission from the update payload. Preserve omitted description; reconcile provider
name and explicitly supplied description without updating data-source labels/schema.

Resolve all destination types before provider writes, and write associations before cleanup.
A failure retains superseded types for fresh-preparation recovery. Before deleting types,
check for remaining provider references rather than assume earlier writes removed every dependency.

Provider declarations with a solution request currently fail explicitly pending provider/table
membership implementation. Existing-table provisioning feasibility, actual SDK clearing/
reassignment, platform-created step cleanup, and managed-solution transport require independent
Dataverse verification before CLI cutover. Request fakes establish payload/order/retry behavior,
not those platform guarantees.
