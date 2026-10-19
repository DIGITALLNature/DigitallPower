# Spec Delta

## Purpose

Define the supported Dataverse .NET and TypeScript generation outputs independently of retired compatibility modes.

## MODIFIED Requirements

### Requirement: Each target has one generation mode

Generation MUST select .NET or TypeScript through the typed configuration without a Full/Light mode selector. TypeScript Full runtime models and attribute-level allowlists MUST NOT be supported.

#### Scenario: Generate TypeScript without a mode selector

- **WHEN** a supported TypeScript configuration is executed
- **THEN** the existing typed-config TypeScript output is generated without selecting Full or Light
- **AND** no legacy Full runtime entity or form-context wrapper model is emitted

#### Scenario: Legacy output controls are supplied

- **WHEN** a configuration supplies `TypescriptGeneratorVersion`, `EntityFilters`, `EntityRefFilters`, or `EntityFormFilters`
- **THEN** generation fails with an actionable unsupported-setting error instead of silently ignoring the setting
- **AND** attribute-level filters are identified as removed without replacement

### Requirement: Supported .NET output is preserved

The .NET target MUST preserve the existing typed-config generation behavior for Modern and Framework output, including configured entities, context, options, logical names, relationships, request wrappers, message constants, and optional metadata artifacts.

#### Scenario: Generate either supported .NET target

- **WHEN** a supported .NET configuration selects Modern or Framework output
- **THEN** its artifacts retain the existing target-specific syntax, names, paths, and configured inclusion behavior

### Requirement: Supported TypeScript output is preserved

The TypeScript target MUST preserve the existing typed-config behavior for entity declarations, form typings, option sets, SDK message names, parameterized request wrappers, and optional form test helpers. Existing localization, naming, deterministic rendering, and template diagnostics MUST remain intact.

#### Scenario: Generate scoped TypeScript artifacts

- **WHEN** a supported TypeScript configuration selects entities, forms, option sets, and requests
- **THEN** generated artifacts retain the existing typed-config names, paths, and contents for the same metadata and settings

#### Scenario: Optional TypeScript outputs are controlled

- **WHEN** form test helpers are enabled or parameterized request wrapper generation is disabled
- **THEN** those settings retain their existing effects without introducing a separate generation mode
