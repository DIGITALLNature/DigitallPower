# Plugin redesign directions

The developer-facing proposal is maintained in
[`docs/design/plugin-module.md`](../docs/design/plugin-module.md).
It consolidates the architectural assessment, alternatives, behavioral tradeoffs,
recommended reconciliation design, and unresolved product/platform decisions.

## Selected directions

- Replace the deployment core while retaining or adapting proven SDK, artifact-reading,
  CLI integration, and test code. Avoid a wholesale rewrite or permanent dual architectures.
- Registration reconciliation is authoritative within a defined ownership scope.
  The ownership mechanism and policies for shared resources still require design.
- The registration attribute contract should include stable step keys. Key persistence,
  scope, and adoption of existing unkeyed steps remain unresolved.
- Implement conservative recovery for unambiguous partial upgrades.
- Deliver incremental slices with one final CLI cutover; coordinate registration-library changes externally.
- Keep step configuration behavior; combined transactions are not rewrite scope.
- Provisionally deploy providers against existing data-source tables. Verify independent
  provisioning after the provider slice and before cutover; agree a fallback if infeasible.

## Boundaries

The document is a proposal, not an implementation-ready specification. Snapshot/change-set/
apply is the recommended architecture, not an approved implementation.
Provider independent-provisioning feasibility, recovery recognition, environment-state
ownership, and other open details must not be treated as settled requirements.

The [implementation plan](../docs/design/plugin-module-implementation.md) defines work-package
dependencies and acceptance gates without assuming a key-storage mechanism or live environment.

The existing CLI capability inventory is the starting baseline. Pre-release internal
structures and incidental restrictions do not constrain the redesign.
Public command documentation should describe implemented behavior, not this proposal.
