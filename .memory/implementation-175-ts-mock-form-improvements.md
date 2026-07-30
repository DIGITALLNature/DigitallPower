# Implementation Plan: Issue #175 — TS Mock Form Improvements

## Context

Issue: https://github.com/DIGITALLNature/DigitallPower/issues/175
Scope: Generated `*.mock.form.ts` files and the runtime TypeScript mock helpers in `src/modules/dgt.power.codegeneration/Templates/tsl/`.

## Recommendation

**Implement all six suggestions.** They are additive, do not break existing consumers, and significantly reduce boilerplate in Jest tests that use `XrmMockFormTestContextBuilder`.

## Affected Files

| File | Purpose |
|------|---------|
| `src/modules/dgt.power.codegeneration/Templates/tsl/EntityFormTestHelper.liquid` | Generates `*.mock.form.ts` files. |
| `src/modules/dgt.power.codegeneration/Templates/tsl/xrm_mock_form_test_context_types.ts` | Shared types, including `XrmFormMockServerData` and `IXrmMockFormTestContextBuilder`. |
| `src/modules/dgt.power.codegeneration/Templates/tsl/xrm_mock_form_test_context_builder.ts` | `XrmMockFormTestContextBuilder` implementation. |
| `src/modules/dgt.power.codegeneration/Templates/tsl/xrm_mock_form_odata_filter.ts` | OData filter logic for `retrieveMultipleRecords`. |
| `tests/dgt.power.codegeneration.tests/TslTemplateContractTests.cs` | Template rendering smoke tests. |

## Changes

### 1. Factory function per mock form

In `EntityFormTestHelper.liquid`, add an exported `createBuilder` function at the end of the namespace:

```typescript
export interface CreateBuilderOptions {
    languageId?: number;
}

export function createBuilder(options?: CreateBuilderOptions): XrmForm.Tester.XrmMockFormTestContextBuilder<
    {{FormClassNamespace}}.TabName,
    {{FormClassNamespace}}.SectionNames,
    {{FormClassNamespace}}.ControlName,
    {{FormClassNamespace}}.AttributeName
> {
    const builder = new XrmMockFormTestContextBuilder(
        [...FormControlTypesConfig],
        [...FormAttributeTypesConfig],
        [...TabSectionControlsConfig]
    );

    if (options?.languageId !== undefined) {
        builder.withLanguageId(options.languageId);
    }

    return builder;
}
```

Use conditional type arguments for `TabName` / `SectionNames` when tabs/sections are absent (fall back to `string`), matching the existing config types.

### 2. Relax server mock types

Change `XrmFormMockServerData` in `xrm_mock_form_test_context_types.ts`:

```typescript
export type XrmFormMockServerData = Record<string, unknown[]>;
```

Update `IXrmMockFormTestContextBuilder.withServerData` signature accordingly. This removes the need for `as unknown as XrmTable.DTO.Table<string>[]` casts when using concrete generated DTO types.

### 3. `retrieveMultipleRecords` without `$select` returns full records

In `xrm_mock_form_odata_filter.ts`, change `executeRetrieveMultipleRecord` so that it only applies `$select` when at least one select field is present:

```typescript
const selectFields = XrmMockFormODataFilter.getSelectFieldNamesFromAstObject(astOptions);
// only restrict to id when an explicit $select was provided
if (selectFields.length === 0) {
    return filteredItems; // or list when no filter
}
selectFields.push(`${entityLogicalName}id`);
return filteredItems.map((x) => XrmMockFormODataFilter.selectProperties(x, selectFields));
```

This aligns the mock behavior with the real Web API, where omitting `$select` returns all columns.

### 4. Re-export FormContext types

In `EntityFormTestHelper.liquid`, add:

```typescript
export type FormContext = {{FormClassNamespace}}.FormContext;
export type ControlName = {{FormClassNamespace}}.ControlName;
export type AttributeName = {{FormClassNamespace}}.AttributeName;
export type TabName = {{FormClassNamespace}}.TabName;
export type SectionNames = {{FormClassNamespace}}.SectionNames;
```

Only emit `TabName` / `SectionNames` if the form actually has tabs/sections to keep the type resolvable.

### 5. Helper for selected SubGrid rows

Add `withSelectedSubGridRows` to `XrmMockFormTestContextBuilder` and to the `IXrmMockFormTestContextBuilder` interface:

```typescript
public withSelectedSubGridRows(name: TControlName, selectedIds: string[]): this {
    return this.withSubGridMockRows<string>({
        entityName: /* infer from control config if possible, else require parameter */,
        subGridRowsAttributeConfig: [],
        gridRows: selectedIds.map((id) => ({ entityId: id, subGridRowsAttributes: [] })),
        selectedRowsIndexes: selectedIds.map((_, i) => i),
    });
}
```

The entity name can be taken from the existing control config where `type === "subgrid"` and `attributeName` / `name` map to the related entity. If the mapping is not available at runtime, add the entity name as a required parameter (`withSelectedSubGridRows(name, entityName, selectedIds)`).

### 6. `languageId` in factory options

Covered by the `CreateBuilderOptions` interface in change #1. The factory calls `builder.withLanguageId(languageId)` when provided.

## Testing

1. Update `TslTemplateContractTests.cs` expected fragments for `EntityFormTestHelper.liquid` to assert presence of:
   - `export function createBuilder`
   - `export type FormContext`
   - `languageId`
2. Add unit tests for `XrmMockFormODataFilter.executeRetrieveMultipleRecord` verifying:
   - no `$select` returns all properties,
   - `$select` still restricts fields and always includes the entity id.
3. Add builder tests for `withSelectedSubGridRows` and `createBuilder` options.

## Risks

- **Type loosening (`Record<string, unknown[]>`):** Slightly weaker type safety, but the current type already forces casts; this is an ergonomic win.
- **Behavior change in `$select`:** Real Web API returns all fields when `$select` is absent, so this is a correctness fix, not a breaking change.
- **Template type conditionals:** Must keep `TabName` / `SectionNames` fallbacks consistent with the generated config types to avoid TypeScript errors in generated files.

## Post-Implementation Review

A subagent verification pass identified one critical fix that was applied:

- **`withSelectedSubGridRows` now sets `this.isSubGridMethodsMock = true`** before delegating to `withSubGridMockRows`. Without this, `buildMockFormContext()` skips `InitSubGridConfigMockRows()` and selected rows are not initialized.

Other findings from the review were pre-existing code-quality issues (e.g. `any` casts in Web API mocks, `==` in `XrmMockFormODataFilter`, import types) and were **not** introduced by Issue #175; they remain unchanged to keep the PR focused.

## Commit Plan

Suggested commits:

```
feat(codegen): add createBuilder factory and type re-exports to mock form helpers
feat(mock): relax XrmFormMockServerData type to Record<string, unknown[]>
fix(mock): return all fields in retrieveMultipleRecords when no $select is given
feat(mock): add withSelectedSubGridRows helper
```

## Status

Implemented and verified:
- `dotnet build src/modules/dgt.power.codegeneration/dgt.power.codegeneration.csproj` succeeds (0 errors)
- `dotnet test --project tests/dgt.power.codegeneration.tests/dgt.power.codegeneration.tests.csproj` passes (57 tests)

