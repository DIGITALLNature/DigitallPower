# Proposal

## Why

`dgtp plugin push` currently recognizes declarative registration attributes only when they come from the `Digitall.Plugins.Registration` assembly. This prevents single-DLL plugin projects that define the supported registration attributes in their own target assembly from being deployed.

## What Changes

- Accept supported registration attributes defined in the target plugin assembly as well as those from the official registration library.
- Continue rejecting matching attribute names and namespaces defined by unrelated assemblies.

## Capabilities

### New Capabilities

None.

### Modified Capabilities

- `plugin-deployment`: Define which assemblies may provide declarative registration attributes for plugin deployment.

## Impact

- `src/modules/dgt.power.plugin/Deployment/Local/AssemblyReflectionReader.cs`
- `tests/dgt.power.plugin.tests/Deployment/Local/AssemblyReflectionReaderTests.cs`
- `openspec/specs/plugin-deployment/spec.md`
