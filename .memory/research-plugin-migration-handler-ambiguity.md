# Migration-Only Provider Handler Resolution

Plugin package deployment carries handler targets by CLR `TypeName` and resolves them through a package-wide type-ID map. A provider handler retained only during an assembly migration therefore requires its old type name to identify exactly one replacement plugin type. Duplicate replacement names are ambiguous even when the package has no current provider declarations; reject them while planning, before provider writes. The planner test should verify the specific diagnostic and that the provider repository receives no writes.
