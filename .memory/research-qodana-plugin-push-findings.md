# Qodana findings in the plugin push module

Qodana's new findings for the resource-oriented plugin push change included analyzer
cleanup in both production and test code:

- Flagged-enum zero values should use `None` rather than `Undefined`.
- Custom exceptions need the standard parameterless, message, and inner-exception
  constructor forms.
- Test helpers that transfer `TestConsole` ownership need an explicit CA2000
  suppression explaining that ownership transfer.
- Repository interfaces should avoid namespace imports used only by XML
  documentation; fully qualify those documentation references instead.
- Test fixture helpers should suppress S107 when their parameters intentionally map
  one-to-one to the model under test.

The remaining dependency vulnerability warnings are existing package advisories,
not new Qodana findings from the plugin push change.
