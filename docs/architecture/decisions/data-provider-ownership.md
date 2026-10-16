# Data Provider Ownership

## Context

Custom data providers combine a provider record, handler plugin types, and a data-source table that is provisioned outside plugin push.

## Decision

A declaration owns the complete handler configuration of the provider identified by its data-source logical name: declared operations are set, explicit reassignment from external handlers is allowed, and undeclared slots are cleared. When a provider is omitted, only handlers referring to types owned by the target are cleared. Provider records and data-source tables are never deleted. Solution membership adds missing provider records only, never table schema or rows.

## Rationale

Handler ownership does not imply authority over shared records. The table is an existing prerequisite, matching how Custom API records stay external.

## Consequences

Competing declarations of one provider are rejected, so batch preflight must detect them. Provider reconciliation assumes an existing compatible table. Existing-table provisioning, handler clearing semantics, and solution transport still need verification against a live Dataverse environment.