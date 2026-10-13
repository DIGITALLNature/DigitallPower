# Proposal

## Why

DigitallPower's modular C# codebase is increasingly expensive for local and GitHub-hosted agents to navigate. A code-only Graphify graph may improve cross-module discovery without sending source code to an LLM, but only if every agent can obtain a fresh graph with little maintenance burden.

## What Changes

- Pilot Graphify 0.9.80 on maintained C# source and tests, excluding solution/project metadata, generated/build output, and documentation from extraction.
- Generate the code-only graph from each local or GitHub-hosted checkout. Keep graph output and machine-specific cache/path metadata out of version control.
- Pin the Graphify CLI, provide project-level Copilot guidance, and refresh the local graph after commit, branch switch, merge, and rebase where supported.
- Add a GitHub Actions smoke check and generate a fresh graph during GitHub Copilot cloud-agent setup; neither job commits generated files or calls an LLM.
- Include a small query set and documented review criteria to decide whether the pilot provides enough value to retain.

## Capabilities

### New Capabilities

- `codebase-navigation-graph`: Make a fresh, local-AST Graphify code graph available to project contributors and AI agents, while keeping generated graph/cache data out of source control.

### Modified Capabilities

None.

## Impact

Adds Graphify setup/update scripts, `.graphifyignore`, project-level Copilot instructions, Husky-compatible hook scripts, and a code-only GitHub Actions workflow. No application runtime, public CLI, NuGet dependency, or production build behavior changes.