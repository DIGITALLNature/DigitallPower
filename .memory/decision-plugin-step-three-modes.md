# Decision: Plugin Step Enable/Disable Three Identification Modes

## Context

The `dgtp plugin step enable` and `dgtp plugin step disable` commands needed to support multiple ways to identify plugin steps for state changes. Users had different use cases:
1. Single step by its ID (when known)
2. Single step by its metadata (plugin type, message, stage, entity)
3. All steps in a solution (bulk operation)

Additionally, the maintenance module had a separate `ensuresdksteps` command that only supported solution-scoped bulk operations.

## Decision

Implement **three mutually exclusive identification modes** in `plugin step enable/disable`:

1. **`--step-id <guid>`** - Direct lookup by SDK message processing step ID
2. **Composite key** - `--plugin-type <type> --message <msg> --stage <stage> --entity <entity>` (+ optional `--secondary-entity`, `--execution-order`)
3. **`--solution <name>`** - Apply to ALL steps in the specified solution

## Alternatives Considered

### Alternative 1: Keep Separate Commands
Keep `ensuresdksteps` as a separate command and add only single-step operations to `plugin step`.
- **Rejected**: Violates resource-oriented CLI design; creates two ways to do the same thing (bulk step state changes).

### Alternative 2: Only Config File for Bulk
Make `plugin step state set` the only bulk operation command and require users to create config files for solution-scoped operations.
- **Rejected**: Too cumbersome for the common use case of enabling/disabling all steps in a solution.

### Alternative 3: Add --solution to state set
Add `--solution` parameter to `plugin step state set` as well.
- **Rejected**: `state set` is designed for config-file driven operations; adding solution-scoped mode would mix two different paradigms. The config file approach is more flexible (can mix enabled/disabled, different reasons, etc.).

## Consequences

### Positive
- **Consistent API**: Single command (`plugin step enable/disable`) for all step state operations
- **Flexible**: Users can choose the identification mode that fits their use case
- **Clean migration**: `ensuresdksteps` can be removed as it's superseded by `--solution` mode
- **Resource-oriented**: Follows the CLI design pattern of `resource action [identification]`

### Negative
- **Slightly more complex**: Three modes instead of two (step-id + composite key)
- **Validation complexity**: Need to ensure mutual exclusivity between all three modes

## Rationale

The three-mode design provides the best balance of flexibility and simplicity:
- Direct ID lookup is fastest for known steps
- Composite key allows identification without querying the database first
- Solution-scoped mode replaces the `ensuresdksteps` command cleanly

The mutual exclusivity is enforced at the settings validation level, providing clear error messages when users try to mix modes.

## Related

- `decision-resource-oriented-cli-redesign.md` - Overall CLI design philosophy
- `implementation-plugin-step-enable-disable-state.md` - Implementation details
