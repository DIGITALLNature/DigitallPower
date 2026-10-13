# Architecture Decisions

These records preserve rationale that is not obvious from current code or user-facing behavior.
Current requirements are maintained in OpenSpec; implementation details remain with their code and tests.

- [Resource-oriented command modules](resource-oriented-command-modules.md): isolate deployment modules by Dataverse resource.
- [Plan before execution](plan-before-execution.md): share a typed plan between preview and mutation.
- [Versioned code-generation configuration](versioned-codegeneration-config.md): preserve V1 while routing V2 to typed targets.
- [Telemetry data protection](telemetry-data-protection.md): balance diagnostic utility with best-effort redaction.