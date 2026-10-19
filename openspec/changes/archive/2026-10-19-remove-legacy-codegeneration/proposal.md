# Proposal

## Why

Legacy code generation preserves V1 configuration, duplicate TypeScript pipelines, and supporting metadata and template code that increase maintenance effort. The dgtp 3 major release is the opportunity to remove these compatibility paths without redesigning the surviving generators.

## What Changes

- **BREAKING**: Reject unversioned and version 1 configurations with migration guidance instead of executing or adapting them.
- **BREAKING**: Remove TypeScript Full generation and attribute-level filters without replacement.
- Keep one .NET generator and one TypeScript generator, consolidating the existing typed-config TypeScript pipeline and removing Full/Light selection and strategy indirection.
- Remove legacy-only models, mappings, metadata overloads, templates, resources, fixtures, and dependencies where exclusively obsolete.
- Review the remaining config for concrete compatibility baggage. Retain format V2 by default; any necessary incompatible contract cleanup requires a documented decision, format V3, and new schema paths before implementation. Do not introduce changes for the future resource-oriented design.
- Preserve existing typed-config generation behavior, including both .NET targets and optional TypeScript helpers.
- Overhaul the README code-generation section and document direct 2.x-to-3.x migration, including removed output capabilities.

## Capabilities

### New Capabilities

- `codegeneration-output`: Supported .NET and TypeScript artifacts, single-pipeline generation, and removal of legacy runtime model output.

### Modified Capabilities

- `codegeneration-config`: Replace V1 readability with explicit rejection and require an explicit supported format and one output target.

## Impact

Affected areas are `src/modules/dgt.power.codegeneration`, its registrations and call sites, `tests/dgt.power.codegeneration.tests`, related fixtures, `schemas/codegeneration`, `README.md`, and `docs/migrations/2.x-to-3.x.md`. Dependency removals, if justified by exclusive legacy use, must include affected lock files.

Existing V1 users must migrate configuration and, for Full generation, consuming TypeScript code. Attribute-level filters have no replacement. No resource-oriented API, module split, CLI rename to `model`, new generation feature, or general metadata-service redesign is included. The internal module remains `codegeneration`, and existing CLI paths/options remain unchanged.
