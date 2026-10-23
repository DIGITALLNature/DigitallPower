# Spec Delta

## Purpose

Capture and apply explicit process state and ownership across Dataverse environments without implicit changes to unrelated processes.

## ADDED Requirements

### Requirement: Process configuration is resource-oriented

The CLI SHALL expose `process init` and `process apply` for classic workflows, business rules, actions, business process flows, and cloud flows. It SHALL NOT accept the replaced `maintenance createworkflowstate` or `maintenance workflowstate` commands as aliases.

#### Scenario: Legacy workflow command is invoked
- **WHEN** either replaced maintenance command is invoked
- **THEN** parsing fails without a Dataverse write
- **AND** help and completion advertise the resource-oriented replacement instead

### Requirement: Processes are identified by workflow ID

Configuration SHALL identify each process only by its Dataverse `workflowid` as `processId`. An optional `name` SHALL be informational and SHALL NOT be used for matching. Apply SHALL fail preflight for an ID that does not exist in the target, is not a process definition, or belongs to an unsupported category such as dialog, AI flow, or desktop flow.

#### Scenario: Name differs in the target
- **WHEN** a configured process exists in the target with the same ID but a different localized name
- **THEN** apply targets that process

#### Scenario: Process ID is missing in the target
- **WHEN** no definition with a configured process ID exists in the target
- **THEN** preflight fails without writes, even if a process with the same name exists

#### Scenario: Unsupported category
- **WHEN** a configured process ID belongs to a dialog
- **THEN** preflight fails without writes

### Requirement: Commands use explicit paths and options

Both commands SHALL accept a positional file path resolved against the working directory. Init SHALL require one or more `--solution` values and SHALL expose `--capture state|owner|both` (default `state`), `--owner-mapping`, and `--overwrite`. Apply SHALL expose `--owner-mapping`, mutually exclusive `--activate-as <OwnerKey>` and `--activate-as-owner`, and `--dry-run`. Option conflicts SHALL fail before Dataverse access.

#### Scenario: Init without solution
- **WHEN** init is invoked without `--solution`
- **THEN** validation fails without Dataverse access

#### Scenario: Conflicting impersonation options
- **WHEN** apply receives both `--activate-as` and `--activate-as-owner`
- **THEN** validation fails without Dataverse access

### Requirement: Configuration expresses strict V1 intent

Configuration SHALL require `schemaVersion: 1` and a `processes` array, with optional root `$schema` URI. Each entry SHALL require a GUID `processId` and at least one of boolean `active` or logical-key `owner`, and MAY contain a string `name`. Null values, duplicate IDs or properties, unknown fields, unsupported versions, root-level defaults, and blank or whitespace-padded owner keys SHALL be rejected before Dataverse access.

#### Scenario: No requested effect
- **WHEN** an entry contains only `processId` and `name`
- **THEN** local validation fails

#### Scenario: Empty configuration
- **WHEN** apply receives a valid empty `processes` array
- **THEN** it succeeds with no changes and without authenticating

### Requirement: Capture selects by solution

`process init` SHALL select supported process definitions that are components of any selected solution, matched by exact unique name, plus business rules of tables that a selected solution includes with all subcomponents. It SHALL retrieve the complete selection with paging. It SHALL NOT support wildcard, publisher, or managed-state filters.

#### Scenario: Unknown solution
- **WHEN** a selected solution does not exist
- **THEN** init fails without creating or replacing output

#### Scenario: Table included with all subcomponents
- **WHEN** a selected solution contains a table with all subcomponents and that table has a business rule that is not a separate solution component
- **THEN** the business rule is captured

#### Scenario: No matching processes
- **WHEN** the selected solutions contain no supported processes
- **THEN** init succeeds and writes an empty `processes` array

### Requirement: Capture scope is selectable

`process init` SHALL capture state, ownership, or both. Ownership SHALL be captured as logical keys through the source owner mapping. Each captured owner SHALL reverse-map to exactly one key. Existing output SHALL NOT be replaced unless `--overwrite` is set, and SHALL NOT be created or replaced after any selection or mapping error.

#### Scenario: State-only capture
- **WHEN** a user captures only state
- **THEN** the generated configuration records `active` without `owner` and requires no owner mapping

#### Scenario: Ownership capture
- **WHEN** a user captures ownership
- **THEN** each entry records the owner's logical key rather than a source-environment GUID

#### Scenario: Unmapped owner
- **WHEN** a captured owner has no logical key in the source mapping
- **THEN** init fails without writing output

#### Scenario: Existing output
- **WHEN** the output file exists and `--overwrite` is not set
- **THEN** init fails and the file is unchanged

### Requirement: Application changes only explicit intent

`process apply` SHALL leave unlisted processes and omitted properties unchanged. Reapplying an already satisfied configuration SHALL perform no writes. An owner-only update SHALL preserve the original activation state after any necessary intermediate transition.

#### Scenario: Owner-only update
- **WHEN** a listed active process requests a different owner but omits `active`
- **THEN** successful application changes the owner and leaves the process active

#### Scenario: Partial configuration
- **WHEN** a process or property is omitted
- **THEN** application does not infer an activation or ownership change for it

#### Scenario: Desired values already match
- **WHEN** all configured values already match the target
- **THEN** application succeeds without writes

### Requirement: Ownership and impersonation are explicit

Owners SHALL be resolved through the target owner mapping and SHALL be regular or application users; teams SHALL fail preflight. Impersonation SHALL be selected per run: `--activate-as` runs all state transitions as the mapped principal, and `--activate-as-owner` runs a process's state transitions as its configured owner. Owner assignment SHALL run as the connection identity. Requested impersonation SHALL NOT fall back to the connection identity.

#### Scenario: Team owner
- **WHEN** a configured owner key maps to a team
- **THEN** preflight fails without writes

#### Scenario: Activate as owner without owner
- **WHEN** `--activate-as-owner` is set and an entry has `active` but no `owner`
- **THEN** preflight fails without writes

#### Scenario: Activate as mapped principal
- **WHEN** `--activate-as` names a mapped application user and a process is activated
- **THEN** the activation runs as that user and any owner assignment runs as the connection identity

#### Scenario: Impersonation is unavailable
- **WHEN** an impersonated service cannot be obtained for the requested identity
- **THEN** apply fails before any write instead of continuing without impersonation

### Requirement: Preflight and dry-run precede mutations

Apply SHALL validate the file, options, and mapping locally, then resolve all processes and identities before any write. Missing, unsupported, or ambiguous references SHALL fail preflight. Dry-run SHALL use the same resolution and preflight, report planned transitions per process, be visibly marked, perform no writes, and perform no retry rounds.

#### Scenario: Unresolvable owner key
- **WHEN** a configured owner key is missing from the target mapping
- **THEN** preflight fails and no process is changed

#### Scenario: Dry-run
- **WHEN** a valid configuration is applied in dry-run mode
- **THEN** the intended transitions are reported without Dataverse writes

### Requirement: Dependent activations are retried in bounded rounds

Apply SHALL retry failed activations in further rounds while each round reduces the number of failed activations, and SHALL stop when a round makes no progress. Assignment, deactivation, and restoration failures SHALL NOT be retried.

#### Scenario: Parent flow depends on child flow
- **WHEN** a parent flow activation fails because its child flow is not yet active, and the child activates later in the same round
- **THEN** the parent activation succeeds in a later round

#### Scenario: No progress
- **WHEN** a retry round activates no additional process
- **THEN** retrying stops and the remaining activations are reported as failed

### Requirement: Partial failures remain failures

After successful preflight, apply SHALL continue independent processes after a process's final failure. If owner assignment fails after an intermediate deactivation, apply SHALL attempt exactly one reactivation to restore the original state. It SHALL report completed changes, failures, and unrecovered intermediate changes, and exit unsuccessfully if any requested operation fails. It SHALL NOT promise atomic rollback.

#### Scenario: Assignment fails on an active process
- **WHEN** a process was deactivated for an owner change and assignment fails
- **THEN** apply attempts one reactivation
- **AND** reports the assignment failure and whether the original state was restored

#### Scenario: One process fails after another succeeds
- **WHEN** one process update fails while unrelated updates can proceed
- **THEN** independent updates continue
- **AND** the report identifies successes, failures, and any unrecovered intermediate changes
- **AND** the command exits with the failure code required by command-execution
