// Copyright (c) DIGITALL Nature. All rights reserved
// DIGITALL Nature licenses this file to you under the Microsoft Public License.

using System.Text.Json;
using dgt.power.dataverse;
using Microsoft.PowerPlatform.Dataverse.Client;
using Microsoft.Xrm.Sdk.Query;
using Spectre.Console;
using Spectre.Console.Cli;

namespace dgt.power.maintenance.Logic;

public class ApplyDeploymentSettings(IOrganizationServiceAsync2 organizationService) : AsyncCommand<ApplyDeploymentSettings.Settings>
{
    private readonly IOrganizationServiceAsync2 _organizationService = organizationService;

    public class Settings : CommandSettings
    {
        [CommandOption("--settings-file|-s")]
        public required string SettingsFile { get; set; }
    }

    public override async Task<int> ExecuteAsync(CommandContext context, Settings settings)
    {
        await AnsiConsole.Progress()
            .StartAsync(async context =>
            {
                var loadSettingsFileTask = context.AddTask("loading settings file").IsIndeterminate();
                var applyEnvTask = context.AddTask("apply environment settings", false);

                var settingsFilePath = settings.SettingsFile;

                JsonElement settingsJson;
                using (var fs = new FileStream(settingsFilePath, FileMode.Open, FileAccess.Read, FileShare.ReadWrite))
                {
                    settingsJson = await JsonSerializer.DeserializeAsync<JsonElement>(fs);
                }

                if (!settingsJson.TryGetProperty("EnvironmentVariables", out var envSettingsElement) || envSettingsElement.ValueKind != JsonValueKind.Array)
                {
                    AnsiConsole.MarkupLine("[orange3]no environment settings part found in config[/]");
                    applyEnvTask.StopTask();
                }
                else
                {
                    applyEnvTask.MaxValue(envSettingsElement.GetArrayLength()).StartTask();

                    await Parallel.ForEachAsync(envSettingsElement.EnumerateArray(), async (envSetting, _) =>
                    {
                        var envName = envSetting.GetProperty("SchemaName").GetString();
                        var envValue = envSetting.GetProperty("Value").GetString();

                        AnsiConsole.MarkupLine($"[grey]handling environment variable: {envName}[/]");

                        var envDefinitionQuery = new QueryByAttribute
                        {
                            EntityName = EnvironmentVariableDefinition.EntityLogicalName,
                            ColumnSet = new ColumnSet(EnvironmentVariableDefinition.LogicalNames.SchemaName),
                            Attributes = { EnvironmentVariableDefinition.LogicalNames.SchemaName },
                            Values = { envName },
                        };
                        var envDefionitions = await _organizationService.RetrieveMultipleAsync(envDefinitionQuery);
                        var envDefinition = (envDefionitions.Entities.FirstOrDefault()?.ToEntity<EnvironmentVariableDefinition>()) ?? throw new InvalidOperationException($"Environment variable definition '{envName}' not found");

                        var envValueQuery = new QueryByAttribute
                        {
                            EntityName = EnvironmentVariableValue.EntityLogicalName,
                            ColumnSet = new ColumnSet(EnvironmentVariableValue.LogicalNames.Value),
                            Attributes = { EnvironmentVariableValue.LogicalNames.EnvironmentVariableDefinitionId },
                            Values = { envDefinition.Id },
                        };
                        var envValues = await _organizationService.RetrieveMultipleAsync(envValueQuery);
                        var existingEnvValue = envValues.Entities.FirstOrDefault()?.ToEntity<EnvironmentVariableValue>();

                        if (existingEnvValue == null)
                        {
                            AnsiConsole.MarkupLine($"[grey]{envName}: creating new value[/]");
                            var newEnvValue = new EnvironmentVariableValue
                            {
                                EnvironmentVariableDefinitionId = envDefinition.ToEntityReference(),
                                Value = envValue,
                            };
                            await _organizationService.CreateAsync(newEnvValue);
                        }
                        else if (existingEnvValue.Value != envValue)
                        {
                            AnsiConsole.MarkupLine($"[grey]{envName}: updating value[/]");
                            existingEnvValue.Value = envValue;
                            await _organizationService.UpdateAsync(existingEnvValue);
                        }
                        else
                        {
                            AnsiConsole.MarkupLine($"[grey]{envName}: value up to date[/]");
                        }

                        applyEnvTask.Increment(1);
                    });
                }

                loadSettingsFileTask.StopTask();
            });

        return 0;
    }
}