## Purpose

Transfer Dataverse team templates through resource-oriented commands with explicit control over synchronization and removal.

## Requirements

### Requirement: Teamtemplate transfer is resource-oriented

The CLI SHALL expose `teamtemplates import` and `teamtemplates export`. It SHALL NOT accept
`import teamtemplates` or `export teamtemplates`, including compatibility aliases. Help and
completion SHALL advertise only the new paths.

#### Scenario: New transfer paths
- **WHEN** help is requested for either resource-oriented command
- **THEN** help describes the corresponding transfer operation

#### Scenario: Legacy transfer path
- **WHEN** either replaced verb-first command is invoked
- **THEN** parsing fails without a Dataverse write

### Requirement: Export provides importable resource data

Export SHALL produce version 1 resource data usable by import, preserving GUIDs, logical table
names, names, descriptions, and access rights. Object type codes SHALL be resolved in the target
environment rather than transferred.

#### Scenario: Template transfer
- **WHEN** exported template data is imported into a compatible target environment
- **THEN** the resulting template preserves its configured table association and access rights
  without relying on equal environment-specific object type codes

### Requirement: Transfer files use a versioned envelope

Files SHALL contain `schemaVersion: 1` and a `templates` array matching the published resource
schema. Import SHALL reject legacy bare arrays, unsupported versions, invalid fields, and
malformed input without writes. Each entry SHALL require a non-empty GUID, logical table name,
nonblank name, and valid access-rights mask. Template names SHALL be at most 100 characters;
descriptions SHALL be at most 1,500 characters; logical table names SHALL match
`^[a-z][a-z0-9_]*$`.

#### Scenario: Versioned resource file
- **WHEN** a file contains version 1 and valid template entries
- **THEN** import accepts its structure for preflight validation

#### Scenario: Unsupported file contract
- **WHEN** input is a legacy array, has an unsupported version, or violates the resource schema
- **THEN** import fails with an actionable diagnostic and no Dataverse writes

### Requirement: Template identity is GUID-based

Import SHALL match only by GUID and preserve the supplied GUID on creation. It SHALL NOT fall back
to names. A GUID associated with another table, duplicate input GUIDs, or duplicate table/name
pairs in input SHALL fail preflight. Table/name equality SHALL be checked case-insensitively.

#### Scenario: Existing GUID
- **WHEN** an input GUID exists on the same target table with changed writable values
- **THEN** import plans an update of that GUID rather than recreation

#### Scenario: New GUID
- **WHEN** an input GUID does not exist and has no table/name conflict
- **THEN** import plans creation with that GUID

#### Scenario: Conflicting table assignment
- **WHEN** an existing GUID is supplied with a different logical table name
- **THEN** import fails before any mutation

#### Scenario: Duplicate input identity
- **WHEN** input repeats a GUID or table/name pair
- **THEN** import fails before any mutation

### Requirement: Name collisions do not remap identity

Import SHALL reject a table/name pair already belonging to a different target GUID, including a
system-template GUID. It SHALL NOT adopt that GUID, merge records, or create a duplicate. Name
comparisons SHALL be case-insensitive.

#### Scenario: Same name under another GUID
- **WHEN** a target template has the input table/name pair but a different GUID
- **THEN** preflight reports the conflict and import performs no writes

### Requirement: Export selection is table-based

Export SHALL include all non-system templates by default and SHALL support explicit
logical-table-name filtering. It SHALL exclude system templates. Import SHALL process the entire
file without import selection filters.

#### Scenario: Default export
- **WHEN** export runs without a table filter
- **THEN** it exports all non-system templates and no system templates

#### Scenario: Filtered export
- **WHEN** export specifies logical table names
- **THEN** only non-system templates associated with those tables are exported

#### Scenario: Import is not restricted by cleanup selection
- **WHEN** input includes a valid template outside explicitly selected cleanup tables
- **THEN** that entry is still processed by import

### Requirement: Export replacement requires explicit consent

Export SHALL preserve existing destination files unless `--overwrite` is supplied. Dry-run SHALL
NOT create or replace the destination.

#### Scenario: Existing destination without overwrite
- **WHEN** the destination exists and `--overwrite` is absent
- **THEN** export fails without changing it

#### Scenario: Explicit overwrite or dry-run
- **WHEN** export is run with `--overwrite` or `--dry-run`
- **THEN** overwrite permits replacement on success, while dry-run writes no file

### Requirement: Import validates writable data and table metadata

Import SHALL require a non-zero combination of Dataverse access-team rights: Read (`1`), Write
(`2`), Append (`4`), Append To (`16`), Delete (`65536`), Share (`262144`), or Assign (`524288`);
Create (`32`) is unsupported. Target tables SHALL have resolvable object type codes and
`AutoCreateAccessTeams == true`. Import SHALL NOT enable access teams implicitly. Name,
description, and rights SHALL define writable state; omitted or null description SHALL clear it.
System status SHALL NOT be a transfer field.

#### Scenario: Access teams disabled
- **WHEN** an input or cleanup table is unknown, lacks an object type code, or has false or null
  access-team enablement
- **THEN** preflight fails without mutations

#### Scenario: Invalid access rights
- **WHEN** a rights mask is missing or contains an unsupported access-team rights combination
- **THEN** preflight rejects the input without mutations

#### Scenario: Description clearing
- **WHEN** an otherwise valid input entry omits description or sets it to null
- **THEN** applying it clears any existing description

### Requirement: System templates are protected

Import SHALL reject input GUIDs targeting system templates before any writes. Cleanup SHALL never
delete system templates, even inside selected tables.

#### Scenario: Import targets a system template
- **WHEN** any input GUID belongs to a target system template
- **THEN** preflight fails and no template is mutated

#### Scenario: System template inside cleanup scope
- **WHEN** an unlisted system template belongs to an explicitly selected cleanup table
- **THEN** it is not planned for deletion and remains unchanged

### Requirement: Import leaves unlisted teamtemplates unchanged by default

Import SHALL create or update listed teamtemplates without deleting or modifying templates solely
because they are absent from the file. Reapplying unchanged input SHALL avoid unnecessary
mutations.

#### Scenario: Partial import
- **WHEN** the target contains an unlisted template and cleanup is not explicitly requested
- **THEN** that template remains unchanged

#### Scenario: Repeated import
- **WHEN** a listed template already matches the input
- **THEN** import does not unnecessarily recreate or update it

### Requirement: Cleanup is explicit and bounded

Removal of unlisted templates SHALL require an explicit cleanup request and a non-empty explicit
list of logical table names. Global scopes SHALL NOT be supported. The scope SHALL NOT be inferred
from file contents or export filters. Missing or invalid scopes SHALL fail before any mutation.
Only unlisted non-system templates inside selected tables SHALL be eligible for deletion.

#### Scenario: Scoped cleanup
- **WHEN** cleanup is explicitly requested for a valid target scope
- **THEN** only unlisted templates inside that scope are eligible for removal

#### Scenario: Cleanup lacks a scope
- **WHEN** cleanup is requested without a valid explicit scope
- **THEN** import fails without any Dataverse writes

#### Scenario: Global scope rejected
- **WHEN** cleanup requests a global rather than explicitly enumerated table scope
- **THEN** import fails before any mutation

#### Scenario: Out-of-scope template
- **WHEN** an unlisted template belongs to a table outside the explicit cleanup scope
- **THEN** cleanup leaves it unchanged

### Requirement: Empty input is non-destructive

A valid empty `templates` list SHALL succeed as a no-op without cleanup. Empty input with cleanup
SHALL fail preflight even with a valid explicit table scope. Missing or invalid files SHALL fail
rather than becoming empty successful input.

#### Scenario: Empty import without cleanup
- **WHEN** a valid empty list is imported without cleanup
- **THEN** import succeeds without mutations

#### Scenario: Empty import with cleanup
- **WHEN** a valid empty list is imported with cleanup
- **THEN** import fails before any mutation

#### Scenario: Missing input
- **WHEN** the input file does not exist or cannot be parsed
- **THEN** import reports failure and performs no mutations

### Requirement: Preflight validates the complete operation

Import SHALL validate the complete input and target plan before its first mutation, including
schema, rights, duplicates, conflicts, system-template protection, table metadata, and cleanup
scope. Any preflight failure SHALL prevent all Dataverse writes.

#### Scenario: Later invalid entry
- **WHEN** a file contains a valid entry followed by an invalid or conflicting entry
- **THEN** preflight rejects the operation without applying the valid entry or any cleanup

### Requirement: Dry-run uses the real preflight

Import dry-run SHALL perform the same preflight as apply and report planned creates, updates,
unchanged entries, and deletes without Dataverse or export-file writes. Invalid plans SHALL fail
in dry-run as they do in apply.

#### Scenario: Valid dry-run
- **WHEN** dry-run validates a plan containing changes and scoped cleanup
- **THEN** it reports the plan and leaves templates and files unchanged

#### Scenario: Invalid dry-run
- **WHEN** dry-run encounters a conflict or invalid scope
- **THEN** it reports failure and performs no writes

### Requirement: Runtime failures stop further mutations

Import SHALL execute cleanup only after all planned creates/updates succeed. On the first mutation
failure it SHALL stop further writes, return failure, and report completed and failed work.
Previously completed changes SHALL NOT be represented as rolled back. Cancellation SHALL prevent
additional mutations.

#### Scenario: Create or update failure
- **WHEN** a create or update fails after earlier successful work
- **THEN** import reports partial results, performs no further mutations, and does not start
  cleanup

#### Scenario: Cleanup failure
- **WHEN** a deletion fails after earlier successful mutations
- **THEN** import reports partial results and attempts no remaining deletions

#### Scenario: Cancellation
- **WHEN** import observes cancellation during execution
- **THEN** it starts no additional mutations and does not report full success
