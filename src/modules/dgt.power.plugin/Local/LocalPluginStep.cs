// Copyright (c) DIGITALL Nature. All rights reserved
// DIGITALL Nature licenses this file to you under the Microsoft Public License.

namespace dgt.power.plugin.Local;

/// <summary>
/// Declarative SDK message processing step, parsed either from a <c>PluginRegistrationAttribute</c>
/// or synthesized from a <c>CustomDataProviderRegistrationAttribute</c>. Message/message-filter
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
