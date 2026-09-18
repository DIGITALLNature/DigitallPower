// Copyright (c) DIGITALL Nature. All rights reserved
// DIGITALL Nature licenses this file to you under the Microsoft Public License.

using dgt.power.common.Exceptions;

namespace dgt.power.plugin;

/// <summary>
/// Thrown when a target assembly contains one or more workflow activities (types deriving from
/// <c>System.Activities.CodeActivity</c>). Workflow activities are a legacy Dataverse plugin type
/// that is not supported by <c>plugin push</c> - callers should migrate to a Custom API instead
/// (the supported replacement for invokable-action scenarios); the legacy <c>push</c> command
/// still registers workflow activities as-is in the meantime.
/// Exit code: <see cref="dgt.power.common.Commands.ExitCode.NotSupported"/> (3).
/// </summary>
[Serializable]
public sealed class WorkflowActivityNotSupportedException : AbstractPowerException
{
    public WorkflowActivityNotSupportedException(string assemblyName, IReadOnlyList<string> workflowTypeNames)
        : base(BuildMessage(assemblyName, workflowTypeNames))
    {
    }

    private static string BuildMessage(string assemblyName, IReadOnlyList<string> workflowTypeNames) =>
        $"NOT_SUPPORTED: Assembly '{assemblyName}' contains {workflowTypeNames.Count} workflow activity type(s) " +
        $"({string.Join(", ", workflowTypeNames)}). Workflow activities (classic Dataverse workflow/Power Automate " +
        "actions) are deprecated and not supported by 'plugin push'. Replace them with a Custom API - it covers the " +
        "same invokable-action scenarios, is fully supported going forward, and works cross-platform. " +
        "If you still need to register this assembly as-is in the meantime, use the legacy 'push' command.";
}
