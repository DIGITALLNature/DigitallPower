# Where things stand

Use this when the user asks where things stand or what to do next. Look before you answer:

- `pnpm exec openspec list --json` lists the changes in progress.
- `pnpm exec openspec status --change "<name>" --json` shows which parts of a change's plan exist and what comes next. `pnpm exec openspec status --all --json` does this for every change.
- `pnpm exec openspec show "<name>"` prints a change or a spec.
- If the user names a store, add `--store <id>` to each of these. `pnpm exec openspec store list --json` lists the stores.

Say where things stand in a sentence or two, name the next step and the skill that does it, and offer to do it. Do not start it until they say so.
