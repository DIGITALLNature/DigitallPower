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
- First-version identity uses scoped composite matching; persisted/generated keys and
  identity-related registration-library changes are deferred.
- Own all non-internal steps on selected plugin types. Preserve configuration, enabled
  state, and impersonation, and reconcile only declared fields.
- Reject duplicate local tuples and ambiguous remote matches. Tuple changes create
  new registrations; name/order/attributes/images do not affect matching.
- Implement conservative recovery for unambiguous partial upgrades.
- Initially allow two standalone records only with one desired major/minor train and
  matching target content. The hash guard is provisional, not proof of a failed push.
  Unsupported cross-resource dependencies block cleanup.
- Deliver incremental slices with one final CLI cutover.
- Continue independent directory targets after failure, return overall failure, and stop on cancellation.
- Verify the solution-aware component matrix, add supported components, and fail membership errors.
- Keep step configuration behavior; combined transactions are not rewrite scope.
- Reconcile Custom API associations authoritatively: unlink omitted target-owned handlers,
  allow explicit reassignment from external handlers, and never delete shared API records.
  See [Custom API ownership](decision-plugin-custom-api-ownership.md).
- Exclude managed identity provisioning and binding from push. Preserve existing target
  bindings, never transfer them to a new replacement, and do not parse or detect the removed
  attribute. See [environment identity boundary](decision-plugin-managed-identity-boundary.md).
- Provisionally deploy providers against existing data-source tables. Verify independent
  provisioning after the provider slice and before cutover; agree a fallback if infeasible.

## Component deletion: candidate dependency check

Consider `RetrieveDependenciesForDelete` when revisiting component deletion, particularly
package asset/type removal. The standalone replacement core already uses this request for
type cleanup; assess whether that mechanism can cover the remaining component kinds.
This is an investigation candidate, not an approved implementation change. Verify supported
component types and whether its results adequately predict dependencies affected by
server-managed package updates. It does not by itself establish package-upload deletion
semantics or protect against dependencies changing between inspection and deletion.

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
