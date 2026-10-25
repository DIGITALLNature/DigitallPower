# Build & Test

## Prerequisites

- **.NET SDK 10.0** (the SDK version is pinned via [`global.json`](../../../global.json))
- **Node.js 22 + pnpm** (needed for the TypeScript compile gate in `dgt.power.codegeneration.tests`)
- Network access to `api.nuget.org` (for restore)

## Commands

```bash
pnpm install                 # Install JS tooling, TypeScript compiler, and local OpenSpec CLI
dotnet restore                # Restore dependencies (uses lock files)
dotnet build                  # Build the solution
dotnet test                   # Run all tests
dotnet test --filter "Name~Foo"   # Run a subset of tests
pnpm exec openspec validate --specs  # Validate current OpenSpec specifications
```

## Build a local NuGet package

```bash
dotnet pack src/dgt.power/dgt.power.csproj -c Release
```

The resulting `.nupkg` is placed in the `packages/` folder and can be installed locally with:

```bash
dotnet tool install --global --add-source ./packages dgt.power --version <version>
```
