# Spec Delta

## Purpose

Provide CLI commands to view and change the persisted telemetry preference, so users can manage telemetry state without editing internal state files or relying solely on environment variables.

## ADDED Requirements

### Requirement: Telemetry status reports effective state with visual indicator

`dgtp telemetry status` MUST display the telemetry notice panel first, followed by the effective telemetry state on a single line prefixed with a colored circle indicator: green for enabled, red for disabled. The output MUST include a hint showing the command to change the state. When an environment opt-out (`DGTP_TELEMETRY_OPTOUT` or `DO_NOT_TRACK`) overrides the persisted preference, a second line MUST state that the environment variable is set. It MUST NOT require a Dataverse connection.

#### Scenario: Telemetry is enabled

- **WHEN** the user runs `dgtp telemetry status` with telemetry enabled and no environment opt-outs
- **THEN** the telemetry notice panel is displayed
- **AND** the output shows a green circle followed by "Telemetry is enabled."
- **AND** a line "To disable: dgtp telemetry disable"

#### Scenario: Telemetry is disabled in persisted state

- **WHEN** the user runs `dgtp telemetry status` with `telemetry.enabled` set to `false` in `state.json`
- **THEN** the telemetry notice panel is displayed
- **AND** the output shows a red circle followed by "Telemetry is disabled."
- **AND** a line "To enable: dgtp telemetry enable"

#### Scenario: Telemetry is overridden by environment variable

- **WHEN** the user runs `dgtp telemetry status` with `telemetry.enabled` set to `true` but `DGTP_TELEMETRY_OPTOUT` or `DO_NOT_TRACK` is set
- **THEN** the telemetry notice panel is displayed
- **AND** the output shows a red circle followed by "Telemetry is disabled."
- **AND** a second line "DGTP_TELEMETRY_OPTOUT / DO_NOT_TRACK is set."

### Requirement: Telemetry enable persists the preference

`dgtp telemetry enable` MUST set `telemetry.enabled` to `true` in `state.json` and print a confirmation line prefixed with a green circle indicator. It MUST NOT require a Dataverse connection. If an environment opt-out is active, the command MUST still persist the preference but print the confirmation with a warning emoji instead of a green circle, followed by a second line stating which environment variables are set.

#### Scenario: Enabling telemetry

- **WHEN** the user runs `dgtp telemetry enable`
- **THEN** `state.json` is updated with `telemetry.enabled: true`
- **AND** the output shows a green circle followed by "Telemetry is now enabled."

#### Scenario: Enabling telemetry while environment opt-out is active

- **WHEN** the user runs `dgtp telemetry enable` with `DGTP_TELEMETRY_OPTOUT` or `DO_NOT_TRACK` set
- **THEN** `state.json` is updated with `telemetry.enabled: true`
- **AND** the output shows a warning emoji followed by "Telemetry is now enabled."
- **AND** a second line "DGTP_TELEMETRY_OPTOUT / DO_NOT_TRACK is set."

### Requirement: Telemetry disable persists the preference

`dgtp telemetry disable` MUST set `telemetry.enabled` to `false` in `state.json` and print a confirmation line prefixed with a red circle indicator. It MUST NOT require a Dataverse connection. The anonymous install ID MUST be preserved when telemetry is disabled.

#### Scenario: Disabling telemetry

- **WHEN** the user runs `dgtp telemetry disable`
- **THEN** `state.json` is updated with `telemetry.enabled: false`
- **AND** the output shows a red circle followed by "Telemetry is now disabled."
- **AND** the existing install ID is preserved in `state.json`

### Requirement: Environment opt-outs take precedence over persisted preference

The effective telemetry state MUST be determined by environment opt-outs first, then the persisted preference. `dgtp telemetry status` MUST reflect this precedence. The `--no-telemetry` flag MUST continue to suppress telemetry for a single invocation regardless of the persisted preference.

#### Scenario: Environment opt-out overrides enabled preference

- **WHEN** `telemetry.enabled` is `true` in `state.json` but `DGTP_TELEMETRY_OPTOUT` is set
- **THEN** telemetry is effectively disabled

#### Scenario: Per-invocation suppression overrides enabled preference

- **WHEN** a command is run with `--no-telemetry` while `telemetry.enabled` is `true`
- **THEN** no telemetry span is produced for that invocation

### Requirement: Telemetry commands do not authenticate to Dataverse

The `dgtp telemetry` commands MUST NOT request or establish a Dataverse connection. They MUST complete without network access to Dataverse.

#### Scenario: Running telemetry status offline

- **WHEN** the user runs `dgtp telemetry status` without any stored connections
- **THEN** the command completes successfully without attempting authentication
