# Environment-owned plugin identities

Managed identity provisioning and binding are outside `plugin push`, in both the currently
wired pipeline and the replacement core. The registration library no longer supplies identity
selection; push has no attribute parsing, special rejection, or backward-compatibility path.

## Rationale

A concrete client/tenant ID identifies an environment's Entra app registration or user-assigned
managed identity, not the plugin's code. Embedding it in a reusable artifact couples promoted
code to a security principal and prevents independent stage permissions without rebuilding.

Signing trust and identity selection are separate. Dataverse's federation subject includes
the environment ID and certificate identity, so the same production-suitable signed artifact
can use a different Entra identity in each stage. Federated credentials establish trust;
the Dataverse managed identity record and assembly/package binding select the identity.
See [Microsoft's setup guide](https://learn.microsoft.com/en-us/power-platform/admin/set-up-managed-identity).

## Push invariants

- Do not create, bind, unbind, transfer, or delete managed identities.
- Leave existing target assembly/package bindings untouched during in-place updates.
- New standalone replacements receive no copied binding from the superseded assembly.
- Retry preserves an independently provisioned binding already on the target record.
- Deleting superseded assemblies does not authorize deletion of shared identity records.

Keep remote binding IDs as observed state, not desired configuration. Artifact-state checks
can still include them without making push responsible for their lifecycle.

Each environment's provisioning workflow owns Dataverse records/bindings, Entra federated
credentials, and resource permissions. Managed-solution transport and rebinding behavior need
independent platform verification; push does not promise those semantics.

## Separate provisioning capability

A future dgtp command can ensure the Dataverse identity record and bind a deployed
assembly/package after solution import in each environment. Keep this outside the plugin
rewrite and separate from Entra provisioning. Resolve targets unambiguously, validate existing
record compatibility, make rebinding explicit, and support idempotent reruns. Verify binding
updates on managed-solution components before defining the command API.
