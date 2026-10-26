# Design

## Context

The resource-oriented module ADR already separates plugin and webresource ownership. The source-backed solution inventory contains eleven live module projects:

| Live project | Source project | Test project |
| --- | --- | --- |
| `analyzer` | `src/modules/dgt.power.analyzer` | `tests/dgt.power.analyzer.tests` |
| `codegeneration` | `src/modules/dgt.power.codegeneration` | `tests/dgt.power.codegeneration.tests` |
| `connection` | `src/modules/dgt.power.connection` | `tests/dgt.power.connection.tests` |
| `documenttemplates` | `src/modules/dgt.power.documenttemplates` | `tests/dgt.power.documenttemplates.tests` |
| `duplicaterules` | `src/modules/dgt.power.duplicaterules` | `tests/dgt.power.duplicaterules.tests` |
| `plugin` | `src/modules/dgt.power.plugin` | `tests/dgt.power.plugin.tests` |
| `process` | `src/modules/dgt.power.process` | `tests/dgt.power.process.tests` |
| `solution` | `src/modules/dgt.power.solution` | `tests/dgt.power.solution.tests` |
| `teamtemplates` | `src/modules/dgt.power.teamtemplates` | `tests/dgt.power.teamtemplates.tests` |
| `telemetry` | `src/modules/dgt.power.telemetry` | `tests/dgt.power.telemetry.tests` |
| `webresource` | `src/modules/dgt.power.webresource` | `tests/dgt.power.webresource.tests` |

`DigitallPower.slnx` also contains the `src/dgt.power` CLI host and `src/dgt.power.common` shared infrastructure, plus `tests/dgt.power.cli.tests`, `tests/dgt.power.common.tests`, and `tests/dgt.power.tests` for host, shared-infrastructure, and shared-test concerns. The host project references `dgt.power.common` and all eleven live modules. Each live module references only `dgt.power.common`; there are no direct project references between feature modules. `Program.cs` owns the common DI registrations and `CommandTree.Register` composes active module commands.

The former `src/modules/dgt.power.linter`, `src/modules/dgt.power.profile`, and `src/modules/dgt.power.push` projects are not present in the current solution or tracked source. `tests/dgt.power.profile.tests/Usings.cs` is a tracked leftover without a corresponding project. `schemas/push/schema.json` is still tracked, but no source or test references it; it is a compatibility-sensitive schema artifact, not evidence of a live module. Historical paths in `baseline.sarif.json` are stale analyzer data and are not project inventory.

## Ownership Assessment

| Owner | Cohesive responsibility / exception | Configuration and shared contracts | Tests |
| --- | --- | --- | --- |
| `analyzer` | Solution analysis and report generation; justified capability module | `schemas/analyzer/schema.json`; its `AnalyzerSummary` DTO is currently defined in common | `tests/dgt.power.analyzer.tests` |
| `codegeneration` | C#, TypeScript, and metadata generation; justified capability module | `schemas/codegeneration/v2/*.schema.json` | `tests/dgt.power.codegeneration.tests` |
| `connection` | User-facing connection management; infrastructure capability | Connection metadata is persisted by common and described by `schemas/connection/v1/schema.json` | `tests/dgt.power.connection.tests` |
| `documenttemplates` | Portable Word and Excel document-template import/export | `schemas/documenttemplates/v1/schema.json` | `tests/dgt.power.documenttemplates.tests` |
| `duplicaterules` | Duplicate-rule state and owner capture/apply | `schemas/duplicaterules/v1/schema.json`; shared owner mapping in `schemas/ownership/v1/schema.json` | `tests/dgt.power.duplicaterules.tests` |
| `plugin` | Plugin assembly/package deployment and plugin-step management | `schemas/plugin/state/schema.json` | `tests/dgt.power.plugin.tests` |
| `process` | Process state and owner capture/apply | `schemas/process/v1/schema.json`; shared owner mapping in `schemas/ownership/v1/schema.json` | `tests/dgt.power.process.tests` |
| `solution` | Solution-scoped operations, including linting, versioning, and component copy | `schemas/solution/lint/schema.json` | `tests/dgt.power.solution.tests` |
| `teamtemplates` | Team-template transfer | `schemas/teamtemplates/v1/schema.json`; shared owner mapping in `schemas/ownership/v1/schema.json` | `tests/dgt.power.teamtemplates.tests` |
| `telemetry` | User-facing telemetry controls; justified infrastructure capability | Runtime settings are part of common application state described by `schemas/state/v1/schema.json` | `tests/dgt.power.telemetry.tests` |
| `webresource` | Webresource deployment | `schemas/webresource/schema.json`; the separate legacy `schemas/push/schema.json` has no current source/test consumer | `tests/dgt.power.webresource.tests` |

`dgt.power.common` owns cross-module connection persistence, application state, owner mapping/resolution, file access, command lifecycle, shared queries, and telemetry infrastructure; `schemas/connection/v1/schema.json`, `schemas/state/v1/schema.json`, and `schemas/ownership/v1/schema.json` describe those shared contracts. Owner mapping is consumed by process, duplicate-rule, and team-template workflows. The common test project covers shared code and ownership contracts. `dgt.power` is composition only (DI and command tree), covered by `dgt.power.cli.tests`; `dgt.power.tests` supplies shared test infrastructure.

The eleven live modules each have a focused responsibility. Analysis and code generation are cohesive capabilities rather than one-Dataverse-resource modules; connection and telemetry are justified infrastructure/operator capabilities. No module directly references another module. No pending public behavior or configuration change is part of this audit.

## Residuals and Compatibility

- `AnalyzerSummary` is a public record in `dgt.power.common` but its only consumers are analyzer implementation and analyzer tests. Moving it into the analyzer assembly would change its defining assembly and could break consumers of the common assembly. Do not move it as behavior-preserving cleanup; any ownership correction requires a separately reviewed API proposal.
- `schemas/push/schema.json` has no current source or test consumer, but its published path may be consumed by external configuration editors. Retain it in this audit; removing or redirecting it requires a separately reviewed compatibility decision.
- `tests/dgt.power.profile.tests/Usings.cs` is the only tracked source file under the retired test folder, has no test project, and has no consumer. It can be removed without changing product behavior.
- The host has no retired-module project reference or DI registration, and feature modules have no cross-module project reference. The resource migrations and both retirement changes are complete in OpenSpec; no pending feature decision or legacy runtime dependency was found.

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
