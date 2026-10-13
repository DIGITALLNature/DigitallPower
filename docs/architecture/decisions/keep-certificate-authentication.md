# Keep Certificate Authentication

## Context

Client-certificate authentication was already implemented alongside client-secret, user, and Azure DevOps federated authentication. Maintainers use it less and do not plan to validate it against their own Dataverse environments.

## Decision

Keep certificate authentication (CurrentUser store thumbprints and PFX files) in the typed connection model rather than narrowing the available options.

## Consequences

- Automated tests cover settings, creation, and secret persistence, not end-to-end token acquisition or cross-platform certificate-store behavior. Do not claim broader validation.
- Address defects from reproducible user reports, starting with targeted tests.
- README setup requirements and limitations must stay accurate.
