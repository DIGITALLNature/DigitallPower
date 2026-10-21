# Spec Delta

## ADDED Requirements

### Requirement: Retired verb-first transfer surface is absent

After every legacy transfer resource has an approved disposition and retained functionality has migrated, the CLI SHALL NOT register or accept top-level `import` or `export` branches, including compatibility aliases. Help and completion SHALL omit them and retain approved resource-oriented replacements.

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
