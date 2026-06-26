# Decision: Non-Interactive Mode and Auth-Check for Coding Agents

## Context

When MSAL token expires, `TokenConnector.GetTokenAsync()` silently falls through to `AcquireTokenInteractive`, which opens a browser window. Coding agents (Copilot, Claude, Cursor, etc.) have no visibility into this — the CLI hangs waiting for user interaction with no output or exit code change.

## Decision

Implement three complementary mechanisms to make auth failures agent-friendly:

### 1. Non-Interactive Mode (`--non-interactive` / `DGTP_NON_INTERACTIVE`)

**Activation (choose one):**
- Pass `--non-interactive true` as a CLI flag (picked up by Spectre and by `IConfiguration.AddCommandLine`)
- Set env var `DGTP_NON_INTERACTIVE=true` (or `=1`) before invoking any command

**Behavior:**
- When `MsalUiRequiredException` is caught in `TokenConnector.GetTokenAsync()` and non-interactive mode is active, throw `InteractiveLoginRequiredException` instead of opening the browser.
- The exception handler in `Program.cs` returns exit code `2` (`ExitCode.AuthRequired`).
- A clear `AUTH_REQUIRED: ...` message is printed to stdout.

The env var approach is preferred for agents because it works even when the connection is resolved during DI setup (before command args are available).

### 2. `dgtp profile auth-check` Command

A no-op pre-flight command that:
- Tries a silent-only MSAL token acquire (no browser, no side effects).
- Returns exit code `0` if the token is valid.
- Returns exit code `2` if interactive login is required.
- For non-MSAL (classic connection string) profiles, always returns `0`.

**Intended agent workflow:**
```bash
dgtp profile auth-check
# if exit code == 2:
#   tell user: "Please re-authenticate. Run: dgtp profile create <name> <url> --msal"
#   wait for user confirmation
#   retry dgtp profile auth-check
# proceed with actual command
```

### 3. Clear Console Output Before Browser Launch (interactive mode only)

When non-interactive mode is NOT active and interactive login is triggered, `TokenConnector` now emits:
```
AUTH: Silent token acquisition failed — opening browser for interactive login...
```

This allows any agent monitoring stdout to detect the interactive login event even without non-interactive mode.

## Alternatives Considered

- **Machine-readable JSON output flag:** Too invasive — would require every command to emit JSON.
- **Retry with timeout:** Doesn't help — just delays the hang.
- **Dedicated `auth login` command:** Less useful than `auth-check` because login is already handled by `profile create`.

## Exit Code Semantics

| Code | Meaning |
|------|---------|
| `0`  | Success |
| `1`  | Error (generic) |
| `2`  | Auth required — interactive login needed, tool was blocked from opening browser |

## Files Changed

- `src/dgt.power.common/Commands/ExitCode.cs` — `AuthRequired = 2`
- `src/dgt.power.common/Exceptions/InteractiveLoginRequiredException.cs` — new exception
- `src/dgt.power.common/BaseProgramSettings.cs` — `--non-interactive` flag
- `src/dgt.power.common/Logic/TokenConnector.cs` — non-interactive mode + `TryAcquireTokenSilentAsync` + clear message
- `src/dgt.power.common/IXrmConnection.cs` — `CheckAuthAsync()` method
- `src/dgt.power.common/Logic/XrmConnection.cs` — `CheckAuthAsync` impl + `IsNonInteractive()` helper
- `src/modules/dgt.power.profile/Commands/AuthCheckCommand.cs` — new command
- `src/dgt.power/Program.cs` — register `auth-check`, handle `InteractiveLoginRequiredException`
- `tests/dgt.power.tests/TestConnection.cs` — stub `CheckAuthAsync` → `Task.FromResult(true)`

