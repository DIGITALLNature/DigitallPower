# Plugin Step Management

## Purpose

Specify how `dgtp plugin step enable`, `disable`, and `state set` change the enabled state of existing Dataverse plugin steps.

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

### Requirement: Solution-wide queries are not yet paged

Solution mode SHALL be treated as unpaged for now: it reads solution components and step states without paging, and `todo.md` tracks adopting the shared paging helper from the plugin push rewrite. Until then, very large solutions MAY be processed incompletely.

#### Scenario: A solution exceeds one page of components

- **WHEN** a solution contains more components than a single Dataverse page returns
- **THEN** behavior is limited by the known gap tracked in `todo.md`
