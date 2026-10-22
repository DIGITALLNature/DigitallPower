# Proposal

## Why

Processes and duplicate rules need environment-portable owner references without duplicating principal lookup behavior. Existing workflow code resolves a domain-name string using the first match; it does not provide a shared contract for regular users, application users, and owning teams.

## What Changes

- Define logical owner keys and explicit environment-specific mappings, separate from resource identity and desired lifecycle state.
- Provide read-only, unambiguous resolution of regular users, application users, and teams capable of owning records.
- Support regular users by Entra object ID or domain name, application users by application/client ID, and teams by Entra group object ID or name pattern plus business unit.
- Support explicit business-unit and membership-type filters for Entra group teams and only `*` globbing for team names.
- Return resolved principal identity, kind, and eligibility metadata so each resource can enforce its own assignment constraints before writing.
- Fail explicitly on missing mappings, unresolved or ambiguous targets, and unsupported principal kinds; do not fall back to the connection identity.
- Introduce a strict V1 mapping file and a shared `--owner-mapping` integration convention. Resolve requested keys for apply; resolve the full source mapping for unambiguous owner capture.
- Place the resolver, loader, and related types together in `dgt.power.common/Ownership`, with no new assembly or package dependency.
- Integrate the shared contract in the process and duplicate-rule changes, not by migrating existing commands in this change.
- Out of scope: assignment execution, publish/deactivate sequencing, impersonation, principal provisioning, permission management, a generic deployment engine, and automatic legacy-owner migration.

## Capabilities

### New Capabilities

- `owner-resolution`: Portable owner references and deterministic resolution of existing target principals.

### Modified Capabilities

None.

## Impact

Introduces a shared contract and implementation in `src/dgt.power.common/Ownership`, tests in `tests/dgt.power.common.tests/Ownership`, a schema in `schemas/ownership/v1/schema.json`, and README guidance. `introduce-process-resource` and the ownership portion of `introduce-duplicaterules-resource` are intended consumers. Their command options and resource schemas are integrated in their own changes. Documenttemplates are not an approved ownership consumer. No current CLI behavior or historical owner format changes.
