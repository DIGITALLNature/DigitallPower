# Spec Delta

## Purpose

Transfer Dataverse teamtemplates through resource-oriented commands with explicit control over synchronization and removal.

## ADDED Requirements

### Requirement: Teamtemplate transfer is resource-oriented

The CLI SHALL expose `teamtemplates import` and `teamtemplates export`. It SHALL NOT accept `import teamtemplates` or `export teamtemplates`, including compatibility aliases. Help and completion SHALL advertise only the new paths.

#### Scenario: New transfer paths
- **WHEN** help is requested for either resource-oriented command
- **THEN** help describes the corresponding transfer operation

#### Scenario: Legacy transfer path
- **WHEN** either replaced verb-first command is invoked
- **THEN** parsing fails without a Dataverse write

### Requirement: Export provides importable resource data

Export SHALL produce teamtemplate data usable by the corresponding import contract, preserving the template's configured table association and access-rights intent across environments.

#### Scenario: Template transfer
- **WHEN** exported template data is imported into a compatible target environment
- **THEN** the resulting template preserves its configured table association and access rights without relying on equal environment-specific object type codes

### Requirement: Import leaves unlisted teamtemplates unchanged by default

Import SHALL create or update listed teamtemplates without deleting or modifying templates solely because they are absent from the file. Reapplying unchanged input SHALL avoid unnecessary mutations.

#### Scenario: Partial import
- **WHEN** the target contains an unlisted template and cleanup is not explicitly requested
- **THEN** that template remains unchanged

#### Scenario: Repeated import
- **WHEN** a listed template already matches the input
- **THEN** import does not unnecessarily recreate or update it

### Requirement: Cleanup is explicit and bounded

Removal of unlisted teamtemplates SHALL require an explicit cleanup request and an explicitly bounded target scope. Templates outside that scope SHALL remain unchanged. An absent or invalid cleanup scope SHALL be rejected before removal.

#### Scenario: Scoped cleanup
- **WHEN** cleanup is explicitly requested for a valid target scope
- **THEN** only unlisted templates inside that scope are eligible for removal

#### Scenario: Cleanup lacks a scope
- **WHEN** cleanup is requested without a valid explicit scope
- **THEN** import fails without removing templates
