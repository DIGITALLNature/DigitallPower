// Copyright (c) DIGITALL Nature. All rights reserved
// DIGITALL Nature licenses this file to you under the Microsoft Public License.

using System.Text.RegularExpressions;
using dgt.power.codegeneration.Base;
using dgt.power.codegeneration.Logic;
using dgt.power.codegeneration.Model;
using Microsoft.Xrm.Sdk;
using Microsoft.Xrm.Sdk.Metadata;

namespace dgt.power.codegeneration.Templates.ts;

internal static class TsLiquidTemplateModelFactory
{
    public static TsSdkMessagesTemplateModel CreateSdkMessagesModel(IEnumerable<(string Name, string Message)> sdkMessages, CodeGenerationConfig config)
    {
        var filteredMessages = config.SdkMessageFilters.Count > 0
            ? sdkMessages.Where(message => config.SdkMessageFilters.Contains(message.Message))
            : sdkMessages;

        return new TsSdkMessagesTemplateModel
        {
            Messages = filteredMessages
                .Select(message => new TsSdkMessageModel
                {
                    Name = message.Name,
                    Message = message.Message
                })
                .ToList()
        };
    }

    public static TsOptionSetsTemplateModel CreateOptionSetsModel(SortedDictionary<string, List<Option>> optionSets)
    {
        return new TsOptionSetsTemplateModel
        {
            OptionSets = optionSets
                .Select(optionSet => new TsOptionSetModel
                {
                    Name = Formatter.CamelCase(optionSet.Key),
                    Options = optionSet.Value
                        .Select(option => new TsOptionValueModel
                        {
                            Name = Formatter.Sanitize(Formatter.CamelCase(option.Label)),
                            Label = option.Label,
                            Value = option.Value
                        })
                        .ToList()
                })
                .ToList()
        };
    }

    public static TsBusinessProcessFlowTemplateModel CreateBusinessProcessFlowModel(string typingPath, Tuple<string, string, Guid, string> process, List<Tuple<string, string, List<Guid>>> stages)
    {
        return new TsBusinessProcessFlowTemplateModel
        {
            TypingPath = typingPath,
            ModuleName = $"D365{process.Item1}BPF",
            ProcessName = process.Item2,
            ProcessId = process.Item3.ToString("D"),
            Stages = stages
                .Select(stage => new TsBusinessProcessFlowStageModel
                {
                    Name = SanitizeStageName(stage.Item1),
                    DisplayName = stage.Item2,
                    Ids = stage.Item3
                        .Select(id => id.ToString("D"))
                        .ToList(),
                    NamedIds = stage.Item3
                        .Select(id => new TsNamedIdModel
                        {
                            Name = $"Id_{id:N}",
                            Value = id.ToString("D")
                        })
                        .ToList()
                })
                .ToList()
        };
    }

    public static TsEntityRefTemplateModel CreateEntityRefModel(string typingPath, EntityMetadata entityMetadata, CodeGenerationConfig config, int systemLanguage)
    {
        var uniqueNames = new UniqueNameGenerator(Formatter.CamelCase(entityMetadata.SchemaName));
        var attributes = FilterEntityRefAttributes(entityMetadata, config)
            .Select(attribute => new TsAttributeConstantModel
            {
                Name = uniqueNames.GetName(Formatter.CamelCase(Formatter.Sanitize(attribute.SchemaName)), $"A{entityMetadata.LogicalName}"),
                LogicalName = attribute.LogicalName
            })
            .ToList();

        return new TsEntityRefTemplateModel
        {
            TypingPath = typingPath,
            ModuleName = $"D365{Formatter.CamelCase(entityMetadata.SchemaName)}EntityRef",
            EntityLogicalName = entityMetadata.LogicalName,
            EntitySummary = Summary(GetLocalizedLabel(entityMetadata.Description, config, systemLanguage), 1),
            Attributes = attributes,
            IncludeOptions = !config.SuppressOptions,
            Optionsets = CreateEntityOptionSetModels(FilterEntityRefOptionFields(entityMetadata, config, systemLanguage), uniqueNames, entityMetadata)
        };
    }

    public static TsEntityTemplateModel CreateEntityModel(string typingPath, EntityMetadata entityMetadata, CodeGenerationConfig config, int systemLanguage)
    {
        var uniqueNames = new UniqueNameGenerator(Formatter.CamelCase(entityMetadata.SchemaName));
        var attributes = FilterEntityAttributes(entityMetadata, config).ToList();

        return new TsEntityTemplateModel
        {
            TypingPath = typingPath,
            ModuleName = $"D365{Formatter.CamelCase(entityMetadata.SchemaName)}Entity",
            EntityLogicalName = entityMetadata.LogicalName,
            EntitySummary = Summary(GetLocalizedLabel(entityMetadata.Description, config, systemLanguage), 1),
            AttributeConstants = attributes
                .Select(attribute => new TsAttributeConstantModel
                {
                    Name = uniqueNames.GetName(Formatter.CamelCase(Formatter.Sanitize(attribute.SchemaName)), $"A{entityMetadata.LogicalName}"),
                    LogicalName = attribute.LogicalName
                })
                .ToList(),
            AttributeFields = attributes
                .Select(attribute =>
                {
                    var typeSet = TSTypeSet.ConvertType(attribute.AttributeType, attribute.LogicalName);
                    return new TsAttributeFieldModel
                    {
                        Name = uniqueNames.GetName(Formatter.CamelCase(Formatter.Sanitize(attribute.SchemaName)), $"B{entityMetadata.LogicalName}"),
                        LogicalName = attribute.LogicalName,
                        Summary = Summary(GetLocalizedLabel(attribute.Description, config, systemLanguage), 2),
                        AttributeType = typeSet.Attribute,
                        ControlType = typeSet.Control,
                        XrmEnum = typeSet.XrmEnum
                    };
                })
                .ToList(),
            IncludeOptions = !config.SuppressOptions,
            Optionsets = CreateEntityOptionSetModels(FilterEntityOptionFields(entityMetadata, config, systemLanguage), uniqueNames, entityMetadata)
        };
    }

    public static TsEntityFormTemplateModel CreateEntityFormModel(string typingPath, string form, string formName, FormDetail formDetail, EntityMetadata entityMetadata, CodeGenerationConfig config, int systemLanguage)
    {
        var uniqueNames = new UniqueNameGenerator(Formatter.CamelCase(entityMetadata.SchemaName));
        var filteredTabs = FilterTabs(form, formDetail, config).ToList();
        var filteredGrids = FilterGrids(form, formDetail, config).ToList();
        var filteredAttributes = FilterFormAttributes(form, formDetail, entityMetadata, config).ToList();
        var filteredOptionFields = FilterFormOptionFields(form, formDetail, entityMetadata, config, systemLanguage).ToList();
        var tabConstants = filteredTabs
            .Select(tab => new TsNamedValueModel
            {
                Name = uniqueNames.GetName(Formatter.CamelCase(Formatter.Sanitize(tab.Key)), $"T{entityMetadata.LogicalName}"),
                Value = tab.Key
            })
            .ToList();
        var sectionConstants = FlatSections(form, filteredTabs, config)
            .Select(section => new TsNamedValueModel
            {
                Name = uniqueNames.GetName(Formatter.CamelCase(Formatter.Sanitize(section)), $"S{entityMetadata.LogicalName}"),
                Value = section
            })
            .ToList();
        var gridConstants = filteredGrids
            .Select(grid => new TsNamedValueModel
            {
                Name = uniqueNames.GetName(Formatter.CamelCase(Formatter.Sanitize(grid)), $"X{grid}"),
                Value = grid
            })
            .ToList();
        var formTabs = filteredTabs
            .Select(tab => new TsFormTabModel
            {
                Name = uniqueNames.GetName(Formatter.CamelCase(Formatter.Sanitize(tab.Key)), $"T2{entityMetadata.LogicalName}"),
                Value = tab.Key
            })
            .ToList();
        var tabSectionClasses = filteredTabs
            .Select(tab => new TsFormTabSectionClassModel
            {
                Name = uniqueNames.GetName(Formatter.CamelCase(Formatter.Sanitize(tab.Key)), $"T3{entityMetadata.LogicalName}"),
                Sections = FilterSections(form, config, tab.Value)
                    .Select(section => new TsNamedValueModel
                    {
                        Name = uniqueNames.GetName(Formatter.CamelCase(Formatter.Sanitize(section)), $"S3{entityMetadata.LogicalName}"),
                        Value = section
                    })
                    .ToList()
            })
            .ToList();

        return new TsEntityFormTemplateModel
        {
            TypingPath = typingPath,
            ModuleName = $"D365{Formatter.CamelCase(entityMetadata.SchemaName)}{Formatter.CamelCase(Formatter.Sanitize(formName, true))}Form",
            EntityLogicalName = entityMetadata.LogicalName,
            AttributeConstants = filteredAttributes
                .Select(attribute => new TsAttributeConstantModel
                {
                    Name = uniqueNames.GetName(Formatter.CamelCase(Formatter.Sanitize(attribute.SchemaName)), $"A{entityMetadata.LogicalName}"),
                    LogicalName = attribute.LogicalName
                })
                .ToList(),
            TabConstants = tabConstants,
            SectionConstants = sectionConstants,
            GridConstants = gridConstants,
            AttributeFields = filteredAttributes
                .Select(attribute =>
                {
                    var typeSet = TSTypeSet.ConvertType(attribute.AttributeType, attribute.LogicalName);
                    return new TsAttributeFieldModel
                    {
                        Name = uniqueNames.GetName(Formatter.CamelCase(Formatter.Sanitize(attribute.SchemaName)), $"B{entityMetadata.LogicalName}"),
                        LogicalName = attribute.LogicalName,
                        Summary = Summary(GetLocalizedLabel(attribute.Description, config, systemLanguage), 2),
                        AttributeType = typeSet.Attribute,
                        ControlType = typeSet.Control,
                        XrmEnum = typeSet.XrmEnum
                    };
                })
                .ToList(),
            GridFields = filteredGrids
                .Select(grid => new TsNamedValueModel
                {
                    Name = uniqueNames.GetName(Formatter.CamelCase(Formatter.Sanitize(grid)), $"G{grid}"),
                    Value = grid
                })
                .ToList(),
            IncludeOptions = !config.SuppressOptions,
            Optionsets = CreateEntityOptionSetModels(filteredOptionFields, uniqueNames, entityMetadata),
            FormTabs = formTabs,
            TabSectionClasses = tabSectionClasses
        };
    }

    private static IEnumerable<AttributeMetadata> FilterEntityAttributes(EntityMetadata entityMetadata, CodeGenerationConfig config)
    {
        var filteredAttributes = entityMetadata.Attributes
            .Where(attribute => (attribute.IsValidForGrid == true || attribute.IsValidForForm == true || attribute.IsValidODataAttribute == true || attribute.IsPrimaryId == true)
                && (attribute.IsValidForCreate == true || attribute.IsValidForUpdate == true || attribute.IsValidForRead == true))
            .Where(attribute => !attribute.LogicalName.Contains("entityimage", StringComparison.Ordinal))
            .Where(attribute => attribute.AttributeType != AttributeTypeCode.ManagedProperty);

        if (config.EntityFilters.Count > 0)
        {
            var match = config.EntityFilters.FirstOrDefault(entityFilter => entityFilter.Entity == entityMetadata.LogicalName);
            if (match?.Attributes != null && match.Attributes.Length > 0)
            {
                filteredAttributes = filteredAttributes.Where(attribute => match.Attributes.Contains(attribute.LogicalName));
            }
        }

        return filteredAttributes.OrderBy(attribute => attribute.LogicalName);
    }

    private static IEnumerable<Templates.OptionField> FilterEntityOptionFields(EntityMetadata entityMetadata, CodeGenerationConfig config, int systemLanguage)
    {
        var filteredOptionFields = entityMetadata.Attributes
            .Where(attribute => ((attribute.IsValidForGrid == true || attribute.IsValidForForm == true || attribute.IsValidODataAttribute == true)
                    && (attribute.IsValidForCreate == true || attribute.IsValidForUpdate == true || attribute.IsValidForRead == true))
                && (attribute.AttributeType == AttributeTypeCode.Picklist
                    || attribute.AttributeType == AttributeTypeCode.State
                    || attribute.AttributeType == AttributeTypeCode.Status
                    || attribute.AttributeType == AttributeTypeCode.Boolean
                    || attribute.AttributeType == AttributeTypeCode.Virtual && attribute.AttributeTypeName?.Value == "MultiSelectPicklistType"))
            .Select(attribute => new Templates.OptionField(attribute, config.UseBaseLanguage, systemLanguage));

        if (config.EntityFilters.Count > 0)
        {
            var match = config.EntityFilters.FirstOrDefault(entityFilter => entityFilter.Entity == entityMetadata.LogicalName);
            if (match?.Optionsets != null && match.Optionsets.Length > 0)
            {
                filteredOptionFields = filteredOptionFields.Where(optionField => match.Optionsets.Contains(optionField.LogicalName));
            }
        }

        return filteredOptionFields.OrderBy(optionField => optionField.LogicalName);
    }

    private static IEnumerable<AttributeMetadata> FilterEntityRefAttributes(EntityMetadata entityMetadata, CodeGenerationConfig config)
    {
        var filteredAttributes = entityMetadata.Attributes
            .Where(attribute => (attribute.IsValidForGrid == true || attribute.IsValidForForm == true || attribute.IsValidODataAttribute == true || attribute.IsPrimaryId == true)
                && (attribute.IsValidForCreate == true || attribute.IsValidForUpdate == true || attribute.IsValidForRead == true))
            .Where(attribute => !attribute.LogicalName.Contains("entityimage", StringComparison.Ordinal))
            .Where(attribute => attribute.AttributeType != AttributeTypeCode.ManagedProperty);

        if (config.EntityRefFilters.Count > 0)
        {
            var match = config.EntityRefFilters.FirstOrDefault(entityFilter => entityFilter.Entity == entityMetadata.LogicalName);
            if (match?.Attributes != null && match.Attributes.Length > 0)
            {
                filteredAttributes = filteredAttributes.Where(attribute => match.Attributes.Contains(attribute.LogicalName));
            }
        }

        return filteredAttributes.OrderBy(attribute => attribute.LogicalName);
    }

    private static IEnumerable<Templates.OptionField> FilterEntityRefOptionFields(EntityMetadata entityMetadata, CodeGenerationConfig config, int systemLanguage)
    {
        var filteredOptionFields = entityMetadata.Attributes
            .Where(attribute => ((attribute.IsValidForGrid == true || attribute.IsValidForForm == true || attribute.IsValidODataAttribute == true)
                    && (attribute.IsValidForCreate == true || attribute.IsValidForUpdate == true || attribute.IsValidForRead == true))
                && (attribute.AttributeType == AttributeTypeCode.Picklist
                    || attribute.AttributeType == AttributeTypeCode.State
                    || attribute.AttributeType == AttributeTypeCode.Status
                    || attribute.AttributeType == AttributeTypeCode.Boolean
                    || attribute.AttributeType == AttributeTypeCode.Virtual && attribute.AttributeTypeName?.Value == "MultiSelectPicklistType"))
            .Select(attribute => new Templates.OptionField(attribute, config.UseBaseLanguage, systemLanguage));

        if (config.EntityRefFilters.Count > 0)
        {
            var match = config.EntityRefFilters.FirstOrDefault(entityFilter => entityFilter.Entity == entityMetadata.LogicalName);
            if (match?.Optionsets != null && match.Optionsets.Length > 0)
            {
                filteredOptionFields = filteredOptionFields.Where(optionField => match.Optionsets.Contains(optionField.LogicalName));
            }
        }

        return filteredOptionFields.OrderBy(optionField => optionField.LogicalName);
    }

    private static IEnumerable<AttributeMetadata> FilterFormAttributes(string form, FormDetail formDetail, EntityMetadata entityMetadata, CodeGenerationConfig config)
    {
        var filteredAttributes = entityMetadata.Attributes
            .Where(attribute => attribute.IsValidForCreate == true || attribute.IsValidForUpdate == true || attribute.IsValidForRead == true)
            .Where(attribute => !attribute.LogicalName.Contains("entityimage", StringComparison.Ordinal))
            .Where(attribute => attribute.AttributeType != AttributeTypeCode.ManagedProperty)
            .Where(attribute => formDetail.Attributes.Any(formAttribute => formAttribute.DataFieldName == attribute.LogicalName));

        if (config.EntityFormFilters.Count > 0)
        {
            var match = config.EntityFormFilters.FirstOrDefault(entityFilter => entityFilter.EntityForm == form);
            if (match?.Attributes != null && match.Attributes.Length > 0)
            {
                filteredAttributes = filteredAttributes.Where(attribute => match.Attributes.Contains(attribute.LogicalName));
            }
        }

        return filteredAttributes.OrderBy(attribute => attribute.LogicalName);
    }

    private static IEnumerable<Templates.OptionField> FilterFormOptionFields(string form, FormDetail formDetail, EntityMetadata entityMetadata, CodeGenerationConfig config, int systemLanguage)
    {
        var filteredOptionFields = entityMetadata.Attributes
            .Where(attribute => (attribute.IsValidForCreate == true || attribute.IsValidForUpdate == true || attribute.IsValidForRead == true)
                && (attribute.AttributeType == AttributeTypeCode.Picklist
                    || attribute.AttributeType == AttributeTypeCode.State
                    || attribute.AttributeType == AttributeTypeCode.Status
                    || attribute.AttributeType == AttributeTypeCode.Boolean
                    || attribute.AttributeType == AttributeTypeCode.Virtual && attribute.AttributeTypeName?.Value == "MultiSelectPicklistType"))
            .Where(attribute => formDetail.Attributes.Any(formAttribute => formAttribute.DataFieldName == attribute.LogicalName))
            .Select(attribute => new Templates.OptionField(attribute, config.UseBaseLanguage, systemLanguage));

        if (config.EntityFormFilters.Count > 0)
        {
            var match = config.EntityFormFilters.FirstOrDefault(entityFilter => entityFilter.EntityForm == form);
            if (match?.Optionsets != null && match.Optionsets.Length > 0)
            {
                filteredOptionFields = filteredOptionFields.Where(optionField => match.Optionsets.Contains(optionField.LogicalName));
            }
        }

        return filteredOptionFields.OrderBy(optionField => optionField.LogicalName);
    }

    private static IEnumerable<KeyValuePair<string, List<string>>> FilterTabs(string form, FormDetail formDetail, CodeGenerationConfig config)
    {
        IEnumerable<KeyValuePair<string, List<string>>> filteredTabs = formDetail.Tabs;

        if (config.EntityFormFilters.Count > 0)
        {
            var match = config.EntityFormFilters.FirstOrDefault(entityFilter => entityFilter.EntityForm == form);
            if (match?.Tabs != null && match.Tabs.Length > 0)
            {
                filteredTabs = filteredTabs.Where(tab => match.Tabs.Contains(tab.Key));
            }
        }

        return filteredTabs;
    }

    private static IEnumerable<string> FilterGrids(string form, FormDetail formDetail, CodeGenerationConfig config)
    {
        IEnumerable<string> filteredGrids = formDetail.Grids;

        if (config.EntityFormFilters.Count > 0)
        {
            var match = config.EntityFormFilters.FirstOrDefault(entityFilter => entityFilter.EntityForm == form);
            if (match?.Grids != null && match.Grids.Length > 0)
            {
                filteredGrids = filteredGrids.Where(grid => match.Grids.Contains(grid));
            }
        }

        return filteredGrids;
    }

    private static List<string> FlatSections(string form, IEnumerable<KeyValuePair<string, List<string>>> filteredTabs, CodeGenerationConfig config)
    {
        var sections = new List<string>();
        foreach (var tab in filteredTabs)
        {
            sections.AddRange(FilterSections(form, config, tab.Value));
        }

        return sections;
    }

    private static IEnumerable<string> FilterSections(string form, CodeGenerationConfig config, List<string> sections)
    {
        IEnumerable<string> filteredSections = sections;

        if (config.EntityFormFilters.Count > 0)
        {
            var match = config.EntityFormFilters.FirstOrDefault(entityFilter => entityFilter.EntityForm == form);
            if (match?.Sections != null && match.Sections.Length > 0)
            {
                filteredSections = filteredSections.Where(section => match.Sections.Contains(section));
            }
        }

        return filteredSections;
    }

    private static List<TsOptionSetModel> CreateEntityOptionSetModels(IEnumerable<Templates.OptionField> optionFields, UniqueNameGenerator uniqueNames, EntityMetadata entityMetadata)
    {
        return optionFields
            .Select(optionField => new TsOptionSetModel
            {
                Name = uniqueNames.GetName(Formatter.CamelCase(Formatter.Sanitize(optionField.SchemaName)), $"O{entityMetadata.LogicalName}"),
                Options = optionField.Options
                    .Select(option => new TsOptionValueModel
                    {
                        Name = uniqueNames.GetName(Formatter.CamelCase(Formatter.Sanitize(option.Label)), $"{entityMetadata.LogicalName}{optionField.SchemaName}"),
                        Label = Formatter.Sanitize(option.Label, true, true, true),
                        Value = option.Value
                    })
                    .ToList()
            })
            .ToList();
    }

    private static string GetLocalizedLabel(Label? label, CodeGenerationConfig config, int systemLanguage)
    {
        if (label == null)
        {
            return string.Empty;
        }

        return Formatter.GetLocalizedLabel(label, config.UseBaseLanguage, systemLanguage);
    }

    private static string Summary(string description, int indent)
    {
        if (string.IsNullOrWhiteSpace(description))
        {
            return string.Empty;
        }

        return "/*"
               + Environment.NewLine
               + $"{new string('\t', indent)}* {description.Replace("\n", $"\n{new string('\t', indent)}* ").Trim()}"
               + Environment.NewLine
               + $"{new string('\t', indent)}*/";
    }

    private static string SanitizeStageName(string value)
    {
        var result = Formatter.PreventFirstNumber(value);
        return Regex.Replace(result, "[^0-9a-zA-Z_]+", "_");
    }

    private sealed class UniqueNameGenerator(string reservedName)
    {
        private readonly Dictionary<string, HashSet<string>> _usedTokens = new();
        private readonly string _reservedName = reservedName;

        public string GetName(string value, string scope)
        {
            if (!_usedTokens.TryGetValue(scope, out var usedValues))
            {
                usedValues = [];
                _usedTokens.Add(scope, usedValues);
            }

            var currentValue = value;
            while (usedValues.Contains(currentValue) || currentValue == _reservedName)
            {
                currentValue += "_";
            }

            usedValues.Add(currentValue);
            return currentValue;
        }
    }
}

internal sealed record TsSdkMessagesTemplateModel
{
    public required List<TsSdkMessageModel> Messages { get; init; }
}

internal sealed record TsSdkMessageModel
{
    public required string Name { get; init; }

    public required string Message { get; init; }
}

internal sealed record TsOptionSetsTemplateModel
{
    public required List<TsOptionSetModel> OptionSets { get; init; }
}

internal sealed record TsBusinessProcessFlowTemplateModel
{
    public required string TypingPath { get; init; }

    public required string ModuleName { get; init; }

    public required string ProcessName { get; init; }

    public required string ProcessId { get; init; }

    public required List<TsBusinessProcessFlowStageModel> Stages { get; init; }
}

internal sealed record TsBusinessProcessFlowStageModel
{
    public required string Name { get; init; }

    public required string DisplayName { get; init; }

    public required List<string> Ids { get; init; }

    public required List<TsNamedIdModel> NamedIds { get; init; }
}

internal sealed record TsNamedIdModel
{
    public required string Name { get; init; }

    public required string Value { get; init; }
}

internal sealed record TsEntityRefTemplateModel
{
    public required string TypingPath { get; init; }

    public required string ModuleName { get; init; }

    public required string EntityLogicalName { get; init; }

    public required string EntitySummary { get; init; }

    public required List<TsAttributeConstantModel> Attributes { get; init; }

    public required bool IncludeOptions { get; init; }

    public required List<TsOptionSetModel> Optionsets { get; init; }
}

internal sealed record TsEntityTemplateModel
{
    public required string TypingPath { get; init; }

    public required string ModuleName { get; init; }

    public required string EntityLogicalName { get; init; }

    public required string EntitySummary { get; init; }

    public required List<TsAttributeConstantModel> AttributeConstants { get; init; }

    public required List<TsAttributeFieldModel> AttributeFields { get; init; }

    public required bool IncludeOptions { get; init; }

    public required List<TsOptionSetModel> Optionsets { get; init; }
}

internal sealed record TsEntityFormTemplateModel
{
    public required string TypingPath { get; init; }

    public required string ModuleName { get; init; }

    public required string EntityLogicalName { get; init; }

    public required List<TsAttributeConstantModel> AttributeConstants { get; init; }

    public required List<TsNamedValueModel> TabConstants { get; init; }

    public required List<TsNamedValueModel> SectionConstants { get; init; }

    public required List<TsNamedValueModel> GridConstants { get; init; }

    public required List<TsAttributeFieldModel> AttributeFields { get; init; }

    public required List<TsNamedValueModel> GridFields { get; init; }

    public required bool IncludeOptions { get; init; }

    public required List<TsOptionSetModel> Optionsets { get; init; }

    public required List<TsFormTabModel> FormTabs { get; init; }

    public required List<TsFormTabSectionClassModel> TabSectionClasses { get; init; }
}

internal sealed record TsAttributeConstantModel
{
    public required string Name { get; init; }

    public required string LogicalName { get; init; }
}

internal sealed record TsAttributeFieldModel
{
    public required string Name { get; init; }

    public required string LogicalName { get; init; }

    public required string Summary { get; init; }

    public required string AttributeType { get; init; }

    public required string ControlType { get; init; }

    public required string XrmEnum { get; init; }
}

internal sealed record TsOptionSetModel
{
    public required string Name { get; init; }

    public required List<TsOptionValueModel> Options { get; init; }
}

internal sealed record TsOptionValueModel
{
    public required string Name { get; init; }

    public required string Label { get; init; }

    public required int? Value { get; init; }
}

internal sealed record TsNamedValueModel
{
    public required string Name { get; init; }

    public required string Value { get; init; }
}

internal sealed record TsFormTabModel
{
    public required string Name { get; init; }

    public required string Value { get; init; }
}

internal sealed record TsFormTabSectionClassModel
{
    public required string Name { get; init; }

    public required List<TsNamedValueModel> Sections { get; init; }
}
