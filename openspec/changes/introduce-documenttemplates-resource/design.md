# Design

## Context

Import matches by name and document type, conditionally deactivates missing templates, and can delete/recreate when `ForceUpdate` is set. Word files are rewritten using target metadata in the main document and related XML parts. Export writes a JSON manifest plus files, uses inline `FORCE`/`MISSING` directives, selects activated templates, and inverts status during serialization. Existing tests cover force replacement and document fixtures.

## Goals / Non-Goals

**Goals:** Deploy active templates with portable identity, predictable file handling, validated Word bindings, and explicit partial-failure behavior.

**Non-Goals:** Cleanup, delete/recreate, legacy-format parsing, general document transformation, Excel data-source migration, draft backup/restore, staged activation, Word rendering validation, or a shared synchronization framework.

## Decisions

- Introduce `dgt.power.documenttemplates`, owning both commands, manifest models, content adaptation, and tests. Place the OpenXML dependency with document processing rather than retaining the legacy import assembly.
- Both commands use module-owned settings. Import accepts a positional manifest path and `--dry-run`. Export accepts a positional directory, optional single-value `--entity` and `--type word|excel` filters combined with AND, and `--overwrite`. Shared connection and telemetry options remain available.
- Ordinary import leaves every unlisted template unchanged. There is no cleanup option.
- Use regular updates and preserve existing target IDs. Do not delete/recreate or fall back to replacement after an update failure. Verify API support before lifecycle implementation; return to design review if it is insufficient.
- Export only active Word and Excel templates. Additional filters narrow the active selection; no filter can include drafts.
- Import creates or updates each listed template and ensures it is active, including an existing draft matched in the target. A template is successfully imported only when its content, metadata, and active state have been applied successfully. Activation failure is an import failure, not a warning or success.
- The new manifest has no status field, and neither command exposes status-selection or desired-status options. This is an active-template deployment contract, not a status-preserving round trip. Unlisted drafts remain unchanged just like other unlisted templates.
- Retain Word and Excel transfer capability while validating target-specific metadata adaptation. Do not retain legacy status inversion or copy inline directive syntax without a reviewed contract.
- Keep shared file services reusable, but remove document-specific dependencies from other resource modules.

### Portable manifest and identity

The manifest is `documenttemplates.json` on export. It contains required `schemaVersion: 1` and a required `templates` array. Every entry requires `sourceId`, `name`, `documentType`, `entity`, `file`, `languageCode`, and `description`. Reject unknown properties at both levels and unsupported versions. An optional root `$schema` URI is permitted for editor tooling, not runtime schema selection.

```json
{
  "schemaVersion": 1,
  "templates": [
    {
      "sourceId": "12345678-1234-1234-1234-123456789abc",
      "name": "Firma",
      "documentType": "word",
      "entity": "account",
      "file": "12345678-1234-1234-1234-123456789abc.docx",
      "languageCode": 1031,
      "description": null
    }
  ]
}
```

- `sourceId` is a nonempty GUID. Use it for creation when there is no semantic match; otherwise preserve the matched target ID. Reject a source ID already assigned to a different target template.
- Identity is `(name, documentType, entity)`. Compare names with `OrdinalIgnoreCase`; resolve entity logical names through metadata. Reject empty names and leading/trailing whitespace rather than silently trimming or stripping file extensions. Apply the manifest spelling as the desired display name.
- Duplicate semantic keys or source IDs in the manifest and ambiguous matches in the target are preflight errors. Changing an identity component is not a rename; the unlisted old template remains unchanged.
- `documentType` is `word` or `excel`. `languageCode` is explicit, with no implicit 1033 fallback. `description` is a string or explicit null; null clears an existing description.
- Resolve `file` relative to the manifest directory. Require an existing file within that directory, including after symlink resolution. Subdirectories are allowed; absolute paths, escapes, and search-path fallbacks are not.
- Export uses lowercase source GUID filenames with `.docx` or `.xlsx`. The explicit manifest file reference, not a generated naming convention, controls import.
- An empty manifest is a successful no-op without Dataverse access. Export without matches writes a valid empty manifest and reports zero templates.

### Export writes

Prepare the selected documents and manifest and check all destination collisions before writing output files. Without `--overwrite`, an existing destination file is an error. With it, replace only generated destinations; never clean the directory or remove unrelated files. Detect collisions between generated destinations as well. There is no filesystem transaction guarantee; report partial output on a write failure.

### Word and Excel processing

Use a focused module-local Word adapter, not a general XPath engine or Office migration framework. Read CRM bindings in the main document, headers, and footers and resolve their referenced custom XML parts. Validate used bindings, including repeating-section collection paths, against target entity, attribute, and relationship metadata. Unused custom XML fields do not create validation obligations. Foreign custom XML bindings remain untouched.

Resolve relationship steps from metadata, not naming guesses. Support Dataverse-generated 1:N, N:1, and N:N binding paths. Resolve display/name-field projections from metadata rather than assuming every XML field is an ordinary attribute. An uninterpretable or ambiguous CRM binding is a preflight error, with no skip-validation option. Supported syntax must be grounded in generated examples; arbitrary XPath is not promised.

Adapt actual CRM document-template namespace references using the referenced table's target object type code in the main document, custom XML including namespace declarations/properties, headers, and footers. Related-table bindings can share the primary table's namespace; do not invent a separate namespace per related table. Preserve unrelated content and foreign bindings. Complete adaptation and package checks before writes.

The supplied Word example demonstrates account fields, the `contact_customer_accounts` collection, contact fields, and a repeating section. It does not establish N:1 or N:N XML syntax. Existing fixtures add header/footer coverage. The supplied file is approved for fixture use only after sensitive-content review and sanitization; do not preserve unnecessary business text or values.

Check Excel package structure and document type and transfer its bytes unchanged. Do not rewrite formulas, data sources, or URLs. Neither format receives a rendering, data-access, or general cross-environment usability guarantee.

### Execution and errors

Validate local inputs before acquiring the connection. For a nonempty import, resolve all needed target information and prepare document payloads before any writes. Stop preflight at the first error; do not execute a partially prepared set.

Dry-run uses the same preparation and reports intended creates, updates, and activation without writes. It requires target access and is not a transaction or a guarantee against later server errors.

Execute prepared templates sequentially, best-effort across templates. Stop a failed template's dependent steps, report its identity and any partial effects, and continue with independent templates. Any template failure makes the command fail. An activation failure is not success, even if content was written.

Cancellation stops further work and is not swallowed as an ordinary per-template failure. Already completed writes remain. Do not offer rollback, automatic retries, or batch atomicity. Target state can change after preflight; surface resulting write failures explicitly.

## Technical Evidence Gates

These two gates precede the corresponding implementation. Planning approval does not constitute their technical proof.

1. Verify regular content and metadata updates, activation, and ID preservation for Word and Excel against Dataverse. Test doubles alone cannot establish server support. If unsupported, stop and return to the user; do not add replacement implicitly.
2. Establish generated Word binding examples and metadata resolution for 1:N, N:1, and N:N, including repeating collections and display fields. The supplied 1:N example is a starting point. Obtain missing representative examples before claiming coverage; unresolved mappings return to design review.

## Risks / Trade-offs

- Regular content update may be restricted -> verify against Dataverse before implementing the lifecycle, with no destructive fallback.
- Word syntax or display-field mapping may be insufficiently evidenced -> use generated fixtures and require explicit metadata resolution, not heuristics.
- Incorrect status inversion or name normalization -> test active-only export, activation, case-insensitive matching, and rejection of invalid names.
- Activation fails after content or metadata was written -> report the template as failed and surface partial effects; do not imply rollback.
- Word XML changes corrupt content or miss references -> verify targeted namespace changes and preserved bindings across representative parts.
- Existing output may be overwritten -> default to collision failure; require explicit overwrite and never perform directory cleanup.

## Migration Plan

Introduce the module after the technical evidence gates, then switch CLI paths and remove legacy documenttemplate commands and owned DTOs when consumers have migrated. Move OpenXML references only after consumer checks and synchronize applicable lock files. Update README and `docs/migrations/2.x-to-3.x.md` with positional paths, the V1 manifest, identity/ID rules, strict relative file paths, active-only behavior, removal of `document_status`, `force_update`, `ignore_missing`, and inline directives, no cleanup/replacement, and best-effort execution. Removing the entire legacy transfer modules is a separate dependent change.
