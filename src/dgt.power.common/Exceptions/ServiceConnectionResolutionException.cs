// SPDX-FileCopyrightText: DIGITALL Nature
// SPDX-License-Identifier: MS-RL

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
