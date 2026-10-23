# Spec Delta

## ADDED Requirements

### Requirement: Interactive command phases do not overlap

A command MUST complete initial Dataverse connection acquisition and required sign-in before starting its Dataverse work display. User confirmations MUST occur outside active work displays. Connection acquisition MUST remain lazy so local validation failures and commands without Dataverse work do not trigger authentication.

#### Scenario: Codegeneration establishes its connection

- **WHEN** codegeneration runs with a valid configuration and no established connection
- **THEN** initial connection acquisition and required sign-in complete before its generation display starts
- **AND** generation does not fail due to overlapping interactive displays

#### Scenario: A command requests confirmation

- **WHEN** a command asks the user to confirm a remote operation
- **THEN** no work display is active during the confirmation

#### Scenario: Local configuration is invalid

- **WHEN** codegeneration rejects its configuration before remote work is needed
- **THEN** no authentication or Dataverse connection is attempted

#### Scenario: Initial connection acquisition fails

- **WHEN** a Dataverse command cannot establish its initial connection
- **THEN** the connection error propagates through normal command error handling
- **AND** the remote-work phase does not start
