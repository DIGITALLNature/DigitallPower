# Implementation: Phase 1 linter module

## Scope

The first linter phase introduces a standalone `dgt.power.linter` module with a minimal rule pipeline, a configuration model, and a single built-in rule: unmanaged custom field logical-name validation.

## Design

- `ILintRule` defines a common contract for rule metadata plus evaluation against a shared `LintContext`.
- `LintRuleCatalog` provides the single registry for enabled rules and is the extension point for future rules.
- `LintContext` caches entity metadata and solution component entries so rules can evaluate solution-scoped metadata without repeated Dataverse calls.
- `LintRunCommand` resolves config, filters by solution and rule IDs, executes every enabled rule, and returns a process exit based on the configured severity threshold.
- `LintVerb` exposes the CLI options for `--solutions`, `--config`, `--rules`, `--report`, and `--fail-on`.

## First rule

`UnmanagedFieldNamingRule` checks unmanaged, custom attributes in the selected solutions against
the [DIGITALL Nature naming convention](https://digitallnature.github.io/customizing/naming-conventions/):
`prfx_fieldname[_type-suffix]`.

- Ignores managed attributes, non-custom attributes, and attribute logical names that contain no
  underscore at all (Dataverse-provisioned defaults such as `name`, `createdon`, or an
  auto-created `statecode` on a new custom table - these are outside of what an unmanaged
  customization controls and must never be flagged).
- `publisherPrefixes` (config option, default `["dgt_"]`) accepts one or more allowed prefixes;
  the required suffix is derived internally from the attribute's Dataverse type (Lookup → `_id`,
  Picklist → `_set`, MultiSelectPicklist → `_mset`, Money → `_cur`, DateTime → `_dt`, Customer →
  `_vid`, Integer → `_int`/`_dur`/`_lcid`/`_tzid` by `IntegerFormat`, Decimal → `_dec`, Double →
  `_flt`, Boolean → `_bit`, File → `_file`, Image → `_img`, Memo → `_txt`, String → none/`_txt`/
  `_url`/`_number`/`_email` by `FormatName`), plus a `_rf`/`_cf` modifier for Rollup/Calculated
  fields via `AttributeMetadata.SourceType`. There is deliberately no configurable
  `fieldTypeSuffixes` map - exposing raw SDK class names in a public config surface was a leaky
  abstraction and was removed.
- Emits one `LintFinding` per nonconforming logical name with its entity and component metadata.

## Config typing

`LintRuleConfigEntry.Options` stays a raw `JsonElement` (so `LintConfig` itself is loosely typed at
the top), but exposes `ReadOptions<T>()` which deserializes it into a rule's own strongly-typed
options POCO (e.g. `UnmanagedFieldNamingOptions`) via `System.Text.Json` - no more manual
`TryGetProperty` parsing per rule. `schemas/linter/schema.json` gives each rule id its own closed
`$defs/rules/<id>` schema (not a single shared "options" shape for all rules), so adding a new rule
cannot loosen or collide with an existing rule's schema.

## Validation

The phase-one implementation is intentionally narrow: it adds the module skeleton, the CLI branch, the rule contract, and the config-driven execution flow without touching the analyzer or broader power logic layers.
