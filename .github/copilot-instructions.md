## graphify

For any question about this repo's architecture, structure, components, or how to add/modify/find
code, your first action should be `graphify query "<question>"` when `graphify-out/graph.json`
exists. Use `graphify path "<A>" "<B>"` for relationship questions and `graphify explain "<concept>"`
for focused-concept questions. These return a scoped subgraph, usually much smaller than the full
report or raw grep output.

Triggers: "how do I…", "where is…", "what does … do", "add/modify a <component>",
"explain the architecture", or anything that depends on how files or classes relate.

If `graphify-out/wiki/index.md` exists, use it for broad navigation. Read `graphify-out/GRAPH_REPORT.md`
only for broad architecture review or when query/path/explain do not surface enough context. Only read
source files when (a) modifying/debugging specific code, (b) the graph lacks the needed detail, or
(c) the graph is missing or stale.

Type `/graphify` in Copilot Chat to build or update the graph.

## DigitallPower Graphify Code-Only Pilot

These repository-specific rules override the generic Graphify workflow above. Use only the pinned
Graphify version in `graphify-version.txt`. Build with `graphify extract . --code-only` and refresh
with `graphify update .`. Do not run the full `/graphify` workflow, `graphify label`,
`graphify cluster-only`, or semantic extraction for this repository. Treat graph edges as
navigation hints and verify conclusions against source and tests.
