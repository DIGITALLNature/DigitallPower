# Plugin Deployment Guide

End-to-end workflow for developing, deploying, and managing Dataverse plugins with dgtp.

## Prerequisites

- A Dataverse environment and a [connection](../reference/commands/connection.md) to it
- A .NET plugin project referencing `Digitall.Plugins.Registration` 3.0.0-beta.3 or later
- The `dgtp` CLI [installed](../getting-started/installation.md)

## 1. Write the plugin

Decorate your plugin classes with registration attributes from `Digitall.Plugins.Registration`:

```csharp
using Digitall.Plugins.Registration;

[PluginRegistration(PluginExecutionMode.Synchronous, "Create", PluginExecutionStage.PreOperation, PrimaryEntityName = "account")]
public class AccountCreatePlugin : IPlugin
{
    public void Execute(IServiceProvider serviceProvider) { /* ... */ }
}
```

Supported attributes:

| Attribute | Purpose |
|-----------|---------|
| `PluginRegistrationAttribute` | Registers plugin steps (message, stage, mode, entity, images) |
| `CustomApiRegistrationAttribute` | Links a plugin type to a Custom API |
| `CustomDataProviderRegistrationAttribute` | Creates/updates a custom data provider (experimental) |

See the [registration library](https://github.com/DIGITALLNature/DigitallRegistrationPower) for full attribute usage.

## 2. Build and push

```bash
# Build your plugin project
dotnet build -c Release

# Push the assembly to Dataverse
dgtp plugin push ./bin/Release/MyPlugin.dll --solution MySolution

# Or push a NuGet package
dgtp plugin push ./bin/Release/MyPlugin.1.0.0.nupkg --publisher-prefix contoso --solution MySolution
```

Use `--dry-run` to preview the deployment plan without writing to Dataverse.

See [`plugin push`](../reference/commands/plugin.md#push--deploy-plugin-assembliespackages) for all options, update/upgrade behavior, and solution membership handling.

## 3. Manage plugin steps

After deployment, manage step configuration and state:

```bash
# Set unsecure/secure configuration on a step
dgtp plugin step config set --plugin-type MyPlugin --message Create --stage PreOperation --entity account --unsecure '{"key":"value"}'

# Disable a step
dgtp plugin step disable --plugin-type MyPlugin --message Create --stage PreOperation --entity account

# Bulk enable/disable from a config file
dgtp plugin step state set -c step-states.json
```

See [`plugin step` commands](../reference/commands/plugin.md) for all step management options.

## 4. CI/CD

For automated deployment in Azure Pipelines, see [CI/CD integration](ci-cd.md) for authentication setup, then add the `plugin push` step to your pipeline.

## Troubleshooting

| Problem | Solution |
|---------|----------|
| `plugin push` fails with "no plugins found" | Ensure your assembly references `Digitall.Plugins.Registration` and every `IPlugin` type has a registration attribute |
| Managed import fails with "Unable to delete plugintype" | Unlink the step or Custom API handler in downstream environments before removing the type — see [Removals and solution import](../reference/commands/plugin.md#removals-and-solution-import) |
| Assembly update creates a replacement instead of in-place update | Major/minor version changed — this is expected; steps and images are migrated automatically |
