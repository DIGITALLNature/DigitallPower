# CLI Command Surface

## Purpose

Keep the production CLI, shell completion, and command tests aligned on the same command structure.

## Requirements

### Requirement: Runtime and shell completion share command registration

The production `dgtp` application and shell-completion model MUST derive their command metadata from the same command-tree registration implementation.

#### Scenario: A command is added or changed

- **WHEN** a command path, alias, or option is registered
- **THEN** the runtime CLI and shell completion expose the same command structure

### Requirement: Command paths have structural smoke coverage

The CLI test suite MUST validate command-tree construction and smoke-test registered command paths without requiring a live Dataverse environment.

#### Scenario: The command tree is validated

- **WHEN** the CLI structural tests run
- **THEN** command registration and representative command paths are checked without bootstrapping external services

### Requirement: Secure configuration import is resource-oriented

The CLI MUST NOT register or accept `import secureconfigs`, including as a compatibility
alias. Help and generated shell-completion metadata MUST NOT advertise it. The
supported command for setting secure plugin-step configuration MUST remain
`plugin step config set`.

#### Scenario: A legacy import is invoked

- **WHEN** a user invokes `dgtp import secureconfigs`
- **THEN** command parsing fails with a nonzero exit code and an unknown-command diagnostic
- **AND** no Dataverse operation is performed

#### Scenario: The import surface is inspected

- **WHEN** CLI help or shell-completion command metadata is generated
- **THEN** `import secureconfigs` is absent
- **AND** approved resource-oriented replacements for retained transfer functionality remain available

#### Scenario: The replacement remains available

- **WHEN** a user invokes `dgtp plugin step config set --help`
- **THEN** help succeeds and describes the existing secure-configuration options

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

### Requirement: Bulk-delete transfer is retired without replacement

The CLI SHALL NOT register or accept `import bulkdeletes` or `export bulkdeletes`, including compatibility aliases. Help and completion SHALL omit both paths. No replacement command or supported migration of `bulkdelete.json` SHALL be provided. Retiring these commands SHALL NOT modify existing Dataverse jobs.

#### Scenario: Retired bulk-delete transfer is invoked
- **WHEN** a user invokes `dgtp import bulkdeletes` or `dgtp export bulkdeletes`
- **THEN** parsing fails with a nonzero exit code and an unknown-command diagnostic
- **AND** no Dataverse operation is performed

#### Scenario: Bulk-delete transfer is absent before container retirement
- **WHEN** help or completion is generated while other legacy transfer resources remain registered
- **THEN** both bulk-delete transfer paths are absent
- **AND** unrelated registered transfer commands remain available

### Requirement: Retired verb-first transfer surface is absent

The CLI SHALL NOT register or accept top-level `import` or `export` branches, including compatibility aliases. Help and completion SHALL omit them and preserve independently migrated resource-oriented commands.

#### Scenario: Approved legacy resource pairs are retired
- **WHEN** a user invokes either old transfer direction for bulk deletes, queues, calendars, SLA configurations, routing-rule configurations, user-role assignments, or Outlook synchronization-filter templates
- **THEN** parsing fails with a nonzero exit code without a Dataverse operation
- **AND** no replacement command or supported migration of their legacy JSON formats is provided

#### Scenario: Retirement preserves existing environment data
- **WHEN** a user upgrades to the CLI without the legacy transfer branches
- **THEN** retirement does not modify existing Dataverse jobs, queues, calendars, SLAs, routing rules, role assignments, or Outlook filters

#### Scenario: Migrated template resources remain available
- **WHEN** a user requests help for `teamtemplates import`, `teamtemplates export`, `documenttemplates import`, or `documenttemplates export`
- **THEN** the corresponding resource-oriented help remains available

#### Scenario: Legacy transfer command is invoked after retirement
- **WHEN** a user invokes a command under top-level `import` or `export`
- **THEN** parsing fails with a nonzero exit code and an unknown-command diagnostic
- **AND** no Dataverse operation is performed

#### Scenario: Transfer surface is inspected
- **WHEN** top-level help or completion metadata is generated
- **THEN** the legacy transfer branches are absent
- **AND** approved resource-oriented transfer commands remain registered

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
