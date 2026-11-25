# Tasks

## 1. Deliver the independently usable native template

External repository creation, code changes, and publication require separate authorization; this planning change does not perform them. Native delivery must not depend on dgtp command implementation, engine-hosting experiments, or dgtp release timing.

- [ ] 1.1 Finalize the external repository, NuGet package ID, template identity, short name, and release ownership; verify they are recorded in the template contract and that documentation identifies the real package rather than provisional names.
- [ ] 1.2 Coordinate the standard template's name/namespace/output customization, primary project output, minimal DIGITALL plugin and registration example, and README; verify native generation tests cover default/explicit namespaces and renamed files/references, and that README instructions cover native installation, updates, build prerequisites, and optional `dotnet sln ... add ...`.
- [ ] 1.3 Verify the generated project's supported target framework, signing requirements, and Dataverse package build configuration using Microsoft's build tooling where suitable; explicitly build a natively generated consumer and inspect its `.nupkg` for required runtime DLLs, excluded platform SDK assemblies, and registration metadata recognized by dgtp's local artifact reader. Record verified prerequisites and artifact paths in the template README.
- [ ] 1.4 Coordinate template CI that packs the source template, generates and release-builds a consumer, and checks deployment-package contents; verify a stable template release is published and can be installed and used through native tooling without dgtp or PAC installed.

## 2. Validate native usage

- [ ] 2.1 Walk through native install, generation, customization, build, template update, and optional existing-solution membership in a disposable profile; verify the commands and package contents, document the published native workflow in `docs/guides/plugin-deployment.md`, and document the repeatable acceptance procedure in `docs/development/build-and-test.md` without advertising an unimplemented dgtp command.

## 3. Verify the hosting approach

The team selected dgtp as the shared initialization entry point. Template publication remains independently deliverable; the checks below gate releasing the dgtp integration.

- [ ] 3.1 Select a .NET 10-compatible engine-package release and verify its dependency graph against the plugin module's NuGet/logging dependencies; confirm a minimal host builds without SDK-internal assembly references.
- [ ] 3.2 Prove programmatic discovery and generation using the same template contract, including project filename, namespace, and internal-reference renaming; verify local-fixture tests pass without a `dotnet new`/PAC subprocess or template post-action.
- [ ] 3.3 In a disposable user profile, prove native-to-API and API-to-native installation visibility, native-update visibility on the next host run, and preservation of unrelated templates; document this additional wrapper acceptance procedure in `docs/development/build-and-test.md`. Revisit the wrapper design if interoperability fails, without blocking a valid native template release.

## 4. Implement acquisition and native update guidance

- [ ] 4.1 Add the verified engine dependencies and a narrow asynchronous hosting adapter inside the plugin module, preserving I/O-free construction; synchronize the application lock file and verify locked restore, module build, and local-fixture adapter tests pass.
- [ ] 4.2 Implement stable installed-template discovery and announced first-use installation without modifying unrelated packages; add tests for absent, installed, incompatible, ambiguous, prerelease-only, and changed-during-run selections, and document shared installation effects in `docs/reference/commands/plugin.md`.
- [ ] 4.3 Implement bounded stable-update checks and actionable `dotnet new install <actual-package-id>` hints without updating the installed template; verify version-ordering, newer-stable, prerelease-only, and lookup-failure cases with deterministic tests, and document native update ownership and offline warning behavior.

## 5. Implement safe plugin initialization and CLI wiring

- [ ] 5.1 Add initialization settings and the `PowerCommand<TSettings>` command with default namespace and local validation before acquisition; verify settings and command tests reject invalid names/namespaces/occupied destinations without network access or Dataverse authentication, and document invocation/prerequisites in `docs/reference/commands/plugin.md`.
- [ ] 5.2 Implement contained staged generation, expected-primary-output checks, destination revalidation, and cleanup of owned artifacts only; verify absent/empty destinations, hidden content, linked paths, unsafe outputs, destination races, generation failure/cancellation, suppressed post-actions, and retained installation after generation failure in focused module tests.
- [ ] 5.3 Add escaped actionable output for the selected version, initial installation, verified build command, generated README, and native solution-add command using the actual project path; verify messages with paths containing spaces/markup, failure reporting, no existing-solution edits, and no generated-project restore/build/Git execution. Update `docs/guides/plugin-deployment.md` and the command reference with the dgtp workflow while retaining native instructions.
- [ ] 5.4 Register `plugin init` in `CommandTree.Register`; update command-tree/example/help tests and `SettingsParsingTests` for required name, default/explicit namespace, and inherited options. Verify existing push/register/step paths remain available and no update flag, template-management branch, or solution-integration option is introduced.

## 6. Complete wrapper integration acceptance

- [ ] 6.1 Run first-use, installed-template, update-hint/native-update, and subsequent-generation acceptance checks against the published template in a disposable profile; explicitly release-build the generated consumer, inspect the deployable package, and verify any nearby existing solution is unchanged. Confirm README/build/update/solution-add instructions match observed results.
- [ ] 6.2 Verify application locked restore and `dotnet build` succeed, then run the built plugin and CLI TUnit test DLLs with `dotnet exec`; synchronize implemented main specs and run `pnpm exec openspec validate --all`. Keep the plugin implementation separate from the webresource implementation PR.

## Workflow follow-up

- Native template publication alone does not mean the dgtp integration is implemented and does not satisfy this repository's archive gate.
- Keep this change active while its implementation PR is open and after merge until a stable release containing it is published from `main`.
- Archive only in a separate post-release follow-up after verifying the implementation PR was merged into `main`, the release is published/non-draft/non-prerelease, and the required merge-commit/release-tag/main ancestry holds.
