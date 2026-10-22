# Spec Delta

## Purpose

Deploy active Word and Excel documenttemplates between Dataverse environments using a portable manifest, validated Word bindings, and explicit execution guarantees.

## ADDED Requirements

### Requirement: Documenttemplate transfer is resource-oriented

The CLI SHALL expose `documenttemplates import <Manifest> [--dry-run]` and `documenttemplates export <Directory> [--entity <LogicalName>] [--type word|excel] [--overwrite]`. It SHALL reject the legacy `import documenttemplates` and `export documenttemplates` paths without aliases. Help and completion SHALL advertise the replacement paths.

#### Scenario: Resource help
- **WHEN** help is requested for the new commands
- **THEN** their import or export operation is described

#### Scenario: Replaced path is invoked
- **WHEN** either legacy documenttemplate path is invoked
- **THEN** parsing fails without a Dataverse write

### Requirement: Transfer uses a strict portable V1 manifest

The manifest SHALL require `schemaVersion: 1` and `templates`. Each entry SHALL require a nonempty GUID `sourceId`, nonblank `name`, `documentType` (`word` or `excel`), table logical name `entity`, relative `file`, integer `languageCode`, and string-or-null `description`. Unknown properties and unsupported versions SHALL fail validation. Optional root `$schema` SHALL be a URI. Legacy manifests and inline directives SHALL NOT be accepted.

#### Scenario: Unknown or legacy property
- **WHEN** an entry contains `document_status`, `force_update`, or an unknown property
- **THEN** preflight fails without writes

#### Scenario: Explicit metadata
- **WHEN** a template is imported with an explicit language and null description
- **THEN** that language is applied and any existing description is cleared
- **AND** no implicit language default is substituted

### Requirement: Files resolve within the manifest directory

Import SHALL resolve each document relative to the manifest directory, including permitted subdirectories. Absolute paths, paths escaping that directory after symlink resolution, missing files, invalid packages, and document-type mismatches SHALL fail preflight. Import SHALL NOT search alternative locations.

#### Scenario: Nested relative file
- **WHEN** a valid matching document is referenced in a subdirectory of the manifest directory
- **THEN** import reads that file without depending on the working directory

#### Scenario: File escapes the package
- **WHEN** a relative path or symlink resolves outside the manifest directory
- **THEN** preflight fails without writes

#### Scenario: Document file is invalid
- **WHEN** a document is missing, malformed, or of a different package type than declared
- **THEN** preflight fails without writes

### Requirement: Identity and source IDs have distinct roles

Import SHALL match by `(name, documentType, entity)` with ordinal case-insensitive name comparison and SHALL preserve a matched target ID. New templates SHALL use `sourceId`. Duplicate input keys or IDs, ambiguous target matches, source IDs occupied by a different template, and names with leading or trailing whitespace SHALL fail preflight. Import SHALL NOT delete/recreate templates or infer renames.

#### Scenario: Existing template has a different ID
- **WHEN** an entry matches an existing target template by the semantic key
- **THEN** import preserves its target ID and applies the manifest display-name spelling

#### Scenario: New template
- **WHEN** no semantic match exists and the source ID is unoccupied
- **THEN** import creates the template with the source ID

#### Scenario: Conflicting identity
- **WHEN** an input key or source ID is duplicated, a target match is ambiguous, or a source ID belongs to another target template
- **THEN** preflight fails without writes

#### Scenario: Invalid name
- **WHEN** a name is blank or has leading or trailing whitespace
- **THEN** preflight fails rather than trimming it or stripping document extensions

#### Scenario: Identity component changes
- **WHEN** an entry changes an identity component and has no semantic target match
- **THEN** it is treated as a new template subject to source-ID collision checks
- **AND** the old template is not renamed or deleted

### Requirement: Word validates used CRM bindings

Before writes, import SHALL validate used CRM bindings in the main document, headers, and footers against target metadata, including fields, display projections, repeating collections, and traversed 1:N, N:1, and N:N relationships. Missing, ambiguous, or uninterpretable CRM references SHALL fail preflight without a validation bypass. Unused custom XML fields SHALL NOT be validated; foreign bindings SHALL remain unchanged.

#### Scenario: Related fields and repeating rows
- **WHEN** a Word template binds a related collection and fields within its repeating section
- **THEN** preflight resolves the relationship and validates the used fields against the related target table

#### Scenario: Relationship types
- **WHEN** a used Dataverse-generated binding traverses a 1:N, N:1, or N:N relationship
- **THEN** preflight resolves the actual relationship metadata rather than inferring its target from a name

#### Scenario: Invalid or unsupported CRM reference
- **WHEN** a used CRM field or relationship is missing, ambiguous, or cannot be interpreted
- **THEN** preflight fails without writes and identifies the offending binding

#### Scenario: Unused field is absent from target
- **WHEN** an absent target field occurs only in unused embedded XML
- **THEN** that field does not cause preflight to fail

### Requirement: Word adapts CRM namespace references

Import SHALL adapt supported CRM document-template namespace references to target object type codes in the main document, referenced custom XML and its namespace properties, headers, and footers. It SHALL preserve unrelated content and foreign bindings. Related-table bindings sharing a primary-table namespace SHALL retain that relationship. This contract SHALL NOT imply Word rendering or user data-access validation.

#### Scenario: Word template crosses environments
- **WHEN** a Word template is imported into a compatible target with a different table object type code
- **THEN** the applicable CRM namespace references use the target object type code
- **AND** related bindings sharing that namespace remain consistent across supported parts

#### Scenario: Foreign custom XML
- **WHEN** the document also contains non-CRM custom XML bindings
- **THEN** adaptation leaves those bindings and their content unchanged

### Requirement: Excel content is transferred unchanged

Import SHALL structurally validate the XLSX package and verify the associated target table exists. It SHALL transfer document bytes unchanged without adapting data sources, URLs, or formulas. Successful transfer SHALL NOT imply cross-environment data-source compatibility or rendering validation.

#### Scenario: Excel template transfer
- **WHEN** a valid Excel template passes preflight and is imported
- **THEN** its uploaded document bytes equal the input bytes
- **AND** its explicit metadata is applied

### Requirement: Export selects only active documenttemplates

Export SHALL select only active documenttemplates. Additional export filters SHALL narrow that selection and SHALL NOT allow drafts to be included. The CLI SHALL NOT expose a status-selection option.

#### Scenario: Source contains active and draft templates
- **WHEN** documenttemplates are exported
- **THEN** only active templates are included in the manifest and exported document files

#### Scenario: Additional filter matches a draft
- **WHEN** an additional export filter matches both active and draft templates
- **THEN** only the matching active templates are exported

### Requirement: Import deploys active documenttemplates

Import SHALL ensure that every successfully imported template is active, whether newly created or matched to an existing template. The new manifest SHALL NOT contain a configurable status field, and the CLI SHALL NOT expose a desired-status option. Import SHALL NOT report a template as successful unless its configured content, metadata, and active state have been applied successfully.

#### Scenario: New template is imported
- **WHEN** a listed template is successfully created in the target
- **THEN** it is active when its import completes successfully

#### Scenario: Existing draft is imported
- **WHEN** a listed template matches an existing draft in the target and its import succeeds
- **THEN** the target template has the configured content and metadata and is active

#### Scenario: Activation fails
- **WHEN** content or metadata is written but activation fails
- **THEN** the template and overall import are reported as failed
- **AND** the failure reports that partial changes occurred without implying rollback

#### Scenario: Unlisted draft exists in the target
- **WHEN** import runs and a target draft is absent from the input
- **THEN** that draft remains unchanged

### Requirement: Unlisted documenttemplates remain unchanged

Import SHALL NOT deactivate, delete, or otherwise modify a documenttemplate solely because it is absent from the input. It SHALL NOT offer cleanup.

#### Scenario: Partial documenttemplate import
- **WHEN** import runs and the target contains an unlisted active template
- **THEN** that template remains active and unchanged

### Requirement: Preflight precedes all import writes

Import SHALL validate local inputs before connecting, then resolve required target information and prepare all payloads before any writes. Preflight SHALL stop at the first error without executing prepared entries. `--dry-run` SHALL run the same preparation and report intended changes without writes; it SHALL NOT promise write-time success.

#### Scenario: Later input is invalid
- **WHEN** preflight encounters an invalid entry after earlier entries were prepared
- **THEN** no entry is written

#### Scenario: Dry-run
- **WHEN** a valid nonempty import runs with `--dry-run`
- **THEN** target information is read and intended creation, updates, and activation are reported
- **AND** no Dataverse writes occur

### Requirement: Execution is best-effort across templates

Import SHALL execute prepared templates sequentially and continue independent templates after a template fails. Failed dependent steps SHALL NOT continue. Any template failure SHALL fail the overall command and report identity and partial effects. Cancellation SHALL stop further work rather than continue as an item failure. Completed writes SHALL NOT be represented as rolled back; atomicity and automatic retries are not promised.

#### Scenario: One template fails
- **WHEN** a prepared template write fails and another independent template remains
- **THEN** the failed template's dependent steps are skipped and the remaining template is attempted
- **AND** the command ultimately reports failure even if the remaining template succeeds

#### Scenario: Cancellation
- **WHEN** cancellation is observed during execution
- **THEN** no further template work is started
- **AND** already completed writes are not reported as rolled back

### Requirement: Empty selections are successful

An empty valid import SHALL succeed without Dataverse access. Export with no matching templates SHALL succeed and write a valid empty manifest, subject to normal destination checks. Both commands SHALL report zero templates.

#### Scenario: Empty import
- **WHEN** a valid manifest contains an empty templates array
- **THEN** import succeeds without authenticating or writing

#### Scenario: Export has no matches
- **WHEN** the active-only selection and additional filters produce no matches
- **THEN** export writes a valid manifest with an empty templates array and succeeds

### Requirement: Export protects existing output

Export SHALL write `documenttemplates.json` and lowercase source-GUID `.docx` or `.xlsx` filenames. It SHALL prepare output and check all destination collisions before file writes. Existing files SHALL fail export unless `--overwrite` is supplied. Overwrite SHALL affect only generated destinations and SHALL NOT clean unrelated files. File-write failures SHALL report partial output without implying filesystem atomicity.

#### Scenario: Existing destination without overwrite
- **WHEN** a generated document or manifest destination already exists and `--overwrite` is absent
- **THEN** export fails before writing any output files

#### Scenario: Explicit overwrite
- **WHEN** export runs with `--overwrite`
- **THEN** it may replace generated destinations but leaves unrelated files unchanged

#### Scenario: Export file write fails
- **WHEN** a write fails after some output files were written
- **THEN** export reports failure and identifies partial output without claiming rollback
