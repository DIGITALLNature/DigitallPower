# Telemetry and Privacy

DigitallPower collects anonymous usage telemetry to help improve the tool. Telemetry is **opt-out** — it is enabled by default but can be easily disabled.

## What is collected

| Data | Example | Purpose |
|------|---------|---------|
| Command name | `UserRoleImport` | Understand which modules are used |
| Success/failure | `true` | Track reliability |
| CI environment | `true` | Distinguish interactive vs automated usage |
| OS platform | `Unix` | Platform distribution |
| Tool version | `2.1.0` | Version adoption |
| Anonymous install ID | `a1b2c3d4-...` | Count unique installations |
| Crash data | `ExceptionType`, `anonymized stacktrace` | Improve reliability by identifying common crashes |

**No personally identifiable information is collected.** Error messages and stack traces are automatically anonymized before being sent — the following data is stripped or replaced with fixed placeholders:

| Found in error data | Replaced with |
|---|---|
| GUIDs (record IDs, solution IDs, ...) | `00000000-0000-0000-0000-000000000000` |
| Local home-directory paths (`/Users/<name>/...`, `/home/<name>/...`, `C:\\Users\\<name>\\...`) | just the file name, e.g. `Program.cs` |
| Dataverse organization URLs (`https://contoso.crm4.dynamics.com/...`) | `[dataverse-org-url-redacted]` |
| Entra ID tenant URLs (`https://contoso.onmicrosoft.com/...`) | `[entra-tenant-url-redacted]` |

This anonymization is implemented in `TelemetryAnonymizer` and covered by unit tests for both Unix and Windows path formats. It is a best-effort, regex-based approach; it does not detect arbitrary free-form personal information.

CI detection is centralized in `dgt.power.common.ExecutionEnvironment` and currently recognizes `TF_BUILD`, `BUILD_BUILDURI`, `GITHUB_ACTIONS`, `GITLAB_CI`, `JENKINS_URL`, and `CI`.

## Managing telemetry

```bash
dgtp telemetry status    # Show current state and the telemetry notice
dgtp telemetry enable    # Persistently enable telemetry
dgtp telemetry disable   # Persistently disable telemetry
```

See [`telemetry` commands](../reference/commands/telemetry.md) for the full command reference.

**Per invocation:**

```bash
dgtp export --no-telemetry
```

**Environment variable (takes precedence over the saved preference):**

```bash
export DGTP_TELEMETRY_OPTOUT=1
```

Set `DGTP_TELEMETRY_OPTOUT` or the standard `DO_NOT_TRACK` variable to `1`, `true`, or `yes` to permanently disable telemetry.

**Override telemetry endpoint (advanced):**

```bash
export DGTP_TELEMETRY_CONNECTION_STRING="InstrumentationKey=..."
```

Set `DGTP_TELEMETRY_CONNECTION_STRING` to an Azure Monitor connection string to route telemetry to a custom endpoint. When not set, the build-time embedded connection string is used (or telemetry is disabled if none was embedded).

## Example query

To count how often each module was invoked, split by CI and non-CI usage:

```kusto
dependencies
| where timestamp > ago(30d)
| extend
    module = tostring(customDimensions["dgtp.command"]),
    is_ci = tolower(tostring(customDimensions["dgtp.is_ci"])) == "true"
| where isnotempty(module)
| summarize CI = countif(is_ci), NonCI = countif(not(is_ci)) by module
| order by CI + NonCI desc
```

## Retrieving crash/error data in Application Insights

Every exception (whether it terminates a command, or crashes the process entirely) is recorded both as an OpenTelemetry span attribute (for quick filtering alongside command telemetry) **and** as a standard OpenTelemetry exception event, which the Azure Monitor exporter surfaces natively in the **`exceptions`** table / the **Failures** blade of the Application Insights resource.

**To look at it in the Azure Portal:**

1. Open the Application Insights resource associated with the connection string configured via `DGTP_TELEMETRY_CONNECTION_STRING` (or the build's embedded default).
2. Go to **Investigate → Failures** for a quick overview of the most frequent exception types and their trend over time, or
3. Go to **Monitoring → Logs** and run a KQL query directly, e.g.:

```kusto
// Most frequent (anonymized) exception types over the last 30 days
exceptions
| where timestamp > ago(30d)
| extend
    exceptionType = tostring(customDimensions["exception.type"]),
    version = tostring(customDimensions["dgtp.version"]),
    isFatal = operation_Name == "fatal_exception"
| summarize Count = count() by exceptionType, version, isFatal
| order by Count desc
```

```kusto
// Full anonymized message + stack trace for a specific exception type
exceptions
| where timestamp > ago(30d)
| where tostring(customDimensions["exception.type"]) == "System.IO.DirectoryNotFoundException"
| project timestamp, message = tostring(customDimensions["exception.message"]), stack = tostring(customDimensions["exception.stacktrace"]), version = tostring(customDimensions["dgtp.version"])
| order by timestamp desc
```

Since crashes outside of a command's lifecycle (startup failures, unobserved task exceptions) are recorded as their own `fatal_exception` operation rather than under a `command.*` operation, filter on `operation_Name == "fatal_exception"` to isolate those from regular command-scoped exceptions.

## First-run notice

On the first command invocation with telemetry enabled, the CLI creates an anonymous installation
ID and displays a one-time notice explaining collection and opt-out. Invocations with
`--no-telemetry` or an environment opt-out do not create this ID or display the notice. Help,
version output, and shell completion do not initialize telemetry.

The installation ID is retained across tool upgrades, including major releases, so the notice
does not repeat. Disabling and re-enabling telemetry does not reset it. Using a new application
home causes the notice to appear again on the next telemetry-enabled command.

## Persistent application state

The anonymous installation ID, telemetry preference, and last update-check date are stored in
`state.json` alongside `connections.json` in the per-user application data directory.
Set `DGTP_HOME` to override this location. The directory is shared across tool versions.
On Windows, the default is `%LOCALAPPDATA%\\dgtp`; on Linux, it is `$XDG_DATA_HOME/dgtp`
or `~/.local/share/dgtp`. Other platforms use the local application data directory's `dgtp` folder.

To change the saved telemetry preference, use `dgtp telemetry enable` or `dgtp telemetry disable`.
For advanced use, `state.json` can still be edited directly while dgtp is not running — set
`telemetry.enabled` to `false` or `true`, leaving the other fields unchanged. It defaults to `true`.
Environment opt-outs and `--no-telemetry` always take precedence over this preference.
