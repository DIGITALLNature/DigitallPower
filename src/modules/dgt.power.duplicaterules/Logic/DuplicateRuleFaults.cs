// Copyright (c) DIGITALL Nature. All rights reserved

using System.ServiceModel;
using Microsoft.Xrm.Sdk;

namespace dgt.power.duplicaterules.Logic;

public static class DuplicateRuleFaults
{
    public const int NoCriteria = unchecked((int)0x80048414);
    public const int MaximumPublishedRules = unchecked((int)0x80048419);
    public const int AlreadyPublishing = unchecked((int)0x80048434);

    private static int? FindErrorCode(Exception exception)
    {
        for (var current = exception; current is not null; current = current.InnerException)
        {
            if (current is FaultException<OrganizationServiceFault> { Detail: { } fault })
            {
                return fault.ErrorCode;
            }
        }

        return null;
    }

    public static string DescribePublishFailure(Exception exception)
    {
        ArgumentNullException.ThrowIfNull(exception);
        return FindErrorCode(exception) switch
        {
            MaximumPublishedRules => $"no publication slot available on the base table (0x80048419): {exception.Message}",
            NoCriteria => $"the rule has no criteria and cannot be published (0x80048414): {exception.Message}",
            AlreadyPublishing => $"the rule is already being published by another operation (0x80048434): {exception.Message}",
            _ => exception.Message
        };
    }
}
