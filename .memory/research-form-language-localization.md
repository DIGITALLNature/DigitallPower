# Research: Form/label language localization in codegeneration (TypeScript)

## Context

`dgt.power.codegeneration`'s TypeScript strategies (`TypescriptLightGenerationStrategy`,
`TypescriptFullGenerationStrategy`) generate form typings and option-set label constants for
Dataverse entities. Both V1 (`CodeGenerationConfig.UseBaseLanguage`) and V2
(`TypeScriptCodeGenerationConfig.Language`) configs let users pin the LCID used for label
resolution, or fall back to the org's base language.

## Two distinct localization mechanisms in Dataverse — do not confuse them

1. **Metadata `Label`/`LocalizedLabels`** (attribute display names, option set option labels,
   entity display names). These genuinely support per-request LCID resolution:
   `Formatter.GetLocalizedLabel(Label, useBaseLanguage, lcid)` and the Fluid filter
   `CustomLiquidFilters.Localize` (`{{ someLabel | localize: languageCode }}`) both call
   `label.GetLocalizedLabel(lcid)`, correctly honoring the configured language. This mechanism
   works and needs no connection/session tricks.

2. **Record data attributes on out-of-box (OOB) records** (e.g. `systemform.name`, likely also
   sitemap/ribbon/process names). These are **not** Label-backed. Dataverse resolves the returned
   string based on the **connecting user's personal UI language** (`usersettings.uilanguageid`),
   NOT any per-request parameter, and NOT the org base language directly. There is no supported way
   to override this per-`RetrieveMultiple`-call in the SDK for .NET — no `LanguageCode` parameter
   exists on `RetrieveMultipleRequest`/`QueryExpression`.

## Consequence / bug found

`FormParser`/`MetadataService.ParseSystemFormsCollectionToFormDetails` builds the internal form key
(`FormDetail.FormUniqueName`, used both for the generated file name and for matching `config.Forms`
include/exclude entries) directly from `systemform.name`. If the connecting user's UI language
differs from the language configured for code generation (`language`/`UseBaseLanguage`), the
retrieved form name is in a *different* language than what the user wrote in `config.Forms`,
causing matching to silently fail and forms to be skipped ("fall through the filtering").

## Considered fixes and why they were rejected

- **Impersonate/mutate `usersettings.uilanguageid` before retrieval, restore after** — the only
  known mechanism that would make `systemform.name` actually return in the target language. Rejected
  by the user: too invasive (mutates a real connected user's account state, even if restored),
  and could interfere with concurrent usage in shared/dev environments.
- **Use `systemform.uniquename` instead of `name` for the stable/language-independent key** —
  `uniquename` is documented to be language-independent, but was found to be unreliably populated
  in practice (many system forms have it empty). Rejected.
- **Allow `config.Forms` entries to be a FormId GUID as an alternate match key** — technically
  solid but not pursued; user preferred documenting the limitation instead.

## Fix implemented (final)

Treated as a **known, documented Dataverse platform limitation** rather than something to silently
work around:

- Added `IMetadataService.RetrieveConnectionUserLanguage()` — read-only, uses `WhoAmIRequest` +
  retrieves `usersettings` for that user's `uilanguageid` (falls back to org base language if null).
- Both TypeScript strategies now compare this session language against the configured language
  right before generating forms, and print `Warnings.FormNameLanguageMismatch(...)` via
  `Console.MarkupLine` when they differ.
- Documented in `README.md` under `codegeneration` → "Known limitations", with the mitigation
  (set the connecting user's personal Dataverse UI language to match the configured `language`).

## Separate, already-fixed bug (root cause of the *original* report)

Independent of the above: `FormViewModel` had no `LanguageCode` property at all, so the
`{{ option.Label | localize: LanguageCode }}` filter call in `EntityFormTestHelper.liquid` always
resolved `LanguageCode` to `null`/default, silently ignoring the configured language for option
labels rendered in form test helpers. Fixed by adding `FormViewModel.LanguageCode` and threading the
strategy-computed `languageCode` into it (`TypescriptLightGenerationStrategy.CreateFormFileContent`).
The "Full"/V2-old generator (`TypescriptFullGenerationStrategy.GenerateFormDetailFile`) had an
analogous bug: it hardcoded `metadataService.RetrieveOrganizationLanguage()` for
`TsLiquidTemplateModelFactory.CreateEntityFormModel(...)`'s `systemLanguage` parameter, ignoring
`config.UseBaseLanguage`/override — fixed by passing through the already-computed `languageCode`.
