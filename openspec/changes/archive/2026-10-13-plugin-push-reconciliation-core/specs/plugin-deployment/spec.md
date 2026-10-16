# Spec Delta

## ADDED Requirements

### Requirement: Targets are processed as a batch

`plugin push` MUST accept a file, directory, or glob and process every matched plugin artifact. Duplicate declarations of the same Custom API or data provider across targets MUST be rejected before any artifact is written. After validation, a failing target MUST NOT stop the remaining targets but MUST make the command fail, and cancellation MUST stop the run.

#### Scenario: Two targets declare the same Custom API

- **WHEN** a batch contains two artifacts that declare the same Custom API
- **THEN** the batch is rejected before any artifact is written

#### Scenario: One target fails during deployment

- **WHEN** the first of several targets fails while being applied
- **THEN** the remaining targets are still processed
- **AND** the command exits unsuccessfully

### Requirement: Registrations are reconciled within a defined ownership scope

`plugin push` MUST reconcile the non-internal steps and images of every plugin type in the deployed artifact against declarations and MUST update only declared fields. Steps are matched by message, stage, mode, and primary/secondary entity; name, execution order, filtering attributes, and images MUST NOT affect matching. Duplicate local declarations and ambiguous remote matches MUST fail before any write. Changing a matching field MUST create a new step. Platform-managed steps and internal stages MUST be retained. Step configuration, enabled state, and impersonation of matched steps MUST be preserved.

#### Scenario: A step's execution order changes

- **WHEN** only the execution order of a declared step differs from the registered step
- **THEN** the existing step is updated in place

#### Scenario: A step's stage changes

- **WHEN** the stage of a declared step differs from every registered step
- **THEN** a new step is created and the old step is removed

### Requirement: Cleanup runs last and is guarded

Obsolete steps, types, and assemblies MUST be removed only after registration writes, Custom API and data-provider associations, and solution membership have been applied. Before a type is deleted, remaining dependent steps, Custom API links, and provider references MUST be checked; before an assembly is deleted, remaining types MUST be checked. A failure MUST leave superseded records in place so that a later push can complete the replacement.

#### Scenario: An association write fails

- **WHEN** an association write fails during a replacement
- **THEN** the superseded types and assembly are not deleted

### Requirement: Standalone assemblies are replaced with conservative recovery

A standalone assembly with a different major or minor version MUST be replaced by a new record; steps and images of matching types MUST be reassigned to it before the old assembly is removed. An interrupted replacement MUST be completed only when exactly one other same-name assembly exists and the target assembly's content matches the artifact. Any other combination of same-name assemblies MUST fail. A step tuple present on both assemblies MUST fail as ambiguous.

#### Scenario: An interrupted replacement is retried

- **WHEN** both the old and the new standalone assembly exist and the new one matches the artifact
- **THEN** the push completes the replacement without creating duplicate registrations

#### Scenario: Unrelated same-name assemblies exist

- **WHEN** more than one other same-name assembly exists or the new assembly differs from the artifact
- **THEN** the push fails without writing

### Requirement: Plugin packages are reconciled through upload

For plugin packages, `plugin push` MUST use the package upload to create or update package-owned assemblies and types, MUST fail when expected server-created records are missing or unexpected assemblies exist, and MUST NOT re-upload identical content on retry. Solution membership for a package MUST cover the package and its declared steps, not its owned assemblies.

#### Scenario: A package is pushed again unchanged

- **WHEN** the package content matches the deployed package
- **THEN** no upload occurs

### Requirement: Custom API associations follow declarations

`plugin push` MUST NOT create or delete Custom API records. An explicit declaration MUST link the API to the declared plugin type even when another target currently handles it. An API handled by a type owned by the target and no longer declared MUST be unlinked; other associations MUST be left untouched. Duplicate declarations, missing APIs, and ambiguous unique names MUST fail before any artifact write. Moving a handler between assemblies of one package MUST NOT produce both a link and an unlink.

#### Scenario: A declaration is removed

- **WHEN** a Custom API is no longer declared and its handler is owned by the target
- **THEN** the association is unlinked and the API record remains

### Requirement: Data-provider declarations own handler configuration

A data-provider declaration MUST own all handler slots of the provider identified by its data-source logical name, and undeclared slots MUST be cleared even when they reference external types. If a provider is no longer declared, only handlers referring to types owned by the target MUST be cleared. Provider records and data-source tables MUST NOT be deleted. Solution membership MUST add only missing provider records and MUST NOT add data-source table schema or rows. Competing declarations of one provider MUST be rejected.

#### Scenario: A provider is no longer declared

- **WHEN** a provider declaration is removed from the artifact
- **THEN** handlers referring to target-owned types are cleared
- **AND** the provider record and its data-source table remain

### Requirement: Managed identities are environment-owned

`plugin push` MUST NOT create, bind, unbind, transfer, or delete managed identities. Existing bindings on target records MUST be preserved, and a replacement assembly MUST NOT inherit a binding from the superseded assembly.

#### Scenario: A standalone assembly is replaced

- **WHEN** a new assembly record replaces the old one
- **THEN** no managed identity binding is copied to the new record

### Requirement: Reconciliation reads are complete

Every Dataverse read that reconciliation depends on, including assemblies, types, steps, images, Custom APIs, solution components, and component definitions, MUST read all result pages.

#### Scenario: A result exceeds one page

- **WHEN** a query returns more records than one Dataverse page
- **THEN** all pages are read before a decision is made
