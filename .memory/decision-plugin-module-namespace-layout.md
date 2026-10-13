# Decision: Plugin Module Namespace Layout

## Context

`dgt.power.plugin` grew from a single feature (`plugin push`) to several (`plugin push`, `plugin step enable|disable|state set|config set`). The original layout was purely layered (`Local`, `Remote`, `Planning`, `Execution`, `Output`, `Repositories`, `Commands`), about 80% of it serving only `plugin push`, with exceptions and an enum loose at the module root.

## Decision

Hybrid layout: entry points and shared code stay at the top level, feature-internal code is grouped per feature.

| Namespace | Content |
|-----------|---------|
| `Commands` | All command and settings classes (push and step) - entry points stay easy to discover |
| `Repositories` | Entity repositories shared by all subcommands |
| `Models` | Shared domain types: `Remote*` records, `PluginStepStage`, repository DTOs, `LocalDataProvider`, `DataProviderOperation` |
| `Exceptions` | All module exceptions |
| `Base` | Shared settings base class |
| `Deployment.{Local,Planning,Execution,Output}` | Push-only internals (`Planning.Comparison`, `Planning.Plans`) |
| `StepManagement` | Step enable/disable/state-set internals (handler, selector, config model) |

## Rationale

- Dependencies point one way: feature namespaces depend on `Models`/`Repositories`/`Exceptions`, never the reverse. A type that repositories need (for example `LocalDataProvider`) belongs in `Models`, not in a feature namespace.
- `Deployment` over `Push` (a verb) and `StepManagement` over `Steps`: the names describe the subsystem and avoid clashes with `PluginStep*` types.
- A full feature-slice (repositories split per feature) was rejected: repositories and records are shared, so it would force duplication or a "Shared" namespace.

## Guidance

Put new push-only code under `Deployment`, new step-management code under `StepManagement`, and anything used by more than one subcommand in `Models`, `Repositories` or `Exceptions`. A third feature that needs its own internals gets its own top-level feature namespace.
