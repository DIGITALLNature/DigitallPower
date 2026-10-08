# Telemetry Data Protection

## Context

Crash telemetry helps diagnose failures, but Dataverse URLs, record identifiers, and local paths can expose customer or user information.

## Decision

Record anonymized exception telemetry, replacing GUIDs, recognized Dataverse and Entra tenant URLs, and recognizable home-directory paths. Shorten stack-frame paths to filenames.

## Rationale

Removing all exception detail would make crash reports much less useful. Best-effort redaction retains diagnostic context while reducing common identifiers.

## Consequences

Redaction is regex-based and is not a guarantee against arbitrary free-form PII such as names or email addresses. Keep the limitation explicit in user-facing telemetry documentation and tests; do not describe the process as complete anonymization.