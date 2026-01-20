// Copyright (c) DIGITALL Nature. All rights reserved
// DIGITALL Nature licenses this file to you under the Microsoft Public License.

using System.Runtime.InteropServices;

namespace dgt.power.codegeneration.Model;

public class FormDetail
{
    public Dictionary<string, List<string>> Tabs { get; }
    public HashSet<string> Attributes { get; }
    public HashSet<string> OptionalAttributes { get; }
    public HashSet<string> FormControls { get; }
    public HashSet<string> Grids { get; }
    public HashSet<string> HeaderControlFields { get; }
    public int FormType { get; set; }
    public List<KeyValuePair<string, TabDetail>> TabDetails { get; } = [];
    public FormDetail()
    {
        Tabs = new Dictionary<string, List<string>>();
        Attributes = new HashSet<string>();
        OptionalAttributes = new HashSet<string>();
        FormControls = new HashSet<string>();
        HeaderControlFields = new HashSet<string>();
        Grids = new HashSet<string>();
    }
}
