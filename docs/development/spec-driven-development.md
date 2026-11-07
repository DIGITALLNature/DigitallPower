# Spec-Driven Development

Use [OpenSpec](https://github.com/Fission-AI/OpenSpec) for significant changes to public CLI/API
behavior, configuration and schemas, architecture, module boundaries, or behavior spanning
capabilities. Start with `/opsx-propose`; after review, implement with `/opsx-apply`, update the
main specs, validate them, and archive the completed change. Small fixes, test-only changes, and
behavior-preserving refactors do not require a proposal. The CLI is installed locally with pnpm
and is run as `pnpm exec openspec`.

Current requirements live in [`openspec/specs/`](../../openspec/specs/). Durable design rationale lives in
[`docs/architecture/decisions/](../architecture/decisions/). User-facing documentation lives in
[`docs/](../README.md); implementation details belong beside their code and tests.
Run `pnpm exec openspec update` when refreshing generated Copilot files. Repository-specific rules
(local `pnpm exec` CLI usage and the post-release archive gate) live in
[`openspec/config.yaml`](../../openspec/config.yaml) under `context` and `operations.*.guidance`
and reach the generated workflows at runtime; do not post-process the generated files.

GitHub Copilot cloud agent support is opt-in through `githubCopilot.cloudAgent` in
[`openspec/config.yaml`](../../openspec/config.yaml). Its setup workflow installs the locked pnpm
dependencies and .NET SDK before an agent session; OpenSpec commands continue to use
`pnpm exec openspec`. The setup workflow must be present on the default branch, and cloud-agent
access must be enabled by the repository or organization administrator.

## Graphify Code-Only Pilot

The pilot pins Graphify in [`graphify-version.txt`](../../graphify-version.txt) and builds a fresh C# code
graph locally, in GitHub Copilot cloud-agent setup, and in a GitHub Actions smoke check. The graph,
manifest, and cache stay under ignored `graphify-out/`; they are regenerated from the current
checkout and are not committed. Code extraction uses local AST parsing only. Documentation, images,
and semantic LLM extraction are out of scope.

For a local clone, install the pinned CLI and create the initial graph:

```bash
uv tool install graphifyy==0.9.80
graphify vscode install
graphify extract . --code-only
```

`graphify vscode install` adds the local VS Code skill and refreshes the project Copilot instructions.
The same tracked instructions guide GitHub Web agents. `pnpm install` activates Husky hooks:
Graphify refreshes after commits and branch switches, while post-merge and post-rewrite hooks refresh
after pulls and rebases. These are best-effort; if a refresh is skipped, run `sh scripts/graphify-refresh.sh`.
Cloud-agent setup builds the graph for each current checkout, and the GitHub Actions smoke workflow
checks that code-only extraction and a representative plugin path succeed without committing output.

Use Graphify for cross-module navigation and verify conclusions against source and tests. The measured
results and retention criteria are in [the evaluation note](../architecture/graphify-pilot-evaluation.md).

The separate DigitallPower pilot override in `.github/copilot-instructions.md` preserves code-only
mode when the Graphify-owned section is refreshed. `graphify copilot install` targets Copilot CLI
and is not needed for VS Code or GitHub Web.
