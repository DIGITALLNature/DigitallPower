# Plugin Step Management

## Purpose

Specify how `dgtp plugin step enable`, `disable`, and `state set` change the enabled state
of existing Dataverse plugin steps, and how `plugin step config set` updates existing
steps' secure or unsecure configuration.

## Requirements

### Requirement: Steps are identified by exactly one mode

`plugin step enable` and `plugin step disable` MUST accept exactly one identification mode: `--step-id`, a complete composite key (`--plugin-type`, `--message`, `--stage`, `--entity`, optionally `--secondary-entity` and `--execution-order`), or `--solution`. A composite key MUST resolve to exactly one step; no match or multiple matches MUST fail, and multiple matches MUST list step names and IDs.

#### Scenario: A composite key is ambiguous

- **WHEN** the supplied composite key matches more than one step
- **THEN** the command lists the matching steps and fails without changing any step

#### Scenario: Solution mode applies to all steps

- **WHEN** `--solution <name>` is supplied
- **THEN** every plugin step in the solution with that exact unique name is processed
- **AND** an unknown solution fails, and a solution without plugin steps reports that no steps were found

### Requirement: Applying a state is idempotent

A step that is already in the desired state MUST NOT be written and MUST be reported as unchanged. A supplied reason MUST replace the step description only when the state actually changes.

#### Scenario: A step is already enabled

- **WHEN** `plugin step enable` targets an enabled step
- **THEN** no write occurs and the step is counted as unchanged

### Requirement: Dry-run previews without writing

`--dry-run` MUST report which steps would change using the same resolution and validation as a real run, MUST NOT write to Dataverse, and MUST be visibly marked as a dry run. The commands MUST NOT prompt for confirmation.

#### Scenario: A dry-run targets a disabled step

- **WHEN** `plugin step enable --dry-run` targets a disabled step
- **THEN** the step is reported as one that would be enabled
- **AND** its state and description are unchanged

### Requirement: Failures are isolated and visible

A failure for one step or one `state set` entry MUST be reported and counted without aborting the remaining ones, and the command MUST exit unsuccessfully if any step or entry failed. Cancellation MUST NOT be swallowed. User-controlled values in output MUST be escaped.

#### Scenario: One entry cannot be resolved

- **WHEN** one entry in a `state set` config matches no step
- **THEN** that entry is reported as failed and the remaining entries are still processed
- **AND** the summary reports enabled, disabled, unchanged, and failed counts

### Requirement: State configuration is validated

Each `state set` entry MUST set `enabled` explicitly and use either `stepId` or a complete composite key, never both. `stage` MUST be one of 10, 20, 30, or 40. The configuration format MUST be described by `schemas/plugin/state/schema.json`.

#### Scenario: An entry omits enabled

- **WHEN** a config entry has no `enabled` value
- **THEN** the entry is rejected rather than treated as a disable

### Requirement: Solution-wide queries are paged

Solution mode MUST read solution components and step states across all Dataverse result pages, so large solutions are processed completely.

#### Scenario: A solution exceeds one page of components

- **WHEN** a solution contains more components than a single Dataverse page returns
- **THEN** every plugin step in the solution is processed

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
