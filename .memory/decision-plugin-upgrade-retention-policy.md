# Plugin Upgrade Retention Policy

Supported registration attributes are the authoritative desired state for standalone
`plugin push`. The command does not support manually created or modified plugin types, steps,
images, or Custom API links. A concrete local `IPlugin` without a supported registration attribute
is invalid input.

During a replacement, the command applies the declared target state and removes source
registrations absent from that declaration. It does not retain old assemblies because they contain
undeclared registrations; those registrations are drift to reconcile away. This deliberately avoids
Dataverse managed/unmanaged-solution terminology.

Standalone plugin assemblies are organized by a **major/minor version train**. A deployment whose
local assembly matches an existing train is an in-place content update; build/revision differences
do not create another assembly, irrespective of whether the build/revision moves forward or
backward. A local assembly in a different train follows the replacement
path, irrespective of whether its major/minor version is numerically higher or lower. This permits
rollback across version trains while retaining Dataverse's same-train update behavior.

The supported standalone topology is strict single-train replacement: a given assembly name has
zero or one existing standalone Dataverse record. Zero records creates the local assembly; one
record in the matching major/minor train updates in place; one record in a different train is a
replacement source. Two or more same-name standalone records are unsupported drift and fail
before any writes. Parallel active standalone version trains are out of scope.

Standalone assembly plan and execution output always includes the assembly version. Outdated
assembly lifecycle messages identify each old assembly by name and version, not its Dataverse
record id. `LocalAssembly.Identity` and `RemoteAssembly.Identity` are the canonical calculated
display identities; renderers and executors must not reconstruct the string.

Upgrade rendering is target-state-first: declared registrations and reassigned legacy steps/images
appear under the replacement assembly. Cleanup is a separate red lifecycle outcome that states
the outdated assembly will be deleted, emitted as standalone console output after the assembly
tree.
