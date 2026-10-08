# Webresource Deployment

## Purpose

Specify safe deployment of Dataverse webresources from a local file or directory.

## Requirements

### Requirement: Directory targets have deterministic names

Directory deployments MUST use an explicit publisher prefix for unmapped files. A mapping file MAY override names for supported files under the target directory; invalid or unmatched mapping entries MUST fail before writes.

#### Scenario: A directory contains mapped and unmapped files

- **WHEN** a valid mapping file is supplied
- **THEN** mapped files use their declared logical names and other supported files use the publisher-prefix convention

#### Scenario: A mapping does not identify a supported target file

- **WHEN** a mapping key is absent or unsupported in the target directory
- **THEN** planning fails before Dataverse writes

### Requirement: A complete deployment plan precedes writes

The command MUST render resource actions, missing solution memberships, and any obsolete deletions before execution. Dry-run MUST render the plan and perform no Dataverse writes.

#### Scenario: A dry-run includes all planned operation types

- **WHEN** a directory deployment uses `--dry-run`
- **THEN** creates, updates, solution additions, and eligible obsolete deletions are visible
- **AND** no Dataverse writes occur

### Requirement: Managed resources are protected

An update to a managed webresource MUST fail during planning. An unchanged managed webresource MUST remain a valid no-op.

#### Scenario: A managed resource has identical content

- **WHEN** local and remote content match
- **THEN** the resource is treated as unchanged and is not updated

### Requirement: Obsolete deletion is scoped and non-destructive for empty input

Obsolete deletion MUST require a directory target and a selected solution, and MUST exclude managed resources. An empty directory MUST be a no-op even when obsolete deletion is requested.

#### Scenario: An empty directory is deployed with obsolete deletion enabled

- **WHEN** the target directory contains no supported files
- **THEN** no solution resources are deleted

### Requirement: Changed resources are published after writes and membership updates

Each created or updated resource MUST be published separately after resource writes and solution-membership additions complete. Unchanged resources MUST NOT be published.

#### Scenario: A deployment changes multiple resources

- **WHEN** multiple resources are created or updated
- **THEN** each changed resource is published after all resource writes and membership additions