# Proposal

## Why

Verb-oriented import/export modules obscure resource ownership and contain functions whose continued value must be reviewed. Retiring them requires explicit decisions rather than blanket deletion.

## What Changes

- Remove bulk deletes, queues, calendars, SLA configs, routing-rule configs, user roles, and Outlook synchronization-filter templates in both transfer directions, as approved by the user.
- Provide no replacement commands, new resource modules, or migration support for their legacy JSON configurations.
- Preserve the independently migrated teamtemplate and documenttemplate resource commands.
- **BREAKING**: Remove the legacy `import`/`export` branches and modules after checking that the retained template resources no longer depend on them.
- Reconcile the existing secure-config retirement requirement so it no longer promises continued availability of other legacy import paths.
- Out of scope: modifying existing Dataverse records or jobs, removing migrated resource commands, and implementing new replacements.

## Capabilities

### New Capabilities

None.

### Modified Capabilities

- `cli-command-surface`: Retirement of verb-first import/export branches while preserving the secure-config replacement.

## Impact

Touches legacy module projects and base classes, registration and DI, DTO/schema ownership, tests, dependencies, README, and migration guidance. No additional retained-resource migration is required. Verify the two completed template migrations and coordinate bulk-delete shared-artifact removal with maintenance retirement.
