// Copyright (c) DIGITALL Nature. All rights reserved
// DIGITALL Nature licenses this file to you under the Microsoft Public License.

namespace dgt.power.common.Exceptions;

/// <summary>
/// Thrown when an Azure DevOps service connection could not be resolved by name via the
/// Azure DevOps REST API (missing/ambiguous name, missing permissions, or unreachable API).
/// </summary>
[Serializable]
public class ServiceConnectionResolutionException : AbstractPowerException
{
    public ServiceConnectionResolutionException()
    {
    }

    public ServiceConnectionResolutionException(string message) : base(message)
    {
    }

    public ServiceConnectionResolutionException(string message, Exception innerException) : base(message, innerException)
    {
    }
}
