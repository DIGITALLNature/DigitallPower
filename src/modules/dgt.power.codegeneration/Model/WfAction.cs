// SPDX-FileCopyrightText: DIGITALL Nature
#pragma warning disable CA1002 // InParameters and OutParameters are mutated via .Add() after construction
// SPDX-License-Identifier: MS-RL

namespace dgt.power.codegeneration.Model;

public class WfAction(string logicalName)
{
    public string LogicalName { get; private set; } = logicalName;

    public List<WfParameter> InParameters { get; init; } = new();

    public List<WfParameter> OutParameters { get; init; } = new();
}