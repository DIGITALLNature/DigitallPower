using System.Globalization;
using dgt.power.dataverse;
using ec4u.solutionconcept;
using Microsoft.PowerPlatform.Dataverse.Client;
using Microsoft.Xrm.Sdk;
using Microsoft.Xrm.Sdk.Client;
using Microsoft.Xrm.Sdk.Messages;
using Microsoft.Xrm.Sdk.Query;
using Spectre.Console;
using Spectre.Console.Cli;

namespace dgt.power.solutionconcept;

public class MigrateToDigitallSolutions(IOrganizationServiceAsync2 orgService) : AsyncCommand, IDisposable
{
    private readonly ec4u.solutionconcept.DataContext _dataContext = new(orgService)
    {
        MergeOption = MergeOption.NoTracking,
    };

    public override async Task<int> ExecuteAsync(CommandContext context)
    {
        var runMigration = await AnsiConsole.Status()
            .StartAsync("Checking migration status", async ctx =>
            {
                var (ec4uSolutionConceptVersion, dgtSolutionConceptVersion) = await GetSolutionConceptVersions();

                AnsiConsole.MarkupLine(CultureInfo.InvariantCulture, "{0} Solution Concept ec4u: {1}",
                    ec4uSolutionConceptVersion == null ? Emoji.Known.CrossMark : Emoji.Known.CheckMark,
                    ec4uSolutionConceptVersion ?? "[grey]n/a[/]"
                );
                AnsiConsole.MarkupLine(CultureInfo.InvariantCulture, "{0} Solution Concept dgt: {1}",
                    dgtSolutionConceptVersion == null ? Emoji.Known.CrossMark : Emoji.Known.CheckMark,
                    dgtSolutionConceptVersion ?? "[grey]n/a[/]"
                );

                if (ec4uSolutionConceptVersion == null || dgtSolutionConceptVersion == null)
                {
                    return false;
                }

                var recordCounts = await GetDgtRecordCounts();

                foreach (var (entityName, recordCount) in recordCounts)
                {
                    AnsiConsole.MarkupLine(CultureInfo.InvariantCulture, "{0} Record count for '{1}': {2}",
                        recordCount == 0 ? Emoji.Known.CheckMark : Emoji.Known.CrossMark,
                        entityName,
                        recordCount
                    );
                }

                return recordCounts.Where(c => c.Key != "dgt_carrier_constraint").All(kvp => kvp.Value == 0);
            });

        if (!runMigration)
        {
            AnsiConsole.MarkupLine("Migration skipped");
            return 1;
        }

        AnsiConsole.Write(new Rule("[lime]Preparing migration[/]"));
        var recordCounts = await AnsiConsole.Status()
            .StartAsync("Retrieving record counts", async ctx => await GetEc4uRecordCounts());

        var table = new Table()
            .AddColumn("Table")
            .AddColumn("Record count", c => c.RightAligned());

        foreach (var (entityName, recordCount) in recordCounts)
        {
            table.AddRow(entityName, recordCount.ToString(CultureInfo.InvariantCulture));
        }

        AnsiConsole.Write(table);
        AnsiConsole.Write(new Rule("[lime]Performing migration[/]"));

        await AnsiConsole.Progress()
            .StartAsync(async ctx =>
            {
                var carrierProgressTask = ctx.AddTask("Carriers", true, recordCounts["ec4u_carrier"] + 1);
                var carrierDependencyCheckProgressTask = ctx.AddTaskAfter("Carrier Dependency Checks", carrierProgressTask, false, recordCounts["ec4u_carrier_dependency_check"] + 1);
                var carrierMissingDependencyProgressTask = ctx.AddTaskAfter("Carrier Missing Dependencies", carrierDependencyCheckProgressTask, false, recordCounts["ec4u_carrier_missing_dependency"] + 1);

                var workbenchProgressTask = ctx.AddTaskAfter("Workbenches", carrierProgressTask, false, recordCounts["ec4u_workbench"] + 1);
                var workbenchHistoryProgressTask = ctx.AddTaskAfter("Workbench History", workbenchProgressTask, false, recordCounts["ec4u_workbench_history"] + 1);
                var workbenchCarrierRefProgressTask = ctx.AddTaskAfter("Workbench Carrier References", workbenchProgressTask, false, recordCounts["ec4u_carrier"] + 1);

                static void updateProgressTask(ProgressTask progressTask, string message)
                {
                    progressTask.Increment(1);
                    AnsiConsole.MarkupLine(CultureInfo.InvariantCulture, "[grey]{0}[/]", message.EscapeMarkup());
                };

                var carrierTask = MigrateCarriers((message) => updateProgressTask(carrierProgressTask, message));

                var workbenchTask = await carrierTask.ContinueWith(_ => MigrateWorkbenches((message) => updateProgressTask(workbenchProgressTask, message)));
                var workbenchHistoryTask = await workbenchTask.ContinueWith(_ => MigrateWorkbenchHistories((message) => updateProgressTask(workbenchHistoryProgressTask, message)));
                var workbenchCarrierRefTask = await workbenchTask.ContinueWith(_ => MigrateWorkbenchCarrierReferences((message) => updateProgressTask(workbenchCarrierRefProgressTask, message)));

                var carrierDependencyTask = await carrierTask
                    .ContinueWith(_ => MigrateCarrierDependencies((message) => updateProgressTask(carrierDependencyCheckProgressTask, message)))
                    .ContinueWith(_ => MigrateCarrierDependencyChecks((message) => updateProgressTask(carrierMissingDependencyProgressTask, message)));

                await Task.WhenAll(carrierTask, workbenchTask, carrierDependencyTask, workbenchHistoryTask, workbenchCarrierRefTask);

                AnsiConsole.MarkupLine($"Migration completed {carrierTask.IsFaulted} {workbenchTask.IsFaulted} {carrierDependencyTask.IsFaulted} {workbenchHistoryTask.IsFaulted} {workbenchCarrierRefTask.IsFaulted}");
            });

        return 0;
    }

    private async Task MigrateWorkbenchCarrierReferences(Action<string> progress)
    {
        var carriers = _dataContext.Ec4uCarrierSet
            .Select(c => new DgtCarrier
            {
                DgtCarrierId = c.Ec4uCarrierId,
                DgtWorkbenchId = c.Ec4uCarWorkbenchId,
            });

        await Parallel.ForEachAsync(carriers, async (carrier, cancellationToken) =>
        {
            await orgService.UpdateAsyncBypassingPlugins(new DgtCarrier { Attributes = carrier.Attributes, }, cancellationToken);
            progress($"carrier workbench ref migrated: {carrier.DgtReference} ({carrier.DgtCarrierId})");
        });

        progress("all carrier workbech refs migrated");
    }

    private async Task MigrateCarrierDependencyChecks(Action<string> progress)
    {
        var carrierDependencyChecks = _dataContext.Ec4uCarrierDependencyCheckSet
            .Select(d => new DgtCarrierDependencyCheck
            {
                DgtCarrierDependencyCheckId = d.Ec4uCarrierDependencyCheckId,
                DgtCarrierId = d.Ec4uCdcCarrierId,
                DgtCheckref = d.Ec4uCdcCheckref,
                Statecode = d.Statecode,
                Statuscode = d.Statuscode,
                OverriddenCreatedOn = d.CreatedOn,
            });

        await Parallel.ForEachAsync(carrierDependencyChecks, async (carrierDependencyCheck, cancellationToken) =>
        {
            await orgService.CreateAsyncBypassingPlugins(new DgtCarrierDependencyCheck { Attributes = carrierDependencyCheck.Attributes }, cancellationToken);
            progress($"carrier dependency check migrated: {carrierDependencyCheck.DgtCarrierDependencyCheckId}");
        });

        progress("all carrier dependency checks migrated");
    }

    private async Task MigrateCarrierDependencies(Action<string> progress)
    {
        var carrierDependencies = _dataContext.Ec4uCarrierMissingDependencySet
            .Select(d => new DgtCarrierMissingDependency
            {
                DgtCarrierDependencyCheckId = d.Ec4uCmdCarrierDependencyCheckId,
                DgtCarrierId = d.Ec4uCmdCarrierId,
                DgtCarrierMissingDependencyId = d.Ec4uCarrierMissingDependencyId,
                DgtComponent = d.Ec4uCmdComponent,
                DgtRequiredComponentObjectid = d.Ec4uCmdRequiredComponentObjectid,
                DgtRequiredComponentTypeName = d.Ec4uCmdRequiredComponentTypeName,
                DgtRequiredComponentTypeNo = d.Ec4uCmdRequiredComponentTypeNo,
                DgtSolutionComponentRecordid = d.Ec4uCmdSolutionComponentRecordid,
                Statecode = d.Statecode,
                Statuscode = d.Statuscode,
                OverriddenCreatedOn = d.CreatedOn,
            });

        await Parallel.ForEachAsync(carrierDependencies, async (carrierDependency, cancellationToken) =>
        {
            await orgService.CreateAsyncBypassingPlugins(new DgtCarrierMissingDependency { Attributes = carrierDependency.Attributes }, cancellationToken);
            progress($"carrier dependency migrated: {carrierDependency.DgtCarrierMissingDependencyId}");
        });

        progress("all carrier dependencies migrated");
    }

    private async Task MigrateWorkbenchHistories(Action<string> progress)
    {
        var workbenchHistories = _dataContext.Ec4uWorkbenchHistorySet
            .Select(w => new DgtWorkbenchHistory
            {
                DgtCarrierId = w.Ec4uWbhCarrierId,
                DgtCarrierVersion = w.Ec4uWbhCarrierVersion,
                DgtComponentMoverLog = w.Ec4uWbhComponentMoverLog,
                DgtConstraintCheckLog = w.Ec4uWbhConstraintCheckLog,
                DgtEntry = w.Ec4uWbhEntry,
                DgtWorkbenchHistoryId = w.Ec4uWorkbenchHistoryId,
                DgtWorkbenchId = w.Ec4uWbhWorkbenchId,
                DgtWorkbenchVersion = w.Ec4uWbhWorkbenchVersion,
                OwnerId = w.OwnerId,
                Statecode = w.Statecode,
                Statuscode = w.Statuscode,
                OverriddenCreatedOn = w.CreatedOn,
            });

        await Parallel.ForEachAsync(workbenchHistories, async (workbenchHistory, cancellationToken) =>
        {
            var deactivateWorkbenchHistory = false;

            if (workbenchHistory.Statecode!.Value == Ec4uWorkbenchHistory.Options.Statecode.Inactive)
            {
                deactivateWorkbenchHistory = true;
                workbenchHistory.Statecode = new OptionSetValue(DgtWorkbenchHistory.Options.Statecode.Active);
                workbenchHistory.Statuscode = new OptionSetValue(DgtWorkbenchHistory.Options.Statuscode.Active);
            }

            await orgService.CreateAsyncBypassingPlugins(new DgtWorkbenchHistory { Attributes = workbenchHistory.Attributes }, cancellationToken);

            if (deactivateWorkbenchHistory)
            {
                await orgService.UpdateAsyncBypassingPlugins(new DgtWorkbenchHistory(workbenchHistory.Id)
                {
                    Statecode = new OptionSetValue(DgtWorkbenchHistory.Options.Statecode.Inactive),
                    Statuscode = new OptionSetValue(DgtWorkbenchHistory.Options.Statuscode.Success),
                }, cancellationToken);
            }

            progress($"workbench history migrated: {workbenchHistory.DgtWorkbenchHistoryId}");
        });

        progress("all workbench histories migrated");
    }

    private async Task MigrateWorkbenches(Action<string> progress)
    {
        var workbenches = _dataContext.Ec4uWorkbenchSet
            .Select(w => new DgtWorkbench
            {
                DgtWorkbenchId = w.Ec4uWorkbenchId,
                DgtCarrierId = w.Ec4uWbCarrierId,
                DgtName = w.Ec4uWbName,
                DgtNatureSet = w.Ec4uWbNatureSet,
                DgtSolutionfriendlyname = w.Ec4uWbSolutionfriendlyname,
                DgtSolutionid = w.Ec4uWbSolutionid,
                DgtSolutionuniquename = w.Ec4uWbSolutionuniquename,
                DgtSolutionversion = w.Ec4uWbSolutionversion,
                DgtTargetCarrierId = w.Ec4uWbTargetCarrierId,
                OwnerId = w.OwnerId,
                Statecode = w.Statecode,
                Statuscode = w.Statuscode,
                OverriddenCreatedOn = w.CreatedOn,
            });

        await Parallel.ForEachAsync(workbenches, async (workbench, cancellationToken) =>
        {
            var deactivateWorkbench = false;

            workbench.Statuscode = new OptionSetValue(workbench.Statuscode!.Value switch
            {
                Ec4uWorkbench.Options.Statuscode.Active => DgtWorkbench.Options.Statuscode.Active,
                Ec4uWorkbench.Options.Statuscode.Close => DgtWorkbench.Options.Statuscode.Close,
                Ec4uWorkbench.Options.Statuscode.Finalize => DgtWorkbench.Options.Statuscode.Finalize,
                Ec4uWorkbench.Options.Statuscode.Failure => DgtWorkbench.Options.Statuscode.Failure,
                Ec4uWorkbench.Options.Statuscode.Merge => DgtWorkbench.Options.Statuscode.Merge,
                Ec4uWorkbench.Options.Statuscode.Inactive => DgtWorkbench.Options.Statuscode.Inactive,
                _ => DgtWorkbench.Options.Statuscode.Active,
            });

            if (workbench.Statecode!.Value == Ec4uWorkbench.Options.Statecode.Inactive)
            {
                deactivateWorkbench = true;
                workbench.Statecode = new OptionSetValue(DgtWorkbench.Options.Statecode.Active);
                workbench.Statuscode = new OptionSetValue(DgtWorkbench.Options.Statuscode.Active);
            }

            if (workbench.DgtNatureSet != null)
            {
                workbench.DgtNatureSet = new OptionSetValue(workbench.DgtNatureSet.Value switch
                {
                    Ec4uWorkbench.Options.Ec4uWbNatureSet.Assembly => DgtWorkbench.Options.DgtNatureSet.Assembly,
                    Ec4uWorkbench.Options.Ec4uWbNatureSet.EnvironmentVariable => DgtWorkbench.Options.DgtNatureSet.EnvironmentVariable,
                    Ec4uWorkbench.Options.Ec4uWbNatureSet.ConnectionReference => DgtWorkbench.Options.DgtNatureSet.ConnectionReference,
                    _ => DgtWorkbench.Options.DgtNatureSet.Workbench,
                });
            }

            await orgService.CreateAsyncBypassingPlugins(new DgtWorkbench { Attributes = workbench.Attributes }, cancellationToken);

            if (deactivateWorkbench)
            {
                await orgService.UpdateAsyncBypassingPlugins(new DgtWorkbench(workbench.Id)
                {
                    Statecode = new OptionSetValue(DgtWorkbench.Options.Statecode.Inactive),
                    Statuscode = new OptionSetValue(DgtWorkbench.Options.Statuscode.Inactive),
                }, cancellationToken);
            }

            progress($"workbench migrated: {workbench.DgtName} ({workbench.DgtWorkbenchId})");
        });

        progress("all workbenches migrated");
    }

    private async Task MigrateCarriers(Action<string> progress)
    {
        var carriers = _dataContext.Ec4uCarrierSet
            .Select(c => new DgtCarrier
            {
                DgtCarrierId = c.Ec4uCarrierId,
                DgtConstraintMset = c.Ec4uConstraintMset,
                DgtHandshakeTs = c.Ec4uCarHandshakeTs,
                DgtLockedOpt = c.Ec4uCarLockedOpt,
                DgtReference = c.Ec4uCarReference,
                DgtSolutionfriendlyname = c.Ec4uCarSolutionfriendlyname,
                DgtSolutionid = c.Ec4uCarSolutionid,
                DgtSolutionuniquename = c.Ec4uCarSolutionuniquename,
                DgtSolutionversion = c.Ec4uCarSolutionversion,
                DgtTransportOrderNo = c.Ec4uCarTransportOrderNo,
                Statecode = c.Statecode,
                Statuscode = c.Statuscode,
                OverriddenCreatedOn = c.CreatedOn,
            });

        await Parallel.ForEachAsync(carriers, async (carrier, cancellationToken) =>
        {
            var deactivateCarrier = false;

            if (carrier.Statecode!.Value == Ec4uCarrier.Options.Statecode.Inactive)
            {
                deactivateCarrier = true;
                carrier.Statecode = new OptionSetValue(DgtCarrier.Options.Statecode.Active);
                carrier.Statuscode = new OptionSetValue(DgtCarrier.Options.Statuscode.Active);
            }

            if (carrier.DgtConstraintMset != null)
            {
                carrier.DgtConstraintMset = new OptionSetValueCollection(carrier.DgtConstraintMset.Select(o => o.Value switch
                {
                    Ec4uCarrier.Options.Ec4uConstraintMset.PreventFlows => new OptionSetValue(DgtCarrier.Options.DgtConstraintMset.PreventFlows),
                    Ec4uCarrier.Options.Ec4uConstraintMset.PreventItemsWithouthActiveLayer => new OptionSetValue(DgtCarrier.Options.DgtConstraintMset.PreventItemsWithouthActiveLayer),
                    Ec4uCarrier.Options.Ec4uConstraintMset.PreventManagedEntitiesWithAllAssets => new OptionSetValue(DgtCarrier.Options.DgtConstraintMset.PreventManagedEntitiesWithAllAssets),
                    Ec4uCarrier.Options.Ec4uConstraintMset.PreventPluginAssemblys => new OptionSetValue(DgtCarrier.Options.DgtConstraintMset.PreventPluginAssemblys),
                    _ => null,
                }).Where(ov => ov != null).ToList());
            }

            await orgService.CreateAsyncBypassingPlugins(new DgtCarrier { Attributes = carrier.Attributes, }, cancellationToken);

            if (deactivateCarrier)
            {
                await orgService.UpdateAsyncBypassingPlugins(new DgtCarrier(carrier.Id)
                {
                    Statecode = new OptionSetValue(DgtCarrier.Options.Statecode.Inactive),
                    Statuscode = new OptionSetValue(DgtCarrier.Options.Statuscode.Inactive),
                }, cancellationToken);
            };

            var constraintRecordRefs = carrier.DgtConstraintMset?.Select(constraint => constraint.Value switch
                {
                    DgtCarrier.Options.DgtConstraintMset.PreventFlows => new EntityReference(DgtCarrierConstraint.EntityLogicalName, Guid.Parse("82a9f61f-ce36-ef11-8409-000d3aa8c88e")),
                    DgtCarrier.Options.DgtConstraintMset.PreventItemsWithouthActiveLayer => new EntityReference(DgtCarrierConstraint.EntityLogicalName, Guid.Parse("dd0be450-ce36-ef11-8409-000d3aa8c88e")),
                    DgtCarrier.Options.DgtConstraintMset.PreventManagedEntitiesWithAllAssets => new EntityReference(DgtCarrierConstraint.EntityLogicalName, Guid.Parse("2285cf57-ce36-ef11-8409-000d3aa8c88e")),
                    DgtCarrier.Options.DgtConstraintMset.PreventPluginAssemblys => new EntityReference(DgtCarrierConstraint.EntityLogicalName, Guid.Parse("4984b838-ce36-ef11-8409-000d3aa8c88e")),
                    _ => null
                })
                .Where(entityRef => entityRef != null)
                .Select(entityRef => entityRef!)
                .ToList();

            if (constraintRecordRefs?.Count > 0)
            {
                orgService.Associate(DgtCarrier.EntityLogicalName, carrier.Id, new Relationship(DgtCarrier.Relations.ManyToMany.DgtCarrierConstraintDgtCarrierDgtCarrier), new EntityReferenceCollection(constraintRecordRefs));
            }
            progress($"carrier migrated: {carrier.DgtReference} ({carrier.DgtCarrierId})");
        });

        progress("all carriers migrated");
    }

    private async Task<Dictionary<string, int>> GetEc4uRecordCounts() => await GetRecordCounts("ec4u_carrier", "ec4u_carrier_dependency_check", "ec4u_carrier_missing_dependency", "ec4u_workbench", "ec4u_workbench_history");

    private async Task<Dictionary<string, int>> GetDgtRecordCounts() => await GetRecordCounts("dgt_carrier", "dgt_carrier_constraint", "dgt_carrier_dependency_check", "dgt_carrier_missing_dependency", "dgt_workbench", "dgt_workbench_history");

    private async Task<Dictionary<string, int>> GetRecordCounts(params string[] tableNames)
    {
        var getRecordCount = (string tableName) =>
        {
            var query = new QueryExpression(tableName)
            {
                ColumnSet = new ColumnSet(true),
            };
            return orgService.RetrieveMultipleAsync(query).ContinueWith(task => KeyValuePair.Create(tableName, task.Result.Entities.Count));
        };

        var recordCountTasks = tableNames.AsParallel().Select(getRecordCount).ToArray();
        var recordCounts = await Task.WhenAll(recordCountTasks);

        return recordCounts.ToDictionary(kvp => kvp.Key, kvp => kvp.Value);
    }

    private async Task<(string? ec4uSolutionConceptVersion, string? dgtSolutionConceptVersion)> GetSolutionConceptVersions()
    {
        var solutionQuery = new QueryExpression(Solution.EntityLogicalName)
        {
            ColumnSet = new ColumnSet(Solution.LogicalNames.UniqueName, Solution.LogicalNames.Version),
        };
        solutionQuery.Criteria.AddCondition(Solution.LogicalNames.UniqueName, ConditionOperator.In, ["ec4u_solution_concept", "DIGITALLSolutions"]);

        var solutionCollection = await orgService.RetrieveMultipleAsync(solutionQuery);
        var solutions = solutionCollection.Entities.Cast<Solution>().ToList();

        var ec4uSolutionConceptVersion = solutions.FirstOrDefault(s => s.UniqueName == "ec4u_solution_concept")?.Version;
        var dgtSolutionConceptVersion = solutions.FirstOrDefault(s => s.UniqueName == "DIGITALLSolutions")?.Version;

        return (ec4uSolutionConceptVersion, dgtSolutionConceptVersion);
    }

    public void Dispose()
    {
        _dataContext.Dispose();
        GC.SuppressFinalize(this);
    }
}

public static class OrganizationServiceExtensions
{
    public static async Task<Guid> CreateAsyncBypassingPlugins(this IOrganizationServiceAsync2 orgService, Entity entity, CancellationToken cancellationToken)
    {
        var createRequest = new CreateRequest { Target = entity };
        createRequest.Parameters.Add("BypassCustomPluginExecution", true);

        var createResponse = (CreateResponse)await orgService.ExecuteAsync(createRequest, cancellationToken);
        return createResponse.id;
    }

    public static async Task UpdateAsyncBypassingPlugins(this IOrganizationServiceAsync2 orgService, Entity entity, CancellationToken cancellationToken = default)
    {
        var updateRequest = new UpdateRequest { Target = entity };
        updateRequest.Parameters.Add("BypassCustomPluginExecution", true);

        await orgService.ExecuteAsync(updateRequest, cancellationToken);
    }
}