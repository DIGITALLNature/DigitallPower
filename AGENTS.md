# Agent Guidelines

Instructions for all AI agents (GitHub Copilot, Claude, Cursor, etc.) working on this repository.

---

## Documentation Maintenance (MANDATORY)

When making changes to this codebase, **you MUST keep the documentation up to date**. This is not optional.

### Rules

1. **README.md must reflect the current state of the project.** After any change that affects the public API, architecture, configuration, project structure, or usage patterns, update the corresponding section(s) in `README.md`.

2. **What requires a README update:**
   - Adding, removing, or renaming public classes, interfaces, or methods
   - Adding or removing NuGet dependencies
   - Adding new Organization Request fakes
   - Changing configuration/environment variables
   - Modifying the builder APIs or fluent extensions
   - Adding new folders or restructuring the project
   - Changing build/test commands or CI/CD workflows
   - Adding new features or capabilities

3. **What does NOT require a README update:**
   - Internal refactoring that doesn't change the public API
   - Bug fixes that don't change behavior or usage
   - Test-only changes
   - Code style / formatting changes

4. **CHANGELOG.md is auto-generated** by semantic-release. Do NOT edit it manually.

5. **Keep documentation in English.** All documentation in this repository is written in English.

### Documentation Style

- Use concise, technical language
- Include code examples for new public APIs
- Keep the table of contents in sync with the actual sections
- Use tables for listing related items (request fakes, config vars, etc.)
- Architecture diagrams use ASCII art (no external dependencies)

---

## Code Conventions

- **Language:** C# with latest LangVersion, nullable enabled, implicit usings
- **Target Framework:** net10.0
- **Naming:** Follow standard .NET naming conventions (PascalCase for public members)
- **Licensing header:** All source files start with `// Copyright (c) DIGITALL Nature. All rights reserved`
- **Tests:** Use TUnit framework with TUnit.Assertions and TUnit.Mocks

---

## Commit Messages

This project uses [Conventional Commits](https://www.conventionalcommits.org/) enforced by commitlint + Husky.

### Format

```
<type>(<scope>): <short description>

[optional body]

[optional footer(s)]
```

### Types

| Type | When to use | Version bump |
|------|-------------|--------------|
| `feat` | New feature or capability | minor |
| `fix` | Bug fix | patch |
| `docs` | Documentation only | none |
| `refactor` | Code change that neither fixes a bug nor adds a feature | none |
| `perf` | Performance improvement | patch |
| `test` | Adding or updating tests only | none |
| `chore` | Tooling, CI, dependencies, config | none |
| `style` | Formatting, white-space, etc. (no logic change) | none |

### Rules

- **Subject line:** imperative mood, lowercase, no period at end, max 100 chars
- **Breaking changes:** Add `!` after type/scope (e.g. `feat!: remove deprecated API`) or add `BREAKING CHANGE:` footer
- **Scope:** optional, use the affected component (e.g. `feat(query): add fiscal year grouping`)

### Examples

```
feat: add BulkUpsert organization request fake
fix(query): correct paging cookie generation for empty results
docs: update README with relationship management section
refactor: extract condition parsing into dedicated class
test: add coverage for FetchXml aggregate queries
chore: bump Microsoft.PowerPlatform.Dataverse.Client to 1.2.10
feat!: remove deprecated ModelAssemblies property
```

---

## Testing (MANDATORY)

Every code change that modifies behavior **must** be accompanied by tests.

### Rules

1. **New features:** Write tests that cover the happy path and relevant edge cases.
2. **Bug fixes:** Write a test that reproduces the bug before fixing it (test-first when feasible).
3. **Refactoring:** Ensure existing tests still pass. Add tests if coverage gaps are discovered.
4. **Deleted functionality:** Remove or update tests that cover the removed code.

### Test Location

- Tests live in `tests/Digitall.Testing.Tests/`
- Mirror the source folder structure (e.g. `Logic/` tests go in `tests/.../Logic/`)
- Test class naming: `<ClassUnderTest>Tests.cs`

### Test Style

```csharp
[Test]
public async Task MethodName_Scenario_ExpectedResult()
{
    // Arrange
    // Act
    // Assert
}
```

---

## Build & Test Commands

```bash
dotnet restore              # Restore dependencies (uses lock files)
dotnet build                # Build the solution
dotnet test                 # Run all tests
dotnet test --filter "Name~Foo"  # Run filtered tests
```

---

## RTK — Token-Optimized CLI

**rtk** is a CLI proxy that filters and compresses command outputs, saving 60-90% tokens.

**Always prefix shell commands with `rtk`** when available. It passes through unchanged if no filter exists — always safe to use.

```bash
rtk git status              # Compact status
rtk git diff                # Compact diff
rtk dotnet build            # Filtered build output
rtk dotnet test             # Failures only
```

Even in command chains:
```bash
rtk git add . && rtk git commit -m "msg" && rtk git push
```
