// Copyright (c) DIGITALL Nature. All rights reserved
// DIGITALL Nature licenses this file to you under the Microsoft Public License.

namespace dgt.power.codegeneration.Templates.tsl.ViewModels;
// ReSharper disable UnusedAutoPropertyAccessor.Global

public record SdkMessagesViewModel
{
    public IReadOnlyList<SdkMessageViewModel> SdkMessages { get; init; } = [];
}
