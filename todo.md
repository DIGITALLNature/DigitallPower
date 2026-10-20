# Technical Debt & Backlog

## Retire legacy secure configuration import

`import secureconfigs` (`SecureConfigImport`) duplicates secure configuration provisioning in
`plugin step config set`. The plugin module's
`SdkMessageProcessingStepSecureConfigRepository` creates the secure configuration and links the
step in a single transaction; the legacy import still creates and links separately.

- Plan removal of the legacy command, DTO/schema, and dedicated tests after checking other consumers.
- Document migration from the legacy JSON (`pluginStep` display name plus `data`/inline data) to
  `plugin step config set --step-id` or composite-key selection with `--secure`/`--secure-file`.
- Update command-tree coverage, README, migration guidance, and OpenSpec requirements when removing it.
- Until removal, the legacy path remains exposed: after creation succeeds and linking fails, its
  general exception handler uses the command token for cleanup. Concurrent cancellation can abort
  deletion and leave an orphaned secure configuration. Track or fix this separately if removal is
  deferred; the plugin module's transactional creation does not have this rollback gap.

Source: #203, Copilot review discussion `discussion_r4313851799`.

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
