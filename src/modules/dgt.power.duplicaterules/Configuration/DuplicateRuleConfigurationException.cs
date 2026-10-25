// Copyright (c) DIGITALL Nature. All rights reserved

namespace dgt.power.duplicaterules.Configuration;

public sealed class DuplicateRuleConfigurationException : Exception
{
    // ReSharper disable once UnusedMember.Global -- Required by the standard exception constructor contract.
    public DuplicateRuleConfigurationException()
        : base("Invalid duplicate-rule configuration.")
    {
    }

    public DuplicateRuleConfigurationException(string message)
        : base(message)
    {
    }

    public DuplicateRuleConfigurationException(string message, Exception innerException)
        : base(message, innerException)
    {
    }
}
