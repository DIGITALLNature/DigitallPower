<!-- digitallpower:post-merge-archive -->
## Repository archive gate (required)

This repository rule takes precedence over archive suggestions and completion examples below.
Keep the change active until a stable GitHub release containing the implementation has been
published from `main`. Merging into `beta` (including beta preparation for a later main release)
does not permit archiving. Merging into `main` without a published stable release, or publishing
a beta/prerelease, is also insufficient.

Before any archive-related writes, verify that the implementation PR for this change is merged
into `main`, and that the published stable release includes that PR:
1. Check the PR with `gh pr view <number-or-url> --repo DIGITALLNature/DigitallPower --json state,mergedAt,baseRefName,mergeCommit,url`. Require `state: MERGED`, non-null `mergedAt`, and `baseRefName: main`. Confirm it contains this change.
2. Check the corresponding GitHub release with `gh release view <tag> --repo DIGITALLNature/DigitallPower --json isDraft,isPrerelease,publishedAt,tagName`. Require a published, non-draft, non-prerelease release.
3. Fetch current `main` and the release tag, then verify the PR merge commit is an ancestor of the release tag and the release tag commit is reachable from `origin/main`.

Do not use an unrelated PR or release, or infer release status from the current branch. If the PR, release, or commit ancestry cannot be verified, report the blocker and stop without archiving; confirmation or optional guidance cannot bypass this gate.

After implementation, report "Implementation complete; awaiting stable release on main" rather
than offering immediate archival. Archive only as a separate post-release follow-up using a
checkout containing the released implementation, never during beta preparation or in the
implementation PR.
<!-- /digitallpower:post-merge-archive -->

# Doing a step without its skill

Use this when the skill for a step is not installed. Each section is the CLI path for one skill. Run the commands from the project root and read their `--json` output. The user can install the skill instead with `pnpm exec openspec config profile`, then `pnpm exec openspec update`.

## openspec-explore

Exploring is a conversation, not a command. Read `pnpm exec openspec list --json`, the specs that matter, and the code, then think the problem through with the user. Write no code and no change files until they decide what to build.

## openspec-propose, openspec-new-change, openspec-continue-change, openspec-ff-change

1. Start the change: `pnpm exec openspec new change "<name>"`, using a short kebab-case name.
2. Run `pnpm exec openspec status --change "<name>" --json` to see which artifacts are ready.
3. For each artifact you write, run `pnpm exec openspec instructions <artifact-id> --change "<name>" --json` and follow its `instruction` and `template`. Write the file at its `outputPath`.
4. Repeat steps 2 and 3. Stop after one artifact for a step-by-step change; keep going until planning is complete when the user wants the whole plan.

## openspec-update-change

Read the change's files, revise the ones the user asked about, then run `pnpm exec openspec validate "<name>"` and fix what it reports. Change planning files only, never code.

## openspec-apply-change

Run `pnpm exec openspec instructions apply --change "<name>" --json`, read the files it lists under `contextFiles`, then do the unchecked tasks in order. Tick each task in `tasks.md` (`- [ ]` to `- [x]`) as soon as it is done.

## openspec-verify-change

Read the change's tasks and specs, then check the code against each: every task done, every requirement and scenario implemented. Report what is missing. Change no files.

## openspec-sync-specs

No command updates the main specs and keeps the change open. Either merge each file under `openspec/changes/<name>/specs/` into the matching `openspec/specs/<capability>/spec.md` by hand and run `pnpm exec openspec validate --specs`, or archive the change, which merges them for you.

## openspec-archive-change, openspec-bulk-archive-change

First check the change is finished: `pnpm exec openspec status --change "<name>" --json` shows its artifacts, and `pnpm exec openspec list --json` shows `completedTasks` of `totalTasks`. If anything is unfinished, tell the user and ask before you go on. Then run `pnpm exec openspec archive "<name>" --yes`, which merges the change's spec updates into the main specs and moves the change to the archive. For several changes, do one at a time.

## openspec-onboard

Walk the user through one small change in their own project: explore it, propose it, apply it, archive it, using the sections above.
