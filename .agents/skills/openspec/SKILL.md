---
name: openspec
description: The starting point for anything OpenSpec. Use when the user asks for OpenSpec work without naming a skill, is unsure how to do something in OpenSpec, asks where a change stands or what to do next, or asks for several OpenSpec steps at once. Also use when the user says "openspec" and no more specific OpenSpec skill fits.
allowed-tools: Bash(openspec:*)
license: MIT
compatibility: Requires openspec CLI.
metadata:
  author: openspec
  version: "1.0"
  references: "4dc55c469b49"
---

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

OpenSpec helps people and coding agents agree on what software should do, and keeps that agreement true as the code changes. Each change is planned as an update to the specs, reviewed before code is written, and merged into them once it is built.

You are the starting point for anything OpenSpec in this project. Work out what the user needs, then do one of three things: answer their question, hand the request to the skill that does it, or run several skills in order.

**How OpenSpec fits together**

- `openspec/specs/` says how the system behaves today.
- `openspec/changes/<name>/` holds one planned change: its proposal, spec updates, design and tasks.
- A change usually goes: explore (optional), propose, apply, verify (optional), archive. Archiving updates the specs and moves the change to `openspec/changes/archive/`.

**When the user is unsure**

If they ask where things stand or what to do next, read `references/where-things-stand.md` and follow it.

If they ask how OpenSpec works or how to set something up, read `references/answers.md` and answer from it.

**Pick the skill**

When the user asks for work, find the row that matches, then use that skill and follow its steps.

| The user wants to | Use |
|---|---|
| think an idea or problem through before deciding what to build | `openspec-explore` |
| describe what to build and get the whole plan: proposal, specs, design, tasks | `openspec-propose` |
| start a change and write its plan one artifact at a time | `openspec-new-change` |
| write the next artifact of a change | `openspec-continue-change` |
| write every remaining artifact of a change at once | `openspec-ff-change` |
| revise the plan of an existing change | `openspec-update-change` |
| implement the tasks of a change | `openspec-apply-change` |
| check the code against a change before archiving it | `openspec-verify-change` |
| update the main specs from a change and keep the change open | `openspec-sync-specs` |
| finish a change and archive it | `openspec-archive-change` |
| archive several finished changes at once | `openspec-bulk-archive-change` |
| take a guided first run through OpenSpec | `openspec-onboard` |

- If the request could mean more than one row, ask which, naming the choices.
- If the skill for the row is not installed, say so, then read `references/without-skills.md` and follow the section for that skill. Tell the user they can install it with `pnpm exec openspec config profile`, then `pnpm exec openspec update`.

**Several steps at once**

If the request asks for more than one row, such as "verify this change, then archive it", do them in order, one skill at a time. Finish each before you start the next. Each skill's own rules still apply: when one tells you to stop and wait for the user, stop there and say what comes next.

**If this project does not use OpenSpec**

If you picked this skill yourself and `pnpm exec openspec list --json` reports `"root": null`, set OpenSpec aside and answer the request normally. If the user asked for OpenSpec, offer to set it up with `pnpm exec openspec init`.
