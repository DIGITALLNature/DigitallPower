// Copyright (c) DIGITALL Nature. All rights reserved

namespace dgt.power.teamtemplates.Configuration;

public sealed class TeamTemplateConfigurationException : InvalidOperationException
{
    // ReSharper disable once UnusedMember.Global — required by the standard exception constructor contract (CA1032).
    public TeamTemplateConfigurationException() { }

    public TeamTemplateConfigurationException(string message) : base(message) { }

    // ReSharper disable once UnusedMember.Global — required by the standard exception constructor contract (CA1032).
    public TeamTemplateConfigurationException(string message, Exception innerException) : base(message, innerException) { }
}
