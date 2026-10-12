# Decision: Remove ensuresdksteps Command

## Context

The maintenance module had a command `ensuresdksteps` that enabled or disabled all SDK steps within a specified solution. This was a solution-scoped bulk operation.

With the addition of the `plugin step` commands (`enable`, `disable`, `state set`, `config set`), there was functional overlap. The `plugin step enable/disable` commands were extended to support a `--solution` parameter that provides the same functionality.

## Decision

**Remove the `maintenance ensuresdksteps` command** as it is superseded by `plugin step enable/disable --solution`.

This is a **BREAKING CHANGE** that simplifies the CLI by:
1. Consolidating all step state management under the `plugin step` resource-oriented commands
2. Removing redundancy
3. Following the resource-oriented CLI design (`resource action [identifier]`)

## Migration

**Before**:
```bash
# Enable all steps in a solution
dgtp maintenance ensuresdksteps -s mysolution

# Disable all steps in a solution
dgtp maintenance ensuresdksteps -s mysolution -d
```

**After**:
```bash
# Enable all steps in a solution
dgtp plugin step enable --solution mysolution

# Disable all steps in a solution
dgtp plugin step disable --solution mysolution
```

## Consequences

### Positive
- **Simpler CLI**: One less command to maintain and document
- **Consistent**: All step operations are under `plugin step`
- **Resource-oriented**: Follows the `resource action [identifier]` pattern
- **More flexible**: The new commands also support single-step operations via step-id or composite key

### Negative
- **Breaking change**: Existing scripts using `ensuresdksteps` will fail
- **Migration required**: Users need to update scripts to use the new commands

## Related

- `decision-resource-oriented-cli-redesign.md` - Overall CLI design philosophy
- `implementation-plugin-step-enable-disable-state.md` - Replacement implementation
