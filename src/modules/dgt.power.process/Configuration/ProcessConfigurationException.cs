// Copyright (c) DIGITALL Nature. All rights reserved

namespace dgt.power.process.Configuration;

public sealed class ProcessConfigurationException : Exception
{
    public ProcessConfigurationException()
        : base("Invalid process configuration.")
    {
    }

    public ProcessConfigurationException(string message)
        : base(message)
    {
    }

    public ProcessConfigurationException(string message, Exception innerException)
        : base(message, innerException)
    {
    }
}
