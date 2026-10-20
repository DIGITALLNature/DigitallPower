# Design

## Context

Code generation is a single `codegeneration` (`cg`) command that takes a mandatory config file (`-c`), a target directory, and a `--folder` name. The config is a V2 polymorphic document discriminated by `type` (`dotnet` | `typescript`) with a shared base class (`namespace`, `language`, `entities`, `requests`, `optionSets`). Entity selection resolves names, solutions, and a wildcard mask additively and writes the result back into the config object. TypeScript clears its whole subtree on each run; .NET deletes known generated files. Forms are generated for every selected entity and narrowed by a filter list of identifiers derived from generated filenames.

See proposal.md - Why for motivation. This document is a design checkpoint, not an implementation-ready design. Confirmed decisions are separated from outstanding decisions below; illustrative configuration examples discussed during exploration do not establish property names or schemas.

## Goals / Non-Goals

**Goals:**
- A config file that is the shared, authoritative recipe for what is generated and how.
- A resource-oriented CLI rooted at `model`.
- Targets that evolve independently, without a shared option shape.
- Predictable generation with reference validation before output changes.

**Non-Goals:**
- Config-free generation or a parallel CLI contract for generation settings. Individual property placement remains reviewable.
- Multiple targets in one config.
- Compatibility with the existing command or V2 config format.

## Decisions

### Resource root is `model`
`model` is the CLI root; the action is `generate`. Target subcommands (`model dotnet generate`) are not decided: target-specific configuration alone does not justify them; only distinct CLI inputs or workflows would.

*Alternatives:* `generate models` (keeps every resource a Dataverse object, but breaks resource-first grammar); `types`, `sdk`, `codegen`, `metadata` (rejected as too narrow, too broad, mechanism-named, or naming the input rather than the output).

### Clean break
Compatibility with the existing command and V2 config format is not a constraint on the redesign. This does not require changing every setting or output. User-affecting breaking changes are documented in the migration guide.

### Config is the contract; the CLI owns the destination
The config defines what is generated and how. The CLI supplies connection, config path, output location, and execution controls. Individual settings may later move to CLI parameters, but no override mechanism or config-free mode is introduced.

### One target per config; independent shapes
Each config describes one target. .NET and TypeScript contracts can evolve independently in structure, options, defaults, validation, and schema versions. Shared meaning should use consistent terminology, but shared shape is not required. The current unused TypeScript `namespace` property illustrates why target contracts should not be forced together. A minimal shared envelope and internal implementation reuse remain design choices, not requirements for a shared configuration base class.

### Explicit, independent artifact sections
Artifact categories are opt-in: a section requests that artifact and omission means it is not generated. Categories can be selected independently (for example, TypeScript entities without forms). Required supporting declarations are dependencies of a requested artifact, not separate options. Defaults keep sections concise; there are no switches for every member.

### Entity selection: include/exclude
Include/exclude selection is the agreed direction. The proposed algebra is the union of includes minus the union of excludes, with exclusion winning and no order dependence. Exact selector syntax, wildcard behavior, and diagnostics still need finalization.

Selecting a table always generates its full model definition, regardless of whether a solution includes the whole table or only selected components. Component-level solution scoping applies to components such as forms, not to partial entity models.

### Shared default scope, replaceable per section
An optional shared scope is the default for artifact sections. A section may declare its own selection, which replaces the default entirely. There is no narrowing or merging, so artifact independence is preserved.

### Resolve before writing
All explicit references (tables, solutions, operations, choices, and so on) are resolved and checked before any output is produced or any directory is cleared. Unknown explicit references fail rather than silently disappearing. Wildcard no-match behavior and implementation details of resolution remain open.

### Owned output directory
The CLI supplies the destination. The tool owns that output directory and clears it on regeneration; no manifest, ownership marker, or mandatory overwrite guard is planned. Recipes must not share a destination if their outputs need to coexist. Help and migration guidance must explain directory ownership.

Exact flags, target subfolders, the fate of `--folder`, and protection against catastrophic paths remain to be designed. A possible `--confirm` parameter was raised for later consideration, not agreed as part of this checkpoint.

### Form selection is broad within table or solution scope
Generate all supported forms belonging to selected tables, or included in selected solutions. A solution selector scopes form components, rather than expanding to every form of each referenced table. Combined with table selection, the table selector intentionally requests all supported forms for those tables.

Do not carry forward individual form-name filters, filename-derived identifiers, or individual form-ID lists. Extra declarations within the selected scope are an accepted trade-off for a straightforward shared recipe. Form selection remains independent of requesting entity-model output. The existing active Main, Quick Create, and Quick View set is the baseline to review, not a settled future support matrix.

Selection and generated naming are separate. Removing name-based selection does not solve localized or colliding generated identifiers.

### TypeScript output uses exported modules
Target bundler-based TypeScript projects with exported modules and explicit imports, without a parallel ambient-output mode. Type-only imports have no runtime output; required runtime values must have real implementations rather than exist only in declaration files.

Existing `XrmForm`, `XrmTable`, and related namespace names are not carried forward by default. Export names, grouping, and module paths remain open. Dataverse's standard `Xrm` API remains the platform API; generated modules do not replace it.

### .NET operation and message selection is explicit
Use one selection for custom operations and platform SDK messages, without an automatically appended built-in list:

| Selected item | Generated output |
|---|---|
| Custom API | Typed request/response classes and message-name constant |
| Classic Action | Existing typed-generation support is under review; removal is not decided |
| Platform SDK message | Message-name constant only |
| Unknown explicit name | Validation error before output changes |

Reuse SDK request/response types. Do not generate platform wrappers, including for platform messages lacking an SDK class. A platform message's existence alone does not establish a reliable parameter contract. Explicitly listing desired platform messages is the accepted trade-off; no generated catalogue or plugin-registration eligibility boundary is required.

### TypeScript Custom APIs provide directly executable requests
For each selected Custom API, generate:

- A parameter interface with required and optional inputs.
- A request class accepting that object, exposing request properties, and implementing `getMetadata()` for direct use with `Xrm.WebApi.online.execute`.
- A response-body interface where applicable. The caller parses and casts the response; no inference or execution wrapper is promised.

Parameterless APIs support construction without arguments. Do not generate separate TypeScript message constants, request factories, executors, transport/authentication abstractions, or a replacement Xrm type hierarchy.

Generated artifacts must work with standard Xrm typings. Reuse those types where suitable; narrowly scoped support types (for example for entity references) may be needed. Their shape and precise parameter mapping remain to be designed.

### Required choices are automatic dependencies
Include choices required by requested artifacts without a duplicate selection list. An independent choice section selects additional global choices or supports choices-only generation. Representation, localization, naming, and deduplication remain open.

## Risks / Trade-offs

- [Clearing a mis-specified destination deletes user files] -> Document directory ownership and resolve references before clearing; path safeguards and optional confirmation still need discussion.
- [Independent target shapes duplicate concepts] -> Share terminology where meaning is shared; share logic internally rather than the config shape.
- [Clean break forces every user to migrate] -> Migration guide with before/after examples; fail fast with actionable errors on legacy configs.
- [Broad form scope increases generation work and editor/file churn] -> Solution-based component selection keeps project scope manageable; avoid fragile per-form filters.
- [Module output breaks consumers of generated globals] -> Document import migration, request construction, runtime values, and test-helper usage; validate against representative bundled consumers.
- [Localization and duplicate form names destabilize generated APIs] -> Resolve naming separately before finalizing the output contract.

## Outstanding Design Decisions

These are unresolved and can affect specs and tasks. They are blockers for completing the design, not details delegated to implementation:

- Generated names, module organization, stable form identifiers, localization, and collision handling.
- Entity APIs for both targets, .NET framework support, supporting artifacts, and existing output options.
- Choice representation, naming/localization, dependency deduplication, and additional-choice selector details.
- Decide whether to drop classic Action request/response generation in favor of Custom APIs; assess migration impact separately from message-name constant support.
- Custom API binding and parameter mappings, optional/null handling, and any required support types.
- Final include/exclude selectors, wildcard diagnostics, and shared-default interpretation across artifact kinds.
- Supported form types and activation state; dependencies such as referenced Quick View forms.
- Target subcommands versus a single `generate` command; exact parameters and output layout.
- Config envelope, version/schema numbering, and legacy-config diagnostics.
- Failure/cancellation behavior after reference resolution, including whether rendering failures preserve previous output.
- Internal API and module boundaries. Retaining `dgt.power.codegeneration` is a recommendation, not a confirmed project-name decision.
