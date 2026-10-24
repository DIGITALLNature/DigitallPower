# Design

## Context

The CLI already has telemetry infrastructure in `src/dgt.power/Telemetry/` (TelemetryState, TelemetryInterceptor, etc.) and persists telemetry preferences in `state.json` via `StateStore` in `src/dgt.power.common/Storage/`. Currently, users can only change the persisted preference by manually editing `state.json` or using environment variables. See proposal.md for motivation.

The existing module pattern (e.g., `dgt.power.connection`) shows how to create a self-contained feature module: a `.csproj` referencing `dgt.power.common`, command classes deriving from `PowerCommand<TSettings>` or `Command<TSettings>`, and a settings class deriving from `BaseProgramSettings`. Connection commands demonstrate that commands which don't need Dataverse simply don't inject `IDataverseConnection`.

## Goals / Non-Goals

**Goals:**
- Provide `dgtp telemetry status|enable|disable` commands that read and write the persisted telemetry preference via `StateStore`
- Follow the existing module structure exactly (new `dgt.power.telemetry` module under `src/modules/`)
- No Dataverse authentication required for any telemetry command
- Environment opt-outs and `--no-telemetry` continue to take precedence over the persisted preference

**Non-Goals:**
- Changing the telemetry collection mechanism (OpenTelemetry spans, Azure Monitor exporter)
- Adding a `dgtp telemetry reset` command to clear the install ID
- Modifying the state file schema or migration logic
- Changing the first-run telemetry notice behavior

## Decisions

- **New module `dgt.power.telemetry`**: Follows the existing pattern of one module per feature area. The module is small (3 commands + settings) and cohesive. Placing it in `src/modules/` keeps the CLI host (`dgt.power`) clean and matches how `dgt.power.connection` is organized.

- **Commands derive from `PowerCommand<TSettings>`**: All three commands are traced units (they produce telemetry spans). Using `PowerCommand<TSettings>` gives us the telemetry span and end-line behavior for free, consistent with the command-execution spec. The settings class derives from `BaseProgramSettings` to inherit `--no-telemetry` and other shared options.

- **Commands do NOT inject `IDataverseConnection`**: Following the convention that commands which never touch Dataverse don't request it, the telemetry commands only inject `TelemetryStateProvider`, `IAnsiConsole`, and `ICommandSpanFactory`. This means they never authenticate.

- **Settings classes**: Two settings types:
  - `TelemetrySettings : BaseProgramSettings` — used for the branch (no additional options)
  - Individual commands use `TelemetrySettings` directly since `status`, `enable`, and `disable` have no command-specific options beyond the inherited ones.

- **`status` output format**: The telemetry notice panel is displayed first (fixed context), followed by the status line with a colored circle indicator (green `●` for enabled, red `●` for disabled, matching the plugin step management pattern) and a how-to line. When an environment opt-out overrides the persisted preference, a second line states "DGTP_TELEMETRY_OPTOUT / DO_NOT_TRACK is set.". Placing the variable status output after the fixed notice panel makes the status line the last (and most prominent) content, and simplifies scripting (`dgtp telemetry status | tail -2`).

  Example outputs:
  ```
  ┌─ Telemetry Notice ──────────────────────────────────┐
  │ ...                                                 │
  └─────────────────────────────────────────────────────┘

  [green]●[/] Telemetry is enabled.
  Run [bold]dgtp telemetry disable[/] to disable telemetry.
  ```

  ```
  ┌─ Telemetry Notice ──────────────────────────────────┐
  │ ...                                                 │
  └─────────────────────────────────────────────────────┘

  [red]●[/] Telemetry is disabled.
  Run [bold]dgtp telemetry enable[/] to enable telemetry.
  ```

  ```
  ┌─ Telemetry Notice ──────────────────────────────────┐
  │ ...                                                 │
  └─────────────────────────────────────────────────────┘

  [red]●[/] Telemetry is disabled.
  DGTP_TELEMETRY_OPTOUT / DO_NOT_TRACK is set.
  ```

- **Move `TelemetryNotice` to `dgt.power.common`**: The `TelemetryNotice` class currently lives in `src/dgt.power/Telemetry/` as `internal`. It is a cross-cutting UI concern (like `StorageSecurityNotice` which already lives in common). Move it to `dgt.power.common/Telemetry/TelemetryNotice.cs`, make it `public`, and update `Program.cs` to use the new namespace. Both the main app (first-run notice) and the telemetry module (`TelemetryStatusCommand`) can then use it.

- **Create `TelemetryStateProvider` in `dgt.power.common`**: The effective telemetry state is a combination of the persisted preference (`StateStore.TelemetryEnabled`) and environment opt-outs (`DGTP_TELEMETRY_OPTOUT` / `DO_NOT_TRACK`). Previously this logic was fragmented across `TelemetryConfig` (internal to `dgt.power`) and inline checks in the telemetry commands. Consolidate it into a single `public sealed class TelemetryStateProvider` in `dgt.power.common/Telemetry/` with three members: `IsEnabled` (persisted && !env opt-out), `HasOptOutEnvVar` (env var check, inlined from the deleted `TelemetryEnvironment`), and `SetEnabled(bool)` (delegates to `StateStore`). The provider is registered as a DI singleton in `Program.cs`. This makes `TelemetryConfig` and `TelemetryEnvironment` redundant — both are deleted. `TelemetryConfig.IsCi` call sites now use `ExecutionEnvironment.IsCiAgent` directly (the passthrough added no value). Per-invocation suppression (`--no-telemetry`) remains in `TelemetryInterceptor`/`TelemetryState` — it is an invocation-time concern, not a configuration query.

- **Module dependency rule**: Modules only reference `dgt.power.common`, never each other. This is already the case for all 9 existing modules (verified: zero module-to-module `ProjectReference` entries). The new `dgt.power.telemetry` module follows the same rule — its `.csproj` references only `dgt.power.common`.

- **`enable`/`disable` confirmation**: A single confirmation line with the same colored circle indicator used in `status` — green `●` for enable, red `●` for disable. When `enable` runs while an environment opt-out is active, the green circle is replaced with a warning emoji (`Emoji.Known.Warning`) to signal that the preference was saved but will not take effect, followed by a second line "DGTP_TELEMETRY_OPTOUT / DO_NOT_TRACK is set."

  ```
  [green]●[/] Telemetry is now enabled.
  ```

  ```
  [red]●[/] Telemetry is now disabled.
  ```

  ```
  ⚠️ Telemetry is now enabled.
  DGTP_TELEMETRY_OPTOUT / DO_NOT_TRACK is set.
  ```

- **Install ID preservation**: `StateStore.SetTelemetryEnabled(false)` already preserves the install ID (verified in `StateStoreTests`). No additional handling needed.

- **Test project `dgt.power.telemetry.tests`**: Mirrors the module structure. Tests use TUnit. Since commands don't need Dataverse, tests are simpler than connection tests — they only need a `TelemetryStateProvider` (wrapping a `StateStore` with a temp `DgtpHome`) and a `TestConsole`. The old `TelemetryConfigTests` in `dgt.power.cli.tests` is replaced by `TelemetryStateProviderTests` covering `IsEnabled`, `HasOptOutEnvVar`, and `SetEnabled`; `IsCi` and `GetOrCreateInstallId` tests are removed as they tested passthroughs already covered by `ExecutionEnvironmentTests` and `StateStoreTests`.

## Risks / Trade-offs

- [Users may expect `dgtp telemetry disable` to also stop the current invocation's telemetry] → The command itself produces a telemetry span (since it's a `PowerCommand`), but the state change takes effect on the next invocation. This is documented in the README.
- [Race condition if state.json is edited while a telemetry command runs] → `StateStore` already uses file locking (`AcquireLock`) with a 10-second timeout, so concurrent access is safe.
- [New module adds a project to the solution] → Minimal overhead; follows the established pattern. The module has no external package dependencies beyond `dgt.power.common`.

## Migration Plan

No migration needed. This is a purely additive change — new commands that use existing infrastructure. Existing `state.json` files continue to work unchanged.

## Open Questions

None.
