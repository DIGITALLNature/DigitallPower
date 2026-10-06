# Runtime error diagnostics

The CLI-host exception handler and per-target `plugin push` failure handler use the shared
`ExceptionExtensions.DiagnosticMessage()` formatter. Both preserve exception-chain messages
and show `OrganizationServiceFault` error codes (unsigned eight-digit hex) and inner-fault
messages. Repeated identical lines are collapsed.

Do not gate these diagnostics on `DEBUG`/`RELEASE`: a release deployment needs enough information
to diagnose platform failures. Removing the host conditional alone is insufficient because plugin
push catches target failures before they reach that handler.

The default output deliberately excludes stack traces, `TraceText`, exception `Data`, and arbitrary
fault `ErrorDetails`. Those can contain record or configuration payloads and are unnecessary for
the concise diagnostic path. Exception and service messages are not guaranteed to be free of
environment-specific data. Console rendering must escape them using interpolated Spectre markup.

Interactive-login failures retain their dedicated authentication message and exit code. Telemetry
ownership and redaction are unchanged; local console diagnostics are independent of telemetry.

References:
- `src/dgt.power.common/Extensions/ExceptionExtensions.cs`
- `src/dgt.power/Program.cs`
- `src/modules/dgt.power.plugin/Commands/PluginPushCommand.cs`
- `tests/dgt.power.cli.tests/ExceptionExtensionsTests.cs`
