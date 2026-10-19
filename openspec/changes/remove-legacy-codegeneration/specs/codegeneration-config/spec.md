# Spec Delta

## REMOVED Requirements

### Requirement: Legacy V1 configurations remain readable

**Reason**: dgtp 3 removes the V1 execution and compatibility paths.

**Migration**: Convert the configuration to an explicitly versioned typed config; split combined .NET and TypeScript configs into separate invocations. Full TypeScript consumers must adapt to the supported output, and attribute filters have no replacement.

## MODIFIED Requirements

### Requirement: V2 configurations select one output target

A V2 configuration MUST set `version` to `2` and MUST select exactly one supported `type`: `dotnet` or `typescript`. Shared entity, request, and option-set scope MUST be resolved independently from target-specific output settings.

#### Scenario: A supported V2 configuration is loaded

- **WHEN** a V2 configuration declares `type` as `dotnet` or `typescript`
- **THEN** it is deserialized into the matching typed configuration model

#### Scenario: V2 target is missing or unsupported

- **WHEN** a V2 configuration has no string `type` or declares another value
- **THEN** configuration resolution fails with an actionable error

### Requirement: Unsupported configuration versions fail explicitly

Configuration loading MUST require an explicit integer `version` of `2`. Missing, malformed, legacy, and unsupported versions MUST fail before authentication, metadata retrieval, or output changes. Errors MUST identify the supported format; legacy and missing-version errors MUST direct users to the 2.x-to-3.x migration guide.

#### Scenario: An unknown version is loaded

- **WHEN** a configuration declares an unsupported integer version
- **THEN** resolution fails and identifies version 2 as the supported format

#### Scenario: A V1 configuration is loaded

- **WHEN** a configuration declares `version` equal to `1`
- **THEN** resolution fails with migration guidance instead of adapting or executing the configuration
- **AND** no Dataverse authentication or output changes occur

#### Scenario: A version is omitted

- **WHEN** a configuration omits `version`, even if it has a valid `type`
- **THEN** resolution fails with migration guidance rather than inferring a format

#### Scenario: A version has an invalid representation

- **WHEN** `version` is null, a string, a boolean, an object, an array, or a non-integer number
- **THEN** resolution fails with an actionable version-validation error

## ADDED Requirements

### Requirement: Schemas require the runtime configuration contract

Supported target schemas MUST require explicit `version` and `type` fields and MUST describe the supported target settings and defaults. `$schema` annotations MUST remain usable. Retired V1 schemas MUST NOT be presented as supported by dgtp 3.

#### Scenario: A config lacks a required discriminator

- **WHEN** a config omits `version` or `type`
- **THEN** both runtime loading and validation against the supported target schema reject it

#### Scenario: A documented config is validated

- **WHEN** a current README example is loaded and validated against its declared schema
- **THEN** both accept its supported version, target, and settings
