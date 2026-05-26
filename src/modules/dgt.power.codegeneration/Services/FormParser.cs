// Copyright (c) DIGITALL Nature. All rights reserved
// DIGITALL Nature licenses this file to you under the Microsoft Public License.

using System.Globalization;
using dgt.power.codegeneration.Logic;
using System.Text.RegularExpressions;
using System.Xml;
using dgt.power.codegeneration.Constants;
using dgt.power.codegeneration.Model;
using dgt.power.dataverse;
using Microsoft.Xrm.Sdk;
using Spectre.Console;

namespace dgt.power.codegeneration.Services
{
    public static class FormParser
    {
        /// <summary>
        /// Parse form values
        /// </summary>
        /// <param name="form"></param>
        /// <param name="formUniqueName"></param>
        /// <param name="entityLogicalName"></param>
        /// <param name="bpfControls"></param>
        /// <returns></returns>
        public static FormDetail ParseForm(Entity form, string formUniqueName, string entityLogicalName, SortedSet<BpfControlDetail>? bpfControls)
        {
            var formId = form.GetAttributeValue<Guid>(SystemForm.LogicalNames.FormId).ToString();
            var formxml = form.GetAttributeValue<string>(SystemForm.LogicalNames.FormXml);
            var formType = form.GetAttributeValue<OptionSetValue>(SystemForm.LogicalNames.Type).Value;
            var doc = new XmlDocument();
            doc.LoadXml(formxml);

            var formDetail = new FormDetail()
            {
                FormType = formType,
                FormTypeName = FormatFormType(GetFormType(formType)),
                FormUniqueName = formUniqueName,
                FormEntityName = entityLogicalName,
                FormId = formId
            };

            var tabs = doc.SelectNodes("/form/tabs/tab[*]");
            if (tabs == null)
            {
                return formDetail;
            }

            foreach (XmlNode tab in tabs)
            {
                // Map form xlm tab and sections
                MapFormXmlTabIntoFormDetail(formDetail, doc, tab);
            }

            // Map form xml header controls
            MapFormXmlHeaderControlsIntoFormDetail(formDetail, doc);

            // Map bp process controls that will be mapped as "header_process_"
            MapBpfControlsAttributesIntoFormDetail(formDetail, formType, bpfControls);
            return formDetail;

        }

        /// <summary>
        /// Map quick form id to generated form class
        /// </summary>
        /// <param name="formDetail"></param>
        /// <param name="allForms"></param>
        /// <returns></returns>
        public static void MapQuickFormId(FormDetail formDetail, List<FormDetail> allForms)
        {
            foreach(var quickView in formDetail.QuickViews)
            {
                quickView.QuickViewFormClass = GetQuickViewFormClass(quickView.QuickViewFormId, allForms) ?? quickView.QuickViewFormClass ?? "Xrm.Controls.QuickFormControl";
            }
        }

        /// <summary>
        /// Map form xml tab
        /// </summary>
        /// <param name="formDetail"></param>
        /// <param name="doc"></param>
        /// <param name="tab"></param>
        private static void MapFormXmlTabIntoFormDetail(FormDetail formDetail, XmlDocument doc, XmlNode tab)
        {
            var tabId = Regex.Replace(tab.Attributes?["id"]?.Value?.ToUpperInvariant() ?? "", "[{}]", "", RegexOptions.NonBacktracking);
            var tabName = tab.Attributes?["name"]?.Value;
            if (string.IsNullOrWhiteSpace(tabName) && formDetail.FormType != SystemForm.Options.Type.QuickViewForm)
            {
                AnsiConsole.MarkupLine($"[bold red]Warning:[/] tabName empty for form: {formDetail.FormEntityName} - {formDetail.FormTypeName} - {formDetail.FormUniqueName}");
            }
            var tabDetailName = !string.IsNullOrWhiteSpace(tabName) ? tabName : tabId;
            if (tabDetailName != null)
            {
                var tabDetail = new TabDetail()
                {
                    TabName = tabDetailName
                };
                var columns = tab.SelectNodes(".//columns/column[*]");
                var sectionList = new List<string>();
                if(columns != null)
                {
                    // Loop over columns in tab
                    foreach (XmlNode column in columns)
                    {
                        // Loop over sections in the column
                        MapFormXmlTabColumnIntoFormDetail(formDetail, doc, tabDetail, column, sectionList);
                    }
                }
                formDetail.Tabs.Add(tabDetailName, sectionList);
                formDetail.TabDetails.Add(tabDetail);
            }
        }

        /// <summary>
        /// Map form xml tab column into form detail
        /// </summary>
        /// <param name="formDetail"></param>
        /// <param name="doc"></param>
        /// <param name="tabDetail"></param>
        /// <param name="column"></param>
        /// <param name="sectionList"></param>
        private static void MapFormXmlTabColumnIntoFormDetail(
            FormDetail formDetail,
            XmlDocument doc,
            TabDetail tabDetail,
            XmlNode column,
            List<string> sectionList)
        {
            var sections = column.SelectNodes(".//sections/section[*]");
            if (sections == null)
            {
                return;
            }
            foreach (XmlNode section in sections)
            {
                var sectionName = section.Attributes?["name"]?.Value;
                if (string.IsNullOrWhiteSpace(sectionName) && formDetail.FormType != SystemForm.Options.Type.QuickViewForm)
                {
                    AnsiConsole.MarkupLine($"[bold red]Warning:[/] section name is empty for tab {tabDetail.TabName} in form {formDetail.FormEntityName} - {formDetail.FormTypeName} - {formDetail.FormUniqueName}");
                    continue;
                }
                MapFormXmlSectionIntoFormDetail(formDetail, doc, tabDetail, sectionList, section, sectionName);
            }
        }

        /// <summary>
        /// Maps form xml tab secntion in the form detail
        /// </summary>
        /// <param name="formDetail"></param>
        /// <param name="doc"></param>
        /// <param name="tabDetail"></param>
        /// <param name="sectionList"></param>
        /// <param name="section"></param>
        /// <param name="sectionName"></param>
        private static void MapFormXmlSectionIntoFormDetail(FormDetail formDetail, XmlDocument doc, TabDetail tabDetail, List<string> sectionList, XmlNode section, string? sectionName)
        {
            var sectionDetail = new SectionDetail();
            if(!string.IsNullOrWhiteSpace(sectionName))
            {
                sectionList.Add(sectionName);
                tabDetail.Sections.Add(new KeyValuePair<string, SectionDetail>(sectionName, sectionDetail));
            }

            var rows = section.SelectNodes(".//rows/row[*]");
            if(rows == null)
            {
                return;
            }
            foreach (XmlNode row in rows)
            {
                var cells = row.SelectNodes(".//cell[*]");
                if (cells == null)
                {
                    continue;
                }
                foreach (XmlNode cell in cells)
                {
                    var control = cell.SelectSingleNode(".//control");
                    if (control != null)
                    {
                        MapFormXmlSectionControlsIntoFormDetail(formDetail, doc, sectionDetail, control);
                    }
                }
            }
        }

        /// <summary>
        /// Map section detail controls
        /// </summary>
        /// <param name="formDetail"></param>
        /// <param name="doc"></param>
        /// <param name="sectionDetail"></param>
        /// <param name="control"></param>
        private static void MapFormXmlSectionControlsIntoFormDetail(FormDetail formDetail, XmlDocument doc, SectionDetail sectionDetail, XmlNode control)
        {
            var fieldName = control.Attributes?["datafieldname"]?.Value;
            var controlId = control.Attributes?["id"]?.Value;
            if (fieldName != null)
            {
                // Map as form attribute
                formDetail.Attributes.Add(new FormAttributeData
                {
                    DataFieldName = fieldName,
                    IsOptionalAttribute = false
                });
            }
            if (controlId != null)
            {
                var newFormControl = MapSectionControlToFormXmlControl(control, doc, controlId, fieldName);
                
                if (newFormControl != null)
                {
                    // Map as web resource control
                    var countRepeatedControlFields = formDetail.FormControls
                        .Count(formControl => !string.IsNullOrWhiteSpace(formControl.DataFieldName) &&
                            !string.IsNullOrWhiteSpace(formControl.ControlId) &&
                            formControl.ControlId == newFormControl.ControlId);
                    if (countRepeatedControlFields > 0)
                    {
                        newFormControl.RepeatedControl = countRepeatedControlFields;
                        newFormControl.ControlId += newFormControl.RepeatedControl.ToString(CultureInfo.InvariantCulture);
                    }
                    formDetail.FormControls.Add(newFormControl);
                    sectionDetail.ControlNames.Add(newFormControl.ControlId);
                    if (newFormControl.IsSubgrid)
                    {
                        // Map controls as subgrid
                        formDetail.Grids.Add(controlId);
                    }

                    if(newFormControl.ClassId == ControlClassNames.XrmClassId.QuickView)
                    {
                        MapFormXmlQuickViewControlsIntoFormDetailQuickViews(formDetail, control, controlId);
                    }
                }
            }
        }

        /// <summary>
        /// Map Section control to form xml control
        /// </summary>
        /// <param name="control"></param>
        /// <param name="doc"></param>
        /// <param name="controlId"></param>
        /// <param name="fieldName"></param>
        /// <returns></returns>
        private static FormXmlControlData? MapSectionControlToFormXmlControl(XmlNode control, XmlDocument doc, string controlId, string? fieldName)
        {
            var uniqueId = control.Attributes?["uniqueid"]?.Value;
            var isSubgrid = control.Attributes?["indicationOfSubgrid"]?.Value == "true";
            var classId = MapClassId(control.Attributes?["classid"]?.Value);
            var isWebResource = controlId.StartsWith("WebResource_", StringComparison.InvariantCulture);
            var isVisible = control.ParentNode?.Attributes?["visible"]?.Value == "false";
            var isDisabled = control.Attributes?["disabled"]?.Value == "true";
            // Map control as normal control
            return new FormXmlControlData
            {
                DataFieldName = fieldName ?? string.Empty,
                ControlId = controlId,
                IsSubgrid = isSubgrid,
                IsWebResource = isWebResource,
                IsVisible = isVisible,
                IsDisabled = isDisabled,
                ClassId = MapClassId(classId),
                CustomControlClass = GetCustomControlClass(doc, classId, uniqueId),
                RepeatedControl = 0
            };
        }

        /// <summary>
        /// Map form quick view controls into form detail quick view names
        /// </summary>
        /// <param name="formDetail"></param>
        /// <param name="control"></param>
        /// <param name="controlId"></param>
        private static void MapFormXmlQuickViewControlsIntoFormDetailQuickViews(FormDetail formDetail, XmlNode control, string controlId)
        {
            var quickFormParameters = control.SelectNodes(".//parameters/QuickForms");
            if (quickFormParameters != null && quickFormParameters.Count > 0 && quickFormParameters[0]?.InnerText != null)
            {
                var quickViewFormId = RetrieveQuickViewFormId(quickFormParameters[0]?.InnerText);
                var quickControl = new QuickViewFormControl
                {
                    ControlName = controlId,
                    QuickViewFormId = quickViewFormId,
                    QuickViewFormClass = "Xrm.Controls.QuickFormControl"
                };
                formDetail.QuickViews.Add(quickControl);
            }
        }

        /// <summary>
        /// Retrieve quick view form id from the inner xml
        /// </summary>
        /// <param name="quickViewFormIdNodeText"></param>
        /// <returns></returns>
        private static string RetrieveQuickViewFormId(string? quickViewFormIdNodeText)
        {
            if(string.IsNullOrWhiteSpace(quickViewFormIdNodeText))
            {
                return string.Empty;
            }
            var doc = new XmlDocument();
            doc.LoadXml(quickViewFormIdNodeText);
            return doc.SelectSingleNode("//QuickFormIds/QuickFormId")?.InnerText?.ToLowerInvariant() ?? string.Empty;
        }
        /// <summary>
        /// Map form xml header controls into the attributes and header controls
        /// </summary>
        /// <param name="formDetail"></param>
        /// <param name="doc"></param>
        private static void MapFormXmlHeaderControlsIntoFormDetail(FormDetail formDetail, XmlDocument doc)
        {
            var headerControls = doc.SelectNodes("./form/header/rows/row/cell/control");
            if(headerControls == null)
            {
                return;
            }
            foreach (XmlNode headerControl in headerControls)
            {
                if (headerControl != null && headerControl.Attributes?["datafieldname"] != null)
                {
                    var headerFieldName = headerControl.Attributes["datafieldname"]?.Value;
                    if (string.IsNullOrWhiteSpace(headerFieldName))
                    {
                        continue;
                    }
                    formDetail.HeaderControlFields.Add(headerFieldName);
                    if (!formDetail.Attributes.Any(x => x.DataFieldName == headerFieldName))
                    {
                        formDetail.Attributes.Add(new FormAttributeData
                        {
                            DataFieldName = headerFieldName,
                            IsOptionalAttribute = false
                        });
                    }
                }
            }
        }

        /// <summary>
        /// Map business process flow controls as optional attributes into the form
        /// </summary>
        /// <param name="formDetail"></param>
        /// <param name="formType"></param>
        /// <param name="bpfControls"></param>
        private static void MapBpfControlsAttributesIntoFormDetail(FormDetail formDetail, int formType, SortedSet<BpfControlDetail>? bpfControls)
        {
            // Exclude header process controls in case of quick view forms
            if (bpfControls != null && formType != SystemForm.Options.Type.QuickViewForm)
            {
                var bfControlFieldNames = bpfControls
                    .Where(bpfControl => !formDetail.Attributes.Any(x => x.DataFieldName == bpfControl.DataFieldName))
                    .Select(bpfControl => bpfControl.DataFieldName);
                foreach (var bpfControlFieldName in bfControlFieldNames)
                {
                    // bpf process controls should be added to the optional attributes only if not in attributes already
                    formDetail.Attributes.Add(new FormAttributeData
                    {
                        DataFieldName = bpfControlFieldName,
                        IsOptionalAttribute = true
                    });
                    
                }
            }
        }

        /// <summary>
        /// Function to get the custom control class id from the xml document
        /// </summary>
        /// <param name="doc"></param>
        /// <param name="classId"></param>
        /// <param name="uniqueId"></param>
        /// <returns></returns>
        private static string GetCustomControlClass(XmlDocument doc, string classId, string? uniqueId)
        {
            // If custom control, the control type is found in the form xml control descriptions
            if (classId == ControlClassNames.XrmClassId.CustomControl && !string.IsNullOrWhiteSpace(uniqueId))
            {
                var customControl = doc.SelectNodes($"/form/controlDescriptions/controlDescription[@forControl='{uniqueId}']/customControl[@id[not(.='')]]");
                var customControlQuickView = doc.SelectNodes($"/form/controlDescriptions/controlDescription[@forControl='{uniqueId}']/customControl/parameters/QuickForms");
                if (customControl != null && customControl.Count == 1)
                {
                    if(customControlQuickView != null &&
                        customControlQuickView.Count > 1 &&
                        customControlQuickView[0]?.InnerText != null) {
                        return ControlClassNames.XrmClassId.QuickView;
                    }
                    return MapClassId(customControl[0]?.Attributes?["id"]?.Value);
                }
            }
            return string.Empty;
        }

        /// <summary>
        /// Simple substitution auxiliary function to map class id
        /// </summary>
        /// <param name="classId"></param>
        /// <returns></returns>
        private static string MapClassId(string? classId)
        {
            if (string.IsNullOrWhiteSpace(classId))
            {
                return string.Empty;
            }
            return Regex.Replace(classId.ToUpperInvariant(), "[{}]", "", RegexOptions.NonBacktracking);
        }

        /// <summary>
        /// Generate quick view form class so it matches with the mapped form name
        /// </summary>
        /// <param name="quickViewFormId"></param>
        /// <param name="allForms"></param>
        /// <returns></returns>
        private static string GetQuickViewFormClass(string quickViewFormId, List<FormDetail> allForms)
        {
            var quickViewForm = allForms.FirstOrDefault(x => x.FormId != null && x.FormId.ToString() == quickViewFormId);
            if (quickViewForm != null)
            {
                return MapFormClass(quickViewForm);
            }
            return "Xrm.Controls.QuickFormControl";
        }

        /// <summary>
        /// Map quick view form class name
        /// </summary>
        /// <param name="form"></param>
        /// <returns></returns>
        private static string MapFormClass(FormDetail form)
        {
            var schemaName = Formatter.CamelCase(form.FormEntityName);
            var formType = Formatter.CamelCase(Formatter.Sanitize((form.FormTypeName)));
            var formName = Formatter.CamelCase(Formatter.Sanitize(form.FormUniqueName));
            return $"XrmForm.{schemaName}.{formType}.{formName}.QuickFormControl";
        }

        public static string FormatFormType(string formType)
        {
             return formType
                .Replace("main", "Main")
                .Replace("quickview", "Quick View")
                .Replace("quickcreate", "Quick Create");
        }

        public static string GetFormType(int? type)
        {
            return type switch
            {
                2 => "main",
                6 => "quickview",
                _ => "quickcreate"
            };
        }
    }
}
