# Tasks

## 1. Connection display boundary

- [x] 1.1 Add a deterministic connection regression test under an explicitly configured Spectre live display; verify the original implementation fails with display exclusivity instead of the intended missing-credential error.
- [x] 1.2 Remove the live status wrapper from `DataverseConnection.ConnectUsingAsync` while preserving safe outcome lines, exception/cancellation propagation, caching, and unsuccessful-client disposal; verify the regression and existing connection feedback/lifecycle tests pass.
- [x] 1.3 Document initial authentication and display ownership on `IDataverseConnection` and in the repository's command conventions; verify the guidance matches the retained behavior and leave the README unchanged.

## 2. Command orchestration

- [x] 2.1 Integrate the existing codegeneration constructor/call-site changes and keep acquisition after local configuration validation but before generation status; add tests for both target types with a display-exercising connection double, and verify they detect the original ordering defect.
- [x] 2.2 Add connection-failure coverage proving metadata and generators do not run; retain invalid-config, pre-cancellation, and token-forwarding coverage, and verify these command tests pass.
- [x] 2.3 Inspect remaining production status/progress/live entry points and their first connection acquisition and confirmation paths; fix only demonstrated violations with corresponding tests, and verify already-correct commands remain unchanged.

## 3. Integration and specification checks

- [x] 3.1 Build affected projects and run the connection and codegeneration test DLLs through `dotnet exec` with explicit selectors as appropriate; verify actual passing test counts, zero failures, and no build errors, running additional module tests only for changes identified in task 2.3.
- [x] 3.2 Run existing applicable formatting/analyzer checks for changed C# files; verify no new findings are introduced without modifying `baseline.sarif.json`.
- [x] 3.3 Sync both capability deltas into main specs after implementation and run `pnpm exec openspec validate --all`; verify current specs and implementation agree and all artifacts validate.

## Workflow follow-up

- Archive the change after implementation and required review.
- Run `pnpm run openspec:update` and review generated instruction changes.
