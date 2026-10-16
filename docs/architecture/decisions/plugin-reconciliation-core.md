# Plugin Reconciliation Core

## Context

The earlier push pipeline built a nested plan that the renderer, executors, and change check each interpreted. A failed standalone upgrade could also block later pushes, and ownership of shared resources was implicit.

## Decision

Replace the deployment core with snapshot, change set, apply. Local declarations are validated, a scoped remote snapshot is loaded, and one diff produces flat change collections that are rendered and applied without re-deciding. Registration is authoritative within a defined ownership scope: all non-internal steps on the deployed plugin types. Matching uses scoped composite identity (message, stage, mode, entities); persisted or generated keys are deferred. Interrupted standalone upgrades are recovered conservatively. Independent targets continue after a failure, and cancellation stops the run. This supersedes [Plan before execution](plan-before-execution.md) for plugin push.

## Rationale

A flat change set gives the renderer and the applier one source of truth, so a new write touches one place. Authority over a bounded scope removes drift. Retaining proven SDK, artifact-reading, CLI, and test code avoided a wholesale rewrite and a permanent dual architecture.

## Consequences

Preparation and application are not a transaction, and concurrent deployment against one target is unsupported. Recovery relies on a provisional content-hash guard. Shared-resource ownership is defined in [Custom API association ownership](custom-api-association-ownership.md), [Data provider ownership](data-provider-ownership.md), and [Environment-owned plugin identities](environment-owned-plugin-identities.md). `RetrieveDependenciesForDelete` is a candidate for broader deletion checks but is not an approved guard.