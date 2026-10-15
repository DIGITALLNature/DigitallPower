# Technical Debt & Backlog

## Paging: `plugin step enable|disable --solution`

`SolutionComponentRepository.ListComponentIdsAsync` and
`SdkMessageProcessingStepRepository.ListStatesAsync` issue a single `RetrieveMultipleAsync` call each and do
not follow `MoreRecords`/`PagingCookie`. For solutions with more than one Dataverse page of steps the
state change silently covers only the first page.

Adopt the reusable paging helper from the plugin push rewrite once it is merged, and add a test with
more than one page of results.
