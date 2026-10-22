# Proposal

## Why

Codegeneration in v3 can fail before doing work because its status display encloses lazy connection acquisition, which starts another exclusive Spectre display. Connection infrastructure and command orchestration currently compete for display ownership; the fix should establish a shared boundary rather than only reorder one caller.

## What Changes

- Remove the connection-owned live status display while preserving durable, sanitized connection success/failure feedback.
- Require commands to complete initial connection acquisition and any required authentication before starting Dataverse work displays.
- Keep lazy, cached connection acquisition, I/O-free constructors, non-interactive authentication behavior, and the thin `PowerCommand` base.
- Complete the existing codegeneration fix and add regression tests that exercise actual Spectre display exclusivity.
- Check other live-display entry points and change only demonstrated violations.
- Update directly related documentation and current capability specs.

Out of scope: replacing every service's connection dependency, introducing a display manager or another command base, changing authentication policy, adding dependencies, or changing configuration/CLI syntax.

## Capabilities

### New Capabilities

None.

### Modified Capabilities

- `command-execution`: Define command-owned interactive phases, including connection acquisition before Dataverse work displays and confirmation outside those displays.
- `connection-management`: Preserve durable connection feedback without a connection-owned live display.

## Impact

Primary implementation targets are `DataverseConnection.ConnectUsingAsync`, `IDataverseConnection` documentation, and `CodeGenerationCommand`, with focused connection/codegeneration tests. Other commands change only if inspection identifies the same ordering defect. Interface and contributor guidance must reflect the boundary; README changes are not required. No new dependencies, schema changes, command-tree changes, or user-facing API removals are intended.
