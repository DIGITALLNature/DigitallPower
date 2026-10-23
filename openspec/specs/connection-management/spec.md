# Connection Management

## Purpose

Specify how `dgtp` stores, selects, and authenticates typed Dataverse connections without exposing secrets in connection metadata.

## Requirements

### Requirement: Typed connection definitions

`dgtp connection` MUST persist named connections as typed definitions with one of the discriminators `interactive`, `deviceCode`, `clientSecret`, `clientCertificate`, or `azureDevOpsFederated`. The `profile` command branch MUST NOT exist, and 2.x profile storage MUST NOT be imported.

#### Scenario: A connection is created

- **WHEN** a user creates a named connection
- **THEN** its type-specific metadata is stored in the schema-versioned `connections.json`
- **AND** names are stored with their original casing and looked up case-insensitively

### Requirement: Secrets are stored outside connection metadata

Client secrets and PFX passwords MUST be stored in protected secret storage and MUST NOT appear in `connections.json`, `state.json`, or token blobs. Unencrypted secret storage MUST require the explicit `DGTP_ALLOW_UNENCRYPTED_STORAGE` opt-in and MUST emit a warning when selected.

#### Scenario: A client-secret connection is created

- **WHEN** `--client-secret` is supplied
- **THEN** the secret is stored separately under the connection name
- **AND** the connection metadata contains no secret value

### Requirement: Create persists only after verification

`dgtp connection create` MUST stage secret material, verify the candidate connection with a Dataverse `WhoAmI` request, and only then persist the definition. A failed verification MUST restore previously staged secrets and MUST leave existing definitions and the current selection unchanged. `--no-verify` MUST bypass the Dataverse check.

#### Scenario: Verification fails

- **WHEN** the connectivity check fails during create
- **THEN** no new definition is persisted and the current selection is unchanged
- **AND** any staged secret is restored or removed

### Requirement: Connection selection precedence

An invocation MUST resolve its connection from `--connection-string` / `DGTP_CONNECTION_STRING` first, then from `--connection` / `DGTP_CONNECTION`, then from the current saved connection. Connection strings MUST NOT be persisted.

#### Scenario: Both a connection string and a named connection are supplied

- **WHEN** both are present
- **THEN** the connection string is used

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

### Requirement: Non-interactive authentication

When `--non-interactive` or `DGTP_NON_INTERACTIVE` is set, authentication MUST NOT open a browser or start a device-code flow. If interactive sign-in is required, the command MUST print an `AUTH_REQUIRED:` message and exit with code `2`.

#### Scenario: A user token has expired

- **WHEN** a command runs in non-interactive mode and silent token acquisition fails
- **THEN** the exit code is `2`

### Requirement: Status and refresh

`dgtp connection status` MUST attempt silent token acquisition without side effects and exit `0` when authentication succeeds and `2` when interactive login is required. It is a token check, not a Dataverse permission check. `dgtp connection refresh` MUST reauthenticate interactively for user connections, print `AUTH: Starting interactive authentication...` before doing so, and MUST NOT reauthenticate service-principal credentials.

#### Scenario: An agent probes authentication

- **WHEN** `dgtp connection status` returns `2`
- **THEN** the caller can run `dgtp connection refresh` and retry

### Requirement: Azure DevOps federated connections

`azureDevOpsFederated` connections MUST obtain tokens through `AzurePipelinesCredential` using the pipeline's `SYSTEM_ACCESSTOKEN` and MUST NOT cache tokens on disk. With `--service-connection-name`, the CLI MUST resolve URL, tenant, application ID, and service connection ID via the Azure DevOps REST API; zero or multiple matches MUST fail and point to the explicit options. The service connection name and the explicit values are mutually exclusive.

#### Scenario: An ambiguous service connection name is supplied

- **WHEN** more than one service connection matches the name
- **THEN** creation fails and recommends the explicit `--tenant`, `--client-id`, and `--service-connection-id` options

### Requirement: Certificate authentication

`clientCertificate` connections MUST accept a CurrentUser certificate-store thumbprint or a PFX path. A PFX password MUST be supplied with `--certificate-password` only together with `--certificate-path`, is never prompted for, and an omitted password replaces any previously stored one.

#### Scenario: A thumbprint is used

- **WHEN** a thumbprint is supplied
- **THEN** the certificate is read from the CurrentUser `My` store and no PFX password is consumed

### Requirement: Deleting connections removes unreferenced cached accounts

Deleting a connection MUST remove its cached user account from the persistent token cache only when no remaining connection references the same home account ID. The shared token cache MUST NOT be cleared as a whole.

#### Scenario: Two connections share one account

- **WHEN** one of them is deleted
- **THEN** the cached account remains available to the other

### Requirement: Stable state location

Connection metadata, application state, and secret storage MUST live in a per-user directory independent of assembly version, resolved from `DGTP_HOME`, else `XDG_DATA_HOME/dgtp` or `~/.local/share/dgtp` on Linux, else `LocalApplicationData/dgtp`. State files MUST be written atomically under a lock, reject invalid JSON and unsupported schema versions without resetting, and preserve unknown fields.

#### Scenario: The tool is upgraded to a new major version

- **WHEN** a new major version starts
- **THEN** it reads the same connections and state

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
