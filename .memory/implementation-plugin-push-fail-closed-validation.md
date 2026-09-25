# Plugin Push Fail-Closed Validation

`plugin push` must not reconcile Dataverse state from incomplete local assembly metadata. When
`MetadataLoadContext` cannot load every CLR type, `AssemblyReflectionReader` aborts parsing and
returns no deployment target through its existing command error path.

Plugin package solution membership is similarly fail-closed: a package push with `--solution`
requires the environment to expose a `pluginpackage` solution component definition. Planning
throws a clear error rather than silently omitting the requested membership.

Step validation rejects every `Create` pre-image, including post-operation registrations, because
the record does not exist before a Create operation. The `plugin push` package help example
includes its required `--publisher-prefix` option.
