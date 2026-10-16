# Environment-Owned Plugin Identities

## Context

A client or tenant ID identifies an environment's Entra app registration or managed identity, not the plugin's code.

## Decision

Managed identity provisioning and binding are outside `plugin push`. Push never creates, binds, unbinds, transfers, or deletes identities, leaves existing bindings on target records untouched, and never copies a binding to a replacement assembly.

## Rationale

Embedding identity values in a reusable artifact couples promoted code to a security principal and blocks independent stage permissions without a rebuild. Signing trust and identity selection are separate: federated credentials establish trust, while the Dataverse identity record and its binding select the identity.

## Consequences

Each environment's provisioning workflow owns identity records, bindings, federated credentials, and permissions. A future command could ensure the identity record and bind it after solution import; that needs verified behavior for managed-solution components first.