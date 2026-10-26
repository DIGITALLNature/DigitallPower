// SPDX-FileCopyrightText: DIGITALL Nature
// SPDX-License-Identifier: MS-RL

namespace dgt.power.codegeneration.Templates.tsl.ViewModels;
// ReSharper disable UnusedAutoPropertyAccessor.Global

public record SdkMessagesViewModel
{
    public IReadOnlyList<SdkMessageViewModel> SdkMessages { get; init; } = [];
}
