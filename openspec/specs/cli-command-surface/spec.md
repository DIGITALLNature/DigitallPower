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
alias. Import help and generated shell-completion metadata MUST NOT advertise it. The
supported command for setting secure plugin-step configuration MUST remain
`plugin step config set`.

#### Scenario: A legacy import is invoked

- **WHEN** a user invokes `dgtp import secureconfigs`
- **THEN** command parsing fails with a nonzero exit code and an unknown-command diagnostic
- **AND** no Dataverse operation is performed

#### Scenario: The import surface is inspected

- **WHEN** import help or shell-completion command metadata is generated
- **THEN** `secureconfigs` is absent
- **AND** the other registered import commands remain available

#### Scenario: The replacement remains available

- **WHEN** a user invokes `dgtp plugin step config set --help`
- **THEN** help succeeds and describes the existing secure-configuration options