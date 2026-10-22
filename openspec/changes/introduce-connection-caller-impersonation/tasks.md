# Tasks

The design and product contract are approved. Implementation requires a separate apply request. Follow [design.md](design.md) for the approach and [the connection-management delta](specs/connection-management/spec.md) for the behavior contract. Do not add a Clone strategy, an identity fallback, a generic request pipeline, or process-command implementation.

## 1. Connector foundation

- [ ] 1.1 Retain the invocation's selected environment and authentication configuration for additional client creation through the existing connector paths. Verify with focused tests that later saved-state changes cannot switch the selected target and that existing connection-selection precedence remains unchanged.
- [ ] 1.2 Create caller clients through independent SDK connections in both the credential/token-callback and connection-string paths, reusing available credential infrastructure where possible and setting the caller before publication. Verify actual mutable caller-state independence between the standard client and two callers, including connection strings that otherwise permit SDK connection reuse; distinct mock references alone are not proof. Do not introduce Clone or a fallback creation strategy.

## 2. Connection contract and production behavior

- [ ] 2.1 Add `GetServiceAsync(Guid callerId)` with XML documentation for the systemuser ID, borrowed-service ownership, and preflight boundary; wire the production implementation and update every repository implementation and mock. Reject `Guid.Empty` before authentication; unsupported doubles throw explicitly. Verify compilation and focused tests for empty-ID validation without authentication and unsupported implementations.
- [ ] 2.2 Implement the lazy invocation cache and protect initialization/publication with the existing gate, without gating business requests. Verify same-caller reuse, distinct caller services, concurrent acquisition without duplicate publication, and unchanged parameterless-service behavior and caller state.
- [ ] 2.3 Implement failure cleanup, successful-client ownership, and disposal without caching failed attempts. Verify creation/configuration failure, subsequent successful retry, unchanged authentication/cancellation propagation, and disposal of the standard client, every caller client, and unfinished clients. Test the DI/resolver lifetime path that disposes the connection rather than only manually invoking its disposal.
- [ ] 2.4 Preserve standard connection verification and sanitized feedback without adding caller-specific `WhoAmI`, privilege probes, or per-caller "Connected" successes. Verify request counts, feedback, and acquisition-error propagation through existing error handling without sensitive connection data.
- [ ] 2.5 Update README with a short overload usage example, connection-owned disposal, forbidden consumer caller-state mutation, and the limited preflight guarantee. Add a concise `docs/migrations/2.x-to-3.x.md` note for custom interface implementations. Verify that examples match the implemented signature and do not advertise new CLI options or guaranteed operation privileges.

## 3. Caller-aware test infrastructure

- [ ] 3.1 Extend `TestConnection` with minimal explicit caller-to-service configuration; unconfigured caller acquisition throws `NotSupportedException`. Verify configured service selection, rejection of unknown callers, and absence of silent standard-service substitution without introducing a generic recorder or security-role simulation.
- [ ] 3.2 Add routing-contract tests that execute and assert actual requests against the standard service and at least two configured caller services, including interleaving and a propagated Dataverse fault without identity fallback. Verify that existing parameterless test consumers still behave unchanged; coordinate with production connection tests without treating fake routing as proof of SDK isolation.

## 4. Integration verification

- [ ] 4.1 Run focused tests for the affected common, connection, and shared test infrastructure projects and any additional projects with changed doubles, then run `dotnet build` for the solution. Run existing CLI smoke tests as regression coverage; no new settings-parsing cases are required because this change adds no CLI arguments or options. Record successful results before marking complete.
- [ ] 4.2 Run `pnpm exec openspec validate --all --strict` and `git diff --check`; review the complete change, including new files, for consistency with the design and delta spec. Verify that there is no Clone/fallback strategy, new package dependency, unintended CLI/schema change, or process-command implementation.

## Workflow follow-up

- After implementation and verification, synchronize the delta into the main connection-management spec and archive the completed change using the repository's OpenSpec workflow.
- Keep `introduce-process-resource` dependent on the implemented caller-service contract; its consumer integration and legacy workflow removal remain outside this change.
