# Decision: Keep Certificate Authentication in the Connection Rewrite

## Decision

Keep client-certificate authentication as a supported option in the typed connection model,
including CurrentUser certificate-store thumbprints and PFX file references.

## Rationale

The connection rewrite aims to offer multiple authentication options. Certificate authentication
is already implemented alongside client-secret, user, and Azure DevOps federated authentication.
Although it is less commonly used by the maintainers and they do not plan to validate it against
their own Dataverse environment, removing it would unnecessarily narrow the available options.
If users report defects, address them based on reproducible reports.

## Support boundary

The implementation loads a certificate for `ClientCertificateCredential` and stores PFX passwords
separately from connection metadata. Existing automated coverage verifies settings and creation /
secret persistence, but does not constitute end-to-end certificate token-acquisition validation.
Do not infer live authentication or cross-platform certificate-store behavior from those tests.
Keep README setup requirements and limitations accurate; investigate reported issues with targeted
tests before claiming broader validation.
