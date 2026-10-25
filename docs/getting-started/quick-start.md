# Quick Start

Get up and running with dgtp in five commands.

```bash
# 1. Create and select a connection
dgtp connection create dev --url https://contoso-dev.crm4.dynamics.com --tenant contoso.onmicrosoft.com
dgtp connection select dev

# 2. Verify the connection (checks token acquisition)
dgtp connection status

# 3. Export configuration data from the environment
dgtp export queues --filedir ./out/queues

# 4. Generate early-bound C# classes
dgtp codegeneration ./generated -c ./genconfig.json
```

Run `dgtp --help` or `dgtp <command> --help` to discover all options.

## Next steps

- [Command reference](../reference/commands/connection.md) — all commands organized by branch
- [Configuration](../reference/configuration.md) — environment variables and connection settings
- [CI/CD integration](../guides/ci-cd.md) — run dgtp in Azure Pipelines
- [Tab completion](../guides/tab-completion.md) — shell autocompletion setup
