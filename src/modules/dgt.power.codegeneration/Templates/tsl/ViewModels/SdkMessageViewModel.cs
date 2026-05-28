// Copyright (c) DIGITALL Nature. All rights reserved
// DIGITALL Nature licenses this file to you under the Microsoft Public License.

namespace dgt.power.codegeneration.Templates.tsl.ViewModels;
// ReSharper disable UnusedAutoPropertyAccessor.Global

public record SdkMessageViewModel
{
    public SdkMessageViewModel((string Name, string Message) sdkm)
    {
        Name = sdkm.Name;
        Message = sdkm.Message;
    }

    public string Message { get; set; }

    public string Name { get; set; }
}
