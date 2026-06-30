# Implementation: V2 Codegeneration Config Shape

## Summary

The V2 codegeneration config now uses a symmetric nested shape for both targets:

- shared root: `version`, `type`, `namespace`, `language`, `entities`, `requests`, `optionSets`
- TypeScript-specific `output`: `forms`, `customApis`
- .NET-specific `output`: `target`, `virtual`, `editableReadOnly`, `include`

## Key implementation details

- `EntityScopeConfig` replaces the flat `entities`/`solutions`/`entityMask` trio for V2.
- `Requests` remains the single generation trigger for SDK message constants across both generators.
- TypeScript no longer uses a separate V2 toggle for SDK message constants; non-empty `requests` always generates them.
- `output.forms` being omitted means "generate all forms". An empty `output.forms.filter` also means "all forms".
- V1 `CodeGenerationConfig` is preserved as the legacy path and maps into the new V2 runtime shape via `ToDotNetConfig()` and `ToTypeScriptConfig()`.
- `MetadataService.PopulateEntitiesAndSolutions(CodeGenerationConfigBase)` mutates `config.Entities.Names` after expanding `fromSolutions` and `mask`.

## Remaining caveats

- TypeScript V2 still does not expose V1-only `TypingPath` or per-entity filter collections.
- `forms.filter` is still a string-based allowlist using `entity.formname.form` entries.
