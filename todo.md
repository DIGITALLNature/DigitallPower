# Technical Debt & Backlog

## Schema URL naming and versioning

Review published schema paths for resource/capability alignment and establish a convention for
versioning new schema contracts. Do not rename existing URLs solely for consistency or break
configs that reference them.

- `schemas/push/schema.json` describes legacy webresource mappings; current documentation uses
  `schemas/webresource/schema.json`. Check remaining consumers and define legacy retention.
- `schemas/analyzer/schema.json` describes the specific `analyze entityallassets` config rather
  than a general analyzer contract. Consider a capability-specific path when redesigning that area.
- `schemas/plugin/state/schema.json` describes plugin step state, while the CLI path is
  `plugin step state set`. Review whether future schemas should include the `step` segment.
- Existing versioned and unversioned paths coexist. Document a convention for future contracts;
  preserve published legacy paths when introducing replacements and update relevant examples.

The model-generation redesign plans a separate schema family under
`schemas/modelgeneration/v1/`, leaving `schemas/codegeneration/v2/` unchanged.

---

## Global `--verbose` flag

Add a global `--verbose` option (shared settings base, tracer behavior and every module) for
optional diagnostic detail. First intended consumer: `plugin push`, which already tracks
assemblies skipped by the batch reader (`TargetReadResult.Skipped`) and could list them, plus the
reason, when verbose output is requested. Default output stays quiet.

Deliberately out of scope for the plugin rewrite: it changes shared settings and every module.

---

## Package replacement for removals

Idea: mirror the standalone replacement path for plugin packages. A removal would require a new
package identity: the tool creates a new package record, moves surviving steps and Custom API
links onto its types, then deletes the old package. A managed import could then replace the old
package without hitting "Unable to delete plugintype due to 1 step(s) registered on it", because
package assemblies are otherwise updated in place (same `pluginassemblyid` across assembly
versions).

Open questions to prototype:
- Identity scheme: the package unique name is `<prefix>_<package id>` and excludes the version,
  so a new record needs a derived name (for example a major-version suffix) or a new NuGet id.
- Moving steps onto types in a different assembly (see test case P12).
- Whether Dataverse allows two packages with the same assembly name side by side, and whether the
  import deletes the old package cleanly.
- Scope: trigger only for removals; keep in-place updates for everything else.
- User impact: package name change needs a migration guide entry.
