# Spec Delta

## Purpose

Manage publication state and ownership of existing Dataverse duplicate-detection rules through explicit resource-oriented configuration.

## ADDED Requirements

### Requirement: Existing rules have resource-oriented operations

The CLI SHALL expose `duplicaterules init` and `duplicaterules apply` to capture and apply publication state and ownership of existing rules. These operations SHALL NOT create or delete rules or transfer rule definitions and conditions.

#### Scenario: Existing rule is captured
- **WHEN** a selected existing rule is captured
- **THEN** its publication state and ownership can be represented in configuration without exporting its definition for deployment

#### Scenario: Configured rule does not exist
- **WHEN** a configured rule cannot be found
- **THEN** preflight fails rather than creating it

### Requirement: Existing rules are identified by GUID

Application SHALL identify existing rules by their configured GUID, relying on IDs transported separately through solutions. It SHALL NOT fall back to name or definition matching. A missing target GUID SHALL fail preflight even if a rule with the same display name exists.

#### Scenario: Same name but different ID
- **WHEN** the configured GUID is absent but a rule with the same name exists
- **THEN** preflight fails without changing that rule

### Requirement: Rule changes express explicit intent

Application SHALL leave unlisted rules and omitted properties unchanged. It SHALL support publishing, unpublishing, and ownership changes and avoid unnecessary writes when the requested state is already satisfied.

#### Scenario: State-only configuration
- **WHEN** a rule requests publication without an ownership change
- **THEN** application publishes the rule without changing its owner

#### Scenario: Owner-only configuration
- **WHEN** a rule requests only a supported ownership change and application succeeds
- **THEN** the requested owner is applied and the rule retains its original publication state

#### Scenario: Already satisfied rule
- **WHEN** the target already matches all specified properties
- **THEN** application succeeds without unnecessary writes

### Requirement: Duplicate-rule application is prevalidated

Application SHALL resolve all configured rule and owner references and validate requested operations before any write. Missing or ambiguous references and unsupported owner assignments SHALL fail preflight. Dry-run SHALL report the planned changes without writing.

#### Scenario: Invalid reference in a batch
- **WHEN** one configured rule or owner cannot be resolved uniquely
- **THEN** no rule in the batch is changed

#### Scenario: Dry-run
- **WHEN** a valid batch is evaluated in dry-run mode
- **THEN** planned publication and ownership changes are reported without writes

### Requirement: Ownership uses shared target resolution

Configured owners SHALL use the shared logical-owner and environment-mapping contract rather than resource-specific lookup heuristics. Duplicate-rule application SHALL validate resolved principal kinds against its own platform constraints before writes. Shared support for a principal kind SHALL NOT imply that duplicate rules accept that kind.

#### Scenario: Resolved owner is not allowed
- **WHEN** the shared resolver identifies a principal whose kind is unsupported for duplicate-rule ownership
- **THEN** preflight fails without any rule writes

### Requirement: Duplicate-rule partial failures are observable

After preflight succeeds, application SHALL continue independent rules after a final runtime failure. It SHALL report completed and failed operations and exit unsuccessfully if any requested operation fails, without promising atomic rollback.

#### Scenario: Publication fails for one rule
- **WHEN** publication fails for one rule but an unrelated rule can be processed
- **THEN** the unrelated rule is processed
- **AND** the result identifies the failure and any completed changes
- **AND** the command exits with the failure code required by command-execution
