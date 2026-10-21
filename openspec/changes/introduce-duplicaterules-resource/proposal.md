# Proposal

## Why

Duplicate-rule DTOs and publish/unpublish test executors exist, but the current CLI has no productive duplicate-rule command. A dedicated resource should manage existing rules without mixing their lifecycle with process behavior.

## What Changes

- Introduce a `dgt.power.duplicaterules` module with resource-oriented `init` and `apply` operations for existing rules.
- Capture and apply publish/unpublish state and ownership; specify independent optional properties without implicit changes to omitted rules.
- Provide preflight validation, dry-run, and explicit partial-failure reporting.
- Define rule identity and portable ownership resolution before implementation; do not assume process internals are reusable.
- Out of scope: creating rules, transporting definitions or conditions, and deleting rules.

## Capabilities

### New Capabilities

- `duplicate-rule-management`: Capture and application of existing duplicate-rule state and ownership.

### Modified Capabilities

None.

## Impact

Touches host registration and DI, duplicate-rule DTO ownership, new schemas and module tests, and existing publish/unpublish test helpers. README documents the new commands. Can proceed independently of process implementation; no mandatory dependency on a new shared framework.
