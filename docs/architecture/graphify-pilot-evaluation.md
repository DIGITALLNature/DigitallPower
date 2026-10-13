# Graphify Code-Only Pilot Evaluation

## Setup

The pilot used Graphify 0.9.80 with `extract --code-only`. Source is parsed locally; no semantic model backend was configured. The scan excluded solution/project metadata, Graphify output, generated build output, and the Graphify/Copilot automation files.

## Measured Build

- Cold scan: 6.23 seconds on the local macOS development machine
- Included: 707 code files
- Output: 11,338 nodes and 22,077 edges across 487 communities
- Duplicate nodes after excluding project metadata: 3 exact duplicates (the unfiltered scan reported 49)
- Output size: approximately 17 MiB for `graph.json` and 172 KiB for `manifest.json`
- No generated graph artifacts are tracked; outputs live under ignored `graphify-out/`

## Query Findings

| Question / query | Result | Assessment |
|---|---|---|
| `graphify path PluginPushCommand PluginDeploymentPlanner` | Two-hop path through `ProcessTargetAsync`, ending in an inferred call edge | Useful starting point; confirm the call in source |
| `graphify path CommandTree CompletionEngine` | No directed path; undirected traversal found a six-hop route through Spectre.Console CLI | Weak signal; the relationship is not directly represented |
| Broad plugin planning/execution query | 112 nodes; response truncated with unrelated repositories and tests | Too broad; add context filters or use `path` |
| Telemetry anonymization/caller query | Returned only `Exception` and `TelemetryAnonymizer` | Did not identify the caller; source search remains necessary |

## Pilot Decision Gate

Keep Graphify only if teammates and both VS Code/GitHub cloud agents find it useful for representative cross-module questions, use query results to locate verifiable source/test evidence, and the fresh cloud setup remains within its timeout budget. Treat the graph as an optional navigation index; do not use it as the behavioral source of truth. Do not add semantic documentation extraction or versioned graph snapshots as part of this pilot.