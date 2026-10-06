# Custom Data Provider Deployment

## Registration contract and identity

`plugin push` reads three-argument `CustomDataProviderRegistrationAttribute` declarations in
`Digitall.Plugins.Registration` through `CustomAttributeData`, without referencing the registration
package at runtime. The supported registration-library minimum is 3.0.0, which introduces this
breaking contract. The constructor requires `dataSourceSchemaName`, `eventRegistration`, and
`providerName` on every declaration; optional metadata remains in named properties.
The reader resolves these exact constructor parameter names through `CustomAttributeData`, not
attribute instances or read-only property getters. Required-value validation rejects both the old
two-argument entity-name constructor and obsolete parameterless declarations with a concise
required-values error, without constructor-specific migration instructions or a separate
constructor-count guard. Already-built legacy assemblies retain their constructor
metadata even though new source declarations would fail compilation against the new library.
Only operation values 0 through 4 are accepted; no `Unspecified` enum member or default sentinel is
needed to detect missing arguments.

The lowercased configuration-table schema name identifies the provider through
`entitydataprovider.datasourcelogicalname`. It is not a business virtual-table name. Multiple remote
records for the same logical name are ambiguous and fail planning. Metadata is merged across
declarations, including across package assemblies; conflicting explicit values and different
classes claiming the same operation are rejected.

Omitted optional description and table labels preserve existing values. An explicitly empty
description clears it. New table labels default to schema name and singular label + `" Records"`.
Undeclared provider operations preserve existing handlers.

## Dataverse registration model

Provider operations are primitive plugin-type GUID fields on `EntityDataProvider`:
`retrieveplugin`, `retrievemultipleplugin`, `createplugin`, `updateplugin`, and `deleteplugin`.
They are not `EntityReference` values and must not be registered as ordinary SDK steps.
`DataProviderOperation` represents exactly these five operations, with one field mapping in
`DataProviderOperationExtensions`. Local declarations, remote state, plans, and repository writes
use enum keys. Provider queries explicitly select only the five mapped handler columns; bulk
handlers and unrelated GUID columns are outside reconciliation scope. Do not infer handlers from
an attribute-name suffix.
`EntityDataProviderRepository` reads/writes the generated early-bound `EntityDataProvider` model.
The enum's field mapping uses its `LogicalNames` constants; the model's additional platform handler
properties do not expand the five-operation deployment scope.
Dataverse maintains internal MainOperation steps; stage-30 remote steps are excluded from ordinary
step reconciliation, solution additions, and dependent-step purge.

The removed combined `push` module synthesized synchronous stage-30 SDK steps too. Its tests only
covered event/message mapping, not platform acceptance. Prior success therefore does not establish
correct provider provisioning. Legacy code skipped unresolved message filters and only added new
steps to solutions, potentially masking registration or solution-membership failures.

## Configuration-table provisioning

The configuration table is itself a specialized, organization-owned virtual table backed by the
built-in JsonConverter provider, resolved by its exact `name` in the target environment. An ordinary custom table
cannot be converted. Planning validates existing table metadata instead of treating any existing
table as suitable.

The public [EntityMetadata reference](https://learn.microsoft.com/en-us/power-apps/developer/data-platform/webapi/reference/entitymetadata)
exposes no dedicated `IsDataSource` flag. `TableType` distinguishes standard and elastic tables,
not configuration tables; `DataSourceId` references a configuration record used by a business
virtual table. The JsonConverter `DataProviderId` comparison is therefore a backing-provider
compatibility check, not an independent platform declaration that the table is a data source.

Creation disables unsupported virtual-table capabilities, sets external table/column mappings,
names the primary-name column `<publisher-prefix>_name` (prefix before the first underscore,
lowercased) with the same external name, and leaves existing primary-name columns unchanged. It
uses the organization's base language for labels, maps the primary ID after creation, and publishes
only the affected table. Label comparisons also use base-language localized labels, not the
deployment user's localized label; otherwise pushes from differently localized users are not
idempotent. New provider operations not declared locally are left unset. Do not assign an
unconfirmed hard-coded Not Implemented plugin type. Existing undeclared handlers remain untouched.
The JsonConverter backing provider is resolved during planning and before table creation.
The planner calls the repository's `ValidateDataSourceAsync` for both new and existing tables.
Backing-provider resolution is private to the repository and shared with creation; planning does
not depend on JsonConverter IDs or implement the metadata compatibility rules.
Incompatible existing tables raise `InvalidDataSourceException` (an `AbstractPowerException`);
missing or ambiguous JsonConverter resolution remains a separate environment failure.

The resolved JsonConverter ID backs the configuration table itself, not the user's business virtual
tables. Missing or duplicate name matches fail explicitly, with no hard-coded GUID fallback.
Successful lookup is cached per repository instance and used for both table validation and creation.
`ismanaged` is not a lookup constraint: the built-in may expose it as null. Platform acceptance of
empty custom-provider handler fields still needs live validation.

**Platform verification caveat:** special table-provisioning metadata is based on public PRT
payload comparisons and third-party implementations, not a local live-Dataverse verification.
Request-payload tests cannot prove platform acceptance. Fresh metadata may also encounter platform
cache lag before dependent provider creation; there is no automatic cache-lag retry. Validate
bootstrap in a non-production environment before production use.

Data-provider deployment is labelled experimental in user-facing documentation, not all plugin
deployment. Remove the designation only after live validation of first-time provisioning and
platform-owned MainOperation-step cleanup during handler replacement.
Keep public documentation concise: describe experimental status and deployment scope, and link
to the registration library for attribute usage rather than duplicating its API reference/examples.
Keep live-platform verification caveats in developer notes; experimental does not mean untested.

Configuration instance records, custom configuration columns, business virtual tables, and
mappings are deliberately outside deployment scope.

## Execution, upgrades, and solution membership

Create all handler plugin types before applying provider assignments. Package execution collects
type IDs across assemblies and applies each aggregated provider once before type or assembly
cleanup. Provider reconciliation is package-wide so moving a handler to another assembly does not
incorrectly fail a per-assembly deletion check. Standalone assembly upgrades migrate existing operation references to same-name replacement
types, including undeclared operations. Planning rejects deleting a referenced type when the
deployment does not provide a replacement.

`PluginTypeDeploymentExecutor` exposes internal `ApplyTypesAsync`, `ApplyRegistrationsAsync`,
`ApplyDataProvidersAsync`, and `DeleteTypesAsync` phases. `PluginPushExecutor` explicitly sequences
them; no deferred-lifecycle boolean or optional result dictionary determines execution mode.
Standalone ordering remains types, providers, registrations, deletions, identity, outdated-assembly
cleanup. Package ordering remains assembly/type/registration work across assemblies, providers once,
then type/outdated-assembly cleanup. The public type-level `ApplyAsync` remains a full-lifecycle
convenience wrapper. A failed provider write must propagate before obsolete type/assembly deletion;
tests cover both standalone upgrades and cross-assembly package moves.

With a target solution, include the provider record and data-source table definition alongside the
assembly/package. Resolve the provider's component type using `solutioncomponentdefinition`;
its object type code is not necessarily its solution component type. Table solution membership
uses the metadata ID and must transport the schema, not a configuration-record ID.

Writes are sequential, not transactional. CLI-host and per-target plugin failures use
`ExceptionExtensions.DiagnosticMessage()` to preserve exception-chain context and include
Dataverse fault codes and inner-fault messages. TraceText, arbitrary ErrorDetails, and stack traces
are deliberately not dumped. This behavior is independent of build configuration; removing only
the host's release/debug conditional would not fix errors caught inside plugin push.

Execution tests share repository/planner/executor wiring through `PluginDeploymentTestFactory`.
Its default provider stub is shared between planning and execution; provider-specific tests can
inject their own repository. Keep the legacy attribute constructor in the fixture to exercise
rejection of old registrations rather than silently losing that migration coverage.

## References

- `src/modules/dgt.power.plugin/Local/AssemblyReflectionReader.cs`
- `src/modules/dgt.power.plugin/Local/DataProviderRegistrationGrouper.cs`
- `src/modules/dgt.power.plugin/Planning/DataProviderDeploymentPlanner.cs`
- `src/modules/dgt.power.plugin/Planning/PluginDeploymentPlanner.cs`
- `src/modules/dgt.power.plugin/Repositories/EntityDataProviderRepository.cs`
- `src/modules/dgt.power.plugin/Execution/PluginTypeDeploymentExecutor.cs`
- `src/modules/dgt.power.plugin/Execution/PluginPushExecutor.cs`
- [Microsoft: provider plugin registration](https://learn.microsoft.com/en-us/power-apps/developer/data-platform/virtual-entities/custom-ve-data-providers#plug-in-registration)
- [Microsoft: provider registration walkthrough](https://learn.microsoft.com/en-us/power-apps/developer/data-platform/virtual-entities/sample-ve-provider-crud-operations#step-2-creating-data-provider-and-adding-plug-ins-to-the-provider)
- [Microsoft: EntityDataProvider reference](https://learn.microsoft.com/en-us/power-apps/developer/data-platform/reference/entities/entitydataprovider)
- [Microsoft: virtual-table considerations](https://learn.microsoft.com/en-us/power-apps/developer/data-platform/virtual-entities/api-considerations-ve)
- [Third-party provisioning](https://github.com/phuocle/Dynamics-Crm-DevKit/blob/master/v5/DynamicsCrm.DevKit.Cli/Tasks/TaskDataSource.cs)
- [Public PRT payload comparison](https://github.com/jamesoleinik/launch-control/blob/master/scripts/python/register_ve_data_provider.py)
