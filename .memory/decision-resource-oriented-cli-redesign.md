# Decision: Resource-Oriented CLI Redesign (`plugin push`, planned `webresource push`)

## Problem

The legacy `push` command (`dgt.power.push` module) mixes plugin assembly and web resource deployment
in one command/one set of service classes, even though the two resource kinds are independent. The CLI
style also does not match the `<resource> <verb>` convention used by `az`/`pac` (e.g. `az vm create`,
`pac plugin push`).

## Decision

Reshape the CLI toward resource-oriented commands: `dgtp plugin push`, `dgtp webresource push`, etc.,
implemented as **new, independent modules** (`dgt.power.plugin`, later `dgt.power.webresource`) built
from scratch rather than refactored in place from `dgt.power.push`. The legacy `push` command stays
functional (and will later get a deprecation warning, following the same `[DeprecatedCommand]` pattern
already used for `profile`, see `decision-generic-command-deprecation.md`) until all resource commands
have parity and the legacy module is removed.

### Why rewrite instead of refactor
- The old module's `AssemblyProcessor`/`AssemblyModelBuilder` classes couple parsing, planning, and
  Dataverse I/O together, making them hard to test and reason about independently for plugins vs. web
  resources.
- Splitting plugins and web resources into independent modules removes the need for any shared
  abstraction between two resource kinds that don't actually share behavior anymore.
- A clean-slate implementation allowed introducing a testable layered architecture (see
  `implementation-plugin-push-outdated-assembly-migration.md`) and revisiting version-upgrade migration
  semantics instead of preserving accidental complexity.

### Module-local dependency injection convention
Dependency injection (`Microsoft.Extensions.DependencyInjection`, wired in the `dgt.power` host's
`Program.cs`) is reserved for components that are used **across modules** (e.g. `IConnector`,
`IXrmConnection`, telemetry). Components that are local to a single module - repositories, planners,
executors, migrators - are constructed directly via `new` inside the command's `InvokeAsync`, not
registered in the global `IServiceCollection`. This keeps module-local wiring visible at the call site
and avoids growing the global container with types no other module needs.

See `PluginPushCommand.cs` in `dgt.power.plugin` for the reference pattern: it constructs all
`Dataverse/*Repository` instances, `PluginTypeReconciler`, and `OutdatedAssemblyMigrator` directly,
casting `Connection` to `(IOrganizationServiceAsync2)` once at the top of the method.

## Alternatives Considered

- **Refactor `dgt.power.push` in place** — rejected; the user explicitly wanted to question and
  revisit existing behavior (naming, upgrade-migration semantics, tuple-vs-record shapes) rather than
  preserve it, which is easier starting from an empty module.
- **Register module repos in the global DI container** — rejected per the module-local convention
  above; would blur the boundary between cross-module and module-local components.

## Status

`dgt.power.plugin` (`plugin push`) is implemented and wired into the CLI. `webresource push` and the
deprecation warning on the legacy `push` command are not yet started.
