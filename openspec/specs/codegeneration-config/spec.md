# Code Generation Configuration

## Purpose

Define versioned configuration resolution for .NET and TypeScript Dataverse code generation.

## Requirements

### Requirement: Legacy V1 configurations remain readable

Configurations with no `version` or with `version` equal to `1` MUST be read using the legacy configuration model and MUST produce a migration warning.

#### Scenario: A legacy configuration is loaded

- **WHEN** a configuration omits `version` or sets it to `1`
- **THEN** the V1 result is returned and a deprecation warning is shown

### Requirement: V2 configurations select one output target

A V2 configuration MUST set `version` to `2` and MUST select exactly one supported `type`: `dotnet` or `typescript`. Shared entity, request, and option-set scope MUST be resolved independently from target-specific output settings.

#### Scenario: A supported V2 configuration is loaded

- **WHEN** a V2 configuration declares `type` as `dotnet` or `typescript`
- **THEN** it is deserialized into the matching typed configuration model

#### Scenario: V2 target is missing or unsupported

- **WHEN** a V2 configuration has no string `type` or declares another value
- **THEN** configuration resolution fails with an actionable error

### Requirement: Unsupported configuration versions fail explicitly

The configuration factory MUST reject versions other than `1` and `2` instead of silently treating them as a supported format.

#### Scenario: An unknown version is loaded

- **WHEN** a configuration declares an unsupported version
- **THEN** resolution fails and identifies the supported versions