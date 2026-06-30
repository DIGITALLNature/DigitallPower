# Research: V2 TypeScript Config Caveats

## Resolved by the redesign

The original V2 TypeScript gaps around flat `include` growth and top-level form/test-helper settings were addressed:

- `xrmMockFormHelpers` moved into `output.forms.testHelpers`
- `onlyFormsFromSolutions` moved into `output.forms.fromSolutions`
- `customApis` moved into `output.customApis`
- entity selection moved into shared `entities: { names, fromSolutions, mask }`
- SDK message constants are now implied by non-empty `requests`

## Remaining caveats

### 1. `TypingPath` is still V1-only
V2 still does not expose the legacy `TypingPath` override. TypeScript template reference paths remain fixed.

### 2. Per-entity allowlists are still V1-only
V1 `EntityFilters`, `EntityRefFilters`, and `EntityFormFilters` still have no V2 equivalent.

### 3. `forms.filter` remains string-encoded
Entries still use the implicit `entity.formname.form` format. This is compact but not strongly structured.
