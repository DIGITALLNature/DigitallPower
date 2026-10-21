# Proposal

## Why

Verb-oriented import/export modules obscure resource ownership and contain functions whose continued value must be reviewed. Retiring them requires explicit decisions rather than blanket deletion.

## What Changes

- Review bulk deletes, queues, calendars, SLA configs, routing-rule configs, user roles, and Outlook templates individually with the user.
- Record retain/migrate, replace, or remove decisions for both import and export.
- Route retained functionality into separate resource changes; teamtemplates and documenttemplates already have their own changes.
- **BREAKING**: Remove approved obsolete paths and finally the legacy `import`/`export` branches and modules after all retained resources migrate.
- Reconcile the existing secure-config retirement requirement so it no longer promises continued availability of other legacy import paths.
- Out of scope: unapproved resource removals and implementing all retained resource migrations inside this change.

## Capabilities

### New Capabilities

None.

### Modified Capabilities

- `cli-command-surface`: Retirement of verb-first import/export branches while preserving the secure-config replacement.

## Impact

Touches legacy module projects and base classes, registration and DI, DTO/schema ownership, tests, dependencies, README, and migration guidance. Final retirement depends on both template changes and all additionally approved resource migrations. Bulk-delete disposition must be coordinated with the maintenance retirement change.
