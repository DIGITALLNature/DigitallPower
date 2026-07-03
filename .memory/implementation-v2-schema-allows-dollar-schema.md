# V2 config schemas allow `$schema`

## Context

Editors commonly insert a top-level `$schema` property into JSON config files. With `additionalProperties: false`, this caused validation errors in V2 codegeneration config schemas.

## Implementation

Added top-level `$schema` to both V2 codegeneration schemas:
- `schemas/codegeneration/v2/dotnet.schema.json`
- `schemas/codegeneration/v2/typescript.schema.json`

Property shape:
- type: `string`
- description: `A field for the JSON schema specification: https://json-schema.org/`

## Notes

This is compatibility-focused and does not change generator behavior.
