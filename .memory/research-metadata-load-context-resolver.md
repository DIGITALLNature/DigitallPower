# MetadataLoadContext Resolver Precedence

Standalone DLL and package parsing need the target directory in the `MetadataLoadContext` resolver
to load sibling registration and SDK dependencies. The module directory can contain copies of
those DLLs, however; passing both copies to `PathAssemblyResolver` can cause
`MetadataLoadContext` to report that an assembly has already been loaded.

`MetadataLoadContextFactory` constructs resolver paths in this precedence order:

1. .NET runtime directory;
2. target DLL/package extraction directory; and
3. plugin module directory.

It retains only the first path for each DLL filename, so target dependencies take precedence over
module copies while runtime assemblies remain canonical.
