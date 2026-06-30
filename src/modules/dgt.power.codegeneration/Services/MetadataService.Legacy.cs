// Copyright (c) DIGITALL Nature. All rights reserved
// DIGITALL Nature licenses this file to you under the Microsoft Public License.

using System.Text.RegularExpressions;
using System.Xml;
using System.Xml.Linq;
using dgt.power.codegeneration.Base;
using dgt.power.codegeneration.Logic;
using dgt.power.codegeneration.Model;
using dgt.power.codegeneration.Templates;
using dgt.power.dataverse;
using Microsoft.Xrm.Sdk;
using Microsoft.Xrm.Sdk.Messages;
using Microsoft.Xrm.Sdk.Metadata;
using Microsoft.Xrm.Sdk.Query;
using Spectre.Console;

namespace dgt.power.codegeneration.Services;

/// <summary>V1 legacy overloads for <see cref="MetadataService"/>.</summary>
public partial class MetadataService
{
    public void PopulateEntitiesAndSolutions(CodeGenerationConfig config)
    {
        ArgumentNullException.ThrowIfNull(config);
        var entities = (RetrieveAllEntitiesResponse)connection.Execute(new RetrieveAllEntitiesRequest
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

        if (config.Solutions.Count != 0)
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

                var entityComponents = connection.RetrieveMultiple(componentsQuery).Entities;

                foreach (var entityComponent in entityComponents)
                    entitySet.Add(
                        entities.EntityMetadata.Single(e => e.MetadataId == (Guid)entityComponent["objectid"])
                            .LogicalName
                    );
            }
        }

        config.Entities = entitySet;
    }
    public IEnumerable<WfAction> RetrieveActions(CodeGenerationConfig config)
    {
        ArgumentNullException.ThrowIfNull(config);

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
        if (config.Actions.Count != 0)
        {
            names.AddRange(config.Actions);
        }

        if (names.Count == 0) return Array.Empty<WfAction>();

        sdkmessageLink.LinkCriteria = new FilterExpression(LogicalOperator.And)
        {
            Conditions = {new ConditionExpression(SdkMessage.LogicalNames.Name, ConditionOperator.In, names.Cast<object>().ToArray())}
        };
        var actions = connection.RetrieveMultiple(query);


        var result = new List<WfAction>();

        XNamespace x = "http://schemas.microsoft.com/winfx/2006/xaml";

        foreach (var action in actions.Entities)
        {
            using var stringReader = new StringReader((string)action["xaml"]);
            using var xmlReader = XmlReader.Create(stringReader);
            var doc = XDocument.Load(xmlReader);
            var extract = doc.Descendants(x + "Property")
                .Where(i => i.DescendantsAndSelf(x + "Property.Attributes").Any())
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
                            .Attribute("Value")?.Value.Replace("\\r\\n", "///\r\n", StringComparison.Ordinal),
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

    public IEnumerable<WfAction> RetrieveCustomApis(CodeGenerationConfig config)
    {
        ArgumentNullException.ThrowIfNull(config);
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
        if (config.CustomAPIs.Count != 0)
        {
            names.AddRange(config.CustomAPIs);
        }

        if (names.Count == 0)
        {
            return [];
        }

        sdkmessageLink.LinkCriteria = new FilterExpression(LogicalOperator.And)
        {
            Conditions = {new ConditionExpression(SdkMessage.LogicalNames.Name, ConditionOperator.In, names.Cast<object>().ToArray())}
        };
        var customApis = connection.RetrieveMultiple(query);


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
            var inparams = connection.RetrieveMultiple(inquery);
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
            var outparams = connection.RetrieveMultiple(outquery);
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


    public IReadOnlyList<(string Name, string Message)> RetrieveSdkMessageNames(CodeGenerationConfig config)
    {
        ArgumentNullException.ThrowIfNull(config);
        var result = new List<(string Name, string Message)>
        {
            ("Assign", "Assign"),
            ("Create", "Create"),
            ("Delete", "Delete"),
            ("GrantAccess", "GrantAccess"),
            ("ModifyAccess", "ModifyAccess"),
            ("Retrieve", "Retrieve"),
            ("RetrieveMultiple", "RetrieveMultiple"),
            ("RetrievePrincipalAccess", "RetrievePrincipalAccess"),
            ("RetrieveSharedPrincipalsAndAccess", "RetrieveSharedPrincipalsAndAccess"),
            ("RevokeAccess", "RevokeAccess"),
            ("SetState", "SetState"),
            ("Update", "Update")
        };

        var names = new List<string>();
        if (config.AdditionalSdkMessages.Count != 0)
        {
            names.AddRange(config.AdditionalSdkMessages);
        }

        if (config.Actions.Count != 0)
        {
            names.AddRange(config.Actions);
        }

        if (config.CustomAPIs.Count != 0)
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
                Conditions = {new ConditionExpression(SdkMessage.LogicalNames.Name, ConditionOperator.In, names.Cast<object>().ToArray())}
            }
        };
        var sdkMessages = connection.RetrieveMultiple(query)?.Entities.Select(x => x.ToEntity<SdkMessage>()) ??
                          Enumerable.Empty<SdkMessage>();
        var hashSet = new HashSet<string>();
        foreach (var sdkMessage in sdkMessages)
        {
            var message = sdkMessage.Name!;
            var name = Formatter.CamelCase(message);
            if (hashSet.Contains(name))
            {
                console.WriteLine($"Warning: multiple sdk messages for: [bold]{name}[/]({message})");
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
        ArgumentNullException.ThrowIfNull(config);
        var result = new SortedDictionary<string, List<Option>>();
        foreach (var globalOptionSet in config.GlobalOptionSets)
        {
            var request = new RetrieveOptionSetRequest {Name = globalOptionSet};
            var response = (RetrieveOptionSetResponse)connection.Execute(request);
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

    public IReadOnlyList<Tuple<string, string, Guid, string>> RetrieveBusinessProcessFlows(CodeGenerationConfig config)
    {
        ArgumentNullException.ThrowIfNull(config);
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
        var bpfs = connection.RetrieveMultiple(query)
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

    public IReadOnlyList<BpfControlDetail> RetrieveBusinessProcessFlowControlsForMainEntity(CodeGenerationConfig config, string entityName)
    {
        ArgumentNullException.ThrowIfNull(config);
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
        if (config is { OnlyFormsFromSolutions: true, Solutions.Count: > 0 })
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
            querySolutionAndSolutionComponent.LinkCriteria.AddCondition(Solution.LogicalNames.UniqueName, ConditionOperator.In, config.Solutions.Select(static object (s) => s).ToArray());

            queryWorkflow.Criteria.AddFilter(queryAnd);
        }
        List<Workflow> returnList = connection
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
}
