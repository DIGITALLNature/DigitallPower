// Copyright (c) DIGITALL Nature. All rights reserved
// DIGITALL Nature licenses this file to you under the Microsoft Public License.

namespace dgt.registration;

[AttributeUsage(AttributeTargets.Class)]
public sealed class PluginRegistrationAttribute(string messageName, int mode, int stage) : Attribute
{
    public string MessageName => messageName;

    public int Mode => mode;

    public int Stage => stage;

    public string? PrimaryEntityName { get; set; }

    public string? SecondaryEntityName { get; set; }

    public int ExecutionOrder { get; set; }

    public string? Configuration { get; set; }

    public bool PreEntityImage { get; set; }

    public bool PostEntityImage { get; set; }

    public string[]? FilterAttributes { get; set; }

    public string[]? PreEntityImageAttributes { get; set; }

    public string[]? PostEntityImageAttributes { get; set; }
}
