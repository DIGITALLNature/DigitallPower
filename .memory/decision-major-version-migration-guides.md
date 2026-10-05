# Decision: Major-Version Migration Guides

## Decision

Document every user-affecting breaking change introduced by a major release in its corresponding
guide under `docs/migrations/`, using the format `<from>-to-<to>.md` (for example,
`2.x-to-3.x.md`). Keep `README.md` focused on the current product and prominently link to the
migration guides directory rather than retaining command or option documentation for removed
interfaces. The README link should remain generic across future major releases.

## Rationale

The README is the current user-facing reference, while migration guides need to describe both old
and new behavior, including removed commands and compatibility limits. Keeping these concerns in
separate documents avoids presenting obsolete interfaces as supported while preserving actionable
upgrade instructions for existing users.

## Scope

Each guide is the migration reference for user-affecting breaking changes in that major-version
transition, and links to current command documentation for supported behavior. Internal-only
breaking changes do not need migration guidance. The guide is not a substitute for release notes or
a complete inventory of non-breaking changes.
