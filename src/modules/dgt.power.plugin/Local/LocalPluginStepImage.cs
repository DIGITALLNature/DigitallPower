// Copyright (c) DIGITALL Nature. All rights reserved
// DIGITALL Nature licenses this file to you under the Microsoft Public License.

namespace dgt.power.plugin.Local;

/// <summary>
/// Declarative pre/post image for a <see cref="LocalPluginStep"/>, parsed from a
/// <c>PluginRegistrationAttribute</c>. Contains no Dataverse identifiers - those are resolved
/// later when the step is compared with the target environment.
/// </summary>
/// <param name="ImageType">SdkMessageProcessingStepImage.Options.ImageType value (PreImage/PostImage).</param>
/// <param name="Name">Image name (e.g. "PreImage"/"PostImage").</param>
/// <param name="EntityAlias">Image entity alias, mirrors <paramref name="Name"/> for default images.</param>
/// <param name="MessagePropertyName">Name of the message property the image is attached to (e.g. "Target").</param>
/// <param name="Attributes">Attributes to include on the image; null means all attributes.</param>
public sealed record LocalPluginStepImage(
    int ImageType,
    string Name,
    string EntityAlias,
    string MessagePropertyName,
    IReadOnlyList<string>? Attributes);
