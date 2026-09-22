// Copyright (c) DIGITALL Nature. All rights reserved
// DIGITALL Nature licenses this file to you under the Microsoft Public License.

namespace dgt.power.plugin.Planning;

public sealed class PluginPlanNode(string label, string action, string? icon = null)
{
    private readonly List<PluginPlanNode> _children = [];

    public string Label { get; } = label;

    public string Action { get; } = action;

    public string? Icon { get; } = icon;

    public IReadOnlyList<PluginPlanNode> Children => _children;

    public void AddChild(PluginPlanNode child)
    {
        ArgumentNullException.ThrowIfNull(child);
        _children.Add(child);
    }
}
