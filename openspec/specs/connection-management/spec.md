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

When establishing a Dataverse connection for a command, `dgtp` MUST leave a persistent success or failure line identifying the target environment and authentication type. Connection feedback MUST NOT echo raw connection strings or provider exception messages. Cancellation MUST propagate without being reported as a connection failure.

#### Scenario: Connection verification succeeds

- **WHEN** the command's connection passes connectivity verification
- **THEN** a success line remains visible after the progress display ends
- **AND** it identifies the target environment and authentication type

#### Scenario: Connection establishment fails

- **WHEN** connection creation or connectivity verification fails
- **THEN** a failure line remains visible after the progress display ends
- **AND** the original exception propagates to the command's error handling
- **AND** the failure line contains neither the raw connection string nor the provider exception message

#### Scenario: A connection attempt is cancelled

- **WHEN** connection establishment throws an `OperationCanceledException`
- **THEN** cancellation propagates without a connection failure line

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
