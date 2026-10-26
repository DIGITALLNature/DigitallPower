# Tasks

## 1. Characterize current output

- [x] 1.1 ~~Add characterization tests for `ExportJson.GetJson` and `FileService.ExportJsonFile`~~ Obsolete: the export module was removed by `retire-legacy-import-export`, so no `Regex.Unescape` workaround remains to characterize (recorded in design.md).
- [x] 1.2 Confirm which JSON-writing commands are in scope (analyzer and lint reports in particular) and record the decision in design.md. Verify by listing every `JsonSerializer.Serialize*` call site under `src/` with its classification.

## 2. Shared serializer

- [x] 2.1 Add a shared JSON output helper in `dgt.power.common` that applies the relaxed encoder to caller-provided options, with unit tests for literal non-ASCII text, required escapes, HTML-sensitive characters, supplementary characters, and round-trip parsing. Verify with `dotnet test --project tests/dgt.power.common.tests/dgt.power.common.tests.csproj`.

## 3. Adopt in writers

- [x] 3.1 Use the helper in the process, duplicaterules, teamtemplates, and (if merged) documenttemplates capture writers; update their writer tests to assert literal umlauts and keep existing output shape. Verify with each module's test project.
- [x] 3.2 ~~Replace the `Regex.Unescape` workaround in `ExportJson` and `FileService` with the helper~~ Obsolete: the export module no longer exists (see 1.1).
- [x] 3.3 Add a reader test per resource proving that previously escaped files still load identically. Verify with each module's test project.

## 4. Documentation and verification

- [x] 4.1 Mention the UTF-8 readable-output guarantee and the one-time diff for previously captured files in README. Verify that the README examples match the writer test fixtures.
- [ ] 4.2 Run `dotnet build`, the affected test projects, and `pnpm exec openspec validate --all`. Verify 0 new warnings and all tests pass.

## Workflow follow-up

- The current `json-file-output` capability spec lives in `openspec/specs/json-file-output/spec.md` and ships with the implementation PR.
- Keep this change active after the implementation PR merges into `beta` or `main`. Archive it only in a separate follow-up after verifying that the merged implementation PR targets `main`, a published stable (non-draft, non-prerelease) GitHub release contains it, and the PR merge commit is an ancestor of the release tag that is reachable from `origin/main`.
