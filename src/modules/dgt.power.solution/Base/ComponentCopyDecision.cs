// SPDX-FileCopyrightText: DIGITALL Nature
// SPDX-License-Identifier: MS-RL

namespace dgt.power.solution.Base;

/// <summary>What copy-components decided for a single (componenttype, objectid) pair, and why - used for both the dry-run report and the actual <c>AddSolutionComponentRequest</c> call.</summary>
public sealed record ComponentCopyDecision(
    int ComponentType,
    Guid ObjectId,
    string ComponentTypeName,
    bool Include,
    bool DoNotIncludeSubcomponents,
    string Reason,
    bool IsAppBound = false);
