# Design

## Context

`CopyComponentsCommand` validates solutions, builds a deduplicated plan through `CopyComponentsContext`, applies app handling, renders the plan, and adds components through `AddSolutionComponentRequest`. Unmanaged tables are copied completely in best-practice mode; managed tables are anchors without automatic subcomponents. Managed attributes with active customizations may still be copied. App modes can skip, retain, or strip platform-added subcomponents.

The command currently changes memberships, not field definitions. Metadata is shared across solutions in the same environment. `ProtectCalculatedFields` in the legacy maintenance module instead scans all entities, selects unmanaged `SourceType == 1` attributes, excludes basis-currency fields and `bpf_duration`, and sets `IsCustomizable` false.

Official documentation establishes the intended model:

- [Specialized columns](https://learn.microsoft.com/en-us/power-apps/developer/data-platform/specialized-columns): `SourceType` 1 = Calculated, 2 = Rollup, 3 = Formula; null denotes no such calculation. Other values must not be treated as calculation-bearing by a broad non-null check.
- [Managed properties](https://learn.microsoft.com/en-us/power-platform/alm/use-managed-properties): protection takes effect after managed deployment, not as a lock on unmanaged development.
- [Column managed properties](https://learn.microsoft.com/en-us/power-apps/maker/data-platform/set-managed-properties-for-field): disabling customization affects the whole column and its subordinate customization options.

These documents describe the intended managed-property model, not a guarantee that every property is immutable. The approved evidence and explicit user acceptance below govern implementation; labels/translations may remain editable.

## Goals / Non-Goals

**Goals:** Add a bounded protection plan to the existing solution owner; preserve raw mode; validate all protection work before mutations; make shared-environment changes, exceptions, and partial failures visible.

**Non-Goals:** A new module, public configuration schema, additional switch, environment-wide scan, formula-only locking, data-access security, managed-field takeover, protection of prompt columns, or atomic multi-operation transactions. Do not preserve a dependency on the legacy maintenance module.

## Decisions

### 1. Default policy, with existing raw bypass

The user explicitly chose default protection in best-practice mode, rejecting the recommended opt-in alternative. No new flag is planned. `--raw` continues to disable all best-practice behavior, including protection-specific reads and writes; it is not a protection-only escape hatch. `--dry-run` prepares the same plan without writes. Update both option descriptions accordingly.

This is a user-affecting breaking behavior change, despite an unchanged command path and argument shape. Document it in the major-version migration guide.

### 2. Derive candidates from the final intentional plan

After existing copy filtering and app handling, collect:

1. Included attribute component IDs.
2. Attributes of included table roots that the plan adds completely.

Resolve selected attribute IDs even when the source has no explicit parent table membership row. Reuse existing metadata retrieval when practical, but do not rely on parent membership to make standalone attributes discoverable. Deduplicate by attribute metadata identity.

Limit candidates to `IsManaged == false` and source types 1, 2, or 3. A known null source type is a non-calculation column, not an error. Exclude managed attributes even if they have an active customization layer; copying them does not grant ownership of their managed properties.

Do not expand skeleton-only tables, unrelated existing target membership, or fields brought in solely by platform app/dependency expansion. A field already in the target is still in scope if intentionally selected by this run.

Preserve basis-currency companion and `bpf_duration` exceptions explicitly. Determine reliable technical companion identification, especially rollup status/date and currency companions, through the platform gate; no name-suffix heuristic or silent unknown-metadata skip. Do not introduce additional business-field exceptions without design review.

### 3. Prepare a protection plan before any mutation

Extend the existing copy planning flow with solution-owned protection preparation, not a generic metadata policy engine. The plan retains table/name/type, metadata identity, observed protection/mutability, and action/reason for reporting and execution.

Resolve all required metadata before the first `AddSolutionComponent`, app cleanup, or metadata write. A missing attribute, unknown managed state, unavailable required protection property, failed retrieval, or an eligible unprotected property that cannot be changed fails the command with a precise diagnostic and zero mutations.

Classify already false as unchanged before testing whether it can be changed. Recognized out-of-scope columns and documented technical exceptions are not failures. Only request protection-specific metadata in best-practice mode. Preflight cannot guarantee permissions or eliminate concurrent metadata changes.

Render the full plan before execution, including shared-environment consequences, protection changes, already protected columns, and skipped exceptions. Do not authenticate during local-invalid-input exits or acquire the initial connection inside a work display.

### 4. Copy first, protect second, confirm each write

Execution order:

```text
Local validation -> connection -> copy plan -> protection preflight -> render
                                                                     |
                                         dry-run ----------------> return
                                                                     |
                                              copy and app cleanup
                                                                     |
                                          sequential protection writes
                                               + per-field read-back
```

Any copy/app-cleanup failure prevents the entire protection phase. Protection failure stops subsequent protection work. Do not retry mutations or attempt automatic reversal. Use cancellation-aware async operations and preserve cancellation rather than handling it as an ordinary per-field error.

Write only the intended managed property with the metadata identifiers and type-specific payload required by the SDK/platform. Do not blindly resubmit full retrieved metadata or change formulas, labels, requiredness, types, or other managed properties. The minimal valid payload and any required publication are evidence-gated. Avoid global publication; if the approved effect requires broader mutations, stop for design review.

Confirm each write by reading the relevant metadata and verifying `IsCustomizable.Value == false` before proceeding. Treat a read-back failure or mismatch as failure with potentially changed, unconfirmed state. Report confirmed copy effects and protection effects separately; a request fault or cancellation may leave an in-flight server operation uncertain.

An unchanged rerun issues no protection writes. This promise does not change existing membership-add idempotence behavior.

### 5. Approved platform evidence and accepted limitations

On 2026-10-26 the user authorized a disposable test solution, publisher, and table in the stored `dupgate` connection, including managed export/import and cleanup. Existing business components were not modified. Formula definitions were created through supported Power Apps editors, not speculative SDK formula payloads.

- Minimal `UpdateAttributeRequest` payloads succeeded for Calculated (`StringAttributeMetadata`), Rollup (`IntegerAttributeMetadata`), and Formula (`DecimalAttributeMetadata`). Payloads contained only metadata ID, logical name, and `IsCustomizable = new BooleanManagedProperty(false)`, with entity name and `MergeLabels = true`. Use corresponding typed metadata for other supported storage types rather than resubmitting retrieved metadata.
- Per-field read-back confirmed false. Serialized before/after metadata differed only in `IsCustomizable` and the expected server `ModifiedOn` timestamp; formulas and unrelated settings were unchanged. An unchanged rerun issued zero updates.
- Both unmanaged and managed exports contained protection false for all three fields without publication. After removing only the disposable source solution and table, importing the managed archive in the same environment returned managed fields with protection false and `CanBeChanged = false`. No publish operation is required by this implementation.
- A label update to the managed calculated field succeeded and read-back confirmed its new label despite `IsCustomizable = false`. The user explicitly accepted editable labels/translations as compatible with protection. Do not promise either a formula-only lock or complete immutability of every column property.
- This was a same-environment roundtrip, not an independent target-environment test. Original-publisher servicing was prepared but not executed. Universal cross-environment restrictions and servicing behavior are not verified; disclose these limits rather than reporting those tests as passed.
- Complete-table retrieval returned rollup date/state companions with `SourceType = 0`, `AttributeOf = null`, and `IsLogical = false`: classify them as non-calculation fields, not by name suffix. The currency base field had `MoneyAttributeMetadata.IsBaseCurrency = true`, while the business money field had false. Use that explicit flag for basis-currency exceptions. Retain the exact historic `bpf_duration` exception as compatibility policy, not a newly verified live BPF test.

The user explicitly approved these conclusions and limitations and released implementation gates on 2026-10-26. No further platform experiments are required. Further live writes beyond disposable cleanup require separate approval. Accurate fixtures must model these observations without claiming unperformed platform tests.

## Risks / Trade-offs

- Shared metadata affects other solutions -> explain scope and consequences before execution; never describe protection as target-local.
- Column-level managed-property protection affects downstream flexibility -> disclose editable labels/translations, unverified servicing and cross-environment limits, and managed-only intent; preserve `--raw`.
- Metadata can change after preflight -> surface runtime failures, read back each change, make no atomicity or permission-completeness claim.
- Copy succeeds but protection partly fails -> fail the command with separate confirmed/unconfirmed outcomes; do not claim rollback.
- Complete tables add more candidates than explicit membership rows -> test complete-table expansion, standalone attributes, duplicates, and absence of unrelated updates.
- Old fakes may accept unsupported metadata operations -> require real platform evidence and accurate controllable metadata test doubles.

## Migration Plan

Implement within `dgt.power.solution` using the approved evidence and accepted limits above. Preserve existing CLI registration and app-handling tests; add protection and failure coverage. Update README, option help, and `docs/migrations/2.x-to-3.x.md` before release. No file format or JSON schema change is planned.

Existing pipelines using best-practice mode will acquire a metadata mutation; users must assess this policy before upgrading. `--raw` bypasses it but also changes selection as it does today. Removing or disabling the new code later does not restore modified properties. Any restoration requires a separately assessed metadata change by the appropriate owner, not an automatic rollback.

Maintenance retirement can finish independently. Do not revive `maintenance protectfields`, and do not archive this change merely because its planning or implementation is complete.
