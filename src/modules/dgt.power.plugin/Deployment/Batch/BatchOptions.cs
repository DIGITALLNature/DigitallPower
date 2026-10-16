// Copyright (c) DIGITALL Nature. All rights reserved
// DIGITALL Nature licenses this file to you under the Microsoft Public License.

namespace dgt.power.plugin.Deployment.Batch;

/// <param name="Solution">Unique name of the solution that receives memberships; <see langword="null"/> to skip.</param>
/// <param name="PublisherPrefix">Publisher customization prefix; required when the batch contains a package.</param>
/// <param name="DryRun">Report only; never write to Dataverse. Takes precedence over confirmation.</param>
/// <param name="Confirm">Prompt before applying each target that has changes.</param>
/// <param name="NonInteractive">Suppress prompts.</param>
/// <param name="IsCiAgent">Suppress prompts on CI agents.</param>
internal sealed record BatchOptions(string? Solution, string? PublisherPrefix, bool DryRun, bool Confirm, bool NonInteractive, bool IsCiAgent);
