// Copyright (c) DIGITALL Nature. All rights reserved
// DIGITALL Nature licenses this file to you under the Microsoft Public License.

using dgt.power.codegeneration.Model;

namespace dgt.power.codegeneration.Templates.tsl.ViewModels
{
    public class CustomApiParameterViewModel
    {
        public string ParameterName { get; init; }
        public string ParameterType { get; init; }
        public bool IsOptional { get; init; }

        public CustomApiParameterViewModel(WfParameter parameter)
        {
            ParameterName = parameter.UniqueName ?? string.Empty; // Unique name is a required param in custom api req/responses            
            IsOptional = parameter.IsOptional;
            if (parameter.UniqueName == "Target")
            {
                ParameterName = "entity";
                ParameterType = "Xrm.LookupValue";
                return;
            }
            switch (parameter.Type)
            {
                case "bool":
                    ParameterType = "boolean";
                    break;
                case "DateTime":
                    ParameterType = "Date";
                    break;
                case "decimal":
                case "Money":
                case "float":
                case "int":
                    ParameterType = "number";
                    break;
                case "Guid":
                    if (parameter.IsOutput)
                    {
                        ParameterType = "string";
                        break;
                    }
                    ParameterType = "XrmWebApi.WebApiGuidProperty";
                    break;
                case "EntityReference":
                    ParameterType = "Xrm.LookupValue";
                    break;
                case "EntityCollection":
                    ParameterType = "EntityCollection"; // TOBE CLARIFIED
                    break;
                case "Entity":
                    ParameterType = "Entity"; // TOBE CLARIFIED
                    break;
                case "OptionSetValue":
                    ParameterType = "OptionSetValue"; // TOBE CLARIFIED
                    break;
                default:
                    ParameterType = parameter.Type ?? "string";
                    break;
            }
        }
    }
}
