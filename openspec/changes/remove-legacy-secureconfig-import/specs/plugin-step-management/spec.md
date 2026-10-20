# Spec Delta

## ADDED Requirements

### Requirement: Configuration uses explicit step identification

`plugin step config set` MUST accept either `--step-id` or a complete composite key
(`--plugin-type`, `--message`, `--stage`, `--entity`, optionally `--secondary-entity` and
`--execution-order`), never both. A composite key MUST match exactly one step; zero or
multiple matches MUST fail without changing configuration.

#### Scenario: A secure configuration targets a step ID

- **WHEN** a user supplies an existing step ID and a secure configuration value
- **THEN** that step's secure configuration is created or updated

#### Scenario: A composite key is ambiguous

- **WHEN** the supplied composite key matches multiple steps
- **THEN** the command fails and lists matching step names and IDs without changing configuration

#### Scenario: Identification modes are mixed

- **WHEN** a user supplies both a step ID and composite-key options
- **THEN** validation fails before Dataverse access

### Requirement: Configuration accepts values or raw files

`plugin step config set` MUST accept secure values through `--secure` or `--secure-file`
and unsecure values through `--unsecure` or `--unsecure-file`. Inline and file input for
the same configuration MUST be mutually exclusive. Files MUST be read as UTF-8 text,
not interpreted as legacy configuration envelopes. An omitted configuration MUST remain
unchanged.

#### Scenario: A secure value is read from a file

- **WHEN** a user selects an existing step and supplies `--secure-file`
- **THEN** the file's text becomes the secure configuration value
- **AND** the unsecure configuration is unchanged unless explicitly supplied

#### Scenario: A value is explicitly empty

- **WHEN** an empty secure or unsecure value is explicitly supplied
- **THEN** the selected configuration is set to an empty value rather than treated as omitted
