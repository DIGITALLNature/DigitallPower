// Copyright (c) DIGITALL Nature. All rights reserved
#pragma warning disable CA1002 // InParameters and OutParameters are mutated via .Add() after construction
// DIGITALL Nature licenses this file to you under the Microsoft Public License.

namespace dgt.power.codegeneration.Model;

public class WfAction(string logicalName)
{
    public string LogicalName { get; private set; } = logicalName;

    public List<WfParameter> InParameters { get; init; } = new();

    public List<WfParameter> OutParameters { get; init; } = new();
}