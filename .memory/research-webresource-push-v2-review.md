# Webresource Push V2 Design Review

The resource-oriented `webresource push` module correctly isolates webresource deployment from
the legacy combined `push` module. It uses local discovery, pure state comparison, async
repositories, and explicit settings validation. It does not reference the legacy processor.

Its deployment-plan pipeline should follow the `plugin push` contract more closely:

- The command should build and render a complete typed plan before performing writes, and an
  executor should receive that plan rather than loading remote state and rebuilding it itself.
- A complete plan must include solution membership additions and obsolete deletions. These
  operations must be visible to `--dry-run`, not only reported after a real write. Created and
  updated resources are collected and published in one request during execution, but publishing is
  intentionally not a plan-rendered operation. `--publish-mode batch` is the default; `single`
  publishes each changed resource separately for comparison or timeout troubleshooting. The
  completed publish line reports each request duration in seconds; single mode also reports the
  summed publish duration.
- Terminal output should have distinct `Plan` and `Execution` phases and explicitly report a
  no-change deployment.
- The webresource plan renderer follows the plugin renderer’s terminal conventions: the tree root
  is the target directory, and solution membership is a labelled section with the solution name,
  `+` links, or a green checkmark when all managed components are present. Obsolete webresources
  remain in a separate section because they have no local path in the target-rooted tree; each uses
  a red `-` icon. Like solution membership, the obsolete section is a labelled list rather than a
  horizontal rule. Resource actions use the same `Create`, `Update`, and `Unchanged` terminology
  as plugin deployment plans.
- The host sets `Console.OutputEncoding` to UTF-8 after the dotnet-suggest early-exit gate so
  Spectre.Console can render plan emojis on Windows. The setting must remain after that gate,
  whose standard-output contract permits completion candidates only.

Two correctness constraints are important when implementing that alignment:

- A managed webresource should be rejected only when the planned action would update it. An
  identical managed resource is a valid no-op and must not fail deployment.
- Dataverse resource-name matching is case-insensitive. Remote snapshots and plan keys must use a
  case-insensitive comparer; otherwise a remote record whose stored name casing differs from the
  local/mapped name is incorrectly planned as a create.

The dedicated command intentionally changes several legacy behaviors: it requires an explicit
publisher prefix for directory targets, requires an explicit logical name for single-file targets,
uses `--mapping-file` with a `mappings` JSON property rather than legacy `--config`/`maps`, prevents
managed updates, surfaces solution-add failures, and publishes both creates and updates. The legacy
command remains needed for callers that rely on its automatic solution-derived/default `new` prefix
or its old mapping-file contract until migration guidance or compatibility support is provided.
