// SPDX-FileCopyrightText: DIGITALL Nature
// SPDX-License-Identifier: MS-RL

namespace dgt.power.solution.Base;

/// <summary>
/// How <c>solution copy-components</c> treats model-driven apps (componenttype 80). Dataverse rejects
/// <c>DoNotIncludeSubcomponents</c> on non-Entity roots, so an app cannot be added without its platform-side expansion.
/// </summary>
public enum AppHandling
{
    /// <summary>Apps are not copied (default).</summary>
    Skip,

    /// <summary>Apps are copied, then every subcomponent Dataverse added on its own (and that is not part of the plan) is removed again.</summary>
    Strip,

    /// <summary>Apps are copied and Dataverse may add whatever subcomponents it considers part of the app.</summary>
    Allow
}
