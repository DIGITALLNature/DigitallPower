// Copyright (c) DIGITALL Nature. All rights reserved
// DIGITALL Nature licenses this file to you under the Microsoft Public License.

namespace dgt.power.codegeneration.Model;

public class FormDetail
{
    public SortedSet<string> HeaderControlFields { get; }
    public SortedSet<string> Grids { get; }
    public SortedSet<FormAttributeData> Attributes { get; }
    public SortedSet<FormXmlControlData> FormControls { get; }
    public SortedSet<TabDetail> TabDetails { get; }
    public int FormType { get; set; }
    public SortedDictionary<string, List<string>> Tabs { get; }

    public FormDetail()
    {
        Attributes = [];
        FormControls = [];
        Grids = [];
        HeaderControlFields = [];
        TabDetails = [];
        Tabs = [];
    }
}
