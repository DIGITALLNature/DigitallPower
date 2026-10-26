# Spec Delta

## ADDED Requirements

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

## MODIFIED Requirements

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
