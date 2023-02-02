using System.Globalization;
using dgt.power.common.Extensions;
using dgt.power.dataverse;
using dgt.power.push.Model;
using Microsoft.Crm.Sdk.Messages;
using Microsoft.Xrm.Sdk;
using Microsoft.Xrm.Sdk.Query;
using Spectre.Console;
using PluginType = dgt.power.push.Model.PluginType;

namespace dgt.power.push.Logic;

internal static class Processor
{
    #region PluginAssembly
    public static Assembly CreatePluginAssembly(Assembly dll, IOrganizationService service, string solution)
    {
        var pluginAssembly = new PluginAssembly
        {
            SourceType = new OptionSetValue(dll.SourceType),
            IsolationMode = new OptionSetValue(dll.IsolationMode),
            Name = dll.Name,
            Content = dll.Content
        };

        AnsiConsole.Markup(CultureInfo.InvariantCulture, "Create Assembly [green]{0} ({1})[/]", pluginAssembly.Name, dll.Version);
        pluginAssembly.Id = service.Create(pluginAssembly);
        AnsiConsole.MarkupLine(CultureInfo.InvariantCulture, " -> Id [italic]{0:D}[/]", pluginAssembly.Id);

        if (!string.IsNullOrWhiteSpace(solution))
        {
            AddPluginAssemblyToSolution(service, pluginAssembly, solution);
        }

        return new Assembly
        {
            Name = pluginAssembly.Name,
            Version = dll.Version,
            Content = dll.Content,
            Id = pluginAssembly.Id
        };
    }

    public static Assembly UpdatePluginAssembly(Assembly dll, Assembly crm, IOrganizationService service, string solution)
    {
        AnsiConsole.MarkupLine(CultureInfo.InvariantCulture,  "Update Assembly [green]{0}[/] [italic]({1} -> {2})[/]", crm.Name, crm.Version, dll.Version);
        //purge missing types first to avoid "PluginType [xxx] not found in PluginAssembly" 
        foreach (var oldType in crm.PluginTypes.ToList().Where(t => dll.PluginTypes.All(d => d.TypeName != t.TypeName)))
        {
            crm.PluginTypes.Remove(DeletePluginType(oldType, service));
        }
        var pluginAssembly = new PluginAssembly(crm.Id)
        {
            Content = dll.Content
        };
        service.Update(pluginAssembly);
        if (!string.IsNullOrWhiteSpace(solution) && !crm.Solutions.Contains(solution))
        {
            AddPluginAssemblyToSolution(service, pluginAssembly, solution);
        }
        crm.Content = dll.Content;
        crm.Version = dll.Version;
        return crm;
    }

    private static void AddPluginAssemblyToSolution(IOrganizationService service, PluginAssembly pluginAssembly,
        string solution)
    {
        // PluginAssembly = 91
        var addReq = new AddSolutionComponentRequest
        {
            AddRequiredComponents = false,
            ComponentType = 91,
            ComponentId = pluginAssembly.Id,
            SolutionUniqueName = solution
        };

        try
        {
            AnsiConsole.MarkupLine(CultureInfo.InvariantCulture, "Add Assembly [green]{0}[/] to Solution [bold]{1}[/]", pluginAssembly.Name!, solution);
            service.Execute(addReq);
        }
        catch (Exception ex)
        {
            try
            {
                service.Delete(PluginAssembly.EntityLogicalName, pluginAssembly.Id);
            }
            catch (Exception rb)
            {
                AnsiConsole.MarkupLine(CultureInfo.InvariantCulture, "Rollback failed; cleanup manually");
                AnsiConsole.WriteException(rb.RootException());
            }

            throw new Exception("The Plugin Registration was aborted. Assembly: " + pluginAssembly.Name,
                ex.RootException());
        }
    }
    #endregion

    #region PluginType
    public static Assembly UpsertAndPurgePluginTypes(Assembly dll, Assembly crm, IOrganizationService service, string solution)
    {
        // Update
        foreach (var updateType in dll.PluginTypes.Where(t => crm.PluginTypes.Contains(t)))
        {
            updateType.Id = crm.PluginTypes.Single(e => e.Equals(updateType)).Id;
            UpdatePluginType(updateType, service);
        }
        // New
        foreach (var newType in dll.PluginTypes.Where(d => crm.PluginTypes.All(t => t.TypeName != d.TypeName)))
        {
            crm.PluginTypes.Add(CreatePluginType(crm, newType, service, solution));
        }
        return crm;
    }

    private static PluginType CreatePluginType(Assembly crm, PluginType pluginType, IOrganizationService service, string solution)
    {
        var type = new dataverse.PluginType
        {
            PluginAssemblyId = new EntityReference(pluginType.ParentTypeCode, crm.Id),
            Name = pluginType.Name,
            TypeName = pluginType.TypeName,
            FriendlyName = pluginType.FriendlyName
        };
        AnsiConsole.Markup(CultureInfo.InvariantCulture, " Create PluginType [green]{0}[/] for Assembly [bold]{1}[/]", pluginType.Name, pluginType.ParentName!);
        type.Id = service.Create(type);
        AnsiConsole.MarkupLine(CultureInfo.InvariantCulture, " -> Id [italic]{0:D}[/]", type.Id);

        if (!string.IsNullOrWhiteSpace(pluginType.CustomApi))
        {
            var query = new QueryExpression
            {
                EntityName = CustomAPI.EntityLogicalName,
                ColumnSet = new ColumnSet(CustomAPI.LogicalNames.UniqueName),
                NoLock = true,
                Criteria = new FilterExpression(LogicalOperator.And)
                {
                    Conditions = { new ConditionExpression(CustomAPI.LogicalNames.UniqueName, ConditionOperator.Equal, pluginType.CustomApi) }
                }
            };
            var api = service.RetrieveMultiple(query).Entities.Select(e => e.ToEntity<CustomAPI>()).Single();
            AnsiConsole.Markup(CultureInfo.InvariantCulture, "  Link PluginType [green]{0}[/] to Custom API [bold]{1}[/]", pluginType.Name, api.UniqueName!);
            service.Update(new CustomAPI(api.Id)
            {
                PluginTypeId = type.ToEntityReference()
            });
            AnsiConsole.MarkupLine(CultureInfo.InvariantCulture, " -> Id [italic]{0:D}[/]", api.Id);
        }

        return new PluginType
        {
            ParentId = crm.Id,
            ParentName = pluginType.ParentName,
            Id = type.Id,
            Name = pluginType.Name,
            TypeName = pluginType.TypeName,
            FriendlyName = pluginType.FriendlyName,
            CustomApi = pluginType.CustomApi
        };
    }

    private static PluginType DeletePluginType(PluginType pluginType, IOrganizationService service)
    {
        AnsiConsole.MarkupLine(CultureInfo.InvariantCulture, " Delete PluginType [green]{0}[/] for Assembly [bold]{1}[/] first", pluginType.Name, pluginType.ParentName!);
        var request = new RetrieveDependenciesForDeleteRequest
        {
            ComponentType = 90,//PluginType
            ObjectId = pluginType.Id
        };
        var response = (RetrieveDependenciesForDeleteResponse)service.Execute(request);
        foreach (var dependency in response.EntityCollection.Entities.Select(e => e.ToEntity<Dependency>()))
        {
            if (dependency.DependentComponentType!.Value == 92)//SDK Message Processing Step
            {
                AnsiConsole.MarkupLine(CultureInfo.InvariantCulture, "  Delete [green]{0}[/] for PluginType [bold]{1}[/]", "PluginStep", pluginType.Name);
                service.Delete(SdkMessageProcessingStep.EntityLogicalName, dependency.DependentComponentObjectId!.Value);
            }
        }
        service.Delete(pluginType.TypeCode, pluginType.Id);
        return pluginType;
    }

    private static void UpdatePluginType(PluginType pluginType, IOrganizationService service)
    {
        if (!string.IsNullOrWhiteSpace(pluginType.CustomApi))
        {
            var query = new QueryExpression
            {
                EntityName = CustomAPI.EntityLogicalName,
                ColumnSet = new ColumnSet(CustomAPI.LogicalNames.UniqueName, CustomAPI.LogicalNames.PluginTypeId),
                NoLock = true,
                Criteria = new FilterExpression(LogicalOperator.And)
                {
                    Conditions = { new ConditionExpression(CustomAPI.LogicalNames.PluginTypeId, ConditionOperator.Equal, pluginType.Id) }
                }
            };
            var apis = service.RetrieveMultiple(query).Entities.Select(e => e.ToEntity<CustomAPI>());
            var missing = true;
            foreach (var api in apis)
            {
                if (string.Compare(api.UniqueName, pluginType.CustomApi, StringComparison.OrdinalIgnoreCase) != 0)
                {
                    AnsiConsole.Markup(CultureInfo.InvariantCulture, "  Unlink PluginType [green]{0}[/] from Custom API [bold]{1}[/]", pluginType.Name, api.UniqueName!);
                    service.Update(new CustomAPI(api.Id)
                    {
                        PluginTypeId = null
                    });
                }
                else
                {
                    AnsiConsole.Markup(CultureInfo.InvariantCulture, "  Match PluginType [green]{0}[/] for Custom API [bold]{1}[/]", pluginType.Name, api.UniqueName!);
                    missing = false;
                }
                AnsiConsole.MarkupLine(CultureInfo.InvariantCulture, " -> Id [italic]{0:D}[/]", api.Id);
            }
            if (missing)
            {
                query = new QueryExpression
                {
                    EntityName = CustomAPI.EntityLogicalName,
                    ColumnSet = new ColumnSet(CustomAPI.LogicalNames.UniqueName),
                    NoLock = true,
                    Criteria = new FilterExpression(LogicalOperator.And)
                    {
                        Conditions = { new ConditionExpression(CustomAPI.LogicalNames.UniqueName, ConditionOperator.Equal, pluginType.CustomApi) }
                    }
                };
                var api = service.RetrieveMultiple(query).Entities.Select(e => e.ToEntity<CustomAPI>()).Single();
                AnsiConsole.Markup("  Link PluginType [green]{0}[/] to Custom API [bold]{1}[/]", pluginType.Name, api.UniqueName!);
                service.Update(new CustomAPI(api.Id)
                {
                    PluginTypeId = new EntityReference(dataverse.PluginType.EntityLogicalName, pluginType.Id)
                });
                AnsiConsole.MarkupLine(CultureInfo.InvariantCulture, " -> Id [italic]{0:D}[/]", api.Id);
            }
        }
    }

    public static Assembly UpsertAndPurgeWorkflowTypes(Assembly dll, Assembly crm, IOrganizationService service)
    {
        // New
        foreach (var newType in dll.WorkflowTypes.Where(d => crm.WorkflowTypes.All(t => t.TypeName != d.TypeName)))
        {
            crm.WorkflowTypes.Add(CreateWorkflowType(crm, newType, service));
        }
        // Purge
        foreach (var oldType in crm.WorkflowTypes.ToList().Where(t => dll.WorkflowTypes.All(d => d.TypeName != t.TypeName)))
        {
            crm.WorkflowTypes.Remove(DeleteWorkflowType(oldType, service));
        }
        return crm;
    }

    private static WorkflowType CreateWorkflowType(Assembly crm, WorkflowType workflowType, IOrganizationService service)
    {
        var type = new dataverse.PluginType
        {
            PluginAssemblyId = new EntityReference(workflowType.ParentTypeCode, crm.Id),
            Name = workflowType.Name,
            TypeName = workflowType.TypeName,
            FriendlyName = workflowType.FriendlyName,
            WorkflowActivityGroupName = workflowType.WorkflowActivityGroupName
        };
        AnsiConsole.Markup(" Create WorkflowType [green]{0}[/] in WorkflowActivityGroupName [bold]{1}[/] for Assembly {2}", workflowType.Name, workflowType.WorkflowActivityGroupName, workflowType.ParentName!);
        type.Id = service.Create(type);
        AnsiConsole.MarkupLine(CultureInfo.InvariantCulture, " -> Id [italic]{0:D}[/]", type.Id);

        return new WorkflowType
        {
            ParentId = crm.Id,
            ParentName = workflowType.ParentName,
            Id = type.Id,
            Name = workflowType.Name,
            TypeName = workflowType.TypeName,
            FriendlyName = workflowType.FriendlyName,
            WorkflowActivityGroupName = workflowType.WorkflowActivityGroupName
        };
    }

    private static WorkflowType DeleteWorkflowType(WorkflowType workflowType, IOrganizationService service)
    {
        AnsiConsole.MarkupLine(CultureInfo.InvariantCulture, " Delete PluginType [green]{0}[/] in WorkflowActivityGroupName [bold]{1}[/] for Assembly {2}", workflowType.Name, workflowType.WorkflowActivityGroupName, workflowType.ParentName!);
        service.Delete(workflowType.TypeCode, workflowType.Id);
        return workflowType;
    }
    #endregion

    #region PluginStep
    public static Assembly UpsertAndPurgePluginSteps(Assembly dll, Assembly crm, IOrganizationService service, string solution)
    {
        foreach (var dllPluginType in dll.PluginTypes)
        {
            var crmPluginType = crm.PluginTypes.Single(e => e.Equals(dllPluginType));
            if (!dllPluginType.PluginSteps.Any())
            {
                AnsiConsole.MarkupLine(CultureInfo.InvariantCulture, "  No PluginSteps (Custom API): [green]{0}[/]", dllPluginType.Name);
                continue;
            }
            // Update
            foreach (var updateStep in dllPluginType.PluginSteps.Where(p => crmPluginType.PluginSteps.Contains(p)))
            {
                UpdatePluginStep(updateStep, crmPluginType.PluginSteps.Single(e => e.Equals(updateStep)), service);
            }
            // New
            foreach (var newStep in dllPluginType.PluginSteps.Where(d => crmPluginType.PluginSteps.All(t => !t.Equals(d))))
            {
                crmPluginType.PluginSteps.Add(CreatePluginStep(crmPluginType, newStep, service, solution));
            }
            // Purge
            foreach (var oldStep in crmPluginType.PluginSteps.ToList().Where(t => dllPluginType.PluginSteps.All(d => !d.Equals(t))))
            {
                crmPluginType.PluginSteps.Remove(DeletePluginStep(oldStep, service));
            }
        }
        return crm;
    }

    private static PluginStep CreatePluginStep(PluginType pluginType, PluginStep pluginStep, IOrganizationService service, string solution)
    {
        var step = new SdkMessageProcessingStep
        {
            Stage = new OptionSetValue(pluginStep.Stage),
            SdkMessageId = new EntityReference(pluginStep.MessageTypeCode, pluginStep.MessageId),
            EventHandler = new EntityReference(pluginStep.ParentTypeCode, pluginType.Id),
            Rank = pluginStep.ExecutionOrder,
            Name = pluginStep.Name,
            Mode = new OptionSetValue(pluginStep.Mode),
            AsyncAutoDelete = SdkMessageProcessingStep.Options.Mode.Asynchronous == pluginStep.Mode,
            FilteringAttributesField = (pluginStep.FilterAttributes == null || pluginStep.FilterAttributes.Length < 1) ? null : string.Join(",", pluginStep.FilterAttributes),
            Configuration = pluginStep.Configuration
        };

        AnsiConsole.MarkupLine(CultureInfo.InvariantCulture, "  Validate PluginStep [green]{0}[/]", step.Name);
        Validator.Validate(step);

        if (!Guid.Empty.Equals(pluginStep.MessageFilterId))
        {
            step.SdkMessageFilterId = new EntityReference(pluginStep.MessageFilterTypeCode, pluginStep.MessageFilterId);
        }

        try
        {
            AnsiConsole.Markup("  Create PluginStep: [green]{0}[/]", step.Name);
            step.Id = service.Create(step);
            AnsiConsole.MarkupLine(CultureInfo.InvariantCulture, " -> Id [italic]{0:D}[/]", step.Id);
        }
        catch (Exception ex)
        {
            throw new Exception("The Plugin Registration was aborted. Maybe you try to register a Async with Pre. Check your Plugin dll and try again. Plugin Step: " + step.Name, ex.RootException());
        }

        if (!string.IsNullOrWhiteSpace(solution))
        {
            // SDKMessageProcessingStep = 92
            var addReq = new AddSolutionComponentRequest
            {
                AddRequiredComponents = false,
                ComponentType = 92,
                ComponentId = step.Id,
                SolutionUniqueName = solution
            };

            try
            {
                AnsiConsole.MarkupLine(CultureInfo.InvariantCulture, "  Add PluginStep [green]{0}[/] to Solution [bold]{1}[/]", step.Name, solution);
                service.Execute(addReq);
            }
            catch (Exception ex)
            {
                try
                {
                    service.Delete(PluginAssembly.EntityLogicalName, step.Id);
                }
                catch (Exception rb)
                {
                    AnsiConsole.MarkupLine(CultureInfo.InvariantCulture, "Rollback failed; cleanup manually");
                    AnsiConsole.WriteException(rb.RootException());
                }
                throw new Exception("The Plugin Registration was aborted. PluginStep: " + step.Name, ex.RootException());
            }
        }

        return new PluginStep
        {
            Id = step.Id,
            ParentId = pluginType.Id,
            ParentName = pluginStep.ParentName,
            Mode = pluginStep.Mode,
            MessageName = pluginStep.MessageName,
            Stage = pluginStep.Stage,
            PrimaryEntityName = pluginStep.PrimaryEntityName,
            SecondaryEntityName = pluginStep.SecondaryEntityName,
            FilterAttributes = pluginStep.FilterAttributes,
            ExecutionOrder = pluginStep.ExecutionOrder,
            Configuration = pluginStep.Configuration
        };
    }

    private static PluginStep DeletePluginStep(PluginStep pluginStep, IOrganizationService service)
    {
        using (var context = new DataContext(service))
        {
            AnsiConsole.MarkupLine(CultureInfo.InvariantCulture, "  Delete PluginStep [green]{0}[/]", pluginStep.Name);
            //del image
#pragma warning disable CS8602 // Dereference of a possibly null reference.
            (from mpsi in context.SdkMessageProcessingStepImageSet
             where mpsi.SdkMessageProcessingStepId.Equals(new EntityReference(pluginStep.TypeCode, pluginStep.Id))
             select mpsi).ToList().ForEach(e =>
             {
                 AnsiConsole.MarkupLine(CultureInfo.InvariantCulture, "   Delete PluginStepImage [bold]{0}[/] for PluginStep [green]{1}[/]", e.Name!, pluginStep.Name);
                 service.Delete(SdkMessageProcessingStepImage.EntityLogicalName, e.Id);
             });
#pragma warning restore CS8602 // Dereference of a possibly null reference.
        }
        service.Delete(pluginStep.TypeCode, pluginStep.Id);
        return pluginStep;
    }

    private static PluginStep UpdatePluginStep(PluginStep dllPluginStep, PluginStep crmPluginStep, IOrganizationService service)
    {
        AnsiConsole.MarkupLine(CultureInfo.InvariantCulture, "  Check PluginStep [green]{0}[/]", crmPluginStep.Name);

        var name = dllPluginStep.Name;

        var updatedStep = service.Retrieve(SdkMessageProcessingStep.EntityLogicalName, crmPluginStep.Id, new ColumnSet(true))?.ToEntity<SdkMessageProcessingStep>();
        if (updatedStep == null) return crmPluginStep;
        var updated = false;
        if (crmPluginStep.Name != name)
        {
            AnsiConsole.MarkupLine(CultureInfo.InvariantCulture, "   Rename PluginStep from [navy]{0}[/] to [green]{1}[/]", crmPluginStep.Name, name);
            updatedStep.Name = name;
            updated = true;
        }
        if (crmPluginStep.ExecutionOrder != dllPluginStep.ExecutionOrder)
        {
            AnsiConsole.MarkupLine(CultureInfo.InvariantCulture, "   Update ExecutionOrder from [navy]{0}[/] to [green]{1}[/] for PluginStep {2}", crmPluginStep.ExecutionOrder, dllPluginStep.ExecutionOrder, name);
            updatedStep.Rank = dllPluginStep.ExecutionOrder;
            crmPluginStep.ExecutionOrder = dllPluginStep.ExecutionOrder;
            updated = true;
        }

        var crmFilter = (crmPluginStep.FilterAttributes == null || crmPluginStep.FilterAttributes.Length < 1) ? null : string.Join(",", crmPluginStep.FilterAttributes);
        var dllFilter = (dllPluginStep.FilterAttributes == null || dllPluginStep.FilterAttributes.Length < 1) ? null : string.Join(",", dllPluginStep.FilterAttributes);

        if (!string.IsNullOrEmpty(crmFilter) && string.IsNullOrEmpty(dllFilter))
        {
            AnsiConsole.MarkupLine(CultureInfo.InvariantCulture, "   Update PluginStep [green]{0}[/] filters from '{1}' to <empty>", name, crmFilter);
            updatedStep.FilteringAttributesField = null;
            crmPluginStep.FilterAttributes = null;
            updated = true;
        }
        else if (string.IsNullOrEmpty(crmFilter) && !string.IsNullOrEmpty(dllFilter))
        {
            AnsiConsole.MarkupLine(CultureInfo.InvariantCulture, "   Update PluginStep [green]{0}[/] filters from <empty> to '{1}'", name, dllFilter);
            updatedStep.FilteringAttributesField = dllFilter;
            crmPluginStep.FilterAttributes = dllPluginStep.FilterAttributes;
            updated = true;
        }
        else if (!string.IsNullOrEmpty(crmFilter) && !string.IsNullOrEmpty(dllFilter))
        {
            var crmFilteringAttributes = crmFilter.Split(new[] { "," }, StringSplitOptions.RemoveEmptyEntries);
            var dllFilteringAttributes = dllPluginStep.FilterAttributes;
            Array.Sort(crmFilteringAttributes);
            Array.Sort(dllFilteringAttributes!);
            if (!string.Join(",", crmFilteringAttributes).Equals(string.Join(",", dllFilteringAttributes!), StringComparison.Ordinal))
            {
                AnsiConsole.MarkupLine(CultureInfo.InvariantCulture, "   Update PluginStep [green]{0}[/] filters from '{1}' to '{2}'", name, crmFilter, dllFilter);
                updatedStep.FilteringAttributesField = dllFilter;
                crmPluginStep.FilterAttributes = dllPluginStep.FilterAttributes;
                updated = true;
            }
        }

        if (!string.IsNullOrEmpty(crmPluginStep.Configuration) && string.IsNullOrEmpty(dllPluginStep.Configuration))
        {
            AnsiConsole.MarkupLine(CultureInfo.InvariantCulture, "   Update PluginStep [green]{0}[/] Configuration from '{1}' to <empty>", name, crmPluginStep.Configuration);
            updatedStep.Configuration = null;
            crmPluginStep.Configuration = null;
            updated = true;
        }
        else if (string.IsNullOrEmpty(crmPluginStep.Configuration) && !string.IsNullOrEmpty(dllPluginStep.Configuration))
        {
            AnsiConsole.MarkupLine(CultureInfo.InvariantCulture, "   Update PluginStep [green]{0}[/] Configuration from <empty> to '{1}'", name, dllPluginStep.Configuration);
            updatedStep.Configuration = dllPluginStep.Configuration;
            crmPluginStep.Configuration = dllPluginStep.Configuration;
            updated = true;
        }
        else if (!crmPluginStep.Configuration?.Equals(dllPluginStep.Configuration,StringComparison.Ordinal) ?? false)
        {
            AnsiConsole.MarkupLine(CultureInfo.InvariantCulture, "   Update PluginStep [green]{0}[/] Configuration '{1}' to '{2}'", name, crmPluginStep.Configuration, dllPluginStep.Configuration!);
            updatedStep.Configuration = dllPluginStep.Configuration;
            crmPluginStep.Configuration = dllPluginStep.Configuration;
            updated = true;
        }

        if (!updated) return crmPluginStep;

        AnsiConsole.MarkupLine(CultureInfo.InvariantCulture, "  Validate PluginStep [green]{0}[/]", updatedStep.Name!);
        Validator.Validate(updatedStep);

        AnsiConsole.MarkupLine(CultureInfo.InvariantCulture, "  Update PluginStep [green]{0}[/]", crmPluginStep.Name);
        service.Update(updatedStep);
        return crmPluginStep;
    }
    #endregion

    #region PluginStepImage

    public static Assembly UpsertAndPurgePluginStepImages(Assembly dll, Assembly crm, IOrganizationService service)
    {
        foreach (var dllPluginType in dll.PluginTypes)
        {
            var crmPluginType = crm.PluginTypes.Single(e => e.Equals(dllPluginType));
            if (!dllPluginType.PluginSteps.Any())
            {
                //Custom API
                continue;
            }
            foreach (var dllPluginStep in dllPluginType.PluginSteps.Where(p => crmPluginType.PluginSteps.Contains(p)))
            {
                var crmPluginStep = crmPluginType.PluginSteps.Single(e => e.Equals(dllPluginStep));
                // Update
                foreach (var updateStepImage in dllPluginStep.PluginStepImages.Where(p => crmPluginStep.PluginStepImages.Contains(p)))
                {
                    UpdatePluginStepImage(crmPluginStep, updateStepImage, crmPluginStep.PluginStepImages.Single(e => e.Equals(updateStepImage)), service);
                }
                // New
                foreach (var newStepImage in dllPluginStep.PluginStepImages.Where(d => crmPluginStep.PluginStepImages.All(t => !t.Equals(d))))
                {
                    crmPluginStep.PluginStepImages.Add(CreatePluginStepImage(crmPluginStep, newStepImage, service));
                }
                // Purge
                foreach (var oldStepImage in crmPluginStep.PluginStepImages.ToList().Where(t => dllPluginStep.PluginStepImages.All(d => !d.Equals(t))))
                {
                    crmPluginStep.PluginStepImages.Remove(DeletePluginStepImage(oldStepImage, service));
                }
            }
        }
        return crm;
    }

    private static PluginStepImage CreatePluginStepImage(PluginStep pluginStep, PluginStepImage pluginStepImage, IOrganizationService service)
    {
        var image = new SdkMessageProcessingStepImage
        {
            SdkMessageProcessingStepId = new EntityReference(pluginStepImage.ParentName, pluginStep.Id),
            ImageType = new OptionSetValue(pluginStepImage.ImageType),
            Name = pluginStepImage.Name,
            EntityAlias = pluginStepImage.EntityAlias,
            MessagePropertyName = pluginStepImage.MessagePropertyName,
            AttributesField = (pluginStepImage.Attributes == null || pluginStepImage.Attributes.Length < 1) ? null : string.Join(",", pluginStepImage.Attributes)
        };
        AnsiConsole.MarkupLine(CultureInfo.InvariantCulture, "   Validate PluginStepImage: [green]{0}[/] for [bold]{1}[/]", image.Name, pluginStepImage.ParentName!);
        Validator.ValidateImage(pluginStep.Name, pluginStep.MessageName, pluginStep.Stage, pluginStepImage.ImageType);
        AnsiConsole.Markup("   Create PluginStepImage: [green]{0}[/] for [bold]{1}[/]", image.Name, pluginStepImage.ParentName!);
        image.Id = service.Create(image);
        AnsiConsole.MarkupLine(CultureInfo.InvariantCulture, " -> Id [italic]{0:D}[/]", image.Id);

        pluginStepImage.Id = image.Id;
        pluginStepImage.ParentId = pluginStep.Id;
        return new PluginStepImage
        {
            ParentId = pluginStep.Id,
            ParentName = pluginStep.ParentName,
            Id = image.Id,
            Name = pluginStepImage.Name,
            EntityAlias = pluginStepImage.EntityAlias,
            ImageType = pluginStepImage.ImageType,
            Attributes = pluginStepImage.Attributes!,
            MessagePropertyName = pluginStepImage.MessagePropertyName
        };
    }

    private static PluginStepImage DeletePluginStepImage(PluginStepImage pluginStepImage, IOrganizationService service)
    {
        AnsiConsole.MarkupLine(CultureInfo.InvariantCulture, "   Delete PluginStepImage: [green]{0}[/] for [bold]{1}[/]", pluginStepImage.Name, pluginStepImage.ParentName!);
        service.Delete(pluginStepImage.TypeCode, pluginStepImage.Id);
        return pluginStepImage;
    }

    private static PluginStepImage UpdatePluginStepImage(PluginStep pluginStep, PluginStepImage dllPluginStepImage, PluginStepImage crmPluginStepImage, IOrganizationService service)
    {
        AnsiConsole.MarkupLine(CultureInfo.InvariantCulture, "   Check PluginStepImage: [green]{0}[/] for [bold]{1}[/]", crmPluginStepImage.Name, crmPluginStepImage.ParentName!);

        var crmFilter = crmPluginStepImage.Attributes;
        var dllFilter = dllPluginStepImage.Attributes;

        var updatedStepImage = service.Retrieve(SdkMessageProcessingStepImage.EntityLogicalName, crmPluginStepImage.Id, new ColumnSet(true))?.ToEntity<SdkMessageProcessingStepImage>();
        if (updatedStepImage == null) return crmPluginStepImage;
        var updated = false;

        if (!(crmFilter == null || crmFilter.Length < 1) && (dllFilter == null || dllFilter.Length < 1))
        {
            AnsiConsole.MarkupLine(CultureInfo.InvariantCulture, "    Update PreImage filters from [green]{0}[/] to <empty>", string.Join(",", crmFilter));
            updatedStepImage.AttributesField = null;
            crmPluginStepImage.Attributes = null;
            updated = true;
        }
        else if ((crmFilter == null || crmFilter.Length < 1) && !(dllFilter == null || dllFilter.Length < 1))
        {
            AnsiConsole.MarkupLine(CultureInfo.InvariantCulture, "    Update PreImage filters from <empty> to [green]{0}[/]", string.Join(",", dllFilter));
            updatedStepImage.AttributesField = string.Join(",", dllFilter);
            crmPluginStepImage.Attributes = dllFilter;
            updated = true;
        }
        else if (!(crmFilter == null || crmFilter.Length < 1) && !(dllFilter == null || dllFilter.Length < 1))
        {
            Array.Sort(crmFilter);
            Array.Sort(dllFilter);
            if (!string.Join(",", crmFilter).Equals(string.Join(",", dllFilter), StringComparison.Ordinal))
            {
                AnsiConsole.MarkupLine(CultureInfo.InvariantCulture, "    Update PreImage filters from [navy]{0}[/] to [green]{1}[/]", string.Join(",", crmFilter), string.Join(",", dllFilter));
                updatedStepImage.AttributesField = string.Join(",", dllFilter);
                crmPluginStepImage.Attributes = dllFilter;
                updated = true;
            }
        }
        if (!updated) return crmPluginStepImage;
        AnsiConsole.Markup("   Validate PluginStepImage: [green]{0}[/] for [bold]{1}[/]", crmPluginStepImage.Name, crmPluginStepImage.ParentName!);
        Validator.ValidateImage(pluginStep.Name, pluginStep.MessageName, pluginStep.Stage, updatedStepImage.ImageType!.Value);
        AnsiConsole.MarkupLine(CultureInfo.InvariantCulture, "   Update PluginStepImage: [green]{0}[/] for [bold]{1}[/]", crmPluginStepImage.Name, crmPluginStepImage.ParentName!);
        service.Update(updatedStepImage);
        return crmPluginStepImage;
    }

    #endregion
}
