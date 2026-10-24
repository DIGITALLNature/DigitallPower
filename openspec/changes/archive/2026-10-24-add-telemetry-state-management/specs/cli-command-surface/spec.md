# Spec Delta

## ADDED Requirements

### Requirement: Telemetry command branch is registered

The CLI MUST register a top-level `telemetry` branch with `status`, `enable`, and `disable` subcommands. The branch and its commands MUST appear in help output and shell completion metadata.

#### Scenario: Telemetry branch help is displayed

- **WHEN** the user runs `dgtp telemetry --help`
- **THEN** the help lists the `status`, `enable`, and `disable` subcommands

#### Scenario: Telemetry subcommand help is displayed

- **WHEN** the user runs `dgtp telemetry status --help`
- **THEN** the help succeeds and describes the status command

#### Scenario: Shell completion includes telemetry commands

- **WHEN** shell completion metadata is generated
- **THEN** the `telemetry` branch and its subcommands are included
