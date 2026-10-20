# Spec Delta

## ADDED Requirements

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
