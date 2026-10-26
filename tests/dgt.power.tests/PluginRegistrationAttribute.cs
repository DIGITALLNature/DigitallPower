// SPDX-FileCopyrightText: DIGITALL Nature
// SPDX-License-Identifier: MS-RL

// ReSharper disable CheckNamespace
// ReSharper disable UnusedMember.Global

namespace Digitall.Plugins.Registration;

[AttributeUsage(AttributeTargets.Class)]
public sealed class PluginRegistrationAttribute(string messageName, int mode, int stage) : Attribute
{
    public string MessageName => messageName;

    public int Mode => mode;

    public int Stage => stage;

    // ReSharper disable once UnusedAutoPropertyAccessor.Global
    public string? PrimaryEntityName { get; set; }

    public string? SecondaryEntityName { get; set; }

    // ReSharper disable once UnusedAutoPropertyAccessor.Global
    public int ExecutionOrder { get; set; }

    public string? Configuration { get; set; }

    public bool PreEntityImage { get; set; }

    public bool PostEntityImage { get; set; }

    public string[]? FilterAttributes { get; set; }

    public string[]? PreEntityImageAttributes { get; set; }

    public string[]? PostEntityImageAttributes { get; set; }
}
