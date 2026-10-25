# Tab Completion

`dgtp` supports shell tab completion via [dotnet-suggest](https://github.com/dotnet/command-line-api/blob/main/docs/dotnet-suggest.md).

## Quick setup (recommended)

Run both steps in one command:

```bash
# Install dotnet-suggest first if you haven't already
dotnet tool install -g dotnet-suggest

# Register dgtp AND install the shell shim
dgtp complete setup --all
```

Then reload your shell (`source ~/.zshrc` or open a new terminal).

## Manual setup

**1. Install the `dotnet-suggest` global tool:**

```bash
dotnet tool install -g dotnet-suggest
```

**2. Register `dgtp` with dotnet-suggest:**

```bash
dgtp complete setup
```

**3. Install the shell shim:**

```bash
dgtp complete install-shell
```

This auto-detects your current shell and writes the shim to your RC file.
Use `--shell bash|zsh|pwsh|fish` to override the detected shell.
Use `--dry-run` to preview what would be written without making changes.

The shim is written with idempotency markers — running the command again does nothing if already installed:

```
# >>> dgtp tab completion start >>>
...dotnet-suggest shim script...
# <<< dgtp tab completion end <<<
```

## `complete` command reference

| Command | Description |
|---------|-------------|
| `dgtp complete setup` | Registers dgtp with dotnet-suggest |
| `dgtp complete setup --all` | Registers AND installs shell shim |
| `dgtp complete setup --all --shell bash` | Same, with explicit shell override |
| `dgtp complete install-shell` | Installs shell shim (auto-detects shell) |
| `dgtp complete install-shell --shell zsh` | Installs shim for zsh explicitly |
| `dgtp complete install-shell --dry-run` | Preview without writing |

## What gets completed

| Input | Completions |
|-------|-------------|
| `dgtp <TAB>` | `connection` `export` `import` `maintenance` `analyze` `solution` `codegeneration` `plugin` `webresource` `telemetry` `complete` |
| `dgtp export <TAB>` | `teamtemplates` `bulkdeletes` `queues` … |
| `dgtp export --<TAB>` | `--filedir` `--filename` `--inline` `--no-telemetry` |
| `dgtp connection <TAB>` | `list` `create` `delete` `select` `status` `refresh` |
| `dgtp telemetry <TAB>` | `status` `enable` `disable` |
| `dgtp plugin <TAB>` | `push` |
| `dgtp plugin step <TAB>` | `enable` `disable` `config` `state` |
| `dgtp plugin step config <TAB>` | `set` |
| `dgtp plugin step state <TAB>` | `set` |

> **Note:** Tab completion is static (command names and option flags only). It does not connect to Dataverse and requires no network access.
