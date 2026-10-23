# Spec Delta

## ADDED Requirements

### Requirement: Explicit caller service acquisition

A connection SHALL provide an explicit acquisition operation for a service acting as a specified Dataverse `systemuser` ID. It SHALL reject an empty caller ID before authentication or connection creation. Implementations without impersonation support SHALL fail explicitly rather than ignore the caller.

#### Scenario: A caller service is requested

- **WHEN** a supported connection receives a valid, non-empty Dataverse caller ID
- **THEN** it returns a ready service configured to execute subsequent requests as that caller

#### Scenario: An empty caller ID is supplied

- **WHEN** caller acquisition receives an empty GUID
- **THEN** it fails with an argument error before authentication or connection creation

#### Scenario: An implementation does not support impersonation

- **WHEN** an unsupported connection implementation receives a non-empty caller ID
- **THEN** it throws an explicit not-supported error instead of returning its standard service

### Requirement: Caller isolation within an invocation

Standard and caller services SHALL use the invocation's same selected environment and authentication configuration while retaining independent caller state. Acquiring or using one caller SHALL NOT change another service's identity. Neither acquisition failures nor request faults SHALL cause execution under a substitute identity.

#### Scenario: Requests use multiple identities

- **WHEN** a consumer interleaves requests on the standard service, caller A's service, caller B's service, and the standard service again
- **THEN** each request uses its service's original identity
- **AND** the standard service remains unchanged

#### Scenario: Saved connection selection changes

- **WHEN** the invocation has selected its connection and the saved current connection subsequently changes
- **THEN** a newly acquired caller service still uses the invocation's original environment and authentication configuration

#### Scenario: A caller request fails

- **WHEN** Dataverse rejects a request executed through caller A's service
- **THEN** the fault propagates without executing that request through the standard service or caller B's service

### Requirement: Invocation-owned caller service lifetime

A connection SHALL lazily retain one successfully initialized service per caller ID for its invocation. Repeated or concurrent acquisition of the same ID SHALL return the same fully configured service. Different caller IDs SHALL have isolated services. The connection SHALL own and dispose its standard and caller services; consumers SHALL borrow them without disposal or caller-state mutation.

#### Scenario: The same caller is acquired repeatedly

- **WHEN** a caller service has been successfully acquired and the same caller ID is requested again
- **THEN** the same service instance is returned

#### Scenario: The same caller is acquired concurrently

- **WHEN** concurrent acquisition calls request the same caller ID
- **THEN** they receive the same fully configured service
- **AND** duplicate caller services are not published

#### Scenario: Different callers are acquired

- **WHEN** caller A and caller B have different IDs
- **THEN** their acquired services are distinct and neither shares mutable caller state with the standard service

#### Scenario: The connection is disposed

- **WHEN** the connection's lifetime ends after acquiring a standard service and multiple caller services
- **THEN** the connection disposes all of those services
- **AND** consumers have not needed to dispose the borrowed services

### Requirement: Failed caller acquisition is not cached

Caller initialization or configuration failures SHALL propagate and SHALL NOT be cached as successful acquisitions. The connection SHALL dispose unfinished clients. A later acquisition SHALL be allowed to retry initialization. Authentication failures and cancellation SHALL propagate without returning a substitute service.

#### Scenario: Caller client creation fails

- **WHEN** creation of a caller client fails
- **THEN** the failure propagates and no successful caller service is cached or returned
- **AND** any client created by the failed attempt is disposed

#### Scenario: Caller configuration fails

- **WHEN** initialization has created a client but configuring it for the requested caller fails
- **THEN** that client is disposed and is neither cached nor returned

#### Scenario: Acquisition succeeds after a failed attempt

- **WHEN** an earlier acquisition failed and a later acquisition for the same caller can initialize successfully
- **THEN** the later call returns and caches the newly initialized caller service

#### Scenario: Acquisition is cancelled

- **WHEN** caller acquisition encounters an operation-cancelled error
- **THEN** cancellation propagates without returning another service
- **AND** any unfinished client from that attempt is disposed and not cached

### Requirement: Caller acquisition does not prevalidate operation privileges

Successful caller acquisition SHALL guarantee a ready, caller-configured service, not caller existence, enabled status, or permission for later operations. Acquisition SHALL NOT add a caller-specific `WhoAmI` or privilege probe. SDK initialization requests SHALL remain permitted. Faults from actual caller operations SHALL propagate unchanged.

#### Scenario: A caller service is acquired without an additional probe

- **WHEN** a connection acquires a caller service
- **THEN** it performs no additional caller-specific `WhoAmI` or privilege probe
- **AND** the existing standard connection verification remains in effect

#### Scenario: Dataverse denies a later operation

- **WHEN** a service has been acquired successfully but Dataverse denies a subsequent operation under that caller
- **THEN** the original Dataverse fault propagates without an identity fallback

### Requirement: Caller acquisition preserves connection feedback

Caller acquisition SHALL preserve existing standard connection feedback without adding a per-caller "Connected" success message that implies verified impersonation privileges. Acquisition failures SHALL use existing command error handling without exposing tokens or raw connection strings.

#### Scenario: Multiple caller services are acquired

- **WHEN** an invocation establishes its standard connection and acquires multiple caller services
- **THEN** the standard connection feedback remains available
- **AND** caller acquisition emits no additional "Connected" success messages

#### Scenario: A caller acquisition failure is surfaced

- **WHEN** caller acquisition fails during command execution
- **THEN** the failure reaches the existing command error handling
- **AND** user-facing feedback exposes neither tokens nor raw connection strings
