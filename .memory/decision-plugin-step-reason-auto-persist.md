# Decision: Auto-Persist Reason for Plugin Step State Changes

## Context

The `dgtp plugin step enable` and `dgtp plugin step disable` commands support an optional `--reason` parameter to document why a step's state was changed. Initially, persisting this reason to the step's description field in Dataverse required an explicit `--persist-reason` flag.

This created a cumbersome API:
```bash
# To save the reason
dgtp plugin step disable --step-id <id> --reason "Maintenance" --persist-reason

# To not save the reason (just display it)
dgtp plugin step disable --step-id <id> --reason "Maintenance"
```

## Decision

**Remove the `--persist-reason` flag and auto-persist the reason when provided.**

If `--reason` is specified, it is automatically stored in the step's description field in Dataverse.

## Alternatives Considered

### Alternative 1: Keep --persist-reason Flag
Keep the explicit opt-in flag for persisting reasons.
- **Rejected**: Adds unnecessary friction; if users provide a reason, they almost always want it persisted.

### Alternative 2: Always Persist Reason (No Flag)
Always persist the reason, even if empty.
- **Rejected**: Would overwrite existing descriptions with empty strings when no reason is provided.

### Alternative 3: Never Persist Automatically
Require a separate command to update the description.
- **Rejected**: Creates extra steps for a common use case; violates principle of least surprise.

## Consequences

### Positive
- **Simpler API**: One less flag to remember and specify
- **Intuitive**: If you provide a reason, it gets saved - matching user expectations
- **Consistent**: Aligns with how `plugin step state set` works (reason in config is persisted)
- **Backward compatible**: Existing scripts without `--persist-reason` continue to work; those with `--persist-reason` will fail validation (removed parameter)

### Negative
- **Breaking change**: Scripts using `--persist-reason` will need to be updated
- **No dry-run for reason**: Cannot preview what would be persisted without actually persisting (but this is acceptable as reasons are typically intentional)

## Migration

**Before**:
```bash
dgtp plugin step disable --step-id <id> --reason "Maintenance" --persist-reason
```

**After**:
```bash
dgtp plugin step disable --step-id <id> --reason "Maintenance"
```

If `--persist-reason` was used without `--reason`, simply remove the flag. If both were used, keep only `--reason`.

## Related

- `implementation-plugin-step-enable-disable-state.md` - Implementation details
- `decision-plugin-step-three-modes.md` - Identification modes design
