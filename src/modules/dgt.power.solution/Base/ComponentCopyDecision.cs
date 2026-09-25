// Copyright (c) DIGITALL Nature. All rights reserved
// DIGITALL Nature licenses this file to you under the Microsoft Public License.

namespace dgt.power.solution.Base;

/// <summary>What copy-components decided for a single (componenttype, objectid) pair, and why - used for both the dry-run report and the actual <c>AddSolutionComponentRequest</c> call.</summary>
public sealed record ComponentCopyDecision(
    int ComponentType,
    Guid ObjectId,
    string ComponentTypeName,
    bool Include,
    bool DoNotIncludeSubcomponents,
    string Reason);
