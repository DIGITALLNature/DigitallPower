# Spec Delta

## Purpose

Manage publication state and ownership of existing Dataverse duplicate-detection rules through explicit resource-oriented configuration.

## ADDED Requirements

### Requirement: Existing rules have resource-oriented operations

The CLI SHALL expose `duplicaterules init` and `duplicaterules apply` to capture and apply publication state and ownership of existing rules. These operations SHALL NOT create or delete rules or transfer rule definitions and conditions.

#### Scenario: Existing rule is captured
- **WHEN** a selected existing rule is captured
- **THEN** its publication state and ownership can be represented in configuration without exporting its definition for deployment

#### Scenario: Configured rule does not exist
- **WHEN** a configured rule cannot be found
- **THEN** preflight fails rather than creating it

### Requirement: Commands use explicit paths and options

Both commands SHALL accept a positional file path. Init SHALL expose `--capture state|owner|both` (default `state`), `--entity`, repeatable `--rule-id`, `--owner-mapping`, `--unmapped-owners fail|skip` (default `fail`), and `--overwrite`. Apply SHALL expose `--owner-mapping`, `--dry-run`, `--no-wait`, and `--timeout` (default 300 positive integer seconds). Relative CLI paths SHALL resolve against the working directory without automatic file search.

#### Scenario: Invalid timeout
- **WHEN** apply receives a noninteger or nonpositive timeout
- **THEN** local validation fails without Dataverse access

#### Scenario: Dry-run with no-wait
- **WHEN** apply runs with both flags
- **THEN** it reports intended submission-only behavior without writes

### Requirement: Configuration expresses strict V1 intent

Configuration SHALL require `schemaVersion: 1` and `rules`, with optional root `$schema` URI. Each entry SHALL require a nonempty GUID `ruleId` and at least one of boolean `published` or logical-key `owner`. An optional string `name` SHALL be informational only and SHALL NOT be used for matching or as a requested effect. Null values, duplicate IDs/properties, unknown fields, unsupported versions, and blank or whitespace-padded owner keys SHALL fail local validation. Omitted effect properties SHALL mean unchanged.

#### Scenario: No requested effect
- **WHEN** an entry contains only its rule ID, optionally with an informational name
- **THEN** local validation fails

#### Scenario: Strict input
- **WHEN** configuration contains null intent, duplicate IDs/properties, or definition fields
- **THEN** local validation fails without writes

### Requirement: Existing rules are identified by GUID

Application SHALL identify existing rules by their configured GUID, relying on IDs transported separately through solutions. It SHALL NOT fall back to name or definition matching. A missing target GUID SHALL fail preflight even if a rule with the same display name exists.

#### Scenario: Same name but different ID
- **WHEN** the configured GUID is absent but a rule with the same name exists
- **THEN** preflight fails without changing that rule

#### Scenario: Informational name differs from target
- **WHEN** the configured informational name differs from the target rule's current name
- **THEN** application matches by GUID and does not rename the rule

### Requirement: Rule changes express explicit intent

Application SHALL leave unlisted rules and omitted properties unchanged. It SHALL support publishing, unpublishing, and ownership changes and avoid unnecessary writes when the requested state is already satisfied.

#### Scenario: State-only configuration
- **WHEN** a rule requests publication without an ownership change
- **THEN** application publishes the rule without changing its owner

#### Scenario: Owner-only configuration
- **WHEN** a rule requests only a supported ownership change and application succeeds
- **THEN** the requested owner is applied and the rule retains its original publication state

#### Scenario: Already satisfied rule
- **WHEN** the target already matches all specified properties
- **THEN** application succeeds without unnecessary writes

### Requirement: Init selection is complete and explicit

Init SHALL capture all rules unless restricted by an exact base-table logical name or a set of repeated GUID options. Filters SHALL combine with AND. Missing explicitly selected IDs and IDs conflicting with the table filter SHALL fail capture. Selection SHALL include all pages. Selected Publishing or unknown/inconsistent rule states SHALL fail capture before output is written.

#### Scenario: Repeated selection IDs
- **WHEN** the same GUID appears several times as a selection option
- **THEN** it selects one rule rather than producing duplicate configuration entries

#### Scenario: Selected ID conflicts with table
- **WHEN** an explicitly requested rule belongs to another base table than the filter
- **THEN** capture fails instead of silently omitting it

#### Scenario: Transitional selected rule
- **WHEN** a selected rule is Publishing
- **THEN** capture fails without replacing output, including for owner-only capture

### Requirement: Capture emits only requested properties

Init SHALL emit GUID-ordered entries containing the informational rule name and exactly the requested state, owner, or both. Owner capture SHALL follow the shared full-source-mapping and reverse-key contract, without invented keys or raw principal IDs; rules whose owner that contract skips SHALL be emitted without `owner` and reported as warnings. State-only capture SHALL NOT require mapping, but a supplied mapping SHALL be locally validated.

#### Scenario: Default capture
- **WHEN** init runs without a capture option
- **THEN** it captures publication state without owner intent or required mapping

#### Scenario: Capture owner aliases
- **WHEN** several source mapping keys identify a captured owner
- **THEN** capture fails rather than selecting a key

#### Scenario: Platform-owned rule
- **WHEN** a selected rule is owned by the SYSTEM or INTEGRATION platform principal without a matching key
- **THEN** init warns per principal and writes the rule with `published` but without `owner`, omitting it for owner-only capture

#### Scenario: Unmapped owner by default
- **WHEN** a selected rule's owner is not a platform principal and has no key, and `--unmapped-owners` is `fail`
- **THEN** capture fails without writing output

#### Scenario: Unmapped owner skipped
- **WHEN** a selected rule's owner has no key and `--unmapped-owners skip` is set
- **THEN** init warns and writes the rule without `owner`, omitting it for owner-only capture

### Requirement: Empty input and capture output are deliberate

Apply with a valid empty rules array SHALL succeed, reporting zero rules and no changes without Dataverse access; supplied mappings SHALL still be locally validated. Init with zero selected rules SHALL write a valid empty configuration. Existing output SHALL fail init unless `--overwrite` is supplied. Capture validation SHALL finish before output writes. Write failures SHALL be reported without implying rollback.

#### Scenario: Empty apply
- **WHEN** apply receives an empty rules array
- **THEN** it succeeds with zero rules and no changes without authentication or target lookups

#### Scenario: Empty apply with invalid supplied mapping
- **WHEN** apply receives an empty rules array and an explicitly supplied missing or invalid mapping
- **THEN** local validation fails without authentication

#### Scenario: Empty capture
- **WHEN** init selects no rules
- **THEN** it succeeds with an empty rules array subject to normal output checks

#### Scenario: Capture failure with overwrite
- **WHEN** overwrite is allowed but capture validation fails
- **THEN** existing output remains unchanged

### Requirement: Duplicate-rule application is prevalidated

Application SHALL resolve all configured rule and owner references and validate requested operations before any write. Missing or ambiguous references and unsupported owner assignments SHALL fail preflight. Dry-run SHALL report the planned changes without writing.

#### Scenario: Invalid reference in a batch
- **WHEN** one configured rule or owner cannot be resolved uniquely
- **THEN** no rule in the batch is changed

#### Scenario: Dry-run
- **WHEN** a valid batch is evaluated in dry-run mode
- **THEN** planned publication and ownership changes are reported without writes

#### Scenario: Listed rule is already publishing
- **WHEN** a configured rule is Publishing or has an unknown/inconsistent state
- **THEN** preflight fails without any writes rather than adopting its running operation

### Requirement: Ownership uses shared target resolution

Configured owners SHALL use the shared logical-owner and environment-mapping contract rather than resource-specific lookup heuristics. Duplicate-rule application SHALL validate resolved principal kinds against its own platform constraints before writes. Shared support for a principal kind SHALL NOT imply that duplicate rules accept that kind.

#### Scenario: Resolved owner is not allowed
- **WHEN** the shared resolver identifies a principal whose kind is unsupported for duplicate-rule ownership
- **THEN** preflight fails without any rule writes

### Requirement: Owner changes preserve publication without implicit transitions

Application SHALL support direct assignment to regular users, application users, and owning teams without implicit unpublish/republish. It SHALL reject disabled users and new owners lacking the duplicate-rule read privilege for actual owner changes, but permit capture and already satisfied owner intent. Explicit unpublish SHALL complete before assignment; assignment SHALL complete before dependent publish. Failed assignment SHALL skip that publish without changing rule definitions.

#### Scenario: Owner-only published rule
- **WHEN** ownership alone changes successfully on a published rule
- **THEN** it remains published without publish/unpublish requests

#### Scenario: Disabled new owner
- **WHEN** an actual owner change targets a disabled user
- **THEN** preflight fails without any rule writes

#### Scenario: New owner lacks read privilege
- **WHEN** an actual owner change targets a user or owning team without `prvReadDuplicateRule`
- **THEN** preflight fails without any rule writes

#### Scenario: Existing disabled owner is already desired
- **WHEN** desired ownership already matches a disabled user
- **THEN** that satisfied effect requires no assignment and does not fail solely due to disabled status

#### Scenario: Assignment fails before publication
- **WHEN** a rule requests both a new owner and publication but assignment fails
- **THEN** its publish is not submitted and independent rules may continue

### Requirement: Publication capacity is checked without eviction

For base tables receiving new publishes, preflight SHALL count all Published and Publishing rules, including unlisted rules, project explicit changes, and reject occupancy above five before writes. Execution SHALL treat a slot as freed only after confirmed unpublish. A publish rejected at runtime by the platform's capacity limit SHALL be reported as failed without queuing or retrying it. Application SHALL NOT change unlisted rules to make room.

#### Scenario: End state exceeds limit
- **WHEN** requested publishes would exceed five occupied slots after requested unpublishes
- **THEN** preflight fails without writes

#### Scenario: Unpublish does not finish
- **WHEN** an expected slot release fails or remains unconfirmed
- **THEN** its slot remains occupied and a publish needing that slot is reported failed/skipped

#### Scenario: Capacity changed concurrently
- **WHEN** the platform rejects a publish because the base table reached its published-rule limit after preflight
- **THEN** that publish is reported as failed for lack of capacity and independent rules continue

### Requirement: Requests are executed in ordered phases

Application SHALL execute explicit unpublishes sequentially and confirm each by read-back before remaining owner changes and publishes. It SHALL submit publishes sequentially without awaiting each job separately, then observe accepted publishes together. Independent item failures SHALL NOT halt all submissions. Pending publishes SHALL use a shared deadline, not a new deadline per rule.

#### Scenario: Several rules need publication
- **WHEN** several publishes are valid and capacity permits
- **THEN** their requests are submitted without waiting for each preceding job to finish
- **AND** their accepted jobs are subsequently observed together

#### Scenario: Rule replacement within capacity
- **WHEN** one explicit unpublish is needed to free a slot for another rule
- **THEN** the unpublish is confirmed before the new publish is submitted

### Requirement: Wait mode confirms completion within a shared publish budget

The publish observation phase SHALL default to 300 seconds, overridden by `--timeout`, starting after the publish submission loop. Pending rules SHALL be observed together on a two-second cadence. Normal mode SHALL confirm final target state, retain returned job IDs, and report failed or canceled jobs as terminal failures. Timeout SHALL fail unresolved entries with last observed state and partial effects, without resubmitting mutations.

#### Scenario: One job fails and another succeeds
- **WHEN** one submitted publish fails while another completes
- **THEN** both outcomes are reported and the overall command fails

#### Scenario: Shared deadline
- **WHEN** several pending rules reach the publish deadline
- **THEN** unresolved rules fail without each receiving a new timeout window

#### Scenario: Accepted request is not completion
- **WHEN** a publish request is accepted but Published has not been confirmed
- **THEN** normal mode does not report that rule as successfully published

### Requirement: No-wait reports accepted publishes without completion claims

`--no-wait` SHALL skip only final publish observation, not explicit unpublish confirmation or owner changes. Accepted publishes SHALL count as successful submissions and include returned job IDs without claiming Published state. Observed submission/earlier-phase failures SHALL still fail the command. The command SHALL explicitly state that later server-side outcomes are unverified.

#### Scenario: Submission-only success
- **WHEN** every needed publish request is accepted in no-wait mode and all earlier effects succeed
- **THEN** the command succeeds with submitted/unverified outcomes rather than Published claims

#### Scenario: No-wait requires slot release
- **WHEN** no-wait execution needs an explicit unpublish to release capacity
- **THEN** it still confirms the unpublish by read-back before submitting the publish

### Requirement: Cancellation and unknown results do not imply rollback

Cancellation SHALL stop further client work rather than be treated as an ordinary item failure. Timeout or cancellation SHALL NOT imply a server job was cancelled or completed changes rolled back. Unknown mutation outcomes SHALL NOT release slots or trigger automatic resubmission. Diagnostics SHALL identify rule, phase, returned job ID where available, and known partial effects.

#### Scenario: Cancel after publish submission
- **WHEN** cancellation occurs after a publish job was accepted
- **THEN** client work stops and reports that the server job may still run

### Requirement: Duplicate-rule partial failures are observable

After preflight succeeds, application SHALL continue independent rules after a final runtime failure. It SHALL report completed and failed operations and exit unsuccessfully if any requested operation fails, without promising atomic rollback.

#### Scenario: Publication fails for one rule
- **WHEN** publication fails for one rule but an unrelated rule can be processed
- **THEN** the unrelated rule is processed
- **AND** the result identifies the failure and any completed changes
- **AND** the command exits with the failure code required by command-execution
