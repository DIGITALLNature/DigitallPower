# Tasks

## 1. Registration discovery and tests

- [ ] 1.1 Pass the inspected target assembly through registration-attribute detection and parsing, and accept supported attributes only from that assembly or `Digitall.Plugins.Registration`; verify the focused `AssemblyReflectionReaderTests` pass.
- [ ] 1.2 Add a fixture with a supported registration attribute defined in the same assembly as its plugin type and assert it is recognized, while retaining coverage for official-library acceptance and unrelated-assembly rejection; verify the focused test project passes.

## 2. User documentation

- [ ] 2.1 Update the plugin command reference and deployment guide to describe both supported attribute sources and the single-DLL use case; verify the revised guidance is consistent with the spec delta.

## 3. Integration validation

- [ ] 3.1 Run the plugin test project and `pnpm exec openspec validate --all`; verify both complete successfully.
