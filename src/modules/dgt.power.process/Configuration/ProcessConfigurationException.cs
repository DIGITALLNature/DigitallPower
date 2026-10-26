// Copyright (c) DIGITALL Nature. All rights reserved

namespace dgt.power.process.Configuration;

public sealed class ProcessConfigurationException : Exception
{
    // ReSharper disable once UnusedMember.Global -- Required by the standard exception constructor contract.
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
