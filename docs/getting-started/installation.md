# Installation

DigitallPower is published as a [.NET global tool](https://www.nuget.org/packages/dgt.power):

```bash
dotnet tool install -g dgt.power
```

Update to the latest version:

```bash
dotnet tool update -g dgt.power
```

Uninstall:

```bash
dotnet tool uninstall -g dgt.power
```

After installation the command `dgtp` is available globally.

## Requirements

- **.NET 10.0 runtime** (the SDK version is pinned via [`global.json`](../../global.json))
- Network access to your Dataverse environment (`*.dynamics.com`) and to `api.nuget.org` (for the version check)
- An account with sufficient privileges on the target Dataverse environment

## Verify installation

```bash
dgtp --version
dgtp --help
```
