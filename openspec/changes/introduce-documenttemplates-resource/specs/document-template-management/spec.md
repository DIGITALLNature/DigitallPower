# Spec Delta

## Purpose

Transfer Word and Excel documenttemplates between Dataverse environments with explicit lifecycle and cleanup behavior.

## ADDED Requirements

### Requirement: Documenttemplate transfer is resource-oriented

The CLI SHALL expose `documenttemplates import` and `documenttemplates export`. It SHALL NOT accept `import documenttemplates` or `export documenttemplates`, including compatibility aliases. Help and completion SHALL advertise the replacement paths.

#### Scenario: Resource help
- **WHEN** help is requested for the new commands
- **THEN** their import or export operation is described

#### Scenario: Replaced path is invoked
- **WHEN** either legacy documenttemplate path is invoked
- **THEN** parsing fails without a Dataverse write

### Requirement: Document transfer preserves template intent

Export and import SHALL support Word and Excel documenttemplates with their configured document content, metadata, and status. Word template references that depend on target table metadata SHALL be adapted for the target rather than blindly preserving source object type codes.

#### Scenario: Word template crosses environments
- **WHEN** a Word template is imported into a compatible target with a different table object type code
- **THEN** supported table references in the resulting document use the target metadata

#### Scenario: Excel template transfer
- **WHEN** an exported Excel template is imported
- **THEN** its configured content and metadata remain usable under the transfer contract

### Requirement: Unlisted documenttemplates remain unchanged by default

Import SHALL NOT deactivate, delete, or otherwise modify a documenttemplate solely because it is absent from the input unless explicit scoped cleanup is requested.

#### Scenario: Partial documenttemplate import
- **WHEN** import runs without cleanup and the target contains an unlisted active template
- **THEN** that template remains active and unchanged

### Requirement: Documenttemplate cleanup is explicit and bounded

Cleanup of unlisted documenttemplates SHALL require an explicit request and valid explicit target scope. Templates outside that scope SHALL remain unchanged. Missing or invalid scope SHALL be rejected before cleanup mutations.

#### Scenario: Scoped cleanup
- **WHEN** cleanup is requested for a valid scope
- **THEN** only unlisted templates inside the scope are eligible for cleanup

#### Scenario: Cleanup scope is missing
- **WHEN** cleanup is requested without a valid explicit scope
- **THEN** no cleanup mutation is performed and the command fails
