// Copyright (c) DIGITALL Nature. All rights reserved

namespace dgt.power.process.Configuration;

public sealed class ProcessConfigurationException(string message, Exception? innerException = null)
    : Exception(message, innerException)
{
}
