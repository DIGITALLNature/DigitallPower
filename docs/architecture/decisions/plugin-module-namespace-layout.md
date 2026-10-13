# Plugin Module Namespace Layout

## Context

`dgtp plugin push` and the `plugin step` commands share one module. A purely layered namespace layout (`Local`, `Remote`, `Planning`, `Execution`, ...) mostly served push, and made feature boundaries invisible.

## Decision

Keep entry points and shared code at the module top level and group feature-internal code per feature:

- Shared: `Commands`, `Repositories`, `Models` (records and enums used across features), `Exceptions`, `Base`.
- Push internals: `Deployment.*` (`Local`, `Planning`, `Output`, and execution or reconciliation code).
- Step state internals: `StepManagement`.

## Rationale

Dependencies point one way: feature namespaces depend on shared namespaces, never the reverse. Types needed by repositories therefore live in `Models`. Commands stay at the top level so entry points are easy to discover. Repositories and records are shared, so a full feature-slice layout would force duplication or a catch-all shared namespace.

## Consequences

New push-only code goes under `Deployment`, step-state code under `StepManagement`, and anything used by more than one subcommand under the shared namespaces. Tests mirror the source layout.
