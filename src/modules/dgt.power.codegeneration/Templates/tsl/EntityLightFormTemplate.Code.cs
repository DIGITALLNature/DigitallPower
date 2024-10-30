// Copyright (c) DIGITALL Nature. All rights reserved
// DIGITALL Nature licenses this file to you under the Microsoft Public License.

using dgt.power.codegeneration.Base;
using dgt.power.codegeneration.Logic;
using dgt.power.codegeneration.Model;
using dgt.power.codegeneration.Templates.ts;
using dgt.power.dataverse;
using Microsoft.Xrm.Sdk;
using Microsoft.Xrm.Sdk.Metadata;

namespace dgt.power.codegeneration.Templates.tsl;

public partial class EntityLightFormTemplate(string typingPath,string form, string formName, FormDetail formDetail, EntityMetadata entityMetadata, CodeGenerationConfig cfg, int systemLanguage) : ITemplate
{
    private BaseTemplate BaseTemplate = new(entityMetadata, systemLanguage, cfg.UseBaseLanguage);

    private IEnumerable<AttributeMetadata> Filter(AttributeMetadata[] attributes)
    {
        var filter = attributes
            .Where(a => (a.IsValidForGrid == true || a.IsValidForForm == true || a.IsValidODataAttribute == true || a.IsPrimaryId == true) && (a.IsValidForCreate == true || a.IsValidForUpdate == true || a.IsValidForRead == true))
            .Where(a => !a.LogicalName.Contains("entityimage"))
            .Where(a => a.AttributeType != AttributeTypeCode.ManagedProperty)
            .Where(a => formDetail.Fields.Contains(a.LogicalName));

        if (cfg.EntityFormFilters.Count != 0)
        {
            var match = cfg.EntityFormFilters.FirstOrDefault(e => e.EntityForm == form);
            if (match?.Attributes != null && match.Attributes.Length > 0)
            {
                filter = filter.Where(a => match.Attributes.Contains(a.LogicalName));
            }
        }

        return filter.OrderBy(a => a.LogicalName);
    }

    public string GenerateTemplate() => TransformText();

    public struct TypeScriptType(string definitelyTypedAttributeType, string definitelyTypedControlType, string definitelyType)
    {
        public string DefinitelyTypedAttributeType = definitelyTypedAttributeType;
        public string DefinitelyTypedControlType = definitelyTypedControlType;
        public string DefinitelyType = definitelyType;
    }
}

