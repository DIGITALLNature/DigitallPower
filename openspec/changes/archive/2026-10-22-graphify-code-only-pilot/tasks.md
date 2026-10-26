# Tasks

## 1. Pin and Scope Graphify

- [x] 1.1 Pin Graphify 0.9.80 in a repository-owned version file and add `.graphifyignore`/`.gitignore` rules for project metadata and local Graphify output; verify ignore behavior with a fresh code-only scan and confirm `graphify-out/` remains untracked.

## 2. Integrate Agent Guidance

- [x] 2.1 Install the project-scoped Graphify VS Code/Copilot guidance, then scope it to cross-module navigation and source/test verification; verify the diff preserves existing OpenSpec instructions and is read by GitHub Copilot cloud agent.
- [x] 2.2 Add the pinned Graphify installation and fresh code-only graph build to GitHub Copilot setup; verify the setup workflow YAML and its generated graph path.

## 3. Refresh Graph on Git Events

- [x] 3.1 Install Graphify's Husky-aware commit/checkout hooks and add best-effort merge/rewrite refresh hooks; verify the hooks are recognized, incremental refresh succeeds, commitlint still passes, and missing `uvx` does not block Git operations.

## 4. Validate Pilot in CI

- [x] 4.1 Add a GitHub Actions smoke workflow that installs the pinned Graphify version and builds a code-only graph in ephemeral output; verify it runs without an LLM, does not modify tracked files, and fails on extraction errors.

## 5. Evaluate and Document

- [x] 5.1 Document local setup, refresh commands, exclusions, hook behavior, cloud-agent availability, and pilot review questions; verify setup commands in this checkout and record graph size/runtime plus results for the query set. A clean-clone rollout check remains for the team.
- [x] 5.2 Run OpenSpec strict validation and relevant CI/hook checks; confirm `graphify-out/` and machine-specific files remain untracked.