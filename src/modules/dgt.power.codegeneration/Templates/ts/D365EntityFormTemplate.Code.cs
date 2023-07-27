// Copyright (c) DIGITALL Nature. All rights reserved
// DIGITALL Nature licenses this file to you under the Microsoft Public License.

using dgt.power.codegeneration.Base;
using dgt.power.codegeneration.Logic;
using dgt.power.codegeneration.Model;
using Microsoft.Xrm.Sdk;
using Microsoft.Xrm.Sdk.Metadata;

namespace dgt.power.codegeneration.Templates.ts
{
    public partial class D365EntityFormTemplate
    {
        private readonly Dictionary<string, List<string>> _usedTokens = new();
        private readonly string TypingPath;
        private readonly string FormName;
        private readonly string Form;//technical name
        private readonly FormDetail FormDetail;
        private readonly EntityMetadata EntityMetadata;
        private readonly bool SuppressOptions;
        private readonly CodeGenerationConfig _cfg;
        private readonly int _systemLanguage;

        public D365EntityFormTemplate(string typingPath, string form, string formName, FormDetail formDetail, EntityMetadata entity, CodeGenerationConfig cfg, int systemLanguage)
        {
            TypingPath = typingPath;
            FormName = formName;
            Form = form;
            FormDetail = formDetail;
            EntityMetadata = entity;
            SuppressOptions = cfg.SuppressOptions;
            _cfg = cfg;
            _systemLanguage = systemLanguage;
            FilterTabs();
            FilterGrids();
        }

        private List<string> FlatSections()
        {
            var result = new List<string>();
            foreach (var tab in FormDetail.Tabs)
            {
                result.AddRange(FilterSections(tab.Value));
            }
            return result;
        }

        private static string CamelCase(string phrase)
        {
            return Formatter.CamelCase(phrase);
        }

        private string Unique(string value, string scope)
        {
            if (!_usedTokens.ContainsKey(scope)) _usedTokens.Add(scope, new List<string>());

            if (_usedTokens[scope].Contains(value) || value == $"{CamelCase(EntityMetadata.SchemaName)}")
                return Unique(value + "_", scope);

            _usedTokens[scope].Add(value);
            return value;
        }

        private static string Sanitize(string value, bool allowWhitespace = false, bool allowSafeStringChars = false, bool allowFirstNumber = false)
        {
            return Formatter.Sanitize(value, allowWhitespace, allowSafeStringChars, allowFirstNumber);
        }

        private IEnumerable<AttributeMetadata> Filter(AttributeMetadata[] attributes)
        {
            var filter = attributes
                .Where(a => (a.IsValidForCreate == true || a.IsValidForUpdate == true || a.IsValidForRead == true))
                .Where(a => !a.LogicalName.Contains("entityimage"))
                .Where(a => a.AttributeType != AttributeTypeCode.ManagedProperty)
                .Where(a => FormDetail.Fields.Contains(a.LogicalName));

            if (_cfg.EntityFormFilters.Any())
            {
                var match = _cfg.EntityFormFilters.FirstOrDefault(e => e.EntityForm == Form);
                if (match?.Attributes != null && match.Attributes.Length > 0)
                {
                    filter = filter.Where(a => match.Attributes.Contains(a.LogicalName));
                }
            }

            return filter.OrderBy(a => a.LogicalName);
        }

        private IEnumerable<Templates.OptionField> FilterOptions(AttributeMetadata[] attributes)
        {
            var filter = attributes
                .Where(a => (a.IsValidForCreate == true || a.IsValidForUpdate == true || a.IsValidForRead == true)
                            && (a.AttributeType == AttributeTypeCode.Picklist || a.AttributeType == AttributeTypeCode.State ||
                                a.AttributeType == AttributeTypeCode.Status || a.AttributeType == AttributeTypeCode.Boolean ||
                                a.AttributeType == AttributeTypeCode.Virtual &&
                                a.AttributeTypeName?.Value == "MultiSelectPicklistType"))
                .Where(a => FormDetail.Fields.Contains(a.LogicalName))
                .Select(o => new Templates.OptionField(o, _cfg.UseBaseLanguage, _systemLanguage));


            if (_cfg.EntityFormFilters.Any())
            {
                var match = _cfg.EntityFormFilters.FirstOrDefault(e => e.EntityForm == Form);
                if (match?.Optionsets != null && match.Optionsets.Length > 0)
                {
                    filter = filter.Where(a => match.Optionsets.Contains(a.LogicalName));
                }
            }

            return filter.OrderBy(a => a.LogicalName);
        }

        private void FilterTabs()
        {
            if (_cfg.EntityFormFilters.Any())
            {
                var match = _cfg.EntityFormFilters.FirstOrDefault(e => e.EntityForm == Form);
                if (match?.Tabs != null && match.Tabs.Length > 0)
                {
                    var filter = FormDetail.Tabs.Keys.Where(tab => !match.Tabs.Contains(tab)).ToList();
                    foreach (var tab in filter)
                    {
                        FormDetail.Tabs.Remove(tab);
                    }
                }
            }
        }

        private void FilterGrids()
        {
            if (_cfg.EntityFormFilters.Any())
            {
                var match = _cfg.EntityFormFilters.FirstOrDefault(e => e.EntityForm == Form);
                if (match?.Grids != null && match.Grids.Length > 0)
                {
                    var filter = FormDetail.Grids.Where(grid => !match.Grids.Contains(grid)).ToList();
                    foreach (var grid in filter)
                    {
                        FormDetail.Grids.Remove(grid);
                    }
                }
            }
        }

        private IEnumerable<string> FilterSections(List<string> sections)
        {
            var filter = sections.AsEnumerable();

            if (_cfg.EntityFormFilters.Any())
            {
                var match = _cfg.EntityFormFilters.FirstOrDefault(e => e.EntityForm == Form);
                if (match?.Sections != null && match.Sections.Length > 0)
                {
                    filter = filter.Where(section => match.Sections.Contains(section)).ToList();
                }
            }

            return filter;
        }

        private string GetLocalizedLabel(Label label)
        {
            return Formatter.GetLocalizedLabel(label, _cfg.UseBaseLanguage, _systemLanguage);
        }

        private static string Summary(string description, int indent)
        {
            if (string.IsNullOrWhiteSpace(description))
            {
                return string.Empty;
            }
            return "/*" +
                   $"{Environment.NewLine}" +
                   $"{new string('\t', indent)}* {description.Replace("\n", $"\n{new string('\t', indent)}* ").Trim()}" +
                   $"{Environment.NewLine}" +
                   $"{new string('\t', indent)}*/";
        }
    }
}
