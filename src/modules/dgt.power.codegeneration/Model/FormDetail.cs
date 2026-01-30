// Copyright (c) DIGITALL Nature. All rights reserved
// DIGITALL Nature licenses this file to you under the Microsoft Public License.

namespace dgt.power.codegeneration.Model;

public class FormDetail
{
    public SortedSet<FormAttributeData> Attributes { get; }
    public SortedSet<FormXmlControlData> FormControls { get; }
    public string FormId { get; set; }
    public string FormUniqueName { get; set; }
    public int FormType { get; set; }
    public string FormTypeName { get; set; }
    public SortedSet<string> Grids { get; }
    public SortedSet<string> HeaderControlFields { get; }
    public SortedSet<string> QuickViews { get; }
    public SortedSet<TabDetail> TabDetails { get; }
    public SortedDictionary<string, List<string>> Tabs { get; }

    public FormDetail()
    {
        Attributes = [];
        FormControls = [];
        FormUniqueName = string.Empty;
        FormTypeName = string.Empty;
        Grids = [];
        HeaderControlFields = [];
        TabDetails = [];
        Tabs = [];
        QuickViews = [];
    }
}
