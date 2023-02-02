using System.Runtime.Caching;
using System.Text.RegularExpressions;
using System.Xml;
using System.Xml.Linq;
using dgt.power.codegeneration.Base;
using dgt.power.codegeneration.Logic;
using dgt.power.codegeneration.Model;
using dgt.power.codegeneration.Services.Contracts;
using dgt.power.codegeneration.Templates;
using dgt.power.common;
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

    public MetadataService(IOrganizationService connection, ObjectCache metadataCache)
    {
        _connection = connection;
        _metadataCache = metadataCache;
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

        if (config.Solutions.Any())
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
                solutionFilter.Conditions.Clear();
                solutionFilter.AddCondition(new ConditionExpression(SolutionComponent.LogicalNames.SolutionId,
                    ConditionOperator.Equal,
                    solution));

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

    private static string WildCardToRegular(string value)
    {
        return "^" + Regex.Escape(value).Replace("\\?", ".").Replace("\\*", ".*") + "$";
    }

    public IEnumerable<WfAction> RetrieveActions(CodeGenerationConfig config)
    {
        if (config.Hints)
        {
            var rule = new Rule("[red]Generating ALL actions by default is deprecated![/]");
            AnsiConsole.Write(rule);
            var panel = new Panel("Add at least these params to your config file: '\"Actions\": [[\"...\",\"...\"]],'");
            AnsiConsole.Write(panel);
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
        if (config.Actions.Any())
        {
            names.AddRange(config.Actions);
        }

        if (!names.Any()) return Array.Empty<WfAction>();

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
                    Type = ext.Type!.Split(':')[1].TrimEnd(')')
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
        if (config.CustomAPIs.Any())
        {
            names.AddRange(config.CustomAPIs);
        }

        if (!names.Any()) return Array.Empty<WfAction>();

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

            var inquery = new QueryExpression(CustomAPIRequestParameter.EntityLogicalName)
            {
                NoLock = true,
                Orders = {new OrderExpression(CustomAPIRequestParameter.LogicalNames.UniqueName, OrderType.Ascending)},
                ColumnSet = new ColumnSet(
                    CustomAPIRequestParameter.LogicalNames.UniqueName,
                    CustomAPIRequestParameter.LogicalNames.Name,
                    CustomAPIRequestParameter.LogicalNames.Description,
                    CustomAPIRequestParameter.LogicalNames.Type
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
                        Name = (string)inparam["name"],
                        UniqueName = (string)inparam["uniquename"],
                        Description = (string)inparam["description"],
                        Entityname = "",
                        Type = type
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
                        Name = (string)outparam["name"],
                        UniqueName = (string)outparam["uniquename"],
                        Description = (string)outparam["description"],
                        Entityname = "",
                        Type = type
                    });
            }

            result.Add(ia);
        }

        return result;
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

    public IEnumerable<Tuple<string, string>> RetrieveSdkMessageNames(CodeGenerationConfig config)
    {
        var result = new List<Tuple<string, string>>();
        if (config.Hints)
        {
            var rule = new Rule("[red]Generating ALL sdk messages by default is deprecated![/]");
            AnsiConsole.Write(rule);
            var panel = new Panel(
                "Only Assign, Create, Delete, GrantAccess, ModifyAccess, Retrieve, RetrieveMultiple, RetrievePrincipalAccess, RetrieveSharedPrincipalsAndAccess, RevokeAccess, SetState and Update are generated by default in future! \n" +
                "Add at least this param to your config file: '\"AdditionalSdkMessages\": [[]],' (Further sdk messages can be specified here on demand...) \n" +
                "Add the params '\"Actions\": [[\"...\",\"...\"]],' and '\"CustomAPIs\": [[\"...\",\"...\"]],' to add theses sdk messages as well.");

            AnsiConsole.Write(panel);
        }

        result.Add(Tuple.Create("Assign", "Assign"));
        result.Add(Tuple.Create("Create", "Create"));
        result.Add(Tuple.Create("Delete", "Delete"));
        result.Add(Tuple.Create("GrantAccess", "GrantAccess"));
        result.Add(Tuple.Create("ModifyAccess", "ModifyAccess"));
        result.Add(Tuple.Create("Retrieve", "Retrieve"));
        result.Add(Tuple.Create("RetrieveMultiple", "RetrieveMultiple"));
        result.Add(Tuple.Create("RetrievePrincipalAccess", "RetrievePrincipalAccess"));
        result.Add(Tuple.Create("RetrieveSharedPrincipalsAndAccess", "RetrieveSharedPrincipalsAndAccess"));
        result.Add(Tuple.Create("RevokeAccess", "RevokeAccess"));
        result.Add(Tuple.Create("SetState", "SetState"));
        result.Add(Tuple.Create("Update", "Update"));

        var names = new List<string>();
        if (config.AdditionalSdkMessages.Any())
        {
            names.AddRange(config.AdditionalSdkMessages);
        }

        if (config.Actions.Any())
        {
            names.AddRange(config.Actions);
        }

        if (config.CustomAPIs.Any())
        {
            names.AddRange(config.CustomAPIs);
        }

        if (!names.Any()) return result;

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
                AnsiConsole.WriteLine($"Warning: multiple sdk messages for: [bold]{name}[/]({message})");
                name += "_";
            }

            hashSet.Add(name);
            result.Add(Tuple.Create(name, message));
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

    public Dictionary<string, FormDetail> RetrieveFormsDetailsFromSolutions(string entityLogicalName,
        string[] configSolutions)
    {
        // 1.) Retrieve "allowed§ Forms
        var querySolutionForms = new QueryExpression(SolutionComponent.EntityLogicalName);
        querySolutionForms.ColumnSet.AddColumns(SolutionComponent.LogicalNames.ObjectId);
        querySolutionForms.Criteria.AddCondition(SolutionComponent.LogicalNames.ComponentType, ConditionOperator.Equal,
            SolutionComponent.Options.ComponentType.SystemForm);
        var solutionLink = querySolutionForms.AddLink(Solution.EntityLogicalName,
            SolutionComponent.LogicalNames.SolutionId, Solution.LogicalNames.SolutionId);
        solutionLink.LinkCriteria.FilterOperator = LogicalOperator.Or;
        foreach (var configSolution in configSolutions)
        {
            solutionLink.LinkCriteria.AddCondition(Solution.LogicalNames.UniqueName, ConditionOperator.Equal,
                configSolution);
        }

        var formIds = _connection.RetrieveMultiple(querySolutionForms).Entities
            .Select(x => x.ToEntity<SolutionComponent>())
            .Select(x => x.ObjectId);
        var allForms = RetrieveFormsForEntity(entityLogicalName);

        return allForms.Entities.Where(e => formIds.Contains(e.Id))
            .ToDictionary(
                form =>
                    $"{form.GetAttributeValue<string>(SystemForm.LogicalNames.Name)}.{GetFormType(form.GetAttributeValue<OptionSetValue>(SystemForm.LogicalNames.Type).Value)}",
                ParseForms);
    }

    public Dictionary<string, FormDetail> RetrieveFormsDetails(string entityLogicalName)
    {
        var allForms = RetrieveFormsForEntity(entityLogicalName);
        return allForms.Entities
            .ToDictionary(
                form =>
                    $"{form.GetAttributeValue<string>(SystemForm.LogicalNames.Name)}.{GetFormType(form.GetAttributeValue<OptionSetValue>(SystemForm.LogicalNames.Type).Value)}",
                ParseForms);
    }

    private static string GetFormType(int type)
    {
        return type switch
        {
            2 => "main",
            6 => "quickview",
            _ => "quickcreate"
        };
    }

    // TODO: Extract to FormParser.cs
    private static FormDetail ParseForms(Entity form)
    {
        var formxml = form.GetAttributeValue<string>(SystemForm.LogicalNames.FormXml);
        var doc = new XmlDocument();
        doc.LoadXml(formxml);

        var result = new FormDetail();


        var tabs = doc.SelectNodes("/form/tabs/tab[*]");
        if (tabs == null)
        {
            return result;
        }

        foreach (XmlNode tab in tabs)
        {
            var sectionlist = new List<string>();
            var columns = tab.SelectNodes(".//columns/column[*]");
#pragma warning disable CS8602
            foreach (XmlNode column in columns)
            {
                var sections = column.SelectNodes(".//sections/section[*]");
                foreach (XmlNode section in sections)
                {
                    var sectionName = section.Attributes["name"] != null
                        ? section.Attributes["name"].Value
                        : section.Attributes["id"].Value;
                    sectionlist.Add(sectionName);

                    var rows = section.SelectNodes(".//rows/row[*]");
                    foreach (XmlNode row in rows)
                    {
                        var cells = row.SelectNodes(".//cell[*]");
                        foreach (XmlNode cell in cells)
                        {
                            var control = cell.SelectSingleNode(".//control");
                            if (control != null && control.Attributes["datafieldname"] != null)
                            {
                                result.Fields.Add(control.Attributes["datafieldname"].Value);
                            }

                            if (control != null && control.Attributes["id"] != null &&
                                control.Attributes["indicationOfSubgrid"]?.Value == "true")
                            {
                                result.Grids.Add(control.Attributes["id"].Value);
                            }
                        }
                    }
                }
            }

            var tabName = tab.Attributes["name"] != null ? tab.Attributes["name"].Value : tab.Attributes["id"].Value;
#pragma warning restore CS8602

            result.Tabs.Add(tabName, sectionlist);
        }

        return result;
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
}
