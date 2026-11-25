# Tasks

## 1. Establish the template marker convention

- [ ] 1.1 Obtain a separately authorized and reviewed `DIGITALLNature/webresources-template` update on `main` with the four reserved markers and insertion conventions described in `design.md`, current dgtp deployment syntax/tool pin, execution-time connection parameters, missing-deployment-value validation, and safe CI/onboarding defaults; verify the source diff, README setup guidance, and commit SHA. Do not edit that repository under this change's authorization. Treat incomplete upstream preparation as an integration blocker, not permission to ship against the current unprepared template.
- [x] 1.2 Capture small archive fixtures for supported markers, missing required markers, unknown markers, multiple occurrences, and changed bundler filenames; verify expected customization is independent of file locations while dotfiles, unrelated text, binary content, license, and lockfile remain preserved. Keep production template files out of the dgtp package and ordinary build inputs.

## 2. Acquire explicitly selected template revisions

- [x] 2.1 Implement cancellation-aware runtime acquisition from the fixed public template repository using built-in HTTP facilities; test default `main` resolution without tags/releases, explicit SHA, lightweight/annotated tags, tag-name encoding, rejection of user-selected branches, and reporting/downloading of the exact resolved SHA with fake HTTP responses. Verify there is no per-release pin, latest-tag lookup, or version-specific adapter.
- [x] 2.2 Enforce finite timeouts and compressed-download limits, and classify missing refs, rate limits, interrupted downloads, and HTTP failures without falling back; verify focused tests produce actionable errors, clean temporary downloads, and never send Dataverse credentials.
- [x] 2.3 Document the moving `main` default, optional tag/SHA selection, reported-commit reproducibility, network requirements, and failure recovery in the new `docs/guides/webresource-scaffolding.md`; verify examples match the implemented selectors and do not imply tags, GitHub Releases, Git, PAC, or Dataverse authentication are required.

## 3. Generate and publish the customized project safely

- [x] 3.1 Implement staged archive extraction with single-root removal, dotfile preservation, platform-aware path/collision checks, link rejection, and expanded-size/entry limits; verify fixture tests cover valid archives plus traversal, rooted paths, collisions, links, corrupt archives, and exceeded limits without writes outside staging.
- [x] 3.2 Implement filename-independent UTF-8 text scanning and replacement for the configured markers, with required-marker presence checks before replacement and unresolved reserved-marker checks afterward; verify repeated occurrences, ordinary TODOs, Webpack-to-Rolldown marker relocation, safely escaped values, and empty replacements for omitted deployment defaults. Verify binaries/unrelated content/lockfiles remain unchanged, no exact-count or filename checks are introduced, and scaffold-ready template onboarding/CI behavior is preserved without bundler or pipeline adapters.
- [x] 3.3 Implement absent/empty-destination publication and cancellation/error cleanup without file merging or recursive deletion of user destinations; verify real-filesystem tests cover hidden content, destination files and links, unsafe ancestors, paths with spaces, late competing writes, and absent/originally-empty destinations after handled failures.
- [ ] 3.4 Extend the scaffolding guide with destination restrictions, supported name/global forms, optional deployment defaults, execution-time connection selection and missing values, missing/unresolved marker diagnostics, and remaining model/CI setup; verify the steps against the generated fixture project and command behavior, and ensure the generated README provides the referenced additional setup steps.

## 4. Expose the local-only command

- [x] 4.1 Add `WebResourceInitSettings` and the `PowerCommand<TSettings>` init command in the webresource module, validating required package/global inputs and explicitly supplied deployment values before acquisition; verify module tests cover invalid/missing values, the success hint pointing to the generated `README.md`, failure output, cancellation, no Dataverse acquisition, and no package-manager or script execution.
- [x] 4.2 Register `webresource init` and examples through `CommandTree.Register`; extend `CommandTreeTests.cs` and `SettingsParsingTests.cs` for init help, required arguments/options, explicit/default version selection, optional solution/publisher-prefix values independently and together, inherited common flags without embedding connection values, and unchanged `webresource push` behavior. Verify shared command metadata continues to build with `ValidateExamples()`.
- [x] 4.3 Add the init reference and guide link to `docs/reference/commands/webresource.md`, keeping it user-facing and actionable; verify documented optional deployment defaults, README success hint, syntax, and completion/help metadata agree and clearly distinguish generation from dependency installation or deployment.

## 5. Verify real-template generation and integrate the capability

- [ ] 5.1 Run the real-template acceptance check against the prepared `main` from group 1, recording its resolved SHA: scaffold representative projects with and without deployment defaults into paths containing spaces, run `pnpm install --frozen-lockfile` and `pnpm run build:prod`, and verify bundles expose the selected global, the lockfile remains unchanged, defaults are consistent, missing script parameters fail before any Dataverse call, script commands match the template's tool pin, CI requires deliberate enablement, and the success hint points to actionable generated README instructions. Repeat explicit SHA selection to verify reproducibility without pinning dgtp's default.
- [ ] 5.2 Document the marker convention, coordinated marker-change rule, repeatable real-template acceptance procedure, and targeted test commands in `docs/development/webresource-scaffolding.md`; link to that page from `docs/development/build-and-test.md`. Verify no bundler/version adapters or tag-publishing process are required and the acceptance check stays opt-in without adding downloads/frontend installs to ordinary .NET builds/tests.
- [x] 5.3 Run `dotnet build` and the built webresource and CLI test DLLs via `dotnet exec`; verify zero build errors and passing relevant suites. Use the existing repository analysis/format checks for changed C# files, and synchronize the application lockfile if any dependency change proves necessary.
- [ ] 5.4 Sync the implemented `webresource-scaffolding` requirements to the main specs, then run `pnpm exec openspec validate --all` and strict validation of this change; verify specs describe the actual behavior and all artifacts remain coherent without archiving.

## Workflow follow-up

- Merge the shared scaffolding proposals first, then start each implementation from an up-to-date target branch in a separate branch and PR. Implement webresource scaffolding first, followed by plugin scaffolding; this order is recommended, not a technical dependency.
- Keep the change active through implementation review, beta integration, and stable release preparation.
- Archive only in a separate post-release follow-up after verifying the implementation PR was merged into `main`, a containing non-draft/non-prerelease GitHub release is published, and the required release/main commit ancestry holds.
