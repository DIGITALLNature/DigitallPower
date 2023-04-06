using System.Globalization;
using dgt.power.common.Extensions;
using dgt.power.dataverse;
using dgt.power.push.Model;
using Microsoft.Crm.Sdk.Messages;
using Microsoft.Xrm.Sdk;
using Microsoft.Xrm.Sdk.Client;
using Microsoft.Xrm.Sdk.Query;
using Spectre.Console;
using PluginType = dgt.power.push.Model.PluginType;

namespace dgt.power.push.Logic;

internal class AssemblyProcessor
{
    private readonly DataContext _context;
    private readonly IOrganizationService _service;

    public AssemblyProcessor(IOrganizationService service)
    {
        _service = service;
        _context = new DataContext(_service) { MergeOption = MergeOption.NoTracking };
    }


    #region PluginPackage

    public Package CreatePluginPackage(Package packageLocal, string solutionPrefix)
    {
        var package = new PluginPackage
        {
            Name = $"{solutionPrefix}_{packageLocal.Name}",
            Content = packageLocal.Content,
            Version = packageLocal.Version
        };

        AnsiConsole.Markup(CultureInfo.InvariantCulture, "Create Package [green]{0} ({1})[/]", package.Name,
            package.Version);
        package.Id = _service.Create(package);
        AnsiConsole.MarkupLine(CultureInfo.InvariantCulture, " -> Id [italic]{0:D}[/]", package.Id);

        return new Package
        {
            Name = package.Name,
            Version = package.Version,
            Content = package.Content,
            Id = package.Id
        };
    }

    public Package UpdatePluginPackage(Guid packageCrmId, Package packageLocal)
    {
        var package = new PluginPackage
        {
            Id = packageCrmId,
            Content = packageLocal.Content,
            Version = packageLocal.Version
        };

        AnsiConsole.MarkupLine(CultureInfo.InvariantCulture, "Update Package [green]{0} ({1})[/]", packageLocal.Name, package.Version);
       _service.Update(package);

        return new Package
        {
            Name = package.Name,
            Version = package.Version,
            Content = package.Content,
            Id = package.Id
        };
    }

    #endregion

    #region PluginAssembly

    public Model.Assembly CreatePluginAssembly(Model.Assembly dll, string solution)
    {
        var pluginAssembly = new PluginAssembly
        {
            SourceType = new OptionSetValue(dll.SourceType),
            IsolationMode = new OptionSetValue(dll.IsolationMode),
            Name = dll.Name,
            Content = dll.Content
        };

        AnsiConsole.Markup(CultureInfo.InvariantCulture, "Create Assembly [green]{0} ({1})[/]", pluginAssembly.Name,
            dll.Version);
        pluginAssembly.Id = _service.Create(pluginAssembly);
        AnsiConsole.MarkupLine(CultureInfo.InvariantCulture, " -> Id [italic]{0:D}[/]", pluginAssembly.Id);

        if (!string.IsNullOrWhiteSpace(solution))
        {
            AddPluginAssemblyToSolution(pluginAssembly, solution);
        }

        return new Model.Assembly
        {
            Name = pluginAssembly.Name,
            Version = dll.Version,
            Content = dll.Content,
            Id = pluginAssembly.Id
        };
    }

    public Model.Assembly UpdatePluginAssembly(Model.Assembly dll, Model.Assembly crm, string solution)
    {
        AnsiConsole.MarkupLine(CultureInfo.InvariantCulture, "Update Assembly [green]{0}[/] [italic]({1} -> {2})[/]",
            crm.Name, crm.Version, dll.Version);
        //purge missing types first to avoid "PluginType [xxx] not found in PluginAssembly"
        foreach (var oldType in crm.PluginTypes.ToList().Where(t => dll.PluginTypes.All(d => d.TypeName != t.TypeName)))
        {
            crm.PluginTypes.Remove(DeletePluginType(oldType));
        }

        var pluginAssembly = new PluginAssembly(crm.Id)
        {
            Content = dll.Content
        };
        _service.Update(pluginAssembly);
        if (!string.IsNullOrWhiteSpace(solution) && !crm.Solutions.Contains(solution))
        {
            AddPluginAssemblyToSolution(pluginAssembly, solution);
        }

        crm.Content = dll.Content;
        crm.Version = dll.Version;
        return crm;
    }

    private void AddPluginAssemblyToSolution(PluginAssembly pluginAssembly, string solution)
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
            AnsiConsole.MarkupLine(CultureInfo.InvariantCulture, "Add Assembly [green]{0}[/] to Solution [bold]{1}[/]",
                pluginAssembly.Name!, solution);
            _service.Execute(addReq);
        }
        catch (Exception ex)
        {
            try
            {
                _service.Delete(PluginAssembly.EntityLogicalName, pluginAssembly.Id);
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

    public Model.Assembly UpsertAndPurgePluginTypes(Model.Assembly dll, Model.Assembly crm, string solution)
    {
        // Update
        foreach (var updateType in dll.PluginTypes.Where(t => crm.PluginTypes.Contains(t)))
        {
            updateType.Id = crm.PluginTypes.Single(e => e.Equals(updateType)).Id;
            UpdatePluginType(updateType);
        }

        // New
        foreach (var newType in dll.PluginTypes.Where(d => crm.PluginTypes.All(t => t.TypeName != d.TypeName)))
        {
            crm.PluginTypes.Add(CreatePluginType(crm, newType, solution));
        }

        return crm;
    }

    private PluginType CreatePluginType(Model.Assembly crm, PluginType pluginType, string solution)
    {
        var type = new dataverse.PluginType
        {
            PluginAssemblyId = new EntityReference(pluginType.ParentTypeCode, crm.Id),
            Name = pluginType.Name,
            TypeName = pluginType.TypeName,
            FriendlyName = pluginType.FriendlyName
        };
        AnsiConsole.Markup(CultureInfo.InvariantCulture, " Create PluginType [green]{0}[/] for Assembly [bold]{1}[/]",
            pluginType.Name, pluginType.ParentName!);
        type.Id = _service.Create(type);
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
                    Conditions =
                    {
                        new ConditionExpression(CustomAPI.LogicalNames.UniqueName, ConditionOperator.Equal,
                            pluginType.CustomApi)
                    }
                }
            };
            var api = _service.RetrieveMultiple(query).Entities.Select(e => e.ToEntity<CustomAPI>()).Single();
            AnsiConsole.Markup(CultureInfo.InvariantCulture,
                "  Link PluginType [green]{0}[/] to Custom API [bold]{1}[/]", pluginType.Name, api.UniqueName!);
            _service.Update(new CustomAPI(api.Id)
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

    private PluginType DeletePluginType(PluginType pluginType)
    {
        AnsiConsole.MarkupLine(CultureInfo.InvariantCulture,
            " Delete PluginType [green]{0}[/] for Assembly [bold]{1}[/] first", pluginType.Name,
            pluginType.ParentName!);
        var request = new RetrieveDependenciesForDeleteRequest
        {
            ComponentType = 90, //PluginType
            ObjectId = pluginType.Id
        };
        var response = (RetrieveDependenciesForDeleteResponse)_service.Execute(request);
        foreach (var dependency in response.EntityCollection.Entities.Select(e => e.ToEntity<Dependency>()))
        {
            if (dependency.DependentComponentType!.Value == 92) //SDK Message Processing Step
            {
                AnsiConsole.MarkupLine(CultureInfo.InvariantCulture,
                    "  Delete [green]{0}[/] for PluginType [bold]{1}[/]", "PluginStep", pluginType.Name);
                _service.Delete(SdkMessageProcessingStep.EntityLogicalName,
                    dependency.DependentComponentObjectId!.Value);
            }
        }

        _service.Delete(pluginType.TypeCode, pluginType.Id);
        return pluginType;
    }

    private void UpdatePluginType(PluginType pluginType)
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
                    Conditions =
                    {
                        new ConditionExpression(CustomAPI.LogicalNames.PluginTypeId, ConditionOperator.Equal,
                            pluginType.Id)
                    }
                }
            };
            var apis = _service.RetrieveMultiple(query).Entities.Select(e => e.ToEntity<CustomAPI>());
            var missing = true;
            foreach (var api in apis)
            {
                if (string.Compare(api.UniqueName, pluginType.CustomApi, StringComparison.OrdinalIgnoreCase) != 0)
                {
                    AnsiConsole.Markup(CultureInfo.InvariantCulture,
                        "  Unlink PluginType [green]{0}[/] from Custom API [bold]{1}[/]", pluginType.Name,
                        api.UniqueName!);
                    _service.Update(new CustomAPI(api.Id)
                    {
                        PluginTypeId = null
                    });
                }
                else
                {
                    AnsiConsole.Markup(CultureInfo.InvariantCulture,
                        "  Match PluginType [green]{0}[/] for Custom API [bold]{1}[/]", pluginType.Name,
                        api.UniqueName!);
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
                        Conditions =
                        {
                            new ConditionExpression(CustomAPI.LogicalNames.UniqueName, ConditionOperator.Equal,
                                pluginType.CustomApi)
                        }
                    }
                };
                var api = _service.RetrieveMultiple(query).Entities.Select(e => e.ToEntity<CustomAPI>()).Single();
                AnsiConsole.Markup("  Link PluginType [green]{0}[/] to Custom API [bold]{1}[/]", pluginType.Name,
                    api.UniqueName!);
                _service.Update(new CustomAPI(api.Id)
                {
                    PluginTypeId = new EntityReference(dataverse.PluginType.EntityLogicalName, pluginType.Id)
                });
                AnsiConsole.MarkupLine(CultureInfo.InvariantCulture, " -> Id [italic]{0:D}[/]", api.Id);
            }
        }
    }

    public Model.Assembly UpsertAndPurgeWorkflowTypes(Model.Assembly dll, Model.Assembly crm)
    {
        // New
        foreach (var newType in dll.WorkflowTypes.Where(d => crm.WorkflowTypes.All(t => t.TypeName != d.TypeName)))
        {
            crm.WorkflowTypes.Add(CreateWorkflowType(crm, newType));
        }

        // Purge
        foreach (var oldType in crm.WorkflowTypes.ToList()
                     .Where(t => dll.WorkflowTypes.All(d => d.TypeName != t.TypeName)))
        {
            crm.WorkflowTypes.Remove(DeleteWorkflowType(oldType));
        }

        return crm;
    }

    private WorkflowType CreateWorkflowType(Model.Assembly crm, WorkflowType workflowType)
    {
        var type = new dataverse.PluginType
        {
            PluginAssemblyId = new EntityReference(workflowType.ParentTypeCode, crm.Id),
            Name = workflowType.Name,
            TypeName = workflowType.TypeName,
            FriendlyName = workflowType.FriendlyName,
            WorkflowActivityGroupName = workflowType.WorkflowActivityGroupName
        };
        AnsiConsole.Markup(
            " Create WorkflowType [green]{0}[/] in WorkflowActivityGroupName [bold]{1}[/] for Assembly {2}",
            workflowType.Name, workflowType.WorkflowActivityGroupName, workflowType.ParentName!);
        type.Id = _service.Create(type);
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

    private WorkflowType DeleteWorkflowType(WorkflowType workflowType)
    {
        AnsiConsole.MarkupLine(CultureInfo.InvariantCulture,
            " Delete PluginType [green]{0}[/] in WorkflowActivityGroupName [bold]{1}[/] for Assembly {2}",
            workflowType.Name, workflowType.WorkflowActivityGroupName, workflowType.ParentName!);
        _service.Delete(workflowType.TypeCode, workflowType.Id);
        return workflowType;
    }

    #endregion

    #region PluginStep

    public Model.Assembly UpsertAndPurgePluginSteps(Model.Assembly dll, Model.Assembly crm, string solution)
    {
        foreach (var dllPluginType in dll.PluginTypes)
        {
            var crmPluginType = crm.PluginTypes.Single(e => e.Equals(dllPluginType));
            if (!dllPluginType.PluginSteps.Any())
            {
                AnsiConsole.MarkupLine(CultureInfo.InvariantCulture, "  No PluginSteps (Custom API): [green]{0}[/]",
                    dllPluginType.Name);
                continue;
            }

            // Update
            foreach (var updateStep in dllPluginType.PluginSteps.Where(p => crmPluginType.PluginSteps.Contains(p)))
            {
                UpdatePluginStep(updateStep, crmPluginType.PluginSteps.Single(e => e.Equals(updateStep)));
            }

            // New
            foreach (var newStep in dllPluginType.PluginSteps.Where(d =>
                         crmPluginType.PluginSteps.All(t => !t.Equals(d))))
            {
                crmPluginType.PluginSteps.Add(CreatePluginStep(crmPluginType, newStep, solution));
            }

            // Purge
            foreach (var oldStep in crmPluginType.PluginSteps.ToList()
                         .Where(t => dllPluginType.PluginSteps.All(d => !d.Equals(t))))
            {
                crmPluginType.PluginSteps.Remove(DeletePluginStep(oldStep));
            }
        }

        return crm;
    }

    private PluginStep CreatePluginStep(PluginType pluginType, PluginStep pluginStep, string solution)
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
            FilteringAttributesField = pluginStep.FilterAttributes == null || pluginStep.FilterAttributes.Length < 1
                ? null
                : string.Join(",", pluginStep.FilterAttributes),
            Configuration = pluginStep.Configuration
        };

        AnsiConsole.MarkupLine(CultureInfo.InvariantCulture, "  Validate PluginStep [green]{0}[/]", step.Name);
        AssemblyValidator.Validate(step);

        if (!Guid.Empty.Equals(pluginStep.MessageFilterId))
        {
            step.SdkMessageFilterId = new EntityReference(pluginStep.MessageFilterTypeCode, pluginStep.MessageFilterId);
        }

        try
        {
            AnsiConsole.Markup("  Create PluginStep: [green]{0}[/]", step.Name);
            step.Id = _service.Create(step);
            AnsiConsole.MarkupLine(CultureInfo.InvariantCulture, " -> Id [italic]{0:D}[/]", step.Id);
        }
        catch (Exception ex)
        {
            throw new Exception(
                "The Plugin Registration was aborted. Maybe you try to register a Async with Pre. Check your Plugin dll and try again. Plugin Step: " +
                step.Name, ex.RootException());
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
                AnsiConsole.MarkupLine(CultureInfo.InvariantCulture,
                    "  Add PluginStep [green]{0}[/] to Solution [bold]{1}[/]", step.Name, solution);
                _service.Execute(addReq);
            }
            catch (Exception ex)
            {
                try
                {
                    _service.Delete(PluginAssembly.EntityLogicalName, step.Id);
                }
                catch (Exception rb)
                {
                    AnsiConsole.MarkupLine(CultureInfo.InvariantCulture, "Rollback failed; cleanup manually");
                    AnsiConsole.WriteException(rb.RootException());
                }

                throw new Exception("The Plugin Registration was aborted. PluginStep: " + step.Name,
                    ex.RootException());
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

    private PluginStep DeletePluginStep(PluginStep pluginStep)
    {
        AnsiConsole.MarkupLine(CultureInfo.InvariantCulture, "  Delete PluginStep [green]{0}[/]", pluginStep.Name);
        //del image
#pragma warning disable CS8602 // Dereference of a possibly null reference.
        (from mpsi in _context.SdkMessageProcessingStepImageSet
            where mpsi.SdkMessageProcessingStepId.Equals(new EntityReference(pluginStep.TypeCode, pluginStep.Id))
            select mpsi).ToList().ForEach(e =>
        {
            AnsiConsole.MarkupLine(CultureInfo.InvariantCulture,
                "   Delete PluginStepImage [bold]{0}[/] for PluginStep [green]{1}[/]", e.Name!, pluginStep.Name);
            _service.Delete(SdkMessageProcessingStepImage.EntityLogicalName, e.Id);
        });
#pragma warning restore CS8602 // Dereference of a possibly null reference.


        _service.Delete(pluginStep.TypeCode, pluginStep.Id);
        return pluginStep;
    }

    private PluginStep UpdatePluginStep(PluginStep dllPluginStep, PluginStep crmPluginStep)
    {
        AnsiConsole.MarkupLine(CultureInfo.InvariantCulture, "  Check PluginStep [green]{0}[/]", crmPluginStep.Name);

        var name = dllPluginStep.Name;

        var updatedStep = _service
            .Retrieve(SdkMessageProcessingStep.EntityLogicalName, crmPluginStep.Id, new ColumnSet(true))
            ?.ToEntity<SdkMessageProcessingStep>();
        if (updatedStep == null)
        {
            return crmPluginStep;
        }

        var updated = false;
        if (crmPluginStep.Name != name)
        {
            AnsiConsole.MarkupLine(CultureInfo.InvariantCulture,
                "   Rename PluginStep from [navy]{0}[/] to [green]{1}[/]", crmPluginStep.Name, name);
            updatedStep.Name = name;
            updated = true;
        }

        if (crmPluginStep.ExecutionOrder != dllPluginStep.ExecutionOrder)
        {
            AnsiConsole.MarkupLine(CultureInfo.InvariantCulture,
                "   Update ExecutionOrder from [navy]{0}[/] to [green]{1}[/] for PluginStep {2}",
                crmPluginStep.ExecutionOrder, dllPluginStep.ExecutionOrder, name);
            updatedStep.Rank = dllPluginStep.ExecutionOrder;
            crmPluginStep.ExecutionOrder = dllPluginStep.ExecutionOrder;
            updated = true;
        }

        var crmFilter = crmPluginStep.FilterAttributes == null || crmPluginStep.FilterAttributes.Length < 1
            ? null
            : string.Join(",", crmPluginStep.FilterAttributes);
        var dllFilter = dllPluginStep.FilterAttributes == null || dllPluginStep.FilterAttributes.Length < 1
            ? null
            : string.Join(",", dllPluginStep.FilterAttributes);

        if (!string.IsNullOrEmpty(crmFilter) && string.IsNullOrEmpty(dllFilter))
        {
            AnsiConsole.MarkupLine(CultureInfo.InvariantCulture,
                "   Update PluginStep [green]{0}[/] filters from '{1}' to <empty>", name, crmFilter);
            updatedStep.FilteringAttributesField = null;
            crmPluginStep.FilterAttributes = null;
            updated = true;
        }
        else if (string.IsNullOrEmpty(crmFilter) && !string.IsNullOrEmpty(dllFilter))
        {
            AnsiConsole.MarkupLine(CultureInfo.InvariantCulture,
                "   Update PluginStep [green]{0}[/] filters from <empty> to '{1}'", name, dllFilter);
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
            if (!string.Join(",", crmFilteringAttributes)
                    .Equals(string.Join(",", dllFilteringAttributes!), StringComparison.Ordinal))
            {
                AnsiConsole.MarkupLine(CultureInfo.InvariantCulture,
                    "   Update PluginStep [green]{0}[/] filters from '{1}' to '{2}'", name, crmFilter, dllFilter);
                updatedStep.FilteringAttributesField = dllFilter;
                crmPluginStep.FilterAttributes = dllPluginStep.FilterAttributes;
                updated = true;
            }
        }

        if (!string.IsNullOrEmpty(crmPluginStep.Configuration) && string.IsNullOrEmpty(dllPluginStep.Configuration))
        {
            AnsiConsole.MarkupLine(CultureInfo.InvariantCulture,
                "   Update PluginStep [green]{0}[/] Configuration from '{1}' to <empty>", name,
                crmPluginStep.Configuration);
            updatedStep.Configuration = null;
            crmPluginStep.Configuration = null;
            updated = true;
        }
        else if (string.IsNullOrEmpty(crmPluginStep.Configuration) &&
                 !string.IsNullOrEmpty(dllPluginStep.Configuration))
        {
            AnsiConsole.MarkupLine(CultureInfo.InvariantCulture,
                "   Update PluginStep [green]{0}[/] Configuration from <empty> to '{1}'", name,
                dllPluginStep.Configuration);
            updatedStep.Configuration = dllPluginStep.Configuration;
            crmPluginStep.Configuration = dllPluginStep.Configuration;
            updated = true;
        }
        else if (!crmPluginStep.Configuration?.Equals(dllPluginStep.Configuration, StringComparison.Ordinal) ?? false)
        {
            AnsiConsole.MarkupLine(CultureInfo.InvariantCulture,
                "   Update PluginStep [green]{0}[/] Configuration '{1}' to '{2}'", name, crmPluginStep.Configuration,
                dllPluginStep.Configuration!);
            updatedStep.Configuration = dllPluginStep.Configuration;
            crmPluginStep.Configuration = dllPluginStep.Configuration;
            updated = true;
        }

        if (!updated)
        {
            return crmPluginStep;
        }

        AnsiConsole.MarkupLine(CultureInfo.InvariantCulture, "  Validate PluginStep [green]{0}[/]", updatedStep.Name!);
        AssemblyValidator.Validate(updatedStep);

        AnsiConsole.MarkupLine(CultureInfo.InvariantCulture, "  Update PluginStep [green]{0}[/]", crmPluginStep.Name);
        _service.Update(updatedStep);
        return crmPluginStep;
    }

    #endregion

    #region PluginStepImage

    public Model.Assembly UpsertAndPurgePluginStepImages(Model.Assembly dll, Model.Assembly crm)
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
                foreach (var updateStepImage in dllPluginStep.PluginStepImages.Where(p =>
                             crmPluginStep.PluginStepImages.Contains(p)))
                {
                    UpdatePluginStepImage(crmPluginStep, updateStepImage,
                        crmPluginStep.PluginStepImages.Single(e => e.Equals(updateStepImage)));
                }

                // New
                foreach (var newStepImage in dllPluginStep.PluginStepImages.Where(d =>
                             crmPluginStep.PluginStepImages.All(t => !t.Equals(d))))
                {
                    crmPluginStep.PluginStepImages.Add(CreatePluginStepImage(crmPluginStep, newStepImage));
                }

                // Purge
                foreach (var oldStepImage in crmPluginStep.PluginStepImages.ToList()
                             .Where(t => dllPluginStep.PluginStepImages.All(d => !d.Equals(t))))
                {
                    crmPluginStep.PluginStepImages.Remove(DeletePluginStepImage(oldStepImage));
                }
            }
        }

        return crm;
    }

    private PluginStepImage CreatePluginStepImage(PluginStep pluginStep, PluginStepImage pluginStepImage)
    {
        var image = new SdkMessageProcessingStepImage
        {
            SdkMessageProcessingStepId = new EntityReference(pluginStepImage.ParentName, pluginStep.Id),
            ImageType = new OptionSetValue(pluginStepImage.ImageType),
            Name = pluginStepImage.Name,
            EntityAlias = pluginStepImage.EntityAlias,
            MessagePropertyName = pluginStepImage.MessagePropertyName,
            AttributesField = pluginStepImage.Attributes == null || pluginStepImage.Attributes.Length < 1
                ? null
                : string.Join(",", pluginStepImage.Attributes)
        };
        AnsiConsole.MarkupLine(CultureInfo.InvariantCulture,
            "   Validate PluginStepImage: [green]{0}[/] for [bold]{1}[/]", image.Name, pluginStepImage.ParentName!);
        AssemblyValidator.ValidateImage(pluginStep.Name, pluginStep.MessageName, pluginStep.Stage, pluginStepImage.ImageType);
        AnsiConsole.Markup("   Create PluginStepImage: [green]{0}[/] for [bold]{1}[/]", image.Name,
            pluginStepImage.ParentName!);
        image.Id = _service.Create(image);
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

    private PluginStepImage DeletePluginStepImage(PluginStepImage pluginStepImage)
    {
        AnsiConsole.MarkupLine(CultureInfo.InvariantCulture,
            "   Delete PluginStepImage: [green]{0}[/] for [bold]{1}[/]", pluginStepImage.Name,
            pluginStepImage.ParentName!);
        _service.Delete(pluginStepImage.TypeCode, pluginStepImage.Id);
        return pluginStepImage;
    }

    private PluginStepImage UpdatePluginStepImage(PluginStep pluginStep, PluginStepImage dllPluginStepImage,
        PluginStepImage crmPluginStepImage)
    {
        AnsiConsole.MarkupLine(CultureInfo.InvariantCulture, "   Check PluginStepImage: [green]{0}[/] for [bold]{1}[/]",
            crmPluginStepImage.Name, crmPluginStepImage.ParentName!);

        var crmFilter = crmPluginStepImage.Attributes;
        var dllFilter = dllPluginStepImage.Attributes;

        var updatedStepImage = _service
            .Retrieve(SdkMessageProcessingStepImage.EntityLogicalName, crmPluginStepImage.Id, new ColumnSet(true))
            ?.ToEntity<SdkMessageProcessingStepImage>();
        if (updatedStepImage == null)
        {
            return crmPluginStepImage;
        }

        var updated = false;

        if (!(crmFilter == null || crmFilter.Length < 1) && (dllFilter == null || dllFilter.Length < 1))
        {
            AnsiConsole.MarkupLine(CultureInfo.InvariantCulture,
                "    Update PreImage filters from [green]{0}[/] to <empty>", string.Join(",", crmFilter));
            updatedStepImage.AttributesField = null;
            crmPluginStepImage.Attributes = null;
            updated = true;
        }
        else if ((crmFilter == null || crmFilter.Length < 1) && !(dllFilter == null || dllFilter.Length < 1))
        {
            AnsiConsole.MarkupLine(CultureInfo.InvariantCulture,
                "    Update PreImage filters from <empty> to [green]{0}[/]", string.Join(",", dllFilter));
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
                AnsiConsole.MarkupLine(CultureInfo.InvariantCulture,
                    "    Update PreImage filters from [navy]{0}[/] to [green]{1}[/]", string.Join(",", crmFilter),
                    string.Join(",", dllFilter));
                updatedStepImage.AttributesField = string.Join(",", dllFilter);
                crmPluginStepImage.Attributes = dllFilter;
                updated = true;
            }
        }

        if (!updated)
        {
            return crmPluginStepImage;
        }

        AnsiConsole.Markup("   Validate PluginStepImage: [green]{0}[/] for [bold]{1}[/]", crmPluginStepImage.Name,
            crmPluginStepImage.ParentName!);
        AssemblyValidator.ValidateImage(pluginStep.Name, pluginStep.MessageName, pluginStep.Stage,
            updatedStepImage.ImageType!.Value);
        AnsiConsole.MarkupLine(CultureInfo.InvariantCulture,
            "   Update PluginStepImage: [green]{0}[/] for [bold]{1}[/]", crmPluginStepImage.Name,
            crmPluginStepImage.ParentName!);
        _service.Update(updatedStepImage);
        return crmPluginStepImage;
    }

    #endregion

    public string GetSolutionPrefix(string solution, string defaultValue = "new")
    {
        var prefix = (from s in _context.SolutionSet
            join p in _context.PublisherSet on s.PublisherId.Id equals p.Id
            where s.UniqueName == solution
            select p.CustomizationPrefix).SingleOrDefault();

        return prefix ?? defaultValue;
    }
}
