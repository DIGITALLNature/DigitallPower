# Decision: resource-oriented CLI restructuring (planned 2026-09-21)

Status: **planned, rollout in progress** - Phase 0 (Sarif.Sdk), Phase 1 (`dgtp solution lint`) and
Phase 2 (`dgtp solution version`) done; Phase 3+ not started.
Update this file's status line as phases land, and prune the "Open items" section as they resolve.

Team decision: move the whole `dgtp` CLI towards a resource-oriented shape (a command acts on ONE
instance of a named resource: `dgtp <resource> <verb> <target> [options]`), mirroring a pattern a
colleague already established on the (unmerged, remote-only) branch `feat/plugin-push-v2` for the
"plugin" resource:

```csharp
// Base/<Resource>Settings.cs - branch-level marker, no members
public class PluginSettings : BaseProgramSettings;

// Commands/<Resource><Verb>Settings.cs - concrete settings, single-target positional arg
public class PluginPushSettings : PluginSettings
{
    [CommandArgument(0, "<Target>")] public required string Target { get; set; }
    ...
}

// CommandTree.cs
config.AddBranch<PluginSettings>("plugin", plugin =>
{
    plugin.AddCommand<PluginPushCommand>("push").WithAlias("register")...
});
```

Module is named after the noun/resource (`dgt.power.plugin`), not the verb.

## Confirmed decisions (do not re-litigate without new information from the team)

1. **`dgt.power.linter` is renamed to `dgt.power.solution`** (project, namespace, test project) -
   not just a CLI surface change.
2. **Old `analyze` and `maintenance` CLI branches/modules are hard-removed** once their commands are
   ported - no `[DeprecatedCommand]` alias/transition period (unlike `profile` → `connection`). This
   is internal tooling, not a public API with external consumers to protect.
3. **The ported solution-version command is named `dgtp solution version`** (resource name is not
   repeated in the verb, matching `plugin push`, not `plugin plugin-push`).
4. **Multi-solution support is dropped.** `dgtp solution lint` takes a single
   `[CommandArgument(0, "<Solution>")]`, not `--solutions sol1,sol2`. Deliberate breaking change -
   resource-oriented commands act on one resource instance at a time.
5. **`Sarif.Sdk` (NuGet, MIT, Microsoft-owned, netstandard2.0, currently 5.7.0) replaces the
   hand-rolled SARIF POCOs in `Reporting/SarifWriter.cs`.** Done first, isolated, before Phase 1.
6. **Commit/versioning style: one single `feat!:` + `BREAKING CHANGE:` commit at the very end** of
   the whole restructuring, not per phase. Intermediate phase commits use normal
   `feat`/`fix`/`refactor` types without the breaking-change marker.

## Maintenance command → target resource mapping (documentation only for now - see scope note below)

**Scope for now: only the `solution`-targeted rows are actually being ported.** Everything else in
this table stays untouched in `dgt.power.maintenance`/`maintenance` for the time being - no `column`
or `workflow` module is being created yet. This table just records the eventual target so we don't
have to re-derive it later; it is not a commitment to build those modules now.

| `maintenance` command | Target | New command (proposed) | In scope now? |
|---|---|---|---|
| `solution-version` (`IncrementSolutionVersion`) | `solution` | `dgtp solution version` | **done** (Phase 2) |
| `removeredundantcomponents` (`RemoveRedundantComponents`) | `solution` | `dgtp solution remove-redundant-components` | yes - not started yet |
| `autonumber` (`AutoNumberFormatAction`) | `column` (future module) | `dgtp column autonumber` | no - stays in maintenance |
| `protectfields` (`ProtectCalculatedFields`) | `column` (future module) | `dgtp column protect` | no - stays in maintenance |
| `createworkflowstate` (`CreateWorkflowStateConfig`) | `workflow` (future module) | `dgtp workflow create-state-config` | no - stays in maintenance |
| `workflowstate` (`UpdateWorkflowState`) | `workflow` (future module) | `dgtp workflow set-state` | no - stays in maintenance |
| `filterfxplugins` (`FilterPowerFxPluginSteps`) | `plugin` (colleague's in-progress module) | `dgtp plugin filter-fx-steps` | no - stays in maintenance |
| `ensuresdksteps` (`EnsureSdkStepStatus`) | `plugin` (colleague's in-progress module) | `dgtp plugin ensure-sdk-steps` | no - stays in maintenance |
| `carrierinfo` (`ExportCarrierInfo`) | `export` (existing branch) | `dgtp export carrierinfo` | no - stays in maintenance |
| `bulkdelete` (`BulkDeleteUtil`) | **undecided** - no clean resource fit | TBD | no - stays in maintenance |

When a command IS ported (the two `solution` rows), it is removed from `maintenance` without an
alias, same hard-cut policy as everything else (decision #2) - but `maintenance` itself is **not**
removed as a module/branch until every one of its commands has an eventual new home, which is not
happening now.

## `analyze` → lint rule mapping (confirmed direction; exact rule ids TBD per rule when built)

| `analyze` command | Becomes lint rule (working id, may change) |
|---|---|
| `entityallassets` | `completeness.entity-all-assets` |
| `noactivelayer` | `layer.no-active-layer` |
| `redundantcomponents` | `completeness.redundant-components` |
| `redundantpatches` | `completeness.redundant-patch` |
| `activelayer` | `layer.managed-no-active-layer` (already anticipated in earlier linter architecture notes) |
| `toplayer` | `layer.not-top-layer` |

Once all six are ported as `ILintRule`s, delete `dgt.power.analyzer` and the `analyze` CommandTree
branch entirely (hard cut, per decision #2).

## Rollout order

0. **Sarif.Sdk swap - DONE.** `Reporting/SarifWriter.cs` now builds `Microsoft.CodeAnalysis.Sarif.SarifLog`/`Run`/`Result`/etc. and calls `log.Save(path)` (from the `Sarif.Sdk` NuGet package, added to `dgt.power.linter.csproj`) instead of hand-rolled POCOs + `System.Text.Json`. Output is compact (single-line) JSON - `Save` has no `Formatting` overload in this version, so tests assert on the compact `"key":"value"` shape (no space after `:`), not pretty-printed. Reading (`SarifLog.Load(path)`) replaces the old `JsonSerializer.Deserialize<SarifLog>`. Brings in `Newtonsoft.Json` as a transitive dependency (Sarif.Sdk's own serialization) - no version conflicts observed against the rest of the solution.
1. **Establish `dgtp solution lint` - DONE.** `dgt.power.linter` renamed to `dgt.power.solution`
   (project, namespace, `tests/dgt.power.solution.tests`). CLI branch is now `solution` with a
   `lint` subcommand; `LintVerb`/`LintRunCommand` renamed to `SolutionLintSettings`/
   `SolutionLintCommand` (new empty branch marker `SolutionSettings : BaseProgramSettings`), single
   required `[CommandArgument(0, "<Solution>")]` replaces `--solutions sol1,sol2`. Schema moved to
   `schemas/solution/lint/schema.json`. **Caveat discovered during this phase:** the actual
   runtime-registered command tree lives in a local `RegisterCommands` function inside
   `Program.cs`'s top-level statements, which is a hand-maintained duplicate of `CommandTree.cs`
   (used only by `dgt.power.cli.tests`) - the two had already drifted apart before this phase (e.g.
   `Program.cs` has a `connection` branch that `CommandTree.cs` does not). Both were updated in
   this phase to keep them in sync for the `solution`/`lint` branch, but the underlying duplication
   is pre-existing tech debt, out of scope for this restructuring.
2. **Pilot maintenance migration - DONE.** `IncrementSolutionVersion` → `dgtp solution version`.
   `IncrementSolutionVersionSettings` → `SolutionVersionSettings : SolutionSettings` (moved from
   `dgt.power.maintenance` to `src/modules/dgt.power.solution/Base/`), command class renamed
   `IncrementSolutionVersion` → `SolutionVersionCommand` (moved to
   `src/modules/dgt.power.solution/SolutionVersionCommand.cs`). Removed from the `maintenance`
   branch without an alias (hard cut, decision #2). Tests moved to
   `tests/dgt.power.solution.tests/SolutionVersionCommandTests.cs`; added
   `SettingsParsingTests.SolutionVersionSettings_ParsesPositionalArgumentAndFlag` (renamed from the
   old `IncrementSolutionVersionSettings_...` test).
3. **Not started yet.** The only other `solution`-scoped command, `removeredundantcomponents` →
   `dgtp solution remove-redundant-components`, still lives in `dgt.power.maintenance` -
   no `RemoveRedundantComponents`/`RemoveRedundantComponentsVerb` code has moved and there is no
   `solution remove-redundant-components` command yet. Everything else in the mapping table also
   stays in `dgt.power.maintenance` untouched - **no `column`/`workflow` module is created in this
   pass.** Revisit the rest of the table as a separate, later decision.
4. **`analyze` → lint rules**: port all six checks as `ILintRule`s, then delete `dgt.power.analyzer`
   + the `analyze` branch.
5. **Deferred, not started**: `--preset` feature for `dgtp solution lint` (e.g. `--preset
   recommended`, config layers on top: "recommended plus rule A, minus rule B").

## Open items to resolve during implementation

- Exact final rule ids for the six ported `analyze` checks (working ids above are placeholders).
- Where `bulkdelete` ends up.
- `column`/`workflow` (and moving `filterfxplugins`/`ensuresdksteps`/`carrierinfo`) are explicitly
  **out of scope for now** - only revisit once `solution` is fully done and stable.
