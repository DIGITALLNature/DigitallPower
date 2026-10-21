# Design

## Context

`TeamTemplateImport` matches by GUID and deletes unlisted templates across its queried target set. `ShouldDeleteMissingTeamTemplate` explicitly asserts this behavior. Export records logical table names and resolves object type codes through metadata. Import and export use separate generic settings and base helpers.

## Goals / Non-Goals

**Goals:** Bring both directions under one resource owner and make destructive synchronization an explicit choice.

**Non-Goals:** Share a generic template pipeline with documenttemplates or retire unrelated transfer commands.

## Decisions

- Introduce `dgt.power.teamtemplates`, owning both commands, resource models, and tests. Avoid runtime dependencies on legacy import/export assemblies.
- Default import creates/updates listed templates only. The legacy global delete pass is not retained as the default.
- Cleanup must be explicitly requested and restricted to an explicit scope. Rejected scopes must not produce removal writes.
- Preserve logical-table-name portability instead of persisting target-specific object type codes as identity.
- Reuse shared file/configuration infrastructure where appropriate, not resource-specific base classes merely to minimize moved code.

## Decision Gates Before Implementation

Obtain user decisions before implementing the public contract:

1. Choose portable template identity, treatment of existing GUIDs, duplicate-name behavior, and create/update matching.
2. Choose file/schema versioning, import/export selection options, overwrite behavior, and empty-input semantics.
3. Define the cleanup selector, whether any explicit whole-environment scope is allowed, and handling of system templates.
4. Confirm preflight/dry-run and runtime failure guarantees for templates. The process/duplicate-rule guarantees were approved specifically for those resources and are not implicitly extended here.
5. Define required table/access-team metadata validation and which existing DTO fields are writable versus informational.

## Risks / Trade-offs

- Existing deletion tests encode an unsafe default -> replace them with default-preservation and explicit-scoped-cleanup tests.
- Environment-specific IDs fail to match -> approve identity before writing the migration logic.
- Access teams are not enabled for the target table -> validate the actual metadata value, not merely its presence.

## Migration Plan

Introduce the new module and tests, replace host paths, and remove only teamtemplate code from the legacy modules. Keep useful fixtures while updating behavior assertions. Update README and `docs/migrations/2.x-to-3.x.md`, including changed deletion semantics and any file changes. Legacy transfer module removal is deferred to `retire-legacy-import-export`.
