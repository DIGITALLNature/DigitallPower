# Custom API association ownership

The replacement deployment core reconciles handler associations, not Custom API records.
The CLI remains on the previous pipeline until the final cutover.

## Selected policy

- Explicit declarations authorize reassignment even when the current handler belongs
  to a different deployment target.
- Omission unlinks associations currently handled by target-owned types, including the
  superseded standalone assembly. Other associations are untouched.
- API records remain external prerequisites: never create or delete them during push.
- Reject duplicate declarations across the entire target, missing APIs, and ambiguous
  unique-name lookups before artifact writes.

## Implementation invariants

`CustomApiChange` stores the API ID, previous handler, and an optional `PluginTypeReference`
destination. This value record identifies a CLR type within its owning assembly and is also
the key for handler resolution. A null destination means unlinking, not deletion.

Calculate omission across the whole package, not independently per assembly. Otherwise
moving an API handler between package assemblies can produce both a link and a later unlink.
Unchanged associations are displayed from desired state rather than pseudo-write operations.
Reassignment and unlinking appear in destructive-change counts.

Resolve all destination types before association writes. Apply associations before
registration/type cleanup, so an association failure retains old types for fresh-preparation
recovery. Reconcile already-completed writes on retry; do not replay the old change set.
API reads collect all pages. Application does not reload full registration snapshots or
claim concurrency guarantees.

Platform-created SDK steps and package asset removal retain their existing conservative
guards until their platform lifecycle is verified.
