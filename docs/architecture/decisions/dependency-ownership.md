# Dependency Ownership and Cleanup Boundaries

## Context

Package references are repeated across modules, test helpers, and build tools, and cleanup attempts risk breaking runtime resolution.

## Decision

Treat dependency cleanup as a dedicated solution-wide change, not incidental feature work. Review direct ownership, unused references, security overrides, shared version management, build-tool alignment, production/test runtime parity, and JavaScript package boundaries together. Central Package Management is not adopted; evaluate it within that review.

## Rationale

- Repeated references do not duplicate packages; each project has its own restore graph and the executable resolves the combined graph. Keep explicit references where a project uses a package's API (for example `IOrganizationServiceAsync2` in webresource repositories). Namespace searches alone miss indirect needs such as logging abstractions required by `ServiceClient` constructors.
- Security overrides in `dgt.power.common` flow through project references. `PrivateAssets=all` build tools (SourceLink, static analysis) do not, so their versions must be managed in the project or shared `Directory.Build.props`.
- A newer package in the shared test-helper project can make module tests resolve a different implementation (for example `System.Runtime.Caching`) than the CLI.
- CI restores with `--locked-mode`, so production dependency changes must update `packages.lock.json`.
- Confirm removal candidates with restore, build, and affected tests. Version centralization and removal are separate changes.
- The root TypeScript compiler and the Jest fixture compiler must not be blindly unified (the fixture's ts-jest peer range excludes TypeScript 7), and the root `@odata/parser` supports generated fixture imports.
