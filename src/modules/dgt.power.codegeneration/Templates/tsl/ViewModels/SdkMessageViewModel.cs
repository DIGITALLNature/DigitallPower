// SPDX-FileCopyrightText: DIGITALL Nature
// SPDX-License-Identifier: MS-RL

namespace dgt.power.codegeneration.Templates.tsl.ViewModels;
// ReSharper disable UnusedAutoPropertyAccessor.Global

public record SdkMessageViewModel
{
    public SdkMessageViewModel((string Name, string Message) sdkm)
    {
        Name = sdkm.Name;
        Message = sdkm.Message;
    }

    public string Message { get; init; }

    public string Name { get; init; }
}
