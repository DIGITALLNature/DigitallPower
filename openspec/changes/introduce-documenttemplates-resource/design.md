# Design

## Context

Import matches by name and document type, conditionally deactivates missing templates, and can delete/recreate when `ForceUpdate` is set. Word files are rewritten using target metadata in the main document and related XML parts. Export writes a JSON manifest plus files, uses inline `FORCE`/`MISSING` directives, selects activated templates, and inverts status during serialization. Existing tests cover force replacement and document fixtures.

## Goals / Non-Goals

**Goals:** Own document transfer end to end and distinguish ordinary import from destructive replacement and cleanup.

**Non-Goals:** General document transformation or reuse of teamtemplate-specific synchronization logic.

## Decisions

- Introduce `dgt.power.documenttemplates`, owning both commands, manifest models, content adaptation, and tests. Place the OpenXML dependency with document processing rather than retaining the legacy import assembly.
- Ordinary import leaves unlisted templates unchanged. Cleanup requires an explicit bounded scope.
- Treat delete/recreate as a distinct destructive effect, not a cosmetic update; its exact public contract remains gated.
- Retain Word and Excel transfer capability while validating target-specific metadata adaptation and status mapping. Do not copy inverted booleans or inline directive syntax without a reviewed contract.
- Keep shared file services reusable, but remove document-specific dependencies from other resource modules.

## Decision Gates Before Implementation

Obtain user decisions and refine specs before coding:

1. Define portable identity, manifest/schema version, relative file paths, collision handling, and export selection including active/draft templates.
2. Define status representation and round-trip semantics; characterize the legacy inversion before replacing it.
3. Decide when replacement is necessary, how explicitly it must be requested, which IDs must survive, and what happens after deletion succeeds but creation fails.
4. Define cleanup scope and whether cleanup means deactivation only or also supports deletion.
5. Specify supported Word XML references/parts, Excel adaptation requirements, missing/invalid-file handling, and metadata lookup failures.
6. Confirm template preflight/dry-run, partial-failure, and cancellation guarantees rather than inheriting unapproved guarantees from the state-management resources.

## Risks / Trade-offs

- Deletion succeeds but recreation fails -> surface partial effects; approve replacement/recovery semantics before implementation.
- Incorrect status inversion or name normalization -> characterize with existing fixtures and explicit round-trip tests.
- Word XML changes corrupt content or miss references -> use representative fixtures across relevant parts.
- File-name collisions overwrite documents -> settle manifest and path identity before export implementation.

## Migration Plan

Introduce the module and characterize retained document behavior, then switch CLI paths and remove documenttemplate commands from the old modules. Move OpenXML references only after consumer checks and synchronize applicable lock files. Update README and `docs/migrations/2.x-to-3.x.md`, including cleanup, replacement, status, and format changes. Removing the legacy transfer modules is a separate dependent change.
