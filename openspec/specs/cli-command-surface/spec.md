# CLI Command Surface

## Purpose

Keep the production CLI, shell completion, and command tests aligned on the same command structure.

## Requirements

### Requirement: Runtime and shell completion share command registration

The production `dgtp` application and shell-completion model MUST derive their command metadata from the same command-tree registration implementation.

#### Scenario: A command is added or changed

- **WHEN** a command path, alias, or option is registered
- **THEN** the runtime CLI and shell completion expose the same command structure

### Requirement: Command paths have structural smoke coverage

The CLI test suite MUST validate command-tree construction and smoke-test registered command paths without requiring a live Dataverse environment.

#### Scenario: The command tree is validated

- **WHEN** the CLI structural tests run
- **THEN** command registration and representative command paths are checked without bootstrapping external services