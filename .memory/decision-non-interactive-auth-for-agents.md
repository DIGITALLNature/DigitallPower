# Decision: Non-Interactive Mode and Auth-Check for Coding Agents

## Context

When a user token expires, Azure Identity may require interactive authentication and open a browser. Coding agents (Copilot, Claude, Cursor, etc.) have no visibility into this — the CLI can wait for user interaction without a useful exit code.

## Decision

Implement three complementary mechanisms to make auth failures agent-friendly:

### 1. Non-Interactive Mode (`--non-interactive` / `DGTP_NON_INTERACTIVE`)

**Activation (choose one):**
- Pass `--non-interactive` as a CLI flag
- Set env var `DGTP_NON_INTERACTIVE=true` (or `=1`) before invoking any command

**Behavior:**
- Azure Identity credentials are configured with `DisableAutomaticAuthentication` in non-interactive mode, so they do not open a browser.
- The exception handler in `Program.cs` returns exit code `2` (`ExitCode.AuthRequired`).
- A clear `AUTH_REQUIRED: ...` message is printed to stdout.

The env var approach is preferred for agents because it works even when the connection is resolved during DI setup (before command args are available).

### 2. `dgtp connection status` Command (replaces `dgtp profile auth-check`)

A no-op pre-flight command that:
- Tries a silent-only MSAL token acquire (no browser, no side effects).
- Returns exit code `0` if the token is valid.
- Returns exit code `2` if interactive login is required.
- For connection-string (non-MSAL) connections, always returns `0`.

**Intended agent workflow:**
```bash
dgtp connection status
# if exit code == 2:
#   tell user: "Please re-authenticate. Run: dgtp connection refresh"
#   wait for user confirmation (or run refresh directly)
#   retry dgtp connection status
# proceed with actual command
```

### 3. `dgtp connection refresh` Command

Forces an interactive MSAL browser login for the active connection and persists the refreshed token.
For connection-string connections this is a no-op.

```bash
dgtp connection refresh   # opens browser, user logs in, token saved → exit 0
```

### 4. Refresh status output

`dgtp connection refresh` prints a status marker before starting interactive authentication:
```
AUTH: Starting interactive authentication...
```

This lets an agent monitoring stdout identify the interactive refresh operation.

## Alternatives Considered

- **Machine-readable JSON output flag:** Too invasive — would require every command to emit JSON.
- **Retry with timeout:** Doesn't help — just delays the hang.
- **Dedicated `auth login` command:** Less useful than `connection status` because login is already handled by `connection create` / `connection refresh`.
- **`profile auth-check` (original):** Renamed to `connection status`; the legacy `profile` branch was removed in v3.

## Exit Code Semantics

| Code | Meaning |
|------|---------|
| `0`  | Success |
| `1`  | Error (generic) |
| `2`  | Auth required — interactive login needed, tool was blocked from opening browser |

## Current implementation mapping

- `BaseProgramSettings` exposes `--non-interactive`; `ConnectionInvocationOptions` resolves it with
  `DGTP_NON_INTERACTIVE`.
- `CredentialFactory` constructs Azure.Identity user credentials with
  `DisableAutomaticAuthentication` when non-interactive mode is active.
- `IXrmConnection` / `XrmConnection` implement silent `CheckAuthAsync` and persisted
  `RefreshAuthAsync`.
- `ConnectionStatusCommand` returns `ExitCode.AuthRequired` (`2`) when login is required; the
  `connection` branch is the only connection command branch in v3.
