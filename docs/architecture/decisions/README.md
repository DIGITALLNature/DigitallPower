# Architecture Decisions

These records preserve rationale that is not obvious from current code or user-facing behavior.
Current requirements are maintained in OpenSpec; implementation details remain with their code and tests.

- [Resource-oriented command modules](resource-oriented-command-modules.md): isolate deployment modules by Dataverse resource.
- [Plan before execution](plan-before-execution.md): share a typed plan between preview and mutation.
- [Versioned code-generation configuration](versioned-codegeneration-config.md): preserve V1 while routing V2 to typed targets.
- [Telemetry data protection](telemetry-data-protection.md): balance diagnostic utility with best-effort redaction.
- [Plugin reconciliation core](plugin-reconciliation-core.md): snapshot, change set, apply for plugin push; supersedes plan-before-execution there.
- [Custom API association ownership](custom-api-association-ownership.md): push reconciles handler links, never API records.
- [Data provider ownership](data-provider-ownership.md): declarations own handler slots, not provider records or tables.
- [Environment-owned plugin identities](environment-owned-plugin-identities.md): managed identities stay outside push.
- [Plugin module namespace layout](plugin-module-namespace-layout.md): shared code at top level, feature internals grouped per feature.
- [Typed connection storage](typed-connection-storage.md): version-stable home directory, separate metadata, secrets, and token caches.
- [Azure DevOps Workload Identity Federation](azure-devops-workload-identity-federation.md): `AzurePipelinesCredential` with a self-constructed OIDC request URI.
- [Non-interactive authentication](non-interactive-authentication.md): exit code 2 and status/refresh commands for coding agents.
- [Keep certificate authentication](keep-certificate-authentication.md): retained without live validation.
- [Dependency ownership](dependency-ownership.md): dependency cleanup is a dedicated solution-wide change.
- [Command composition](command-composition.md): thin lifecycle base, explicit services and a cached `IDataverseConnection`.
