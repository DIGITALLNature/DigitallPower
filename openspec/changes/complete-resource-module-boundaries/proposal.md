# Proposal

## Why

Resource extraction can leave obsolete references, feature DTOs in shared infrastructure, and orphaned projects or tests. A final source-grounded audit should close those gaps without manufacturing assemblies for every CLI node or Dataverse table.

## What Changes

- Audit module ownership throughout the solution against one DLL per cohesive business resource.
- Allow justified capability modules such as analysis/code generation and infrastructure modules such as connections.
- Close remaining behavior-preserving ownership and dependency gaps after resource migration and legacy retirement.
- Keep genuinely shared infrastructure and Dataverse types shared; do not duplicate them solely to satisfy DLL counts.
- Out of scope: new public behavior, automatic decomposition of every existing module, and unreviewed feature removals.

## Capabilities

### New Capabilities

None. This is an architecture audit and behavior-preserving cleanup.

### Modified Capabilities

None. Spec generation is explicitly skipped; any newly discovered public behavior change requires its own proposal.

## Impact

Touches only confirmed residual project references, DI wiring, DTO ownership, tests, and architecture documentation. Start inventory early; perform final cleanup after all approved resource migrations and both legacy retirement changes. Update README structure where changed and the existing resource-oriented module ADR where durable rationale needs clarification.
