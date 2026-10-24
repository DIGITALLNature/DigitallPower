# Proposal

## Why

Telemetry state is currently managed only through environment variables (`DGTP_TELEMETRY_OPTOUT`, `DO_NOT_TRACK`) or by manually editing `state.json`. There is no CLI command to view or change the persisted telemetry preference, which makes it difficult for users to discover and manage their telemetry settings without knowing internal file formats.

## What Changes

- Add a new `dgtp telemetry` command branch with three subcommands:
  - `dgtp telemetry status` — displays the current telemetry state (enabled/disabled, install ID presence, environment opt-out status, CI detection)
  - `dgtp telemetry enable` — persistently enables telemetry by setting `telemetry.enabled` to `true` in `state.json`
  - `dgtp telemetry disable` — persistently disables telemetry by setting `telemetry.enabled` to `false` in `state.json`
- Create a new module `dgt.power.telemetry` under `src/modules/` containing the command implementations
- Register the `telemetry` branch in `CommandTree.cs`
- Add the new module to the solution file and the main `dgt.power` project references
- Add a dedicated test project `dgt.power.telemetry.tests`
- Update `CommandTreeTests.cs` and `SettingsParsingTests.cs` with coverage for the new commands
- Update README telemetry section to document the new commands

## Capabilities

### New Capabilities

- `telemetry-state-management`: CLI commands to view and change the persisted telemetry preference without manually editing state files or relying solely on environment variables.

### Modified Capabilities

- `cli-command-surface`: The command tree gains a new top-level `telemetry` branch; shell completion and structural tests must cover it.

## Impact

- New project: `src/modules/dgt.power.telemetry/dgt.power.telemetry.csproj` referencing `dgt.power.common`
- New test project: `tests/dgt.power.telemetry.tests/dgt.power.telemetry.tests.csproj`
- Modified: `src/dgt.power/CommandTree.cs` (register new branch)
- Modified: `src/dgt.power/dgt.power.csproj` (add project reference)
- Modified: `DigitallPower.slnx` (add new projects)
- Modified: `tests/dgt.power.cli.tests/CommandTreeTests.cs` and `SettingsParsingTests.cs`
- Modified: `README.md` (document new commands)
- No Dataverse connection is required for any telemetry management command
- No breaking changes to existing telemetry behavior; environment opt-outs and `--no-telemetry` continue to take precedence
