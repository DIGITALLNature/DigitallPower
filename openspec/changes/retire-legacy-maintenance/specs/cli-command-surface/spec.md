# Spec Delta

## ADDED Requirements

### Requirement: Bulk-delete execution is retired without replacement

The CLI SHALL NOT register or accept `maintenance bulkdelete`, including compatibility aliases. Help and completion SHALL omit it. No replacement command SHALL be provided. Retiring this command SHALL NOT cancel, disable, or delete existing Dataverse jobs.

#### Scenario: Retired bulk-delete execution is invoked
- **WHEN** a user invokes `dgtp maintenance bulkdelete`
- **THEN** parsing fails with a nonzero exit code and an unknown-command diagnostic
- **AND** no Dataverse operation is performed

#### Scenario: Bulk-delete execution is absent before container retirement
- **WHEN** help or completion is generated while other maintenance commands remain registered
- **THEN** `maintenance bulkdelete` is absent
- **AND** unrelated registered maintenance commands remain available

### Requirement: Retired maintenance surface is absent

The CLI SHALL NOT register or accept the `maintenance` branch, including compatibility aliases. Help and completion SHALL omit it. Independently migrated process commands SHALL remain available. Deferred Solution feature requests SHALL NOT be prerequisites or advertised as supported replacements.

#### Scenario: All approved standalone maintenance commands are retired
- **WHEN** a user invokes `maintenance bulkdelete`, `maintenance autonumber`, `maintenance protectfields`, `maintenance carrierinfo`, `maintenance removeredundantcomponents`, or `maintenance filterfxplugins`
- **THEN** parsing fails with a nonzero exit code and an unknown-command diagnostic
- **AND** no Dataverse operation is performed

#### Scenario: Retirement preserves existing environment state
- **WHEN** a user upgrades to the CLI without the maintenance branch
- **THEN** retirement does not modify jobs, auto-number formats, managed properties, carrier data, solution memberships, or plugin filtering attributes

#### Scenario: Legacy maintenance command is invoked after retirement
- **WHEN** a user invokes a command under `maintenance`
- **THEN** parsing fails with a nonzero exit code and an unknown-command diagnostic
- **AND** no Dataverse operation is performed

#### Scenario: Retired surface is inspected
- **WHEN** top-level help or completion metadata is generated
- **THEN** `maintenance` is absent
- **AND** approved resource-oriented replacements remain registered
