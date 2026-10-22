# Design

## Context

A cold Graphify 0.9.80 code-only scan after applying pilot exclusions found 707 code files and produced 11,338 nodes and 22,077 edges in 6.23 seconds. A focused path from `PluginPushCommand` to `PluginDeploymentPlanner` was useful; broad CLI/architecture queries were noisy or truncated. The repository uses Husky 9 with `core.hooksPath=.husky/_`.

## Goals / Non-Goals

**Goals:** Make a current code graph available to local contributors and GitHub Copilot cloud-agent sessions; keep refresh best-effort and reproducible; measure query value before expanding scope.

**Non-Goals:** Commit generated graph snapshots; extract documentation/media or call an LLM; require Graphify for every coding task; auto-stage or auto-commit generated output.

## Decisions

- Pin the Graphify CLI to 0.9.80 in one repository-owned version file. Install it with `uv tool install` for local use and in GitHub Actions.
- Use `graphify extract . --code-only` for initial setup and `graphify update .` for incremental updates. The pilot excludes solution/project metadata (`*.sln`, `*.slnx`, `*.csproj`), Graphify/agent automation (`.github/`, `.husky/`, `.copilot/`, `scripts/`), and `graphify-out/`; existing `.gitignore` rules continue to exclude build outputs.
- Keep all `graphify-out/` data untracked, including `graph.json`, manifest, report, cache, and machine paths. Generate a fresh graph from the current checkout for each cloud-agent session; local caches accelerate repeated runs.
- Use Graphify's project Copilot instructions and skills for query guidance. Query the graph for cross-module navigation, then verify relevant claims against code and tests. Graph absence or a poor answer must fall back to normal source exploration.
- Use the shared `.github/copilot-instructions.md` section for GitHub Web/VS Code guidance and the installed VS Code skill for local `/graphify` use. Query the graph for cross-module navigation, then verify relevant claims against code and tests. Graph absence or a poor answer must fall back to normal source exploration.
- Install Graphify's post-commit and post-checkout hook blocks through its Husky-aware installer. Add best-effort `post-merge` and `post-rewrite` hooks to run `graphify update .`, because the built-in hooks do not refresh after pulls/rebases. These hooks warn and succeed when Graphify is not installed or refresh fails.
- Install Graphify's post-commit and post-checkout hook blocks through its Husky-aware installer. Commit no absolute interpreter path; the hook's pinned path is blanked so Graphify resolves the installed CLI per clone. Add best-effort `post-merge` and `post-rewrite` hooks to run the pinned `graphify update .`, because the built-in hooks do not refresh after pulls/rebases. These hooks warn and succeed when Graphify is not installed or refresh fails.
- Add a GitHub Actions smoke workflow that installs the pinned CLI and performs code-only extraction into an ephemeral output directory. It fails on extraction errors but never uploads, commits, or pushes graph files. The Copilot cloud setup also builds a fresh code-only graph before agent work.
- Do not retain/register Graphify's union merge driver. No graph file is versioned in the pilot, so a custom merge driver would be unused and not configured on GitHub's merge workers.

## Risks / Trade-offs

- [Repeated cloud setup costs time] → Measure the setup duration in the smoke workflow and cloud-agent setup; retain only if it fits the existing setup budget.
- [Graph queries may be noisy or misleading] → Keep instructions scoped to architecture/cross-module questions and require source/test verification.
- [Graphify instructions may conflict with existing Copilot/OpenSpec guidance] → Inspect and reconcile the generated `.github/copilot-instructions.md` before committing it.
- [Husky or missing Graphify could affect Git operations] → Verify generated hooks preserve the existing `commit-msg` hook; refresh hooks must be best-effort and must not block commit, checkout, merge, or rebase.
- [Graph output is regenerated, not shared as a snapshot] → Every local setup and cloud-agent setup rebuilds from the exact checkout; no stale graph artifact is pulled from another branch.

## Migration Plan

1. Pin Graphify and define exclusions plus local output ignores.
2. Install project Copilot guidance and Husky-compatible local refresh hooks; generate the initial local graph for review without adding its output to Git.
3. Add cloud-agent graph generation and a separate code-only GitHub Actions smoke check.
4. Query the graph with the documented pilot questions, record results and runtime, then decide whether to keep or remove the integration.

Rollback removes the Graphify setup/workflow, project instructions and added hooks, then removes the pinned version file and ignore rules. Local `graphify-out/` data can be deleted with Graphify's purge option.

## Open Questions

- If code-only navigation meets the value bar, should a later proposal add model-assisted Markdown/OpenSpec extraction? That would require separate data-governance, cost, determinism, and CI decisions.