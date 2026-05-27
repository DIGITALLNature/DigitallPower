// Copyright (c) DIGITALL Nature. All rights reserved
// DIGITALL Nature licenses this file to you under the Microsoft Public License.

namespace dgt.power.codegeneration.Model;

public class WfAction(string logicalName)
{
    public string LogicalName { get; private set; } = logicalName;

    public List<WfParameter> InParameters { get; set; } = new();

    public List<WfParameter> OutParameters { get; set; } = new();
}