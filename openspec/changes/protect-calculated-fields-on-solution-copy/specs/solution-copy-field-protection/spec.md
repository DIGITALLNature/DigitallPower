# Spec Delta

## Purpose

Protect calculation-bearing columns intentionally included in a solution copy as a managed-deployment policy, with bounded metadata changes and explicit failure guarantees.

## ADDED Requirements

### Requirement: Best-practice copying protects calculation-bearing columns

In best-practice mode, `solution copy-components` SHALL set eligible unmanaged columns' `IsCustomizable.Value` to false. Eligible calculation types SHALL be Calculated, Rollup, and Power Fx Formula (`SourceType` 1, 2, and 3). Protection SHALL apply to the whole column's customization settings, not only its formula, without changing the calculation definition.

#### Scenario: Each supported calculation type is protected
- **WHEN** a best-practice copy succeeds and includes an eligible unprotected column of any supported type
- **THEN** the command sets its customization property to false and verifies that result
- **AND** it leaves its formula, type, and unrelated column settings unchanged

#### Scenario: Protection changes shared metadata
- **WHEN** the command presents a protection plan
- **THEN** it explains that updates affect shared metadata in the current environment, including other solutions containing those columns
- **AND** the restriction takes effect after managed deployment, not as an unmanaged-development or data-access lock

### Requirement: Protection follows the final intentional copy scope

Protection SHALL cover explicit included attribute components and eligible attributes of tables copied completely by the final plan. It SHALL NOT cover managed columns, simple or prompt columns, excluded components, unrelated existing target membership, or fields included solely through automatic app/dependency expansion. Each eligible column SHALL be considered once.

#### Scenario: Complete table expands the protection scope
- **WHEN** the final plan copies a table completely
- **THEN** its eligible columns are considered even without separate source attribute membership rows

#### Scenario: Skeleton tables do not expand protection
- **WHEN** a table is copied without subcomponents
- **THEN** only its independently included attribute components can enter the protection scope

#### Scenario: Multiple sources select the same column
- **WHEN** an eligible column is selected explicitly and through one or more complete tables
- **THEN** it appears once in the protection plan and receives at most one protection update

#### Scenario: Copying expands an app automatically
- **WHEN** Dataverse adds fields not intentionally included by the final copy plan
- **THEN** those additions alone do not make the fields protection candidates

#### Scenario: Target contains unrelated columns
- **WHEN** an eligible column exists only through unrelated target membership
- **THEN** the command does not change its metadata

#### Scenario: A managed calculation column has an active customization
- **WHEN** the copy plan includes that managed column
- **THEN** the protection policy does not modify it despite its active customization layer

### Requirement: Technical protection exceptions are explicit

The command SHALL exclude technical companion fields from protection. Basis-currency companion fields and `bpf_duration` SHALL remain explicit exceptions. The plan SHALL identify skipped in-scope exceptions and their reasons; it SHALL NOT silently classify unknown metadata as an exception.

#### Scenario: A complete table contains a technical companion
- **WHEN** protection candidates include a basis-currency companion or `bpf_duration`
- **THEN** the plan reports the exception and the command does not update that column

#### Scenario: Metadata cannot establish eligibility
- **WHEN** the command cannot establish a selected column's calculation type, managed state, or required protection metadata
- **THEN** preflight fails with a diagnostic rather than silently omitting a possible protection requirement

### Requirement: Raw mode bypasses field protection

`--raw` SHALL disable protection planning and mutations, including protection-specific metadata reads. Existing raw copy selection and app-handling behavior SHALL remain unchanged.

#### Scenario: Raw mode encounters unavailable protection metadata
- **WHEN** the user copies with `--raw` and protection-only metadata would be unavailable
- **THEN** no protection-only request or update is made
- **AND** copy success depends only on the existing raw operation

### Requirement: Full protection preflight precedes all mutations

The command SHALL resolve the entire protection plan before any copy, app-cleanup, or protection mutation. Already protected columns SHALL be no-ops. An unprotected eligible column whose property cannot be changed, missing required metadata, or a failed preparation read SHALL fail preflight with no mutations. Preflight SHALL NOT be advertised as complete permission or runtime-success validation.

#### Scenario: A later candidate cannot be protected
- **WHEN** an earlier candidate is valid but a later unprotected candidate has `CanBeChanged` false
- **THEN** the command fails before adding, removing, or updating any component or metadata
- **AND** it identifies the blocking column

#### Scenario: Protection is already satisfied
- **WHEN** an eligible column has `IsCustomizable.Value` false
- **THEN** the command reports unchanged protection without an update, even if `CanBeChanged` is false

#### Scenario: No columns require protection
- **WHEN** the final copy plan contains no eligible protection changes
- **THEN** the existing copy operation proceeds without protection updates

### Requirement: Dry-run previews copying and protection without writes

`--dry-run` SHALL perform the same copy and protection preparation as an actual run in the selected mode. Output SHALL identify each candidate's table, logical column name, calculation type, and planned change, unchanged status, or exception reason. Dry-run SHALL make no Dataverse mutations.

#### Scenario: Best-practice preview contains protection changes
- **WHEN** the user previews a valid plan with unprotected eligible columns
- **THEN** output lists their intended protection and shared-metadata consequences
- **AND** no add, remove, metadata update, or publish operation is submitted

#### Scenario: Preview preparation fails
- **WHEN** a required metadata read or eligibility check fails during dry-run
- **THEN** the command fails with the same preparation diagnostic as an actual run and makes no mutations

#### Scenario: Raw preview
- **WHEN** `--raw` and `--dry-run` are combined
- **THEN** only the existing raw copy plan is prepared and displayed without protection-specific reads or writes

### Requirement: Protection follows successful copying and fails explicitly

Protection SHALL begin only after all copy and app-cleanup operations succeed. Protection updates SHALL be sequential, read back after each update, and stop at the first failure, mismatch, or cancellation. The command SHALL return failure for an unsuccessful run and report partial effects without claiming atomicity or rollback.

#### Scenario: Copying or app cleanup fails
- **WHEN** any copy or app-cleanup step fails
- **THEN** no protection update begins
- **AND** the command reports the failure and any preceding copy effects

#### Scenario: A protection update fails
- **WHEN** copying succeeds but a protection update fails or its read-back is not false
- **THEN** no later protection update is submitted and the command fails
- **AND** output distinguishes copied components, confirmed protected columns, and failed or unconfirmed protection

#### Scenario: Read-back fails after an accepted update
- **WHEN** the update completes but confirmation cannot be retrieved
- **THEN** the command identifies the column as unconfirmed rather than unchanged or rolled back
- **AND** it stops further protection work

#### Scenario: Execution is cancelled
- **WHEN** cancellation occurs after some mutations have completed
- **THEN** no further client mutations are submitted
- **AND** cancellation reporting preserves known completed and unconfirmed effects without claiming server-side rollback

#### Scenario: An unchanged plan is reapplied
- **WHEN** all eligible columns already have protection false and copying succeeds
- **THEN** no protection updates are submitted

### Requirement: Help and migration guidance disclose the policy

CLI help, README, and major-version migration guidance SHALL describe default protection for all three types, whole-column and shared-environment effects, bounded scope, `--raw` bypass, dry-run behavior, and non-atomic execution. They SHALL NOT claim formula-only locking, protection in unmanaged development, or complete rollback.

#### Scenario: Existing automation upgrades
- **WHEN** users review copy-component help and the major-version migration guide
- **THEN** they can identify the new default metadata mutation and choose `--raw` to bypass it
- **AND** guidance explains that `--raw` also disables existing best-practice selection rules, not just protection
