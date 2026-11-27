// SPDX-FileCopyrightText: DIGITALL Nature
// SPDX-License-Identifier: MS-RL

namespace dgt.power.webresource.Scaffolding;

internal sealed record WebResourceScaffoldingProgress(
    WebResourceScaffoldingProgress.Stage CurrentStage,
    long BytesReceived = 0,
    long? TotalBytes = null)
{
    internal enum Stage
    {
        Resolving,
        Downloading,
        Creating
    }
}
