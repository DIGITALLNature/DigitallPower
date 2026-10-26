// SPDX-FileCopyrightText: DIGITALL Nature
// SPDX-License-Identifier: MS-RL

namespace dgt.power.plugin.Deployment.Local;

/// <summary>
/// Declarative SDK message processing step parsed from a <c>PluginRegistrationAttribute</c>. Message/message-filter
/// identifiers are intentionally not resolved here - that requires a Dataverse lookup and is done
/// by the Dataverse layer when the step is compared with the target environment.
/// </summary>
public sealed record LocalPluginStep(
    string Name,
    int Mode,
    string MessageName,
    int Stage,
    string PrimaryEntityName,
    string SecondaryEntityName,
    IReadOnlyList<string>? FilterAttributes,
    int? ExecutionOrder,
    IReadOnlyList<LocalPluginStepImage> Images);
