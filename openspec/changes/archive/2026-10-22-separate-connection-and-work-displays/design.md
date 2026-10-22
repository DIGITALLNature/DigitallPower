# Design

## Context

See proposal.md for motivation. `DataverseConnection.GetServiceAsync` serializes initial acquisition and caches only a verified service. Its `ConnectUsingAsync` currently starts a Spectre status display around credential creation and verification, while printing durable sanitized outcome messages. `CredentialFactory` can launch browser authentication or print device-code instructions.

The existing uncommitted codegeneration fix acquires the connection before the generation display and updates constructor call sites, but adds no behavioral regression test. The earlier test invocation is not sufficient evidence: implementation must run the built test DLL via `dotnet exec` and verify a real test count.

Inspected webresource push, solution lint, solution component copy, maintenance status/progress operations, and analyzer live-display operations already acquire their services before work displays. Plugin push hands an already-connected service to deployment orchestration. These are patterns to preserve, not reasons for speculative rewrites.

## Goals / Non-Goals

**Goals:** Establish one owner for work-display lifetimes; preserve connection error propagation, feedback safety, lazy acquisition, and authentication policy; prove both the shared boundary and codegeneration ordering.

**Non-Goals:** Enforce the boundary with a new generic runtime framework, remove every console dependency, replace all module service constructors, or redesign token refresh.

## Decisions

### Remove only the connection-owned live display

Flatten the body of `ConnectUsingAsync` without changing its creation/verification order, original exception propagation, disposal on unsuccessful acquisition, or sanitized success/failure lines. Keep cancellation excluded from connection failure reporting.

Plain console messages do not claim Spectre's exclusive live-display context. Keeping them avoids unnecessarily redesigning all connection feedback. Moving every message into callers would duplicate reporting or require a new notification abstraction.

### Commands own initial acquisition and interactive phases

Keep the codegeneration connection acquisition after configuration validation and before the generation status. Document the ordering contract on `IDataverseConnection` and in contributor guidance: initial acquisition can authenticate; business services must not be the first acquirer inside a work display.

Keep `PowerCommand` responsible only for telemetry and the final result line. Eager host/base-class authentication would contact Dataverse for local failures and commands that do not use it.

Connected-service injection, already used by plugin deployment, remains the preferred pattern when new orchestration constructs business operations. Converting all existing metadata/generator dependencies is not necessary for this bounded fix.

### Test display ownership and ordering separately

Add a connection-layer test invoking real connection acquisition inside a Spectre display with deterministic local credential failure. It must reach the expected credential error, preserve safe failure feedback, and not fail with a nested-display exception. This is a defensive infrastructure test, not permission to initiate interactive authentication inside work displays.

Add command tests using a dedicated console and a connection double that exercises Spectre exclusivity during acquisition. Prove valid generation acquires before the work display, and prove acquisition failure prevents metadata/generator execution. Retain invalid-config/cancellation coverage and cover both generation targets where relevant. Make display capability settings explicit so redirected or non-interactive defaults cannot make the test vacuous.

Use existing connection-verifier/connector test seams for success, cancellation, and disposal checks where available; no real network or interactive login belongs in automated tests.

### Avoid generic locking or spinner suppression

A lock around all displays can deadlock when the outer callback waits for an inner display. Silently dropping displays masks ownership errors and still does not make arbitrary authentication safe. Explicit phases solve the demonstrated failure without global coordination.

## Risks / Trade-offs

- [Connection acquisition no longer animates] -> Retain durable sanitized outcome messages; do not replace the hidden spinner with another implicit display.
- [Existing services can still lazily authenticate if misused] -> Document the boundary and test command ordering; defer a broad connected-service dependency migration.
- [Headless consoles may not reproduce exclusive-display behavior] -> Configure a dedicated interactive-capable test console and verify tests fail against the original ordering/display implementation.
- [Token refresh can occur after initial acquisition] -> Preserve SDK authentication behavior; this change removes the application's nested connection display but does not promise that every future credential event is a separate command phase.
- [Existing worktree edits may be mistaken for a complete fix] -> Integrate them without reverting unrelated work and validate through built DLL execution with reported test counts.

## Migration Plan

No stored-data, configuration, dependency, or CLI migration is needed. Keep the ordering contract in interface/contributor guidance; leave the README unchanged. Sync the two capability deltas after implementation, validate all OpenSpec artifacts, and archive on completion. Rollback is a code revert; no state conversion is involved.
