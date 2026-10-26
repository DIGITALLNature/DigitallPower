# Spec Delta

## ADDED Requirements

### Requirement: Legacy analysis commands are retired
The CLI SHALL NOT register or accept the `analyze` branch or its command paths, including compatibility aliases. Help and shell completion SHALL omit the branch. No replacement command or supported migration for analyzer configuration or report files SHALL be provided.

#### Scenario: A retired analysis command is invoked
- **WHEN** a user invokes `analyze entityallassets`, `analyze noactivelayer`, `analyze activelayer`, `analyze toplayer`, `analyze redundantcomponents`, or `analyze redundantpatches`
- **THEN** parsing fails with a nonzero exit code and an unknown-command diagnostic
- **AND** no Dataverse operation is performed

#### Scenario: The retired analysis surface is inspected
- **WHEN** top-level help or shell-completion metadata is generated
- **THEN** `analyze` and its command paths are absent
- **AND** unrelated registered commands remain available
