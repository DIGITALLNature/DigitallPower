// Copyright (c) DIGITALL Nature. All rights reserved
// DIGITALL Nature licenses this file to you under the Microsoft Public License.

using dgt.power.codegeneration.Base;
using dgt.power.codegeneration.Logic;
using Microsoft.Xrm.Sdk;
using Microsoft.Xrm.Sdk.Metadata;

namespace dgt.power.codegeneration.Templates.ts
{
    public partial class D365EntityTemplate : ITemplate
    {
        private readonly Dictionary<string, List<string>> _usedTokens = new();
        private readonly string TypingPath;
        private readonly EntityMetadata EntityMetadata;
        private readonly bool SuppressOptions;
        private readonly CodeGenerationConfig _cfg;
        private readonly int _systemLanguage;

        public D365EntityTemplate(string typingPath, EntityMetadata entity, CodeGenerationConfig cfg, int systemLanguage)
        {
            TypingPath = typingPath;
            EntityMetadata = entity;
            SuppressOptions = cfg.SuppressOptions;
            _cfg = cfg;
            _systemLanguage = systemLanguage;
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

        private IEnumerable<AttributeMetadata> Filter(AttributeMetadata[] attributes)
        {
            var filter = attributes
               .Where(a => (a.IsValidForGrid == true || a.IsValidForForm == true || a.IsValidODataAttribute == true || a.IsPrimaryId == true) && (a.IsValidForCreate == true || a.IsValidForUpdate == true || a.IsValidForRead == true))
               .Where(a => !a.LogicalName.Contains("entityimage"))
               .Where(a => a.AttributeType != AttributeTypeCode.ManagedProperty);

            if (_cfg.EntityFilters.Any())
            {
                var match = _cfg.EntityFilters.FirstOrDefault(e => e.Entity == EntityMetadata.LogicalName);
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
               .Where(a => ((a.IsValidForGrid == true || a.IsValidForForm == true || a.IsValidODataAttribute == true) && (a.IsValidForCreate == true || a.IsValidForUpdate == true || a.IsValidForRead == true))
                           && (a.AttributeType == AttributeTypeCode.Picklist ||
                               a.AttributeType == AttributeTypeCode.State ||
                               a.AttributeType == AttributeTypeCode.Status ||
                               a.AttributeType == AttributeTypeCode.Boolean ||
                               a.AttributeType == AttributeTypeCode.Virtual &&
                               a.AttributeTypeName?.Value == "MultiSelectPicklistType")).Select(o => new Templates.OptionField(o, _cfg.UseBaseLanguage, _systemLanguage));

            if (_cfg.EntityFilters.Any())
            {
                var match = _cfg.EntityFilters.FirstOrDefault(e => e.Entity == EntityMetadata.LogicalName);
                if (match?.Optionsets != null && match.Optionsets.Length > 0)
                {
                    filter = filter.Where(a => match.Optionsets.Contains(a.LogicalName));
                }
            }

            return filter.OrderBy(a => a.LogicalName);
        }

        private static string Sanitize(string value, bool allowWhitespace = false, bool allowSafeStringChars = false, bool allowFirstNumber = false)
        {
            return Formatter.Sanitize(value, allowWhitespace, allowSafeStringChars, allowFirstNumber);
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

        public string GenerateTemplate() => TransformText();
    }
}
