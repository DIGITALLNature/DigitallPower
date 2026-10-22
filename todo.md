# Technical Debt & Backlog

## Enforce initial connection acquisition before interactive work displays

Connection acquisition no longer owns a live display, and codegeneration connects before its
generation display. Commands and downstream services can still initiate first-time authentication
inside work displays if a new command overlooks the ordering.

Investigate a development-time safeguard rather than relying only on command conventions.
Acceptance criteria: local validation failures and no-work outcomes do not authenticate; initial
connection acquisition finishes before remote-work displays; connection failure prevents remote
work; automated checks catch a representative new-command violation. Evaluate lifecycle hooks,
connected-service injection, or architecture checks without committing to one design. Initial
acquisition ordering does not eliminate later SDK credential refresh.

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
