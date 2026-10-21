# Spec Delta

## Purpose

Capture and apply explicit process state and ownership across Dataverse environments without implicit changes to unrelated processes.

## ADDED Requirements

### Requirement: Process configuration is resource-oriented

The CLI SHALL expose `process init` and `process apply` for classic workflows, cloud flows, actions, business process flows, and business rules. It SHALL NOT accept the replaced `maintenance createworkflowstate` or `maintenance workflowstate` commands as aliases.

#### Scenario: Legacy workflow command is invoked
- **WHEN** either replaced maintenance command is invoked
- **THEN** parsing fails without a Dataverse write
- **AND** help and completion advertise the resource-oriented replacement instead

### Requirement: Capture scope is selectable

`process init` SHALL allow capture of state, ownership, or both for the selected processes. Ownership references SHALL use logical keys that can be mapped separately for each target environment.

#### Scenario: State-only capture
- **WHEN** a user captures only state
- **THEN** the generated configuration records state without instructing ownership changes

#### Scenario: Ownership capture
- **WHEN** a user captures ownership
- **THEN** captured owner references can be resolved through target-environment mappings rather than relying exclusively on source-environment GUIDs

### Requirement: Application changes only explicit intent

`process apply` SHALL leave unlisted processes and omitted properties unchanged. Reapplying an already satisfied configuration SHALL perform no unnecessary writes. An owner-only update SHALL preserve the original activation state after any necessary intermediate transition.

#### Scenario: Owner-only update
- **WHEN** a listed active process requests a different owner but omits state
- **THEN** successful application changes the owner and leaves the process active

#### Scenario: Partial configuration
- **WHEN** a process or property is omitted
- **THEN** application does not infer an activation or ownership change for it

#### Scenario: Desired values already match
- **WHEN** all configured values already match the target
- **THEN** application succeeds without unnecessary writes

### Requirement: Ownership and impersonation are explicit

The process resource SHALL support mapped regular users and application users where the process type permits them. It SHALL reject unsupported owner/type combinations before writing. Explicitly requested impersonation SHALL NOT silently fall back to the connection identity.

#### Scenario: Unsupported owner
- **WHEN** the requested identity cannot own the selected process type
- **THEN** preflight fails without writes

#### Scenario: Impersonation is unavailable
- **WHEN** requested impersonation cannot be executed
- **THEN** the operation reports failure instead of continuing without impersonation

### Requirement: Preflight and dry-run precede mutations

Application SHALL resolve all referenced processes and identities and validate requested operations before any write. Missing or ambiguous references SHALL fail preflight. Dry-run SHALL show the planned changes without writes.

#### Scenario: Ambiguous process
- **WHEN** a configured process reference resolves to multiple candidates
- **THEN** preflight fails and no process is changed

#### Scenario: Dry-run
- **WHEN** a valid configuration is applied in dry-run mode
- **THEN** the intended changes are reported without Dataverse writes

### Requirement: Partial failures remain failures

After successful preflight, application SHALL continue independent objects after an object's final runtime failure. It SHALL report failed and completed changes, including intermediate state changes, and exit unsuccessfully if any requested operation fails. It SHALL NOT promise atomic rollback.

#### Scenario: One process fails after another succeeds
- **WHEN** one process update fails while unrelated updates can proceed
- **THEN** independent updates continue
- **AND** the report identifies successes, failures, and any unrecovered intermediate changes
- **AND** the command exits with the failure code required by command-execution
