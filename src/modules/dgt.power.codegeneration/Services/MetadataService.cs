// Copyright (c) DIGITALL Nature. All rights reserved
// DIGITALL Nature licenses this file to you under the Microsoft Public License.

using System.Runtime.Caching;
using System.Text.RegularExpressions;
using System.Xml;
using System.Xml.Linq;
using dgt.power.codegeneration.Base;
using dgt.power.codegeneration.Logic;
using dgt.power.codegeneration.Model;
using dgt.power.codegeneration.Services.Contracts;
using dgt.power.codegeneration.Templates;
using dgt.power.dataverse;
using Microsoft.Xrm.Sdk;
using Microsoft.Xrm.Sdk.Messages;
using Microsoft.Xrm.Sdk.Metadata;
using Microsoft.Xrm.Sdk.Query;
using Spectre.Console;

namespace dgt.power.codegeneration.Services;

public class MetadataService : IMetadataService
{
    private readonly IOrganizationService _connection;
    private readonly ObjectCache _metadataCache;
    private readonly IAnsiConsole _console;
    private readonly Dictionary<string, List<string>> _usedTokens = new();

    public MetadataService(IOrganizationService connection, ObjectCache metadataCache, IAnsiConsole console)
    {
        _connection = connection;
        _metadataCache = metadataCache;
        _console = console;
    }

    public void PopulateEntitiesAndSolutions(CodeGenerationConfig config)
    {
        var entities = (RetrieveAllEntitiesResponse)_connection.Execute(new RetrieveAllEntitiesRequest
        {
            EntityFilters = EntityFilters.Entity, RetrieveAsIfPublished = true
        });

        var entitySet = config.Entities.ToHashSet();
        if (!string.IsNullOrWhiteSpace(config.EntityMask))
        {
            var regularPattern = WildCardToRegular(config.EntityMask);

            foreach (var metadata in entities.EntityMetadata)
            {
                if (Regex.IsMatch(metadata.LogicalName, regularPattern))
                {
                    entitySet.Add(metadata.LogicalName);
                }
            }
        }

        if (config.Solutions.Length != 0)
        {
            var componentsQuery = new QueryExpression
            {
                EntityName = SolutionComponent.EntityLogicalName,
                ColumnSet = new ColumnSet(SolutionComponent.LogicalNames.ObjectId),
                Criteria = new FilterExpression()
            };
            var solutionLink = new LinkEntity(SolutionComponent.EntityLogicalName,
                Solution.EntityLogicalName,
                SolutionComponent.LogicalNames.SolutionId,
                Solution.LogicalNames.SolutionId,
                JoinOperator.Inner);
            var solutionFilter = solutionLink.LinkCriteria = new FilterExpression();
            componentsQuery.LinkEntities.Add(solutionLink);
            componentsQuery.Criteria.AddCondition(new ConditionExpression(
                    SolutionComponent.LogicalNames.ComponentType,
                    ConditionOperator.Equal,
                    SolutionComponent.Options.ComponentType.Entity
                )
            );

            foreach (var solution in config.Solutions)
            {
                var solutionId = FetchSolution(solution)!.Id;
                solutionFilter.Conditions.Clear();
                solutionFilter.AddCondition(new ConditionExpression(SolutionComponent.LogicalNames.SolutionId,
                    ConditionOperator.Equal,
                    solutionId));

                var entityComponents = _connection.RetrieveMultiple(componentsQuery).Entities;

                foreach (var entityComponent in entityComponents)
                    entitySet.Add(
                        entities.EntityMetadata.Single(e => e.MetadataId == (Guid)entityComponent["objectid"])
                            .LogicalName
                    );
            }
        }

        config.Entities = entitySet;
    }

    public Solution? FetchSolution(string uniqueName) =>
        _connection.RetrieveMultiple(new QueryExpression(Solution.EntityLogicalName)
        {
            NoLock = true,
            ColumnSet = new ColumnSet(Solution.LogicalNames.SolutionId, Solution.LogicalNames.FriendlyName, Solution.LogicalNames.UniqueName, Solution.LogicalNames.Version, Solution.LogicalNames.IsManaged),
            Criteria = new FilterExpression
            {
                Conditions =
                {
                    new ConditionExpression(Solution.LogicalNames.UniqueName,
                        ConditionOperator.Equal, uniqueName)
                }
            }
        }).Entities.SingleOrDefault()?.ToEntity<Solution>();   

    public IEnumerable<WfAction> RetrieveActions(CodeGenerationConfig config)
    {
        if (config.Hints)
        {
            var rule = new Rule("[red]Generating ALL actions by default is deprecated![/]");
            _console.Write(rule);
            var panel = new Panel("Add at least these params to your config file: '\"Actions\": [[\"...\",\"...\"]],'");
            _console.Write(panel);
        }

        var query = new QueryExpression(Workflow.EntityLogicalName)
        {
            NoLock = true,
            Orders = {new OrderExpression(Workflow.LogicalNames.UniqueName, OrderType.Ascending)},
            ColumnSet = new ColumnSet(
                Workflow.LogicalNames.UniqueName,
                Workflow.LogicalNames.Xaml),
            Criteria = new FilterExpression(LogicalOperator.And)
            {
                Conditions =
                {
                    new ConditionExpression(Workflow.LogicalNames.Type, ConditionOperator.Equal,
                        Workflow.Options.Type.Definition),
                    new ConditionExpression(Workflow.LogicalNames.RendererObjectTypeCode, ConditionOperator.Null),
                    new ConditionExpression(Workflow.LogicalNames.Category, ConditionOperator.Equal,
                        Workflow.Options.Category.Action)
                }
            }
        };
        var sdkmessageLink = query.AddLink(SdkMessage.EntityLogicalName,
            Workflow.LogicalNames.SdkMessageId,
            SdkMessage.LogicalNames.SdkMessageId
        );

        sdkmessageLink.Columns.AddColumn(SdkMessage.LogicalNames.Name);
        sdkmessageLink.EntityAlias = "msg";

        var names = new List<string>();
        if (config.Actions.Length != 0)
        {
            names.AddRange(config.Actions);
        }

        if (names.Count == 0) return Array.Empty<WfAction>();

        sdkmessageLink.LinkCriteria = new FilterExpression(LogicalOperator.And)
        {
            // ReSharper disable once CoVariantArrayConversion
            Conditions = {new ConditionExpression(SdkMessage.LogicalNames.Name, ConditionOperator.In, names.ToArray())}
        };
        var actions = _connection.RetrieveMultiple(query);


        var result = new List<WfAction>();

        XNamespace x = "http://schemas.microsoft.com/winfx/2006/xaml";

        foreach (var action in actions.Entities)
        {
            var doc = XDocument.Load(XmlReader.Create(new StringReader((string)action["xaml"])));
            var extract = doc.Descendants(x + "Property")
                .Where(i => i.DescendantsAndSelf(x + "Property.Attributes").Any())
                .ToList()
                .Select(i => new
                {
                    Name = i.Attribute("Name")?.Value,
                    Type = i.Attribute("Type")?.Value,
                    Direction =
                        i.DescendantsAndSelf()
                            .SingleOrDefault(d => d.Name.LocalName == "ArgumentDirectionAttribute")?
                            .Attribute("Value")?.Value,
                    Description =
                        i.DescendantsAndSelf()
                            .SingleOrDefault(d => d.Name.LocalName == "ArgumentDescriptionAttribute")?
                            .Attribute("Value")?.Value.Replace("\\r\\n", "///\r\n"),
                    EntityName =
                        i.DescendantsAndSelf()
                            .SingleOrDefault(d => d.Name.LocalName == "ArgumentEntityAttribute")?
                            .Attribute("Value")?.Value
                });

            var wfa = new WfAction((string)((AliasedValue)action["msg.name"]).Value);
            foreach (var ext in extract)
            {
                var wfParameter = new WfParameter
                {
                    Name = ext.Name,
                    UniqueName = ext.Name,
                    Description = ext.Description,
                    Entityname = ext.EntityName,
                    Type = ext.Type!.Split(':')[1].TrimEnd(')'),
                    IsOptional = false, // Not used for actions,
                    IsOutput = ext.Direction == "Output"
                };

                switch (ext.Direction)
                {
                    case "Input":
                        wfa.InParameters.Add(wfParameter);
                        break;
                    case "Output":
                        wfa.OutParameters.Add(wfParameter);
                        break;
                    default:
                        throw new ArgumentException("Parameter has no direction?!");
                }
            }

            result.Add(wfa);
        }

        return result;
    }

    public IEnumerable<WfAction> RetrieveCustomAPIs(CodeGenerationConfig config)
    {
        var query = new QueryExpression(CustomAPI.EntityLogicalName)
        {
            NoLock = true,
            Orders = {new OrderExpression(CustomAPI.LogicalNames.UniqueName, OrderType.Ascending)},
            ColumnSet = new ColumnSet(
                CustomAPI.LogicalNames.CustomAPIId,
                CustomAPI.LogicalNames.UniqueName,
                CustomAPI.LogicalNames.BoundEntityLogicalName
            )
        };
        var sdkmessageLink = query.AddLink(
            SdkMessage.EntityLogicalName,
            CustomAPI.LogicalNames.SdkMessageId,
            SdkMessage.LogicalNames.SdkMessageId);
        sdkmessageLink.Columns.AddColumn(SdkMessage.LogicalNames.Name);
        sdkmessageLink.EntityAlias = "msg";

        var names = new List<string>();
        if (config.CustomAPIs.Length != 0)
        {
            names.AddRange(config.CustomAPIs);
        }

        if (names.Count == 0)
        {
            return [];
        }

        sdkmessageLink.LinkCriteria = new FilterExpression(LogicalOperator.And)
        {
            // ReSharper disable once CoVariantArrayConversion
            Conditions = {new ConditionExpression(SdkMessage.LogicalNames.Name, ConditionOperator.In, names.ToArray())}
        };
        var customApis = _connection.RetrieveMultiple(query);


        var result = new List<WfAction>();
        foreach (var customApi in customApis.Entities)
        {
            var ia = new WfAction((string)((AliasedValue)customApi[$"msg.{SdkMessage.LogicalNames.Name}"]).Value);

            var target = customApi.GetAttributeValue<string>(CustomAPI.LogicalNames.BoundEntityLogicalName);
            if (!string.IsNullOrWhiteSpace(target))
            {
                ia.InParameters.Add(
                    new WfParameter
                    {
                        Name = "Target",
                        UniqueName = "Target",
                        Description = "bound Target",
                        Entityname = target,
                        Type = nameof(EntityReference),
                        IsOptional = false, // Bound parameters will be required parameters
                        IsOutput = false
                    });
            }

            var inquery = new QueryExpression(CustomAPIRequestParameter.EntityLogicalName)
            {
                NoLock = true,
                Orders = {new OrderExpression(CustomAPIRequestParameter.LogicalNames.UniqueName, OrderType.Ascending)},
                ColumnSet = new ColumnSet(
                    CustomAPIRequestParameter.LogicalNames.UniqueName,
                    CustomAPIRequestParameter.LogicalNames.Name,
                    CustomAPIRequestParameter.LogicalNames.Description,
                    CustomAPIRequestParameter.LogicalNames.Type,
                    CustomAPIRequestParameter.LogicalNames.IsOptional
                ),
                Criteria = new FilterExpression(LogicalOperator.And)
                {
                    Conditions =
                    {
                        new ConditionExpression(CustomAPIRequestParameter.LogicalNames.CustomAPIId,
                            ConditionOperator.Equal, customApi[CustomAPI.LogicalNames.CustomAPIId])
                    }
                }
            };
            var inparams = _connection.RetrieveMultiple(inquery);
            foreach (var inparam in inparams.Entities)
            {
                var type = GetParamType(inparam
                    .GetAttributeValue<OptionSetValue>(CustomAPIRequestParameter.LogicalNames.Type).Value);
                ia.InParameters.Add(
                    new WfParameter
                    {
                        Name = (string)inparam[CustomAPIRequestParameter.LogicalNames.Name],
                        UniqueName = (string)inparam[CustomAPIRequestParameter.LogicalNames.UniqueName],
                        Description = inparam.GetAttributeValue<string>(CustomAPIRequestParameter.LogicalNames.Description),
                        Entityname = "",
                        Type = type,
                        IsOptional = (bool?)inparam[CustomAPIRequestParameter.LogicalNames.IsOptional] ?? false,
                        IsOutput = false
                    });
            }

            var outquery = new QueryExpression(CustomAPIResponseProperty.EntityLogicalName)
            {
                NoLock = true,
                Orders = {new OrderExpression(CustomAPIResponseProperty.LogicalNames.UniqueName, OrderType.Ascending)},
                ColumnSet = new ColumnSet(
                    CustomAPIResponseProperty.LogicalNames.UniqueName,
                    CustomAPIResponseProperty.LogicalNames.Name,
                    CustomAPIResponseProperty.LogicalNames.Description,
                    CustomAPIResponseProperty.LogicalNames.Type
                ),
                Criteria = new FilterExpression(LogicalOperator.And)
                {
                    Conditions =
                    {
                        new ConditionExpression(CustomAPIResponseProperty.LogicalNames.CustomAPIId,
                            ConditionOperator.Equal, customApi[CustomAPI.LogicalNames.CustomAPIId])
                    }
                }
            };
            var outparams = _connection.RetrieveMultiple(outquery);
            foreach (var outparam in outparams.Entities)
            {
                var type = GetParamType(outparam
                    .GetAttributeValue<OptionSetValue>(CustomAPIResponseProperty.LogicalNames.Type).Value);

                ia.OutParameters.Add(
                    new WfParameter
                    {
                        Name = (string)outparam[CustomAPIResponseProperty.LogicalNames.Name],
                        UniqueName = (string)outparam[CustomAPIResponseProperty.LogicalNames.UniqueName],
                        Description = outparam.GetAttributeValue<string>(CustomAPIResponseProperty.LogicalNames.Description),
                        Entityname = "",
                        Type = type,
                        IsOptional = true, // assume all responses can be optional
                        IsOutput = true
                    });
            }

            result.Add(ia);
        }

        return result;
    }

    public IEnumerable<(string Name, string Message)> RetrieveSdkMessageNames(CodeGenerationConfig config)
    {
        var result = new List<(string Name, string Message)>();
        if (config.Hints)
        {
            var rule = new Rule("[red]Generating ALL sdk messages by default is deprecated![/]");
            _console.Write(rule);
            var panel = new Panel(
                "Only Assign, Create, Delete, GrantAccess, ModifyAccess, Retrieve, RetrieveMultiple, RetrievePrincipalAccess, RetrieveSharedPrincipalsAndAccess, RevokeAccess, SetState and Update are generated by default in future! \n" +
                "Add at least this param to your config file: '\"AdditionalSdkMessages\": [[]],' (Further sdk messages can be specified here on demand...) \n" +
                "Add the params '\"Actions\": [[\"...\",\"...\"]],' and '\"CustomAPIs\": [[\"...\",\"...\"]],' to add theses sdk messages as well.");

            _console.Write(panel);
        }

        result.Add(("Assign", "Assign"));
        result.Add(("Create", "Create"));
        result.Add(("Delete", "Delete"));
        result.Add(("GrantAccess", "GrantAccess"));
        result.Add(("ModifyAccess", "ModifyAccess"));
        result.Add(("Retrieve", "Retrieve"));
        result.Add(("RetrieveMultiple", "RetrieveMultiple"));
        result.Add(("RetrievePrincipalAccess", "RetrievePrincipalAccess"));
        result.Add(("RetrieveSharedPrincipalsAndAccess", "RetrieveSharedPrincipalsAndAccess"));
        result.Add(("RevokeAccess", "RevokeAccess"));
        result.Add(("SetState", "SetState"));
        result.Add(("Update", "Update"));

        var names = new List<string>();
        if (config.AdditionalSdkMessages.Length != 0)
        {
            names.AddRange(config.AdditionalSdkMessages);
        }

        if (config.Actions.Length != 0)
        {
            names.AddRange(config.Actions);
        }

        if (config.CustomAPIs.Length != 0)
        {
            names.AddRange(config.CustomAPIs);
        }

        if (names.Count == 0) return result;

        var query = new QueryExpression(SdkMessage.EntityLogicalName)
        {
            NoLock = true,
            Orders = {new OrderExpression(SdkMessage.LogicalNames.Name, OrderType.Ascending)},
            ColumnSet = new ColumnSet(SdkMessage.LogicalNames.Name),
            Criteria = new FilterExpression(LogicalOperator.And)
            {
                // ReSharper disable once CoVariantArrayConversion
                Conditions = {new ConditionExpression(SdkMessage.LogicalNames.Name, ConditionOperator.In, names.ToArray())}
            }
        };
        var sdkMessages = _connection.RetrieveMultiple(query)?.Entities.Select(x => x.ToEntity<SdkMessage>()) ??
                          Enumerable.Empty<SdkMessage>();
        var hashSet = new HashSet<string>();
        foreach (var sdkMessage in sdkMessages)
        {
            var message = sdkMessage.Name!;
            var name = Formatter.CamelCase(message);
            if (hashSet.Contains(name))
            {
                _console.WriteLine($"Warning: multiple sdk messages for: [bold]{name}[/]({message})");
                name += "_";
            }

            hashSet.Add(name);
            result.Add((name, message));
        }

        hashSet.Clear();

        return result;
    }

    public SortedDictionary<string, List<Option>> RetrieveOptionSets(CodeGenerationConfig config)
    {
        var result = new SortedDictionary<string, List<Option>>();
        foreach (var globalOptionSet in config.GlobalOptionSets)
        {
            var request = new RetrieveOptionSetRequest {Name = globalOptionSet};
            var response = (RetrieveOptionSetResponse)_connection.Execute(request);
            var metadata = (OptionSetMetadata)response.OptionSetMetadata;
            result.Add(globalOptionSet, new List<Option>());
            foreach (var option in metadata.Options)
            {
                result[globalOptionSet].Add(new Option(option.Value,
                    Formatter.GetLocalizedLabel(option.Label, config.UseBaseLanguage, RetrieveOrganizationLanguage())));
            }

            result[globalOptionSet]
                .Sort((x, y) => x.Value.GetValueOrDefault(-1).CompareTo(y.Value.GetValueOrDefault(-1)));
        }

        return result;
    }

    public EntityMetadata RetrieveEntityMetadata(string entity, EntityFilters filter = EntityFilters.Default)
    {
        var key = $"{entity.ToLowerInvariant()}_{filter:D}";
        if (!_metadataCache.Contains(key))
        {
            var metadata = (RetrieveEntityResponse)_connection.Execute(new RetrieveEntityRequest
            {
                EntityFilters = filter, LogicalName = entity, RetrieveAsIfPublished = true
            });

            _metadataCache.Add(key, metadata.EntityMetadata,
                new CacheItemPolicy {Priority = CacheItemPriority.NotRemovable});
            return metadata.EntityMetadata;
        }

        return (EntityMetadata)_metadataCache[key];
    }

    public int RetrieveOrganizationLanguage()
    {
        var query = new QueryExpression(Organization.EntityLogicalName)
        {
            NoLock = true, ColumnSet = new ColumnSet(Organization.LogicalNames.LanguageCode)
        };
        var organization = _connection.RetrieveMultiple(query);

        return organization.Entities.Single().ToEntity<Organization>().LanguageCode!.Value;
    }

    public List<Tuple<string, string, Guid, string>> RetrieveBusinessProcessFlows(CodeGenerationConfig config)
    {
        var result = new List<Tuple<string, string, Guid, string>>();
        var query = new QueryExpression(Workflow.EntityLogicalName)
        {
            ColumnSet = new ColumnSet(
                Workflow.LogicalNames.WorkflowId,
                Workflow.LogicalNames.Name,
                Workflow.LogicalNames.UniqueName
            ),
            NoLock = true,
            Criteria = new FilterExpression(LogicalOperator.And)
            {
                Conditions =
                {
                    new ConditionExpression(Workflow.LogicalNames.Category, ConditionOperator.Equal,
                        Workflow.Options.Category.BusinessProcessFlow)
                }
            },
            Orders = {new OrderExpression(Workflow.LogicalNames.Name, OrderType.Ascending)}
        };
        var bpfs = _connection.RetrieveMultiple(query)
                       ?.Entities?
                       .Select(x => x.ToEntity<Workflow>()) ??
                   Enumerable.Empty<Workflow>();

        foreach (var bpf in bpfs)
        {
            var workflow = bpf.Name!;
            var name = Formatter.CamelCase(workflow);
            var workflowid = bpf.Id;
            var uniquename = bpf.UniqueName!;
            if (config.BusinessProcessFlows.Contains(uniquename))
            {
                result.Add(Tuple.Create(name, workflow, workflowid, uniquename));
            }
        }

        return result;
    }

    public List<BpfControlDetail> RetrieveBusinessProcessFlowControlsForMainEntity(CodeGenerationConfig config, string entityName)
    {
        var queryWorkflow = new QueryExpression(Workflow.EntityLogicalName)
        {
            ColumnSet = new ColumnSet(
                Workflow.LogicalNames.WorkflowId,
                Workflow.LogicalNames.Name,
                Workflow.LogicalNames.UniqueName,
                Workflow.LogicalNames.ClientData,
                Workflow.LogicalNames.Category,
                Workflow.LogicalNames.PrimaryEntity
            ),
            NoLock = true,
            Criteria = new FilterExpression(LogicalOperator.And)
            {
                Conditions =
                {
                    new ConditionExpression(Workflow.LogicalNames.Category, ConditionOperator.Equal, Workflow.Options.Category.BusinessProcessFlow),
                    new ConditionExpression(Workflow.LogicalNames.PrimaryEntity, ConditionOperator.Equal, entityName),
                    new ConditionExpression(Workflow.LogicalNames.ClientData, ConditionOperator.NotNull)
                }
            },
            Orders = { new OrderExpression(Workflow.LogicalNames.Name, OrderType.Ascending) }
        };
        if (config.OnlyFormsFromSolutions && config.Solutions.Length > 0)
        {
            var queryAnd = new FilterExpression(LogicalOperator.And);
            var querySolutionComponent = new LinkEntity(
                Workflow.EntityLogicalName,
                SolutionComponent.EntityLogicalName,
                Workflow.LogicalNames.WorkflowId,
                SolutionComponent.LogicalNames.ObjectId,
                JoinOperator.Any);
            queryAnd.AnyAllFilterLinkEntity = querySolutionComponent;
            querySolutionComponent.LinkCriteria.AddCondition(SolutionComponent.LogicalNames.ComponentType, ConditionOperator.Equal, SolutionComponent.Options.ComponentType.Workflow);

            var querySolutionAndSolutionComponent = querySolutionComponent.AddLink(Solution.EntityLogicalName, Solution.LogicalNames.SolutionId, SolutionComponent.LogicalNames.SolutionId);
            querySolutionAndSolutionComponent.LinkCriteria.AddCondition(Solution.LogicalNames.UniqueName, ConditionOperator.In, config.Solutions);
            
            queryWorkflow.Criteria.AddFilter(queryAnd);
        }
        List<Workflow> returnList = _connection
            .RetrieveMultiple(queryWorkflow)?
            .Entities
            .Select(x => x.ToEntity<Workflow>())
            .OrderBy(x => x.Name)
            .ToList() ?? [];


        if (returnList.Count == 0)
        {
            return [];
        }

        return returnList
            .Select(w => BpfControlParser.ParseBpfClientDetail(w, entityName))
            .SelectMany(x => x)
            .OrderBy(x => x.WorkflowName)
            .ThenBy(x => x.DataFieldName)
            .ToList();
    }
    public List<Tuple<string, string, List<Guid>>> RetrieveBusinessProcessFlowStages(Guid processId)
    {
        var result = new List<Tuple<string, string, List<Guid>>>();
        var query = new QueryExpression(ProcessStage.EntityLogicalName)
        {
            NoLock = true,
            ColumnSet = new ColumnSet(
                ProcessStage.LogicalNames.StageName,
                ProcessStage.LogicalNames.ProcessStageId
            ),
            Criteria = new FilterExpression(LogicalOperator.And)
            {
                Conditions = {new ConditionExpression(ProcessStage.LogicalNames.ProcessId, ConditionOperator.Equal, processId)}
            },
            Orders = {new OrderExpression(ProcessStage.LogicalNames.StageName, OrderType.Ascending)}
        };

        var stages = (_connection.RetrieveMultiple(query)
                         ?.Entities?.Select(x => x.ToEntity<ProcessStage>()) ??
                     Enumerable.Empty<ProcessStage>()).ToArray();

        var unique = new HashSet<string>(stages.Count());
        foreach (var stage in stages)
        {
            var name = Formatter.CamelCase(stage.StageName);
            var stagename = stage.StageName!;
            var processstageid = stage.Id;
            if (unique.Contains(stagename))
            {
                var ids = result.Find(t => t.Item2.Equals(stagename,StringComparison.Ordinal))!.Item3;
                ids.Add(processstageid);
            }
            else
            {
                unique.Add(stagename);
                result.Add(Tuple.Create(name, stagename, new List<Guid> {processstageid}));
            }
        }

        return result;
    }

    public Dictionary<string, FormDetail> RetrieveFormsDetailsFromSolutions(string entityLogicalName, string[] configSolutions, SortedSet<BpfControlDetail>? bpfControls)
    {
        var querySystemForm = new QueryExpression(SystemForm.EntityLogicalName);
        querySystemForm.ColumnSet.AddColumns(
            SystemForm.LogicalNames.ObjectTypeCode,
            SystemForm.LogicalNames.FormXml,
            SystemForm.LogicalNames.Name,
            SystemForm.LogicalNames.FormId,
            SystemForm.LogicalNames.Type);
        querySystemForm.Criteria.AddCondition(SystemForm.LogicalNames.ObjectTypeCode, ConditionOperator.Equal, entityLogicalName);
        querySystemForm.Criteria.AddCondition(
            SystemForm.LogicalNames.Type,
            ConditionOperator.In,
            SystemForm.Options.Type.Main,
            SystemForm.Options.Type.QuickViewForm,
            SystemForm.Options.Type.QuickCreate);
        querySystemForm.Criteria.AddCondition(
            SystemForm.LogicalNames.FormActivationState,
            ConditionOperator.Equal,
            SystemForm.Options.FormActivationState.Active);

        // System form linked through the solution component
        var query_Or = new FilterExpression(LogicalOperator.Or);
        querySystemForm.Criteria.AddFilter(query_Or);
        var query_Or_Or1 = new FilterExpression(LogicalOperator.Or);
        query_Or.AddFilter(query_Or_Or1);
        var query_Or_Or1_solutioncomponent = new LinkEntity(
            SystemForm.EntityLogicalName,
            SolutionComponent.EntityLogicalName,
            SystemForm.LogicalNames.FormId,
            SolutionComponent.LogicalNames.ObjectId,
            JoinOperator.Any);
        query_Or_Or1.AnyAllFilterLinkEntity = query_Or_Or1_solutioncomponent;
        query_Or_Or1_solutioncomponent.LinkCriteria.AddCondition(SolutionComponent.LogicalNames.ComponentType, ConditionOperator.Equal, SolutionComponent.Options.ComponentType.SystemForm);  

        var query_Or_Or1_solutioncomponent_And = new FilterExpression();
        query_Or_Or1_solutioncomponent.LinkCriteria = query_Or_Or1_solutioncomponent_And;
        var query_Or_Or1_solutioncomponent_And_solution = new LinkEntity(
            SystemForm.EntityLogicalName,
            Solution.EntityLogicalName,
            SystemForm.LogicalNames.SolutionId,
            Solution.LogicalNames.SolutionId,
            JoinOperator.Any);
        query_Or_Or1_solutioncomponent_And.AnyAllFilterLinkEntity = query_Or_Or1_solutioncomponent_And_solution;

        foreach (var solutionName in configSolutions)
        {
            query_Or_Or1_solutioncomponent_And_solution.LinkCriteria.AddCondition(Solution.LogicalNames.UniqueName, ConditionOperator.Equal, solutionName);
        }

        // System form linked through the entity component
        var query_Or_Or2 = new FilterExpression(LogicalOperator.Or);
        query_Or.AddFilter(query_Or_Or2);
        var query_Or_Or2_entity = new LinkEntity(
            SystemForm.EntityLogicalName,
            "entity", //Should be generated with a vanilla org
            SystemForm.LogicalNames.ObjectTypeCode,
            "objecttypecode",
            JoinOperator.Any);
        query_Or_Or2.AnyAllFilterLinkEntity = query_Or_Or2_entity;

        var query_Or_Or2_entity_And = new FilterExpression();
        query_Or_Or2_entity.LinkCriteria = query_Or_Or2_entity_And;
        var query_Or_Or2_entity_And_solutioncomponent = new LinkEntity(
            SystemForm.EntityLogicalName,
            SolutionComponent.EntityLogicalName,
            "entityid",//Should be generated with a vanilla org
            SolutionComponent.LogicalNames.ObjectId,
            JoinOperator.Any);
        query_Or_Or2_entity_And.AnyAllFilterLinkEntity = query_Or_Or2_entity_And_solutioncomponent;

        query_Or_Or2_entity_And_solutioncomponent.LinkCriteria.AddCondition(
            SolutionComponent.LogicalNames.RootComponentBehavior,
            ConditionOperator.Equal,
            SolutionComponent.Options.RootComponentBehavior.IncludeSubcomponents);
        var query_Or_Or2_entity_And_solutioncomponent_And = new FilterExpression();
        query_Or_Or2_entity_And_solutioncomponent.LinkCriteria.AddFilter(query_Or_Or2_entity_And_solutioncomponent_And);
        var query_Or_Or2_entity_And_solutioncomponent_And_solution = new LinkEntity(
             SystemForm.EntityLogicalName,
            Solution.EntityLogicalName,
            SystemForm.LogicalNames.SolutionId,
            Solution.LogicalNames.SolutionId,
            JoinOperator.Any);
        query_Or_Or2_entity_And_solutioncomponent_And.AnyAllFilterLinkEntity = query_Or_Or2_entity_And_solutioncomponent_And_solution;
        foreach (var solutionName in configSolutions)
        {
            query_Or_Or2_entity_And_solutioncomponent_And_solution.LinkCriteria.AddCondition(Solution.LogicalNames.UniqueName, ConditionOperator.Equal, solutionName);
        }

        var systemForms = _connection.RetrieveMultiple(querySystemForm);

        return ParseSystemFormsCollectionToFormDetails(systemForms, entityLogicalName, bpfControls);
    }

    public Dictionary<string, FormDetail> RetrieveFormsDetails(string entityLogicalName, SortedSet<BpfControlDetail>? bpfControls)
    {
        var systemForms = RetrieveFormsForEntity(entityLogicalName);
        return ParseSystemFormsCollectionToFormDetails(systemForms, entityLogicalName, bpfControls);
    }

    private Dictionary<string, FormDetail> ParseSystemFormsCollectionToFormDetails(EntityCollection forms, string entityLogicalName, SortedSet<BpfControlDetail>? bpfControls)
    {
        var allForms = forms.Entities.Select(x => x.ToEntity<SystemForm>());
        return allForms.Select(form =>
        {
            // Made unique name with scope by entity and form type
            var formScope = $"{entityLogicalName}.{form.Type?.Value}";
            return FormParser.ParseForm(form,
                Unique(form.GetAttributeValue<string>(SystemForm.LogicalNames.Name).Trim(), formScope),
                entityLogicalName,
                bpfControls,
                _console);
        })
       .ToDictionary(formDetail => $"{formDetail.FormUniqueName}.{FormParser.GetFormType(formDetail.FormType)}", formDetail => formDetail);
    }    

    private EntityCollection RetrieveFormsForEntity(string logicalName)
    {
        var query = new QueryExpression(SystemForm.EntityLogicalName);
        query.ColumnSet.AddColumns(
            SystemForm.LogicalNames.FormXml,
            SystemForm.LogicalNames.Name,
            SystemForm.LogicalNames.FormId,
            SystemForm.LogicalNames.Type
        );
        query.Criteria.AddCondition(SystemForm.LogicalNames.ObjectTypeCode, ConditionOperator.Equal, logicalName);
        query.Criteria.AddCondition(SystemForm.LogicalNames.Type, ConditionOperator.In,
            SystemForm.Options.Type.Main,
            SystemForm.Options.Type.QuickViewForm,
            SystemForm.Options.Type.QuickCreate
        );
        query.Criteria.AddCondition(SystemForm.LogicalNames.FormActivationState, ConditionOperator.Equal,
            SystemForm.Options.FormActivationState.Active);

        return _connection.RetrieveMultiple(query);
    }

    private string Unique(string value, string scope)
    {
        if (!_usedTokens.ContainsKey(scope)) _usedTokens.Add(scope, new List<string>());

        if (_usedTokens[scope].Contains(value))
            return Unique(value + "_", scope);

        _usedTokens[scope].Add(value);
        return value;
    }

    private static string WildCardToRegular(string value)
    {
        return "^" + Regex.Escape(value).Replace("\\?", ".").Replace("\\*", ".*") + "$";
    }

    private static string GetParamType(int paramType) =>
        paramType switch
        {
            0 => "bool",
            1 => "DateTime",
            2 => "decimal",
            3 => "Entity",
            4 => "EntityCollection",
            5 => "EntityReference",
            6 => "float",
            7 => "int",
            8 => "Money",
            9 => "OptionSetValue",
            10 => "string",
            11 => "string[]",
            12 => "Guid",
            _ => "object"
        };
}
