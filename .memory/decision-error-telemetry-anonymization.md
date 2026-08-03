# Decision: Error Telemetry and Anonymization

## Context

To improve the reliability of the DigitallPower CLI, we need to collect crash data and exception details. However, since the tool is used in enterprise environments, we must ensure that no personally identifiable information (PII) or sensitive environment data (like Dataverse organization URLs or user paths) is transmitted.

## Decision

We will implement automated error telemetry that captures exceptions from three main entry points:
1. **Spectre.Console Exception Handler:** Captures all exceptions thrown during command execution.
2. **AppDomain.CurrentDomain.UnhandledException:** Captures fatal crashes that happen outside of the command lifecycle (e.g., during startup).
3. **TaskScheduler.UnobservedTaskException:** Captures exceptions from leaked tasks.

### Anonymization Strategy

To protect user privacy, all captured error data must be anonymized before transmission:

1. **GUIDs:** All GUIDs (including Dataverse record IDs, solution IDs, etc.) are replaced with a zero-GUID (`00000000-0000-0000-0000-000000000000`).
2. **Local home-directory paths:** Absolute paths under a recognizable home-directory root (`/Users/<name>/...`, `/home/<name>/...`, `/root/...`, `C:\Users\<name>\...`) are shortened to just the file name (e.g. `/Users/jdoe/Source/dgtp/src/dgt.power/Program.cs` becomes `Program.cs`). This applies to both exception **messages** and **stack traces** — messages of exceptions like `DirectoryNotFoundException` or `FileNotFoundException` frequently embed the full local path and, with it, the OS username.
3. **Stack trace frames:** Every `at ... in <path>:line N` frame additionally has its path shortened to the file name regardless of which root it lives under (build-machine paths without a home directory still reveal folder layout, even if they don't carry a username).
4. **Dataverse organization URLs:** Any `https://<org>[.api][.crmN].dynamics.com/...` URL is replaced with the fixed placeholder `[dataverse-org-url-redacted]` — Dataverse SDK exceptions routinely embed the org URL, which directly identifies the customer and is at least as sensitive as a record GUID.
5. **Entra ID tenant URLs:** Any `https://<tenant>.onmicrosoft.com/...` URL is replaced with `[entra-tenant-url-redacted]` for the same reason.
6. **Exception messages:** GUID, path, and URL anonymization above is applied uniformly to `error.Message`.

**Known scope limitations (accepted trade-offs):**
- Only paths under a recognized home-directory root are stripped from *messages* (not every conceivable absolute path) — this avoids false positives that would otherwise mangle unrelated text such as generic URLs (`/tmp/`, `/var/`, arbitrary API paths were deliberately excluded from the message-level regex; see `TelemetryAnonymizerTests.Anonymize_WithUnrelatedUrl_LeavesUrlUntouched`).
- Freeform PII embedded directly in a message without a recognizable path/URL shape (e.g. `"User jdoe attempted ..."`, email addresses) is **not** detected. Anonymization here is regex-based and best-effort, not a guarantee.

## Implementation Details

- **`TelemetryAnonymizer`:** Static internal class in `dgt.power.Telemetry`. `Anonymize(string)` handles GUIDs, org/tenant URLs, and home-directory paths for single-line text (exception messages); `AnonymizeStackTrace(string)` additionally shortens every stack frame's path and then delegates to `Anonymize` for the rest. Line splitting uses a regex (`\r\n|\r|\n`) rather than `Environment.NewLine`, so a stack trace captured with different line endings than the current OS (e.g. crossing a serialization boundary) is still processed frame-by-frame instead of being treated as one giant unsplit line.
- **`ITracer.TrackFatalException`:** Added to the tracer interface to allow logging exceptions that occur outside of a specific `IPowerLogic` activity.
- **Exception recording as OTel events:** Both `Tracer.Exception` and `Tracer.TrackFatalException` record the anonymized exception as a standard OpenTelemetry **exception event** (`ActivityEvent("exception", tags: {exception.type, exception.message, exception.stacktrace, exception.escaped})`) on top of setting them as span tags. The Azure Monitor exporter only projects exception *events* — not plain span tags — into the Application Insights `exceptions` table / Failures blade, so this is required for crashes to show up as first-class exception telemetry rather than only as custom dimensions on a `dependency` row.
- **`Program.cs` Integration:** The global handlers are registered as early as possible after the tracer is initialized.

### Provider Lifecycle

- Global exception handlers must only record errors through `ITracer`; they must never flush or dispose the local `TracerProvider` captured during startup.
- `Program.cs` owns the provider and flushes/disposes it exactly once from its normal `finally` block after removing the global handler subscriptions.
- `TrackFatalException` starts its activity without an explicit name so the caller-info default (`TrackFatalException`) becomes the stable operation name.

## Alternatives Considered

- **Transmitting full stack traces:** Rejected due to privacy concerns regarding local user names in paths.
- **Transmitting full messages without anonymization:** Rejected because Dataverse error messages often contain specific record GUIDs.
- **Removing messages entirely:** Rejected because it would make debugging significantly harder. The anonymization strike a balance between privacy and utility.

## Consequences

- Improved ability to identify and fix recurring crashes.
- Transparent documentation in README.md about what is collected.
- Small performance overhead for regex replacement during crashes (acceptable since the app is terminating anyway).
