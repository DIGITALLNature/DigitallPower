// SPDX-FileCopyrightText: DIGITALL Nature
// SPDX-License-Identifier: MS-RL

using Spectre.Console.Cli.Help;
using Spectre.Console.Rendering;

namespace dgt.power.Completion;

/// <summary>
/// An <see cref="IHelpProvider"/> that captures the Spectre.Console command model
/// without producing any console output. Used in the dotnet-suggest path to obtain
/// the full <see cref="ICommandModel"/> for completion generation.
/// </summary>
internal sealed class ModelCaptureHelpProvider : IHelpProvider
{
    public ICommandModel? Model { get; private set; }

    public IEnumerable<IRenderable> Write(ICommandModel model, ICommandInfo? command)
    {
        Model = model;
        return [];
    }
}
