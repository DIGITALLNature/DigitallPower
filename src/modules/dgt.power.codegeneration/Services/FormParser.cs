// Copyright (c) DIGITALL Nature. All rights reserved
// DIGITALL Nature licenses this file to you under the Microsoft Public License.

using System.Globalization;
using System.Text.RegularExpressions;
using System.Xml;
using dgt.power.codegeneration.Model;
using dgt.power.dataverse;
using Microsoft.Xrm.Sdk;

namespace dgt.power.codegeneration.Services
{
    public static class FormParser
    {
        public static FormDetail ParseForms(Entity form, SortedSet<BpfControlDetail>? bpfControls)
        {
            var formxml = form.GetAttributeValue<string>(SystemForm.LogicalNames.FormXml);
            var formType = form.GetAttributeValue<OptionSetValue>(SystemForm.LogicalNames.Type);
            var doc = new XmlDocument();
            doc.LoadXml(formxml);

            var formDetail = new FormDetail();
            formDetail.FormType = form.GetAttributeValue<OptionSetValue>(SystemForm.LogicalNames.Type).Value;

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
        /// Map form xml tab
        /// </summary>
        /// <param name="formDetail"></param>
        /// <param name="doc"></param>
        /// <param name="tab"></param>
        private static void MapFormXmlTabIntoFormDetail(FormDetail formDetail, XmlDocument doc, XmlNode tab)
        {
            var tabId = Regex.Replace(tab.Attributes?["id"]?.Value?.ToUpperInvariant() ?? "", "[{}]", "", RegexOptions.NonBacktracking);
            var tabName = tab.Attributes?["name"]?.Value;
            var tabDetailName = !string.IsNullOrWhiteSpace(tabName) ? tabName : tabId;
            if (tabDetailName != null)
            {
                var tabDetail = new TabDetail()
                {
                    TabName = tabDetailName,
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
                if (string.IsNullOrWhiteSpace(sectionName))
                {
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
        private static void MapFormXmlSectionIntoFormDetail(FormDetail formDetail, XmlDocument doc, TabDetail tabDetail, List<string> sectionList, XmlNode section, string sectionName)
        {
            sectionList.Add(sectionName);

            var sectionDetail = new SectionDetail();
            tabDetail.Sections.Add(new KeyValuePair<string, SectionDetail>(sectionName, sectionDetail));

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
                    IsOptionalAttribute = false,
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
                }
            }
        }


        private static FormXmlControlData? MapSectionControlToFormXmlControl(XmlNode control, XmlDocument doc, string controlId, string? fieldName)
        {
            var uniqueId = control.Attributes?["uniqueid"]?.Value;
            var isSubgrid = control.Attributes?["indicationOfSubgrid"]?.Value == "true";
            var classId = MapClassId(control.Attributes?["classid"]?.Value);
            var isWebResource = controlId.StartsWith("WebResource_", StringComparison.InvariantCulture);
            // Map control as normal control
            return new FormXmlControlData
            {
                DataFieldName = fieldName ?? string.Empty,
                ControlId = controlId,
                IsSubgrid = isSubgrid,
                IsWebResource = isWebResource,
                ClassId = MapClassId(classId),
                CustomControlClass = GetCustomControlClass(doc, classId, uniqueId),
                RepeatedControl = 0,
            };
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
                            IsOptionalAttribute = false,
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
        private static void MapBpfControlsAttributesIntoFormDetail(FormDetail formDetail, OptionSetValue? formType, SortedSet<BpfControlDetail>? bpfControls)
        {
            // Exclude header process controls in case of quick view forms
            if (bpfControls != null && formType?.Value != SystemForm.Options.Type.QuickViewForm)
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
                        IsOptionalAttribute = true,
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
            if (classId == "F9A8A302-114E-466A-B582-6771B2AE0D92" && !string.IsNullOrWhiteSpace(uniqueId))
            {
                var customControl = doc.SelectNodes($"/form/controlDescriptions/controlDescription[@forControl='{uniqueId}']/customControl[@id[not(.='')]]");
                if (customControl != null && customControl.Count == 1)
                {
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
    }
}
