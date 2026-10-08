# Plugin Deployment

## Purpose

Specify the safety and compatibility contract for deploying Dataverse plugin assemblies and packages.

## Requirements

### Requirement: Plan before deployment writes

`dgtp plugin push` MUST build and render the target deployment plan before applying Dataverse writes. Dry-run MUST stop after rendering and MUST NOT write to Dataverse.

#### Scenario: A dry-run previews a target

- **WHEN** `plugin push` is invoked with `--dry-run`
- **THEN** the plan and applicable solution-membership changes are shown
- **AND** no deployment writes occur

#### Scenario: A target is applied

- **WHEN** a non-dry-run plan is applied
- **THEN** execution applies the already-rendered plan without recalculating deployment decisions

### Requirement: Confirmation is limited to interactive deployment

When `--confirm` is selected, the command MUST ask before applying a target plan with changes. Dry-run, CI, and non-interactive execution MUST NOT prompt.

#### Scenario: Deployment is declined

- **WHEN** the user declines an interactive confirmation
- **THEN** that target remains unchanged

### Requirement: Existing step configuration is preserved during plugin replacement

Plugin replacement MUST preserve existing unsecure and secure configuration for matching declared steps. `plugin push` MUST NOT read or source-control configuration secrets; explicit configuration provisioning remains a separate operation.

#### Scenario: A matching step is migrated

- **WHEN** a declared step is reassigned to a replacement plugin type
- **THEN** its existing environment-provisioned configuration is preserved

### Requirement: Custom data-provider deployment limitations are visible

User-facing documentation MUST identify custom data-provider deployment as experimental until first-time provisioning and platform-owned step cleanup have been validated against a live Dataverse environment.

#### Scenario: A user considers provider deployment

- **WHEN** custom data-provider deployment is documented
- **THEN** the experimental status and non-production validation requirement are stated