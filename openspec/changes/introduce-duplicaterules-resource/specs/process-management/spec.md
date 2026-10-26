# Spec Delta

## MODIFIED Requirements

### Requirement: Commands use explicit paths and options

Both commands SHALL accept a positional file path resolved against the working directory. Init SHALL require one or more `--solution` values and SHALL expose `--capture state|owner|both` (default `state`), `--owner-mapping`, `--unmapped-owners fail|skip` (default `fail`), and `--overwrite`. Apply SHALL expose `--owner-mapping`, mutually exclusive `--activate-as <OwnerKey>` and `--activate-as-owner`, and `--dry-run`. Option conflicts and invalid option values SHALL fail before Dataverse access.

#### Scenario: Init without solution
- **WHEN** init is invoked without `--solution`
- **THEN** validation fails without Dataverse access

#### Scenario: Conflicting impersonation options
- **WHEN** apply receives both `--activate-as` and `--activate-as-owner`
- **THEN** validation fails without Dataverse access

#### Scenario: Invalid unmapped-owner handling
- **WHEN** init receives an `--unmapped-owners` value other than exactly `fail` or `skip`
- **THEN** validation fails without Dataverse access

### Requirement: Capture scope is selectable

`process init` SHALL capture state, ownership, or both. Ownership SHALL be captured as logical keys through the source owner mapping. Each captured owner SHALL reverse-map to exactly one key, except that owners skipped by the shared owner-capture contract SHALL be emitted without `owner`: with `both`, the entry keeps `active`; with `owner`, the entry is omitted. Init SHALL warn about every skipped record, grouped by principal. Existing output SHALL NOT be replaced unless `--overwrite` is set, and SHALL NOT be created or replaced after any selection or mapping error.

#### Scenario: State-only capture
- **WHEN** a user captures only state
- **THEN** the generated configuration records `active` without `owner` and requires no owner mapping

#### Scenario: Ownership capture
- **WHEN** a user captures ownership
- **THEN** each entry records the owner's logical key rather than a source-environment GUID

#### Scenario: Unmapped owner
- **WHEN** a captured owner that is not a platform principal has no logical key in the source mapping and `--unmapped-owners` is `fail`
- **THEN** init fails without writing output

#### Scenario: Unmapped owner skipped
- **WHEN** a captured owner has no logical key and `--unmapped-owners skip` is set
- **THEN** init warns and writes the affected processes without `owner`, omitting them for owner-only capture

#### Scenario: Platform-owned process
- **WHEN** a captured process is owned by the SYSTEM or INTEGRATION platform principal without a matching key
- **THEN** init warns and writes it without `owner`, omitting it for owner-only capture

#### Scenario: Existing output
- **WHEN** the output file exists and `--overwrite` is not set
- **THEN** init fails and the file is unchanged
