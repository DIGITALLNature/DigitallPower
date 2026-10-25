// Copyright (c) DIGITALL Nature. All rights reserved

namespace dgt.power.teamtemplates.Configuration;

public sealed class TeamTemplateConfigurationException : InvalidOperationException
{
    public TeamTemplateConfigurationException() { }

    public TeamTemplateConfigurationException(string message) : base(message) { }

    public TeamTemplateConfigurationException(string message, Exception innerException) : base(message, innerException) { }
}
