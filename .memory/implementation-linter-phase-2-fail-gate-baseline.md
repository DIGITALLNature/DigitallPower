# Implementation: Phase 2 linter fail-gate + SARIF baseline

Builds on `implementation-linter-module-phase-1.md`. Adds the pipeline quality-gate mechanics to
`dgtp lint run`.

## CLI additions (`LintVerb`)

- `--fail-on <None|Info|Warning|Error>` (default `Error`) - `None` disables the gate entirely
  (command always exits 0); otherwise the command fails if any non-baselined finding's severity is
  `>=` the threshold.
- `--report <path>` (already existed) - JSON findings, each entry now also carries a `Baselined`
  bool.
- `--sarif-output <path>` - full SARIF 2.1.0 export of all findings (baselined ones carry a
  `suppressions` entry).
- `--baseline <path>` - path to a SARIF file of previously-accepted findings. Matching findings are
  excluded from the `--fail-on` gate but still appear in console/report/SARIF output.
- `--update-baseline` - overwrites `--baseline` with this run's findings instead of gating; requires
  `--baseline` to be set; always exits 0.

## Baseline matching

`LintFinding.BaselineKey` = `RuleId|SolutionUniqueName|ComponentType|ComponentLogicalName|ComponentId`
(pipe-joined, per the architecture decision - human-diffable, not a hash). Stored in each SARIF
result's `partialFingerprints.dgtpLintKey`. `SarifWriter.ReadBaselineKeys` reads a baseline file back
into a `HashSet<string>` of keys; findings matching that set are treated as suppressed.

## `Reporting/SarifWriter.cs`

Single static class, SARIF POCOs are private nested types (keeps "one top-level type per file").
`WriteAsync` is used for both `--sarif-output` and `--baseline`/`--update-baseline` - same shape,
the only difference is whether `suppressedKeys` is passed (baseline writes never mark suppressions,
since a baseline is the accepted-state snapshot itself, not a report against a prior baseline).
Severity → SARIF `level`: Error→`error`, Warning→`warning`, Info→`note`.

## Known caveat carried over from Phase 1 — RESOLVED

`UnmanagedFieldNamingRule` used to read `component.SolutionId?.Name` directly for
`SolutionUniqueName`, which is empty in the `Digitall.Dataverse.Testing` fake environment. Fixed as
part of the entity-component-membership rework (see
`implementation-linter-entity-component-membership.md`): `LintContext` now resolves and exposes
`SolutionUniqueNamesById` from the same query used to scope components, and the rule reads
`EntityComponentMembership.SolutionUniqueName` from that instead of the unreliable `EntityReference.Name`.
