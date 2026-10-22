# Spec Delta

## MODIFIED Requirements

### Requirement: Durable and safe connection feedback

When establishing a Dataverse connection for a command's operations, `dgtp` MUST leave a persistent success or failure line identifying the target environment and authentication type, except for candidate verification during `dgtp connection create`. Connection feedback MUST NOT echo raw connection strings or provider exception messages. Cancellation MUST propagate without being reported as a connection failure. Connection acquisition MUST NOT start a live display.

#### Scenario: Connection verification succeeds

- **WHEN** the command's connection passes connectivity verification
- **THEN** a persistent success line identifies the target environment and authentication type
- **AND** connection acquisition does not start a live display

#### Scenario: Connection establishment fails

- **WHEN** credential construction, service-client creation, or connectivity verification fails while establishing a command's operational connection
- **THEN** a persistent failure line identifies the target environment and authentication type
- **AND** the original exception propagates to the command's error handling
- **AND** the failure line contains neither the raw connection string nor the provider exception message

#### Scenario: A connection attempt is cancelled

- **WHEN** connection establishment throws an `OperationCanceledException`
- **THEN** cancellation propagates without a connection failure line

#### Scenario: A candidate connection is verified during creation

- **WHEN** `dgtp connection create` verifies a candidate connection
- **THEN** verification alone does not require a separate persistent connection success or failure line
- **AND** verification failures propagate through command error handling
- **AND** the saved-connection confirmation is emitted only after successful persistence, including when `--no-verify` bypasses verification
