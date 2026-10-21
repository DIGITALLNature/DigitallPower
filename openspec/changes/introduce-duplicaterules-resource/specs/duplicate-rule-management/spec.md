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

### Requirement: Rule changes express explicit intent

Application SHALL leave unlisted rules and omitted properties unchanged. It SHALL support publishing, unpublishing, and ownership changes and avoid unnecessary writes when the requested state is already satisfied.

#### Scenario: State-only configuration
- **WHEN** a rule requests publication without an ownership change
- **THEN** application publishes the rule without changing its owner

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

### Requirement: Duplicate-rule partial failures are observable

After preflight succeeds, application SHALL continue independent rules after a final runtime failure. It SHALL report completed and failed operations and exit unsuccessfully if any requested operation fails, without promising atomic rollback.

#### Scenario: Publication fails for one rule
- **WHEN** publication fails for one rule but an unrelated rule can be processed
- **THEN** the unrelated rule is processed
- **AND** the result identifies the failure and any completed changes
- **AND** the command exits with the failure code required by command-execution
