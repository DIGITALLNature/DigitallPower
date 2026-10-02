# SDK Message Filter Secondary Entity Resolution

For entity-scoped SDK messages, `SecondaryEntityName` values of `null`, empty, or `none` mean
the target `sdkmessagefilter.secondaryobjecttypecode` must be either null or the literal `none`
sentinel. Dataverse environments use both representations. Omitting this predicate can select an
arbitrary filter for the same message and primary entity that is scoped to a different secondary
entity.

`SdkMessageRepository.ResolveAsync` therefore always constrains the secondary object type:

- an `OR` filter for `ConditionOperator.Null` or literal `none` for unspecified/`none` secondary
  entities;
- `ConditionOperator.Equal` for a declared secondary entity.

Global messages remain a separate path and do not resolve an SDK message filter.
