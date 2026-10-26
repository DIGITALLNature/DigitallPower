# Proposal

## Why

The legacy `analyze` branch groups six solution-inspection commands that have no resource-oriented owner in the current CLI. Retiring the branch removes this standalone surface and its maintenance burden without changing Dataverse state.

## What Changes

- Remove `analyze entityallassets`, `noactivelayer`, `activelayer`, `toplayer`, `redundantcomponents`, and `redundantpatches` without replacement.
- Remove the `analyze` branch, analyzer module, and artifacts used exclusively by these commands after checking for remaining consumers.
- Retire the analyzer-specific options, configuration input, and generated `Analyze/` reports; no supported migration for these files or outputs is provided.
- **BREAKING**: Remove the public `analyze` CLI surface.
- Out of scope: introducing replacement analysis commands, changing solution operations, or modifying Dataverse data or metadata.

## Capabilities

### New Capabilities

None.

### Modified Capabilities

- `cli-command-surface`: The legacy `analyze` branch is no longer registered or advertised.

## Impact

Touches command registration, `dgt.power.analyzer` and its tests, project references, analyzer-only shared DTOs/helpers, README command and repository-layout documentation, and the 3.x migration guide. Implementation must verify remaining consumers before removing shared types and preserve unrelated solution commands and CLI completion behavior.
