// Copyright (c) DIGITALL Nature. All rights reserved
// DIGITALL Nature licenses this file to you under the Microsoft Public License.

namespace dgt.power.codegeneration.Model;

public class FormDetail
{
    public FormDetail()
    {
        Tabs = new Dictionary<string, List<string>>();
        Fields = new HashSet<string>();
        Grids = new HashSet<string>();
    }

    public Dictionary<string, List<string>> Tabs { get; }
    public HashSet<string> Fields { get; }
    public HashSet<string> Grids { get; }
}