# Design

## Context

The resource-oriented module ADR already separates plugin and webresource ownership. The host references nine module projects, while directories such as `dgt.power.linter`, `dgt.power.profile`, and `dgt.power.push` also exist without matching module projects in the current inventory. Directory names alone do not establish live modules.

## Goals / Non-Goals

**Goals:** Produce a source-backed ownership assessment and close actual residual gaps after migration.

**Non-Goals:** Require one assembly per Dataverse table, CLI child command, or technical operation; add public functionality; or invent normative behavior specs for an internal audit.

## Decisions

- Use one DLL per cohesive business resource, with justified capability/infrastructure exceptions. Analysis, code generation, and connections are not automatically architectural violations.
- Evaluate references, DI, resource DTOs, execution logic, schemas, tests, and documentation together. DLL count alone is not acceptance evidence.
- Shared Dataverse models and genuine infrastructure can remain shared. Resource-specific models belong with their owner when no actual cross-resource consumer needs them.
- Do not require blanket bans on cross-module references. Inspect and justify genuine business dependencies; reject accidental coupling and reliance on retired verb modules.
- Declare `skip_specs: true`: the planned cleanup preserves behavior. A discovered public behavior or API break requires a separately reviewed change before implementation.

## Assessment and Execution Gates

Start an inventory early, but judge the final architecture only after all resource migrations and both retirement changes complete. Record each live module's owner or justified exception and each residual issue's concrete references and affected consumers. Do not delete leftover-looking directories without checking their tracked contents and consumers.

## Risks / Trade-offs

- Excessively granular assemblies increase complexity without isolation -> use cohesive business boundaries.
- Moving a public type causes an unintended API break -> separate that change for review rather than labeling it internal.
- Audit becomes another feature rewrite -> limit implementation to confirmed residual, behavior-preserving gaps.

## Migration Plan

Inspect the final project graph; fix remaining references, ownership, and orphaned artifacts; update directly affected architecture/project documentation. Preserve command parsing and behavior with existing focused tests and a solution build. This change closes the sequence; it does not supersede unresolved feature decisions.
