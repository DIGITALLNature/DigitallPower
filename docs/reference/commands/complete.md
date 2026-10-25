# `complete` — Shell tab completion

| Command | Description |
|---------|-------------|
| `dgtp complete setup` | Registers dgtp with dotnet-suggest |
| `dgtp complete setup --all` | Registers AND installs shell shim |
| `dgtp complete setup --all --shell bash` | Same, with explicit shell override |
| `dgtp complete install-shell` | Installs shell shim (auto-detects shell) |
| `dgtp complete install-shell --shell zsh` | Installs shim for zsh explicitly |
| `dgtp complete install-shell --dry-run` | Preview without writing |

See [Tab completion setup](../../guides/tab-completion.md) for the full setup guide.
