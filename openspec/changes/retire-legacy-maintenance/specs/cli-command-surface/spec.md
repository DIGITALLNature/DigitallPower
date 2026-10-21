# Spec Delta

## ADDED Requirements

### Requirement: Retired maintenance surface is absent

After all maintenance functionality has an approved disposition and retained functionality has migrated, the CLI SHALL NOT register or accept the `maintenance` branch, including compatibility aliases. Help and completion SHALL omit it. Resource-oriented replacements SHALL remain available.

#### Scenario: Legacy maintenance command is invoked after retirement
- **WHEN** a user invokes a command under `maintenance`
- **THEN** parsing fails with a nonzero exit code and an unknown-command diagnostic
- **AND** no Dataverse operation is performed

#### Scenario: Retired surface is inspected
- **WHEN** top-level help or completion metadata is generated
- **THEN** `maintenance` is absent
- **AND** approved resource-oriented replacements remain registered
