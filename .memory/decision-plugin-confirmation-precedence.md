# Plugin Push Confirmation Precedence

`plugin push --confirm` is an opt-in, per-target interactive approval prompt shown after a
deployment plan is rendered and before writes begin.

Execution-mode precedence is:

1. `--dry-run` renders the plan and never prompts or writes.
2. `--non-interactive` and CI execution suppress confirmation and execute without prompting.
3. `--confirm` prompts for approval in an interactive execution context.
4. The default executes immediately after rendering the plan.

This keeps `--confirm` useful for operator sessions without creating CI hangs or failures. A
declined confirmation reports cancellation and leaves that target unchanged.
